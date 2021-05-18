using System;
using System.Data;
using System.Windows.Forms;

namespace ProjectWF
{
    public partial class FormCustomer : Form
    {
        public enum mode
        {
            nomal,
            select,
        };

        private ControlHelper control;
        private mode formMode;
        public int ReturnCustumerID { get; set; }
        public string ReturnCustumerName { get; set; }

        public FormCustomer(mode mode = mode.nomal)
        {
            InitializeComponent();

            ConfigDataGridView();
            CustomerHelpers.ConfigSearch(cbFields);

            this.formMode = mode;
            if (formMode == mode.select)
            {
                ReturnCustumerID = -1;
                ReturnCustumerName = "";
                btnSelect.Show();
            }

            control = new ControlHelper();
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
        private int GetCurrentItemID()
        {
            int curRowIdx = dgvCustomer.CurrentRow.Index;
            int id = Convert.ToInt32(dgvCustomer.Rows[curRowIdx].Cells["CustomerID"].Value.ToString());
            return id;
        }

        private void RenderDataGridView(string field = "", string value = "")
        {
            CustomerLinq.DataGridViewHelper(dgvCustomer, field, value);
        }

        private void HandleRowEnter(int idx)
        {
            txtFName.Text = dgvCustomer.Rows[idx].Cells["FirstName"].Value.ToString().Trim();
            txtLName.Text = dgvCustomer.Rows[idx].Cells["LastName"].Value.ToString().Trim();
            txtAddress.Text = dgvCustomer.Rows[idx].Cells["Address"].Value.ToString().Trim();
            txtPhone.Text = dgvCustomer.Rows[idx].Cells["Phone"].Value.ToString().Trim();
            txtEmail.Text = dgvCustomer.Rows[idx].Cells["Email"].Value.ToString().Trim();
        }

        private void AddCustomer()
        {
            CustomerLinq.Add(txtFName.Text, txtLName.Text, txtAddress.Text, txtPhone.Text, txtEmail.Text);
        }

        private void EditCustomer()
        {
            int idNeedEdit = GetCurrentItemID();
            CustomerLinq.Edit(idNeedEdit, txtFName.Text, txtLName.Text, txtAddress.Text, txtPhone.Text, txtEmail.Text);
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

            RenderDataGridView();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            control.HandledAddClick();
            control.ClearTextBox();
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
                if (MyMessageBox.Question("Bạn có chắn xóa khách hàng đã chọn không?"))
                {
                    int idNeedDel = GetCurrentItemID();
                    CustomerLinq.Delete(idNeedDel);
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
                            AddCustomer();
                        }
                        break;
                    case ControlHelper.ControlMode.Edit:
                        {
                            EditCustomer();
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

        private void dgvCustomer_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int idx = e.RowIndex;
            HandleRowEnter(idx);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            RenderDataGridView(cbFields.Text, txtFieldValue.Text);
        }

        private void cbFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFieldValue.Clear();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (dgvCustomer.CurrentRow != null)
            {
                int curRowIdx = dgvCustomer.CurrentRow.Index;
                int idSelected = GetCurrentItemID();

                string fName = dgvCustomer.Rows[curRowIdx].Cells["FirstName"].Value.ToString();
                string LName = dgvCustomer.Rows[curRowIdx].Cells["LastName"].Value.ToString();
                string fullName = $"{fName} {LName}";

                this.ReturnCustumerID = idSelected;
                this.ReturnCustumerName = fullName;

                this.Close();
            }
            else
            {
                MyMessageBox.Error("Không thể chọn!");
            }
        }
        #endregion
    }
}
