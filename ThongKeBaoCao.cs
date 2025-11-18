using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using OfficeOpenXml;
using System.IO;

namespace GiaoDienDangNhap
{
    public partial class ThongKeBaoCao : Form
    {
        // ===================================================================
        // CONNECTION STRING - THAY ĐỔI THEO CẤU HÌNH CỦA BẠN
        // ===================================================================
        private string connectionString = @"Data Source=HUYNE;Initial Catalog=QUANLY_PETSHOP_V9;Integrated Security=True;TrustServerCertificate=True";

        public ThongKeBaoCao()
        {
            InitializeComponent();
        }

        // ===================================================================
        // FORM LOAD - KHỞI TẠO
        // ===================================================================
        private void ThongKeBaoCao_Load(object sender, EventArgs e)
        {
            try
            {
                // Set giá trị mặc định cho DateTimePicker
                dtp_TuNgay.Value = DateTime.Now.AddMonths(-1);
                dtp_DenNgay.Value = DateTime.Now;

                // Format DateTimePicker
                dtp_TuNgay.Format = DateTimePickerFormat.Custom;
                dtp_TuNgay.CustomFormat = "dd/MM/yyyy";
                dtp_DenNgay.Format = DateTimePickerFormat.Custom;
                dtp_DenNgay.CustomFormat = "dd/MM/yyyy";

                // Cấu hình DataGridView
                ConfigureDataGridView();

                // Cấu hình TextBox (chỉ đọc, không cho nhập)
                txt_tongdoanhthu.ReadOnly = true;
                txt_doanhthuthucung.ReadOnly = true;
                txt_doanhthudichvu.ReadOnly = true;
                txt_doanhthuphukien.ReadOnly = true;

                // Set giá trị ban đầu
                txt_tongdoanhthu.Text = "0 đ";
                txt_doanhthuthucung.Text = "0 đ";
                txt_doanhthudichvu.Text = "0 đ";
                txt_doanhthuphukien.Text = "0 đ";

                // Set màu sắc cho TextBox
                txt_tongdoanhthu.ForeColor = Color.Green;
                txt_doanhthuthucung.ForeColor = Color.Navy;
                txt_doanhthudichvu.ForeColor = Color.OrangeRed;
                txt_doanhthuphukien.ForeColor = Color.Purple;

                // Set font cho TextBox (Times New Roman)
                Font boldFont = new Font("Times New Roman", 14, FontStyle.Bold);
                txt_tongdoanhthu.Font = boldFont;
                txt_doanhthuthucung.Font = boldFont;
                txt_doanhthudichvu.Font = boldFont;
                txt_doanhthuphukien.Font = boldFont;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi tạo form: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===================================================================
        // CẤU HÌNH DATAGRIDVIEW
        // ===================================================================
        private void ConfigureDataGridView()
        {
            // Cấu hình chung
            dgv_BaoCao.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv_BaoCao.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv_BaoCao.MultiSelect = false;
            dgv_BaoCao.ReadOnly = true;
            dgv_BaoCao.AllowUserToAddRows = false;
            dgv_BaoCao.AllowUserToDeleteRows = false;
            dgv_BaoCao.RowHeadersVisible = false;

            // Màu xen kẽ
            dgv_BaoCao.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            dgv_BaoCao.DefaultCellStyle.SelectionBackColor = Color.DarkBlue;
            dgv_BaoCao.DefaultCellStyle.SelectionForeColor = Color.White;

            // Định dạng header
            dgv_BaoCao.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            dgv_BaoCao.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv_BaoCao.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            dgv_BaoCao.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv_BaoCao.EnableHeadersVisualStyles = false;

            // Chiều cao dòng
            dgv_BaoCao.RowTemplate.Height = 30;
        }

        // ===================================================================
        // NÚT XEM BÁO CÁO (LẤY TỪ BẢNG BaoCaoThongKe)
        // ===================================================================
        private void btn_modulieu_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime tuNgay = dtp_TuNgay.Value.Date;
                DateTime denNgay = dtp_DenNgay.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59);

                // Kiểm tra ngày hợp lệ
                if (tuNgay > denNgay)
                {
                    MessageBox.Show("Từ ngày phải nhỏ hơn hoặc bằng đến ngày!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Load dữ liệu từ bảng BaoCaoThongKe
                LoadBaoCaoThongKe(tuNgay, denNgay);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xem báo cáo: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===================================================================
        // LOAD DỮ LIỆU TỪ BẢNG BaoCaoThongKe
        // ===================================================================
        private void LoadBaoCaoThongKe(DateTime tuNgay, DateTime denNgay)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Query lấy dữ liệu từ BaoCaoThongKe
                    string query = @"
                        SELECT 
                            MaBaoCao AS 'Mã báo cáo',
                            TenBaoCao AS 'Tên báo cáo',
                            NgayLap AS 'Ngày lập',
                            TuNgay AS 'Từ ngày',
                            DenNgay AS 'Đến ngày',
                            TongDoanhThuThuCung AS 'DT Thú cưng',
                            TongDoanhThuPhuKien AS 'DT Phụ kiện',
                            TongDoanhThuDichVu AS 'DT Dịch vụ',
                            TongDoanhThu AS 'Tổng doanh thu',
                            SoDonHang AS 'Số đơn hàng',
                            SoThuCungBan AS 'Số thú cưng bán',
                            SoPhuKienBan AS 'Số phụ kiện bán',
                            SoDichVu AS 'Số dịch vụ',
                            GhiChu AS 'Ghi chú'
                        FROM BaoCaoThongKe
                        WHERE NgayLap BETWEEN @TuNgay AND @DenNgay
                        ORDER BY NgayLap DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TuNgay", tuNgay);
                        cmd.Parameters.AddWithValue("@DenNgay", denNgay);

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        // Hiển thị lên DataGridView
                        dgv_BaoCao.DataSource = dt;

                        // Format các cột tiền
                        FormatMoneyColumns();

                        // Format cột ngày
                        FormatDateColumns();

                        // Nếu có dữ liệu, lấy bản ghi đầu tiên để hiển thị tổng quan
                        if (dt.Rows.Count > 0)
                        {
                            DataRow row = dt.Rows[0];

                            decimal tongDT = row["Tổng doanh thu"] != DBNull.Value ? Convert.ToDecimal(row["Tổng doanh thu"]) : 0;
                            decimal dtThuCung = row["DT Thú cưng"] != DBNull.Value ? Convert.ToDecimal(row["DT Thú cưng"]) : 0;
                            decimal dtDichVu = row["DT Dịch vụ"] != DBNull.Value ? Convert.ToDecimal(row["DT Dịch vụ"]) : 0;
                            decimal dtPhuKien = row["DT Phụ kiện"] != DBNull.Value ? Convert.ToDecimal(row["DT Phụ kiện"]) : 0;

                            txt_tongdoanhthu.Text = tongDT.ToString("N0") + " đ";
                            txt_doanhthuthucung.Text = dtThuCung.ToString("N0") + " đ";
                            txt_doanhthudichvu.Text = dtDichVu.ToString("N0") + " đ";
                            txt_doanhthuphukien.Text = dtPhuKien.ToString("N0") + " đ";

                            MessageBox.Show($"Đã tải {dt.Rows.Count} báo cáo!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Không có báo cáo nào trong khoảng thời gian này!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Reset về 0
                            txt_tongdoanhthu.Text = "0 đ";
                            txt_doanhthuthucung.Text = "0 đ";
                            txt_doanhthudichvu.Text = "0 đ";
                            txt_doanhthuphukien.Text = "0 đ";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load báo cáo: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===================================================================
        // FORMAT CÁC CỘT TIỀN TRONG DATAGRIDVIEW
        // ===================================================================
        private void FormatMoneyColumns()
        {
            string[] moneyColumns = { "DT Thú cưng", "DT Phụ kiện", "DT Dịch vụ", "Tổng doanh thu" };

            foreach (string colName in moneyColumns)
            {
                if (dgv_BaoCao.Columns[colName] != null)
                {
                    dgv_BaoCao.Columns[colName].DefaultCellStyle.Format = "N0";
                    dgv_BaoCao.Columns[colName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }
        }

        // ===================================================================
        // FORMAT CÁC CỘT NGÀY TRONG DATAGRIDVIEW
        // ===================================================================
        private void FormatDateColumns()
        {
            string[] dateColumns = { "Ngày lập", "Từ ngày", "Đến ngày" };

            foreach (string colName in dateColumns)
            {
                if (dgv_BaoCao.Columns[colName] != null)
                {
                    dgv_BaoCao.Columns[colName].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                }
            }
        }

        // ===================================================================
        // NÚT XUẤT EXCEL
        // ===================================================================
        private void btn_XuatExcel_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra có dữ liệu không
                if (dgv_BaoCao.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất!\nVui lòng click [Xem báo cáo] trước.", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Hiển thị Save File Dialog
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel Files|*.xlsx";
                saveFileDialog.Title = "Lưu báo cáo Excel";
                saveFileDialog.FileName = "BaoCaoThongKe_" + DateTime.Now.ToString("ddMMyyyy_HHmmss");

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Tạo Excel package bằng MemoryStream
                    using (ExcelPackage excel = new ExcelPackage())
                    {
                        var worksheet = excel.Workbook.Worksheets.Add("Báo cáo thống kê");

                        // ===== PHẦN 1: TIÊU ĐỀ =====
                        worksheet.Cells["A1"].Value = "BÁO CÁO THỐNG KÊ DOANH THU PET SHOP";
                        worksheet.Cells["A1:H1"].Merge = true;
                        worksheet.Cells["A1"].Style.Font.Size = 18;
                        worksheet.Cells["A1"].Style.Font.Bold = true;
                        worksheet.Cells["A1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        worksheet.Cells["A1"].Style.Font.Color.SetColor(Color.DarkBlue);

                        // ===== PHẦN 2: THỜI GIAN BÁO CÁO =====
                        worksheet.Cells["A2"].Value = "Từ ngày: " + dtp_TuNgay.Value.ToString("dd/MM/yyyy") +
                                                      " - Đến ngày: " + dtp_DenNgay.Value.ToString("dd/MM/yyyy");
                        worksheet.Cells["A2:H2"].Merge = true;
                        worksheet.Cells["A2"].Style.Font.Size = 12;
                        worksheet.Cells["A2"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                        worksheet.Cells["A3"].Value = "Ngày xuất: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                        worksheet.Cells["A3:H3"].Merge = true;
                        worksheet.Cells["A3"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                        // ===== PHẦN 3: THỐNG KÊ TỔNG QUAN =====
                        int row = 5;
                        worksheet.Cells[row, 1].Value = "THỐNG KÊ TỔNG QUAN";
                        worksheet.Cells[row, 1, row, 2].Merge = true;
                        worksheet.Cells[row, 1].Style.Font.Bold = true;
                        worksheet.Cells[row, 1].Style.Font.Size = 14;
                        worksheet.Cells[row, 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        worksheet.Cells[row, 1].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);

                        row++;
                        worksheet.Cells[row, 1].Value = "Tổng doanh thu:";
                        worksheet.Cells[row, 2].Value = txt_tongdoanhthu.Text;
                        worksheet.Cells[row, 2].Style.Font.Bold = true;
                        worksheet.Cells[row, 2].Style.Font.Color.SetColor(Color.Green);

                        row++;
                        worksheet.Cells[row, 1].Value = "Doanh thu thú cưng:";
                        worksheet.Cells[row, 2].Value = txt_doanhthuthucung.Text;

                        row++;
                        worksheet.Cells[row, 1].Value = "Doanh thu dịch vụ:";
                        worksheet.Cells[row, 2].Value = txt_doanhthudichvu.Text;

                        row++;
                        worksheet.Cells[row, 1].Value = "Doanh thu phụ kiện:";
                        worksheet.Cells[row, 2].Value = txt_doanhthuphukien.Text;

                        // ===== PHẦN 4: CHI TIẾT BÁO CÁO =====
                        row += 2;
                        worksheet.Cells[row, 1].Value = "CHI TIẾT BÁO CÁO THỐNG KÊ";
                        worksheet.Cells[row, 1, row, dgv_BaoCao.Columns.Count].Merge = true;
                        worksheet.Cells[row, 1].Style.Font.Bold = true;
                        worksheet.Cells[row, 1].Style.Font.Size = 14;
                        worksheet.Cells[row, 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        worksheet.Cells[row, 1].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);

                        // Headers
                        row++;
                        for (int i = 0; i < dgv_BaoCao.Columns.Count; i++)
                        {
                            worksheet.Cells[row, i + 1].Value = dgv_BaoCao.Columns[i].HeaderText;
                            worksheet.Cells[row, i + 1].Style.Font.Bold = true;
                            worksheet.Cells[row, i + 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            worksheet.Cells[row, i + 1].Style.Fill.BackgroundColor.SetColor(Color.Navy);
                            worksheet.Cells[row, i + 1].Style.Font.Color.SetColor(Color.White);
                            worksheet.Cells[row, i + 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        }

                        // Data
                        row++;
                        for (int i = 0; i < dgv_BaoCao.Rows.Count; i++)
                        {
                            for (int j = 0; j < dgv_BaoCao.Columns.Count; j++)
                            {
                                var value = dgv_BaoCao.Rows[i].Cells[j].Value;
                                worksheet.Cells[row + i, j + 1].Value = value != null ? value.ToString() : "";
                            }
                        }

                        // ===== PHẦN 5: FORMAT =====
                        // Auto fit columns
                        worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                        // Thêm border cho bảng dữ liệu
                        int lastRow = row + dgv_BaoCao.Rows.Count - 1;
                        var dataRange = worksheet.Cells[row - 1, 1, lastRow, dgv_BaoCao.Columns.Count];
                        dataRange.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        dataRange.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        dataRange.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        dataRange.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                        // Lưu file bằng cách ghi byte array
                        byte[] fileBytes = excel.GetAsByteArray();
                        File.WriteAllBytes(saveFileDialog.FileName, fileBytes);

                        // Thông báo thành công
                        DialogResult result = MessageBox.Show(
                            "Xuất Excel thành công!\n\n" +
                            "File: " + saveFileDialog.FileName + "\n\n" +
                            "Bạn có muốn mở file ngay không?",
                            "Thành công",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Information
                        );

                        // Mở file nếu user chọn Yes
                        if (result == DialogResult.Yes)
                        {
                            try
                            {
                                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                                {
                                    FileName = saveFileDialog.FileName,
                                    UseShellExecute = true
                                });
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Không thể mở file: " + ex.Message);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất Excel: " + ex.Message + "\n\n" +
                    "Chi tiết: " + ex.StackTrace, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===================================================================
        // NÚT IN BÁO CÁO
        // ===================================================================
        private void btn_inbaocao_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Để in báo cáo:\n\n" +
                "1. Click [Xuất Excel]\n" +
                "2. Mở file Excel\n" +
                "3. Nhấn Ctrl + P để in\n\n" +
                "Hoặc: File → Print trong Excel",
                "Hướng dẫn in báo cáo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        // ===================================================================
        // CÁC EVENT HANDLER KHÁC
        // ===================================================================
        private void dtp_TuNgay_ValueChanged(object sender, EventArgs e)
        {
        }

        private void dtp_DenNgay_ValueChanged(object sender, EventArgs e)
        {
        }

        private void txt_tongdoanhthu_TextChanged(object sender, EventArgs e)
        {
        }

        private void txt_doanhthuthucung_TextChanged(object sender, EventArgs e)
        {
        }

        private void txt_doanhthudichvu_TextChanged(object sender, EventArgs e)
        {
        }

        private void txt_doanhthuphukien_TextChanged(object sender, EventArgs e)
        {
        }

        private void dgv_BaoCao_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void dgv_BaoCao_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                // Nếu có dòng được chọn
                if (dgv_BaoCao.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dgv_BaoCao.SelectedRows[0];

                    // Lấy giá trị từ các cột (dùng tên cột chính xác như trong query)
                    decimal tongDT = GetDecimalValue(selectedRow, "Tổng doanh thu");
                    decimal dtThuCung = GetDecimalValue(selectedRow, "DT Thú cưng");
                    decimal dtPhuKien = GetDecimalValue(selectedRow, "DT Phụ kiện");
                    decimal dtDichVu = GetDecimalValue(selectedRow, "DT Dịch vụ");

                    // Cập nhật lên các TextBox
                    txt_tongdoanhthu.Text = tongDT.ToString("N0") + " đ";
                    txt_doanhthuthucung.Text = dtThuCung.ToString("N0") + " đ";
                    txt_doanhthuphukien.Text = dtPhuKien.ToString("N0") + " đ";
                    txt_doanhthudichvu.Text = dtDichVu.ToString("N0") + " đ";

                    // Tùy chọn: đổi màu viền hoặc highlight để người dùng biết đang chọn dòng nào
                    txt_tongdoanhthu.ForeColor = Color.Green;
                    txt_doanhthuthucung.ForeColor = Color.Navy;
                    txt_doanhthudichvu.ForeColor = Color.OrangeRed;
                    txt_doanhthuphukien.ForeColor = Color.Purple;
                }
            }
            catch (Exception ex)
            {
                // Không hiện lỗi làm phiền người dùng khi chỉ click chọn dòng
                // MessageBox.Show("Lỗi chọn dòng: " + ex.Message);
            }
        }
        private decimal GetDecimalValue(DataGridViewRow row, string columnName)
        {
            if (row.Cells[columnName].Value == null || row.Cells[columnName].Value == DBNull.Value)
                return 0;
            decimal result;
            if (decimal.TryParse(row.Cells[columnName].Value.ToString(), out result))
                return result;
            return 0;
        }
    }
}