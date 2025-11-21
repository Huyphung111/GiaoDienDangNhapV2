using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace GiaoDienDangNhap
{
    public partial class frmDichVu : Form
    {
        // --- CẤU HÌNH KẾT NỐI ---
        string connectionString = @"Data Source=HUYNE;Initial Catalog=QUANLY_PETSHOP_V9;Integrated Security=True";
        private SqlConnection Conn = null;

        // --- BIẾN DÙNG CHUNG ---
        private DataTable dt_DichVu;
        private bool isDataLoaded = false;

        // --- BIẾN TRẠNG THÁI ---
        string mode = "view";
        string modePhieu = "view";

        public frmDichVu()
        {
            InitializeComponent();
            this.VisibleChanged += FrmDichVu_VisibleChanged;
        }

        private void FrmDichVu_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible && !isDataLoaded)
            {
                isDataLoaded = true;
                Application.DoEvents();

                LoadDataGridViewTab2();
                LoadComboBoxKhachHang();
                LoadComboBoxDichVu();
                LoadDataGridViewTab1();

                SetControlsStatus(true);
                SetControlsStatusPhieu(true);
            }
        }

        private void frmDichVu_Load(object sender, EventArgs e) { }

        // ===================================================================
        // TAB 2: QUẢN LÝ DỊCH VỤ
        // ===================================================================

        private void LoadDataGridViewTab2()
        {
            Conn = new SqlConnection(connectionString);
            string sql = "SELECT MaDV, TenDV, Gia, MoTa FROM DichVu";
            dt_DichVu = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(sql, Conn);
            try
            {
                Conn.Open();
                da.Fill(dt_DichVu);
                dgvDanhSachDV.DataSource = null;
                dgvDanhSachDV.DataSource = dt_DichVu;
                dgvDanhSachDV.Columns["MaDV"].HeaderText = "Mã DV";
                dgvDanhSachDV.Columns["TenDV"].HeaderText = "Tên Dịch Vụ";
                dgvDanhSachDV.Columns["Gia"].HeaderText = "Giá";
                dgvDanhSachDV.Columns["MoTa"].HeaderText = "Mô Tả";
                dgvDanhSachDV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvDanhSachDV.Columns["Gia"].DefaultCellStyle.Format = "N0";
            }
            catch (Exception ex) { MessageBox.Show("Lỗi tải Tab 2: " + ex.Message); }
            finally { if (Conn.State == ConnectionState.Open) Conn.Close(); }
        }

        private void dgvDanhSachDV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDanhSachDV.Rows[e.RowIndex];
                txtMaDV.Text = row.Cells["MaDV"].Value.ToString();
                txtTenDV.Text = row.Cells["TenDV"].Value.ToString();
                txtMoTa.Text = row.Cells["MoTa"].Value.ToString();
                if (row.Cells["Gia"].Value != DBNull.Value)
                    numGia.Value = Convert.ToDecimal(row.Cells["Gia"].Value);
                else
                    numGia.Value = 0;
            }
        }

        private void btnThemDV_Click(object sender, EventArgs e)
        {
            mode = "add";
            ClearControls();
            SetControlsStatus(false);
            txtMaDV.Focus();
        }

        private void btnSuaDV_Click(object sender, EventArgs e)
        {
            if (txtMaDV.Text.Trim() == "") { MessageBox.Show("Chọn dịch vụ để sửa!"); return; }
            mode = "edit";
            SetControlsStatus(false);
        }

        private void btnXoaDV_Click(object sender, EventArgs e)
        {
            if (txtMaDV.Text.Trim() == "") return;
            if (MessageBox.Show("Xóa dịch vụ này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Conn = new SqlConnection(connectionString);
                try
                {
                    Conn.Open();
                    SqlCommand cmd = new SqlCommand($"DELETE FROM DichVu WHERE MaDV = N'{txtMaDV.Text}'", Conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Đã xóa!");
                    ClearControls();
                    LoadDataGridViewTab2();
                    LoadComboBoxDichVu();
                }
                catch (Exception ex) { MessageBox.Show("Lỗi xóa: " + ex.Message); }
                finally { Conn.Close(); }
            }
        }

        private void btnLuuDV_Click(object sender, EventArgs e)
        {
            if (txtMaDV.Text.Trim() == "") { MessageBox.Show("Vui lòng nhập Mã dịch vụ!"); return; }
            if (txtTenDV.Text.Trim() == "") { MessageBox.Show("Nhập tên dịch vụ!"); return; }

            Conn = new SqlConnection(connectionString);
            string sql = "";
            string maDV = txtMaDV.Text.Trim().Replace("'", "''");
            string tenDV = txtTenDV.Text.Trim().Replace("'", "''");
            string moTa = txtMoTa.Text.Trim().Replace("'", "''");

            if (mode == "add")
                sql = $"INSERT INTO DichVu (MaDV, TenDV, Gia, MoTa) VALUES (N'{maDV}', N'{tenDV}', {numGia.Value}, N'{moTa}')";
            else if (mode == "edit")
                sql = $"UPDATE DichVu SET TenDV = N'{tenDV}', Gia = {numGia.Value}, MoTa = N'{moTa}' WHERE MaDV = N'{maDV}'";

            try
            {
                Conn.Open();
                SqlCommand cmd = new SqlCommand(sql, Conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Lưu thành công!");
                mode = "view";
                SetControlsStatus(true);
                ClearControls();
                LoadDataGridViewTab2();
                LoadComboBoxDichVu();
            }
            catch (Exception ex) { MessageBox.Show("Lỗi lưu: " + ex.Message); }
            finally { Conn.Close(); }
        }

        private void btnHuyDV_Click(object sender, EventArgs e)
        {
            mode = "view";
            SetControlsStatus(true);
            ClearControls();
            LoadDataGridViewTab2();
        }

        private void btnTimKiemDV_Click(object sender, EventArgs e)
        {
            string filter = txtTimKiemDV.Text.Trim().Replace("'", "''");
            if (dt_DichVu == null) return;
            dt_DichVu.DefaultView.RowFilter = string.IsNullOrEmpty(filter) ? "" : $"TenDV LIKE '%{filter}%'";
        }

        private void ClearControls() { txtMaDV.Text = ""; txtTenDV.Text = ""; numGia.Value = 0; txtMoTa.Text = ""; }

        private void SetControlsStatus(bool view)
        {
            bool maDvEditable = mode == "add" && !view;
            txtMaDV.ReadOnly = !maDvEditable;
            txtMaDV.Enabled = true;
            txtTenDV.Enabled = !view; numGia.Enabled = !view; txtMoTa.Enabled = !view;
            btnThemDV.Enabled = view; btnSuaDV.Enabled = view; btnXoaDV.Enabled = view;
            btnLuuDV.Enabled = !view; btnHuyDV.Enabled = !view;
            dgvDanhSachDV.Enabled = view;
        }

        // ===================================================================
        // TAB 1: LẬP PHIẾU DỊCH VỤ
        // ===================================================================

        private void LoadDataGridViewTab1()
        {
            Conn = new SqlConnection(connectionString);
            string sql = @"SELECT p.MaPhieu, k_nd.HoTen AS TenKhachHang, d.TenDV AS TenDichVu, 
                                 p.NgayDat, p.NgayThucHien, p.GiaDichVu, p.TrangThai, p.GhiChu 
                           FROM PhieuDichVu p 
                           JOIN KhachHang k ON p.MaKH = k.MaKH 
                           JOIN NguoiDung k_nd ON k.MaNguoiDung = k_nd.MaNguoiDung 
                           JOIN DichVu d ON p.MaDV = d.MaDV";
            DataTable dtPhieu = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(sql, Conn);
            try
            {
                Conn.Open();
                da.Fill(dtPhieu);
                dgvDanhSachPhieu.DataSource = dtPhieu;
                dgvDanhSachPhieu.Columns["MaPhieu"].HeaderText = "Mã Phiếu";
                dgvDanhSachPhieu.Columns["TenKhachHang"].HeaderText = "Khách Hàng";
                dgvDanhSachPhieu.Columns["TenDichVu"].HeaderText = "Dịch Vụ";
                dgvDanhSachPhieu.Columns["NgayDat"].Visible = false;
                dgvDanhSachPhieu.Columns["NgayThucHien"].HeaderText = "Ngày Hẹn";
                dgvDanhSachPhieu.Columns["GiaDichVu"].HeaderText = "Giá";
                dgvDanhSachPhieu.Columns["TrangThai"].HeaderText = "Trạng Thái";
                dgvDanhSachPhieu.Columns["GhiChu"].Visible = false;
                dgvDanhSachPhieu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvDanhSachPhieu.Columns["GiaDichVu"].DefaultCellStyle.Format = "N0";
                dgvDanhSachPhieu.Columns["NgayThucHien"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }
            catch (Exception ex) { MessageBox.Show("Lỗi tải Tab 1: " + ex.Message); }
            finally { if (Conn.State == ConnectionState.Open) Conn.Close(); }
        }

        private void LoadComboBoxKhachHang()
        {
            Conn = new SqlConnection(connectionString);
            string sql = "SELECT a.MaKH, b.HoTen FROM KhachHang a JOIN NguoiDung b ON a.MaNguoiDung = b.MaNguoiDung";
            SqlDataAdapter da = new SqlDataAdapter(sql, Conn);
            DataTable dt = new DataTable();
            try
            {
                Conn.Open();
                da.Fill(dt);
                Conn.Close();
                cboKhachHang.DataSource = null;
                cboKhachHang.Items.Clear();
                cboKhachHang.DataSource = dt;
                cboKhachHang.DisplayMember = "HoTen";
                cboKhachHang.ValueMember = "MaKH";
                cboKhachHang.DropDownStyle = ComboBoxStyle.DropDownList;
                cboKhachHang.SelectedIndex = -1;
            }
            catch (Exception ex) { MessageBox.Show("Lỗi load khách hàng: " + ex.Message); }
            finally { if (Conn != null && Conn.State == ConnectionState.Open) Conn.Close(); }
        }

        // *** FIX CHÍNH: LoadComboBoxDichVu ***
        private void LoadComboBoxDichVu()
        {
            try
            {
                // Hủy đăng ký sự kiện tạm thời
                cboDichVu.SelectedIndexChanged -= cboDichVu_SelectedIndexChanged;

                if (dt_DichVu != null && dt_DichVu.Rows.Count > 0)
                {
                    cboDichVu.DataSource = null;
                    cboDichVu.Items.Clear();
                    cboDichVu.DataSource = dt_DichVu.Copy();
                    cboDichVu.DisplayMember = "TenDV";
                    cboDichVu.ValueMember = "MaDV";
                    cboDichVu.DropDownStyle = ComboBoxStyle.DropDownList;
                    cboDichVu.SelectedIndex = -1;
                }

                // Đăng ký lại sự kiện
                cboDichVu.SelectedIndexChanged += cboDichVu_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load combo dịch vụ: " + ex.Message);
            }
        }

        // *** FIX CHÍNH: Sự kiện chọn dịch vụ ***
        private void cboDichVu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboDichVu.SelectedIndex < 0)
            {
                txtGiaDichVu.Text = "";
                return;
            }

            try
            {
                // Cách 1: Lấy từ DataRowView
                DataRowView drv = cboDichVu.SelectedItem as DataRowView;
                if (drv != null && drv.Row["Gia"] != DBNull.Value)
                {
                    decimal gia = Convert.ToDecimal(drv.Row["Gia"]);
                    // FIX: Hiển thị số nguyên, không có phần thập phân
                    txtGiaDichVu.Text = gia.ToString("0");
                    return;
                }

                // Cách 2: Lấy từ dt_DichVu
                if (cboDichVu.SelectedValue != null && dt_DichVu != null)
                {
                    string maDV = cboDichVu.SelectedValue.ToString();
                    DataRow[] rows = dt_DichVu.Select($"MaDV = '{maDV.Replace("'", "''")}'");
                    if (rows.Length > 0 && rows[0]["Gia"] != DBNull.Value)
                    {
                        decimal gia = Convert.ToDecimal(rows[0]["Gia"]);
                        // FIX: Hiển thị số nguyên, không có phần thập phân
                        txtGiaDichVu.Text = gia.ToString("0");
                        return;
                    }
                }

                txtGiaDichVu.Text = "0";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi lấy giá: {ex.Message}");
                txtGiaDichVu.Text = "0";
            }
        }

        private void dgvDanhSachPhieu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvDanhSachPhieu.Rows.Count - 1)
            {
                DataGridViewRow row = dgvDanhSachPhieu.Rows[e.RowIndex];
                txtMaPhieu.Text = row.Cells["MaPhieu"].Value.ToString();
                cboKhachHang.Text = row.Cells["TenKhachHang"].Value.ToString();
                cboDichVu.Text = row.Cells["TenDichVu"].Value.ToString();
                txtGiaDichVu.Text = row.Cells["GiaDichVu"].Value.ToString();
                cboTrangThai.Text = row.Cells["TrangThai"].Value.ToString();

                if (row.Cells["NgayDat"].Value != DBNull.Value && DateTime.TryParse(row.Cells["NgayDat"].Value.ToString(), out DateTime dateDat))
                    dtpNgayDat.Value = dateDat;
                else
                    dtpNgayDat.Value = DateTime.Now;

                if (row.Cells["NgayThucHien"].Value != DBNull.Value && DateTime.TryParse(row.Cells["NgayThucHien"].Value.ToString(), out DateTime dateHen))
                    dtpNgayHen.Value = dateHen;
                else
                    dtpNgayHen.Value = DateTime.Now;

                if (row.Cells["GhiChu"].Value != null)
                    txtGhiChu.Text = row.Cells["GhiChu"].Value.ToString();
            }
        }

        private void btnThemPhieu_Click(object sender, EventArgs e)
        {
            modePhieu = "add";
            ClearControlsPhieu();
            SetControlsStatusPhieu(false);
            txtMaPhieu.Focus();
            dtpNgayDat.Value = DateTime.Now;
            dtpNgayHen.Value = DateTime.Now;
            cboTrangThai.Text = "Chờ xử lý";
        }

        private void btnSuaPhieu_Click(object sender, EventArgs e)
        {
            if (txtMaPhieu.Text.Trim() == "") { MessageBox.Show("Chọn phiếu cần sửa!"); return; }
            modePhieu = "edit";
            SetControlsStatusPhieu(false);
        }

        private void btnLuuPhieu_Click(object sender, EventArgs e)
        {
            if (txtMaPhieu.Text.Trim() == "" && modePhieu == "add")
            {
                MessageBox.Show("Vui lòng nhập Mã Phiếu!");
                return;
            }
            if (cboKhachHang.SelectedIndex == -1 || cboDichVu.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn Khách hàng và Dịch vụ!");
                return;
            }

            Conn = new SqlConnection(connectionString);
            try
            {
                Conn.Open();
                string maKH = cboKhachHang.SelectedValue.ToString();
                string maDV = cboDichVu.SelectedValue.ToString();
                string maPhieu = txtMaPhieu.Text.Trim().Replace("'", "''");
                string gia = string.IsNullOrEmpty(txtGiaDichVu.Text) ? "0" : txtGiaDichVu.Text.Replace(",", "");
                string ngayDat = dtpNgayDat.Value.ToString("yyyy-MM-dd HH:mm:ss");
                string ngayHen = dtpNgayHen.Value.ToString("yyyy-MM-dd HH:mm:ss");
                string ghiChu = txtGhiChu.Text.Replace("'", "''");
                string trangThai = cboTrangThai.Text;

                string sql = "";
                if (modePhieu == "add")
                    sql = $"INSERT INTO PhieuDichVu (MaPhieu, MaKH, MaDV, NgayDat, NgayThucHien, GiaDichVu, TrangThai, GhiChu) VALUES (N'{maPhieu}', N'{maKH}', N'{maDV}', '{ngayDat}', '{ngayHen}', {gia}, N'{trangThai}', N'{ghiChu}')";
                else if (modePhieu == "edit")
                    sql = $"UPDATE PhieuDichVu SET MaKH=N'{maKH}', MaDV=N'{maDV}', NgayDat='{ngayDat}', NgayThucHien='{ngayHen}', GiaDichVu={gia}, TrangThai=N'{trangThai}', GhiChu=N'{ghiChu}' WHERE MaPhieu=N'{maPhieu}'";

                SqlCommand cmd = new SqlCommand(sql, Conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Lưu phiếu thành công!");

                modePhieu = "view";
                SetControlsStatusPhieu(true);
                LoadDataGridViewTab1();
            }
            catch (Exception ex) { MessageBox.Show("Lỗi lưu phiếu: " + ex.Message); }
            finally { if (Conn.State == ConnectionState.Open) Conn.Close(); }
        }

        private void btnXoaPhieu_Click(object sender, EventArgs e)
        {
            if (txtMaPhieu.Text.Trim() == "") return;
            if (MessageBox.Show("Xóa phiếu này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Conn = new SqlConnection(connectionString);
                try
                {
                    Conn.Open();
                    SqlCommand cmd = new SqlCommand($"DELETE FROM PhieuDichVu WHERE MaPhieu=N'{txtMaPhieu.Text}'", Conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Đã xóa phiếu!");
                    ClearControlsPhieu();
                    LoadDataGridViewTab1();
                }
                catch (Exception ex) { MessageBox.Show("Lỗi xóa: " + ex.Message); }
                finally { Conn.Close(); }
            }
        }

        private void btnHuyPhieu_Click(object sender, EventArgs e)
        {
            modePhieu = "view";
            SetControlsStatusPhieu(true);
            ClearControlsPhieu();
        }

        private void btnLamMoiPhieu_Click(object sender, EventArgs e)
        {
            ClearControlsPhieu();
            LoadDataGridViewTab1();
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (txtMaPhieu.Text.Trim() == "") { MessageBox.Show("Chọn phiếu để thanh toán!"); return; }
            if (cboTrangThai.Text != "Hoàn thành" && MessageBox.Show("Phiếu chưa hoàn thành. Thanh toán luôn?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.No) return;

            Conn = new SqlConnection(connectionString);
            try
            {
                Conn.Open();
                string gia = txtGiaDichVu.Text.Replace(",", "");
                string maPhieu = txtMaPhieu.Text.Trim().Replace("'", "''");
                string maHDMoi = "HD" + DateTime.Now.ToString("yyyyMMddHHmmssfff");

                string sqlHD = $"INSERT INTO HoaDon (MaHD, MaPhieu, LoaiHoaDon, TongTien, TrangThai, GhiChu) VALUES (N'{maHDMoi}', N'{maPhieu}', N'Dịch vụ', {gia}, N'Đã thanh toán', N'Thanh toán phiếu DV {maPhieu}')";
                SqlCommand cmd = new SqlCommand(sqlHD, Conn);
                cmd.ExecuteNonQuery();

                string sqlUpdate = $"UPDATE PhieuDichVu SET TrangThai = N'Đã thanh toán' WHERE MaPhieu = N'{maPhieu}'";
                SqlCommand cmdUp = new SqlCommand(sqlUpdate, Conn);
                cmdUp.ExecuteNonQuery();

                MessageBox.Show("Thanh toán thành công! Hóa đơn: " + maHDMoi);
                LoadDataGridViewTab1();
            }
            catch (Exception ex) { MessageBox.Show("Lỗi thanh toán: " + ex.Message); }
            finally { Conn.Close(); }
        }

        private void btnTimKiemPhieu_Click(object sender, EventArgs e)
        {
            string key = txtTimKiem.Text.Trim().Replace("'", "''");
            DataTable dt = dgvDanhSachPhieu.DataSource as DataTable;
            if (dt != null)
            {
                dt.DefaultView.RowFilter = string.IsNullOrEmpty(key) ? "" :
                    $"TenKhachHang LIKE '%{key}%' OR TenDichVu LIKE '%{key}%' OR MaPhieu LIKE '%{key}%'";
            }
        }

        private void ClearControlsPhieu()
        {
            txtMaPhieu.Text = ""; cboKhachHang.SelectedIndex = -1; cboDichVu.SelectedIndex = -1;
            txtGiaDichVu.Text = ""; cboTrangThai.SelectedIndex = -1; txtGhiChu.Text = "";
            dtpNgayDat.Value = DateTime.Now; dtpNgayHen.Value = DateTime.Now;
        }

        private void SetControlsStatusPhieu(bool view)
        {
            bool maPhieuEditable = modePhieu == "add" && !view;
            txtMaPhieu.ReadOnly = !maPhieuEditable;
            txtMaPhieu.Enabled = true;
            cboKhachHang.Enabled = !view; cboDichVu.Enabled = !view; dtpNgayHen.Enabled = !view;
            cboTrangThai.Enabled = !view; txtGhiChu.Enabled = !view; txtGiaDichVu.Enabled = false;
            btnThemPhieu.Enabled = view; btnSuaPhieu.Enabled = view; btnXoaPhieu.Enabled = view;
            btnLuuPhieu.Enabled = !view; btnHuyPhieu.Enabled = !view; btnThanhToan.Enabled = view;
            dgvDanhSachPhieu.Enabled = view;
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadComboBoxKhachHang();
            LoadDataGridViewTab1();
            ClearControlsPhieu();
            MessageBox.Show("Đã cập nhật dữ liệu mới nhất!", "Thông báo");
        }

        private void gbDanhSachPhieu_Enter(object sender, EventArgs e) { }
        private void tabPage1_Click(object sender, EventArgs e) { }
    }
}