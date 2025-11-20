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
        private void button1_Click(object sender, EventArgs e)
        {
            ThuCungGiaoDien formThuCung = new ThuCungGiaoDien();
            LoadFormToPanel(formThuCung);
        }

        // ═══════════════════════════════════════════════════════════
        // NÚT DỊCH VỤ (nếu có button2)
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
        // NÚT PHỤ KIỆN (nếu có button3)
        // ═══════════════════════════════════════════════════════════
        private void button3_Click(object sender, EventArgs e)
        {
            SanPham formSanPham = new SanPham();
            LoadFormToPanel(formSanPham);
        }

        // ═══════════════════════════════════════════════════════════
        // NÚT HÓA ĐƠN (nếu có button4)
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

        private void LoadFormToPanel(Form form)
        {
            panel_NoiDung.Controls.Clear();

            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            panel_NoiDung.Controls.Add(form);
            form.Show();
        }
    }
}