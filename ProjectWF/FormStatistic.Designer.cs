
namespace ProjectWF
{
    partial class FormStatistic
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
            this.dateTimePickerStart = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerEnd = new System.Windows.Forms.DateTimePicker();
            this.dgvOrder = new System.Windows.Forms.DataGridView();
            this.btnFilter = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnAll = new System.Windows.Forms.Button();
            this.btnMonth = new System.Windows.Forms.Button();
            this.btnWeek = new System.Windows.Forms.Button();
            this.btnDay = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrder)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dateTimePickerStart
            // 
            this.dateTimePickerStart.CustomFormat = "dd/MM/yyyy";
            this.dateTimePickerStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerStart.Location = new System.Drawing.Point(20, 30);
            this.dateTimePickerStart.MaxDate = new System.DateTime(2100, 1, 1, 0, 0, 0, 0);
            this.dateTimePickerStart.MinDate = new System.DateTime(2021, 1, 1, 0, 0, 0, 0);
            this.dateTimePickerStart.Name = "dateTimePickerStart";
            this.dateTimePickerStart.Size = new System.Drawing.Size(167, 20);
            this.dateTimePickerStart.TabIndex = 18;
            this.dateTimePickerStart.Value = new System.DateTime(2021, 5, 19, 0, 0, 0, 0);
            // 
            // dateTimePickerEnd
            // 
            this.dateTimePickerEnd.CustomFormat = "dd/MM/yyyy";
            this.dateTimePickerEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerEnd.Location = new System.Drawing.Point(230, 30);
            this.dateTimePickerEnd.MaxDate = new System.DateTime(2100, 1, 1, 0, 0, 0, 0);
            this.dateTimePickerEnd.MinDate = new System.DateTime(2021, 1, 1, 0, 0, 0, 0);
            this.dateTimePickerEnd.Name = "dateTimePickerEnd";
            this.dateTimePickerEnd.Size = new System.Drawing.Size(162, 20);
            this.dateTimePickerEnd.TabIndex = 20;
            this.dateTimePickerEnd.Value = new System.DateTime(2021, 5, 19, 0, 0, 0, 0);
            // 
            // dgvOrder
            // 
            this.dgvOrder.AllowUserToAddRows = false;
            this.dgvOrder.AllowUserToDeleteRows = false;
            this.dgvOrder.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrder.Location = new System.Drawing.Point(17, 30);
            this.dgvOrder.MultiSelect = false;
            this.dgvOrder.Name = "dgvOrder";
            this.dgvOrder.ReadOnly = true;
            this.dgvOrder.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrder.Size = new System.Drawing.Size(560, 380);
            this.dgvOrder.TabIndex = 21;
            this.dgvOrder.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOrder_CellContentClick);
            // 
            // btnFilter
            // 
            this.btnFilter.Location = new System.Drawing.Point(20, 77);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(372, 23);
            this.btnFilter.TabIndex = 22;
            this.btnFilter.Text = "Lọc phạm vi";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnAll);
            this.groupBox1.Controls.Add(this.btnMonth);
            this.groupBox1.Controls.Add(this.btnFilter);
            this.groupBox1.Controls.Add(this.btnWeek);
            this.groupBox1.Controls.Add(this.btnDay);
            this.groupBox1.Controls.Add(this.dateTimePickerStart);
            this.groupBox1.Controls.Add(this.dateTimePickerEnd);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(422, 169);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Phạm vi";
            // 
            // btnAll
            // 
            this.btnAll.Location = new System.Drawing.Point(317, 125);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(75, 23);
            this.btnAll.TabIndex = 28;
            this.btnAll.Text = "Tất cả";
            this.btnAll.UseVisualStyleBackColor = true;
            this.btnAll.Click += new System.EventHandler(this.btnAll_Click);
            // 
            // btnMonth
            // 
            this.btnMonth.Location = new System.Drawing.Point(230, 125);
            this.btnMonth.Name = "btnMonth";
            this.btnMonth.Size = new System.Drawing.Size(75, 23);
            this.btnMonth.TabIndex = 27;
            this.btnMonth.Text = "Tháng";
            this.btnMonth.UseVisualStyleBackColor = true;
            // 
            // btnWeek
            // 
            this.btnWeek.Location = new System.Drawing.Point(112, 125);
            this.btnWeek.Name = "btnWeek";
            this.btnWeek.Size = new System.Drawing.Size(75, 23);
            this.btnWeek.TabIndex = 26;
            this.btnWeek.Text = "Tuần";
            this.btnWeek.UseVisualStyleBackColor = true;
            // 
            // btnDay
            // 
            this.btnDay.Location = new System.Drawing.Point(20, 125);
            this.btnDay.Name = "btnDay";
            this.btnDay.Size = new System.Drawing.Size(75, 23);
            this.btnDay.TabIndex = 25;
            this.btnDay.Text = "Hôm nay";
            this.btnDay.UseVisualStyleBackColor = true;
            this.btnDay.Click += new System.EventHandler(this.btnDay_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvOrder);
            this.groupBox2.Location = new System.Drawing.Point(440, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(594, 426);
            this.groupBox2.TabIndex = 25;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Hoá đơn";
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(32, 399);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 29;
            this.btnExit.Text = "Thoát";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(339, 259);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 30;
            this.button1.Text = "Xem chi tiết";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormStatistic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1046, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormStatistic";
            this.Text = "Thống kê";
            this.Load += new System.EventHandler(this.FormStatistic_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrder)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePickerStart;
        private System.Windows.Forms.DateTimePicker dateTimePickerEnd;
        private System.Windows.Forms.DataGridView dgvOrder;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnDay;
        private System.Windows.Forms.Button btnWeek;
        private System.Windows.Forms.Button btnMonth;
        private System.Windows.Forms.Button btnAll;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button button1;
    }
}