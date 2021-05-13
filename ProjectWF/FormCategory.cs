using System;
using System.Data;
using System.Windows.Forms;

namespace ProjectWF
{
    public partial class FormCategory : Form
    {
        private ControlHelper control;
        DataTable dataTable;
        SqlHelper sqlHelper;

        public FormCategory()
        {
            InitializeComponent();

            sqlHelper = new SqlHelper();
            control = new ControlHelper();
            ConfigDataGridView();
        }

        #region Methods
        private void ConfigDataGridView()
        {
            dgvCategory.AutoGenerateColumns = false;
            dgvCategory.Columns.Add(MyUtils.CreateCol(30, "CategoryID", "ID"));
            dgvCategory.Columns.Add(MyUtils.CreateCol(200, "CategoryName", "Danh mục"));
            dgvCategory.Columns.Add(MyUtils.CreateCol(300, "Description", "Mô tả"));
        }

        private void GetDataGridView()
        {
            dataTable = CategoryHelpers.DataGridViewHelper(sqlHelper, dgvCategory);
        }

        private void HandleRowEnter(int idx)
        {
            txtCateName.Text = dgvCategory.Rows[idx].Cells["CategoryName"].Value.ToString().Trim();
            txtCateDesc.Text = dgvCategory.Rows[idx].Cells["Description"].Value.ToString().Trim();
        }

        private void AddRow()
        {
            DataRow newRow = dataTable.NewRow();
            newRow["CategoryName"] = txtCateName.Text;
            newRow["Description"] = txtCateDesc.Text;
            dataTable.Rows.Add(newRow);
            sqlHelper.Update(dataTable);
            GetDataGridView();
            control.ClearTextBox();
        }

        private void EditRow()
        {
            int curRowIdx = dgvCategory.CurrentRow.Index;
            DataRow editRow = dataTable.Rows[curRowIdx];
            editRow["CategoryName"] = txtCateName.Text;
            editRow["Description"] = txtCateDesc.Text;
            sqlHelper.Update(dataTable);
        }

        public bool IsInvalid()
        {
            if (!NewValidation.IsTextInvalid(txtCateName.Text, 1, CategoryHelpers.sizeCategoryNameParam, "Tên danh mục"))
            {
                txtCateName.Focus();
                return false;
            }

            if (!NewValidation.IsTextInvalid(txtCateDesc.Text, 1, CategoryHelpers.sizeDescriptionParam, "Mô tả", false))
            {
                txtCateDesc.Focus();
                return false;
            }

            return true;
        }
        #endregion

        #region Events
        private void FormCategory_Load(object sender, EventArgs e)
        {
            // Control helper
            control.AddBtnControls(btnAdd, btnEdit, btnDelete, btnSave, btnCancel);
            control.AddTextBoxs(txtCateName, txtCateDesc);
            control.AddDataGridView(dgvCategory);

            // Control mode
            control.SwitchMode(ControlHelper.ControlMode.None);
            txtCateName.Focus();

            // DataGridView
            GetDataGridView();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            control.HandledAddClick();
            txtCateName.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            control.HandledEditClick();
            txtCateName.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvCategory.CurrentRow != null)
            {
                if (MyMessageBox.Question("Bạn có chắn xóa bản ghi đã chọn không?"))
                {
                    int curRowIdx = dgvCategory.CurrentRow.Index;
                    int idNeedDel = Convert.ToInt32(dgvCategory.Rows[curRowIdx].Cells["CategoryID"].Value.ToString());

                    DataTableHelpers.RemoveRow(dataTable, "CategoryID", idNeedDel);

                    dataTable.Rows[curRowIdx].Delete();
                    sqlHelper.Update(dataTable);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (IsInvalid())
            {
                // Cập nhật data
                switch (control.GetMode())
                {
                    case ControlHelper.ControlMode.Add:
                        {
                            AddRow();
                        }
                        break;
                    case ControlHelper.ControlMode.Edit:
                        {
                            EditRow();
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

        private void dgvCate_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int idx = e.RowIndex;
            HandleRowEnter(idx);
        }
        #endregion
    }
}
