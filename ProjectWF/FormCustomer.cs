using System;
using System.Data;
using System.Windows.Forms;

namespace ProjectWF
{
    public partial class FormCustomer : Form
    {
        private ControlHelper control;
        DataTable dataTable;
        SqlHelper sqlHelper;

        public FormCustomer()
        {
            InitializeComponent();

            ConfigDataGridView();
            sqlHelper = new SqlHelper();
            control = new ControlHelper();
            CustomerHelpers.ConfigSearch(cbFields);
        }

        #region Methods
        private void ConfigDataGridView()
        {
            dgvCustomer.AutoGenerateColumns = false;
            dgvCustomer.Columns.Add(MyUtils.CreateCol(30, "CustomerID", "ID"));
            dgvCustomer.Columns.Add(MyUtils.CreateCol(100, "FirstName", "Họ"));
            dgvCustomer.Columns.Add(MyUtils.CreateCol(100, "LastName", "Tên"));
            dgvCustomer.Columns.Add(MyUtils.CreateCol(160, "Address", "Địa chỉ"));
            dgvCustomer.Columns.Add(MyUtils.CreateCol(100, "Phone", "Số điện thoại"));
            dgvCustomer.Columns.Add(MyUtils.CreateCol(160, "Email", "Email"));
        }

        private void GetDataGridView(string whereQuery = "")
        {
            dataTable = CustomerHelpers.DataGridViewHelper(sqlHelper, dgvCustomer, whereQuery);
        }

        private void HandleRowEnter(int idx)
        {
            txtFName.Text = dgvCustomer.Rows[idx].Cells["FirstName"].Value.ToString().Trim();
            txtLName.Text = dgvCustomer.Rows[idx].Cells["LastName"].Value.ToString().Trim();
            txtAddress.Text = dgvCustomer.Rows[idx].Cells["Address"].Value.ToString().Trim();
            txtPhone.Text = dgvCustomer.Rows[idx].Cells["Phone"].Value.ToString().Trim();
            txtEmail.Text = dgvCustomer.Rows[idx].Cells["Email"].Value.ToString().Trim();
        }

        private void AddSup()
        {
            DataRow newRow = dataTable.NewRow();

            newRow["FirstName"] = txtFName.Text;
            newRow["LastName"] = txtLName.Text;
            newRow["Address"] = txtAddress.Text;
            newRow["Phone"] = txtPhone.Text;
            newRow["Email"] = txtEmail.Text;

            dataTable.Rows.Add(newRow);
            sqlHelper.Update(dataTable);
            GetDataGridView();
        }

        private void EditSup()
        {
            int curRowIdx = dgvCustomer.CurrentRow.Index;
            DataRow editRow = dataTable.Rows[curRowIdx];

            editRow["FirstName"] = txtFName.Text;
            editRow["LastName"] = txtLName.Text;
            editRow["Address"] = txtAddress.Text;
            editRow["Phone"] = txtPhone.Text;
            editRow["Email"] = txtEmail.Text;

            sqlHelper.Update(dataTable);
        }

        public bool IsInvalid()
        {
            if (!NewValidation.IsTextInvalid(txtFName.Text, 1, CustomerHelpers.sizeFirstNameParam, "Họ"))
            {
                txtFName.Focus();
                return false;
            }

            if (!NewValidation.IsTextInvalid(txtLName.Text, 1, CustomerHelpers.sizeLastNameParam, "Tên"))
            {
                txtLName.Focus();
                return false;
            }

            if (!NewValidation.IsTextInvalid(txtAddress.Text, 1, CustomerHelpers.sizeAddressParam, "Địa chỉ"))
            {
                txtAddress.Focus();
                return false;
            }

            if (!NewValidation.IsTextInvalid(txtPhone.Text, 1, CustomerHelpers.sizePhoneParam, "Số điện thoại") || !NewValidation.IsPhoneInvalid(txtPhone.Text))
            {
                txtPhone.Focus();
                return false;
            }

            if (!NewValidation.IsTextInvalid(txtEmail.Text, 1, CustomerHelpers.sizeEmailParam, "Email") || !NewValidation.IsEmailInvalid(txtEmail.Text))
            {
                txtEmail.Focus();
                return false;
            }

            return true;
        }
        #endregion

        #region Events
        private void FormCustomer_Load(object sender, EventArgs e)
        {
            control.AddBtnControls(btnAdd, btnEdit, btnDelete, btnSave, btnCancel);
            control.AddTextBoxs(txtFName, txtLName, txtAddress, txtPhone, txtEmail);
            control.AddDataGridView(dgvCustomer);

            control.SwitchMode(ControlHelper.ControlMode.None);
            txtFName.Focus();

            GetDataGridView();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            control.HandledAddClick();
            txtFName.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            control.HandledEditClick();
            txtFName.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvCustomer.CurrentRow != null)
            {
                if (MyMessageBox.Question("Bạn có chắn xóa bản ghi đã chọn không?"))
                {
                    int curRowIdx = dgvCustomer.CurrentRow.Index;
                    int idNeedDel = Convert.ToInt32(dgvCustomer.Rows[curRowIdx].Cells["CustomerID"].Value.ToString());

                    DataTableHelpers.RemoveRow(dataTable, "CustomerID", idNeedDel);

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

        private void dgvCustomer_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int idx = e.RowIndex;
            HandleRowEnter(idx);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string whereQuery = CustomerHelpers.GetWhereQuery(cbFields, txtFieldValue);
            GetDataGridView(whereQuery);
        }

        private void cbFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFieldValue.Clear();
        }
        #endregion
    }
}
