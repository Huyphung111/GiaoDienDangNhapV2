using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace GiaoDienDangNhap
{
    public partial class Form2 : Form
    {
        // Connection String - THAY ĐỔI THEO CẤU HÌNH CỦA BẠN
        private string connectionString = @"Data Source=HUYNE;Initial Catalog=QUANLY_PETSHOP_V9;Integrated Security=True;TrustServerCertificate=True";

        public Form2()
        {
            InitializeComponent();

            // Khởi tạo - set password char
            textBox2.PasswordChar = '*';
            textBox3.PasswordChar = '*';

            // ẨN TẤT CẢ LABEL LỖI KHI MỞ FORM
            txt_tdn.Visible = false;  // "Tên tài khoản đã có người dùng"
            label5.Visible = false;   // "Mật khẩu nhập lại không trùng khớp"
            txt_matkhauitnhat6kitu.Visible = false;  // "Mật khẩu ít nhất 6 kí tự"
            txt_emailkhongphuhop.Visible = false;  // "Email không phù hợp"
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                textBox1.ForeColor = Color.Black;

                // Ẩn label lỗi khi user bắt đầu nhập
                if (!string.IsNullOrEmpty(textBox1.Text))
                {
                    txt_tdn.Visible = false;
                }
            }
            catch { }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                textBox2.ForeColor = Color.Black;

                // Ẩn label lỗi khi user bắt đầu nhập
                if (!string.IsNullOrEmpty(textBox2.Text))
                {
                    txt_matkhauitnhat6kitu.Visible = false;
                }
            }
            catch { }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                textBox3.ForeColor = Color.Black;

                // Ẩn label lỗi khi user bắt đầu nhập
                if (!string.IsNullOrEmpty(textBox3.Text))
                {
                    label5.Visible = false;
                }
            }
            catch { }
        }

        private void txt_email_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // Ẩn label lỗi email khi user bắt đầu nhập
                if (!string.IsNullOrEmpty(((TextBox)sender).Text))
                {
                    txt_emailkhongphuhop.Visible = false;
                }
            }
            catch { }
        }

        // HÀM KIỂM TRA TÀI KHOẢN ĐÃ TỒN TẠI TRONG SQL
        private bool TaiKhoanDaTonTai(string username)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT COUNT(*) FROM NguoiDung WHERE TenDangNhap = @username";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        int count = (int)cmd.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối database: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // HÀM KIỂM TRA EMAIL HỢP LỆ
        private bool EmailHopLe(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        // HÀM KIỂM TRA EMAIL ĐÃ TỒN TẠI TRONG SQL
        private bool EmailDaTonTai(string email)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT COUNT(*) FROM NguoiDung WHERE Email = @email";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@email", email);
                        int count = (int)cmd.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối database: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // ✅ HÀM THÊM TÀI KHOẢN MỚI VÀO SQL - ĐÃ SỬA LỖI
        private bool ThemTaiKhoanVaoSQL(string username, string password, string email, string hoTen = "Khách hàng")
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Tạo người dùng (mặc định là khách hàng)
                    string queryNguoiDung = @"INSERT INTO NguoiDung (TenDangNhap, MatKhau, HoTen, Email, MaPhanQuyen, TrangThai) 
                                            VALUES (@username, @password, @hoTen, @email, 2, 1);
                                            SELECT SCOPE_IDENTITY();";

                    int maNguoiDung;
                    using (SqlCommand cmd = new SqlCommand(queryNguoiDung, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);
                        cmd.Parameters.AddWithValue("@hoTen", hoTen);
                        cmd.Parameters.AddWithValue("@email", email);

                        maNguoiDung = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    // ✅ SỬA: Tạo MaKH tự động theo format KH + số thứ tự
                    string queryGetMaxKH = "SELECT ISNULL(MAX(CAST(SUBSTRING(MaKH, 3, LEN(MaKH)) AS INT)), 0) FROM KhachHang WHERE MaKH LIKE 'KH%'";
                    int maxNumber;
                    using (SqlCommand cmd = new SqlCommand(queryGetMaxKH, conn))
                    {
                        maxNumber = (int)cmd.ExecuteScalar();
                    }

                    string maKH = "KH" + (maxNumber + 1).ToString("D3"); // VD: KH001, KH002, KH003...

                    // Tạo record khách hàng với MaKH
                    string queryKhachHang = "INSERT INTO KhachHang (MaKH, MaNguoiDung) VALUES (@maKH, @maNguoiDung)";
                    using (SqlCommand cmd2 = new SqlCommand(queryKhachHang, conn))
                    {
                        cmd2.Parameters.AddWithValue("@maKH", maKH);
                        cmd2.Parameters.AddWithValue("@maNguoiDung", maNguoiDung);
                        cmd2.ExecuteNonQuery();
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tạo tài khoản: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // ẨN TẤT CẢ LABEL LỖI TRƯỚC KHI KIỂM TRA
            txt_tdn.Visible = false;
            label5.Visible = false;
            txt_matkhauitnhat6kitu.Visible = false;
            txt_emailkhongphuhop.Visible = false;

            // Lấy dữ liệu
            string username = textBox1.Text.Trim();
            string password = textBox2.Text;
            string confirmPassword = textBox3.Text;
            string email = txt_email.Text.Trim();

            // KIỂM TRA TÊN TÀI KHOẢN
            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Tên tài khoản không được để trống!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Focus();
                return;
            }

            if (username.Length < 3)
            {
                MessageBox.Show("Tên tài khoản phải có ít nhất 3 ký tự!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Focus();
                return;
            }

            // KIỂM TRA TÀI KHOẢN ĐÃ TỒN TẠI TRONG SQL
            if (TaiKhoanDaTonTai(username))
            {
                txt_tdn.Visible = true;
                textBox1.Focus();
                return;
            }

            // KIỂM TRA MẬT KHẨU
            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Mật khẩu không được để trống!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Focus();
                return;
            }

            if (password.Length < 6)
            {
                txt_matkhauitnhat6kitu.Visible = true;
                textBox2.Focus();
                return;
            }

            // KIỂM TRA NHẬP LẠI MẬT KHẨU
            if (string.IsNullOrWhiteSpace(confirmPassword))
            {
                MessageBox.Show("Vui lòng nhập lại mật khẩu!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox3.Focus();
                return;
            }

            // KIỂM TRA MẬT KHẨU KHỚP NHAU
            if (password != confirmPassword)
            {
                label5.Visible = true;
                textBox3.Focus();
                return;
            }

            // KIỂM TRA EMAIL
            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Email không được để trống!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!EmailHopLe(email))
            {
                txt_emailkhongphuhop.Visible = true;
                return;
            }

            if (EmailDaTonTai(email))
            {
                MessageBox.Show("Email này đã được sử dụng!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // NẾU HỢP LỆ - THÊM TÀI KHOẢN MỚI VÀO SQL
            if (ThemTaiKhoanVaoSQL(username, password, email))
            {
                // Cập nhật lại dictionary (backup)
                Form1.accounts[username] = password;

                MessageBox.Show("Tạo tài khoản thành công!\nBạn có thể đăng nhập ngay bây giờ.",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // Đóng form đăng ký và quay về form đăng nhập
                this.Close();
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
        }

        private void txt_tdn_Click(object sender, EventArgs e)
        {
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.ForeColor == Color.Gray)
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black;
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.ForeColor == Color.Gray)
            {
                textBox2.Text = "";
                textBox2.ForeColor = Color.Black;
            }
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.ForeColor == Color.Gray)
            {
                textBox3.Text = "";
                textBox3.ForeColor = Color.Black;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button1_Click(sender, e);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txt_matkhauitnhat6kitu_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void txt_emailkhongphuhop_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}