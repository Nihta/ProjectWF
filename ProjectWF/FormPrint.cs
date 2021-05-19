using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectWF
{
    public partial class FormPrint : Form
    {
        public FormPrint(DataTable dataTable, string date, int totalAmount, string customerId)
        {
            InitializeComponent();

            ConfigDataGridViewOrderDetail();

            dataGridView1.DataSource = dataTable;
            labelDate.Text = date;
            labelTotalAmount.Text = totalAmount.ToString();

            TableCustomer tableCustomer = CustomerLinq.GetCustomerById(Convert.ToInt32(customerId));

            labelFullName.Text = $"{tableCustomer.FirstName} {tableCustomer.LastName}";
            labelPhone.Text = tableCustomer.Phone;
        }

        private void FormPrint_Load(object sender, EventArgs e)
        {

        }

        private void ConfigDataGridViewOrderDetail()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Add(MyUtils.CreateCol(100, "ProductName", "Tên sản phẩm"));
            dataGridView1.Columns.Add(MyUtils.CreateCol(80, "Quantity", "Số lượng"));
            dataGridView1.Columns.Add(MyUtils.CreateCol(100, "Price", "Tổng tiền"));
            dataGridView1.Columns.Add(MyUtils.CreateCol(180, "Note", "Ghi chú"));
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
