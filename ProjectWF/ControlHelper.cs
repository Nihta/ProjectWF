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
                    EnableControl(true);
                    break;
                case ControlMode.Add:
                    EnableTextBox(true);
                    EnableControl(false);
                    break;
                case ControlMode.Edit:
                    EnableTextBox(true);
                    EnableControl(false);
                    break;
            }
            this.mode = mode;
        }
    }
}
