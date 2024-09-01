using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace CylinderManagementSystem
{
    public partial class Customer_DC : Form
    {
        SqlConnection con;
        SqlDataReader sdr;
        SqlCommand cmd;
        SqlDataAdapter adpt;
        DataTable dt;
        string id_Month, id_Year, cdc_id, pay_mode = "NA";
        int dc_Id = 00;
        int in_Stock_Value, out_Stock_Value, balance, update_quantity;
        bool update = false;
        bool online = true;
        public Customer_DC()
        {
            InitializeComponent();
            //load_CDC_Id();
            load_Cmb_Cust_Name();
            load_cylinder_type();
            load_Particular();
            load_Unit_Combo();
            load_Vehicle_Combo();
        }
        private void load_Vehicle_Combo()
        {
            try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT DISTINCT Vehicle_No FROM Tb_CDC_Details", con);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    cmb_Vehicle_No.Items.Add(sdr[0]);
                }
                sdr.Close();
            }
            catch { }
            finally { con.Close(); }
        }
        private void Customer_DC_Load(object sender, EventArgs e)
        {
            cmb_Category.Text = "CYLINDER";
            if (cmb_Category.Text == "CYLINDER")
            {
                table_panel_Entry.ColumnStyles[1].SizeType = SizeType.Percent;
                table_panel_Entry.ColumnStyles[1].Width = 8;
                table_panel_Entry.ColumnStyles[2].SizeType = SizeType.Percent;
                table_panel_Entry.ColumnStyles[2].Width = 8;
                table_panel_Entry.ColumnStyles[3].SizeType = SizeType.Percent;
                table_panel_Entry.ColumnStyles[3].Width = 8;
                cmb_cy_Type.Text = "";
                load_Item_No_Combo();
                cmb_Particulars.ResetText();
                txt_Rate.Clear();
                txt_Quantity.Clear();
                cmb_Status.ResetText();
            }
            panel48.Hide();
            panel19.Show();
            panel37.Hide();
            panel38.Hide();
            panel56.Hide();
            txt_Paid_Amt_2.Hide();
            txt_Paid_Amt.Show();
            chkbox_Add_New_Product.Enabled = false;
            chk_If_Cancel.Enabled = false;
            txt_CDC_Id.Enabled = false;
            txt_Cust_Name.Enabled = false;
            cmb_Cust_Name.Enabled = false;
            txt_Deposit_Amt.Enabled = false;
            txt_For_Days.Enabled = false;
            txt_Contact_Person_Name.Enabled = false;
            txt_Contact.Enabled = false;
            txt_Address.Enabled = false;
        }
        public void getconnection()
        {
            string str = ConfigurationManager.ConnectionStrings["CylinderData"].ConnectionString;
            con = new SqlConnection(str);
        }
        private void cmb_Category_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_Category.Text == "CYLINDER")
            {
                table_panel_Entry.ColumnStyles[1].SizeType = SizeType.Percent;
                table_panel_Entry.ColumnStyles[1].Width = 8;
                table_panel_Entry.ColumnStyles[2].SizeType = SizeType.Percent;
                table_panel_Entry.ColumnStyles[2].Width = 8;
                table_panel_Entry.ColumnStyles[3].SizeType = SizeType.Percent;
                table_panel_Entry.ColumnStyles[3].Width = 8;
                cmb_cy_Type.Text = "";
                load_Item_No_Combo();
                cmb_Particulars.ResetText();
                txt_Rate.Clear();
                txt_Quantity.Clear();
                cmb_Status.ResetText();
            }
            else if (cmb_Category.Text == "MATERIAL")
            {
                table_panel_Entry.ColumnStyles[1].SizeType = SizeType.Percent;
                table_panel_Entry.ColumnStyles[1].Width = 0;
                table_panel_Entry.ColumnStyles[2].SizeType = SizeType.Percent;
                table_panel_Entry.ColumnStyles[2].Width = 0;
                table_panel_Entry.ColumnStyles[3].SizeType = SizeType.Percent;
                table_panel_Entry.ColumnStyles[3].Width = 0;
                cmb_cy_Type.Text = "NA";
                txt_Quantity.Clear();
                cmb_Status.ResetText();
                cmb_Particulars.ResetText();
                txt_Rate.Clear();
                cmb_Particulars.Enabled = true;
                cmb_cy_Type.Enabled = true;
                txt_Quantity.Enabled = true;
            }
            load_Particular();
        }
        private void load_Item_No_Combo()
        {
            cmb_Item_No.Items.Clear();
            try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT DISTINCT Part_No FROM Tb_Purchase_Content_Master WHERE Purchase_Type = 'CYLINDER' AND Cylinder_Status = 'FILL'", con);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    cmb_Item_No.Items.Add(sdr[0]);
                }
                sdr.Close();
            }
            catch { }
            finally { con.Close(); }
        }
        public void load_Particular()
        {
            cmb_Particulars.Items.Clear();
            try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT DISTINCT Particulars FROM Tb_Inventory_Master WHERE Cylinder_Type = '" + cmb_cy_Type.Text.ToString() +"'", con);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    cmb_Particulars.Items.Add(sdr[0]);
                }
                sdr.Close();
            }
            catch { }
            finally { con.Close(); }
        }
        public void load_Unit_Combo()
        {
            try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT DISTINCT Unit FROM Tb_Purchase_Content_Master", con);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    cmb_Unit.Items.Add(sdr[0]);
                }
                sdr.Close();
            }
            catch { }
            finally { con.Close(); }
        }
        public void load_cylinder_type()
        {
            try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT DISTINCT Cylinder_Type FROM Tb_Purchase_Content_Master WHERE Purchase_Type = 'CYLINDER'", con);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    cmb_cy_Type.Items.Add(sdr[0]);
                }
                sdr.Close();
            }
            catch { }
            finally { con.Close(); }
        }
        private void dgv_Content_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dgv_Content.Rows[e.RowIndex].Cells["sr"].Value = (e.RowIndex + 1).ToString();
        }
        public void clear_data()
        {
            cmb_cy_Type.ResetText();
            cmb_Particulars.ResetText();
            txt_Quantity.Clear();
            txt_Rate.Clear();
            cmb_Unit.ResetText();
            txt_Total.Clear();
            cmb_Status.ResetText();
            txt_Sr_No.Clear();
            cmb_Item_No.ResetText();
        }
        private void btn_Click(object sender, EventArgs e)
        {
            if (cmb_Category.Text == "")
            {
                MessageBox.Show("Select Material Category");
            }
         
            else if (cmb_cy_Type.Text == "")
            {
                MessageBox.Show("Select Cylinder Type");
            }
            else if (cmb_Particulars.Text == "")
            {
                MessageBox.Show("Enter Particular");
            }
            
            else if (txt_Quantity.Text == "")
            {
                MessageBox.Show("Enter Quantity");
            }
            else if (cmb_Unit.Text == "")
            {
                MessageBox.Show("Enter Unit");
            }
            else
            {
                if(btn_Save.Text == "Save")
                {
                    if (cmb_Category.Text == "CYLINDER")
                    {
                        int quantity = Convert.ToInt32(txt_Quantity.Text);
                        for (int i = 0; i < quantity; i++)
                        {
                            int index = dgv_Content.Rows.Add();
                            dgv_Content.Rows[index].Cells["type"].Value = cmb_cy_Type.Text.Trim();
                            dgv_Content.Rows[index].Cells["particulars"].Value = cmb_Particulars.Text.Trim();
                            dgv_Content.Rows[index].Cells["rate"].Value = txt_Rate.Text.Trim();
                            dgv_Content.Rows[index].Cells["quantity"].Value = "1";
                            dgv_Content.Rows[index].Cells["unit"].Value = cmb_Unit.Text.Trim();
                            dgv_Content.Rows[index].Cells["total"].Value = (Convert.ToInt32(txt_Total.Text) / quantity).ToString().Trim();
                            dgv_Content.Rows[index].Cells["status"].Value = cmb_Status.Text.Trim();
                            dgv_Content.Rows[index].Cells["item"].Value = cmb_Item_No.Text;
                            dgv_Content.Rows[index].Cells["serial"].Value = txt_Sr_No.Text;
                        }
                    }
                    else
                    {
                        int index = dgv_Content.Rows.Add();
                        dgv_Content.Rows[index].Cells["type"].Value = "NA";
                        dgv_Content.Rows[index].Cells["particulars"].Value = cmb_Particulars.Text.Trim();
                        dgv_Content.Rows[index].Cells["serial"].Value = "NA";
                        dgv_Content.Rows[index].Cells["item"].Value = "NA";
                        dgv_Content.Rows[index].Cells["serial"].Value = "NA";
                        dgv_Content.Rows[index].Cells["rate"].Value = txt_Rate.Text.Trim();
                        dgv_Content.Rows[index].Cells["quantity"].Value = txt_Quantity.Text.ToString().Trim();
                        dgv_Content.Rows[index].Cells["unit"].Value = cmb_Unit.Text.Trim();
                        dgv_Content.Rows[index].Cells["total"].Value = txt_Total.Text.ToString().Trim();
                        dgv_Content.Rows[index].Cells["status"].Value = cmb_Status.Text.ToString();
                    }
                    cmb_Item_No.Items.Remove(cmb_Item_No.Text);
                    clear_data();
                    calculate_Grand_Total();
                }
                else if (btn_Save.Text == "Update")
                {
                    string add_Status;
                    if (btn_Add.Text == "UPDATE")
                        add_Status = "update";
                    else
                        add_Status = "new";

                    if (cmb_Category.Text == "CYLINDER")
                    {
                        int quantity = Convert.ToInt32(txt_Quantity.Text);
                        for (int i = 0; i < quantity; i++)
                        {
                            DataTable table = new DataTable();
                            table = (DataTable)dgv_Content.DataSource;
                            DataRow row = table.NewRow();
                            set_Table(table);
                            row["Cy_Type"] = cmb_cy_Type.Text.Trim();
                            row["Particulars"] = cmb_Particulars.Text.Trim();
                            row["Rate"] = txt_Rate.Text.Trim();
                            row["Sr_No"] = txt_Sr_No.Text;
                            row["Item_No"] = cmb_Item_No.Text;
                            row["Quantity"] = "1";
                            row["Unit"] = cmb_Unit.Text.Trim();
                            row["Total"] = (Convert.ToInt32(txt_Total.Text) / quantity).ToString().Trim();
                            row["Sell_Status"] = cmb_Status.Text.Trim();
                            row["add_status"] = add_Status;
                            table.Rows.Add(row);
                            table.AcceptChanges();
                        }
                    }
                    else
                    {
                        DataTable table = new DataTable();
                        table = (DataTable)dgv_Content.DataSource;
                        DataRow row = table.NewRow();
                        set_Table(table);
                        row["Cy_Type"] = "NA";
                        row["Particulars"] = cmb_Particulars.Text.Trim();
                        row["Sr_No"] = "NA";
                        row["Item_No"] = "NA";
                        row["Rate"] = txt_Rate.Text.Trim();
                        row["Quantity"] = txt_Quantity.Text.ToString().Trim();
                        row["Unit"] = cmb_Unit.Text.Trim();
                        row["Total"] = txt_Total.Text.ToString().Trim();
                        row["Sell_Status"] = cmb_Status.Text.Trim();
                        row["add_status"] = add_Status;
                        table.Rows.Add(row);
                        table.AcceptChanges();
                    }
                    clear_data();
                    calculate_Grand_Total();
                }
                btn_Add.Text = "ADD";
            }
        }
        public void set_Table(DataTable tb)
        {
            if (tb.Columns.Contains("add_status"))
            { return; }
            else
            { tb.Columns.Add("add_status"); }
        }
        public void calculate_Grand_Total()
        {
            lbl_Total_Items.Text = dgv_Content.RowCount.ToString();
            int total_Amt = 0;
            foreach (DataGridViewRow row in dgv_Content.Rows)
            {
                total_Amt = total_Amt + Convert.ToInt32(row.Cells["total"].Value);
            }
            lbl_Total_Amt.Text = (Math.Abs(total_Amt)).ToString();
            if(txt_Paid_Amt.Text == "")
            {
                lbl_Balance.Text = lbl_Total_Amt.Text;
            }
            else
            {
                lbl_Balance.Text = (Convert.ToInt32(lbl_Total_Amt.Text) - Convert.ToInt32(txt_Paid_Amt.Text)).ToString();
            }
        }
        private void txt_Quantity_TextChanged(object sender, EventArgs e)
        {
            if(btn_Add.Text == "ADD")
            {
                if (txt_Quantity.Text != "")
                {
                    if (Convert.ToInt32(txt_Quantity.Text) > Convert.ToInt32(lbl_In_Stock.Text))
                    {
                        MessageBox.Show("Required Quantity is Not Available In Stock");
                    }
                    if (txt_Rate.Text != "")
                    {
                        calculate_total();
                    }
                    else 
                    {
                        txt_Rate.Text = "0";
                    }
                }
            }
        }
        public void calculate_total()
        {
            int rate, quantity;
           
            rate = Convert.ToInt32(txt_Rate.Text);
            quantity = Convert.ToInt32(txt_Quantity.Text);

            txt_Total.Text = (rate * quantity).ToString();
        }
        private void txt_Rate_TextChanged(object sender, EventArgs e)
        {
            if (txt_Rate.Text == "" || txt_Quantity.Text == "")
            {

            }
            else
            {
                calculate_total();
            }
        }
        private void rdb_Regular_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_Regular.Checked == true)
            {
                txt_CDC_Id.Enabled = true;
                panel6.Hide();
                if (btn_Search.Text == "Search")
                {
                    cmb_Cust_Name.Show();
                    txt_Cust_Name.Hide();
                    load_Cmb_Cust_Name();
                    panel56.Hide();
                    txt_Cust_Name.Enabled = false;
                    cmb_Cust_Name.Enabled = false;
                    txt_Contact.Enabled = false;
                    txt_Contact_Person_Name.Enabled = false;
                    txt_Address.Enabled = false;
                    txt_Deposit_Amt.Enabled = true;
                    txt_For_Days.Enabled = true;
                }
            }
            else if (rdb_Walk_In.Checked == true)
            {
                cmb_Cust_Name.Show();
                txt_Cust_Name.Hide();
            }
        }
        private void rdb_Walk_In_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_Walk_In.Checked == true)
            {
                txt_CDC_Id.Enabled = true;
                panel6.Show();
                
                if (btn_Search.Text == "Search")
                {
                    txt_Cust_Name.Show();
                    cmb_Cust_Name.Hide();
                    txt_Cust_Name.Enabled = false;
                    cmb_Cust_Name.Enabled = false;
                    dt_Sell_Date.Enabled = true;
                    txt_Deposit_Amt.Enabled = true;
                    txt_For_Days.Enabled = true;
                    txt_Address.Enabled = true;
                    txt_Contact.Enabled = true;
                    txt_Contact_Person_Name.Enabled = true;
                }
            }
            else if (rdb_Regular.Checked == true)
            {
                if (btn_Search.Text == "Search")
                {    
                    cmb_Cust_Name.Hide();
                    txt_Cust_Name.Show();
                }
            }
        }
        public void load_Cmb_Cust_Name()
        {
            cmb_Cust_Name.Items.Clear();
            try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT DISTINCT Cust_Name FROM Tb_Customer_Master WHERE Cust_Status = 'REGULAR'", con);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    this.cmb_Cust_Name.Items.Add(sdr[0]);
                }
                sdr.Close();
            }
            catch { }
            finally { con.Close(); }
        }
        private void cmb_Cust_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_Cust_Details();
            txt_Cust_Name.Text = cmb_Cust_Name.Text;
            /*try
            {
                cmb_CDC_Id.Items.Clear();
                getconnection();
                con.Open();

                cmd = new SqlCommand("SELECT DISTINCT C_Dc_No FROM Tb_CDC_Details WHERE C_Name = '" + txt_Cust_Name.Text.ToString() + "' AND Cancel_Status = 'NA'", con);
                sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    if (sdr[0].ToString() != "NA")
                    {
                        cmb_CDC_Id.Items.Add(sdr[0]);
                    }
                }
                sdr.Close();
            }
            catch { }
            finally { con.Close(); }*/
            txt_Cust_Name.Text = cmb_Cust_Name.Text.ToString();
        }
        public void load_Cust_Details()
        {
            try
            {
                getconnection();
                con.Open();
                string Cust_Name = cmb_Cust_Name.Text.ToString();
                cmd = new SqlCommand("SELECT Cust_Address, Cust_PhoneNo, Cust_CPerson, CP_PhoneNo, Cust_Status FROM Tb_Customer_Master WHERE Cust_Name = '" + Cust_Name + "'", con);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    txt_Address.Text = sdr[0].ToString();
                    txt_Contact.Text = sdr[1].ToString();
                    txt_Contact_Person_Name.Text = sdr[2].ToString();
                    if(sdr[4].ToString() == "Regular")
                    {
                        rdb_Regular.Checked = true;
                    }
                    else if(sdr[4].ToString() == "Walk In")
                    {
                        rdb_Walk_In.Checked = true;
                    }
                }
                sdr.Close();
            }
            catch { }
            finally { con.Close(); }
        }
        //public void load_CDC_Id()
        //{
        //    calculate_CDC_Id();
        //    id_Month = DateTime.Now.ToString("MMM");
        //    id_Year = DateTime.Now.ToString("yyyy");
        //    cdc_id = id_Month + "-" + id_Year + "-" + dc_Id;
        //    txt_CDC_Id.Text = cdc_id;
        //    lbl_CDC_Print.Text = cdc_id;
        //}
        private void button2_Click(object sender, EventArgs e)
        {
            online = false;
            if (txt_Cust_Name.Text == "")
            {
                MessageBox.Show("Please Enter Customer Name");
            }
            else if (txt_CDC_Id.Text == "")
            {
                MessageBox.Show("Please Enter Delivery Challan No");
            }
            else if(cmb_Vehicle_No.Text=="")
            {
                MessageBox.Show("Please Enter Vehicle Number");
            }
            else if (btn_Save.Text == "Save")
            {
                if (rdb_Walk_In.Checked == true)
                {
                    try
                    {
                        if (txt_Contact.Text == "")
                            txt_Contact.Text = "NA";
                        if (txt_Contact_Person_Name.Text == "")
                            txt_Contact_Person_Name.Text = "NA";
                        getconnection();
                        con.Open();
                        cmd = new SqlCommand("Insert_Customer_Master", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@C_InDate", dt_Sell_Date.Text.Trim());
                        cmd.Parameters.AddWithValue("@Cust_Name", txt_Cust_Name.Text.Trim());
                        cmd.Parameters.AddWithValue("@Cust_Address", txt_Address.Text.Trim());
                        cmd.Parameters.AddWithValue("@Cust_PhoneNo", txt_Contact.Text.Trim());
                        cmd.Parameters.AddWithValue("@Cust_GSTNo", "NA");
                        cmd.Parameters.AddWithValue("@Cust_CPerson", txt_Contact_Person_Name.Text.Trim());
                        cmd.Parameters.AddWithValue("@CP_PhoneNo", txt_Contact.Text.ToString());
                        cmd.Parameters.AddWithValue("@Cust_Status", "Walk In");
                        cmd.ExecuteNonQuery();
                    }
                    catch { }
                }
                // try
                {
                    if(txt_Deposit_Amt.Text == "")
                    {
                        txt_Deposit_Amt.Text = "NA";
                    }
                    if (txt_For_Days.Text == "")
                    {
                        txt_For_Days.Text = "NA";
                    }
                    if (txt_Deposit_Return.Text == "")
                    {
                        txt_Deposit_Return.Text = "NA";
                    }
                    if (cmb_Reason.Text == "")
                    {
                        cmb_Reason.Text = "NA";
                    }
                    if(cmb_Reference.Text == "")
                    {
                        cmb_Reference.Text = "NA";
                    }
                    if (txt_Remark.Text == "")
                    {
                        txt_Remark.Text = "NA";
                    }
                    if (txt_Paid_Amt.Text == "")
                    {
                        txt_Paid_Amt.Text = "0";
                    }
                    getconnection();
                    con.Open();
                    cmd = new SqlCommand("Insert_Cust_DC_Details", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@dc_no", txt_CDC_Id.Text.ToString());
                    cmd.Parameters.AddWithValue("@dc_date", dt_Sell_Date.Text.ToString());
                    cmd.Parameters.AddWithValue("@cust_name", txt_Cust_Name.Text.ToString());
                    cmd.Parameters.AddWithValue("@total_amt", lbl_Total_Amt.Text.ToString());
                    cmd.Parameters.AddWithValue("@paid_amt", txt_Paid_Amt.Text.ToString());
                    cmd.Parameters.AddWithValue("@remark", txt_Remark.Text.ToString());
                    cmd.Parameters.AddWithValue("@total_items", lbl_Total_Items.Text.ToString());
                    cmd.Parameters.AddWithValue("@balance_amt", lbl_Balance.Text.ToString());
                    cmd.Parameters.AddWithValue("@pay_mode", pay_mode);
                    cmd.Parameters.AddWithValue("@reference", cmb_Reference.Text.ToString());
                    cmd.Parameters.AddWithValue("@deposit_Amt", txt_Deposit_Amt.Text.ToString());
                    cmd.Parameters.AddWithValue("@deposit_Return", txt_Deposit_Return.Text.ToString());
                    cmd.Parameters.AddWithValue("@for_Days", txt_For_Days.Text.ToString());
                    cmd.Parameters.AddWithValue("@reason", cmb_Reason.Text.ToString());
                    cmd.Parameters.AddWithValue("@cancel_status", "NA");
                    cmd.Parameters.AddWithValue("@Tax_Status", "NA"); 
                    cmd.Parameters.AddWithValue("@C_ID", dc_Id); 
                    cmd.Parameters.AddWithValue("@Vehicle_No", cmb_Vehicle_No.Text.ToString());
                    cmd.ExecuteNonQuery();
                    foreach (DataGridViewRow row in dgv_Content.Rows)
                    {
                        cmd = new SqlCommand("Insert_Cust_DC_Content", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@dc_no", txt_CDC_Id.Text.ToString());
                        cmd.Parameters.AddWithValue("@cy_type", row.Cells["type"].Value.ToString().Trim());
                        cmd.Parameters.AddWithValue("@particular", row.Cells["particulars"].Value.ToString().Trim());
                        cmd.Parameters.AddWithValue("@sr_no", row.Cells["serial"].Value.ToString().Trim());
                        cmd.Parameters.AddWithValue("@item_no", row.Cells["item"].Value.ToString().Trim());
                        cmd.Parameters.AddWithValue("@quantity", row.Cells["quantity"].Value.ToString().Trim());
                        cmd.Parameters.AddWithValue("@rate", row.Cells["rate"].Value.ToString().Trim());
                        cmd.Parameters.AddWithValue("@unit", row.Cells["unit"].Value.ToString().Trim());
                        cmd.Parameters.AddWithValue("@total", row.Cells["total"].Value.ToString().Trim());
                        cmd.Parameters.AddWithValue("@sell_status", row.Cells["status"].Value.ToString().Trim());
                        cmd.Parameters.AddWithValue("@received_status", "Pending");
                        cmd.ExecuteNonQuery();
                        string particular = row.Cells["particulars"].Value.ToString();
                        calculate_In_Stock_Value(particular);
                        int quantity = Convert.ToInt32(row.Cells["quantity"].Value);
                        cmd = new SqlCommand("UPDATE Tb_Inventory_Master SET In_Stock = '" + (in_Stock_Value - quantity).ToString() + "', Out_Stock = '" + (out_Stock_Value + quantity) + "' WHERE Particulars = '" + particular + "'", con);
                        cmd.ExecuteNonQuery();
                        cmd = new SqlCommand("UPDATE Tb_Purchase_Content_Master SET Cylinder_Status = 'SENT TO CUSTOMER', Cust_Supp_Name = '" + cmb_Cust_Name.Text + "' WHERE Particulers = '" + particular + "' AND Part_No = '" + row.Cells["item"].Value.ToString() + "'", con);
                        cmd.ExecuteNonQuery();
                    }
                }
                //  catch { }
                 MessageBox.Show("Data Saved SuccessFully.","Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //if(result == DialogResult.Yes)
                //{
                //    //btn_Print.PerformClick();
                //    Report_CDC form = new Report_CDC();
                //    form.get_ID(lbl_CDC_Print.Text);
                //    form.Show();
                //    clear_All_Data();
                //  //  load_CDC_Id();
                //}
                //else
                //{
                    clear_All_Data();
                //    load_CDC_Id();
               // }
                //btn_Print.Show();
            }
            else if (btn_Save.Text == "Update")
            {
                // try
                {
                    if (txt_Deposit_Amt.Text == "")
                    {
                        txt_Deposit_Amt.Text = "NA";
                    }
                    if (txt_For_Days.Text == "")
                    {
                        txt_For_Days.Text = "NA";
                    }
                    if (txt_Deposit_Return.Text == "")
                    {
                        txt_Deposit_Return.Text = "NA";
                    }
                    if (cmb_Reason.Text == "")
                    {
                        cmb_Reason.Text = "NA";
                    }
                    if (cmb_Reference.Text == "")
                    {
                        cmb_Reference.Text = "NA";
                    }
                    if (txt_Remark.Text == "")
                    {
                        txt_Remark.Text = "NA";
                    }
                    if (txt_Paid_Amt.Text == "")
                    {
                        txt_Paid_Amt.Text = "0";
                    }
                    getconnection();
                    con.Open();
                    cmd = new SqlCommand("Update_Cust_DC_Details", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@dc_date", dt_Sell_Date.Text.ToString());
                    cmd.Parameters.AddWithValue("@dc_no", txt_CDC_Id.Text.ToString().Trim());
                    cmd.Parameters.AddWithValue("@cust_name", txt_Cust_Name.Text.ToString());
                    cmd.Parameters.AddWithValue("@total_amt", lbl_Total_Amt.Text.ToString());
                    cmd.Parameters.AddWithValue("@paid_amt", txt_Paid_Amt_2.Text.ToString());
                    cmd.Parameters.AddWithValue("@remark", txt_Remark.Text.ToString());
                    cmd.Parameters.AddWithValue("@total_items", lbl_Total_Items.Text.ToString());
                    cmd.Parameters.AddWithValue("@balance_amt", lbl_Balance.Text.ToString());
                    cmd.Parameters.AddWithValue("@pay_mode", pay_mode);
                    cmd.Parameters.AddWithValue("@reference", cmb_Reference.Text.ToString());
                    cmd.Parameters.AddWithValue("@deposit_Amt", txt_Deposit_Amt.Text.ToString());
                    cmd.Parameters.AddWithValue("@deposit_Return", txt_Deposit_Return.Text.ToString());
                    cmd.Parameters.AddWithValue("@for_Days", txt_For_Days.Text.ToString());
                    cmd.Parameters.AddWithValue("@reason", cmb_Reason.Text.ToString());
                    cmd.Parameters.AddWithValue("@Vehicle_No", cmb_Vehicle_No.Text.ToString());
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
                foreach (DataGridViewRow row in dgv_Content.Rows)
                {
                    if (row.Cells["add_status"].Value == null)
                    {
                        //No Change
                    }
                    else if (row.Cells["add_status"].Value.ToString() == "new")
                    {
                        cmd = new SqlCommand("Insert_Cust_DC_Content", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@dc_no", txt_CDC_Id.Text.ToString());
                        cmd.Parameters.AddWithValue("@cy_type", row.Cells["type"].Value.ToString().Trim());
                        cmd.Parameters.AddWithValue("@particular", row.Cells["particulars"].Value.ToString().Trim());
                        cmd.Parameters.AddWithValue("@sr_no", row.Cells["serial"].Value.ToString().Trim());
                        cmd.Parameters.AddWithValue("@item_no", row.Cells["item"].Value.ToString().Trim());
                        cmd.Parameters.AddWithValue("@quantity", row.Cells["quantity"].Value.ToString().Trim());
                        cmd.Parameters.AddWithValue("@rate", row.Cells["rate"].Value.ToString().Trim());
                        cmd.Parameters.AddWithValue("@unit", row.Cells["unit"].Value.ToString().Trim());
                        cmd.Parameters.AddWithValue("@total", row.Cells["total"].Value.ToString().Trim());
                        cmd.Parameters.AddWithValue("@sell_status", row.Cells["status"].Value.ToString().Trim());
                        cmd.Parameters.AddWithValue("@received_status", "Pending");
                        cmd.ExecuteNonQuery();
                        cmd = new SqlCommand("UPDATE Tb_Inventory_Master SET In_Stock = In_Stock - " + Convert.ToInt32(row.Cells["quantity"].Value) + ",Out_Stock = Out_Stock + " + Convert.ToInt32(row.Cells["quantity"].Value) + " WHERE Particulars = '" + row.Cells["particulars"].Value.ToString() + "'", con);
                        cmd.ExecuteNonQuery();
                    }
                    else if (row.Cells["add_status"].Value.ToString() == "update")
                    {
                        cmd = new SqlCommand("Update_Cust_DC_Content", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@dc_no", txt_CDC_Id.Text.ToString());
                        cmd.Parameters.AddWithValue("@cy_type", row.Cells["type"].Value.ToString().Trim());
                        cmd.Parameters.AddWithValue("@particular", row.Cells["particulars"].Value.ToString().Trim());
                        cmd.Parameters.AddWithValue("@sr_no", row.Cells["serial"].Value.ToString().Trim());
                        cmd.Parameters.AddWithValue("@item_no", row.Cells["item"].Value.ToString().Trim());
                        cmd.Parameters.AddWithValue("@quantity", row.Cells["quantity"].Value.ToString().Trim());
                        cmd.Parameters.AddWithValue("@rate", row.Cells["rate"].Value.ToString().Trim());
                        cmd.Parameters.AddWithValue("@unit", row.Cells["unit"].Value.ToString().Trim());
                        cmd.Parameters.AddWithValue("@total", row.Cells["total"].Value.ToString().Trim());
                        cmd.Parameters.AddWithValue("@sell_status", row.Cells["status"].Value.ToString().Trim());
                        cmd.Parameters.AddWithValue("@received_status", "Pending");
                        cmd.Parameters.AddWithValue("@cust_dc_no", "NA");
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Data Updated SuccessFully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //if (result == DialogResult.Yes)
                //{
                //    //btn_Print.PerformClick();
                //    Report_CDC form = new Report_CDC();
                //    form.get_ID(lbl_CDC_Print.Text);
                //    form.Show();
                //    clear_All_Data();
                //}
                //else
                //{
                   clear_All_Data();
               // }
                //btn_Print.Show();
            }
        }
        public void clear_All_Data()
        {
            cmb_Vehicle_No.ResetText();
            txt_CDC_Id.Clear();
            cmb_CDC_Id.ResetText();
            txt_Cust_Name.Clear();
            cmb_Cust_Name.ResetText();
            txt_Address.Clear();
            txt_Contact.Clear();
            txt_Contact_Person_Name.Clear();
            txt_Deposit_Amt.Clear();
            txt_Deposit_Return.Clear();
            txt_For_Days.Clear();
            cmb_Reason.ResetText();
            cmb_Category.ResetText();
            rdb_Regular.Checked = false;
            rdb_Walk_In.Checked = false;
            cmb_cy_Type.ResetText();
            txt_Sr_No.Clear();
            lbl_Total_Amt.ResetText();
            lbl_Total_Items.ResetText();
            txt_Paid_Amt.Clear();
            txt_Paid_Amt_2.Clear();
            rdbtn_Cash.Checked = false;
            rdbtn_Other.Checked = false;
            cmb_Reference.ResetText();
            txt_Remark.Clear();
            lbl_Balance.Text = "0";
            lbl_In_Stock.Text = "0";
            if(btn_Save.Text == "Save")
            {
                if (dgv_Content.RowCount > 0)
                    dgv_Content.Rows.Clear();
            }
            else
            {
                if (dgv_Content.RowCount > 0)
                {
                    DataTable DT = (DataTable)dgv_Content.DataSource;
                    if (DT != null)
                        DT.Clear();
                }
            }
            txt_CDC_Id.Clear();
        }
        public void calculate_In_Stock_Value(string particular)
        {
            {
                getconnection();
                con.Open();
                SqlCommand command = new SqlCommand("SELECT In_Stock, Out_Stock FROM Tb_Inventory_Master WHERE Particulars = '" + particular + "'", con);
                sdr = command.ExecuteReader();
                while (sdr.Read())
                {
                    in_Stock_Value = Convert.ToInt32(sdr[0]);
                    out_Stock_Value = Convert.ToInt32(sdr[1]);
                }
                sdr.Close();
            }
        }
        private void cmb_Particulars_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_In_Stock_Value();
        }
        public void load_In_Stock_Value()
        {
            if(btn_Add.Text == "ADD")
            {
                getconnection();
                con.Open();
                if (cmb_Category.Text == "CYLINDER")
                {
                    cmd = new SqlCommand("select count(Cylinder_Status) from Tb_Purchase_Content_Master where Particulers = '" + cmb_Particulars.Text.ToString() + "' AND Cylinder_Status IN ('FILL')", con);
                }
                else if (cmb_Category.Text == "MATERIAL")
                {
                    cmd = new SqlCommand("select In_Stock from Tb_Inventory_Master where Particulars = '" + cmb_Particulars.Text.ToString() + "'", con);
                }
                try
                {
                    sdr = cmd.ExecuteReader();
                    sdr.Read();
                    lbl_In_Stock.Text = sdr[0].ToString();
                    sdr.Close();
                }
                catch { }
            }
        }
        private void dgv_Content_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string value = dgv_Content.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            if(btn_Save.Text == "Save")
            {
                if (dgv_Content.Columns["edit"].Index == e.ColumnIndex)
                {
                    if (btn_Add.Text == "UPDATE")
                    {
                        MessageBox.Show("Cannot Add Record While Another is in the Process", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        btn_Add.Text = "UPDATE";
                        DataGridViewRow row = dgv_Content.Rows[e.RowIndex];
                        if (row.Cells["item"].Value.ToString() != "NA")
                        {
                            cmb_Category.Text = "CYLINDER";
                        }
                        else
                        {
                            cmb_Category.Text = "MATERIAL";
                        }
                        cmb_cy_Type.Text = row.Cells["type"].Value.ToString();
                        cmb_Particulars.Text = row.Cells["particulars"].Value.ToString();
                        txt_Quantity.Text = row.Cells["quantity"].Value.ToString();
                        txt_Quantity.Enabled = false;
                        update_quantity = Convert.ToInt32(txt_Quantity.Text);
                        txt_Rate.Text = row.Cells["rate"].Value.ToString();
                        cmb_Unit.Text = row.Cells["unit"].Value.ToString();
                        cmb_Status.Text = row.Cells["status"].Value.ToString();
                        txt_Total.Text = row.Cells["total"].Value.ToString();
                        cmb_Item_No.Text = row.Cells["item"].Value.ToString();
                        txt_Sr_No.Text = row.Cells["serial"].Value.ToString();
                        dgv_Content.Rows.RemoveAt(e.RowIndex);
                        table_panel_Entry.Enabled = true;
                    }
                }
                else if (dgv_Content.Columns["remove"].Index == e.ColumnIndex)
                {
                    DialogResult result = MessageBox.Show("Are You Sure.?", "Confirmation", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        getconnection();
                        con.Open();
                        cmb_Item_No.Items.Add(dgv_Content.Rows[e.RowIndex].Cells["item"].Value.ToString());
                        dgv_Content.Rows.RemoveAt(e.RowIndex);
                        calculate_Grand_Total();
                    }
                }
            }
            else if (btn_Save.Text == "Update")
            {
                if (dgv_Content.Columns["edit"].Index == e.ColumnIndex)
                {
                    if (btn_Add.Text == "UPDATE")
                    {
                        MessageBox.Show("Cannot Add Record While Another is in the Process", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        btn_Add.Text = "UPDATE";
                        DataGridViewRow row = dgv_Content.Rows[e.RowIndex];
                        if (row.Cells["item"].Value.ToString() != "NA")
                        {
                            cmb_Category.Text = "CYLINDER";
                        }
                        else
                        {
                            cmb_Category.Text = "MATERIAL";
                        }
                        cmb_cy_Type.Text = row.Cells["type"].Value.ToString();
                        cmb_Particulars.Text = row.Cells["particulars"].Value.ToString();
                        txt_Quantity.Text = row.Cells["quantity"].Value.ToString();
                        txt_Quantity.Enabled = false;
                        update_quantity = Convert.ToInt32(txt_Quantity.Text);
                        txt_Rate.Text = row.Cells["rate"].Value.ToString();
                        cmb_Unit.Text = row.Cells["unit"].Value.ToString();
                        cmb_Status.Text = row.Cells["status"].Value.ToString();
                        txt_Total.Text = row.Cells["total"].Value.ToString();
                        cmb_Item_No.Text = row.Cells["item"].Value.ToString();
                        txt_Sr_No.Text = row.Cells["serial"].Value.ToString();
                        cmb_Item_No.Enabled = false;
                        txt_Sr_No.Text = row.Cells["serial"].Value.ToString();
                        dgv_Content.Rows.RemoveAt(e.RowIndex);
                        table_panel_Entry.Enabled = true;
                    }
                }
                else if (dgv_Content.Columns["remove"].Index == e.ColumnIndex)
                {
                    DialogResult result = MessageBox.Show("Are You Sure.?", "Confirmation", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        if(dgv_Content.Rows[e.RowIndex].Cells["add_status"].Value == null)
                        {
                            //No Change
                            int update_Instock = Convert.ToInt32(dgv_Content.Rows[e.RowIndex].Cells["quantity"].Value);
                            getconnection();
                            con.Open();
                            cmd = new SqlCommand("UPDATE Tb_Inventory_Master SET In_Stock = In_Stock + " + update_Instock + ", Out_Stock = Out_Stock - " + update_Instock + " WHERE Particulars = '" + dgv_Content.Rows[e.RowIndex].Cells["particulars"].Value.ToString() + "'", con);
                            cmd.ExecuteNonQuery();
                            cmd = new SqlCommand("UPDATE Tb_Purchase_Content_Master SET Cylinder_Status = 'EMPTY', Cust_Supp_Name = 'NA' WHERE Part_No = '" + dgv_Content.Rows[e.RowIndex].Cells["item"].Value.ToString() + "' AND Particulers = '" + dgv_Content.Rows[e.RowIndex].Cells["particulars"].Value.ToString() + "'", con);
                            cmd.ExecuteNonQuery();
                            cmd = new SqlCommand("DELETE FROM Tb_CDC_Content_Details WHERE C_Dc_No = '" + cmb_CDC_Id.Text.ToString() + "' AND Item_No ='" + dgv_Content.Rows[e.RowIndex].Cells["item"].Value.ToString() + "'", con);
                            cmd.ExecuteNonQuery();
                            cmb_Item_No.Items.Add(dgv_Content.Rows[e.RowIndex].Cells["item"].Value.ToString());
                            dgv_Content.Rows.RemoveAt(e.RowIndex);
                            calculate_Grand_Total();
                        }
                        else if (dgv_Content.Rows[e.RowIndex].Cells["add_status"].Value.ToString() == "")
                        {
                            //No Change
                            int update_Instock = Convert.ToInt32(dgv_Content.Rows[e.RowIndex].Cells["quantity"].Value);
                            getconnection();
                            con.Open();
                            cmd = new SqlCommand("UPDATE Tb_Inventory_Master SET In_Stock = In_Stock + " + update_Instock + ", Out_Stock = Out_Stock - " + update_Instock + " WHERE Particulars = '" + dgv_Content.Rows[e.RowIndex].Cells["particulars"].Value.ToString() + "'", con);
                            cmd.ExecuteNonQuery();
                            cmd = new SqlCommand("UPDATE Tb_Purchase_Content_Master SET Cylinder_Status = 'EMPTY', Cust_Supp_Name = 'NA' WHERE Part_No = '" + dgv_Content.Rows[e.RowIndex].Cells["item"].Value.ToString() + "' AND Particulers = '" + dgv_Content.Rows[e.RowIndex].Cells["particulars"].Value.ToString() + "'", con);
                            cmd.ExecuteNonQuery();
                            cmd = new SqlCommand("DELETE FROM Tb_CDC_Content_Details WHERE C_Dc_No = '" + cmb_CDC_Id.Text.ToString() + "' AND Item_No ='" + dgv_Content.Rows[e.RowIndex].Cells["item"].Value.ToString() + "'", con);
                            cmd.ExecuteNonQuery();
                            cmb_Item_No.Items.Add(dgv_Content.Rows[e.RowIndex].Cells["item"].Value.ToString());
                            dgv_Content.Rows.RemoveAt(e.RowIndex);
                            calculate_Grand_Total();
                        }
                        else if (dgv_Content.Rows[e.RowIndex].Cells["add_status"].Value.ToString() == "new")
                        {
                            dgv_Content.Rows.RemoveAt(e.RowIndex);
                            calculate_Grand_Total();
                        }
                        else if(dgv_Content.Rows[e.RowIndex].Cells["add_status"].Value.ToString() == "update")
                        {
                            int update_Instock = Convert.ToInt32(dgv_Content.Rows[e.RowIndex].Cells["quantity"].Value);
                            getconnection();
                            con.Open();
                            cmd = new SqlCommand("UPDATE Tb_Inventory_Master SET In_Stock = In_Stock + " + update_Instock + ", Out_Stock = Out_Stock - " + update_Instock + " WHERE Particulars = '" + dgv_Content.Rows[e.RowIndex].Cells["particulars"].Value.ToString() + "'", con);
                            cmd.ExecuteNonQuery();
                            cmd = new SqlCommand("UPDATE Tb_Purchase_Content_Master SET Cylinder_Status = 'EMPTY', Cust_Supp_Name = 'NA' WHERE Part_No = '" + dgv_Content.Rows[e.RowIndex].Cells["item"].Value.ToString() + "' AND Particulers = '" + dgv_Content.Rows[e.RowIndex].Cells["particulars"].Value + "'", con);
                            cmd.ExecuteNonQuery();
                            cmd = new SqlCommand("DELETE FROM Tb_CDC_Content_Details WHERE C_Dc_No = '" + cmb_CDC_Id.Text.ToString() + "' AND Item_No ='" + dgv_Content.Rows[e.RowIndex].Cells["item"].Value.ToString() + "'", con);
                            cmd.ExecuteNonQuery();
                            cmb_Item_No.Items.Add(dgv_Content.Rows[e.RowIndex].Cells["item"].Value.ToString());
                            dgv_Content.Rows.RemoveAt(e.RowIndex);
                            calculate_Grand_Total();
                        }     
                    }
                }
            }
           /* else if (value == "SUBMIT")
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT Sr_No FROM Tb_Purchase_Content_Master WHERE Part_No = '" + dgv_Content.Rows[e.RowIndex].Cells["item"].Value.ToString() + "'", con);
                sdr = cmd.ExecuteReader();

                if(sdr.Read())
                {
                    dgv_Content.Rows[e.RowIndex].Cells["serial"].Value = sdr[0].ToString();
                }
                else
                {
                    MessageBox.Show("Cylinder Of Given Item No. is Not Found!");
                }
            }*/
        }
        private void chkbox_Add_New_Product_CheckedChanged(object sender, EventArgs e)
        {
            if(chkbox_Add_New_Product.Checked == true)
            {
                table_panel_Entry.Enabled = true;
            }
            else
            {
                table_panel_Entry.Enabled = false;
            }
        }
        private void txt_Paid_Amt_TextChanged(object sender, EventArgs e)
        {
            if (txt_Paid_Amt.Text != "")
            {
                decimal balance = Convert.ToDecimal(lbl_Total_Amt.Text) - Convert.ToDecimal(txt_Paid_Amt.Text);
                if (balance < 0)
                {
                    MessageBox.Show("Please Enter Valid Amount.");
                }
                else
                {
                    lbl_Balance.Text = (Convert.ToDecimal(lbl_Total_Amt.Text) - Convert.ToDecimal(txt_Paid_Amt.Text)).ToString();
                }
            }
            else
            {
                //txt_Paid_Amt.Text = "0";
                lbl_Balance.Text = lbl_Total_Amt.Text;
                panel48.Hide();
            }
        }
        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            panel48.Show();
            cmb_Reference.Text = "";
            panel48.Show();
            pay_mode = "OTHER";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            clear_All_Data();
            if(btn_Search.Text == "Search")
            {
                chk_If_Cancel.Enabled = true;
                chkbox_Add_New_Product.Enabled = true;
                btn_Search.Text = "Cancel";
               
                panel56.Show();
                panel56.Show();
                txt_CDC_Id.Hide();
                cmb_CDC_Id.Show();
                txt_Cust_Name.Hide();
                cmb_Cust_Name.Show();
                chk_If_Cancel.Show();
                chkbox_Add_New_Product.Show();
                //txt_Paid_Amt_2.Show();
                panel37.Show();
                panel38.Show();
                lbl_Total_Amt.Show();
                btn_Save.Text = "Update";
                make_Enable_True_False(false);
                load_Cust_Combo();
                load_CDC_Id_Combo();
                table_panel_Entry.Enabled = false;
            }
            else if(btn_Search.Text == "Cancel")
            {
                btn_Search.Text = "Search";
                txt_Paid_Amt_2.Hide(); ;
               
                panel56.Hide();
                txt_CDC_Id.Show();
                cmb_CDC_Id.Hide();
                txt_Cust_Name.Show();
                cmb_Cust_Name.Hide();
                chk_If_Cancel.Hide();
                chkbox_Add_New_Product.Hide();
                panel37.Hide();
                panel38.Hide();
                //make_Enable_True_False(true);
                rdb_Regular.Enabled = true;
                rdb_Walk_In.Enabled = true;
                chkbox_Add_New_Product.Checked = false;
             //   load_CDC_Id();
                if (dgv_Content.RowCount > 1)
                {
                    dgv_Content.DataSource = null;
                }                
                btn_Save.Text = "Save";
                lbl_CDC_Print.Text = txt_CDC_Id.Text;
                table_panel_Entry.Enabled = true;
            }
           // search_Cdc_Id();
        }
        public void make_Enable_True_False(Boolean value)
        {
            txt_Contact_Person_Name.Enabled = value;
            txt_Contact.Enabled = value;
            rdb_Regular.Enabled = value;
            rdb_Walk_In.Enabled = value;
            txt_Address.Enabled = value;
        }
        public void load_Cust_Combo()
        {
            cmb_Cust_Name.Items.Clear();
            try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT DISTINCT C_Name FROM Tb_CDC_Details", con);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    if (sdr[0].ToString() != "NA")
                    {
                        cmb_Cust_Name.Items.Add(sdr[0]);
                    }
                }
                sdr.Close();
            }
            catch { }
            finally { con.Close(); }
        }
        public void load_CDC_Id_Combo()
        {
            cmb_CDC_Id.Items.Clear();
           // try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT DISTINCT C_Dc_No FROM Tb_CDC_Details WHERE Status = 'NA'", con);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    if (sdr[0].ToString() != "NA")
                    {
                        cmb_CDC_Id.Items.Add(sdr[0]);
                    }
                }
                sdr.Close();
            }
            //catch { }
            //finally { con.Close(); }
        }
        private void chk_If_Cancel_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_If_Cancel.Checked == true)
            {
                if (txt_CDC_Id.Text == "")
                {
                    chkbox_Add_New_Product.Enabled = true;
                    chkbox_Add_New_Product.Show();
                    MessageBox.Show("Select CDC ID First.!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    chk_If_Cancel.Checked = false;
                }
                else
                {
                    chkbox_Add_New_Product.Hide();
                    DialogResult result = MessageBox.Show("Are You Sure?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        getconnection();
                        con.Open();
                        cmd = new SqlCommand("UPDATE Tb_CDC_Details SET Status = 'Cancel' WHERE C_Dc_No = '" + txt_CDC_Id.Text.ToString() + "'", con);
                        cmd.ExecuteNonQuery();
                        cmd = new SqlCommand("UPDATE Tb_CDC_Content_Details SET Receive_Status = 'Cancel' WHERE C_Dc_No = '" + txt_CDC_Id.Text.ToString() + "'", con);
                        cmd.ExecuteNonQuery();
                        foreach (DataGridViewRow row in dgv_Content.Rows)
                        {
                            cmd = new SqlCommand("UPDATE Tb_Inventory_Master SET In_Stock = In_Stock + " + row.Cells["quantity"].Value.ToString() + ", Out_Stock = Out_Stock - " + row.Cells["quantity"].Value.ToString() + " WHERE Particulars = '" + row.Cells["particulars"].Value.ToString() + "'", con);
                            cmd.ExecuteNonQuery();
                            cmd = new SqlCommand("UPDATE Tb_Purchase_Content_Master SET Cylinder_Status = 'FILL', Cust_Supp_Name = 'NA' WHERE Particulers = '" + row.Cells["particulars"].Value.ToString() + "' AND Part_No = '" + row.Cells["item"].Value.ToString() + "'", con);
                            cmd.ExecuteNonQuery();
                        }
                        btn_Save.PerformClick();
                    }
                    else if (result == DialogResult.No)
                    {
                        chk_If_Cancel.Checked = false;
                    }
                }
            }
        }
        private void cmb_cy_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            //load_Particular();
        }
        private void txt_Remark_TextChanged(object sender, EventArgs e)
        {

        }
        private void panel24_Paint(object sender, PaintEventArgs e)
        {

        }
        private void cmb_Reason_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        private void cmb_Unit_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void panel39_Paint(object sender, PaintEventArgs e)
        {

        }
        private void panel12_Paint(object sender, PaintEventArgs e)
        {

        }
        private void label6_Click(object sender, EventArgs e)
        {

        }
        private void panel44_Paint(object sender, PaintEventArgs e)
        {

        }
        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
        private void panel48_Paint(object sender, PaintEventArgs e)
        {

        }
        private void lbl_Balance_Click(object sender, EventArgs e)
        {

        }
        private void panel19_Paint(object sender, PaintEventArgs e)
        {

        }
        private void cmb_Reference_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void label28_Click(object sender, EventArgs e)
        {

        }
        private void txt_Contact_TextChanged(object sender, EventArgs e)
        {

        }
        private void lblPANNumber_Click(object sender, EventArgs e)
        {

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void txt_CDC_Id_TextChanged(object sender, EventArgs e)
        {
            if(txt_CDC_Id.Text=="")
            {
                txt_Cust_Name.Enabled = false;
                cmb_Cust_Name.Enabled = false;
            }
            else
            {
                txt_Cust_Name.Enabled = true;
                cmb_Cust_Name.Enabled = true;
            }
        }
        private void dt_Sell_Date_ValueChanged(object sender, EventArgs e)
        {

        }
        private void lbl_Pur_Date_Click(object sender, EventArgs e)
        {

        }
        private void panel56_Paint(object sender, PaintEventArgs e)
        {

        }
        private void cmb_search_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void cmb_Status_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
        private void panel40_Paint(object sender, PaintEventArgs e)
        {

        }
        private void panel14_Paint(object sender, PaintEventArgs e)
        {

        }
        private void panel49_Paint(object sender, PaintEventArgs e)
        {

        }
        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
        private void panel42_Paint(object sender, PaintEventArgs e)
        {

        }
        private void cmb_Item_No_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmb_Item_No.Text != "")
            {
                cmb_cy_Type.Enabled = false;
                cmb_Particulars.Enabled = false;
                txt_Quantity.Enabled = false;
                txt_Sr_No.Enabled = false;
               
               
            }
            else
            {
                cmb_cy_Type.Enabled = true;
                cmb_Particulars.Enabled = true;
            }
            //try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT Cylinder_Type, Particulers, Sr_No FROM Tb_Purchase_Content_Master WHERE Part_No = '" + cmb_Item_No.Text.ToString() + "'", con);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    cmb_cy_Type.Text = sdr[0].ToString();
                    cmb_Particulars.Text = sdr[1].ToString();
                    txt_Sr_No.Text = sdr[2].ToString();
                }
                sdr.Close();
                load_In_Stock_Value();
                txt_Quantity.Text = "1";
                cmb_Unit.Text = "NOS";
                cmb_Status.Text = "RENT";
            }
            //catch { }
            //finally { con.Close(); }
        }
        private void btn_Print_Click(object sender, EventArgs e)
        {
            Report_CDC form = new Report_CDC();
            form.get_ID(lbl_CDC_Print.Text);
            form.Show();
            clear_All_Data();
        }
        private void cmb_CDC_Id_SelectedIndexChanged(object sender, EventArgs e)
        {
            search_Cdc_Id(cmb_CDC_Id.Text.ToString());
            lbl_CDC_Print.Text = cmb_CDC_Id.Text;
        }
        private void panel37_Paint(object sender, PaintEventArgs e)
        {

        }
        private void Customer_DC_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void cmb_Item_No_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void txt_Paid_Amt_2_TextChanged(object sender, EventArgs e)
        {
            if (txt_Paid_Amt_2.Text != "")
            {
                lbl_Balance.Text = (balance - Convert.ToInt32(txt_Paid_Amt_2.Text)).ToString();
           
                panel19.Show();
            }
            else
            {
                panel48.Hide();
                panel19.Hide();
            }
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            panel48.Hide();
            pay_mode = "CASH";
            cmb_Reference.Text = "NA";
        }
        public void calculate_CDC_Id()
        {
            try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT MAX(C_ID) FROM Tb_CDC_Details", con);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    string val = sdr[0].ToString();
                    if (val == "")
                    {
                        dc_Id = 1;
                    }
                    else
                    {
                        dc_Id = (Convert.ToInt32(val) + 1);
                    }
                }
                sdr.Close();
            }
            catch { }
        }
        public void search_Cdc_Id(string id)
        {
            txt_CDC_Id.Text = id;
            try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT C_Name FROM Tb_CDC_Details WHERE C_Dc_No = '" + id + "'", con);
                sdr = cmd.ExecuteReader();
                sdr.Read();
                cmb_Cust_Name.Text = sdr[0].ToString();
                sdr.Close();
            }
            catch {  }
            load_CDC_Details(id);
            load_Content_Details(id);
        }
        public void load_CDC_Details(string id)
        {
            try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT Total_Amt, Balance_Amt, Remark, Total_Items, Deposit_Amt, Deposit_Return, For_Days, Reason, Pay_Mode, Paid_Amt,Vehicle_No FROM Tb_CDC_Details WHERE C_Dc_No = '" + id + "'", con);
                sdr = cmd.ExecuteReader();
                while(sdr.Read())
                {
                    lbl_Total_Amt.Text = sdr[0].ToString();
                    lbl_Balance.Text = sdr[1].ToString();
                    txt_Remark.Text = sdr[2].ToString();
                    lbl_Total_Items.Text = sdr[3].ToString();
                    txt_Deposit_Amt.Text = sdr[4].ToString();
                    txt_Deposit_Return.Text = sdr[5].ToString();
                    txt_For_Days.Text = sdr[6].ToString();
                    cmb_Reason.Text = sdr[7].ToString();
                    if(sdr[8].ToString() == "CASH")
                    {
                        rdbtn_Cash.Checked = true;
                    }
                    else if(sdr[8].ToString() == "OTHER")
                    {
                        rdbtn_Other.Checked = true;
                    }
                    txt_Paid_Amt.Text = sdr[9].ToString();
                    cmb_Vehicle_No.Text = sdr[10].ToString();
                }
                sdr.Close();
            }
            catch { }
            balance = Convert.ToInt32(lbl_Balance.Text);
           // txt_Paid_Amt.Hide();
           // txt_Paid_Amt_2.Show();
        }
        public void load_Content_Details(string id)
        {
            update = true;
           // try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT Cy_Type, Particulars, Sr_No, Item_No, Quantity, Rate, Unit, Total, Sell_Status FROM Tb_CDC_Content_Details WHERE C_Dc_No = '" + id + "'", con);
                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    dgv_Content.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_Content.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();
                btn_Save.Text = "Update";
            }
            //  catch { }
            foreach (DataGridViewRow row in dgv_Content.Rows)
            {
                //row.Cells[5].Value = "N";
                row.Cells["add_status"].Value = null;
            }
            chkbox_Add_New_Product.Visible = true;
            table_panel_Entry.Enabled = false;
        }
    }
}


//SELECT Total_Amt, Paid_Amt, Remark, Total_Items, Particulars, Sr_No, Item_No, Quantity, Rate, Unit, Total, Status FROM Tb_CDC_Content_Details