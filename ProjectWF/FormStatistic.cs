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
    }
}
