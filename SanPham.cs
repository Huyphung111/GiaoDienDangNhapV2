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
            // Gọi Load ngay trong constructor
            this.Load += SanPham_Load;

            // Đảm bảo hình ảnh được nạp ngay sau khi dữ liệu binding xong
            this.datagriw_sanphamphukien.DataBindingComplete += datagriw_sanphamphukien_DataBindingComplete;
        }

        // ===== FORM LOAD =====
        private void SanPham_Load(object sender, EventArgs e)
        {
            try
            {
                conn = new SqlConnection(connectionString);

                // Cấu hình PictureBox
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

                // Tăng chiều cao của dòng trong DataGridView
                datagriw_sanphamphukien.RowTemplate.Height = 80;

                // Test kết nối
                conn.Open();
                //MessageBox.Show("Kết nối thành công!", "Thông báo");
                conn.Close();

                LoadDanhSach();
                LoadNhaCungCap();
                KhoaControls(true);
                BatTatNut(true, true, true, false, false);

                // Load ảnh vào DataGridView
                LoadAnhVaoGrid();
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

                datagriw_sanphamphukien.DataSource = dt;

                // Kiểm tra có dữ liệu không
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Chưa có dữ liệu sản phẩm trong database!", "Thông báo");
                }
                else
                {
                   // MessageBox.Show("Đã load " + dt.Rows.Count + " sản phẩm", "Thông báo");
                }

                // Ẩn cột HinhAnh gốc (chứa tên file text)
                if (datagriw_sanphamphukien.Columns.Contains("HinhAnh"))
                    datagriw_sanphamphukien.Columns["HinhAnh"].Visible = false;

                // Ẩn cột MaNCC
                if (datagriw_sanphamphukien.Columns["MaNCC"] != null)
                    datagriw_sanphamphukien.Columns["MaNCC"].Visible = false;

                // Kiểm tra và tạo cột Image nếu chưa có
                if (!datagriw_sanphamphukien.Columns.Contains("HinhAnh_Image"))
                {
                    DataGridViewImageColumn imgCol = new DataGridViewImageColumn();
                    imgCol.Name = "HinhAnh_Image";
                    imgCol.HeaderText = "Hình Ảnh";
                    imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
                    imgCol.Width = 120;
                    imgCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    datagriw_sanphamphukien.Columns.Insert(0, imgCol); // Thêm vào cột đầu tiên
                }

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

                if (datagriw_sanphamphukien.Rows.Count > 0)
                {
                    datagriw_sanphamphukien.ClearSelection();
                    datagriw_sanphamphukien.Rows[0].Selected = true;

                    var tenSpColumn = datagriw_sanphamphukien.Columns["TenSP"];
                    if (tenSpColumn != null)
                    {
                        datagriw_sanphamphukien.CurrentCell = datagriw_sanphamphukien.Rows[0].Cells[tenSpColumn.Index];
                    }

                    datagriw_sanphamphukien_SelectionChanged(null, EventArgs.Empty);
                }

                // Đảm bảo hình ảnh trong lưới được nạp ngay sau khi dữ liệu được bind
                LoadAnhVaoGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load dữ liệu: " + ex.Message + "\n\nStack: " + ex.StackTrace,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===== LOAD HÌNH ẢNH VÀO DATAGRIDVIEW =====
        private void datagriw_sanphamphukien_SelectionChanged(object sender, EventArgs e)
        {
            if (datagriw_sanphamphukien.SelectedRows.Count > 0)
            {
                DataGridViewRow row = datagriw_sanphamphukien.SelectedRows[0];

                textBox1.Text = row.Cells["MaSP"].Value?.ToString();
                textBox3.Text = row.Cells["TenSP"].Value?.ToString();
                if (row.Cells["MaNCC"] != null && row.Cells["MaNCC"].Value != null)
                {
                    cb_nhacc.SelectedValue = row.Cells["MaNCC"].Value;
                }
                else
                {
                    cb_nhacc.SelectedIndex = -1; // hoặc giữ nguyên trạng thái cũ
                }
                textBox5.Text = row.Cells["GiaBan"].Value?.ToString();
                textBox4.Text = row.Cells["SoLuong"].Value?.ToString();
                txt_MoTaSanPham.Text = row.Cells["MoTa"].Value?.ToString();

                // Hiển thị ảnh
                var hinhAnhValue = row.Cells["HinhAnh"].Value;
                if (hinhAnhValue != null && !string.IsNullOrWhiteSpace(hinhAnhValue.ToString()))
                {
                    string file = hinhAnhValue.ToString().Trim();
                    string projectPath = Application.StartupPath;
                    string solutionPath = Directory.GetParent(Directory.GetParent(projectPath).FullName).FullName;
                    string fullPath = solutionPath + "\\Images\\SanPhamPhuKien\\" + file;

                    if (File.Exists(fullPath))
                    {
                        if (pictureBox1.Image != null)
                        {
                            var oldImage = pictureBox1.Image;
                            pictureBox1.Image = null;
                            oldImage.Dispose();
                        }

                        pictureBox1.Image = Image.FromFile(fullPath);
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
            }
        }

        private void datagriw_sanphamphukien_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            LoadAnhVaoGrid();
        }

        private void LoadAnhVaoGrid()
        {
            string projectPath = Application.StartupPath;
            string solutionPath = Directory.GetParent(Directory.GetParent(projectPath).FullName).FullName;
            string folder = solutionPath + "\\Images\\SanPhamPhuKien\\";

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

        // ===== CLICK VÀO DÒNG TRONG DATAGRIDVIEW =====
        private void datagriw_sanphamphukien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = datagriw_sanphamphukien.Rows[e.RowIndex];

                textBox1.Text = row.Cells["MaSP"].Value.ToString(); // Mã SP
                textBox3.Text = row.Cells["TenSP"].Value.ToString(); // Tên SP
                cb_nhacc.SelectedValue = row.Cells["MaNCC"].Value; // Nhà cung cấp
                textBox5.Text = row.Cells["GiaBan"].Value.ToString(); // Giá bán
                textBox4.Text = row.Cells["SoLuong"].Value.ToString(); // Số lượng
                txt_MoTaSanPham.Text = row.Cells["MoTa"].Value.ToString(); // Mô tả

                // Hiển thị hình ảnh
                var hinhAnhValue = row.Cells["HinhAnh"].Value;
                if (hinhAnhValue != null && !string.IsNullOrWhiteSpace(hinhAnhValue.ToString()))
                {
                    string file = hinhAnhValue.ToString().Trim();
                    string projectPath = Application.StartupPath;
                    string solutionPath = Directory.GetParent(Directory.GetParent(projectPath).FullName).FullName;
                    string fullPath = solutionPath + "\\Images\\SanPhamPhuKien\\" + file;

                    if (File.Exists(fullPath))
                    {
                        // Giải phóng ảnh cũ trước
                        if (pictureBox1.Image != null)
                        {
                            var oldImage = pictureBox1.Image;
                            pictureBox1.Image = null;
                            oldImage.Dispose();
                        }

                        pictureBox1.Image = Image.FromFile(fullPath);
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
            }
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
                    // Thêm mới
                    sql = @"INSERT INTO SanPhamPhuKien (MaSP, TenSP, MaNCC, GiaBan, SoLuong, MoTa, HinhAnh)
                           VALUES (@MaSP, @TenSP, @MaNCC, @GiaBan, @SoLuong, @MoTa, @HinhAnh)";
                }
                else
                {
                    // Cập nhật
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
                    // Tạo thư mục nếu chưa có
                    string projectPath = Application.StartupPath;
                    string solutionPath = Directory.GetParent(Directory.GetParent(projectPath).FullName).FullName;
                    string folder = solutionPath + "\\Images\\SanPhamPhuKien\\";

                    if (!Directory.Exists(folder))
                        Directory.CreateDirectory(folder);

                    // Copy file vào thư mục dự án
                    string fileName = Path.GetFileName(openFile.FileName);
                    string destPath = folder + fileName;

                    // Giải phóng ảnh cũ nếu có
                    if (pictureBox1.Image != null)
                    {
                        var oldImage = pictureBox1.Image;
                        pictureBox1.Image = null;
                        oldImage.Dispose();
                    }

                    // Copy file
                    File.Copy(openFile.FileName, destPath, true);

                    // Hiển thị ảnh
                    pictureBox1.Image = Image.FromFile(destPath);

                    // Lưu tên file (không lưu full path)
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

        // ===== CÁC EVENT TEXTCHANGED (Để trống hoặc thêm validation) =====
        private void txt_timkiem_TextChanged(object sender, EventArgs e)
        {
            // Có thể thêm tìm kiếm tự động khi gõ
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            // Có thể thêm validation chỉ cho phép nhập số
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            // Có thể thêm validation chỉ cho phép nhập số
        }

        private void txt_MoTaSanPham_TextChanged(object sender, EventArgs e)
        {
        }

        private void cb_nhacc_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void label6_Click(object sender, EventArgs e)
        {
        }
    }
}