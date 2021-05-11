using System;
using System.Data;
using System.Windows.Forms;

namespace ProjectWF
{
    public partial class FormProducts : Form
    {
        private ControlHelper control;
        DataTable dataTable;
        SqlHelper sqlHelper;

        public FormProducts()
        {
            InitializeComponent();

            ConfigDataGridView();
            sqlHelper = new SqlHelper();
            control = new ControlHelper();
        }

        #region Methods
        private void ConfigDataGridView()
        {
            dgvProduct.AutoGenerateColumns = false;
            dgvProduct.Columns.Add(MyUtils.CreateCol(30, "ProductID", "ID"));
            dgvProduct.Columns.Add(MyUtils.CreateCol(100, "ProductName", "Tên sản phẩm"));
            dgvProduct.Columns.Add(MyUtils.CreateCol(100, "Price", "Giá"));
            dgvProduct.Columns.Add(MyUtils.CreateCol(160, "Description", "Mô tả"));
            dgvProduct.Columns.Add(MyUtils.CreateCol(100, "CategoryID", "Danh mục"));
            dgvProduct.Columns.Add(MyUtils.CreateCol(160, "SupplierID", "Nhà cung cấp"));

            //dgvProduct.Columns["SupplierID"].Visible = false;
        }

        private void GetDataGridView()
        {
            string query = "select p.ProductID, p.ProductName, p.Price, p.Description, p.CategoryID, p.SupplierID from TableProducts p";
            dataTable = sqlHelper.ExecuteQuery(SqlHelper.defaultConnStr, query, CommandType.Text);
            dgvProduct.DataSource = dataTable;
        }

        private void HandleRowEnter(int idx)
        {
            txtName.Text = dgvProduct.Rows[idx].Cells["ProductName"].Value.ToString().Trim();
            txtPrice.Text = dgvProduct.Rows[idx].Cells["Price"].Value.ToString().Trim();
            txtDesc.Text = dgvProduct.Rows[idx].Cells["Description"].Value.ToString().Trim();
            cbCate.SelectedValue = dgvProduct.Rows[idx].Cells["CategoryID"].Value.ToString().Trim();
            cbSup.SelectedValue = dgvProduct.Rows[idx].Cells["SupplierID"].Value.ToString().Trim();
        }

        private void AddProduct()
        {
            DataRow newRow = dataTable.NewRow();

            newRow["ProductName"] = txtName.Text;
            newRow["Price"] = Convert.ToInt32(txtPrice.Text);
            newRow["Description"] = txtDesc.Text;
            newRow["CategoryID"] = Convert.ToInt32(cbCate.SelectedValue.ToString());
            newRow["SupplierID"] = Convert.ToInt32(cbSup.SelectedValue.ToString());

            dataTable.Rows.Add(newRow);
            sqlHelper.Update(dataTable);
            GetDataGridView();
        }

        private void EditProduct()
        {
            int curRowIdx = dgvProduct.CurrentRow.Index;
            DataRow editRow = dataTable.Rows[curRowIdx];

            editRow["ProductName"] = txtName.Text;
            editRow["Price"] = Convert.ToInt32(txtPrice.Text);
            editRow["Description"] = txtDesc.Text;
            editRow["CategoryID"] = Convert.ToInt32(cbCate.SelectedValue.ToString());
            editRow["SupplierID"] = Convert.ToInt32(cbSup.SelectedValue.ToString());

            sqlHelper.Update(dataTable);
        }
        
        public bool IsInvalid()
        {
            if (!MyValidation.CommonValidation(txtName.Text, 1, 50, "Tên sản phẩm"))
            {
                txtName.Focus();
                return false;
            }

            if (!MyValidation.IsNumeric(txtPrice.Text, "Giá"))
            {
                txtName.Focus();
                return false;
            }

            if (!MyValidation.CommonValidation(txtDesc.Text, 1, 50, "Mô tả"))
            {
                txtDesc.Focus();
                return false;
            }

            return true;
        }
        #endregion

        #region Events
        private void FormProducts_Load(object sender, EventArgs e)
        {
            // Control helper
            control.AddBtnControls(btnAdd, btnEdit, btnDelete, btnSave, btnCancel);
            control.AddTextBoxs(txtName, txtPrice, txtDesc);
            control.AddComboBoxs(cbCate, cbSup);

            // Control mode
            control.SwitchMode(ControlHelper.ControlMode.None);
            txtName.Focus();

            // DataGridView
            GetDataGridView();

            MyUtils.FillComboBoxWithDataCategory(cbCate);
            MyUtils.FillComboBoxWithDataSupplier(cbSup);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            control.HandledAddClick();
            txtName.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            control.HandledEditClick();
            txtName.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MyMessageBox.Question("Bạn có chắn xóa bản ghi đã chọn không?"))
            {
                int curRowIdx = dgvProduct.CurrentRow.Index;
                dataTable.Rows[curRowIdx].Delete();
                sqlHelper.Update(dataTable);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Console.WriteLine(cbCate.SelectedValue.ToString());

            if (IsInvalid())
            {
                // Cập nhật data
                switch (control.GetMode())
                {
                    case ControlHelper.ControlMode.Add:
                        {
                            AddProduct();
                        }
                        break;
                    case ControlHelper.ControlMode.Edit:
                        {
                            EditProduct();
                        }
                        break;
                }

                // Sau khi cập nhật dữ liệu thành công
                control.SwitchMode(ControlHelper.ControlMode.None);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            control.HandleCancelClick();

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvProduct_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int idx = e.RowIndex;
            HandleRowEnter(idx);
        }
        #endregion
    }
}
