using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace GiaoDienDangNhap
{
    public partial class ThuCung : Form
    {
        string cnnStr = @"Data Source=HUYNE;Initial Catalog=QUANLY_PETSHOP_V9;Integrated Security=True;TrustServerCertificate=True";
        string fileAnh = "";

        public ThuCung()
        {
            InitializeComponent();
        }

        // ====================================================
        // FORM LOAD
        // ====================================================
        private void ThuCung_Load(object sender, EventArgs e)
        {
            picAnh.SizeMode = PictureBoxSizeMode.Zoom;

            // Tăng chiều cao của dòng trong DataGridView
            dataGridView1.RowTemplate.Height = 80;

            // Cấu hình DataGridView để fill đầy
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Đặt tất cả control thành ReadOnly ban đầu
            SetReadOnlyMode(true);

            LoadLoai();
            LoadThuCung();
            LoadAnhVaoGrid();
        }

        // Hàm bật/tắt chế độ ReadOnly
        void SetReadOnlyMode(bool readOnly)
        {
            txtMa.ReadOnly = true; // Mã luôn ReadOnly
            txtTen.ReadOnly = readOnly;
            txtTuoi.ReadOnly = readOnly;
            txtGiaBan.ReadOnly = readOnly;
            txtSoLuong.ReadOnly = readOnly;
            txtMota.ReadOnly = readOnly;

            cbMaloai.Enabled = !readOnly;
            cboGioiTinh.Enabled = !readOnly;
            btnChonAnh.Enabled = !readOnly;
        }

        // ====================================================
        // 1. Load loại thú cưng
        // ====================================================
        void LoadLoai()
        {
            using (SqlConnection cn = new SqlConnection(cnnStr))
            {
                cn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT MaLoai, TenLoai FROM LoaiThuCung ORDER BY TenLoai", cn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Thêm dòng "Tất cả" vào đầu cho ComboBox tìm kiếm
                DataRow rowAll = dt.NewRow();
                rowAll["MaLoai"] = 0;
                rowAll["TenLoai"] = "-- Tất cả --";
                dt.Rows.InsertAt(rowAll, 0);

                // Combobox tìm kiếm
                cboLoai.DataSource = dt;
                cboLoai.DisplayMember = "TenLoai";
                cboLoai.ValueMember = "MaLoai";
                cboLoai.SelectedIndex = 0; // Chọn "Tất cả" mặc định

                // Combobox chọn loại khi thêm (không có "Tất cả")
                SqlDataAdapter da2 = new SqlDataAdapter("SELECT MaLoai, TenLoai FROM LoaiThuCung ORDER BY TenLoai", cn);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);

                cbMaloai.DataSource = dt2;
                cbMaloai.DisplayMember = "TenLoai";
                cbMaloai.ValueMember = "MaLoai";
            }
        }

        // ====================================================
        // 2. Load danh sách thú cưng
        // ====================================================
        void LoadThuCung(string keyword = "", int maLoai = 0)
        {
            using (SqlConnection cn = new SqlConnection(cnnStr))
            {
                cn.Open();

                string sql = @"SELECT tc.*, ltc.TenLoai 
                              FROM ThuCung tc 
                              INNER JOIN LoaiThuCung ltc ON tc.MaLoai = ltc.MaLoai 
                              WHERE 1=1";

                // Tìm theo tên
                if (!string.IsNullOrWhiteSpace(keyword))
                    sql += " AND tc.TenThuCung LIKE @keyword";

                // Lọc theo loại
                if (maLoai > 0)
                    sql += " AND tc.MaLoai = @maLoai";

                sql += " ORDER BY tc.MaThuCung";

                SqlCommand cmd = new SqlCommand(sql, cn);

                if (!string.IsNullOrWhiteSpace(keyword))
                    cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");

                if (maLoai > 0)
                    cmd.Parameters.AddWithValue("@maLoai", maLoai);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt;

                // Ẩn cột HinhAnh gốc (chứa tên file text)
                if (dataGridView1.Columns.Contains("HinhAnh"))
                    dataGridView1.Columns["HinhAnh"].Visible = false;

                // Kiểm tra và tạo cột Image nếu chưa có
                if (!dataGridView1.Columns.Contains("HinhAnh_Image"))
                {
                    DataGridViewImageColumn imgCol = new DataGridViewImageColumn();
                    imgCol.Name = "HinhAnh_Image";
                    imgCol.HeaderText = "Hình Ảnh";
                    imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
                    imgCol.Width = 120;
                    imgCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    dataGridView1.Columns.Insert(0, imgCol);
                }

                if (dataGridView1.Rows.Count > 0)
                {
                    dataGridView1.ClearSelection();
                    dataGridView1.Rows[0].Selected = true;

                    var tenCol = dataGridView1.Columns["TenThuCung"];
                    if (tenCol != null)
                    {
                        dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells[tenCol.Index];
                    }

                    dataGridView1_CellClick(this, new DataGridViewCellEventArgs(0, 0));
                }

                // Nạp lại hình ảnh ngay sau khi DataGridView được bind dữ liệu
                LoadAnhVaoGrid();
            }
        }

        // ====================================================
        // 3. Load hình ảnh vào DataGridView
        // ====================================================
        void LoadAnhVaoGrid()
        {
            string projectPath = Application.StartupPath;
            string solutionPath = Directory.GetParent(Directory.GetParent(projectPath).FullName).FullName;
            string folder = solutionPath + "\\Images\\ThuCung\\";

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
                return;
            }

            foreach (DataGridViewRow row in dataGridView1.Rows)
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
                        using (var img = Image.FromFile(fullPath))
                        {
                            row.Cells["HinhAnh_Image"].Value = new Bitmap(img);
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

        // ====================================================
        // 4. Chọn ảnh
        // ====================================================
        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Image Files|*.jpg;*.png;*.jpeg";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                picAnh.Image = Image.FromFile(dlg.FileName);
                fileAnh = Path.GetFileName(dlg.FileName);

                string projectPath = Application.StartupPath;
                string solutionPath = Directory.GetParent(Directory.GetParent(projectPath).FullName).FullName;
                string dest = solutionPath + "\\Images\\ThuCung\\" + fileAnh;

                string folder = Path.GetDirectoryName(dest);
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                File.Copy(dlg.FileName, dest, true);
            }
        }

        // ====================================================
        // 5. Thêm
        // ====================================================
        private void btnThem_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(cnnStr))
            {
                cn.Open();

                string sql = @"INSERT INTO ThuCung
                               (MaThuCung, TenThuCung, MaLoai, Tuoi, GioiTinh, GiaBan, SoLuong, MoTa, HinhAnh)
                               VALUES (@ma, @ten, @loai, @tuoi, @gt, @gia, @sl, @mota, @anh)";

                SqlCommand cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@ma", txtMa.Text);
                cmd.Parameters.AddWithValue("@ten", txtTen.Text);
                cmd.Parameters.AddWithValue("@loai", cbMaloai.SelectedValue);
                cmd.Parameters.AddWithValue("@tuoi", txtTuoi.Text);
                cmd.Parameters.AddWithValue("@gt", cboGioiTinh.Text);
                cmd.Parameters.AddWithValue("@gia", txtGiaBan.Text);
                cmd.Parameters.AddWithValue("@sl", txtSoLuong.Text);
                cmd.Parameters.AddWithValue("@mota", txtMota.Text);
                cmd.Parameters.AddWithValue("@anh", fileAnh);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Thêm thành công!");
                LoadThuCung();
                LoadAnhVaoGrid();
            }
        }

        // ====================================================
        // 6. Click vào bảng → hiện dữ liệu lên Form
        // ====================================================
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                DataGridViewRow r = dataGridView1.Rows[e.RowIndex];

                txtMa.Text = r.Cells["MaThuCung"].Value?.ToString() ?? "";
                txtTen.Text = r.Cells["TenThuCung"].Value?.ToString() ?? "";
                txtTuoi.Text = r.Cells["Tuoi"].Value?.ToString() ?? "";
                txtGiaBan.Text = r.Cells["GiaBan"].Value?.ToString() ?? "";
                txtSoLuong.Text = r.Cells["SoLuong"].Value?.ToString() ?? "";
                txtMota.Text = r.Cells["MoTa"].Value?.ToString() ?? "";

                var gioiTinhValue = r.Cells["GioiTinh"].Value;
                if (gioiTinhValue != null && !string.IsNullOrWhiteSpace(gioiTinhValue.ToString()))
                {
                    cboGioiTinh.Text = gioiTinhValue.ToString();
                }

                var maLoaiValue = r.Cells["MaLoai"].Value;
                if (maLoaiValue != null)
                {
                    try
                    {
                        cbMaloai.SelectedValue = maLoaiValue;
                    }
                    catch
                    {
                        if (cbMaloai.Items.Count > 0)
                            cbMaloai.SelectedIndex = 0;
                    }
                }

                var hinhAnhValue = r.Cells["HinhAnh"].Value;
                if (hinhAnhValue != null && !string.IsNullOrWhiteSpace(hinhAnhValue.ToString()))
                {
                    string file = hinhAnhValue.ToString().Trim();
                    string projectPath = Application.StartupPath;
                    string solutionPath = Directory.GetParent(Directory.GetParent(projectPath).FullName).FullName;
                    string fullPath = solutionPath + "\\Images\\ThuCung\\" + file;

                    if (File.Exists(fullPath))
                    {
                        if (picAnh.Image != null)
                        {
                            var oldImage = picAnh.Image;
                            picAnh.Image = null;
                            oldImage.Dispose();
                        }

                        picAnh.Image = Image.FromFile(fullPath);
                        fileAnh = file;
                    }
                    else
                    {
                        picAnh.Image = null;
                        fileAnh = "";
                    }
                }
                else
                {
                    picAnh.Image = null;
                    fileAnh = "";
                }

                txtTen.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi load dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ====================================================
        // 7. Sửa
        // ====================================================
        private void btnSua_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(cnnStr))
            {
                cn.Open();

                string sql = @"UPDATE ThuCung SET 
                               TenThuCung=@ten, MaLoai=@loai, Tuoi=@tuoi, GioiTinh=@gt,
                               GiaBan=@gia, SoLuong=@sl, MoTa=@mota, HinhAnh=@anh
                               WHERE MaThuCung=@ma";

                SqlCommand cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@ma", txtMa.Text);
                cmd.Parameters.AddWithValue("@ten", txtTen.Text);
                cmd.Parameters.AddWithValue("@loai", cbMaloai.SelectedValue);
                cmd.Parameters.AddWithValue("@tuoi", txtTuoi.Text);
                cmd.Parameters.AddWithValue("@gt", cboGioiTinh.Text);
                cmd.Parameters.AddWithValue("@gia", txtGiaBan.Text);
                cmd.Parameters.AddWithValue("@sl", txtSoLuong.Text);
                cmd.Parameters.AddWithValue("@mota", txtMota.Text);
                cmd.Parameters.AddWithValue("@anh", fileAnh);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Sửa thành công!");
                LoadThuCung();
                LoadAnhVaoGrid();
            }
        }

        // ====================================================
        // 8. Xóa
        // ====================================================
        private void btnXoa_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(cnnStr))
            {
                cn.Open();

                SqlCommand cmd = new SqlCommand("DELETE FROM ThuCung WHERE MaThuCung=@ma", cn);
                cmd.Parameters.AddWithValue("@ma", txtMa.Text);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Xóa thành công!");
                LoadThuCung();
                LoadAnhVaoGrid();
            }
        }

        // ====================================================
        // 9. NÚT TÌM - TÌM THEO TÊN VÀ LOẠI
        // ====================================================
        private void btnTim_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy từ khóa tìm kiếm
                string keyword = txtTim.Text.Trim();

                // Lấy mã loại từ ComboBox
                int maLoai = 0;
                if (cboLoai.SelectedValue != null)
                {
                    maLoai = Convert.ToInt32(cboLoai.SelectedValue);
                }

                // Load dữ liệu với điều kiện lọc
                LoadThuCung(keyword, maLoai);
                LoadAnhVaoGrid();

                // Thông báo kết quả
                int soKetQua = dataGridView1.Rows.Count;
                string thongBao = $"Tìm thấy {soKetQua} kết quả";

                if (!string.IsNullOrWhiteSpace(keyword))
                    thongBao += $" với từ khóa '{keyword}'";

                if (maLoai > 0)
                {
                    string tenLoai = cboLoai.Text;
                    thongBao += $" thuộc loại '{tenLoai}'";
                }

                MessageBox.Show(thongBao, "Kết quả tìm kiếm",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tìm kiếm: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ====================================================
        // TOOLBAR BUTTONS
        // ====================================================

        // Nút Thêm mới - Xóa trắng form và cho phép nhập
        private void tool_themm_Click(object sender, EventArgs e)
        {
            txtMa.Clear();
            txtTen.Clear();
            txtTuoi.Clear();
            txtGiaBan.Clear();
            txtSoLuong.Clear();
            txtMota.Clear();
            picAnh.Image = null;
            fileAnh = "";

            if (cbMaloai.Items.Count > 0)
                cbMaloai.SelectedIndex = 0;
            if (cboGioiTinh.Items.Count > 0)
                cboGioiTinh.SelectedIndex = 0;

            SetReadOnlyMode(false);
            txtMa.ReadOnly = false;

            txtMa.Focus();
        }

        // Nút Xóa
        private void tool_xoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMa.Text))
            {
                MessageBox.Show("Vui lòng chọn thú cưng cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Bạn có chắc muốn xóa thú cưng '{txtTen.Text}' không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                using (SqlConnection cn = new SqlConnection(cnnStr))
                {
                    try
                    {
                        cn.Open();
                        SqlCommand cmd = new SqlCommand("DELETE FROM ThuCung WHERE MaThuCung=@ma", cn);
                        cmd.Parameters.AddWithValue("@ma", txtMa.Text);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        tool_themm_Click(sender, e);
                        SetReadOnlyMode(true);

                        LoadThuCung();
                        LoadAnhVaoGrid();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi xóa: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // Nút Sửa - Cho phép chỉnh sửa
        private void tool_suaa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMa.Text))
            {
                MessageBox.Show("Vui lòng chọn thú cưng cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SetReadOnlyMode(false);
            txtTen.Focus();
        }

        // Nút Lưu (Lưu sau khi Thêm hoặc Sửa)
        private void tool_luuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMa.Text))
            {
                MessageBox.Show("Vui lòng nhập mã thú cưng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMa.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTen.Text))
            {
                MessageBox.Show("Vui lòng nhập tên thú cưng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTen.Focus();
                return;
            }

            using (SqlConnection cn = new SqlConnection(cnnStr))
            {
                try
                {
                    cn.Open();

                    SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM ThuCung WHERE MaThuCung=@ma", cn);
                    checkCmd.Parameters.AddWithValue("@ma", txtMa.Text);
                    int count = (int)checkCmd.ExecuteScalar();

                    if (count > 0)
                    {
                        string sql = @"UPDATE ThuCung SET 
                                       TenThuCung=@ten, MaLoai=@loai, Tuoi=@tuoi, GioiTinh=@gt,
                                       GiaBan=@gia, SoLuong=@sl, MoTa=@mota, HinhAnh=@anh
                                       WHERE MaThuCung=@ma";

                        SqlCommand cmd = new SqlCommand(sql, cn);

                        cmd.Parameters.AddWithValue("@ma", txtMa.Text);
                        cmd.Parameters.AddWithValue("@ten", txtTen.Text);
                        cmd.Parameters.AddWithValue("@loai", cbMaloai.SelectedValue ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@tuoi", string.IsNullOrWhiteSpace(txtTuoi.Text) ? (object)DBNull.Value : txtTuoi.Text);
                        cmd.Parameters.AddWithValue("@gt", cboGioiTinh.Text);
                        cmd.Parameters.AddWithValue("@gia", string.IsNullOrWhiteSpace(txtGiaBan.Text) ? (object)DBNull.Value : txtGiaBan.Text);
                        cmd.Parameters.AddWithValue("@sl", string.IsNullOrWhiteSpace(txtSoLuong.Text) ? (object)DBNull.Value : txtSoLuong.Text);
                        cmd.Parameters.AddWithValue("@mota", string.IsNullOrWhiteSpace(txtMota.Text) ? (object)DBNull.Value : txtMota.Text);
                        cmd.Parameters.AddWithValue("@anh", string.IsNullOrWhiteSpace(fileAnh) ? (object)DBNull.Value : fileAnh);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        string sql = @"INSERT INTO ThuCung
                                       (MaThuCung, TenThuCung, MaLoai, Tuoi, GioiTinh, GiaBan, SoLuong, MoTa, HinhAnh)
                                       VALUES (@ma, @ten, @loai, @tuoi, @gt, @gia, @sl, @mota, @anh)";

                        SqlCommand cmd = new SqlCommand(sql, cn);

                        cmd.Parameters.AddWithValue("@ma", txtMa.Text);
                        cmd.Parameters.AddWithValue("@ten", txtTen.Text);
                        cmd.Parameters.AddWithValue("@loai", cbMaloai.SelectedValue ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@tuoi", string.IsNullOrWhiteSpace(txtTuoi.Text) ? (object)DBNull.Value : txtTuoi.Text);
                        cmd.Parameters.AddWithValue("@gt", cboGioiTinh.Text);
                        cmd.Parameters.AddWithValue("@gia", string.IsNullOrWhiteSpace(txtGiaBan.Text) ? (object)DBNull.Value : txtGiaBan.Text);
                        cmd.Parameters.AddWithValue("@sl", string.IsNullOrWhiteSpace(txtSoLuong.Text) ? (object)DBNull.Value : txtSoLuong.Text);
                        cmd.Parameters.AddWithValue("@mota", string.IsNullOrWhiteSpace(txtMota.Text) ? (object)DBNull.Value : txtMota.Text);
                        cmd.Parameters.AddWithValue("@anh", string.IsNullOrWhiteSpace(fileAnh) ? (object)DBNull.Value : fileAnh);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Thêm mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    LoadThuCung();
                    LoadAnhVaoGrid();

                    SetReadOnlyMode(true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ====================================================
        // GIỮ LẠI TẤT CẢ EVENT RỖNG
        // ====================================================
        private void txtTen_TextChanged(object sender, EventArgs e) { }
        private void txtTuoi_TextChanged(object sender, EventArgs e) { }
        private void txtGiaBan_TextChanged(object sender, EventArgs e) { }
        private void txtSoLuong_TextChanged(object sender, EventArgs e) { }
        private void txtMa_TextChanged(object sender, EventArgs e) { }
        private void txtMota_TextChanged(object sender, EventArgs e) { }
        private void txtTim_TextChanged(object sender, EventArgs e) { }
        private void cboLoai_SelectedIndexChanged(object sender, EventArgs e) { }
        private void cboGioiTinh_SelectedIndexChanged(object sender, EventArgs e) { }
        private void cbMaloai_SelectedIndexChanged(object sender, EventArgs e) { }
        private void picAnh_Click(object sender, EventArgs e) { }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}