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
    public partial class FormOrderDetail : Form
    {
        public FormOrderDetail(int OrderID)
        {
            InitializeComponent();

            getData(OrderID);
        }

        private void getData(int OrderID)
        {
            using (DataClassesDataContext db = new DataClassesDataContext())
            {
                var query = from order in db.TableOrderDetails
                            join pro in db.TableProducts on order.ProductID equals pro.ProductID
                            where order.OrderID == OrderID
                            select new
                            {
                                pro.ProductName,
                                order.Quantity,
                                order.Note
                            };

                dgvOrderDetail.DataSource = query;
            }
        }

        private void FormOrderDetail_Load(object sender, EventArgs e)
        {

        }
    }
}
