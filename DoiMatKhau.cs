// ===================================================================
// CODE CHO FORM1 - Thêm vào phương thức btn_doimatkhau_Click
// ===================================================================
/*
private void btn_doimatkhau_Click(object sender, EventArgs e)
{
    // Ẩn Form1
    this.Hide();

    DoiMatKhau doiMatKhauForm = new DoiMatKhau();
    doiMatKhauForm.ShowDialog();

    // Sau khi đóng form đổi mật khẩu, hiện lại Form1
    this.Show();
    
    // Làm mới form đăng nhập
    textBox1.Clear();
    textBox2.Clear();
    txt_tdn.Visible = false;
    txt_matkhau.Visible = false;
}
*/

// ===================================================================
// CODE CHO FORM DoiMatKhau.cs
// ===================================================================
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
    public partial class DoiMatKhau : Form
    {
        // Connection String - THAY ĐỔI THEO CẤU HÌNH CỦA BẠN
        private string connectionString = @"Data Source=HUYNE;Initial Catalog=QUANLY_PETSHOP_V9;Integrated Security=True;TrustServerCertificate=True";

        public DoiMatKhau()
        {
            InitializeComponent();

            // Khởi tạo - set password char cho các textbox mật khẩu
            txt_matkhauhientai.PasswordChar = '*';
            txt_nhapmatkhaumoi.PasswordChar = '*';
            txt_nhaplai.PasswordChar = '*';

            // ẨN TẤT CẢ LABEL LỖI KHI MỞ FORM
            txt_taikhoangkhonghople.Visible = false;
            txt_matkhaunhapsai.Visible = false;
            matkhauitnhat6kitu.Visible = false;
            matkhaunhaplaikhongdung.Visible = false;
        }

        private void DoiMatKhau_Load(object sender, EventArgs e)
        {
            // Có thể thêm logic khởi tạo nếu cần
        }

        private void txt_taikhoan_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txt_taikhoan.ForeColor = Color.Black;

                // Ẩn label lỗi khi user bắt đầu nhập
                if (!string.IsNullOrEmpty(txt_taikhoan.Text))
                {
                    txt_taikhoangkhonghople.Visible = false;
                }
            }
            catch { }
        }

        private void txt_matkhauhientai_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txt_matkhauhientai.ForeColor = Color.Black;

                // Ẩn label lỗi khi user bắt đầu nhập
                if (!string.IsNullOrEmpty(txt_matkhauhientai.Text))
                {
                    txt_matkhaunhapsai.Visible = false;
                }
            }
            catch { }
        }

        private void txt_nhapmatkhaumoi_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txt_nhapmatkhaumoi.ForeColor = Color.Black;

                // Ẩn label lỗi khi user bắt đầu nhập
                if (!string.IsNullOrEmpty(txt_nhapmatkhaumoi.Text))
                {
                    matkhauitnhat6kitu.Visible = false;
                }
            }
            catch { }
        }

        private void txt_nhaplai_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txt_nhaplai.ForeColor = Color.Black;

                // Ẩn label lỗi khi user bắt đầu nhập
                if (!string.IsNullOrEmpty(txt_nhaplai.Text))
                {
                    matkhaunhaplaikhongdung.Visible = false;
                }
            }
            catch { }
        }

        // HÀM KIỂM TRA TÀI KHOẢN VÀ MẬT KHẨU HIỆN TẠI
        private bool KiemTraTaiKhoanMatKhau(string username, string currentPassword)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"SELECT COUNT(*) 
                                   FROM NguoiDung 
                                   WHERE TenDangNhap = @username 
                                   AND MatKhau = @password 
                                   AND TrangThai = 1";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", currentPassword);

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

        // HÀM CẬP NHẬT MẬT KHẨU MỚI
        private bool CapNhatMatKhau(string username, string newPassword)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"UPDATE NguoiDung 
                                   SET MatKhau = @newPassword 
                                   WHERE TenDangNhap = @username";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@newPassword", newPassword);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật mật khẩu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void btn_doimatkhau_Click(object sender, EventArgs e)
        {
            // ẨN TẤT CẢ LABEL LỖI TRƯỚC KHI KIỂM TRA
            txt_taikhoangkhonghople.Visible = false;
            txt_matkhaunhapsai.Visible = false;
            matkhauitnhat6kitu.Visible = false;
            matkhaunhaplaikhongdung.Visible = false;

            // Lấy dữ liệu từ các textbox
            string username = txt_taikhoan.Text.Trim();
            string currentPassword = txt_matkhauhientai.Text;
            string newPassword = txt_nhapmatkhaumoi.Text;
            string confirmPassword = txt_nhaplai.Text;

            // KIỂM TRA TÊN TÀI KHOẢN
            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Tên tài khoản không được để trống!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_taikhoan.Focus();
                return;
            }

            // KIỂM TRA MẬT KHẨU HIỆN TẠI
            if (string.IsNullOrWhiteSpace(currentPassword))
            {
                MessageBox.Show("Mật khẩu hiện tại không được để trống!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_matkhauhientai.Focus();
                return;
            }

            // KIỂM TRA TÀI KHOẢN VÀ MẬT KHẨU HIỆN TẠI CÓ ĐÚNG KHÔNG
            if (!KiemTraTaiKhoanMatKhau(username, currentPassword))
            {
                txt_taikhoangkhonghople.Visible = true;
                txt_matkhaunhapsai.Visible = true;
                txt_taikhoan.Focus();
                return;
            }

            // KIỂM TRA MẬT KHẨU MỚI
            if (string.IsNullOrWhiteSpace(newPassword))
            {
                MessageBox.Show("Mật khẩu mới không được để trống!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_nhapmatkhaumoi.Focus();
                return;
            }

            if (newPassword.Length < 6)
            {
                matkhauitnhat6kitu.Visible = true;
                txt_nhapmatkhaumoi.Focus();
                return;
            }

            // KIỂM TRA MẬT KHẨU MỚI KHÔNG ĐƯỢC TRÙNG MẬT KHẨU CŨ
            if (newPassword == currentPassword)
            {
                MessageBox.Show("Mật khẩu mới không được trùng với mật khẩu hiện tại!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_nhapmatkhaumoi.Focus();
                return;
            }

            // KIỂM TRA NHẬP LẠI MẬT KHẨU
            if (string.IsNullOrWhiteSpace(confirmPassword))
            {
                MessageBox.Show("Vui lòng nhập lại mật khẩu mới!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_nhaplai.Focus();
                return;
            }

            // KIỂM TRA MẬT KHẨU KHỚP NHAU
            if (newPassword != confirmPassword)
            {
                matkhaunhaplaikhongdung.Visible = true;
                txt_nhaplai.Focus();
                return;
            }

            // NẾU HỢP LỆ - CẬP NHẬT MẬT KHẨU MỚI
            if (CapNhatMatKhau(username, newPassword))
            {
                // Cập nhật lại dictionary (backup) nếu cần
                if (Form1.accounts.ContainsKey(username))
                {
                    Form1.accounts[username] = newPassword;
                }

                MessageBox.Show("Đổi mật khẩu thành công!\nVui lòng đăng nhập lại với mật khẩu mới.",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // Xóa dữ liệu trong form
                txt_taikhoan.Clear();
                txt_matkhauhientai.Clear();
                txt_nhapmatkhaumoi.Clear();
                txt_nhaplai.Clear();

                // Đóng form đổi mật khẩu
                this.Close();
            }
        }

        private void matkhaunhaplaikhongdung_Click(object sender, EventArgs e)
        {
            // Event handler cho label lỗi
        }

        private void matkhauitnhat6kitu_Click(object sender, EventArgs e)
        {
            // Event handler cho label lỗi
        }

        private void txt_matkhaunhapsai_Click(object sender, EventArgs e)
        {
            // Event handler cho label lỗi
        }

        private void txt_taikhoangkhonghople_Click(object sender, EventArgs e)
        {
            // Event handler cho label lỗi
        }

        private void ck_hien_CheckedChanged(object sender, EventArgs e)
        {
            if (ck_hien.Checked)
            {
                // Hiện mật khẩu
                txt_matkhauhientai.PasswordChar = '\0';
                txt_nhapmatkhaumoi.PasswordChar = '\0';
                txt_nhaplai.PasswordChar = '\0';
            }
            else
            {
                // Ẩn mật khẩu (hiện dấu *)
                txt_matkhauhientai.PasswordChar = '*';
                txt_nhapmatkhaumoi.PasswordChar = '*';
                txt_nhaplai.PasswordChar = '*';
            }
        }
    }
}