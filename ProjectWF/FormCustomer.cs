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

        private void GetDataGridView()
        {
            string query = "select c.CustomerID, c.FirstName, c.LastName, c.Address, c.Phone, c.Email from TableCustomers c";
            dataTable = sqlHelper.ExecuteQuery(SqlHelper.defaultConnStr, query, CommandType.Text);
            dgvCustomer.DataSource = dataTable;
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
            if (!MyValidation.CommonValidation(txtFName.Text, 1, 20, "Họ"))
            {
                txtFName.Focus();
                return false;
            }

            if (!MyValidation.CommonValidation(txtLName.Text, 1, 20, "Tên"))
            {
                txtLName.Focus();
                return false;
            }

            if (!MyValidation.CommonValidation(txtAddress.Text, 1, 50, "Địa chỉ"))
            {
                txtAddress.Focus();
                return false;
            }

            if (!MyValidation.CommonValidation(txtPhone.Text, 1, 11, "Số điện thoại") || !MyValidation.IsPhoneInvalid(txtPhone.Text))
            {
                txtPhone.Focus();
                return false;
            }

            if (!MyValidation.CommonValidation(txtEmail.Text, 1, 30, "Email") || !MyValidation.IsEmailInvalid(txtEmail.Text))
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
            // Control helper
            control = new ControlHelper();
            control.AddBtnControls(btnAdd, btnEdit, btnDelete, btnSave, btnCancel);
            control.AddTextBoxs(txtFName, txtLName, txtAddress, txtPhone, txtEmail);

            // Control mode
            control.SwitchMode(ControlHelper.ControlMode.None);
            txtFName.Focus();

            // DataGridView
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
            if (MyMessageBox.Question("Bạn có chắn xóa bản ghi đã chọn không?"))
            {
                int curRowIdx = dgvCustomer.CurrentRow.Index;
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

        private void dgvCustomer_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int idx = e.RowIndex;
            HandleRowEnter(idx);
        }
        #endregion
    }
}
