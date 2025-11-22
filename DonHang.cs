using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GiaoDienDangNhap
{
    public partial class DonHang : Form
    {
        // Connection string - Thay đổi theo máy của bạn
        string connectionString = @"Data Source=HUYNE;Initial Catalog=QUANLY_PETSHOP_V9;Integrated Security=True";

        public DonHang()
        {
            InitializeComponent();
        }

        // Form Load Event
        private void DonHang_Load(object sender, EventArgs e)
        {
            // Cấu hình PictureBox
            if (pictureBox1 != null)
            {
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }

            LoadComboBoxTrangThai();
            LoadDonHang();
            SetReadOnlyFields();

            // Thêm event CellClick
            dataGridView1.CellClick += dataGridView1_CellClick;
        }

        // Thiết lập các textbox ReadOnly
        private void SetReadOnlyFields()
        {
            // Đơn hàng - cho phép sửa mã đơn hàng để tìm kiếm
            txt_madonhang.ReadOnly = false; // Cho phép nhập để tìm
            txt_makh.ReadOnly = true;
            txt_tongtien.ReadOnly = true;
            dateTimePicker1.Enabled = false;
            cb_trangthai.Enabled = false;

            // Chi tiết đơn hàng - tất cả readonly
            txt_madonhang_chitietdonhang.ReadOnly = true;
            txt_loaisanpham.ReadOnly = true;
            txt_mathucung.ReadOnly = true;
            txt_masanpham.ReadOnly = true;
            txt_soluong.ReadOnly = true;
            txt_dongia.ReadOnly = true;
            txt_thanhtien.ReadOnly = true;
        }

        // Load ComboBox Trạng thái
        private void LoadComboBoxTrangThai()
        {
            cb_trangthai.Items.Clear();
            cb_trangthai.Items.Add("Chờ xử lý");
            cb_trangthai.Items.Add("Đang giao");
            cb_trangthai.Items.Add("Hoàn thành");
            cb_trangthai.Items.Add("Đã hủy");
        }

        // Load dữ liệu Đơn Hàng lên DataGridView
        private void LoadDonHang()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT MaDonHang, MaKH, NgayDat, TongTien, TrangThai FROM DonHang";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridView1.DataSource = dt;

                    // Tùy chỉnh header
                    dataGridView1.Columns["MaDonHang"].HeaderText = "Mã Đơn Hàng";
                    dataGridView1.Columns["MaKH"].HeaderText = "Mã Khách Hàng";
                    dataGridView1.Columns["NgayDat"].HeaderText = "Ngày Đặt";
                    dataGridView1.Columns["TongTien"].HeaderText = "Tổng Tiền";
                    dataGridView1.Columns["TrangThai"].HeaderText = "Trạng Thái";

                    // Auto size columns
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load dữ liệu: " + ex.Message);
            }
        }

        // Khi click vào 1 dòng trong DataGridView
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            LoadDataFromRow(e.RowIndex);
        }

        // Thêm event CellClick để bắt mọi click vào dòng
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            LoadDataFromRow(e.RowIndex);
        }

        // Load dữ liệu từ dòng được chọn
        private void LoadDataFromRow(int rowIndex)
        {
            if (rowIndex >= 0 && rowIndex < dataGridView1.Rows.Count)
            {
                try
                {
                    DataGridViewRow row = dataGridView1.Rows[rowIndex];

                    // Kiểm tra null trước khi lấy dữ liệu
                    if (row.Cells["MaDonHang"].Value != null)
                    {
                        txt_madonhang.Text = row.Cells["MaDonHang"].Value.ToString();
                        txt_makh.Text = row.Cells["MaKH"].Value?.ToString() ?? "";

                        if (row.Cells["NgayDat"].Value != null)
                        {
                            dateTimePicker1.Value = Convert.ToDateTime(row.Cells["NgayDat"].Value);
                        }

                        txt_tongtien.Text = row.Cells["TongTien"].Value?.ToString() ?? "0";
                        cb_trangthai.Text = row.Cells["TrangThai"].Value?.ToString() ?? "";

                        // Load ComboBox Mã CTDH
                        LoadMaCTDHComboBox(txt_madonhang.Text);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi load dữ liệu: " + ex.Message);
                }
            }
        }

        // Load danh sách Mã CTDH vào ComboBox
        private void LoadMaCTDHComboBox(string maDonHang)
        {
            try
            {
                cb_MaCTDH.Items.Clear();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT MaCTDH FROM ChiTietDonHang WHERE MaDonHang = @MaDonHang";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaDonHang", maDonHang);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        cb_MaCTDH.Items.Add(reader["MaCTDH"].ToString());
                    }

                    if (cb_MaCTDH.Items.Count > 0)
                    {
                        cb_MaCTDH.SelectedIndex = 0; // Chọn item đầu tiên
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load Mã CTDH: " + ex.Message);
            }
        }

        // Load Chi Tiết Đơn Hàng theo Mã CTDH - CÓ HIỂN THỊ ẢNH
        private void LoadChiTietDonHang(string maCTDH)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Query JOIN với ThuCung và SanPhamPhuKien để lấy ảnh
                    string query = @"SELECT 
                                    ctdh.MaCTDH, ctdh.MaDonHang, ctdh.LoaiSanPham, 
                                    ctdh.MaThuCung, ctdh.MaSP, ctdh.SoLuong, ctdh.DonGia, ctdh.ThanhTien,
                                    tc.HinhAnh AS HinhAnhThuCung, tc.TenThuCung,
                                    sp.HinhAnh AS HinhAnhSanPham, sp.TenSP
                                    FROM ChiTietDonHang ctdh
                                    LEFT JOIN ThuCung tc ON ctdh.MaThuCung = tc.MaThuCung
                                    LEFT JOIN SanPhamPhuKien sp ON ctdh.MaSP = sp.MaSP
                                    WHERE ctdh.MaCTDH = @MaCTDH";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaCTDH", maCTDH);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        txt_madonhang_chitietdonhang.Text = reader["MaDonHang"].ToString();
                        txt_loaisanpham.Text = reader["LoaiSanPham"].ToString();
                        txt_mathucung.Text = reader["MaThuCung"] != DBNull.Value ? reader["MaThuCung"].ToString() : "";
                        txt_masanpham.Text = reader["MaSP"] != DBNull.Value ? reader["MaSP"].ToString() : "";
                        txt_soluong.Text = reader["SoLuong"].ToString();
                        txt_dongia.Text = reader["DonGia"].ToString();
                        txt_thanhtien.Text = reader["ThanhTien"].ToString();

                        // ===== HIỂN THỊ ẢNH =====
                        string loaiSanPham = reader["LoaiSanPham"].ToString();

                        if (loaiSanPham == "Thú cưng")
                        {
                            // Load ảnh thú cưng
                            if (reader["HinhAnhThuCung"] != DBNull.Value && !string.IsNullOrWhiteSpace(reader["HinhAnhThuCung"].ToString()))
                            {
                                string fileName = reader["HinhAnhThuCung"].ToString().Trim();
                                LoadHinhAnh(fileName, "ThuCung");
                            }
                            else
                            {
                                ClearPictureBox();
                            }
                        }
                        else if (loaiSanPham == "Phụ kiện")
                        {
                            // Load ảnh sản phẩm phụ kiện
                            if (reader["HinhAnhSanPham"] != DBNull.Value && !string.IsNullOrWhiteSpace(reader["HinhAnhSanPham"].ToString()))
                            {
                                string fileName = reader["HinhAnhSanPham"].ToString().Trim();
                                LoadHinhAnh(fileName, "SanPham");
                            }
                            else
                            {
                                ClearPictureBox();
                            }
                        }
                        else
                        {
                            ClearPictureBox();
                        }
                    }
                    else
                    {
                        // Clear nếu không có chi tiết
                        ClearChiTietFields();
                        ClearPictureBox();
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load chi tiết: " + ex.Message);
            }
        }

        // Hàm load hình ảnh vào PictureBox
        // ===========================================================================================
        // THAY THẾ HÀM LoadHinhAnh TRONG FILE DonHang.cs
        // ===========================================================================================

        // Hàm load hình ảnh vào PictureBox - ĐÃ SỬA
        private void LoadHinhAnh(string fileName, string loaiFolder)
        {
            try
            {
                string projectPath = Application.StartupPath;
                string solutionPath = Directory.GetParent(Directory.GetParent(projectPath).FullName).FullName;

                string folder = "";

                if (loaiFolder == "ThuCung")
                {
                    // Thử cả 2 tên thư mục cho Thú Cưng
                    folder = solutionPath + "\\Images\\ThuCung\\";
                    if (!Directory.Exists(folder))
                    {
                        folder = solutionPath + "\\Images\\Thucung\\";
                    }
                }
                else if (loaiFolder == "SanPham")
                {
                    // ✅ SỬA: Thử cả 2 tên thư mục cho Sản Phẩm Phụ Kiện
                    folder = solutionPath + "\\Images\\SanPhamPhuKien\\";
                    if (!Directory.Exists(folder))
                    {
                        folder = solutionPath + "\\Images\\SanPhamPhukien\\";
                    }
                }

                string fullPath = folder + fileName;

                if (File.Exists(fullPath))
                {
                    // Giải phóng ảnh cũ
                    if (pictureBox1.Image != null)
                    {
                        var oldImage = pictureBox1.Image;
                        pictureBox1.Image = null;
                        oldImage.Dispose();
                    }

                    // Load ảnh mới
                    using (var stream = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
                    {
                        pictureBox1.Image = Image.FromStream(stream);
                    }
                }
                else
                {
                    ClearPictureBox();

                    // Debug: Hiển thị thông báo nếu không tìm thấy file
                    // MessageBox.Show($"Không tìm thấy file: {fullPath}", "Thông báo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi load ảnh: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ClearPictureBox();
            }
        }

       
        private void ClearPictureBox()
        {
            if (pictureBox1 != null && pictureBox1.Image != null)
            {
                var oldImage = pictureBox1.Image;
                pictureBox1.Image = null;
                oldImage.Dispose();
            }
        }

        // Clear các textbox chi tiết
        private void ClearChiTietFields()
        {
            txt_madonhang_chitietdonhang.Clear();
            txt_loaisanpham.Clear();
            txt_mathucung.Clear();
            txt_masanpham.Clear();
            txt_soluong.Clear();
            txt_dongia.Clear();
            txt_thanhtien.Clear();
            cb_MaCTDH.Items.Clear();
        }

        // Nút Hủy đơn hàng
        private void btn_huydonhangnay_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_madonhang.Text))
            {
                MessageBox.Show("Vui lòng chọn đơn hàng cần hủy!");
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc muốn hủy đơn hàng này?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        string query = "UPDATE DonHang SET TrangThai = N'Đã hủy' WHERE MaDonHang = @MaDonHang";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@MaDonHang", txt_madonhang.Text);

                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Đã hủy đơn hàng thành công!");
                            LoadDonHang(); // Reload lại data
                            ClearFields();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        // Clear các textbox
        private void ClearFields()
        {
            txt_madonhang.Clear();
            txt_makh.Clear();
            txt_tongtien.Clear();
            cb_trangthai.SelectedIndex = -1;
            dateTimePicker1.Value = DateTime.Now;
            ClearChiTietFields();
            ClearPictureBox();
        }

        // Event khi chọn Mã CTDH trong ComboBox
        private void cb_MaCTDH_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_MaCTDH.SelectedItem != null)
            {
                LoadChiTietDonHang(cb_MaCTDH.SelectedItem.ToString());
            }
        }

        // CÁC HÀM TRỐNG
        private void txt_madonhang_TextChanged(object sender, EventArgs e) { }
        private void txt_makh_TextChanged(object sender, EventArgs e) { }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e) { }
        private void txt_tongtien_TextChanged(object sender, EventArgs e) { }
        private void cb_trangthai_SelectedIndexChanged(object sender, EventArgs e) { }
        private void txt_MaCtdh_TextChanged(object sender, EventArgs e) { }
        private void txt_madonhang_chitietdonhang_TextChanged(object sender, EventArgs e) { }
        private void txt_loaisanpham_TextChanged(object sender, EventArgs e) { }
        private void txt_mathucung_TextChanged(object sender, EventArgs e) { }
        private void txt_masanpham_TextChanged(object sender, EventArgs e) { }
        private void txt_soluong_TextChanged(object sender, EventArgs e) { }
        private void txt_dongia_TextChanged(object sender, EventArgs e) { }
        private void txt_thanhtien_TextChanged(object sender, EventArgs e) { }
        private void groupBox2_Enter(object sender, EventArgs e) { }
        private void gb_donhang_Enter(object sender, EventArgs e) { }
        private void pictureBox1_Click(object sender, EventArgs e) { }

        // ===========================================================================================
        // THAY THẾ HÀM btn_xoa_Click TRONG FILE DonHang.cs
        // ===========================================================================================

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            // Kiểm tra đã chọn chi tiết đơn hàng chưa
            if (cb_MaCTDH.SelectedItem == null || string.IsNullOrWhiteSpace(cb_MaCTDH.SelectedItem.ToString()))
            {
                MessageBox.Show("Vui lòng chọn chi tiết đơn hàng cần xóa!",
                               "Thông báo",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Warning);
                return;
            }

            string maCTDH = cb_MaCTDH.SelectedItem.ToString();
            string loaiSanPham = txt_loaisanpham.Text;
            string maThuCung = txt_mathucung.Text;
            string maSanPham = txt_masanpham.Text;
            int soLuong = 0;

            // Lấy số lượng
            if (!int.TryParse(txt_soluong.Text, out soLuong) || soLuong <= 0)
            {
                MessageBox.Show("Số lượng không hợp lệ!", "Lỗi");
                return;
            }

            // Xác nhận xóa
            DialogResult result = MessageBox.Show(
                $"Bạn có chắc muốn xóa chi tiết đơn hàng này?\n" +
                $"Mã CTDH: {maCTDH}\n" +
                $"Loại: {loaiSanPham}\n" +
                $"Số lượng: {soLuong}\n\n" +
                $"Số lượng sẽ được hoàn trả lại kho!",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        SqlTransaction transaction = conn.BeginTransaction();

                        try
                        {
                            // Bước 1: Trả lại số lượng cho sản phẩm/thú cưng
                            if (loaiSanPham == "Thú cưng" && !string.IsNullOrWhiteSpace(maThuCung))
                            {
                                // Kiểm tra xem thú cưng còn tồn tại không
                                string sqlCheck = "SELECT COUNT(*) FROM ThuCung WHERE MaThuCung = @Ma";
                                SqlCommand cmdCheck = new SqlCommand(sqlCheck, conn, transaction);
                                cmdCheck.Parameters.AddWithValue("@Ma", maThuCung);
                                int exists = (int)cmdCheck.ExecuteScalar();

                                if (exists > 0)
                                {
                                    // Nếu còn tồn tại → Cộng thêm số lượng
                                    string sqlUpdate = "UPDATE ThuCung SET SoLuong = SoLuong + @SoLuong WHERE MaThuCung = @Ma";
                                    SqlCommand cmdUpdate = new SqlCommand(sqlUpdate, conn, transaction);
                                    cmdUpdate.Parameters.AddWithValue("@Ma", maThuCung);
                                    cmdUpdate.Parameters.AddWithValue("@SoLuong", soLuong);
                                    cmdUpdate.ExecuteNonQuery();
                                }
                                else
                                {
                                    // Nếu đã bị xóa → Tạo lại với số lượng ban đầu
                                    // (Cần thêm thông tin khác từ database - tạm thời bỏ qua)
                                    MessageBox.Show(
                                        $"Thú cưng {maThuCung} đã bị xóa khỏi hệ thống.\n" +
                                        "Không thể hoàn trả số lượng!",
                                        "Cảnh báo",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning
                                    );
                                }
                            }
                            else if (loaiSanPham == "Phụ kiện" && !string.IsNullOrWhiteSpace(maSanPham))
                            {
                                // Kiểm tra xem sản phẩm còn tồn tại không
                                string sqlCheck = "SELECT COUNT(*) FROM SanPhamPhuKien WHERE MaSP = @Ma";
                                SqlCommand cmdCheck = new SqlCommand(sqlCheck, conn, transaction);
                                cmdCheck.Parameters.AddWithValue("@Ma", maSanPham);
                                int exists = (int)cmdCheck.ExecuteScalar();

                                if (exists > 0)
                                {
                                    // Nếu còn tồn tại → Cộng thêm số lượng
                                    string sqlUpdate = "UPDATE SanPhamPhuKien SET SoLuong = SoLuong + @SoLuong WHERE MaSP = @Ma";
                                    SqlCommand cmdUpdate = new SqlCommand(sqlUpdate, conn, transaction);
                                    cmdUpdate.Parameters.AddWithValue("@Ma", maSanPham);
                                    cmdUpdate.Parameters.AddWithValue("@SoLuong", soLuong);
                                    cmdUpdate.ExecuteNonQuery();
                                }
                                else
                                {
                                    MessageBox.Show(
                                        $"Sản phẩm {maSanPham} đã bị xóa khỏi hệ thống.\n" +
                                        "Không thể hoàn trả số lượng!",
                                        "Cảnh báo",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning
                                    );
                                }
                            }

                            // Bước 2: Xóa chi tiết đơn hàng
                            string sqlDeleteCTDH = "DELETE FROM ChiTietDonHang WHERE MaCTDH = @MaCTDH";
                            SqlCommand cmdDeleteCTDH = new SqlCommand(sqlDeleteCTDH, conn, transaction);
                            cmdDeleteCTDH.Parameters.AddWithValue("@MaCTDH", maCTDH);
                            int rowsDeleted = cmdDeleteCTDH.ExecuteNonQuery();

                            // Bước 3: Cập nhật tổng tiền đơn hàng
                            string maDonHang = txt_madonhang.Text;
                            string sqlUpdateTongTien = @"
                        UPDATE DonHang 
                        SET TongTien = (
                            SELECT ISNULL(SUM(ThanhTien), 0) 
                            FROM ChiTietDonHang 
                            WHERE MaDonHang = @MaDonHang
                        )
                        WHERE MaDonHang = @MaDonHang";

                            SqlCommand cmdUpdateTongTien = new SqlCommand(sqlUpdateTongTien, conn, transaction);
                            cmdUpdateTongTien.Parameters.AddWithValue("@MaDonHang", maDonHang);
                            cmdUpdateTongTien.ExecuteNonQuery();

                            // Commit transaction
                            transaction.Commit();

                            if (rowsDeleted > 0)
                            {
                                MessageBox.Show(
                                    "Xóa chi tiết đơn hàng thành công!\n" +
                                    $"Đã hoàn trả {soLuong} sản phẩm vào kho.",
                                    "Thành công",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information
                                );

                                // Reload lại dữ liệu
                                LoadDonHang();

                                // Load lại combobox chi tiết
                                LoadMaCTDHComboBox(maDonHang);

                                // Clear các textbox chi tiết
                                ClearChiTietFields();
                                ClearPictureBox();

                                // Load lại tổng tiền
                                string sqlGetTongTien = "SELECT TongTien FROM DonHang WHERE MaDonHang = @MaDonHang";
                                SqlCommand cmdGetTongTien = new SqlCommand(sqlGetTongTien, conn);
                                cmdGetTongTien.Parameters.AddWithValue("@MaDonHang", maDonHang);
                                object tongTienObj = cmdGetTongTien.ExecuteScalar();
                                if (tongTienObj != null)
                                {
                                    txt_tongtien.Text = tongTienObj.ToString();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            // Rollback nếu có lỗi
                            transaction.Rollback();
                            throw ex;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        $"Lỗi khi xóa chi tiết đơn hàng:\n{ex.Message}",
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
        }

        private void btn_quaylai_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}