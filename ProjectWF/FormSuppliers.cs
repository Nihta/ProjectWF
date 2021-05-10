using System;
using System.Data;
using System.Windows.Forms;

namespace ProjectWF
{
    public partial class FormSuppliers : Form
    {
        private ControlHelper control;
        DataTable dataTable;
        SqlHelper sqlHelper;

        public FormSuppliers()
        {
            InitializeComponent();
        }

        #region Methods
        private void GetSuppliers()
        {
            string query = "select SupplierID, SupplierName, Address, Phone, Email from TableSuppliers";

            dataTable = sqlHelper.ExecuteQuery(SqlHelper.defaultConnStr, query, CommandType.Text);

            dgvSupplier.DataSource = dataTable;
        }

        private void AddSup()
        {
            DataRow newRow = dataTable.NewRow();

            newRow["SupplierName"] = txtSupName.Text;
            newRow["Address"] = txtSupAddress.Text;
            newRow["Phone"] = txtSupPhone.Text;
            newRow["Email"] = txtSupEmail.Text;

            dataTable.Rows.Add(newRow);
            sqlHelper.Update(dataTable);
            GetSuppliers();
        }

        private void EditSup()
        {
            int curRowIdx = dgvSupplier.CurrentRow.Index;

            DataRow editRow = dataTable.Rows[curRowIdx];

            editRow["SupplierName"] = txtSupName.Text;
            editRow["Address"] = txtSupAddress.Text;
            editRow["Phone"] = txtSupPhone.Text;
            editRow["Email"] = txtSupEmail.Text;

            sqlHelper.Update(dataTable);
        }
        #endregion


        #region Events
        private void FormSuppliers_Load(object sender, EventArgs e)
        {
            // Khởi tạo control helper
            control = new ControlHelper();
            control.AddBtnControls(btnAdd, btnEdit, btnDelete, btnSave, btnCancel);
            control.AddTextBoxs(txtSupName, txtSupAddress, txtSupPhone, txtSupEmail);

            // Mode mặc định
            control.SwitchMode(ControlHelper.ControlMode.None);
            txtSupName.Focus();

            // GetData
            sqlHelper = new SqlHelper();
            GetSuppliers();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            control.HandledAddClick();
            txtSupName.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            control.HandledEditClick();
            txtSupName.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MyMessageBox.Question("Bạn có chắn xóa bản ghi đã chọn không?"))
            {
                int curRowIdx = dgvSupplier.CurrentRow.Index;
                dataTable.Rows[curRowIdx].Delete();
                sqlHelper.Update(dataTable);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Validation
            //if (!UsersHelpers.IsFullNameInvalid(txtFullName.Text))
            //{
            //    txtFullName.Focus();
            //}
            //else if (!UsersHelpers.isUserNameInvalid(txtUserName.Text))
            //{
            //    txtUserName.Focus();
            //}
            //else if (!UsersHelpers.IsPassWordInvalid(txtPassWord.Text))
            //{
            //    txtPassWord.Focus();
            //}
            //else
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

        private void dgvSupplier_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int idx = e.RowIndex;
            txtSupName.Text = dgvSupplier.Rows[idx].Cells["SupplierName"].Value.ToString().Trim();
            txtSupAddress.Text = dgvSupplier.Rows[idx].Cells["Address"].Value.ToString().Trim();
            txtSupPhone.Text = dgvSupplier.Rows[idx].Cells["Phone"].Value.ToString().Trim();
            txtSupEmail.Text = dgvSupplier.Rows[idx].Cells["Email"].Value.ToString().Trim();
        }
        #endregion
    }
}
