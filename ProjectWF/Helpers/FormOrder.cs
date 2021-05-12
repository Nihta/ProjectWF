using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ProjectWF.Helpers
{
    public partial class FormOrder : Form
    {
        public FormOrder()
        {
            InitializeComponent();
        }

        private void GetCustommers()
        {
            string cmd = "select  * from TableCustomers order by LastName";
            SqlDataReader dataReader = SqlHelper.ExecuteReader(SqlHelper.defaultConnStr, cmd, CommandType.Text);

            DataTable dataTableCustomer = new DataTable();
            dataTableCustomer.Load(dataReader);

            dgvCustomer.DataSource = dataTableCustomer;
        }

        private void GetOrders(int customerID)
        {
            string cmd = $"select OrderID, OrderDate, TotalAmount from TableOrders where  CustemerID = {customerID} order by  OrderDate desc";
            SqlDataReader dataReader = SqlHelper.ExecuteReader(SqlHelper.defaultConnStr, cmd, CommandType.Text);

            DataTable datatable = new DataTable();
            datatable.Load(dataReader);

            dgvOrder.DataSource = datatable;
        }

        private void GetOrderDetail(int orderID)
        {
            string cmd = @"
                select  ProductName, Quantity, Note
                from TableOrderDetails
                join TableProducts TP on TP.ProductID = TableOrderDetails.ProductID
                where  OrderID = " + orderID + "";
            SqlDataReader dataReader = SqlHelper.ExecuteReader(SqlHelper.defaultConnStr, cmd, CommandType.Text);
            DataTable datatable = new DataTable();
            datatable.Load(dataReader);
            dgvOrderDetail.DataSource = datatable;
        }


        private void FormOrder_Load(object sender, EventArgs e)
        {
            //MyUtils.FillComboBoxWithDataCustomer(cbCustomer);
            GetCustommers();
        }

        private void dgvCustomer_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int idx = e.RowIndex;

            int customerId = Convert.ToInt32(dgvCustomer.Rows[idx].Cells["CustomerID"].Value.ToString());
            GetOrders(customerId);
        }

        private void dgvOrder_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int idx = e.RowIndex;
            int orderId = Convert.ToInt32(dgvOrder.Rows[idx].Cells["OrderID"].Value.ToString());
            GetOrderDetail(orderId);
        }
    }
}
