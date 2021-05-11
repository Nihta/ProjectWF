using System;
using System.Data;
using System.Windows.Forms;
using ProjectWF.Helpers;

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
            string cmd = $"INSERT dbo.TableProducts (ProductName, Price, Description, CategoryID, SupplierID) VALUES(N'{txtName.Text}', {Convert.ToInt32(txtPrice.Text)}, N'{txtDesc.Text}', {Convert.ToInt32(cbCate.SelectedValue.ToString())}, {Convert.ToInt32(cbSup.SelectedValue.ToString())});";
            int numOfRowsAffected = SqlHelper.ExecuteNonQuery(SqlHelper.defaultConnStr, cmd, CommandType.Text);
            if (numOfRowsAffected == 1)
            {
                //DataRow newRow = dataTable.NewRow();

                //newRow["ProductName"] = txtName.Text;
                //newRow["Price"] = Convert.ToInt32(txtPrice.Text);
                //newRow["Description"] = txtDesc.Text;
                //newRow["CategoryID"] = Convert.ToInt32(cbCate.SelectedValue.ToString());
                //newRow["SupplierID"] = Convert.ToInt32(cbSup.SelectedValue.ToString());

                //dataTable.Rows.Add(newRow);
                //sqlHelper.Update(dataTable);
                GetDataGridView();
            }
            else
            {
                MyMessageBox.Error("Thêm bản ghi thất bại!");
            }
        }

        private void EditProduct()
        {
            int rowIdxNeedEdit = dgvProduct.CurrentRow.Index;
            int productIDNeedEdit = Convert.ToInt32(dgvProduct.Rows[rowIdxNeedEdit].Cells["ProductID"].Value.ToString().Trim());

            bool isSuccess = ProductHelpers.EditProduct(
                    productIDNeedEdit,
                    txtName.Text,
                    Convert.ToInt32(txtPrice.Text), txtDesc.Text,
                    Convert.ToInt32(cbCate.SelectedValue.ToString()),
                    Convert.ToInt32(cbSup.SelectedValue.ToString())
                );

            if (isSuccess)
            {
                DataRow editRow = dataTable.Rows[rowIdxNeedEdit];

                editRow["ProductName"] = txtName.Text;
                editRow["Price"] = Convert.ToInt32(txtPrice.Text);
                editRow["Description"] = txtDesc.Text;
                editRow["CategoryID"] = Convert.ToInt32(cbCate.SelectedValue.ToString());
                editRow["SupplierID"] = Convert.ToInt32(cbSup.SelectedValue.ToString());
            }
            else
            {
                MyMessageBox.Error("Sửa bản ghi thất bại!");
            }
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
            int rowIdxNeedDel = dgvProduct.CurrentRow.Index;
            int idNeedDel = Convert.ToInt32(dgvProduct.Rows[rowIdxNeedDel].Cells["ProductID"].Value.ToString());

            if (MyMessageBox.Question("Bạn có chắn xóa bản ghi đã chọn không?"))
            {
                if (ProductHelpers.Delete(idNeedDel))
                {
                    dataTable.Rows[rowIdxNeedDel].Delete();
                }
                else
                {
                    MyMessageBox.Error("Xoá bản ghi thất bại!");
                }
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
