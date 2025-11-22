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
            try
            {
                // Cấu hình PictureBox
                if (picAnh != null)
                    picAnh.SizeMode = PictureBoxSizeMode.Zoom;

                // Tăng chiều cao của dòng trong DataGridView
                if (dataGridView1 != null)
                {
                    dataGridView1.RowTemplate.Height = 80;

                    // Cấu hình DataGridView để fill đầy
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    // Set chế độ chọn cả dòng
                    dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dataGridView1.MultiSelect = false;
                }

                // Đặt tất cả control thành ReadOnly ban đầu
                SetReadOnlyMode(true);

                LoadLoai();
                LoadThuCung();

                // Load ảnh SAU KHI form đã hiển thị hoàn toàn
                this.Shown += (s, ev) => LoadAnhVaoGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi tạo: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

                // Xóa cột Image cũ nếu có
                if (dataGridView1.Columns.Contains("HinhAnh_Image"))
                {
                    dataGridView1.Columns.Remove("HinhAnh_Image");
                }

                dataGridView1.DataSource = dt;

                // Ẩn cột HinhAnh gốc (chứa tên file text)
                if (dataGridView1.Columns.Contains("HinhAnh"))
                    dataGridView1.Columns["HinhAnh"].Visible = false;

                // Ẩn cột MaLoai
                if (dataGridView1.Columns.Contains("MaLoai"))
                    dataGridView1.Columns["MaLoai"].Visible = false;

                // Tạo cột Image mới
                DataGridViewImageColumn imgCol = new DataGridViewImageColumn();
                imgCol.Name = "HinhAnh_Image";
                imgCol.HeaderText = "Hình Ảnh";
                imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
                imgCol.Width = 120;
                imgCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dataGridView1.Columns.Insert(0, imgCol);

                // Đặt tên cột
                if (dataGridView1.Columns.Contains("MaThuCung"))
                    dataGridView1.Columns["MaThuCung"].HeaderText = "Mã TC";
                if (dataGridView1.Columns.Contains("TenThuCung"))
                    dataGridView1.Columns["TenThuCung"].HeaderText = "Tên thú cưng";
                if (dataGridView1.Columns.Contains("TenLoai"))
                    dataGridView1.Columns["TenLoai"].HeaderText = "Loại";
                if (dataGridView1.Columns.Contains("Tuoi"))
                    dataGridView1.Columns["Tuoi"].HeaderText = "Tuổi";
                if (dataGridView1.Columns.Contains("GioiTinh"))
                    dataGridView1.Columns["GioiTinh"].HeaderText = "Giới tính";
                if (dataGridView1.Columns.Contains("GiaBan"))
                {
                    dataGridView1.Columns["GiaBan"].HeaderText = "Giá bán";
                    dataGridView1.Columns["GiaBan"].DefaultCellStyle.Format = "N0";
                }
                if (dataGridView1.Columns.Contains("SoLuong"))
                    dataGridView1.Columns["SoLuong"].HeaderText = "Số lượng";
                if (dataGridView1.Columns.Contains("MoTa"))
                    dataGridView1.Columns["MoTa"].HeaderText = "Mô tả";
            }
        }

        // ====================================================
        // 3. Load hình ảnh vào DataGridView
        // ====================================================
        void LoadAnhVaoGrid()
        {
            try
            {
                string projectPath = Application.StartupPath;
                string solutionPath = Directory.GetParent(Directory.GetParent(projectPath).FullName).FullName;

                // Thử cả 2 tên thư mục
                string folder = solutionPath + "\\Images\\ThuCung\\";
                if (!Directory.Exists(folder))
                {
                    folder = solutionPath + "\\Images\\Thucung\\";
                }

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
                // Không hiển thị lỗi
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
                string projectPath = Application.StartupPath;
                string solutionPath = Directory.GetParent(Directory.GetParent(projectPath).FullName).FullName;
                string folder = solutionPath + "\\Images\\ThuCung\\";

                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                fileAnh = Path.GetFileName(dlg.FileName);
                string dest = folder + fileAnh;

                // Giải phóng ảnh cũ
                if (picAnh.Image != null)
                {
                    var oldImage = picAnh.Image;
                    picAnh.Image = null;
                    oldImage.Dispose();
                }

                // Copy file
                File.Copy(dlg.FileName, dest, true);

                // Load ảnh mới
                using (var stream = new FileStream(dest, FileMode.Open, FileAccess.Read))
                {
                    picAnh.Image = Image.FromStream(stream);
                }

                MessageBox.Show("Đã thêm ảnh thành công!", "Thông báo");
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
                    if (!File.Exists(fullPath))
                    {
                        fullPath = solutionPath + "\\Images\\Thucung\\" + file;
                    }

                    if (File.Exists(fullPath))
                    {
                        if (picAnh.Image != null)
                        {
                            var oldImage = picAnh.Image;
                            picAnh.Image = null;
                            oldImage.Dispose();
                        }

                        using (var stream = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
                        {
                            picAnh.Image = Image.FromStream(stream);
                        }
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
        // 9. NÚT TÌM - TÌM THEO TÊN VÀ LOẠI
        // ====================================================
        private void btnTim_Click(object sender, EventArgs e)
        {
            try
            {
                string keyword = txtTim.Text.Trim();
                int maLoai = 0;
                if (cboLoai.SelectedValue != null)
                {
                    maLoai = Convert.ToInt32(cboLoai.SelectedValue);
                }

                LoadThuCung(keyword, maLoai);
                LoadAnhVaoGrid();

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

            if (picAnh.Image != null)
            {
                var oldImage = picAnh.Image;
                picAnh.Image = null;
                oldImage.Dispose();
            }
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
        private void btnThem_Click(object sender, EventArgs e) { }
        private void btnSua_Click(object sender, EventArgs e) { }
        private void btnXoa_Click(object sender, EventArgs e) { }
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

        private void btn_mua_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có chọn thú cưng không
            if (string.IsNullOrWhiteSpace(txtMa.Text))
            {
                MessageBox.Show("Vui lòng chọn thú cưng cần mua!",
                               "Thông báo",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra số lượng
            int soLuongHienTai = 0;
            if (!int.TryParse(txtSoLuong.Text, out soLuongHienTai) || soLuongHienTai <= 0)
            {
                MessageBox.Show("Thú cưng này đã hết hàng!",
                               "Thông báo",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Warning);
                return;
            }

            // Lấy giá bán
            decimal giaBan = 0;
            if (!decimal.TryParse(txtGiaBan.Text, out giaBan))
            {
                MessageBox.Show("Giá bán không hợp lệ!", "Lỗi");
                return;
            }

            // Xác nhận mua
            DialogResult result = MessageBox.Show(
                $"Bạn có muốn mua thú cưng '{txtTen.Text}'?\n" +
                $"Giá: {giaBan:N0} VNĐ\n" +
                $"Số lượng còn lại: {soLuongHienTai}",
                "Xác nhận mua",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                // Lưu thông tin để hiển thị sau
                string maThuCung = txtMa.Text;
                string tenThuCung = txtTen.Text;
                int soLuongMoi = soLuongHienTai - 1;

                using (SqlConnection conn = new SqlConnection(cnnStr))
                {
                    try
                    {
                        conn.Open();

                        // Sửa lại câu SQL - loại bỏ lỗi cú pháp TOP
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
                    
                    -- 3. Thêm chi tiết đơn hàng
                    INSERT INTO ChiTietDonHang (MaCTDH, MaDonHang, LoaiSanPham, MaThuCung, MaSP, SoLuong, DonGia)
                    VALUES (@MaCTDH, @MaDonHang, N'Thú cưng', @MaThuCung, NULL, 1, @DonGia);
                    
                    -- 4. Cập nhật hoặc xóa thú cưng
                    IF @SoLuongMoi > 0
                        UPDATE ThuCung SET SoLuong = @SoLuongMoi WHERE MaThuCung = @MaThuCung;
                    ELSE
                        DELETE FROM ThuCung WHERE MaThuCung = @MaThuCung;
                    
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
                        cmd.Parameters.AddWithValue("@MaThuCung", maThuCung);
                        cmd.Parameters.AddWithValue("@DonGia", giaBan);
                        cmd.Parameters.AddWithValue("@SoLuongMoi", soLuongMoi);
                        cmd.CommandTimeout = 30;

                        // Thực thi và lấy mã đơn hàng
                        string maDonHang = cmd.ExecuteScalar()?.ToString() ?? "";

                        // Hiển thị thông báo
                        MessageBox.Show(
                            $"Đã thêm '{tenThuCung}' vào đơn hàng {maDonHang}!\n" +
                            (soLuongMoi > 0 ? $"Số lượng còn lại: {soLuongMoi}" : "Sản phẩm đã hết hàng!"),
                            "Thành công",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );

                        // Reload dữ liệu
                        LoadThuCung();

                        // Clear form nếu hết hàng
                        if (soLuongMoi == 0)
                        {
                            txtMa.Clear();
                            txtTen.Clear();
                            txtTuoi.Clear();
                            txtGiaBan.Clear();
                            txtSoLuong.Clear();
                            txtMota.Clear();
                            cbMaloai.SelectedIndex = -1;
                            cboGioiTinh.SelectedIndex = -1;

                            if (picAnh.Image != null)
                            {
                                var oldImage = picAnh.Image;
                                picAnh.Image = null;
                                oldImage.Dispose();
                            }
                        }
                        else
                        {
                            txtSoLuong.Text = soLuongMoi.ToString();
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
                        MessageBox.Show($"Lỗi khi mua: {ex.Message}\n\nChi tiết: {ex.StackTrace}",
                                       "Lỗi",
                                       MessageBoxButtons.OK,
                                       MessageBoxIcon.Error);
                    }
                }
            }
        }

        // Hàm tạo hoặc lấy đơn hàng hiện có (mặc định lấy đơn hàng đầu tiên hoặc tạo mới)
        private string TaoHoacLayDonHang(SqlConnection conn, SqlTransaction transaction)
        {
            // Kiểm tra xem có đơn hàng "Chờ xử lý" nào không
            string sqlCheck = "SELECT TOP 1 MaDonHang FROM DonHang WHERE TrangThai = N'Chờ xử lý' ORDER BY NgayDat DESC";
            SqlCommand cmdCheck = new SqlCommand(sqlCheck, conn, transaction);
            object result = cmdCheck.ExecuteScalar();

            if (result != null)
            {
                return result.ToString();
            }

            // Nếu chưa có - Tạo đơn hàng mới
            string maDonHang = "DH" + DateTime.Now.ToString("yyyyMMddHHmmss");

            // Lấy khách hàng đầu tiên (hoặc có thể cho người dùng chọn)
            string sqlGetKH = "SELECT TOP 1 MaKH FROM KhachHang";
            SqlCommand cmdGetKH = new SqlCommand(sqlGetKH, conn, transaction);
            string maKH = cmdGetKH.ExecuteScalar()?.ToString() ?? "KH001";

            string sqlInsert = @"INSERT INTO DonHang (MaDonHang, MaKH, NgayDat, TongTien, TrangThai)
                        VALUES (@MaDonHang, @MaKH, GETDATE(), 0, N'Chờ xử lý')";

            SqlCommand cmdInsert = new SqlCommand(sqlInsert, conn, transaction);
            cmdInsert.Parameters.AddWithValue("@MaDonHang", maDonHang);
            cmdInsert.Parameters.AddWithValue("@MaKH", maKH);
            cmdInsert.ExecuteNonQuery();

            return maDonHang;
        }

        // Hàm tạo mã chi tiết đơn hàng
        private string TaoMaChiTietDonHang(SqlConnection conn, SqlTransaction transaction)
        {
            string sqlCount = "SELECT COUNT(*) FROM ChiTietDonHang";
            SqlCommand cmdCount = new SqlCommand(sqlCount, conn, transaction);
            int count = (int)cmdCount.ExecuteScalar();

            return "CTDH" + (count + 1).ToString("D3");
        }

        // Hàm cập nhật tổng tiền đơn hàng
        private void CapNhatTongTienDonHang(SqlConnection conn, SqlTransaction transaction, string maDonHang)
        {
            string sqlUpdateTong = @"UPDATE DonHang 
                            SET TongTien = (SELECT ISNULL(SUM(ThanhTien), 0) 
                                          FROM ChiTietDonHang 
                                          WHERE MaDonHang = @MaDonHang)
                            WHERE MaDonHang = @MaDonHang";

            SqlCommand cmdUpdateTong = new SqlCommand(sqlUpdateTong, conn, transaction);
            cmdUpdateTong.Parameters.AddWithValue("@MaDonHang", maDonHang);
            cmdUpdateTong.ExecuteNonQuery();
        }

        private void Btn_XemThuCungVaoDonHang_Click(object sender, EventArgs e)
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