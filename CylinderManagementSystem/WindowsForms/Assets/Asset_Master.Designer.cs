namespace CylinderManagementSystem
{
    partial class Asset_Master
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle36 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle29 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle30 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle31 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle32 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle33 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle34 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle35 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dt_Purchase_Date = new System.Windows.Forms.DateTimePicker();
            this.cmb_Asset_Type = new System.Windows.Forms.ComboBox();
            this.lblNameOfAsset = new System.Windows.Forms.Label();
            this.lblPurchaseDate = new System.Windows.Forms.Label();
            this.lblPurchaseCost = new System.Windows.Forms.Label();
            this.lblAssetType = new System.Windows.Forms.Label();
            this.dgv_Asset_Master = new System.Windows.Forms.DataGridView();
            this.tbAssetDetailsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel17 = new System.Windows.Forms.Panel();
            this.panel15 = new System.Windows.Forms.Panel();
            this.panel18 = new System.Windows.Forms.Panel();
            this.panel29 = new System.Windows.Forms.Panel();
            this.panel24 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel30 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.panel25 = new System.Windows.Forms.Panel();
            this.panel14 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.panel11 = new System.Windows.Forms.Panel();
            this.panel12 = new System.Windows.Forms.Panel();
            this.btn_Search = new System.Windows.Forms.Button();
            this.btn_Save = new System.Windows.Forms.Button();
            this.txt_Name_Of_Asset = new System.Windows.Forms.TextBox();
            this.cmb_Serach_Asset = new System.Windows.Forms.ComboBox();
            this.txt_Purchase_Cost = new System.Windows.Forms.TextBox();
            this.panel21 = new System.Windows.Forms.Panel();
            this.panel20 = new System.Windows.Forms.Panel();
            this.panel22 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.Column5 = new System.Windows.Forms.DataGridViewLinkColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewLinkColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.purchase = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Asset_Master)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbAssetDetailsBindingSource)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel18.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel30.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel12.SuspendLayout();
            this.panel20.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dt_Purchase_Date
            // 
            this.dt_Purchase_Date.CustomFormat = "dd/MM/yyyy";
            this.dt_Purchase_Date.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dt_Purchase_Date.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dt_Purchase_Date.Location = new System.Drawing.Point(10, 31);
            this.dt_Purchase_Date.Name = "dt_Purchase_Date";
            this.dt_Purchase_Date.Size = new System.Drawing.Size(144, 23);
            this.dt_Purchase_Date.TabIndex = 1;
            this.dt_Purchase_Date.ValueChanged += new System.EventHandler(this.dt_Purchase_Date_ValueChanged);
            // 
            // cmb_Asset_Type
            // 
            this.cmb_Asset_Type.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_Asset_Type.FormattingEnabled = true;
            this.cmb_Asset_Type.Location = new System.Drawing.Point(421, 31);
            this.cmb_Asset_Type.Name = "cmb_Asset_Type";
            this.cmb_Asset_Type.Size = new System.Drawing.Size(142, 23);
            this.cmb_Asset_Type.TabIndex = 2;
            this.cmb_Asset_Type.SelectedIndexChanged += new System.EventHandler(this.cmb_Asset_Type_SelectedIndexChanged);
            // 
            // lblNameOfAsset
            // 
            this.lblNameOfAsset.AutoSize = true;
            this.lblNameOfAsset.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNameOfAsset.Location = new System.Drawing.Point(163, 10);
            this.lblNameOfAsset.Name = "lblNameOfAsset";
            this.lblNameOfAsset.Size = new System.Drawing.Size(84, 15);
            this.lblNameOfAsset.TabIndex = 6;
            this.lblNameOfAsset.Text = "Name of Asset";
            // 
            // lblPurchaseDate
            // 
            this.lblPurchaseDate.AutoSize = true;
            this.lblPurchaseDate.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPurchaseDate.Location = new System.Drawing.Point(10, 10);
            this.lblPurchaseDate.Name = "lblPurchaseDate";
            this.lblPurchaseDate.Size = new System.Drawing.Size(86, 15);
            this.lblPurchaseDate.TabIndex = 2;
            this.lblPurchaseDate.Text = "Purchase Date";
            // 
            // lblPurchaseCost
            // 
            this.lblPurchaseCost.AutoSize = true;
            this.lblPurchaseCost.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPurchaseCost.Location = new System.Drawing.Point(572, 10);
            this.lblPurchaseCost.Name = "lblPurchaseCost";
            this.lblPurchaseCost.Size = new System.Drawing.Size(85, 15);
            this.lblPurchaseCost.TabIndex = 8;
            this.lblPurchaseCost.Text = "Purchase Cost";
            // 
            // lblAssetType
            // 
            this.lblAssetType.AutoSize = true;
            this.lblAssetType.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAssetType.Location = new System.Drawing.Point(421, 10);
            this.lblAssetType.Name = "lblAssetType";
            this.lblAssetType.Size = new System.Drawing.Size(63, 15);
            this.lblAssetType.TabIndex = 9;
            this.lblAssetType.Text = "Asset Type";
            // 
            // dgv_Asset_Master
            // 
            this.dgv_Asset_Master.AllowUserToAddRows = false;
            this.dgv_Asset_Master.BackgroundColor = System.Drawing.Color.White;
            this.dgv_Asset_Master.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle28.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle28.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle28.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle28.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle28.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle28.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle28.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_Asset_Master.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle28;
            this.dgv_Asset_Master.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Asset_Master.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column5,
            this.Column6,
            this.Column7,
            this.name,
            this.cost,
            this.purchase,
            this.type});
            dataGridViewCellStyle36.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle36.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle36.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle36.ForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle36.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle36.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle36.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_Asset_Master.DefaultCellStyle = dataGridViewCellStyle36;
            this.dgv_Asset_Master.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_Asset_Master.EnableHeadersVisualStyles = false;
            this.dgv_Asset_Master.Location = new System.Drawing.Point(0, 0);
            this.dgv_Asset_Master.Name = "dgv_Asset_Master";
            this.dgv_Asset_Master.RowHeadersVisible = false;
            this.dgv_Asset_Master.Size = new System.Drawing.Size(1209, 409);
            this.dgv_Asset_Master.TabIndex = 28;
            this.dgv_Asset_Master.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_Asset_Master_CellContentClick);
            this.dgv_Asset_Master.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgv_Asset_Master_RowPostPaint);
            // 
            // tbAssetDetailsBindingSource
            // 
            this.tbAssetDetailsBindingSource.DataMember = "Tb_AssetDetails";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(25, 556);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1209, 25);
            this.panel3.TabIndex = 44;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel17);
            this.panel1.Controls.Add(this.panel15);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(25, 581);
            this.panel1.TabIndex = 43;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Silver;
            this.panel5.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel5.Location = new System.Drawing.Point(24, 25);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1, 531);
            this.panel5.TabIndex = 47;
            // 
            // panel17
            // 
            this.panel17.BackColor = System.Drawing.Color.White;
            this.panel17.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel17.Location = new System.Drawing.Point(0, 556);
            this.panel17.Name = "panel17";
            this.panel17.Size = new System.Drawing.Size(25, 25);
            this.panel17.TabIndex = 41;
            // 
            // panel15
            // 
            this.panel15.BackColor = System.Drawing.Color.White;
            this.panel15.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel15.Location = new System.Drawing.Point(0, 0);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(25, 25);
            this.panel15.TabIndex = 40;
            // 
            // panel18
            // 
            this.panel18.BackColor = System.Drawing.Color.White;
            this.panel18.Controls.Add(this.panel6);
            this.panel18.Controls.Add(this.panel29);
            this.panel18.Controls.Add(this.panel24);
            this.panel18.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel18.Location = new System.Drawing.Point(1234, 0);
            this.panel18.Name = "panel18";
            this.panel18.Size = new System.Drawing.Size(25, 581);
            this.panel18.TabIndex = 46;
            // 
            // panel29
            // 
            this.panel29.BackColor = System.Drawing.Color.White;
            this.panel29.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel29.Location = new System.Drawing.Point(0, 0);
            this.panel29.Name = "panel29";
            this.panel29.Size = new System.Drawing.Size(25, 25);
            this.panel29.TabIndex = 46;
            // 
            // panel24
            // 
            this.panel24.BackColor = System.Drawing.Color.White;
            this.panel24.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel24.Location = new System.Drawing.Point(0, 556);
            this.panel24.Name = "panel24";
            this.panel24.Size = new System.Drawing.Size(25, 25);
            this.panel24.TabIndex = 45;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(25, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1209, 25);
            this.panel4.TabIndex = 45;
            this.panel4.Paint += new System.Windows.Forms.PaintEventHandler(this.panel4_Paint);
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.Chocolate;
            this.panel8.Controls.Add(this.panel30);
            this.panel8.Controls.Add(this.panel25);
            this.panel8.Controls.Add(this.panel14);
            this.panel8.Controls.Add(this.panel9);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel8.Location = new System.Drawing.Point(25, 529);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(1209, 27);
            this.panel8.TabIndex = 48;
            this.panel8.Paint += new System.Windows.Forms.PaintEventHandler(this.panel8_Paint);
            // 
            // panel30
            // 
            this.panel30.BackColor = System.Drawing.Color.Transparent;
            this.panel30.Controls.Add(this.button1);
            this.panel30.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel30.Location = new System.Drawing.Point(1068, 0);
            this.panel30.Name = "panel30";
            this.panel30.Size = new System.Drawing.Size(139, 26);
            this.panel30.TabIndex = 47;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(7, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(128, 22);
            this.button1.TabIndex = 28;
            this.button1.Text = "Export To Excel";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel25
            // 
            this.panel25.BackColor = System.Drawing.Color.Silver;
            this.panel25.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel25.Location = new System.Drawing.Point(1207, 0);
            this.panel25.Name = "panel25";
            this.panel25.Size = new System.Drawing.Size(1, 26);
            this.panel25.TabIndex = 46;
            // 
            // panel14
            // 
            this.panel14.BackColor = System.Drawing.Color.Silver;
            this.panel14.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel14.Location = new System.Drawing.Point(1208, 0);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(1, 26);
            this.panel14.TabIndex = 45;
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.Silver;
            this.panel9.Controls.Add(this.panel10);
            this.panel9.Controls.Add(this.panel11);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel9.Location = new System.Drawing.Point(0, 26);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(1209, 1);
            this.panel9.TabIndex = 42;
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.Color.White;
            this.panel10.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel10.Location = new System.Drawing.Point(0, -24);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(1209, 25);
            this.panel10.TabIndex = 41;
            // 
            // panel11
            // 
            this.panel11.BackColor = System.Drawing.Color.White;
            this.panel11.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel11.Location = new System.Drawing.Point(0, 0);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(1209, 25);
            this.panel11.TabIndex = 40;
            // 
            // panel12
            // 
            this.panel12.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel12.Controls.Add(this.btn_Search);
            this.panel12.Controls.Add(this.dt_Purchase_Date);
            this.panel12.Controls.Add(this.btn_Save);
            this.panel12.Controls.Add(this.lblAssetType);
            this.panel12.Controls.Add(this.lblPurchaseCost);
            this.panel12.Controls.Add(this.txt_Name_Of_Asset);
            this.panel12.Controls.Add(this.cmb_Serach_Asset);
            this.panel12.Controls.Add(this.txt_Purchase_Cost);
            this.panel12.Controls.Add(this.lblPurchaseDate);
            this.panel12.Controls.Add(this.cmb_Asset_Type);
            this.panel12.Controls.Add(this.lblNameOfAsset);
            this.panel12.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel12.Location = new System.Drawing.Point(25, 50);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(1209, 70);
            this.panel12.TabIndex = 49;
            // 
            // btn_Search
            // 
            this.btn_Search.BackColor = System.Drawing.Color.White;
            this.btn_Search.Location = new System.Drawing.Point(847, 30);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(114, 25);
            this.btn_Search.TabIndex = 78;
            this.btn_Search.Text = "Search";
            this.btn_Search.UseVisualStyleBackColor = false;
            this.btn_Search.Click += new System.EventHandler(this.button2_Click);
            // 
            // btn_Save
            // 
            this.btn_Save.BackColor = System.Drawing.Color.White;
            this.btn_Save.Location = new System.Drawing.Point(725, 30);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(114, 25);
            this.btn_Save.TabIndex = 4;
            this.btn_Save.Text = "Save";
            this.btn_Save.UseVisualStyleBackColor = false;
            this.btn_Save.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txt_Name_Of_Asset
            // 
            this.txt_Name_Of_Asset.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_Name_Of_Asset.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Name_Of_Asset.Location = new System.Drawing.Point(163, 31);
            this.txt_Name_Of_Asset.Name = "txt_Name_Of_Asset";
            this.txt_Name_Of_Asset.Size = new System.Drawing.Size(249, 23);
            this.txt_Name_Of_Asset.TabIndex = 1;
            this.txt_Name_Of_Asset.TextChanged += new System.EventHandler(this.txt_Name_Of_Asset_TextChanged);
            // 
            // cmb_Serach_Asset
            // 
            this.cmb_Serach_Asset.BackColor = System.Drawing.Color.LemonChiffon;
            this.cmb_Serach_Asset.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_Serach_Asset.FormattingEnabled = true;
            this.cmb_Serach_Asset.Location = new System.Drawing.Point(163, 31);
            this.cmb_Serach_Asset.Name = "cmb_Serach_Asset";
            this.cmb_Serach_Asset.Size = new System.Drawing.Size(249, 23);
            this.cmb_Serach_Asset.TabIndex = 2;
            this.cmb_Serach_Asset.SelectedIndexChanged += new System.EventHandler(this.cmb_Serach_Asset_SelectedIndexChanged);
            // 
            // txt_Purchase_Cost
            // 
            this.txt_Purchase_Cost.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_Purchase_Cost.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Purchase_Cost.Location = new System.Drawing.Point(573, 31);
            this.txt_Purchase_Cost.Name = "txt_Purchase_Cost";
            this.txt_Purchase_Cost.Size = new System.Drawing.Size(143, 23);
            this.txt_Purchase_Cost.TabIndex = 3;
            this.txt_Purchase_Cost.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel21
            // 
            this.panel21.BackColor = System.Drawing.Color.Silver;
            this.panel21.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel21.Location = new System.Drawing.Point(1208, 0);
            this.panel21.Name = "panel21";
            this.panel21.Size = new System.Drawing.Size(1, 409);
            this.panel21.TabIndex = 43;
            // 
            // panel20
            // 
            this.panel20.BackColor = System.Drawing.Color.White;
            this.panel20.Controls.Add(this.panel21);
            this.panel20.Controls.Add(this.dgv_Asset_Master);
            this.panel20.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel20.Location = new System.Drawing.Point(25, 120);
            this.panel20.Name = "panel20";
            this.panel20.Size = new System.Drawing.Size(1209, 409);
            this.panel20.TabIndex = 50;
            // 
            // panel22
            // 
            this.panel22.BackColor = System.Drawing.Color.Silver;
            this.panel22.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel22.Location = new System.Drawing.Point(1208, 0);
            this.panel22.Name = "panel22";
            this.panel22.Size = new System.Drawing.Size(1, 25);
            this.panel22.TabIndex = 45;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(8, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 15);
            this.label1.TabIndex = 46;
            this.label1.Text = "ASSET REGISTER";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.panel22);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(25, 25);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1209, 25);
            this.panel2.TabIndex = 47;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.Silver;
            this.panel6.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel6.Location = new System.Drawing.Point(0, 25);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1, 531);
            this.panel6.TabIndex = 48;
            // 
            // Column5
            // 
            dataGridViewCellStyle29.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column5.DefaultCellStyle = dataGridViewCellStyle29;
            this.Column5.HeaderText = "REMOVE";
            this.Column5.LinkColor = System.Drawing.Color.Maroon;
            this.Column5.Name = "Column5";
            this.Column5.Text = "Remove";
            this.Column5.UseColumnTextForLinkValue = true;
            // 
            // Column6
            // 
            dataGridViewCellStyle30.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column6.DefaultCellStyle = dataGridViewCellStyle30;
            this.Column6.HeaderText = "EDIT";
            this.Column6.Name = "Column6";
            this.Column6.Text = "Edit";
            this.Column6.UseColumnTextForLinkValue = true;
            // 
            // Column7
            // 
            dataGridViewCellStyle31.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column7.DefaultCellStyle = dataGridViewCellStyle31;
            this.Column7.HeaderText = "SR";
            this.Column7.Name = "Column7";
            this.Column7.Width = 80;
            // 
            // name
            // 
            this.name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.name.DataPropertyName = "Asset_Name";
            dataGridViewCellStyle32.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.name.DefaultCellStyle = dataGridViewCellStyle32;
            this.name.HeaderText = "NAME";
            this.name.Name = "name";
            // 
            // cost
            // 
            this.cost.DataPropertyName = "Purchase_Cost";
            dataGridViewCellStyle33.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.cost.DefaultCellStyle = dataGridViewCellStyle33;
            this.cost.HeaderText = "COST";
            this.cost.Name = "cost";
            // 
            // purchase
            // 
            this.purchase.DataPropertyName = "Purchase_Date";
            dataGridViewCellStyle34.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.purchase.DefaultCellStyle = dataGridViewCellStyle34;
            this.purchase.HeaderText = "PURCHASED DATE";
            this.purchase.Name = "purchase";
            this.purchase.Width = 150;
            // 
            // type
            // 
            this.type.DataPropertyName = "Asset_Type";
            dataGridViewCellStyle35.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.type.DefaultCellStyle = dataGridViewCellStyle35;
            this.type.HeaderText = "TYPE";
            this.type.Name = "type";
            this.type.Width = 150;
            // 
            // Asset_Master
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1259, 581);
            this.Controls.Add(this.panel20);
            this.Controls.Add(this.panel12);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel18);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Asset_Master";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Asset_Master_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Asset_Master)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbAssetDetailsBindingSource)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel18.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel30.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel12.ResumeLayout(false);
            this.panel12.PerformLayout();
            this.panel20.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DateTimePicker dt_Purchase_Date;
        private System.Windows.Forms.ComboBox cmb_Asset_Type;
        private System.Windows.Forms.Label lblNameOfAsset;
        private System.Windows.Forms.Label lblPurchaseDate;
        private System.Windows.Forms.Label lblPurchaseCost;
        private System.Windows.Forms.Label lblAssetType;
        private System.Windows.Forms.DataGridView dgv_Asset_Master;
        private System.Windows.Forms.BindingSource tbAssetDetailsBindingSource;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel17;
        private System.Windows.Forms.Panel panel15;
        private System.Windows.Forms.Panel panel18;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.TextBox txt_Name_Of_Asset;
        private System.Windows.Forms.TextBox txt_Purchase_Cost;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.Panel panel25;
        private System.Windows.Forms.Panel panel30;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cmb_Serach_Asset;
        private System.Windows.Forms.Button btn_Search;
        private System.Windows.Forms.Panel panel21;
        private System.Windows.Forms.Panel panel20;
        private System.Windows.Forms.Panel panel29;
        private System.Windows.Forms.Panel panel24;
        private System.Windows.Forms.Panel panel22;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.DataGridViewLinkColumn Column5;
        private System.Windows.Forms.DataGridViewLinkColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn cost;
        private System.Windows.Forms.DataGridViewTextBoxColumn purchase;
        private System.Windows.Forms.DataGridViewTextBoxColumn type;
        private System.Windows.Forms.Panel panel6;
    }
}