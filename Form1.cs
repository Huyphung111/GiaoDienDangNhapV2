using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GiaoDienDangNhap
{
    public partial class Form1 : Form
    {
        // Connection String - THAY ĐỔI THEO CẤU HÌNH CỦA BẠN
        private string connectionString = @"Data Source=HUYNE;Initial Catalog=QUANLY_PETSHOP_V9;Integrated Security=True;TrustServerCertificate=True";
        // Hoặc nếu dùng SQL Authentication:
        // private string connectionString = @"Data Source=YOUR_SERVER_NAME;Initial Catalog=QUANLY_PETSHOP_V3;User ID=sa;Password=your_password";

        // Danh sách lưu tài khoản (backup - không dùng nữa khi có SQL)
        public static Dictionary<string, string> accounts = new Dictionary<string, string>()
        {
            { "admin", "123456" }
        };

        public Form1()
        {
            InitializeComponent();
            // Khởi tạo
            textBox2.PasswordChar = '*'; // Ẩn mật khẩu
            txt_tdn.Visible = false;
            txt_matkhau.Visible = false;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
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
                    txt_matkhau.Visible = false;
                }
            }
            catch { }
        }

        // HÀM KIỂM TRA ĐĂNG NHẬP TỪ SQL SERVER
        private bool KiemTraDangNhap(string username, string password, out string hoTen, out int maPhanQuyen)
        {
            hoTen = "";
            maPhanQuyen = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"SELECT HoTen, MaPhanQuyen 
                                   FROM NguoiDung 
                                   WHERE TenDangNhap = @username 
                                   AND MatKhau = @password 
                                   AND TrangThai = 1";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                hoTen = reader["HoTen"].ToString();
                                maPhanQuyen = Convert.ToInt32(reader["MaPhanQuyen"]);
                                return true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối database: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Ẩn tất cả label lỗi trước
            txt_tdn.Visible = false;
            txt_matkhau.Visible = false;

            // Xác thực đăng nhập
            bool isValid = true;

            // Kiểm tra tài khoản
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                txt_tdn.Visible = true;
                txt_tdn.Text = "Tên đăng nhập không được để trống!";
                txt_tdn.ForeColor = Color.Red;
                isValid = false;
            }

            // Kiểm tra mật khẩu
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                txt_matkhau.Visible = true;
                txt_matkhau.Text = "Mật khẩu không được để trống!";
                txt_matkhau.ForeColor = Color.Red;
                isValid = false;
            }

            if (!isValid)
                return;

            // Xác thực tài khoản và mật khẩu TỪ SQL SERVER
            string username = textBox1.Text.Trim();
            string password = textBox2.Text;
            string hoTen;
            int maPhanQuyen;

            // Kiểm tra đăng nhập từ database
            if (KiemTraDangNhap(username, password, out hoTen, out maPhanQuyen))
            {
                string quyenHan = (maPhanQuyen == 1) ? "Admin" : "Khách hàng";

                MessageBox.Show($"Đăng nhập thành công!\n\nXin chào: {hoTen}\nQuyền hạn: {quyenHan}",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // Ẩn form đăng nhập
                this.Hide();

                // Mở form GiaoDien
                GiaoDien mainForm = new GiaoDien();
                mainForm.FormClosed += (s, args) => this.Close();
                mainForm.Show();
            }
            else
            {
                txt_tdn.Visible = true;
                txt_tdn.Text = "Tên đăng nhập hoặc mật khẩu không đúng!";
                txt_tdn.ForeColor = Color.Red;
                txt_matkhau.Visible = true;
                txt_matkhau.Text = "Tên đăng nhập hoặc mật khẩu không đúng!";
                txt_matkhau.ForeColor = Color.Red;
            }
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.ForeColor = Color.Silver;
        }

        private void button1_MouseClick(object sender, MouseEventArgs e)
        {
            button1.ForeColor = Color.White;
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void txt_tdn_Click(object sender, EventArgs e)
        {
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            // Khi focus vào textbox
            if (textBox1.ForeColor == Color.Gray)
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.White;
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            // Khi focus vào textbox
            if (textBox2.ForeColor == Color.Gray)
            {
                textBox2.Text = "";
                textBox2.ForeColor = Color.White;
            }
        }

        // Xử lý Enter key để đăng nhập
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button1_Click(sender, e);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Nếu button2 là nút đóng hoặc thoát
            Application.Exit();
        }

        // Chức năng mở Form2 để tạo tài khoản
        private void btn_taotaikhoang_Click(object sender, EventArgs e)
        {
            // Ẩn Form1
            this.Hide();

            Form2 registerForm = new Form2();
            registerForm.ShowDialog();

            // Sau khi đóng Form2, hiện lại Form1 và làm mới
            this.Show();
            textBox1.Clear();
            textBox2.Clear();
            txt_tdn.Visible = false;
            txt_matkhau.Visible = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void label3_Click_2(object sender, EventArgs e)
        {

        }

        private void check_hienmatkhau_CheckedChanged(object sender, EventArgs e)
        {
            // Nếu checkbox được check (tích) = hiển thị mật khẩu
            if (check_hienmatkhau.Checked)
            {
                textBox2.PasswordChar = '\0'; // '\0' = không ẩn (hiển thị bình thường)
            }
            else
            {
                textBox2.PasswordChar = '*'; // '*' = ẩn mật khẩu bằng dấu *
            }
        }

        // ===================================================================
        // THÊM CODE NÀY VÀO FORM1.CS
        // ===================================================================

        // Chức năng mở Form DoiMatKhau để đổi mật khẩu
        private void btn_doimatkhau_Click(object sender, EventArgs e)
        {
            // Ẩn Form1 (form đăng nhập)
            this.Hide();

            // Tạo và hiển thị form đổi mật khẩu
            DoiMatKhau doiMatKhauForm = new DoiMatKhau();
            doiMatKhauForm.ShowDialog(); // ShowDialog = hiển thị dạng modal (phải đóng mới làm việc khác)

            // Sau khi đóng form đổi mật khẩu, hiện lại Form1
            this.Show();

            // Làm mới các trường nhập liệu
            textBox1.Clear();
            textBox2.Clear();
            txt_tdn.Visible = false;
            txt_matkhau.Visible = false;
        }


        // ===================================================================
        // HOẶC NẾU BẠN MUỐN KIỂM TRA TRẠNG THÁI ĐĂNG NHẬP TRƯỚC KHI ĐỔI:
        // ===================================================================

        // Biến lưu trạng thái đăng nhập (thêm vào đầu class Form1)
        private static bool isDangNhap = false;
        private static string currentUsername = "";

        // Cập nhật lại hàm button1_Click (nút Đăng nhập) - thêm sau khi đăng nhập thành công:
        /*
        if (KiemTraDangNhap(username, password, out hoTen, out maPhanQuyen))
        {
            // ... code cũ ...

            // Lưu trạng thái đăng nhập
            isDangNhap = true;
            currentUsername = username;

            // ... code cũ ...
        }
        */

        // Phiên bản nâng cao của btn_doimatkhau_Click
        private void btn_doimatkhau_Click_Advanced(object sender, EventArgs e)
        {
            // OPTION 1: Cho phép đổi mật khẩu mà không cần đăng nhập
            // (Người dùng phải nhập đúng tài khoản + mật khẩu hiện tại trong form đổi mật khẩu)

            this.Hide();
            DoiMatKhau doiMatKhauForm = new DoiMatKhau();
            doiMatKhauForm.ShowDialog();
            this.Show();

            // Làm mới form
            textBox1.Clear();
            textBox2.Clear();
            txt_tdn.Visible = false;
            txt_matkhau.Visible = false;
        }

        private void btn_doimatkhau_Click_RequireLogin(object sender, EventArgs e)
        {
            // Kiểm tra đã đăng nhập chưa
            if (!isDangNhap)
            {
                MessageBox.Show("Vui lòng đăng nhập trước khi đổi mật khẩu!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // Nếu đã đăng nhập, mở form đổi mật khẩu
            this.Hide();
            DoiMatKhau doiMatKhauForm = new DoiMatKhau();

            // Có thể truyền username vào form đổi mật khẩu để tự động điền
            // (Cần thêm property public string Username trong DoiMatKhau)
            // doiMatKhauForm.Username = currentUsername;

            doiMatKhauForm.ShowDialog();
            this.Show();
        }
    }
}