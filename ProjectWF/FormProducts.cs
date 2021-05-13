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

        public FormProducts()
        {
            InitializeComponent();

            control = new ControlHelper();
            ConfigDataGridView();
        }

        #region Methods
        private void ConfigDataGridView()
        {
            dgvProduct.AutoGenerateColumns = false;
            dgvProduct.Columns.Add(MyUtils.CreateCol(30, "ProductID", "ID"));
            dgvProduct.Columns.Add(MyUtils.CreateCol(100, "ProductName", "Tên sản phẩm"));
            dgvProduct.Columns.Add(MyUtils.CreateCol(100, "Price", "Giá"));
            dgvProduct.Columns.Add(MyUtils.CreateCol(160, "Description", "Mô tả"));
            dgvProduct.Columns.Add(MyUtils.CreateCol(100, "CategoryName", "Danh mục"));
            dgvProduct.Columns.Add(MyUtils.CreateCol(100, "SupplierName", "Nhà cung cấp"));

            dgvProduct.Columns.Add(MyUtils.CreateCol(100, "CategoryID", "Danh mục"));
            dgvProduct.Columns["CategoryID"].Visible = false;
            dgvProduct.Columns.Add(MyUtils.CreateCol(100, "SupplierID", "Nhà cung cấp"));
            dgvProduct.Columns["SupplierID"].Visible = false;
        }

        private void GetDataGridView()
        {
            dataTable = ProductHelpers.GetDataTable();
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
            int categoryID = Convert.ToInt32(cbCate.SelectedValue.ToString());
            int supplierID = Convert.ToInt32(cbSup.SelectedValue.ToString());
            int price = Convert.ToInt32(txtPrice.Text);

            bool isSuccess = ProductHelpers.AddProduct(
                txtName.Text,
                price,
                txtDesc.Text,
                categoryID,
                supplierID
            );

            if (isSuccess)
            {
                // TODO: Cập nhập lại dataGripView mà không cần GetDataGridView() lại
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

            int categoryID = Convert.ToInt32(cbCate.SelectedValue.ToString());
            int supplierID = Convert.ToInt32(cbSup.SelectedValue.ToString());
            int price = Convert.ToInt32(txtPrice.Text);

            bool isSuccess = ProductHelpers.EditProduct(
                    productIDNeedEdit,
                    txtName.Text,
                    price,
                    txtDesc.Text,
                    categoryID,
                    supplierID
                );

            if (isSuccess)
            {
                DataRow editRow = dataTable.Rows[rowIdxNeedEdit];
                editRow["ProductName"] = txtName.Text;
                editRow["Price"] = price;
                editRow["Description"] = txtDesc.Text;
                editRow["CategoryID"] = categoryID;
                editRow["SupplierID"] = supplierID;
                editRow["CategoryName"] = cbCate.Text;
                editRow["SupplierName"] = cbSup.Text;
            }
            else
            {
                MyMessageBox.Error("Sửa bản ghi thất bại!");
            }
        }

        public bool IsInvalid()
        {
            if (!NewValidation.IsTextInvalid(txtName.Text, 1, 50, "Tên sản phẩm"))
            {
                txtName.Focus();
                return false;
            }

            if (!NewValidation.IsNumeric(txtPrice.Text, "Giá"))
            {
                txtName.Focus();
                return false;
            }

            if (!NewValidation.IsTextInvalid(txtDesc.Text, 1, 50, "Mô tả"))
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
            control.AddDataGridView(dgvProduct);

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
                bool isSuccess = ProductHelpers.Delete(idNeedDel);
                if (isSuccess)
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
                            // TODO: Trường hợp một sản phẩm thể có 2 nhà cung cấp
                            if (ProductHelpers.CheckProductNameExist(txtName.Text))
                            {
                                MyMessageBox.Warning("Tên sản phẩm đã được sử dụng!");
                                txtName.Focus();
                                return;
                            }
                            AddProduct();
                        }
                        break;
                    case ControlHelper.ControlMode.Edit:
                        {
                            int curProductID = Convert.ToInt32(dgvProduct.Rows[dgvProduct.CurrentRow.Index].Cells["ProductID"].Value.ToString().Trim());
                            if (ProductHelpers.CheckProductNameExist(txtName.Text, curProductID))
                            {
                                MyMessageBox.Warning("Trùng tên sản phẩm!");
                                txtName.Focus();
                                return;
                            }
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
