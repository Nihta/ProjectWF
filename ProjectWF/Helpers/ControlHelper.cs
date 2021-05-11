using System.Windows.Forms;

namespace ProjectWF
{
    class ControlHelper
    {
        private ControlMode mode = ControlMode.None;

        public enum ControlMode
        {
            /// <summary>
            /// Chế độ mặc định
            /// </summary>
            None,
            /// <summary>
            /// Chế độ thêm bản ghi
            /// </summary>
            Add,
            /// <summary>
            /// Chế độ chỉnh sửa
            /// </summary>
            Edit,
        }

        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnSave;
        private Button btnCancel;

        private TextBox[] textBoxes;
        private ComboBox[] comboBoxes = null;

        public void AddBtnControls(Button btnAdd, Button btnEdit, Button btnDelete, Button btnSave, Button btnCancel)
        {
            this.btnAdd = btnAdd;
            this.btnEdit = btnEdit;
            this.btnDelete = btnDelete;
            this.btnSave = btnSave;
            this.btnCancel = btnCancel;
        }

        public void AddTextBoxs(params TextBox[] textBoxes)
        {
            this.textBoxes = textBoxes;
        }

        public void AddComboBoxs(params ComboBox[] comboBoxes)
        {
            this.comboBoxes = comboBoxes;
        }

        public void EnableControl(bool isEnable)
        {
            btnAdd.Enabled = isEnable;
            btnEdit.Enabled = isEnable;
            btnDelete.Enabled = isEnable;
            btnSave.Enabled = !isEnable;
            btnCancel.Enabled = !isEnable;
        }

        public void EnableTextBox(bool isEnable)
        {
            foreach (TextBox tb in textBoxes)
            {
                tb.Enabled = isEnable;
            }
        }

        public void ClearTextBox()
        {
            foreach (TextBox tb in textBoxes)
            {
                tb.Clear();
            }
        }

        private void EnableComboBox(bool isEnable)
        {
            if (comboBoxes != null && comboBoxes.Length != 0)
            {
                foreach (ComboBox cb in comboBoxes)
                {
                    cb.Enabled = isEnable;
                }
            }
        }

        public ControlMode GetMode()
        {
            return mode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mode">
        /// Các mode được hỗ trợ: "add", "edit", null
        /// </param>
        public void SwitchMode(ControlMode mode)
        {
            switch (mode)
            {
                case ControlMode.None:
                    EnableTextBox(false);
                    EnableComboBox(false);
                    EnableControl(true);
                    break;
                case ControlMode.Add:
                    EnableTextBox(true);
                    EnableComboBox(true);
                    EnableControl(false);
                    break;
                case ControlMode.Edit:
                    EnableTextBox(true);
                    EnableComboBox(true);
                    EnableControl(false);
                    break;
            }
            this.mode = mode;
        }

        #region HandleEvents
        public void HandledAddClick()
        {
            SwitchMode(ControlMode.Add);
            ClearTextBox();
        }

        public void HandledEditClick()
        {
            SwitchMode(ControlMode.Edit);
        }

        public void HandleCancelClick()
        {
            SwitchMode(ControlMode.None);
        }
        #endregion
    }
}
