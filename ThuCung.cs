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

            LoadLoai();
            LoadThuCung();
            LoadAnhVaoGrid();
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

                dataGridView1.Columns["HinhAnh"].HeaderText = "Tên File Ảnh";
                dataGridView1.Columns["HinhAnh"].Name = "HinhAnh_File";
            }
        }

        // ====================================================
        // 3. Load hình ảnh vào DataGridView
        // ====================================================
        void LoadAnhVaoGrid()
        {
            string folder = Application.StartupPath + "\\Images\\ThuCung\\";

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                try
                {
                    // lấy tên file từ cột HinhAnh (cột trong SQL)
                    var val = row.Cells["HinhAnh"].Value;
                    if (val == null) continue;

                    string file = val.ToString();
                    if (string.IsNullOrWhiteSpace(file)) continue;

                    string fullPath = folder + file;

                    // gán vào cột ImageColumn: HinhAnh_File
                    if (File.Exists(fullPath))
                    {
                        row.Cells["HinhAnh_File"].Value = Image.FromFile(fullPath);
                    }
                }
                catch
                {
                    // bỏ qua dòng lỗi
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

                string dest = Application.StartupPath + "\\Images\\ThuCung\\" + fileAnh;
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

            DataGridViewRow r = dataGridView1.Rows[e.RowIndex];

            txtMa.Text = r.Cells["MaThuCung"].Value.ToString();
            txtTen.Text = r.Cells["TenThuCung"].Value.ToString();
            txtTuoi.Text = r.Cells["Tuoi"].Value.ToString();
            txtGiaBan.Text = r.Cells["GiaBan"].Value.ToString();
            txtSoLuong.Text = r.Cells["SoLuong"].Value.ToString();
            txtMota.Text = r.Cells["MoTa"].Value.ToString();
            cbMaloai.SelectedValue = r.Cells["MaLoai"].Value;

            string file = r.Cells["HinhAnh_File"].Value.ToString();
            string full = Application.StartupPath + "\\Images\\ThuCung\\" + file;

            if (File.Exists(full))
                picAnh.Image = Image.FromFile(full);

            fileAnh = file;
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
