using System;
using System.Data;
using System.Windows.Forms;

namespace ProjectWF
{
    public partial class FormProducts : Form
    {
        private ControlHelper control;
        DataTable dataTable;
        private mode formMode;

        public enum mode
        {
            nomal,
            select,
        };

        public int ReturnProductID { get; set; }
        public string ReturnProductName { get; set; }

        public FormProducts(mode mode = mode.nomal)
        {
            this.formMode = mode;

            InitializeComponent();
            ConfigDataGridView();

            // Chế độ chọn mặt hàng
            if (formMode == mode.select)
            {
                ReturnProductID = -1;
                ReturnProductName = "";
                btnSelect.Show();
            }

            control = new ControlHelper();
        }

        #region Methods
        private void ConfigDataGridView()
        {
            dgvProduct.AutoGenerateColumns = false;
            dgvProduct.Columns.Add(MyUtils.CreateCol(30, "ProductID", "ID"));
            dgvProduct.Columns.Add(MyUtils.CreateCol(160, "ProductName", "Tên sản phẩm"));
            dgvProduct.Columns.Add(MyUtils.CreateCol(100, "Price", "Giá"));
            dgvProduct.Columns.Add(MyUtils.CreateCol(160, "Description", "Mô tả"));
            dgvProduct.Columns.Add(MyUtils.CreateCol(100, "CategoryName", "Danh mục"));
            dgvProduct.Columns.Add(MyUtils.CreateCol(120, "SupplierName", "Nhà cung cấp"));

            dgvProduct.Columns.Add(MyUtils.CreateCol(0, "CategoryID", "Danh mục"));
            dgvProduct.Columns["CategoryID"].Visible = false;
            dgvProduct.Columns.Add(MyUtils.CreateCol(0, "SupplierID", "Nhà cung cấp"));
            dgvProduct.Columns["SupplierID"].Visible = false;
        }

        private int GetCurrentProductID()
        {
            int curRowIdx = dgvProduct.CurrentRow.Index;
            int id = Convert.ToInt32(dgvProduct.Rows[curRowIdx].Cells["ProductID"].Value.ToString());
            return id;
        }

        private void RenderDataGridView(string productName = "", int categoryID = -1, int supplierID = -1)
        {
            ProductLinq.DataGridViewHelper(dgvProduct, productName, categoryID, supplierID);
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

            ProductLinq.Add(txtName.Text, price, txtDesc.Text, categoryID, supplierID);
        }

        private void EditProduct()
        {
            int idNeedEdit = GetCurrentProductID();

            int categoryID = Convert.ToInt32(cbCate.SelectedValue.ToString());
            int supplierID = Convert.ToInt32(cbSup.SelectedValue.ToString());
            int price = Convert.ToInt32(txtPrice.Text);

            ProductLinq.Edit(idNeedEdit, txtName.Text, price, txtDesc.Text, categoryID, supplierID);
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

            if (!NewValidation.IsTextInvalid(txtDesc.Text, 1, 50, "Mô tả", false))
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

            RenderDataGridView();

            MyUtils.FillComboBoxWithDataCategory(cbCate);
            MyUtils.FillComboBoxWithDataSupplier(cbSup);

            MyUtils.FillComboBoxWithDataCategory(cbSearchCate);
            MyUtils.FillComboBoxWithDataSupplier(cbSearchSup);
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
            if (dgvProduct.CurrentRow != null)
            {
                if (MyMessageBox.Question("Bạn có chắn xóa bản ghi đã chọn không?"))
                {
                    int idNeedDel = GetCurrentProductID();
                    ProductLinq.Delete(idNeedDel);
                    RenderDataGridView();
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
                            // TODO: Xử lý trường hợp một sản phẩm thể có 2 nhà cung cấp
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
                RenderDataGridView();
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

        private void btnReset_Click(object sender, EventArgs e)
        {
            RenderDataGridView();
            txtSearchName.Clear();
            cbSearchCate.SelectedIndex = 0;
            cbSearchSup.SelectedIndex = 0;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            int categoryID = Convert.ToInt32(cbSearchCate.SelectedValue.ToString());
            int supplierID = Convert.ToInt32(cbSearchSup.SelectedValue.ToString());
            string productName = txtSearchName.Text;

            RenderDataGridView(productName, categoryID, supplierID);
        }
        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (dgvProduct.CurrentRow != null)
            {
                int curRowIdx = dgvProduct.CurrentRow.Index;
                int idSelected = GetCurrentProductID();

                string productName = dgvProduct.Rows[curRowIdx].Cells["ProductName"].Value.ToString();

                this.ReturnProductID = idSelected;
                this.ReturnProductName = productName;

                this.Close();
            }
            else
            {
                MyMessageBox.Warning("Không thể chọn!");
            }
        }
        #endregion
    }
}
