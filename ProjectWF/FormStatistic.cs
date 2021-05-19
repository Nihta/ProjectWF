using System;
using System.Windows.Forms;

namespace ProjectWF
{
    public partial class FormStatistic : Form
    {
        public FormStatistic()
        {
            InitializeComponent();

            ConfigDataGridView();
        }

        #region Methords
        private void ConfigDataGridView()
        {
            dgvOrder.AutoGenerateColumns = false;
            dgvOrder.Columns.Add(MyUtils.CreateCol(40, "OrderID", "ID"));
            dgvOrder.Columns.Add(MyUtils.CreateCol(200, "Customer", "Khách hàng"));
            dgvOrder.Columns.Add(MyUtils.CreateCol(100, "Phone", "Số điện thoại"));
            dgvOrder.Columns.Add(MyUtils.CreateCol(80, "OrderDate", "Ngày mua"));
            dgvOrder.Columns.Add(MyUtils.CreateCol(100, "TotalAmount", "Tổng tiền"));
        }

        private void RenderDataGridView()
        {
            OrderLinq.DataGridViewHelper(dgvOrder);
        }

        #endregion
        #region Events
        private void FormStatistic_Load(object sender, EventArgs e)
        {
            RenderDataGridView();

            dateTimePickerStart.Value = DateTime.Now;
        }

        #endregion

        private void dgvOrder_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            OrderLinq.DataGridViewHelper2(dgvOrder, dateTimePickerStart.Text, dateTimePickerEnd.Text);
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            OrderLinq.DataGridViewHelper2(dgvOrder, dateTimePickerStart.Text, dateTimePickerEnd.Text, true);
        }

        private void btnDay_Click(object sender, EventArgs e)
        {
            dateTimePickerStart.Value = dateTimePickerEnd.Value = DateTime.Now;
            OrderLinq.DataGridViewHelper2(dgvOrder, dateTimePickerStart.Text, dateTimePickerEnd.Text);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dgvOrder.Rows[dgvOrder.CurrentRow.Index].Cells["OrderID"].Value.ToString());
            FormOrderDetail f = new FormOrderDetail(id);
            this.Hide();
            f.ShowDialog();
            this.Show();
        }
    }
}
