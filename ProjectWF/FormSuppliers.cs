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

            ConfigDataGridView();

            control = new ControlHelper();
            sqlHelper = new SqlHelper();
        }

        #region Methods
        private void ConfigDataGridView()
        {
            dgvSupplier.AutoGenerateColumns = false;
            dgvSupplier.Columns.Add(MyUtils.CreateCol(30, "SupplierID", "ID"));
            dgvSupplier.Columns.Add(MyUtils.CreateCol(200, "SupplierName", "Tên nhà cung cấp"));
            dgvSupplier.Columns.Add(MyUtils.CreateCol(200, "Address", "Địa chỉ"));
            dgvSupplier.Columns.Add(MyUtils.CreateCol(110, "Phone", "Số điện thoại"));
            dgvSupplier.Columns.Add(MyUtils.CreateCol(150, "Email"));
        }

        private void GetDataGridView()
        {
            dataTable = SupplierHelpers.DataGridViewHelper(sqlHelper, dgvSupplier);
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
            GetDataGridView();
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

        public bool IsInvalid()
        {
            if (!SupplierHelpers.IsSupplierNameInvalid(txtSupName.Text))
            {
                txtSupName.Focus();
                return false;
            }

            if (!SupplierHelpers.IsAddressInvalid(txtSupAddress.Text))
            {
                txtSupAddress.Focus();
                return false;
            }

            if (!SupplierHelpers.IsPhoneInvalid(txtSupPhone.Text))
            {
                txtSupPhone.Focus();
                return false;
            }

            if (!SupplierHelpers.IsEmailInvalid(txtSupEmail.Text))
            {
                txtSupEmail.Focus();
                return false;
            }

            return true;
        }

        private void HandleRowEnter(int idx)
        {
            txtSupName.Text = dgvSupplier.Rows[idx].Cells["SupplierName"].Value.ToString().Trim();
            txtSupAddress.Text = dgvSupplier.Rows[idx].Cells["Address"].Value.ToString().Trim();
            txtSupPhone.Text = dgvSupplier.Rows[idx].Cells["Phone"].Value.ToString().Trim();
            txtSupEmail.Text = dgvSupplier.Rows[idx].Cells["Email"].Value.ToString().Trim();
        }
        #endregion


        #region Events
        private void FormSuppliers_Load(object sender, EventArgs e)
        {
            control.AddBtnControls(btnAdd, btnEdit, btnDelete, btnSave, btnCancel);
            control.AddTextBoxs(txtSupName, txtSupAddress, txtSupPhone, txtSupEmail);
            control.AddDataGridView(dgvSupplier);

            // Mode mặc định
            control.SwitchMode(ControlHelper.ControlMode.None);
            txtSupName.Focus();

            // GetData
            GetDataGridView();
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
            if (dgvSupplier.CurrentRow != null)
            {
                if (MyMessageBox.Question("Bạn có chắn xóa bản ghi đã chọn không?"))
                {
                    int curRowIdx = dgvSupplier.CurrentRow.Index;
                    int idNeedDel = Convert.ToInt32(dgvSupplier.Rows[curRowIdx].Cells["SupplierID"].Value.ToString());

                    DataTableHelpers.RemoveRow(dataTable, "SupplierID", idNeedDel);

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

        private void dgvSupplier_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int idx = e.RowIndex;
            HandleRowEnter(idx);
        }
        #endregion
    }
}
