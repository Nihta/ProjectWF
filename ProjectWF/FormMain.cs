using ProjectWF.Helpers;
using System;
using System.Data;
using System.Windows.Forms;

namespace ProjectWF
{
    public partial class FormMain : Form
    {
        /// <summary>
        /// ID của người quản lý
        /// </summary>
        private int curUserId;
        /// <summary>
        /// Tổng tiền
        /// </summary>
        private int totalAmount = 0;
        DataTable dataTableOrderDetail;

        public FormMain(int userId)
        {
            this.curUserId = userId;
            dataTableOrderDetail = new DataTable();

            InitializeComponent();

            ConfigDataTableOrderDetail();
            ConfigDataGridViewOrderDetail();
        }

        private void ConfigDataGridViewOrderDetail()
        {
            dgvOrderDetail.AutoGenerateColumns = false;
            dgvOrderDetail.Columns.Add(MyUtils.CreateCol(100, "ProductName", "Tên sản phẩm"));
            dgvOrderDetail.Columns.Add(MyUtils.CreateCol(80, "Quantity", "Số lượng"));
            dgvOrderDetail.Columns.Add(MyUtils.CreateCol(100, "Price", "Tổng tiền"));
            dgvOrderDetail.Columns.Add(MyUtils.CreateCol(180, "Note", "Ghi chú"));

            // ! Phải chặn sort để có thể xoá chính xác row
            foreach (DataGridViewColumn column in dgvOrderDetail.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void ConfigDataTableOrderDetail()
        {
            DataTableHelpers.AddCol(dataTableOrderDetail, "ProductName", "System.String");
            DataTableHelpers.AddCol(dataTableOrderDetail, "Quantity", "System.Int32");
            DataTableHelpers.AddCol(dataTableOrderDetail, "Note", "System.String");
            DataTableHelpers.AddCol(dataTableOrderDetail, "Price", "System.Int32");
            DataTableHelpers.AddCol(dataTableOrderDetail, "ProductID", "System.Int32");
        }

        #region OrederHandles
        // Thêm hoá đơn
        private void btnAdd_Click(object sender, EventArgs e)
        {
            int custumerIDSelected = Convert.ToInt32(cbCustomer.SelectedValue.ToString());

            if (custumerIDSelected < 1)
            {
                MyMessageBox.Warning("Chưa có thông tin người mua hàng!");
                btnSearchCustomer.Focus();
                return;
            }

            if (dataTableOrderDetail.Rows.Count == 0)
            {
                MyMessageBox.Warning("Chưa có dữ liệu hàng hoá khách mua!");
                cbProduct.Focus();
                return;
            }

            string date = dateTimePickerOrder.Text;
            OrderHelpers.Add(date, totalAmount, custumerIDSelected);
            // Lấy id của order vừa tạo
            int OrderID = OrderHelpers.GetLastOrderID();

            foreach (DataRow dataRow in dataTableOrderDetail.Rows)
            {
                OrderDetailHelpers.Add(
                    Convert.ToInt32(dataRow["Quantity"].ToString()),
                    dataRow["Note"].ToString(),
                    Convert.ToInt32(dataRow["ProductID"].ToString()),
                    OrderID
                 );
            }

            MyMessageBox.Information("Thành công!");
            dataTableOrderDetail.Clear();

        }
        #endregion

        #region OrderDetailHandles
        // Thêm một hàng hoá vào hoá đơn
        private void OrderDetailAdd(int productId, string productName, int quantity, string note)
        {
            int price = ProductHelpers.GetPrice(productId) * quantity;

            DataRow newRow = dataTableOrderDetail.NewRow();

            newRow["ProductName"] = productName;
            newRow["Quantity"] = quantity;
            newRow["Note"] = note;
            newRow["Price"] = price;
            newRow["ProductID"] = productId;

            dataTableOrderDetail.Rows.Add(newRow);

            totalAmount += price;
            txtTotalOrder.Text = totalAmount.ToString();
        }

        private void btnAddOrderItem_Click(object sender, EventArgs e)
        {
            string productName = cbProduct.Text;
            int productId = Convert.ToInt32(cbProduct.SelectedValue.ToString());
            int quatity = Convert.ToInt32(numericUpDownQuantity.Value);
            string note = txtNote.Text;

            OrderDetailAdd(productId, productName, quatity, note);
            // Clear input data
            numericUpDownQuantity.Value = 1;
            txtNote.Clear();
        }

        private void btnDelOrderItem_Click(object sender, EventArgs e)
        {
            if (dgvOrderDetail.CurrentRow != null)
            {
                int curRowIdx = dgvOrderDetail.CurrentRow.Index;

                int price = Convert.ToInt32(dgvOrderDetail.Rows[curRowIdx].Cells["Price"].Value.ToString());
                this.totalAmount -= price;
                txtTotalOrder.Text = totalAmount.ToString();

                dataTableOrderDetail.Rows[curRowIdx].Delete();
            }
        }
        #endregion

        #region Events
        private void FormMain_Load(object sender, EventArgs e)
        {
            dgvOrderDetail.DataSource = dataTableOrderDetail;
            MyUtils.FillComboBoxWithDataProduct(cbProduct);
            MyUtils.FillComboBoxWithDataCustomer(cbCustomer);

            dateTimePickerOrder.Value = DateTime.Now;
        }

        // Chọn khách hàng
        private void btnSearchCustomer_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (FormCustomer f = new FormCustomer(FormCustomer.mode.select))
            {
                f.ShowDialog();
                cbCustomer.SelectedValue = f.ReturnCustumerID;
            }
            this.Show();
        }

        // Chọn mặt hàng
        private void btnSearchProduct_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (FormProducts f = new FormProducts(FormProducts.mode.select))
            {
                f.ShowDialog();
                cbProduct.SelectedValue = f.ReturnProductID;
            }
            this.Show();
        }

        // Show detail
        private void button1_Click(object sender, EventArgs e)
        {
            FormPrint f = new FormPrint(dataTableOrderDetail, dateTimePickerOrder.Text, totalAmount, cbCustomer.SelectedValue.ToString());
            this.Hide();
            f.ShowDialog();
            this.Show();
        }
        #endregion

        #region StripMenu
        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void thôngTinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAccountProfile f = new FormAccountProfile(curUserId);
            f.ShowDialog();
        }

        private void tàiKhoảnToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormUsers f = new FormUsers();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void nhàCungCấpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSuppliers f = new FormSuppliers();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void danhMụcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCategory f = new FormCategory();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void kháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCustomer f = new FormCustomer();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void sảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormProducts f = new FormProducts();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void lịchSửĐặtHàngToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormOrder f = new FormOrder();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void thốngKêToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormStatistic f = new FormStatistic();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }
        #endregion
    }
}
