using System;
using System.Windows.Forms;

namespace ProjectWF
{
    public partial class FormTest : Form
    {
        public FormTest()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FormTest_Load(object sender, EventArgs e)
        {
            var res = MyValidation.IsNumeric("123");
            label1.Text = res.ToString();
        }
    }
}
