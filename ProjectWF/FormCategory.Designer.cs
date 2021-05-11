
namespace ProjectWF
{
    partial class FormCategory
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelControl = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtCateDesc = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCateName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgvCate = new System.Windows.Forms.DataGridView();
            this.panelControl.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCate)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl
            // 
            this.panelControl.Controls.Add(this.btnExit);
            this.panelControl.Controls.Add(this.btnCancel);
            this.panelControl.Controls.Add(this.btnSave);
            this.panelControl.Controls.Add(this.btnDelete);
            this.panelControl.Controls.Add(this.btnEdit);
            this.panelControl.Controls.Add(this.btnAdd);
            this.panelControl.Location = new System.Drawing.Point(12, 338);
            this.panelControl.Name = "panelControl";
            this.panelControl.Size = new System.Drawing.Size(363, 92);
            this.panelControl.TabIndex = 1;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(260, 56);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "Thoát";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(146, 56);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Huỷ bỏ";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(28, 56);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(260, 18);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Xoá";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(146, 18);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "Sửa";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(28, 18);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Thêm";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtCateDesc);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtCateName);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(363, 320);
            this.panel1.TabIndex = 0;
            // 
            // txtCateDesc
            // 
            this.txtCateDesc.Location = new System.Drawing.Point(139, 58);
            this.txtCateDesc.MaxLength = 100;
            this.txtCateDesc.Multiline = true;
            this.txtCateDesc.Name = "txtCateDesc";
            this.txtCateDesc.Size = new System.Drawing.Size(196, 96);
            this.txtCateDesc.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Mô tả (*):";
            // 
            // txtCateName
            // 
            this.txtCateName.Location = new System.Drawing.Point(139, 22);
            this.txtCateName.MaxLength = 50;
            this.txtCateName.Name = "txtCateName";
            this.txtCateName.Size = new System.Drawing.Size(196, 20);
            this.txtCateName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tên danh mục (*):";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgvCate);
            this.panel2.Location = new System.Drawing.Point(381, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(779, 418);
            this.panel2.TabIndex = 2;
            // 
            // dgvCate
            // 
            this.dgvCate.AllowUserToAddRows = false;
            this.dgvCate.AllowUserToDeleteRows = false;
            this.dgvCate.AllowUserToResizeRows = false;
            this.dgvCate.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCate.Location = new System.Drawing.Point(3, 22);
            this.dgvCate.MultiSelect = false;
            this.dgvCate.Name = "dgvCate";
            this.dgvCate.ReadOnly = true;
            this.dgvCate.Size = new System.Drawing.Size(746, 383);
            this.dgvCate.TabIndex = 0;
            this.dgvCate.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCate_RowEnter);
            // 
            // FormCategory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1172, 445);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panelControl);
            this.Controls.Add(this.panel1);
            this.Name = "FormCategory";
            this.Text = "Danh mục";
            this.Load += new System.EventHandler(this.FormCategory_Load);
            this.panelControl.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCate)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelControl;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtCateDesc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCateName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dgvCate;
    }
}