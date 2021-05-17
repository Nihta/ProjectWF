using System;
using System.Data;
using System.Windows.Forms;

namespace ProjectWF
{
    public partial class FormCategory : Form
    {
        private ControlHelper control;

        public FormCategory()
        {
            InitializeComponent();

            ConfigDataGridView();

            control = new ControlHelper();
        }

        #region Methods
        private void ConfigDataGridView()
        {
            dgvCategory.AutoGenerateColumns = false;
            dgvCategory.Columns.Add(MyUtils.CreateCol(30, "CategoryID", "ID"));
            dgvCategory.Columns.Add(MyUtils.CreateCol(200, "CategoryName", "Danh mục"));
            dgvCategory.Columns.Add(MyUtils.CreateCol(300, "Description", "Mô tả"));
        }

        private int GetCurrentItemID()
        {
            int curRowIdx = dgvCategory.CurrentRow.Index;
            int id = Convert.ToInt32(dgvCategory.Rows[curRowIdx].Cells["CategoryID"].Value.ToString());
            return id;
        }

        private void RenderDataGridView()
        {
            CategoryLinq.DataGridViewHelper(dgvCategory);
        }

        private void HandleRowEnter(int idx)
        {
            txtCateName.Text = dgvCategory.Rows[idx].Cells["CategoryName"].Value.ToString().Trim();
            txtCateDesc.Text = dgvCategory.Rows[idx].Cells["Description"].Value.ToString().Trim();
        }

        private void Add()
        {
            CategoryLinq.Add(txtCateName.Text, txtCateDesc.Text);
        }

        private void EditRow()
        {
            int idNeedEdit = GetCurrentItemID();
            CategoryLinq.Edit(idNeedEdit, txtCateName.Text, txtCateDesc.Text);
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
            RenderDataGridView();
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
                if (MyMessageBox.Question("Bạn có chắn xóa danh mục đã chọn không?"))
                {
                    int idNeedDel = GetCurrentItemID();
                    CategoryLinq.Delete(idNeedDel);
                    RenderDataGridView();
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
                            Add();
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

        private void dgvCate_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int idx = e.RowIndex;
            HandleRowEnter(idx);
        }
        #endregion
    }
}
