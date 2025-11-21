using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GiaoDienDangNhap
{
    public partial class GiaoDien : Form
    {
        public GiaoDien()
        {
            InitializeComponent();
        }

        private void GiaoDien_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void panel_NoiDung_Paint(object sender, PaintEventArgs e)
        {
        }

        // ═══════════════════════════════════════════════════════════
        // NÚT THÚ CƯNG
        // ═══════════════════════════════════════════════════════════
        // ═══════════════════════════════════════════════════════════
        // NÚT THÚ CƯNG
        // ═══════════════════════════════════════════════════════════
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Xóa các control cũ trong panel (nếu có)
                panel_NoiDung.Controls.Clear();

                // Tạo instance của form ThuCung
                ThuCung formThuCung = new ThuCung();

                // Set các thuộc tính để form hiển thị như một control
                formThuCung.TopLevel = false;
                formThuCung.FormBorderStyle = FormBorderStyle.None;
                formThuCung.Dock = DockStyle.Fill;

                // Thêm form vào panel
                panel_NoiDung.Controls.Add(formThuCung);

                // Hiển thị form
                formThuCung.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở form Thú Cưng: " + ex.Message + "\n\nStack Trace: " + ex.StackTrace,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ═══════════════════════════════════════════════════════════
        // NÚT DỊCH VỤ
        // ═══════════════════════════════════════════════════════════
        private void button2_Click(object sender, EventArgs e)
        {
            // Xóa các control cũ trong panel (nếu có)
            panel_NoiDung.Controls.Clear();

            // TODO: Thêm form dịch vụ nếu có
            MessageBox.Show("Form Dịch Vụ đang được phát triển!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ═══════════════════════════════════════════════════════════
        // NÚT PHỤ KIỆN (method cũ - giữ rỗng)
        // ═══════════════════════════════════════════════════════════
        private void button3_Click(object sender, EventArgs e)
        {
        }

        // ═══════════════════════════════════════════════════════════
        // NÚT HÓA ĐƠN
        // ═══════════════════════════════════════════════════════════
        private void button4_Click(object sender, EventArgs e)
        {
            // Xóa các control cũ trong panel (nếu có)
            panel_NoiDung.Controls.Clear();

            // TODO: Thêm form hóa đơn nếu có
            MessageBox.Show("Form Hóa Đơn đang được phát triển!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ═══════════════════════════════════════════════════════════
        // NÚT BÁO CÁO - THỐNG KÊ
        // ═══════════════════════════════════════════════════════════
        private void button5_Click(object sender, EventArgs e)
        {
            // Xóa các control cũ trong panel (nếu có)
            panel_NoiDung.Controls.Clear();

            // Tạo instance của form ThongKeBaoCao
            ThongKeBaoCao formBaoCao = new ThongKeBaoCao();

            // Set các thuộc tính để form hiển thị như một control
            formBaoCao.TopLevel = false;
            formBaoCao.FormBorderStyle = FormBorderStyle.None;
            formBaoCao.Dock = DockStyle.Fill;

            // Thêm form vào panel
            panel_NoiDung.Controls.Add(formBaoCao);

            // Hiển thị form
            formBaoCao.Show();
        }

        // ═══════════════════════════════════════════════════════════
        // NÚT TÀI KHOẢN
        // ═══════════════════════════════════════════════════════════
        private void button6_Click(object sender, EventArgs e)
        {
            // Xóa các control cũ trong panel (nếu có)
            panel_NoiDung.Controls.Clear();

            // Tạo instance của form TrangQuanLyTaiKhoan
            TrangQuanLyTaiKhoan formTaiKhoan = new TrangQuanLyTaiKhoan();

            // Set các thuộc tính để form hiển thị như một control
            formTaiKhoan.TopLevel = false;
            formTaiKhoan.FormBorderStyle = FormBorderStyle.None;
            formTaiKhoan.Dock = DockStyle.Fill;

            // Thêm form vào panel
            panel_NoiDung.Controls.Add(formTaiKhoan);

            // Hiển thị form
            formTaiKhoan.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
        }

        // ═══════════════════════════════════════════════════════════
        // NÚT PHỤ KIỆN - METHOD ĐÚNG
        // ═══════════════════════════════════════════════════════════
        private void button3_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Xóa các control cũ trong panel (nếu có)
                panel_NoiDung.Controls.Clear();

                // Tạo instance của form SanPham
                SanPham formSanPham = new SanPham();

                // Set các thuộc tính để form hiển thị như một control
                formSanPham.TopLevel = false;
                formSanPham.FormBorderStyle = FormBorderStyle.None;
                formSanPham.Dock = DockStyle.Fill;

                // Thêm form vào panel
                panel_NoiDung.Controls.Add(formSanPham);

                // Hiển thị form
                formSanPham.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở form Sản Phẩm: " + ex.Message + "\n\nStack Trace: " + ex.StackTrace,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}