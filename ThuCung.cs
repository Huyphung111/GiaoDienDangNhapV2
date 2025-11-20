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
                SqlDataAdapter da = new SqlDataAdapter("SELECT MaLoai, TenLoai FROM LoaiThuCung", cn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Combobox tìm kiếm
                cboLoai.DataSource = dt;
                cboLoai.DisplayMember = "TenLoai";
                cboLoai.ValueMember = "MaLoai";

                // Combobox chọn loại khi thêm
                cbMaloai.DataSource = dt.Copy();
                cbMaloai.DisplayMember = "TenLoai";
                cbMaloai.ValueMember = "MaLoai";
            }
        }

        // ====================================================
        // 2. Load danh sách thú cưng
        // ====================================================
        void LoadThuCung(string keyword = "")
        {
            using (SqlConnection cn = new SqlConnection(cnnStr))
            {
                cn.Open();

                string sql = "SELECT * FROM ThuCung";
                if (keyword != "")
                    sql += $" WHERE TenThuCung LIKE N'%{keyword}%'";

                SqlDataAdapter da = new SqlDataAdapter(sql, cn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt;

                // Ẩn cột HinhAnh gốc (chứa tên file text)
                dataGridView1.Columns["HinhAnh"].Visible = false;

                // Kiểm tra và tạo cột Image nếu chưa có
                if (!dataGridView1.Columns.Contains("HinhAnh_Image"))
                {
                    DataGridViewImageColumn imgCol = new DataGridViewImageColumn();
                    imgCol.Name = "HinhAnh_Image";
                    imgCol.HeaderText = "Hình Ảnh";
                    imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
                    imgCol.Width = 120;
                    imgCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.None; // Cố định độ rộng cột ảnh
                    dataGridView1.Columns.Insert(0, imgCol);
                }
            }
        }

        // ====================================================
        // 3. Load hình ảnh vào DataGridView
        // ====================================================
        void LoadAnhVaoGrid()
        {
            // Đổi đường dẫn sang thư mục gốc của solution
            string projectPath = Application.StartupPath;
            // Lùi 2 cấp từ bin\Debug lên thư mục gốc
            string solutionPath = Directory.GetParent(Directory.GetParent(projectPath).FullName).FullName;
            string folder = solutionPath + "\\Images\\ThuCung\\";

            // Tạo thư mục nếu chưa có
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
                MessageBox.Show($"Đã tạo thư mục: {folder}\nVui lòng copy ảnh vào đây!", "Thông báo");
                return;
            }

            int soAnhThanhCong = 0;
            int soAnhLoi = 0;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;

                try
                {
                    // Lấy tên file từ cột HinhAnh (cột ẩn chứa tên file)
                    var val = row.Cells["HinhAnh"].Value;
                    if (val == null || string.IsNullOrWhiteSpace(val.ToString()))
                    {
                        row.Cells["HinhAnh_Image"].Value = null;
                        continue;
                    }

                    string file = val.ToString().Trim();
                    string fullPath = folder + file;

                    // Gán ảnh vào cột HinhAnh_Image
                    if (File.Exists(fullPath))
                    {
                        // Tạo image mới để tránh lock file
                        using (var img = Image.FromFile(fullPath))
                        {
                            row.Cells["HinhAnh_Image"].Value = new Bitmap(img);
                        }
                        soAnhThanhCong++;
                    }
                    else
                    {
                        row.Cells["HinhAnh_Image"].Value = null;
                        soAnhLoi++;
                        // Debug: Hiển thị file không tìm thấy
                        System.Diagnostics.Debug.WriteLine($"Không tìm thấy: {fullPath}");
                    }
                }
                catch (Exception ex)
                {
                    row.Cells["HinhAnh_Image"].Value = null;
                    soAnhLoi++;
                    System.Diagnostics.Debug.WriteLine($"Lỗi load ảnh: {ex.Message}");
                }
            }

            // Hiển thị kết quả debug
            if (soAnhLoi > 0)
            {
                MessageBox.Show($"Đã load {soAnhThanhCong} ảnh thành công\n" +
                               $"Không tìm thấy {soAnhLoi} ảnh\n\n" +
                               $"Thư mục ảnh: {folder}\n\n" +
                               $"Kiểm tra Output window để xem chi tiết!",
                               "Kết quả load ảnh",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Information);
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

                // Đổi đường dẫn sang thư mục gốc của solution
                string projectPath = Application.StartupPath;
                string solutionPath = Directory.GetParent(Directory.GetParent(projectPath).FullName).FullName;
                string dest = solutionPath + "\\Images\\ThuCung\\" + fileAnh;

                // Tạo thư mục nếu chưa có
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
        // 6. Click vào bảng → hiện dữ liệu lên Form - ĐÃ SỬA
        // ====================================================
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Bỏ qua nếu click vào header
            if (e.RowIndex < 0) return;

            try
            {
                DataGridViewRow r = dataGridView1.Rows[e.RowIndex];

                // Load dữ liệu lên form
                txtMa.Text = r.Cells["MaThuCung"].Value?.ToString() ?? "";
                txtTen.Text = r.Cells["TenThuCung"].Value?.ToString() ?? "";
                txtTuoi.Text = r.Cells["Tuoi"].Value?.ToString() ?? "";
                txtGiaBan.Text = r.Cells["GiaBan"].Value?.ToString() ?? "";
                txtSoLuong.Text = r.Cells["SoLuong"].Value?.ToString() ?? "";
                txtMota.Text = r.Cells["MoTa"].Value?.ToString() ?? "";

                // Xử lý GioiTinh
                var gioiTinhValue = r.Cells["GioiTinh"].Value;
                if (gioiTinhValue != null && !string.IsNullOrWhiteSpace(gioiTinhValue.ToString()))
                {
                    cboGioiTinh.Text = gioiTinhValue.ToString();
                }

                // Xử lý MaLoai - Hiển thị tên loại
                var maLoaiValue = r.Cells["MaLoai"].Value;
                if (maLoaiValue != null)
                {
                    try
                    {
                        cbMaloai.SelectedValue = maLoaiValue;
                    }
                    catch
                    {
                        // Nếu không tìm thấy, để mặc định
                        if (cbMaloai.Items.Count > 0)
                            cbMaloai.SelectedIndex = 0;
                    }
                }

                // Load ảnh
                var hinhAnhValue = r.Cells["HinhAnh"].Value;
                if (hinhAnhValue != null && !string.IsNullOrWhiteSpace(hinhAnhValue.ToString()))
                {
                    string file = hinhAnhValue.ToString().Trim();

                    // Đổi đường dẫn sang thư mục gốc của solution
                    string projectPath = Application.StartupPath;
                    string solutionPath = Directory.GetParent(Directory.GetParent(projectPath).FullName).FullName;
                    string fullPath = solutionPath + "\\Images\\ThuCung\\" + file;

                    if (File.Exists(fullPath))
                    {
                        // Dispose ảnh cũ trước khi load ảnh mới
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

                // Đặt focus vào tên để dễ chỉnh sửa
                txtTen.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi load dữ liệu: {ex.Message}\n\nChi tiết: {ex.StackTrace}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        // 9. Tìm
        // ====================================================
        private void btnTim_Click(object sender, EventArgs e)
        {
            LoadThuCung(txtTim.Text);
            LoadAnhVaoGrid();
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

            // Cho phép chỉnh sửa
            SetReadOnlyMode(false);
            txtMa.ReadOnly = false; // Cho phép nhập mã mới

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

                        // Xóa trắng form và quay về ReadOnly
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

            // Cho phép chỉnh sửa (trừ mã)
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

                    // Kiểm tra mã đã tồn tại chưa
                    SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM ThuCung WHERE MaThuCung=@ma", cn);
                    checkCmd.Parameters.AddWithValue("@ma", txtMa.Text);
                    int count = (int)checkCmd.ExecuteScalar();

                    if (count > 0)
                    {
                        // Nếu đã tồn tại -> Sửa
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
                        // Nếu chưa tồn tại -> Thêm mới
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

                    // Quay về chế độ ReadOnly
                    SetReadOnlyMode(true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ====================================================
        // GIỮ LẠI TẤT CẢ EVENT RỖNG — KHÔNG XOÁ
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