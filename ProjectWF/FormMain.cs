using ProjectWF.Helpers;
using System;
using System.Data;
using System.Windows.Forms;

namespace ProjectWF
{
    public partial class FormMain : Form
    {
        private int curUserId;
        DataTable dataTableOrderDetail;
        private int totalAmount = 0;

        public FormMain()
        {
            InitializeComponent();
        }

        public FormMain(int userId)
        {
            InitializeComponent();
            this.curUserId = userId;

            dataTableOrderDetail = new DataTable();

            ConfigDataGridViewOrderDetail();
            ConfigDataTableOrderDetail();

            dgvOrderDetail.DataSource = dataTableOrderDetail;
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
        private void btnAdd_Click(object sender, EventArgs e)
        {
            int customerId = Convert.ToInt32(cbCustomers.SelectedValue.ToString());
            string date = dateTimePickerOrder.Text;

            if (dataTableOrderDetail.Rows.Count == 0)
            {
                MyMessageBox.Warning("Chưa có dữ liệu hàng hoá khách mua!");
            }
            else
            {
                OrderHelpers.Add(date, totalAmount, customerId);
                // Lấy id của order vừa tạo
                int OrderID = OrderHelpers.GetLastId();

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
        }
        #endregion

        #region OrderDetailHandles
        private void OrderDetailAdd(int productId, string productName, int quantity, string note)
        {
            int price = ProductHelpers.GetPrice(productId) * quantity;

            DataRow row = dataTableOrderDetail.NewRow();

            row["ProductName"] = productName;
            row["Quantity"] = quantity;
            row["Note"] = note;
            row["Price"] = price;
            row["ProductID"] = productId;


            totalAmount += price;
            txtTotalOrder.Text = totalAmount.ToString();

            dataTableOrderDetail.Rows.Add(row);
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
            MyUtils.FillComboBoxWithDataCustomer(cbCustomers);
            MyUtils.FillComboBoxWithDataProduct(cbProduct);
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
        #endregion

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


    }
}
