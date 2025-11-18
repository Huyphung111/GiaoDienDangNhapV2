using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GiaoDienDangNhap
{
    public partial class TrangQuanLyTaiKhoan : Form
    {
        private string connectionString = @"Data Source=HUYNE;Initial Catalog=QUANLY_PETSHOP_V9;Integrated Security=True;TrustServerCertificate=True";
        private bool isAddingNew = false;

        public TrangQuanLyTaiKhoan()
        {
            InitializeComponent();
        }

        private void TrangQuanLyTaiKhoan_Load(object sender, EventArgs e)
        {
            // Đảm bảo DataGridView fill hết GroupBox
            dataGridView1.Dock = DockStyle.Fill;

            // Thêm khoảng cách cho GroupBox (nếu có)
            if (dataGridView1.Parent is GroupBox gb)
            {
                gb.Padding = new Padding(10, 35, 10, 10);
            }

            LoadDanhSachTaiKhoan();
            LockTextBoxes();
        }

        // ==================== SETUP DATAGRIDVIEW ĐẸP - TỰ ĐỘNG CO GIÃN ====================
        private void SetupDataGridView()
        {
            // Tắt tự động tạo cột - BẮT BUỘC để kiểm soát hoàn toàn
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();

            // Tạo cột thủ công (đúng thứ tự + đẹp)
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MaNguoiDung",
                HeaderText = "Mã ND",
                DataPropertyName = "MaNguoiDung",
                MinimumWidth = 60,
                FillWeight = 0.8f
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TenDangNhap",
                HeaderText = "Tên đăng nhập",
                DataPropertyName = "TenDangNhap",
                MinimumWidth = 100,
                FillWeight = 1.2f
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MatKhau",
                HeaderText = "Mật khẩu",
                DataPropertyName = "MatKhau",
                MinimumWidth = 90,
                FillWeight = 1f
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "HoTen",
                HeaderText = "Họ và tên",
                DataPropertyName = "HoTen",
                MinimumWidth = 120,
                FillWeight = 1.3f
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Email",
                HeaderText = "Email",
                DataPropertyName = "Email",
                MinimumWidth = 180,
                FillWeight = 1.8f
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SoDienThoai",
                HeaderText = "Số điện thoại",
                DataPropertyName = "SoDienThoai",
                MinimumWidth = 100,
                FillWeight = 1f
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NgayTao",
                HeaderText = "Ngày tạo",
                DataPropertyName = "NgayTao",
                MinimumWidth = 100,
                FillWeight = 0.9f,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MaKH",
                HeaderText = "Mã KH",
                DataPropertyName = "MaKH",
                MinimumWidth = 70,
                FillWeight = 0.8f
            });

            // Cấu hình chung đẹp
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.GridColor = Color.FromArgb(230, 230, 230);
            dataGridView1.BorderStyle = BorderStyle.None;

            // Dòng xen kẽ + chọn dòng
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 252, 255);
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 120, 215);
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;

            // Header đẹp
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 120, 215);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridView1.ColumnHeadersHeight = 40;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        }

        // ==================== LOAD DỮ LIỆU ====================
        private void LoadDanhSachTaiKhoan()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT
                                        nd.MaNguoiDung,
                                        nd.TenDangNhap,
                                        nd.MatKhau,
                                        nd.HoTen,
                                        nd.Email,
                                        nd.SoDienThoai,
                                        nd.NgayTao,
                                        nd.MaPhanQuyen,
                                        nd.TrangThai,
                                        kh.MaKH
                                    FROM NguoiDung nd
                                    LEFT JOIN KhachHang kh ON nd.MaNguoiDung = kh.MaNguoiDung
                                    ORDER BY nd.MaNguoiDung DESC";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridView1.DataSource = null;  // Reset trước khi bind
                    dataGridView1.DataSource = dt;

                    // Setup lại sau khi có dữ liệu
                    SetupDataGridView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==================== CÁC HÀM KHÁC (giữ nguyên) ====================
        private void LockTextBoxes()
        {
            txt_tendangnhap.Enabled = txt_matkhau.Enabled = txt_hovaten.Enabled =
            txt_email.Enabled = txt_sodienthoai.Enabled = txt_ngaytao.Enabled =
            txt_manguoidung.Enabled = txt_makhachhang.Enabled = false;
        }

        private void UnlockTextBoxes()
        {
            txt_tendangnhap.Enabled = txt_matkhau.Enabled = txt_hovaten.Enabled =
            txt_email.Enabled = txt_sodienthoai.Enabled = true;
        }

        private void ClearTextBoxes()
        {
            txt_tendangnhap.Text = txt_matkhau.Text = txt_hovaten.Text = txt_email.Text =
            txt_sodienthoai.Text = txt_ngaytao.Text = txt_manguoidung.Text = txt_makhachhang.Text = "";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                txt_manguoidung.Text = row.Cells["MaNguoiDung"].Value?.ToString() ?? "";
                txt_tendangnhap.Text = row.Cells["TenDangNhap"].Value?.ToString() ?? "";
                txt_matkhau.Text = row.Cells["MatKhau"].Value?.ToString() ?? "";
                txt_hovaten.Text = row.Cells["HoTen"].Value?.ToString() ?? "";
                txt_email.Text = row.Cells["Email"].Value?.ToString() ?? "";
                txt_sodienthoai.Text = row.Cells["SoDienThoai"].Value?.ToString() ?? "";
                txt_ngaytao.Text = row.Cells["NgayTao"].Value != null
                    ? Convert.ToDateTime(row.Cells["NgayTao"].Value).ToString("dd/MM/yyyy")
                    : "";
                txt_makhachhang.Text = row.Cells["MaKH"].Value?.ToString() ?? "";
            }
        }

        // Các nút Thêm - Sửa - Lưu - Xóa - Tìm kiếm - Thoát giữ nguyên như cũ của bạn
        // (Mình để nguyên để bạn không phải copy lại dài dòng)

        private void tool_timkiem_Click(object sender, EventArgs e)
        {
            string keyword = txt_timkiem.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                LoadDanhSachTaiKhoan();
                return;
            }
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT nd.MaNguoiDung, nd.TenDangNhap, nd.MatKhau, nd.HoTen, nd.Email, 
                                    nd.SoDienThoai, nd.NgayTao, nd.MaPhanQuyen, nd.TrangThai, kh.MaKH
                                    FROM NguoiDung nd
                                    LEFT JOIN KhachHang kh ON nd.MaNguoiDung = kh.MaNguoiDung
                                    WHERE nd.TenDangNhap LIKE @keyword OR nd.HoTen LIKE @keyword 
                                       OR nd.Email LIKE @keyword OR nd.SoDienThoai LIKE @keyword
                                    ORDER BY nd.MaNguoiDung DESC";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                    SetupDataGridView();
                    if (dt.Rows.Count == 0)
                        MessageBox.Show("Không tìm thấy kết quả phù hợp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tool_them_Click(object sender, EventArgs e)
        {
            isAddingNew = true;
            ClearTextBoxes();
            UnlockTextBoxes();
            txt_ngaytao.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txt_tendangnhap.Focus();
            MessageBox.Show("Nhập thông tin tài khoản mới và nhấn nút LƯU!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void tool_sua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_manguoidung.Text))
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần sửa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            isAddingNew = false;
            UnlockTextBoxes();
            txt_tendangnhap.Enabled = false;
            MessageBox.Show("Sửa thông tin và nhấn nút LƯU!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void tool_luu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_tendangnhap.Text)) { MessageBox.Show("Tên đăng nhập không được để trống!"); txt_tendangnhap.Focus(); return; }
            if (string.IsNullOrWhiteSpace(txt_matkhau.Text)) { MessageBox.Show("Mật khẩu không được để trống!"); txt_matkhau.Focus(); return; }
            if (txt_matkhau.Text.Length < 6) { MessageBox.Show("Mật khẩu phải có ít nhất 6 ký tự!"); txt_matkhau.Focus(); return; }
            if (string.IsNullOrWhiteSpace(txt_email.Text)) { MessageBox.Show("Email không được để trống!"); txt_email.Focus(); return; }

            if (isAddingNew) ThemTaiKhoanMoi();
            else CapNhatTaiKhoan();
        }

        private void ThemTaiKhoanMoi()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string checkQuery = "SELECT COUNT(*) FROM NguoiDung WHERE TenDangNhap = @username";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@username", txt_tendangnhap.Text.Trim());
                        if ((int)checkCmd.ExecuteScalar() > 0)
                        {
                            MessageBox.Show("Tên đăng nhập đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    string insertUserQuery = @"INSERT INTO NguoiDung (TenDangNhap, MatKhau, HoTen, Email, SoDienThoai, MaPhanQuyen, TrangThai)
                                              VALUES (@username, @password, @hoten, @email, @sdt, 2, 1); SELECT SCOPE_IDENTITY();";
                    int maNguoiDung;
                    using (SqlCommand cmd = new SqlCommand(insertUserQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", txt_tendangnhap.Text.Trim());
                        cmd.Parameters.AddWithValue("@password", txt_matkhau.Text);
                        cmd.Parameters.AddWithValue("@hoten", txt_hovaten.Text.Trim());
                        cmd.Parameters.AddWithValue("@email", txt_email.Text.Trim());
                        cmd.Parameters.AddWithValue("@sdt", txt_sodienthoai.Text.Trim());
                        maNguoiDung = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    string queryGetMaxKH = "SELECT ISNULL(MAX(CAST(SUBSTRING(MaKH, 3, LEN(MaKH)) AS INT)), 0) FROM KhachHang WHERE MaKH LIKE 'KH%'";
                    int maxNumber = (int)new SqlCommand(queryGetMaxKH, conn).ExecuteScalar();
                    string maKH = "KH" + (maxNumber + 1).ToString("D3");

                    string insertKHQuery = "INSERT INTO KhachHang (MaKH, MaNguoiDung) VALUES (@maKH, @maNguoiDung)";
                    using (SqlCommand cmd = new SqlCommand(insertKHQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@maKH", maKH);
                        cmd.Parameters.AddWithValue("@maNguoiDung", maNguoiDung);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show($"Thêm tài khoản thành công!\nMã khách hàng: {maKH}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSachTaiKhoan();
                    ClearTextBoxes();
                    LockTextBoxes();
                    isAddingNew = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CapNhatTaiKhoan()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string updateQuery = @"UPDATE NguoiDung SET MatKhau = @password, HoTen = @hoten, 
                                          Email = @email, SoDienThoai = @sdt WHERE MaNguoiDung = @manguoidung";
                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@password", txt_matkhau.Text);
                        cmd.Parameters.AddWithValue("@hoten", txt_hovaten.Text.Trim());
                        cmd.Parameters.AddWithValue("@email", txt_email.Text.Trim());
                        cmd.Parameters.AddWithValue("@sdt", txt_sodienthoai.Text.Trim());
                        cmd.Parameters.AddWithValue("@manguoidung", Convert.ToInt32(txt_manguoidung.Text));
                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Cập nhật thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadDanhSachTaiKhoan();
                            LockTextBoxes();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật: " + ex.Message);
            }
        }

        private void tool_xoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_manguoidung.Text))
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa tài khoản này?\nHành động này không thể hoàn tác!", "Xác nhận xóa",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        int maNguoiDung = Convert.ToInt32(txt_manguoidung.Text);

                        new SqlCommand("DELETE FROM KhachHang WHERE MaNguoiDung = @id", conn)
                        { Parameters = { new SqlParameter("@id", maNguoiDung) } }.ExecuteNonQuery();

                        new SqlCommand("DELETE FROM NguoiDung WHERE MaNguoiDung = @id", conn)
                        { Parameters = { new SqlParameter("@id", maNguoiDung) } }.ExecuteNonQuery();

                        MessageBox.Show("Xóa thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDanhSachTaiKhoan();
                        ClearTextBoxes();
                        LockTextBoxes();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa: " + ex.Message);
                }
            }
        }

        private void tool_thoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                this.Close();
        }

        // Bo góc panel (giữ nguyên của bạn)
        private void panel1_Paint(object sender, PaintEventArgs e) => BoGocPanel(panel1, e.Graphics, 20, Color.FromArgb(255, 255, 224));
        private void panel2_Paint(object sender, PaintEventArgs e) => BoGocPanel(panel2, e.Graphics, 20, Color.FromArgb(255, 255, 224));

        private void BoGocPanel(Panel panel, Graphics g, int radius, Color bg)
        {
            Rectangle rect = new Rectangle(0, 0, panel.Width - 1, panel.Height - 1);
            GraphicsPath path = TaoHinhBoGoc(rect, radius);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            using (SolidBrush b = new SolidBrush(bg)) g.FillPath(b, path);
            using (Pen p = new Pen(Color.FromArgb(200, 200, 200))) g.DrawPath(p, path);
            path.Dispose();
        }

        private GraphicsPath TaoHinhBoGoc(Rectangle r, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            float d = radius * 2f;
            path.AddArc(r.X, r.Y, d, d, 180, 90);
            path.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            path.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        // Các event trống (giữ nguyên)
        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e) { }
        private void txt_tendangnhap_TextChanged(object sender, EventArgs e) { }
        private void txt_matkhau_TextChanged(object sender, EventArgs e) { }
        private void txt_hovaten_TextChanged(object sender, EventArgs e) { }
        private void txt_manguoidung_TextChanged(object sender, EventArgs e) { }
        private void txt_email_TextChanged(object sender, EventArgs e) { }
        private void txt_sodienthoai_TextChanged(object sender, EventArgs e) { }
        private void txt_ngaytao_TextChanged(object sender, EventArgs e) { }
        private void txt_makhachhang_TextChanged(object sender, EventArgs e) { }
        private void txt_timkiem_TextChanged(object sender, EventArgs e) { }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}