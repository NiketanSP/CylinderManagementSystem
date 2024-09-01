namespace CylinderManagementSystem
{
    partial class Cylinder_Master
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
            this.txt_Cylinder_Name = new System.Windows.Forms.TextBox();
            this.txt_Cylinder_Rate = new System.Windows.Forms.TextBox();
            this.cmb_Cylinder_Type = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btn_Save = new System.Windows.Forms.Button();
            this.dgv_Cylinder_Master = new System.Windows.Forms.DataGridView();
            this.Column6 = new System.Windows.Forms.DataGridViewLinkColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewLinkColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rdb_Cylinder_Status_Fill = new System.Windows.Forms.RadioButton();
            this.rdb_Cylinder_Status_Empty = new System.Windows.Forms.RadioButton();
            this.rdb_Cylinder_Status_Scrap = new System.Windows.Forms.RadioButton();
            this.panel9 = new System.Windows.Forms.Panel();
            this.label19 = new System.Windows.Forms.Label();
            this.txt_Address = new System.Windows.Forms.TextBox();
            this.txt_Contact_Person_Name = new System.Windows.Forms.TextBox();
            this.lblGSTno = new System.Windows.Forms.Label();
            this.lblPANNumber = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_Contact = new System.Windows.Forms.TextBox();
            this.txt_Gst_No = new System.Windows.Forms.TextBox();
            this.lblMobile = new System.Windows.Forms.Label();
            this.cmb_Supplier_Name = new System.Windows.Forms.ComboBox();
            this.lbl_Pur_Date = new System.Windows.Forms.Label();
            this.lblPhone = new System.Windows.Forms.Label();
            this.txt_Invoice_No = new System.Windows.Forms.TextBox();
            this.dt_In_Date = new System.Windows.Forms.DateTimePicker();
            this.txt_Cylinder_Serial_No = new System.Windows.Forms.ComboBox();
            this.txt_Cylinder_Part_No = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Cylinder_Master)).BeginInit();
            this.panel9.SuspendLayout();
            this.SuspendLayout();
            // 
            // txt_Cylinder_Name
            // 
            this.txt_Cylinder_Name.Location = new System.Drawing.Point(175, 278);
            this.txt_Cylinder_Name.Name = "txt_Cylinder_Name";
            this.txt_Cylinder_Name.Size = new System.Drawing.Size(215, 20);
            this.txt_Cylinder_Name.TabIndex = 2;
            // 
            // txt_Cylinder_Rate
            // 
            this.txt_Cylinder_Rate.Location = new System.Drawing.Point(175, 331);
            this.txt_Cylinder_Rate.Name = "txt_Cylinder_Rate";
            this.txt_Cylinder_Rate.Size = new System.Drawing.Size(215, 20);
            this.txt_Cylinder_Rate.TabIndex = 4;
            // 
            // cmb_Cylinder_Type
            // 
            this.cmb_Cylinder_Type.FormattingEnabled = true;
            this.cmb_Cylinder_Type.Location = new System.Drawing.Point(175, 304);
            this.cmb_Cylinder_Type.Name = "cmb_Cylinder_Type";
            this.cmb_Cylinder_Type.Size = new System.Drawing.Size(215, 21);
            this.cmb_Cylinder_Type.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 226);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Cylinder Serial Number";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 252);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Cylinder Part Number";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 278);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Cylinder Name";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(32, 331);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Cylinder Rate";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(32, 304);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Cylinder Type";
            // 
            // btn_Save
            // 
            this.btn_Save.Location = new System.Drawing.Point(320, 466);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(100, 31);
            this.btn_Save.TabIndex = 17;
            this.btn_Save.Text = "Save";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dgv_Cylinder_Master
            // 
            this.dgv_Cylinder_Master.AllowUserToAddRows = false;
            this.dgv_Cylinder_Master.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_Cylinder_Master.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_Cylinder_Master.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Cylinder_Master.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5});
            this.dgv_Cylinder_Master.Location = new System.Drawing.Point(551, 11);
            this.dgv_Cylinder_Master.Name = "dgv_Cylinder_Master";
            this.dgv_Cylinder_Master.RowHeadersVisible = false;
            this.dgv_Cylinder_Master.Size = new System.Drawing.Size(782, 89);
            this.dgv_Cylinder_Master.TabIndex = 41;
            this.dgv_Cylinder_Master.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvCylinder_Master_RowPostPaint);
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Remove";
            this.Column6.Name = "Column6";
            this.Column6.Text = "Remove";
            this.Column6.UseColumnTextForLinkValue = true;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Edit";
            this.Column7.Name = "Column7";
            this.Column7.Text = "Edit";
            this.Column7.UseColumnTextForLinkValue = true;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "Sr. No";
            this.Column8.Name = "Column8";
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "C_ID";
            this.Column1.HeaderText = "ID";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "C_P_No";
            this.Column2.HeaderText = "Phone No";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "C_Name";
            this.Column3.HeaderText = "Name";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "C_Rate";
            this.Column4.HeaderText = "Rate";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "C_Type";
            this.Column5.HeaderText = "Type";
            this.Column5.Name = "Column5";
            // 
            // rdb_Cylinder_Status_Fill
            // 
            this.rdb_Cylinder_Status_Fill.AutoSize = true;
            this.rdb_Cylinder_Status_Fill.Location = new System.Drawing.Point(105, 376);
            this.rdb_Cylinder_Status_Fill.Name = "rdb_Cylinder_Status_Fill";
            this.rdb_Cylinder_Status_Fill.Size = new System.Drawing.Size(37, 17);
            this.rdb_Cylinder_Status_Fill.TabIndex = 42;
            this.rdb_Cylinder_Status_Fill.TabStop = true;
            this.rdb_Cylinder_Status_Fill.Text = "FILL";
            this.rdb_Cylinder_Status_Fill.UseVisualStyleBackColor = true;
            // 
            // rdb_Cylinder_Status_Empty
            // 
            this.rdb_Cylinder_Status_Empty.AutoSize = true;
            this.rdb_Cylinder_Status_Empty.Location = new System.Drawing.Point(105, 399);
            this.rdb_Cylinder_Status_Empty.Name = "rdb_Cylinder_Status_Empty";
            this.rdb_Cylinder_Status_Empty.Size = new System.Drawing.Size(54, 17);
            this.rdb_Cylinder_Status_Empty.TabIndex = 43;
            this.rdb_Cylinder_Status_Empty.TabStop = true;
            this.rdb_Cylinder_Status_Empty.Text = "EMPTY";
            this.rdb_Cylinder_Status_Empty.UseVisualStyleBackColor = true;
            // 
            // rdb_Cylinder_Status_Scrap
            // 
            this.rdb_Cylinder_Status_Scrap.AutoSize = true;
            this.rdb_Cylinder_Status_Scrap.Location = new System.Drawing.Point(105, 422);
            this.rdb_Cylinder_Status_Scrap.Name = "rdb_Cylinder_Status_Scrap";
            this.rdb_Cylinder_Status_Scrap.Size = new System.Drawing.Size(53, 17);
            this.rdb_Cylinder_Status_Scrap.TabIndex = 44;
            this.rdb_Cylinder_Status_Scrap.TabStop = true;
            this.rdb_Cylinder_Status_Scrap.Text = "Scrap";
            this.rdb_Cylinder_Status_Scrap.UseVisualStyleBackColor = true;
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.White;
            this.panel9.Controls.Add(this.txt_Cylinder_Part_No);
            this.panel9.Controls.Add(this.txt_Cylinder_Serial_No);
            this.panel9.Controls.Add(this.label19);
            this.panel9.Controls.Add(this.rdb_Cylinder_Status_Scrap);
            this.panel9.Controls.Add(this.btn_Save);
            this.panel9.Controls.Add(this.txt_Address);
            this.panel9.Controls.Add(this.rdb_Cylinder_Status_Empty);
            this.panel9.Controls.Add(this.rdb_Cylinder_Status_Fill);
            this.panel9.Controls.Add(this.txt_Contact_Person_Name);
            this.panel9.Controls.Add(this.lblGSTno);
            this.panel9.Controls.Add(this.lblPANNumber);
            this.panel9.Controls.Add(this.label4);
            this.panel9.Controls.Add(this.label6);
            this.panel9.Controls.Add(this.txt_Contact);
            this.panel9.Controls.Add(this.label5);
            this.panel9.Controls.Add(this.txt_Gst_No);
            this.panel9.Controls.Add(this.label3);
            this.panel9.Controls.Add(this.lblMobile);
            this.panel9.Controls.Add(this.label2);
            this.panel9.Controls.Add(this.cmb_Supplier_Name);
            this.panel9.Controls.Add(this.label1);
            this.panel9.Controls.Add(this.lbl_Pur_Date);
            this.panel9.Controls.Add(this.cmb_Cylinder_Type);
            this.panel9.Controls.Add(this.txt_Cylinder_Rate);
            this.panel9.Controls.Add(this.lblPhone);
            this.panel9.Controls.Add(this.txt_Cylinder_Name);
            this.panel9.Controls.Add(this.txt_Invoice_No);
            this.panel9.Controls.Add(this.dt_In_Date);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel9.Location = new System.Drawing.Point(0, 0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(463, 523);
            this.panel9.TabIndex = 45;
            this.panel9.Paint += new System.Windows.Forms.PaintEventHandler(this.panel9_Paint);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(66, 151);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(45, 13);
            this.label19.TabIndex = 45;
            this.label19.Text = "Address";
            // 
            // txt_Address
            // 
            this.txt_Address.Location = new System.Drawing.Point(121, 146);
            this.txt_Address.Multiline = true;
            this.txt_Address.Name = "txt_Address";
            this.txt_Address.Size = new System.Drawing.Size(325, 23);
            this.txt_Address.TabIndex = 44;
            // 
            // txt_Contact_Person_Name
            // 
            this.txt_Contact_Person_Name.Location = new System.Drawing.Point(121, 80);
            this.txt_Contact_Person_Name.Name = "txt_Contact_Person_Name";
            this.txt_Contact_Person_Name.Size = new System.Drawing.Size(184, 20);
            this.txt_Contact_Person_Name.TabIndex = 21;
            // 
            // lblGSTno
            // 
            this.lblGSTno.AutoSize = true;
            this.lblGSTno.Location = new System.Drawing.Point(33, 85);
            this.lblGSTno.Name = "lblGSTno";
            this.lblGSTno.Size = new System.Drawing.Size(80, 13);
            this.lblGSTno.TabIndex = 30;
            this.lblGSTno.Text = "Contact Person";
            // 
            // lblPANNumber
            // 
            this.lblPANNumber.AutoSize = true;
            this.lblPANNumber.Location = new System.Drawing.Point(35, 115);
            this.lblPANNumber.Name = "lblPANNumber";
            this.lblPANNumber.Size = new System.Drawing.Size(61, 13);
            this.lblPANNumber.TabIndex = 26;
            this.lblPANNumber.Text = "Contact No";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(66, 187);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 40;
            this.label4.Text = "GST No";
            // 
            // txt_Contact
            // 
            this.txt_Contact.Location = new System.Drawing.Point(121, 111);
            this.txt_Contact.MaxLength = 10;
            this.txt_Contact.Name = "txt_Contact";
            this.txt_Contact.Size = new System.Drawing.Size(184, 20);
            this.txt_Contact.TabIndex = 20;
            // 
            // txt_Gst_No
            // 
            this.txt_Gst_No.Location = new System.Drawing.Point(121, 182);
            this.txt_Gst_No.Name = "txt_Gst_No";
            this.txt_Gst_No.Size = new System.Drawing.Size(325, 20);
            this.txt_Gst_No.TabIndex = 39;
            // 
            // lblMobile
            // 
            this.lblMobile.AutoSize = true;
            this.lblMobile.Location = new System.Drawing.Point(34, 51);
            this.lblMobile.Name = "lblMobile";
            this.lblMobile.Size = new System.Drawing.Size(82, 13);
            this.lblMobile.TabIndex = 25;
            this.lblMobile.Text = "Customer Name";
            // 
            // cmb_Supplier_Name
            // 
            this.cmb_Supplier_Name.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmb_Supplier_Name.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmb_Supplier_Name.FormattingEnabled = true;
            this.cmb_Supplier_Name.Location = new System.Drawing.Point(121, 46);
            this.cmb_Supplier_Name.Name = "cmb_Supplier_Name";
            this.cmb_Supplier_Name.Size = new System.Drawing.Size(325, 21);
            this.cmb_Supplier_Name.TabIndex = 31;
            // 
            // lbl_Pur_Date
            // 
            this.lbl_Pur_Date.AutoSize = true;
            this.lbl_Pur_Date.Location = new System.Drawing.Point(273, 19);
            this.lbl_Pur_Date.Name = "lbl_Pur_Date";
            this.lbl_Pur_Date.Size = new System.Drawing.Size(54, 13);
            this.lbl_Pur_Date.TabIndex = 30;
            this.lbl_Pur_Date.Text = "Sale Date";
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Location = new System.Drawing.Point(20, 17);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(82, 13);
            this.lblPhone.TabIndex = 24;
            this.lblPhone.Text = "Invoice Number";
            // 
            // txt_Invoice_No
            // 
            this.txt_Invoice_No.BackColor = System.Drawing.SystemColors.Window;
            this.txt_Invoice_No.Location = new System.Drawing.Point(121, 15);
            this.txt_Invoice_No.Name = "txt_Invoice_No";
            this.txt_Invoice_No.Size = new System.Drawing.Size(141, 20);
            this.txt_Invoice_No.TabIndex = 19;
            // 
            // dt_In_Date
            // 
            this.dt_In_Date.CustomFormat = "";
            this.dt_In_Date.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dt_In_Date.Location = new System.Drawing.Point(339, 15);
            this.dt_In_Date.Name = "dt_In_Date";
            this.dt_In_Date.Size = new System.Drawing.Size(107, 20);
            this.dt_In_Date.TabIndex = 29;
            // 
            // txt_Cylinder_Serial_No
            // 
            this.txt_Cylinder_Serial_No.FormattingEnabled = true;
            this.txt_Cylinder_Serial_No.Location = new System.Drawing.Point(175, 224);
            this.txt_Cylinder_Serial_No.Name = "txt_Cylinder_Serial_No";
            this.txt_Cylinder_Serial_No.Size = new System.Drawing.Size(215, 21);
            this.txt_Cylinder_Serial_No.TabIndex = 46;
            this.txt_Cylinder_Serial_No.SelectedIndexChanged += new System.EventHandler(this.txt_Cylinder_Serial_No_SelectedIndexChanged);
            // 
            // txt_Cylinder_Part_No
            // 
            this.txt_Cylinder_Part_No.FormattingEnabled = true;
            this.txt_Cylinder_Part_No.Location = new System.Drawing.Point(175, 251);
            this.txt_Cylinder_Part_No.Name = "txt_Cylinder_Part_No";
            this.txt_Cylinder_Part_No.Size = new System.Drawing.Size(215, 21);
            this.txt_Cylinder_Part_No.TabIndex = 47;
            // 
            // Cylinder_Master
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1267, 523);
            this.Controls.Add(this.panel9);
            this.Controls.Add(this.dgv_Cylinder_Master);
            this.Name = "Cylinder_Master";
            this.Text = "CYLINDER ACTIVITY";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Cylinder_Master)).EndInit();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox txt_Cylinder_Name;
        private System.Windows.Forms.TextBox txt_Cylinder_Rate;
        private System.Windows.Forms.ComboBox cmb_Cylinder_Type;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.DataGridView dgv_Cylinder_Master;
        private System.Windows.Forms.DataGridViewLinkColumn Column6;
        private System.Windows.Forms.DataGridViewLinkColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.RadioButton rdb_Cylinder_Status_Fill;
        private System.Windows.Forms.RadioButton rdb_Cylinder_Status_Empty;
        private System.Windows.Forms.RadioButton rdb_Cylinder_Status_Scrap;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txt_Address;
        private System.Windows.Forms.TextBox txt_Contact_Person_Name;
        private System.Windows.Forms.Label lblGSTno;
        private System.Windows.Forms.Label lblPANNumber;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_Contact;
        private System.Windows.Forms.TextBox txt_Gst_No;
        private System.Windows.Forms.Label lblMobile;
        private System.Windows.Forms.ComboBox cmb_Supplier_Name;
        private System.Windows.Forms.Label lbl_Pur_Date;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.TextBox txt_Invoice_No;
        private System.Windows.Forms.DateTimePicker dt_In_Date;
        private System.Windows.Forms.ComboBox txt_Cylinder_Serial_No;
        private System.Windows.Forms.ComboBox txt_Cylinder_Part_No;
    }
}