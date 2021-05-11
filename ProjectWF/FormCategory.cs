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
        }

        //#region Methods
        private void ConfigDataGridView()
        {
            dgvCate.AutoGenerateColumns = false;
            dgvCate.Columns.Add(MyUtils.CreateCol(30, "CategoryID", "ID"));
            dgvCate.Columns.Add(MyUtils.CreateCol(200, "CategoryName", "Danh mục"));
            dgvCate.Columns.Add(MyUtils.CreateCol(300, "Description", "Mô tả"));
        }

        private void GetDataGridView()
        {
            string query = "select c.CategoryID, c.CategoryName, c.Description from dbo.TableCategorys c";
            dataTable = sqlHelper.ExecuteQuery(SqlHelper.defaultConnStr, query, CommandType.Text);
            dgvCate.DataSource = dataTable;
        }

        private void HandleRowEnter(int idx)
        {
            txtCateName.Text = dgvCate.Rows[idx].Cells["CategoryName"].Value.ToString().Trim();
            txtCateDesc.Text = dgvCate.Rows[idx].Cells["Description"].Value.ToString().Trim();
        }

        private void AddSup()
        {
            DataRow newRow = dataTable.NewRow();
            newRow["CategoryName"] = txtCateName.Text;
            newRow["Description"] = txtCateDesc.Text;
            dataTable.Rows.Add(newRow);
            sqlHelper.Update(dataTable);
            GetDataGridView();
        }

        private void EditSup()
        {
            int curRowIdx = dgvCate.CurrentRow.Index;
            DataRow editRow = dataTable.Rows[curRowIdx];
            editRow["CategoryName"] = txtCateName.Text;
            editRow["Description"] = txtCateDesc.Text;
            sqlHelper.Update(dataTable);
        }

        public bool IsInvalid()
        {
            if (!MyValidation.CommonValidation(txtCateName.Text, 1, 50, "Tên danh mục"))
            {
                txtCateName.Focus();
                return false;
            }

            if (!MyValidation.CommonValidation(txtCateDesc.Text, 1, 100, "Mô tả"))
            {
                txtCateDesc.Focus();
                return false;
            }

            return true;
        }
        //#endregion

        #region Events
        private void FormCategory_Load(object sender, EventArgs e)
        {
            // Init
            sqlHelper = new SqlHelper();

            // Control helper
            control = new ControlHelper();
            control.AddBtnControls(btnAdd, btnEdit, btnDelete, btnSave, btnCancel);
            control.AddTextBoxs(txtCateName, txtCateDesc);

            // Control mode
            control.SwitchMode(ControlHelper.ControlMode.None);
            txtCateName.Focus();

            // DataGridView
            ConfigDataGridView();
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
            if (MyMessageBox.Question("Bạn có chắn xóa bản ghi đã chọn không?"))
            {
                int curRowIdx = dgvCate.CurrentRow.Index;
                dataTable.Rows[curRowIdx].Delete();
                sqlHelper.Update(dataTable);
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
                            AddSup();
                        }
                        break;
                    case ControlHelper.ControlMode.Edit:
                        {
                            EditSup();
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
