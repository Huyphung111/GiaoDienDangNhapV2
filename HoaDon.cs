using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace GiaoDienDangNhap
{
    public partial class HoaDon : Form
    {
        string connectionString = @"Data Source=HUYNE;Initial Catalog=QUANLY_PETSHOP_V9;Integrated Security=True;TrustServerCertificate=True";

        SqlConnection conn;
        SqlDataAdapter daHoaDon;
        DataTable dtHoaDon;

        SqlDataAdapter daChiTiet;
        DataTable dtChiTiet;

        bool isAdding = false;

        public HoaDon()
        {
            InitializeComponent();
        }

        // ============================================
        // LOAD FORM
        // ============================================
        private void HoaDon_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(connectionString);

            cb_trangthai.Items.Clear();
            cb_trangthai.Items.Add("Đã thanh toán");
            cb_trangthai.Items.Add("Chưa thanh toán");

            dataGridView1.CellClick += dataGridView1_CellClick;

            gb_ChiTietHoaDon.Visible = false;

            cb_macthd.DropDownStyle = ComboBoxStyle.DropDownList;

            SetEditable(false);
            LoadDanhSachHoaDon();
        }

        // ============================================
        // KHÓA/MỞ Ô NHẬP
        // ============================================
        private void SetEditable(bool enable)
        {
            txt_mahoadon.ReadOnly = true;
            txt_madonhang.ReadOnly = true;
            txt_phieudichvu.ReadOnly = true;
            txt_loaihoadon.ReadOnly = true;

            datetime_ngaytao.Enabled = enable;
            txt_tongtien.ReadOnly = !enable;
            cb_trangthai.Enabled = enable;
            txt_ghichu.ReadOnly = !enable;
        }

        // ============================================
        // LOAD DANH SÁCH HÓA ĐƠN
        // ============================================
        private void LoadDanhSachHoaDon()
        {
            try
            {
                string sql = @"SELECT MaHD, MaDonHang, MaPhieu, LoaiHoaDon,
                                      NgayTao, TongTien, TrangThai, GhiChu 
                               FROM HoaDon";

                daHoaDon = new SqlDataAdapter(sql, conn);
                dtHoaDon = new DataTable();
                daHoaDon.Fill(dtHoaDon);

                dataGridView1.DataSource = dtHoaDon;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load hóa đơn: " + ex.Message);
            }
        }

        // ============================================
        // HIỂN THỊ CHI TIẾT THEO ROW
        // ============================================
        private void ShowChiTiet(DataRow row)
        {
            if (row == null) return;

            Txt_mahoadon_gb.Text = row["MaHD"].ToString();
            txt_mota.Text = row["MoTa"].ToString();
            txt_soluong.Text = row["SoLuong"].ToString();
            txt_dongia.Text = row["DonGia"].ToString();
            txt_thanhtien.Text = row["ThanhTien"].ToString();
        }

        // ============================================
        // LOAD CHI TIẾT HÓA ĐƠN THEO MaHD
        // ============================================
        private void LoadChiTietHoaDon(string maHD)
        {
            try
            {
                string sql = @"SELECT MaCTHD, MaHD, MoTa, SoLuong, DonGia, ThanhTien
                               FROM ChiTietHoaDon
                               WHERE MaHD = @MaHD";

                daChiTiet = new SqlDataAdapter(sql, conn);
                daChiTiet.SelectCommand.Parameters.AddWithValue("@MaHD", maHD);

                dtChiTiet = new DataTable();
                daChiTiet.Fill(dtChiTiet);

                if (dtChiTiet.Rows.Count == 0)
                {
                    cb_macthd.DataSource = null;

                    Txt_mahoadon_gb.Clear();
                    txt_mota.Clear();
                    txt_soluong.Clear();
                    txt_dongia.Clear();
                    txt_thanhtien.Clear();

                    MessageBox.Show("Hóa đơn này chưa có chi tiết!");
                    return;
                }

                cb_macthd.DataSource = dtChiTiet;
                cb_macthd.DisplayMember = "MaCTHD";
                cb_macthd.ValueMember = "MaCTHD";

                ShowChiTiet(dtChiTiet.Rows[0]);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi chi tiết hóa đơn: " + ex.Message);
            }
        }

        // ============================================
        // CLICK DÒNG HÓA ĐƠN
        // ============================================
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

            txt_mahoadon.Text = row.Cells["MaHD"].Value?.ToString();
            txt_madonhang.Text = row.Cells["MaDonHang"].Value?.ToString();
            txt_phieudichvu.Text = row.Cells["MaPhieu"].Value?.ToString();
            txt_loaihoadon.Text = row.Cells["LoaiHoaDon"].Value?.ToString();

            if (row.Cells["NgayTao"].Value != DBNull.Value)
                datetime_ngaytao.Value = Convert.ToDateTime(row.Cells["NgayTao"].Value);

            txt_tongtien.Text = row.Cells["TongTien"].Value?.ToString();
            cb_trangthai.Text = row.Cells["TrangThai"].Value?.ToString();
            txt_ghichu.Text = row.Cells["GhiChu"].Value?.ToString();

            // Reset chi tiết
            gb_ChiTietHoaDon.Visible = false;
            cb_macthd.DataSource = null;
        }

        // ============================================
        // NÚT HIỆN CHI TIẾT HÓA ĐƠN
        // ============================================
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_mahoadon.Text))
            {
                MessageBox.Show("Chọn hóa đơn trước!");
                return;
            }

            gb_ChiTietHoaDon.Visible = true;
            LoadChiTietHoaDon(txt_mahoadon.Text);
        }

        // ============================================
        // COMBOBOX MACTHD THAY ĐỔI
        // ============================================
        private void cb_macthd_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dtChiTiet == null || dtChiTiet.Rows.Count == 0) return;
            if (cb_macthd.SelectedIndex < 0) return;

            ShowChiTiet(dtChiTiet.Rows[cb_macthd.SelectedIndex]);
        }

        // ============================================
        // NÚT THÊM
        // ============================================
        private void tool_them_Click(object sender, EventArgs e)
        {
            isAdding = true;

            txt_mahoadon.Text = "";
            txt_madonhang.Text = "";
            txt_phieudichvu.Text = "";
            txt_loaihoadon.Text = "";
            txt_tongtien.Text = "";
            txt_ghichu.Text = "";
            cb_trangthai.SelectedIndex = -1;

            datetime_ngaytao.Value = DateTime.Now;

            txt_mahoadon.ReadOnly = false;
            SetEditable(true);
            txt_mahoadon.Focus();
        }

        // ============================================
        // NÚT LƯU
        // ============================================
        private void tool_luu_Click(object sender, EventArgs e)
        {
            if (isAdding)
            {
                SqlCommand cmd = new SqlCommand(
                    @"INSERT INTO HoaDon (MaHD, MaDonHang, MaPhieu, LoaiHoaDon,
                                          NgayTao, TongTien, TrangThai, GhiChu)
                      VALUES (@MaHD, @MaDonHang, @MaPhieu, @LoaiHoaDon,
                              @NgayTao, @TongTien, @TrangThai, @GhiChu)", conn);

                cmd.Parameters.AddWithValue("@MaHD", txt_mahoadon.Text);
                cmd.Parameters.AddWithValue("@MaDonHang",
                    string.IsNullOrWhiteSpace(txt_madonhang.Text) ? (object)DBNull.Value : txt_madonhang.Text);
                cmd.Parameters.AddWithValue("@MaPhieu",
                    string.IsNullOrWhiteSpace(txt_phieudichvu.Text) ? (object)DBNull.Value : txt_phieudichvu.Text);
                cmd.Parameters.AddWithValue("@LoaiHoaDon", txt_loaihoadon.Text);
                cmd.Parameters.AddWithValue("@NgayTao", datetime_ngaytao.Value);
                cmd.Parameters.AddWithValue("@TongTien", decimal.Parse(txt_tongtien.Text));
                cmd.Parameters.AddWithValue("@TrangThai", cb_trangthai.Text);
                cmd.Parameters.AddWithValue("@GhiChu", txt_ghichu.Text);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Thêm hóa đơn thành công!");
                SetEditable(false);
            }
            else
            {
                SqlCommand cmd = new SqlCommand(
                    @"UPDATE HoaDon SET 
                           MaDonHang = @MaDonHang,
                           MaPhieu = @MaPhieu,
                           LoaiHoaDon = @LoaiHoaDon,
                           NgayTao = @NgayTao,
                           TongTien = @TongTien,
                           TrangThai = @TrangThai,
                           GhiChu = @GhiChu
                      WHERE MaHD = @MaHD", conn);

                cmd.Parameters.AddWithValue("@MaHD", txt_mahoadon.Text);
                cmd.Parameters.AddWithValue("@MaDonHang",
                    string.IsNullOrWhiteSpace(txt_madonhang.Text) ? (object)DBNull.Value : txt_madonhang.Text);
                cmd.Parameters.AddWithValue("@MaPhieu",
                    string.IsNullOrWhiteSpace(txt_phieudichvu.Text) ? (object)DBNull.Value : txt_phieudichvu.Text);
                cmd.Parameters.AddWithValue("@LoaiHoaDon", txt_loaihoadon.Text);
                cmd.Parameters.AddWithValue("@NgayTao", datetime_ngaytao.Value);
                cmd.Parameters.AddWithValue("@TongTien", decimal.Parse(txt_tongtien.Text));
                cmd.Parameters.AddWithValue("@TrangThai", cb_trangthai.Text);
                cmd.Parameters.AddWithValue("@GhiChu", txt_ghichu.Text);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Cập nhật thành công!");
                SetEditable(false);
            }

            LoadDanhSachHoaDon();
        }

        // ============================================
        // NÚT SỬA
        // ============================================
        private void tool_sua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_mahoadon.Text))
            {
                MessageBox.Show("Chọn hóa đơn cần sửa!");
                return;
            }

            isAdding = false;
            SetEditable(true);
        }

        // ============================================
        // NÚT XÓA
        // ============================================
        private void tool_xoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_mahoadon.Text))
            {
                MessageBox.Show("Chọn hóa đơn cần xóa!");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn xóa hóa đơn này?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                SqlCommand cmd = new SqlCommand(
                    "DELETE FROM HoaDon WHERE MaHD = @MaHD", conn);

                cmd.Parameters.AddWithValue("@MaHD", txt_mahoadon.Text);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Xóa thành công!");
                LoadDanhSachHoaDon();
            }
        }

        // ============================================
        // CÁC EVENT RỖNG (NHƯ YÊU CẦU)
        // ============================================
        private void txt_mahoadon_TextChanged(object sender, EventArgs e) { }
        private void txt_madonhang_TextChanged(object sender, EventArgs e) { }
        private void txt_phieudichvu_TextChanged(object sender, EventArgs e) { }
        private void txt_loaihoadon_TextChanged(object sender, EventArgs e) { }
        private void datetime_ngaytao_ValueChanged(object sender, EventArgs e) { }
        private void txt_tongtien_TextChanged(object sender, EventArgs e) { }
        private void cb_trangthai_SelectedIndexChanged(object sender, EventArgs e) { }
        private void txt_ghichu_TextChanged(object sender, EventArgs e) { }
        private void Txt_mahoadon_gb_TextChanged(object sender, EventArgs e) { }
        private void txt_mota_TextChanged(object sender, EventArgs e) { }
        private void txt_soluong_TextChanged(object sender, EventArgs e) { }
        private void txt_dongia_TextChanged(object sender, EventArgs e) { }
        private void txt_thanhtien_TextChanged(object sender, EventArgs e) { }
        private void gb_ChiTietHoaDon_Enter(object sender, EventArgs e)
        {
            // Không làm gì cả (hàm rỗng)
        }


    }
}
