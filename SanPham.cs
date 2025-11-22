using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace GiaoDienDangNhap
{
    public partial class SanPham : Form
    {
        // ===== KHAI BÁO BIẾN =====
        SqlConnection conn;
        SqlDataAdapter adapter;
        DataTable dt;
        string connectionString = @"Data Source=HUYNE;Initial Catalog=QUANLY_PETSHOP_V9;Integrated Security=True;TrustServerCertificate=True";
        bool isAddNew = false;
        string imagePath = "";

        public SanPham()
        {
            InitializeComponent();
            // XÓA dòng này để tránh đăng ký event 2 lần
            // this.Load += SanPham_Load;
        }

        // ===== FORM LOAD - CHỈ GIỮ MỘT METHOD =====
        private void SanPham_Load(object sender, EventArgs e)
        {
            try
            {
                conn = new SqlConnection(connectionString);

                // Cấu hình PictureBox
                if (pictureBox1 != null)
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

                // Tăng chiều cao của dòng trong DataGridView
                if (datagriw_sanphamphukien != null)
                {
                    datagriw_sanphamphukien.RowTemplate.Height = 80;

                    // Đăng ký event SelectionChanged (nếu chưa có trong Designer)
                    datagriw_sanphamphukien.SelectionChanged += datagriw_sanphamphukien_SelectionChanged;

                    // Set chế độ chọn cả dòng
                    datagriw_sanphamphukien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    datagriw_sanphamphukien.MultiSelect = false;
                }

                // Test kết nối
                conn.Open();
                conn.Close();

                LoadDanhSach();
                LoadNhaCungCap();

                KhoaControls(true);
                BatTatNut(true, true, true, false, false);

                // Load ảnh SAU KHI form đã hiển thị hoàn toàn
                this.Shown += (s, ev) => LoadAnhVaoGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message + "\n\nStack: " + ex.StackTrace,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===== LOAD DANH SÁCH SẢN PHẨM =====
        private void LoadDanhSach()
        {
            try
            {
                string sql = @"SELECT sp.MaSP, sp.TenSP, ncc.TenNCC, 
                              sp.GiaBan, sp.SoLuong, sp.MoTa, sp.HinhAnh, sp.MaNCC
                              FROM SanPhamPhuKien sp
                              INNER JOIN NhaCungCap ncc ON sp.MaNCC = ncc.MaNCC";

                adapter = new SqlDataAdapter(sql, conn);
                dt = new DataTable();
                adapter.Fill(dt);

                // Xóa cột Image cũ nếu có (để tránh duplicate)
                if (datagriw_sanphamphukien.Columns.Contains("HinhAnh_Image"))
                {
                    datagriw_sanphamphukien.Columns.Remove("HinhAnh_Image");
                }

                datagriw_sanphamphukien.DataSource = dt;

                // Ẩn cột HinhAnh gốc (chứa tên file text)
                if (datagriw_sanphamphukien.Columns.Contains("HinhAnh"))
                    datagriw_sanphamphukien.Columns["HinhAnh"].Visible = false;

                // Ẩn cột MaNCC
                if (datagriw_sanphamphukien.Columns["MaNCC"] != null)
                    datagriw_sanphamphukien.Columns["MaNCC"].Visible = false;

                // Tạo cột Image MỚI
                DataGridViewImageColumn imgCol = new DataGridViewImageColumn();
                imgCol.Name = "HinhAnh_Image";
                imgCol.HeaderText = "Hình Ảnh";
                imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
                imgCol.Width = 120;
                imgCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                datagriw_sanphamphukien.Columns.Insert(0, imgCol);

                // Đặt tên hiển thị
                datagriw_sanphamphukien.Columns["MaSP"].HeaderText = "Mã SP";
                datagriw_sanphamphukien.Columns["TenSP"].HeaderText = "Tên sản phẩm";
                datagriw_sanphamphukien.Columns["TenNCC"].HeaderText = "Nhà cung cấp";
                datagriw_sanphamphukien.Columns["GiaBan"].HeaderText = "Giá bán";
                datagriw_sanphamphukien.Columns["SoLuong"].HeaderText = "Số lượng";
                datagriw_sanphamphukien.Columns["MoTa"].HeaderText = "Mô tả";

                // Format giá tiền
                datagriw_sanphamphukien.Columns["GiaBan"].DefaultCellStyle.Format = "N0";

                // Auto resize columns
                datagriw_sanphamphukien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load dữ liệu: " + ex.Message + "\n\nStack: " + ex.StackTrace,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===== LOAD HÌNH ẢNH VÀO DATAGRIDVIEW =====
        private void LoadAnhVaoGrid()
        {
            try
            {
                // Tìm đường dẫn đúng
                string projectPath = Application.StartupPath;
                string solutionPath = Directory.GetParent(Directory.GetParent(projectPath).FullName).FullName;

                // Thử cả 2 tên thư mục
                string folder = solutionPath + "\\Images\\SanPhamPhuKien\\";
                if (!Directory.Exists(folder))
                {
                    folder = solutionPath + "\\Images\\SanPhamPhukien\\";
                }

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                    return;
                }

                foreach (DataGridViewRow row in datagriw_sanphamphukien.Rows)
                {
                    if (row.IsNewRow) continue;

                    try
                    {
                        var val = row.Cells["HinhAnh"].Value;
                        if (val == null || string.IsNullOrWhiteSpace(val.ToString()))
                        {
                            row.Cells["HinhAnh_Image"].Value = null;
                            continue;
                        }

                        string file = val.ToString().Trim();
                        string fullPath = folder + file;

                        if (File.Exists(fullPath))
                        {
                            using (var stream = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
                            {
                                Image img = Image.FromStream(stream);
                                row.Cells["HinhAnh_Image"].Value = new Bitmap(img);
                                img.Dispose();
                            }
                        }
                        else
                        {
                            row.Cells["HinhAnh_Image"].Value = null;
                        }
                    }
                    catch
                    {
                        row.Cells["HinhAnh_Image"].Value = null;
                    }
                }
            }
            catch
            {
                // Không hiển thị lỗi để tránh gián đoạn
            }
        }

        // ===== LOAD NHÀ CUNG CẤP VÀO COMBOBOX =====
        private void LoadNhaCungCap()
        {
            try
            {
                string sql = "SELECT MaNCC, TenNCC FROM NhaCungCap";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dtNCC = new DataTable();
                da.Fill(dtNCC);

                cb_nhacc.DataSource = dtNCC;
                cb_nhacc.DisplayMember = "TenNCC";
                cb_nhacc.ValueMember = "MaNCC";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load nhà cung cấp: " + ex.Message);
            }
        }

        // ===== SELECTION CHANGED - HIỂN THỊ THÔNG TIN =====
        private void datagriw_sanphamphukien_SelectionChanged(object sender, EventArgs e)
        {
            if (datagriw_sanphamphukien.SelectedRows.Count > 0)
            {
                try
                {
                    DataGridViewRow row = datagriw_sanphamphukien.SelectedRows[0];

                    // Hiển thị thông tin lên các textbox - XỬ LÝ NULL/EMPTY
                    textBox1.Text = row.Cells["MaSP"].Value?.ToString() ?? "";
                    textBox3.Text = row.Cells["TenSP"].Value?.ToString() ?? "";

                    // Xử lý giá bán - chỉ hiển thị số, không có format
                    var giaBanValue = row.Cells["GiaBan"].Value;
                    if (giaBanValue != null && giaBanValue != DBNull.Value)
                    {
                        try
                        {
                            decimal giaBan = Convert.ToDecimal(giaBanValue);
                            textBox5.Text = giaBan.ToString("0"); // Hiển thị số thuần không có dấu phẩy
                        }
                        catch
                        {
                            textBox5.Text = "";
                        }
                    }
                    else
                    {
                        textBox5.Text = "";
                    }

                    // Xử lý số lượng
                    var soLuongValue = row.Cells["SoLuong"].Value;
                    if (soLuongValue != null && soLuongValue != DBNull.Value)
                    {
                        try
                        {
                            int soLuong = Convert.ToInt32(soLuongValue);
                            textBox4.Text = soLuong.ToString();
                        }
                        catch
                        {
                            textBox4.Text = "";
                        }
                    }
                    else
                    {
                        textBox4.Text = "";
                    }

                    txt_MoTaSanPham.Text = row.Cells["MoTa"].Value?.ToString() ?? "";

                    // Set nhà cung cấp
                    if (row.Cells["MaNCC"] != null && row.Cells["MaNCC"].Value != null)
                    {
                        cb_nhacc.SelectedValue = row.Cells["MaNCC"].Value;
                    }
                    else
                    {
                        if (cb_nhacc.Items.Count > 0)
                            cb_nhacc.SelectedIndex = 0;
                    }

                    // Hiển thị ảnh
                    var hinhAnhValue = row.Cells["HinhAnh"].Value;
                    if (hinhAnhValue != null && !string.IsNullOrWhiteSpace(hinhAnhValue.ToString()))
                    {
                        string file = hinhAnhValue.ToString().Trim();
                        string projectPath = Application.StartupPath;
                        string solutionPath = Directory.GetParent(Directory.GetParent(projectPath).FullName).FullName;

                        // Thử cả 2 tên thư mục
                        string fullPath = solutionPath + "\\Images\\SanPhamPhuKien\\" + file;
                        if (!File.Exists(fullPath))
                        {
                            fullPath = solutionPath + "\\Images\\SanPhamPhukien\\" + file;
                        }

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
                            imagePath = file;
                        }
                        else
                        {
                            pictureBox1.Image = null;
                            imagePath = "";
                        }
                    }
                    else
                    {
                        pictureBox1.Image = null;
                        imagePath = "";
                    }

                    // Enable các nút Sửa và Xóa
                    tool_sua.Enabled = true;
                    tool_xoa.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi hiển thị thông tin: " + ex.Message + "\n\nStack: " + ex.StackTrace, "Lỗi");
                }
            }
        }

        // ===== CLICK VÀO DÒNG TRONG DATAGRIDVIEW =====
        private void datagriw_sanphamphukien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Event này không cần xử lý gì vì SelectionChanged đã xử lý rồi
            // Để trống
        }

        // ===== NÚT THÊM =====
        private void tool_them_Click(object sender, EventArgs e)
        {
            isAddNew = true;
            KhoaControls(false);
            ClearForm();

            textBox1.Text = TaoMaSP();
            textBox1.Enabled = false;

            BatTatNut(false, false, false, true, true);
            textBox3.Focus();
        }

        // ===== TẠO MÃ SẢN PHẨM TỰ ĐỘNG =====
        private string TaoMaSP()
        {
            try
            {
                string sql = "SELECT TOP 1 MaSP FROM SanPhamPhuKien ORDER BY MaSP DESC";
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                object result = cmd.ExecuteScalar();
                conn.Close();

                if (result != null)
                {
                    string lastMa = result.ToString();
                    int number = int.Parse(lastMa.Substring(2)) + 1;
                    return "SP" + number.ToString("D3");
                }
                return "SP001";
            }
            catch
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                return "SP001";
            }
        }

        // ===== NÚT SỬA =====
        private void tool_sua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            isAddNew = false;
            KhoaControls(false);
            textBox1.Enabled = false;

            BatTatNut(false, false, false, true, true);
            textBox3.Focus();
        }

        // ===== NÚT XÓA =====
        private void tool_xoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn xóa sản phẩm: " + textBox3.Text + "?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    string sql = "DELETE FROM SanPhamPhuKien WHERE MaSP = @MaSP";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@MaSP", textBox1.Text);

                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    conn.Close();

                    if (rows > 0)
                    {
                        MessageBox.Show("Xóa thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDanhSach();
                        LoadAnhVaoGrid();
                        ClearForm();
                    }
                }
                catch (Exception ex)
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    MessageBox.Show("Lỗi xóa: " + ex.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ===== NÚT LƯU =====
        private void tool_luu_Click(object sender, EventArgs e)
        {
            // Validate
            if (string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("Vui lòng nhập tên sản phẩm!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox3.Focus();
                return;
            }

            if (cb_nhacc.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(textBox5.Text))
            {
                MessageBox.Show("Vui lòng nhập giá bán!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox5.Focus();
                return;
            }

            if (string.IsNullOrEmpty(textBox4.Text))
            {
                MessageBox.Show("Vui lòng nhập số lượng!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox4.Focus();
                return;
            }

            try
            {
                string sql = "";
                if (isAddNew)
                {
                    sql = @"INSERT INTO SanPhamPhuKien (MaSP, TenSP, MaNCC, GiaBan, SoLuong, MoTa, HinhAnh)
                           VALUES (@MaSP, @TenSP, @MaNCC, @GiaBan, @SoLuong, @MoTa, @HinhAnh)";
                }
                else
                {
                    sql = @"UPDATE SanPhamPhuKien 
                           SET TenSP = @TenSP, MaNCC = @MaNCC, GiaBan = @GiaBan, 
                               SoLuong = @SoLuong, MoTa = @MoTa, HinhAnh = @HinhAnh
                           WHERE MaSP = @MaSP";
                }

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaSP", textBox1.Text);
                cmd.Parameters.AddWithValue("@TenSP", textBox3.Text);
                cmd.Parameters.AddWithValue("@MaNCC", cb_nhacc.SelectedValue);
                cmd.Parameters.AddWithValue("@GiaBan", decimal.Parse(textBox5.Text));
                cmd.Parameters.AddWithValue("@SoLuong", int.Parse(textBox4.Text));
                cmd.Parameters.AddWithValue("@MoTa", txt_MoTaSanPham.Text);
                cmd.Parameters.AddWithValue("@HinhAnh", imagePath);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                conn.Close();

                if (rows > 0)
                {
                    string msg = isAddNew ? "Thêm thành công!" : "Cập nhật thành công!";
                    MessageBox.Show(msg, "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadDanhSach();
                    LoadAnhVaoGrid();
                    ClearForm();
                    KhoaControls(true);
                    BatTatNut(true, true, true, false, false);
                }
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                MessageBox.Show("Lỗi lưu dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===== NÚT HỦY =====
        private void tool_huy_Click(object sender, EventArgs e)
        {
            ClearForm();
            KhoaControls(true);
            BatTatNut(true, true, true, false, false);
        }

        // ===== NÚT THÊM ẢNH =====
        private void Btn_ThemAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
            openFile.Title = "Chọn hình ảnh";

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string projectPath = Application.StartupPath;
                    string solutionPath = Directory.GetParent(Directory.GetParent(projectPath).FullName).FullName;
                    string folder = solutionPath + "\\Images\\SanPhamPhuKien\\";

                    if (!Directory.Exists(folder))
                        Directory.CreateDirectory(folder);

                    string fileName = Path.GetFileName(openFile.FileName);
                    string destPath = folder + fileName;

                    if (pictureBox1.Image != null)
                    {
                        var oldImage = pictureBox1.Image;
                        pictureBox1.Image = null;
                        oldImage.Dispose();
                    }

                    File.Copy(openFile.FileName, destPath, true);

                    using (var stream = new FileStream(destPath, FileMode.Open, FileAccess.Read))
                    {
                        pictureBox1.Image = Image.FromStream(stream);
                    }

                    imagePath = fileName;

                    MessageBox.Show("Đã thêm ảnh thành công!", "Thông báo");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi thêm ảnh: " + ex.Message, "Lỗi");
                }
            }
        }

        // ===== NÚT TÌM KIẾM =====
        private void btn_timkiem_Click(object sender, EventArgs e)
        {
            try
            {
                string keyword = txt_timkiem.Text.Trim();

                if (string.IsNullOrEmpty(keyword))
                {
                    LoadDanhSach();
                    LoadAnhVaoGrid();
                    return;
                }

                string sql = @"SELECT sp.MaSP, sp.TenSP, ncc.TenNCC, 
                              sp.GiaBan, sp.SoLuong, sp.MoTa, sp.HinhAnh, sp.MaNCC
                              FROM SanPhamPhuKien sp
                              INNER JOIN NhaCungCap ncc ON sp.MaNCC = ncc.MaNCC
                              WHERE sp.MaSP LIKE @keyword 
                              OR sp.TenSP LIKE @keyword 
                              OR ncc.TenNCC LIKE @keyword";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");

                adapter = new SqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);

                datagriw_sanphamphukien.DataSource = dt;
                LoadAnhVaoGrid();

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy sản phẩm nào!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message);
            }
        }

        // ===== HÀM CLEAR FORM =====
        private void ClearForm()
        {
            textBox1.Clear();
            textBox3.Clear();
            textBox5.Clear();
            textBox4.Clear();
            txt_MoTaSanPham.Clear();

            if (pictureBox1.Image != null)
            {
                var oldImage = pictureBox1.Image;
                pictureBox1.Image = null;
                oldImage.Dispose();
            }

            imagePath = "";

            if (cb_nhacc.Items.Count > 0)
                cb_nhacc.SelectedIndex = 0;
        }

        // ===== HÀM KHÓA/MỞ CONTROLS =====
        private void KhoaControls(bool khoa)
        {
            textBox1.Enabled = !khoa;
            textBox3.Enabled = !khoa;
            textBox5.Enabled = !khoa;
            textBox4.Enabled = !khoa;
            txt_MoTaSanPham.Enabled = !khoa;
            cb_nhacc.Enabled = !khoa;
            Btn_ThemAnh.Enabled = !khoa;
        }

        // ===== HÀM BẬT/TẮT NÚT =====
        private void BatTatNut(bool them, bool sua, bool xoa, bool luu, bool huy)
        {
            tool_them.Enabled = them;
            tool_sua.Enabled = sua;
            tool_xoa.Enabled = xoa;
            tool_luu.Enabled = luu;
            tool_huy.Enabled = huy;
        }

        // ===== CÁC EVENT - XÓA METHOD TRÙNG LẶP =====
        private void txt_timkiem_TextChanged(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void textBox3_TextChanged(object sender, EventArgs e) { }
        private void textBox5_TextChanged(object sender, EventArgs e) { }
        private void textBox4_TextChanged(object sender, EventArgs e) { }
        private void txt_MoTaSanPham_TextChanged(object sender, EventArgs e) { }
        private void cb_nhacc_SelectedIndexChanged(object sender, EventArgs e) { }
        private void pictureBox1_Click(object sender, EventArgs e) { }
        private void label6_Click(object sender, EventArgs e) { }

        // ===== METHOD NÀY CHỈ ĐỂ TRÁNH LỖI DESIGNER =====
        // Bạn có thể xóa sau khi sửa Designer
        private void SanPham_Load_1(object sender, EventArgs e)
        {
            // Không cần làm gì, SanPham_Load đã xử lý
        }

        // ===========================================================================================
        // THAY THẾ 2 HÀM NÀY VÀO CUỐI FILE SanPham.cs (TRƯỚC DẤU } CUỐI CÙNG)
        // ===========================================================================================

        // ===== NÚT MUA SẢN PHẨM PHỤ KIỆN =====
        private void btn_muasanpham_Click(object sender, EventArgs e)
        {
            // Kiểm tra đã chọn sản phẩm chưa
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần mua!",
                               "Thông báo",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra số lượng
            int soLuongHienTai = 0;
            if (!int.TryParse(textBox4.Text, out soLuongHienTai) || soLuongHienTai <= 0)
            {
                MessageBox.Show("Sản phẩm này đã hết hàng!",
                               "Thông báo",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Warning);
                return;
            }

            // Lấy giá bán
            decimal giaBan = 0;
            if (!decimal.TryParse(textBox5.Text, out giaBan))
            {
                MessageBox.Show("Giá bán không hợp lệ!", "Lỗi");
                return;
            }

            // Xác nhận mua
            DialogResult result = MessageBox.Show(
                $"Bạn có muốn mua sản phẩm '{textBox3.Text}'?\n" +
                $"Giá: {giaBan:N0} VNĐ\n" +
                $"Số lượng còn lại: {soLuongHienTai}",
                "Xác nhận mua",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                // Lưu thông tin để hiển thị sau
                string maSanPham = textBox1.Text;
                string tenSanPham = textBox3.Text;
                int soLuongMoi = soLuongHienTai - 1;

                try
                {
                    conn.Open();
                    string sqlBatch = @"
                DECLARE @MaDonHang NVARCHAR(20);
                DECLARE @MaCTDH NVARCHAR(20);
                DECLARE @CountCTDH INT;
                DECLARE @MaKH NVARCHAR(20);
                
                -- 1. Lấy hoặc tạo đơn hàng
                SELECT TOP 1 @MaDonHang = MaDonHang 
                FROM DonHang 
                WHERE TrangThai = N'Chờ xử lý' 
                ORDER BY NgayDat DESC;
                
                IF @MaDonHang IS NULL
                BEGIN
                    SET @MaDonHang = 'DH' + FORMAT(GETDATE(), 'yyyyMMddHHmmss');
                    
                    -- Lấy mã khách hàng đầu tiên
                    SELECT TOP 1 @MaKH = MaKH FROM KhachHang;
                    
                    -- Nếu không có khách hàng, dùng mã mặc định
                    IF @MaKH IS NULL
                        SET @MaKH = 'KH001';
                    
                    -- Tạo đơn hàng mới
                    INSERT INTO DonHang (MaDonHang, MaKH, NgayDat, TongTien, TrangThai)
                    VALUES (@MaDonHang, @MaKH, GETDATE(), 0, N'Chờ xử lý');
                END
                
                -- 2. Tạo mã chi tiết đơn hàng
                SELECT @CountCTDH = COUNT(*) FROM ChiTietDonHang;
                SET @MaCTDH = 'CTDH' + RIGHT('000' + CAST(@CountCTDH + 1 AS VARCHAR), 3);
                
                -- 3. Thêm chi tiết đơn hàng (SẢN PHẨM PHỤ KIỆN)
                INSERT INTO ChiTietDonHang (MaCTDH, MaDonHang, LoaiSanPham, MaThuCung, MaSP, SoLuong, DonGia)
                VALUES (@MaCTDH, @MaDonHang, N'Phụ kiện', NULL, @MaSP, 1, @DonGia);
                
                -- 4. Cập nhật hoặc xóa sản phẩm
                IF @SoLuongMoi > 0
                    UPDATE SanPhamPhuKien SET SoLuong = @SoLuongMoi WHERE MaSP = @MaSP;
                ELSE
                    DELETE FROM SanPhamPhuKien WHERE MaSP = @MaSP;
                
                -- 5. Cập nhật tổng tiền
                UPDATE DonHang 
                SET TongTien = (SELECT ISNULL(SUM(ThanhTien), 0) 
                               FROM ChiTietDonHang 
                               WHERE MaDonHang = @MaDonHang)
                WHERE MaDonHang = @MaDonHang;
                
                -- Trả về mã đơn hàng
                SELECT @MaDonHang AS MaDonHang;
            ";

                    SqlCommand cmd = new SqlCommand(sqlBatch, conn);
                    cmd.Parameters.AddWithValue("@MaSP", maSanPham);
                    cmd.Parameters.AddWithValue("@DonGia", giaBan);
                    cmd.Parameters.AddWithValue("@SoLuongMoi", soLuongMoi);
                    cmd.CommandTimeout = 30;

                    // Thực thi và lấy mã đơn hàng
                    string maDonHang = cmd.ExecuteScalar()?.ToString() ?? "";

                    conn.Close();

                    // Hiển thị thông báo
                    MessageBox.Show(
                        $"Đã thêm '{tenSanPham}' vào đơn hàng {maDonHang}!\n" +
                        (soLuongMoi > 0 ? $"Số lượng còn lại: {soLuongMoi}" : "Sản phẩm đã hết hàng!"),
                        "Thành công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    // Reload dữ liệu
                    LoadDanhSach();

                    // Clear form nếu hết hàng
                    if (soLuongMoi == 0)
                    {
                        ClearForm();
                    }
                    else
                    {
                        textBox4.Text = soLuongMoi.ToString();
                    }

                    // Load ảnh sau - không block UI
                    System.Threading.Tasks.Task.Run(() =>
                    {
                        System.Threading.Thread.Sleep(100);
                        this.Invoke((MethodInvoker)delegate
                        {
                            LoadAnhVaoGrid();
                        });
                    });
                }
                catch (Exception ex)
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();

                    MessageBox.Show($"Lỗi khi mua: {ex.Message}\n\nChi tiết: {ex.StackTrace}",
                                   "Lỗi",
                                   MessageBoxButtons.OK,
                                   MessageBoxIcon.Error);
                }
            }
        }

        // ===== NÚT XEM ĐƠN HÀNG =====
        private void btn_xemdonhang_Click(object sender, EventArgs e)
        {
            try
            {
                // Mở form DonHang
                DonHang formDonHang = new DonHang();
                formDonHang.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở form Đơn Hàng: {ex.Message}",
                               "Lỗi",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
            }
        }
    }
}