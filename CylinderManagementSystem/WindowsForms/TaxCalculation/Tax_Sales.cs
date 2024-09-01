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

namespace CylinderManagementSystem
{
    public partial class Tax_Sales : Form
    {
        SqlConnection con;
        SqlDataReader sdr;
        SqlCommand cmd;
        SqlDataAdapter adpt;
        DataTable dt;

        decimal total, sgst, cgst, igst, total_Sgst, total_Cgst, total_Igst, gst_Percent;
        string id_Month, id_Year, cdc_id, dc_Id, print_Id, particular_or_DC;

        public Tax_Sales()
        {
            InitializeComponent();
        }

        public void getconnection()
        {
            string str = ConfigurationManager.ConnectionStrings["CylinderData"].ConnectionString;
            con = new SqlConnection(str);
        }

        public void load_cmb_Customer_Name()
        {
            try
            {
                getconnection();
                con.Open();

                cmd = new SqlCommand("SELECT DISTINCT Cust_Name FROM Tb_Customer_Master", con);
                sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    cmb_Customer_Name.Items.Add(sdr[0]);
                }
                sdr.Close();
            }
            catch { }
            finally { con.Close(); }
        }

        private void rdbtn_Cash_CheckedChanged(object sender, EventArgs e)
        {
            if(rdb_DC_ALL.Checked==true)
            {
                particular_or_DC = "DC";
                panel21.Hide();
                panel51.Show();
               // panel22.Hide();
               // panel71.Hide();
                panel29.Show();
                panel55.Show();
                panel30.Hide();
                panel29.Dock = DockStyle.Fill;
                dgv_DC_Show.Dock = DockStyle.Fill;
                load_Cdc_Details_By_Customer();
                dgv_DC_Show.Columns["status"].Visible = true;
            }
            else
            {
                particular_or_DC = "Particular";
                panel21.Show();
                //panel22.Show();
                panel29.Hide();
            }
        }

        private void load_Cdc_Details_By_Customer()
        {
            // try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT C_Dc_Date, C_Dc_No, Total_Amt, Total_Items, Paid_Amt FROM Tb_CDC_Details WHERE C_Name = '" + cmb_Customer_Name.Text.ToString() + "' AND Tax_Status = 'NA' AND Status = 'NA'", con);
                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    dgv_DC_Show.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_DC_Show.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();
                //btn_Save.Text = "Update";
            }
            //  catch { }
            txt_Sgst_Percent.Text = "6";
            txt_Igst_Percent.Text = "0";
        }

        private void Tax_Sales_Load(object sender, EventArgs e)
        {
            btn_Print.Hide();
            panel57.Hide();
            panel72.Hide();
            panel55.Hide();
            panel32.Hide();
            panel42.Hide();
            panel43.Hide();
            panel20.Hide();
            panel21.Hide();
            panel29.Hide();
            Panel_Invoice_For.Enabled = false;
            panel30.Hide();
            txt_Invoice_No.Show();
            cmb_Invoice_No.Hide();
            load_Invoice_No();
            load_cmb_Customer_Name();
            set_textbox_default();
            cmb_cy_Type.Enabled = true;
        }

        public void load_Invoice_No()
        { 
            calculate_Max_Invoice_No();
            id_Month = DateTime.Now.Month.ToString();
            id_Year = DateTime.Now.ToString("yyyy");
            cdc_id = id_Month + "-" + id_Year + "-" + dc_Id;
            txt_Invoice_No.Text = cdc_id;
        }

        public void calculate_Max_Invoice_No()
        {
           //try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT MAX(invoice_id) FROM Tb_Tax_Sell_Details", con);
               sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    string val = sdr[0].ToString();
                    if (val == "")
                    {
                        dc_Id = "1";
                    }
                    else
                    {
                        dc_Id = (Convert.ToInt32(val) + 1).ToString();
                    }
                }
                sdr.Close();
            }
            //catch { }
        }

        //SETTING DEFAULT VALUES OF TEXSTBOXES
        private void set_textbox_default()
        {
            txt_Sgst.Text = txt_Cgst.Text = 6.ToString();
            txt_Igst.Text = txt_Discount.Text = 0.ToString();
            txt_Total.Text = 0.ToString();
            txt_Taxable_Amount.Text = 0.ToString();
            cmb_Unit.Text = "NOS";
        }
        private void rdb_Prticulars_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void rdb_Particulars_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_Particulars.Checked==true)
            {
                panel21.Dock = DockStyle.Fill;
                panel21.Show();
                panel51.Hide();
                //panel22.Show();
                panel27.Hide();
                panel71.Show();
                panel19.Show();
                panel17.Show();
                panel55.Hide();
                panel30.Show();
                dgv_Sales_Particular_Details.Show();
                dgv_Sales_Particular_Details.Dock = DockStyle.Fill;

            }
            else
            {
                panel21.Hide();
                //panel22.Hide();
                panel27.Show();
            }
        }

        private void panel19_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel25_Paint(object sender, PaintEventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
          
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void dgv_Sales_Particular_Details_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string value = dgv_Sales_Particular_Details.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            if(btn_Save.Text == "Save")
            {
                if (value == "Edit")
                {
                    if (btn_Add.Text == "UPDATE")
                    {
                        MessageBox.Show("Cannot Add Record While Another is in the Process", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        DataGridViewRow row = dgv_Sales_Particular_Details.Rows[e.RowIndex];
                        if (row.Cells["item"].Value.ToString() == "NA")
                        { cmb_category.Text = "MATERIAL"; }
                        else
                        { cmb_category.Text = "CYLINDER"; }
                        cmb_cy_Type.Text = row.Cells["type"].Value.ToString();
                        cmb_Particulars.Text = row.Cells["particulars"].Value.ToString();
                        txt_Hsn_Ssn.Text = row.Cells["hsn"].Value.ToString();
                        txt_Rate.Text = row.Cells["rate"].Value.ToString();
                        cmb_Item_No.Text = row.Cells["item"].Value.ToString();
                        txt_Discount.Text = row.Cells["discount"].Value.ToString();
                        txt_Quantity.Text = row.Cells["quantity"].Value.ToString();
                        cmb_Unit.Text = row.Cells["unit"].Value.ToString();
                        txt_Total.Text = row.Cells["total_"].Value.ToString();
                        txt_Sgst.Text = row.Cells["sgst_"].Value.ToString();
                        txt_Cgst.Text = row.Cells["cgst_"].Value.ToString();
                        txt_Igst.Text = row.Cells["igst_"].Value.ToString();
                        txt_Taxable_Amount.Text = row.Cells["with_gst"].Value.ToString();
                        dgv_Sales_Particular_Details.Rows.RemoveAt(e.RowIndex);
                        calculate_all_values_Particular();
                        btn_Add.Text = "UPDATE";
                    }
                }
                else if (value == "X")
                {
                    DialogResult result = MessageBox.Show("Are You Sure.?", "Confirmation", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        dgv_Sales_Particular_Details.Rows.RemoveAt(e.RowIndex);
                        calculate_all_values_Particular();
                    }
                }
            }
            else if(btn_Save.Text == "Update")
            {
                if (value == "Edit")
                {
                    if (btn_Add.Text == "UPDATE")
                    {
                        MessageBox.Show("Cannot Add Record While Another is in the Process", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        DataGridViewRow row = dgv_Sales_Particular_Details.Rows[e.RowIndex];
                        if (row.Cells["item"].Value.ToString() == "NA")
                        { cmb_category.Text = "MATERIAL"; }
                        else
                        { cmb_category.Text = "CYLINDER"; }
                        cmb_cy_Type.Text = row.Cells["type"].Value.ToString();
                        cmb_Particulars.Text = row.Cells["particulars"].Value.ToString();
                        txt_Hsn_Ssn.Text = row.Cells["hsn"].Value.ToString();
                        txt_Rate.Text = row.Cells["rate"].Value.ToString();
                        cmb_Item_No.Text = row.Cells["item"].Value.ToString();
                        txt_Discount.Text = row.Cells["discount"].Value.ToString();
                        txt_Quantity.Enabled = false;
                        txt_Quantity.Text = row.Cells["quantity"].Value.ToString();
                        cmb_Unit.Text = row.Cells["unit"].Value.ToString();
                        txt_Total.Text = row.Cells["total_"].Value.ToString();
                        txt_Sgst.Text = row.Cells["sgst_"].Value.ToString();
                        txt_Cgst.Text = row.Cells["cgst_"].Value.ToString();
                        txt_Igst.Text = row.Cells["igst_"].Value.ToString();
                        txt_Taxable_Amount.Text = row.Cells["with_gst"].Value.ToString();
                        dgv_Sales_Particular_Details.Rows.RemoveAt(e.RowIndex);
                        calculate_all_values_Particular();
                        table_panel_Entry.Enabled = true;
                        btn_Add.Text = "UPDATE";
                    }
                }
                else if (value == "X")
                {
                    DialogResult result = MessageBox.Show("Are You Sure.?", "Confirmation", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        DataGridViewRow row = dgv_Sales_Particular_Details.Rows[e.RowIndex];
                        getconnection();
                        con.Open();
                        cmd = new SqlCommand("UPDATE Tb_Inventory_Master SET In_Stock = In_Stock + " + row.Cells["quantity"].Value.ToString() + ", Out_Stock = Out_Stock - " + row.Cells["quantity"].Value.ToString() + " WHERE Particulars = '" + row.Cells["particulars"].Value.ToString() + "'", con);
                        cmd.ExecuteNonQuery();
                        cmd = new SqlCommand("DELETE FROM Tb_Tax_Sell_Content_Particular WHERE Particulers = '" + row.Cells["particulars"].Value.ToString() + "' AND Invoice_No = '" + cmb_Invoice_No.Text + "' AND Item_No = '" + row.Cells["item"].Value.ToString() + "'", con);
                        cmd.ExecuteNonQuery();
                        dgv_Sales_Particular_Details.Rows.RemoveAt(e.RowIndex);
                        calculate_all_values_Particular();
                    }
                }
            }
        }

        private void cmb_Customer_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_Customer_Details();
            if (btn_Save.Text == "Save")
            {
                load_Customer_Details();
                Panel_Invoice_For.Enabled = true;
                if (rdb_DC_ALL.Checked == true)
                {
                    load_Cdc_Details_By_Customer();
                }
            }
        }

        public void load_Customer_Details()
        {
            //try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT Cust_Address, Cust_PhoneNo, Cust_CPerson, CP_PhoneNo, Cust_GSTNo FROM Tb_Customer_Master WHERE Cust_Name = '" + cmb_Customer_Name.Text.ToString() + "'", con);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    lbl_Address.Text = sdr[0].ToString();
                    lbl_Contact_No.Text = sdr[1].ToString();
                    lbl_Contact_Person.Text = sdr[2].ToString();
                    lbl_Contact_Person_Phone.Text = sdr[3].ToString();
                    lbl_GST_No.Text = sdr[4].ToString();
                }
                //sdr.Close();
            }
            //catch { }
            //finally { con.Close(); }
        }

        private void cmb_category_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmb_category.Text == "CYLINDER")
            {
                load_cmb_Item_No();
                table_panel_Entry.ColumnStyles[1].SizeType = SizeType.Percent;
                table_panel_Entry.ColumnStyles[1].Width = 6;
                table_panel_Entry.ColumnStyles[2].SizeType = SizeType.Percent;
                table_panel_Entry.ColumnStyles[2].Width = 6;
                cmb_cy_Type.Text = "";
                cmb_Item_No.Text = "";
                cmb_Particulars.ResetText();
                txt_Rate.Clear();
                txt_Quantity.Clear();
            }
            else if(cmb_category.Text == "MATERIAL")
            {
                table_panel_Entry.ColumnStyles[1].SizeType = SizeType.Percent;
                table_panel_Entry.ColumnStyles[1].Width = 0;
                table_panel_Entry.ColumnStyles[2].SizeType = SizeType.Percent;
                table_panel_Entry.ColumnStyles[2].Width = 0;
                cmb_cy_Type.Text = "NA";
                cmb_Item_No.Text = "NA";
                lbl_Sr_No.Text = "NA";
                txt_Hsn_Ssn.Text = "";
                txt_Quantity.Clear();
                cmb_Particulars.ResetText();
                txt_Rate.Clear();
                cmb_Particulars.Enabled = true;
                cmb_cy_Type.Enabled = true;
                txt_Quantity.Enabled = true;
                load_cmb_Particular();
            }
        }

        private void load_cmb_Particular()
        {
            cmb_Particulars.Items.Clear();
           // try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT DISTINCT Particulars FROM Tb_Inventory_Master WHERE Cylinder_Type = '" + cmb_cy_Type.Text.ToString() + "'", con);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    cmb_Particulars.Items.Add(sdr[0]);
                }
                sdr.Close();
            }
           // catch { }
            //finally { con.Close(); }
        }

        private void load_cmb_Item_No()
        {
            cmb_Item_No.Items.Clear();
            try
            {
                getconnection();
                con.Open();

                //cmd = new SqlCommand("SELECT DISTINCT Part_No FROM Tb_Purchase_Content_Master WHERE Purchase_Type = 'CYLINDER' AND Cylinder_Status = 'FILL'", con);
                cmd = new SqlCommand("SELECT DISTINCT Part_No FROM Tb_Purchase_Content_Master WHERE Purchase_Type = 'CYLINDER'", con);

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

        private void btn_Add_Click(object sender, EventArgs e)
        {
            if (cmb_category.Text == "")
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
            else if (txt_Hsn_Ssn.Text == "")
            {
                MessageBox.Show("Enter HSN/SSN Value");
            }
            else if (txt_Rate.Text == "")
            {
                MessageBox.Show("Enter Rate");
            }
            else if (txt_Discount.Text == "")
            {
                MessageBox.Show("Select Cylinder Type");
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
                if (btn_Save.Text == "Save")
                {
                    int index = dgv_Sales_Particular_Details.Rows.Add();
                    DataGridViewRow row = dgv_Sales_Particular_Details.Rows[index];
                    row.Cells["type"].Value = cmb_cy_Type.Text.Trim();
                    row.Cells["particulars"].Value = cmb_Particulars.Text.Trim();
                    row.Cells["hsn"].Value = txt_Hsn_Ssn.Text.Trim();
                    row.Cells["serial"].Value = lbl_Sr_No.Text.Trim();
                    row.Cells["item"].Value = cmb_Item_No.Text.Trim();
                    row.Cells["rate"].Value = txt_Rate.Text.Trim();
                    row.Cells["discount"].Value = txt_Discount.Text.Trim();
                    row.Cells["total_"].Value = txt_Total.Text.Trim();
                    row.Cells["unit"].Value = cmb_Unit.Text.Trim();
                    row.Cells["quantity"].Value = txt_Quantity.Text.ToString().Trim();
                    row.Cells["sgst_"].Value = txt_Sgst.Text.Trim();
                    row.Cells["cgst_"].Value = txt_Cgst.Text.Trim();
                    row.Cells["igst_"].Value = txt_Igst.Text.Trim();
                    row.Cells["with_gst"].Value = txt_Taxable_Amount.Text.ToString().Trim();
                    clear_data();
                }
                else if (btn_Save.Text == "Update")
                {
                    DataTable table = new DataTable();
                    table = (DataTable)dgv_Sales_Particular_Details.DataSource;
                    DataRow row = table.NewRow();
                    row["Gas_Type"] = cmb_cy_Type.Text.Trim();
                    row["Particulers"] = cmb_Particulars.Text.Trim();
                    row["HSN"] = txt_Hsn_Ssn.Text.Trim();
                    row["Item_No"] = cmb_Item_No.Text.Trim();
                    row["Sr_No"] = lbl_Sr_No.Text.Trim();
                    row["Discount"] = txt_Discount.Text.Trim();
                    row["SGST"] = txt_Sgst.Text.Trim();
                    row["CGST"] = txt_Cgst.Text.Trim();
                    row["IGST"] = txt_Igst.Text.Trim();
                    row["Rate"] = txt_Rate.Text.Trim();
                    row["Quantity"] = txt_Quantity.Text;
                    row["Unit"] = cmb_Unit.Text.Trim();
                    row["Total"] = txt_Total.Text.ToString().Trim();
                    row["Taxable_Amt"] = txt_Taxable_Amount.Text.Trim();
                    table.Columns.Add("update");
                    if (btn_Add.Text == "ADD")
                    { row["update"] = "new"; }
                    else if (btn_Add.Text == "UPDATE")
                    { row["update"] = "update"; }
                    table.Rows.Add(row);
                    table.AcceptChanges();

                    clear_data();
                }
            }
            btn_Add.Text = "ADD";
            cmb_Item_No.Items.Remove(cmb_Item_No.Text);
            calculate_all_values_Particular();
            btn_Add.Focus();
        }

        //FUNCTIONS TO CLEAR TEXT FIELDS
        public void clear_data()
        {
            cmb_Particulars.ResetText();
            if (cmb_category.Text == "MATERIAL")
            {
                txt_Hsn_Ssn.Clear();
            }
            txt_Rate.Clear();
            txt_Discount.Clear();
            txt_Quantity.Clear();
            cmb_Unit.ResetText();
            txt_Total.Clear();
            txt_Sgst.Clear();
            txt_Cgst.Clear();
            txt_Igst.Clear();
            txt_Taxable_Amount.Clear();
            txt_Total.Clear();
            cmb_category.ResetText();
            cmb_Item_No.ResetText();
            cmb_cy_Type.ResetText();
            set_textbox_default();
        }
        public void calculate_all_values_Particular()
        {
            decimal total_amt = 0, gst_amt = 0, total_gst_amt = 0, total = 0, quantity = 0, total_quantity = 0;
            total_Sgst = total_Cgst = total_Igst = total_amt = gst_amt = total_gst_amt = 0;
            int rowcount = dgv_Sales_Particular_Details.RowCount;
            if (rowcount > 0)
            {
                foreach (DataGridViewRow row in dgv_Sales_Particular_Details.Rows)
                {
                    total = Convert.ToDecimal(row.Cells["total_"].Value.ToString());
                    cgst = Convert.ToDecimal(row.Cells["cgst_"].Value.ToString());
                    sgst = Convert.ToDecimal(row.Cells["sgst_"].Value.ToString());
                    igst = Convert.ToDecimal(row.Cells["igst_"].Value.ToString());
                    quantity = Convert.ToDecimal(row.Cells["quantity"].Value.ToString());
                    total_Sgst = total_Sgst + sgst;
                    total_Cgst = total_Cgst + cgst;
                    total_Igst = total_Igst + igst;
                    total_amt = total_amt + total;
                    gst_amt = total * (cgst + sgst + igst) / 100;
                    total_gst_amt = total_gst_amt + gst_amt;
                    total_quantity = total_quantity + quantity;
                }
                total_Sgst = total_Sgst / rowcount;
                total_Cgst = total_Cgst / rowcount;
                total_Igst = total_Igst / rowcount;
            }

            //lbl_Total_Items.Text = rowcount.ToString();
            lbl_Quantity.Text = total_quantity.ToString();
            lbl_Total_Items.Text = (dgv_Sales_Particular_Details.RowCount).ToString();
            //lbl_Sgst_Percent.Text = (total_Sgst).ToString();
            //lbl_Cgst_Percent.Text = (total_Cgst).ToString();
            //lbl_Igst_Percent.Text = (total_Igst).ToString();
            lbl_Total_Amt.Text = total_amt.ToString();
            lbl_GST_Amt.Text = total_gst_amt.ToString();
            lbl_Net_Amt.Text = (Math.Abs(Convert.ToInt64(total_amt + total_gst_amt))).ToString();

            //CALCULATE SGST, CGST, IGST AMOUNT
            lbl_SGST_Amt.Text = (total_amt * total_Sgst / 100).ToString();
            lbl_CGST_Amt.Text = (total_amt * total_Cgst / 100).ToString();
            lbl_IGST_Amt.Text = (total_amt * total_Igst / 100).ToString();
            //lbl_Gst_Percent.Text = (total_Cgst + total_Sgst).ToString();
        }
        private void dgv_Sales_Particular_Details_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dgv_Sales_Particular_Details.Rows[e.RowIndex].Cells["sr"].Value = (e.RowIndex + 1).ToString();
        }
        private void btn_Save_Click(object sender, EventArgs e)
        {
            if(rdb_Particulars.Checked == true)
            {
                if (btn_Save.Text == "Save")
                {
                    if (cmb_Customer_Name.Text == "")
                    {
                        MessageBox.Show("Please Select The Customer First", "STOP", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    else if (dt_In_Date.Text == "")
                    {
                        MessageBox.Show("Please Select The date First", "STOP", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    else if (txt_Invoice_No.Text == "")
                    {
                        MessageBox.Show("Please Enter The Invoice No", "STOP", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    else if (lbl_Total_Items.Text == "")
                    {
                        MessageBox.Show("Please Select DC", "STOP", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    else
                    {
                        //INSERT TAX SELL DETAILS
                        // try
                        {
                            getconnection();
                            con.Open();
                            cmd = new SqlCommand("Insert_Tax_Sell_Details", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ID", dc_Id);
                            cmd.Parameters.AddWithValue("@Cust_Name", cmb_Customer_Name.Text.Trim());
                            cmd.Parameters.AddWithValue("@Date_Of_Sell", dt_In_Date.Text.ToString());
                            cmd.Parameters.AddWithValue("@Invoice_No", txt_Invoice_No.Text.Trim());
                            cmd.Parameters.AddWithValue("@Gst_No", lbl_GST_No.Text.Trim());
                            cmd.Parameters.AddWithValue("@SGST_Amt", lbl_SGST_Amt.Text.Trim());
                            cmd.Parameters.AddWithValue("@CGST_Amt", lbl_CGST_Amt.Text.Trim());
                            cmd.Parameters.AddWithValue("@IGST_Amt", lbl_IGST_Amt.Text.Trim());
                            cmd.Parameters.AddWithValue("@Total_Items", lbl_Total_Items.Text.Trim());
                            cmd.Parameters.AddWithValue("@Total_Quantity", lbl_Quantity.Text.Trim());
                            cmd.Parameters.AddWithValue("@Total_Amt", lbl_Total_Amt.Text.Trim());
                            cmd.Parameters.AddWithValue("@GST_Amt", lbl_GST_Amt.Text.Trim());
                            cmd.Parameters.AddWithValue("@Net_Amt", lbl_Net_Amt.Text.Trim());
                            cmd.Parameters.AddWithValue("@Status", "Particular");
                            cmd.Parameters.AddWithValue("@Paid_Amt", "0");
                            cmd.Parameters.AddWithValue("@Balance_Amt", lbl_Net_Amt.Text.Trim());
                            cmd.ExecuteNonQuery();
                            //   MessageBox.Show("Data Saved Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            
                        }
                        // catch { MessageBox.Show("Purchase Error"); }

                        //INSERT TAX SELL CONTENT DETAILS
                        //try
                        {
                            getconnection();
                            con.Open();
                            foreach (DataGridViewRow row in dgv_Sales_Particular_Details.Rows)
                            {
                                cmd = new SqlCommand("Insert_Tax_Sell_Content_Particular", con);
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@Invoice_No", txt_Invoice_No.Text.ToString().Trim());
                                cmd.Parameters.AddWithValue("@Particulers", row.Cells["particulars"].Value.ToString());
                                cmd.Parameters.AddWithValue("@Gas_Type", row.Cells["type"].Value.ToString());
                                cmd.Parameters.AddWithValue("@HSN", row.Cells["hsn"].Value.ToString());
                                cmd.Parameters.AddWithValue("@Rate", row.Cells["rate"].Value.ToString());
                                cmd.Parameters.AddWithValue("@Sr_No", row.Cells["serial"].Value.ToString());
                                cmd.Parameters.AddWithValue("@Part_No", row.Cells["item"].Value.ToString());
                                cmd.Parameters.AddWithValue("@Disc", row.Cells["discount"].Value.ToString());
                                cmd.Parameters.AddWithValue("@Quantity", row.Cells["quantity"].Value.ToString());
                                cmd.Parameters.AddWithValue("@Unit", row.Cells["unit"].Value.ToString());
                                cmd.Parameters.AddWithValue("@Total", row.Cells["total_"].Value.ToString());
                                cmd.Parameters.AddWithValue("@SGST", row.Cells["sgst_"].Value.ToString());
                                cmd.Parameters.AddWithValue("@CGST", row.Cells["cgst_"].Value.ToString());
                                cmd.Parameters.AddWithValue("@IGST", row.Cells["igst_"].Value.ToString());
                                cmd.Parameters.AddWithValue("@TaxableAmt", row.Cells["with_gst"].Value.ToString());
                                cmd.Parameters.AddWithValue("@Status", "SELL");
                                cmd.ExecuteNonQuery();
                                //UPDATE STATUS INTO PURCHASE CONTENT MASTER
                                cmd = new SqlCommand("UPDATE Tb_Purchase_Content_Master SET Cylinder_Status = 'SELL TO CUSTOMER', Cust_Supp_Name = '" + cmb_Customer_Name.Text + "' WHERE Particulers = '" + row.Cells["particulars"].Value.ToString() + "' AND Part_No = '" + row.Cells["item"].Value.ToString() + "'", con);
                                cmd.ExecuteNonQuery();
                                //UPDATE INVENTORY MASTER
                                cmd = new SqlCommand("UPDATE Tb_Inventory_Master SET In_Stock = In_Stock - " + row.Cells["quantity"].Value.ToString() + ", Out_Stock = Out_Stock + " + row.Cells["quantity"].Value.ToString() + " WHERE Particulars = '" + row.Cells["particulars"].Value.ToString() + "'", con);
                                cmd.ExecuteNonQuery();
                                //MessageBox.Show("Data Saved Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        DialogResult result = MessageBox.Show("Data Saved SuccessFully. Do You Want to Print Invoice?", "Success", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (result == DialogResult.Yes)
                        {
                            //btn_Print.PerformClick();
                            print_Id = txt_Invoice_No.Text;
                            if (particular_or_DC == "Particular")
                            {
                                Report_Tax_Particular form = new Report_Tax_Particular();
                                form.get_ID(print_Id);
                                form.Show();
                            }
                            else
                            {
                                Report_Tax_DC form = new Report_Tax_DC();
                                form.get_ID(print_Id);
                                form.Show();
                            }
                            clear_data();
                            clear_All_Data();
                        }
                        else
                        {
                            clear_data();
                            clear_All_Data();
                        }
                    }
                }
                else if (btn_Save.Text == "Update")
                {
                    // try
                    {
                        getconnection();
                        con.Open();
                        cmd = new SqlCommand("Update_Tax_Sell_Details", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Invoice_No", cmb_Invoice_No.Text.Trim());
                        cmd.Parameters.AddWithValue("@SGST_Amt", lbl_SGST_Amt.Text.Trim());
                        cmd.Parameters.AddWithValue("@CGST_Amt", lbl_CGST_Amt.Text.Trim());
                        cmd.Parameters.AddWithValue("@IGST_Amt", lbl_IGST_Amt.Text.Trim());
                        cmd.Parameters.AddWithValue("@Total_Items", lbl_Total_Items.Text.Trim());
                        cmd.Parameters.AddWithValue("@Total_Quantity", lbl_Quantity.Text.Trim());
                        cmd.Parameters.AddWithValue("@Total_Amt", lbl_Total_Amt.Text.Trim());
                        cmd.Parameters.AddWithValue("@GST_Amt", lbl_GST_Amt.Text.Trim());
                        cmd.Parameters.AddWithValue("@Net_Amt", lbl_Net_Amt.Text.Trim());
                        //cmd.Parameters.AddWithValue("@Status", "NA");
                        cmd.ExecuteNonQuery();
                        //   MessageBox.Show("Data Saved Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clear_data();

                        foreach (DataGridViewRow row in dgv_Sales_Particular_Details.Rows)
                        {
                            if (row.Cells["update"].Value == null)
                            {

                            }
                            else if (row.Cells["update"].Value == "new")
                            {
                                cmd = new SqlCommand("Insert_Tax_Sell_Content_Particular", con);
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@Invoice_No", cmb_Invoice_No.Text.ToString().Trim());
                                cmd.Parameters.AddWithValue("@Particulers", row.Cells["particulars"].Value.ToString());
                                cmd.Parameters.AddWithValue("@Gas_Type", row.Cells["type"].Value.ToString());
                                cmd.Parameters.AddWithValue("@HSN", row.Cells["hsn"].Value.ToString());
                                cmd.Parameters.AddWithValue("@Rate", row.Cells["rate"].Value.ToString());
                                cmd.Parameters.AddWithValue("@Sr_No", row.Cells["serial"].Value.ToString());
                                cmd.Parameters.AddWithValue("@Part_No", row.Cells["item"].Value.ToString());
                                cmd.Parameters.AddWithValue("@Disc", row.Cells["discount"].Value.ToString());
                                cmd.Parameters.AddWithValue("@Quantity", row.Cells["quantity"].Value.ToString());
                                cmd.Parameters.AddWithValue("@Unit", row.Cells["unit"].Value.ToString());
                                cmd.Parameters.AddWithValue("@Total", row.Cells["total_"].Value.ToString());
                                cmd.Parameters.AddWithValue("@SGST", row.Cells["sgst_"].Value.ToString());
                                cmd.Parameters.AddWithValue("@CGST", row.Cells["cgst_"].Value.ToString());
                                cmd.Parameters.AddWithValue("@IGST", row.Cells["igst_"].Value.ToString());
                                cmd.Parameters.AddWithValue("@TaxableAmt", row.Cells["with_gst"].Value.ToString());
                                cmd.Parameters.AddWithValue("@Status", "SELL");
                                cmd.ExecuteNonQuery();
                                //UPDATE STATUS INTO PURCHASE CONTENT MASTER
                                cmd = new SqlCommand("UPDATE Tb_Purchase_Content_Master SET Cylinder_Status = 'SELL TO CUSTOMER', Cust_Supp_Name = '" + cmb_Customer_Name.Text + "' WHERE Particulers = '" + row.Cells["particulars"].Value.ToString() + "' AND Part_No = '" + row.Cells["item"].Value.ToString() + "'", con);
                                cmd.ExecuteNonQuery();
                                //UPDATE INVENTORY MASTER
                                cmd = new SqlCommand("UPDATE Tb_Inventory_Master SET In_Stock = In_Stock - " + row.Cells["quantity"].Value.ToString() + ", Out_Stock = Out_Stock + " + row.Cells["quantity"].Value.ToString() + " WHERE Particulars = '" + row.Cells["particulars"].Value.ToString() + "'", con);
                                cmd.ExecuteNonQuery();
                            }
                            else if (row.Cells["update"].Value == "update")
                            {
                                cmd = new SqlCommand("Update_Tax_Sell_Content_Particular", con);
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@Invoice_No", cmb_Invoice_No.Text.ToString().Trim());
                                cmd.Parameters.AddWithValue("@Particulers", row.Cells["particulars"].Value.ToString());
                                cmd.Parameters.AddWithValue("@Gas_Type", row.Cells["type"].Value.ToString());
                                cmd.Parameters.AddWithValue("@HSN", row.Cells["hsn"].Value.ToString());
                                cmd.Parameters.AddWithValue("@Rate", row.Cells["rate"].Value.ToString());
                                cmd.Parameters.AddWithValue("@Sr_No", row.Cells["serial"].Value.ToString());
                                cmd.Parameters.AddWithValue("@Part_No", row.Cells["item"].Value.ToString());
                                cmd.Parameters.AddWithValue("@Disc", row.Cells["discount"].Value.ToString());
                                cmd.Parameters.AddWithValue("@Quantity", row.Cells["quantity"].Value.ToString());
                                cmd.Parameters.AddWithValue("@Unit", row.Cells["unit"].Value.ToString());
                                cmd.Parameters.AddWithValue("@Total", row.Cells["total_"].Value.ToString());
                                cmd.Parameters.AddWithValue("@SGST", row.Cells["sgst_"].Value.ToString());
                                cmd.Parameters.AddWithValue("@CGST", row.Cells["cgst_"].Value.ToString());
                                cmd.Parameters.AddWithValue("@IGST", row.Cells["igst_"].Value.ToString());
                                cmd.Parameters.AddWithValue("@TaxableAmt", row.Cells["with_gst"].Value.ToString());
                                cmd.Parameters.AddWithValue("@Status", "SELL");
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                    DialogResult result = MessageBox.Show("Data Saved SuccessFully. Do You Want to Print Invoice?", "Success", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result == DialogResult.Yes)
                    {
                        //btn_Print.PerformClick();
                        print_Id = txt_Invoice_No.Text;
                        if (particular_or_DC == "Particular")
                        {
                            Report_Tax_Particular form = new Report_Tax_Particular();
                            form.get_ID(print_Id);
                            form.Show();
                        }
                        else
                        {
                            Report_Tax_DC form = new Report_Tax_DC();
                            form.get_ID(print_Id);
                            form.Show();
                        }
                        clear_data();
                        clear_All_Data();
                    }
                    else
                    {
                        clear_data();
                        clear_All_Data();
                    }
                }
            }
            else if(rdb_DC_ALL.Checked == true)
            {
                if (btn_Save.Text == "Save")
                {
                    if (cmb_Customer_Name.Text == "")
                    {
                        MessageBox.Show("Please Select The Customer First", "STOP", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    else if (dt_In_Date.Text == "")
                    {
                        MessageBox.Show("Please Select The date First", "STOP", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    else if (txt_Invoice_No.Text == "")
                    {
                        MessageBox.Show("Please Enter The Invoice No", "STOP", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    else if (lbl_Total_Items.Text == "0")
                    {
                        MessageBox.Show("Please Select DC", "STOP", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    else
                    {
                        //INSERT TAX SELL DETAILS
                        // try
                        {
                            getconnection();
                            con.Open();
                            cmd = new SqlCommand("Insert_Tax_Sell_Details", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ID", dc_Id);
                            cmd.Parameters.AddWithValue("@Cust_Name", cmb_Customer_Name.Text.Trim());
                            cmd.Parameters.AddWithValue("@Date_Of_Sell", dt_In_Date.Text.ToString());
                            cmd.Parameters.AddWithValue("@Invoice_No", txt_Invoice_No.Text.Trim());
                            cmd.Parameters.AddWithValue("@Gst_No", lbl_GST_No.Text.Trim());
                            cmd.Parameters.AddWithValue("@SGST_Amt", lbl_SGST_Amt.Text.Trim());
                            cmd.Parameters.AddWithValue("@CGST_Amt", lbl_CGST_Amt.Text.Trim());
                            cmd.Parameters.AddWithValue("@IGST_Amt", lbl_IGST_Amt.Text.Trim());
                            cmd.Parameters.AddWithValue("@Total_Items", lbl_Total_Items.Text.Trim());
                            cmd.Parameters.AddWithValue("@Total_Quantity", lbl_Quantity.Text.Trim());
                            cmd.Parameters.AddWithValue("@Total_Amt", lbl_Total_Amt.Text.Trim());
                            cmd.Parameters.AddWithValue("@GST_Amt", lbl_GST_Amt.Text.Trim());
                            cmd.Parameters.AddWithValue("@Net_Amt", lbl_Net_Amt.Text.Trim());
                            cmd.Parameters.AddWithValue("@Status", "DC");
                            cmd.Parameters.AddWithValue("@Paid_Amt", lbl_Paid_Amt.Text.ToString());
                            cmd.Parameters.AddWithValue("@Balance_Amt", lbl_Balance.Text.ToString());
                            cmd.ExecuteNonQuery();
                            //   MessageBox.Show("Data Saved Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            clear_data();
                        }
                        // catch { MessageBox.Show("Purchase Error"); }

                        //INSERT TAX SELL CONTENT DETAILS
                        //try
                        {
                            getconnection();
                            con.Open();
                            foreach (DataGridViewRow row in dgv_DC_Show.Rows)
                            {
                                if (Convert.ToBoolean(row.Cells["status"].Value))
                                {
                                    cmd = new SqlCommand("Insert_Tax_Content_DC", con);
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@Invoice_No", txt_Invoice_No.Text.ToString().Trim());
                                    cmd.Parameters.AddWithValue("@Cdc_No", row.Cells["dcno"].Value.ToString());
                                    cmd.Parameters.AddWithValue("@Total_Item", row.Cells["items"].Value.ToString());
                                    cmd.Parameters.AddWithValue("@Dc_Date", row.Cells["date"].Value.ToString());
                                    cmd.Parameters.AddWithValue("@Dc_Total", row.Cells["dc_total"].Value.ToString());
                                    cmd.Parameters.AddWithValue("@Paid_Amt", row.Cells["paid"].Value.ToString());
                                    cmd.ExecuteNonQuery();
                                    cmd = new SqlCommand("UPDATE Tb_CDC_Details SET Tax_Status = 'TO TAX' WHERE C_Dc_No = '" + row.Cells["dcno"].Value.ToString() + "'", con);
                                    cmd.ExecuteNonQuery();
                                }
                                //MessageBox.Show("Data Saved Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        DialogResult result = MessageBox.Show("Data Saved SuccessFully. Do You Want to Print Invoice?", "Success", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (result == DialogResult.Yes)
                        {
                            //btn_Print.PerformClick();
                            print_Id = txt_Invoice_No.Text;
                            if (particular_or_DC == "Particular")
                            {
                                Report_Tax_Particular form = new Report_Tax_Particular();
                                form.get_ID(print_Id);
                                form.Show();
                            }
                            else
                            {
                                Report_Tax_DC form = new Report_Tax_DC();
                                form.get_ID(print_Id);
                                form.Show();
                            }
                            clear_data();
                            clear_All_Data();
                        }
                        else
                        {
                            clear_data();
                            clear_All_Data();
                        }
                    }
                }
            }
        }
        private void cmb_Item_No_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        private void cmb_Particulars_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_In_Stock_Value();
        }
        public void load_In_Stock_Value()
        {
            getconnection();
            con.Open();
            if (cmb_category.Text == "CYLINDER")
            {
                cmd = new SqlCommand("select count(Cylinder_Status) from Tb_Purchase_Content_Master where Particulers = '" + cmb_Particulars.Text.ToString() + "' AND Cylinder_Status IN ('FILL')", con);
            }
            else if (cmb_category.Text == "MATERIAL")
            {
                cmd = new SqlCommand("select In_Stock from Tb_Inventory_Master where Particulars = '" + cmb_Particulars.Text.ToString() + "'", con);
            }
            try
            {
                sdr = cmd.ExecuteReader();
                sdr.Read();
                lbl_In_Stock.Text = sdr[0].ToString();
               // sdr.Close();
            }
            catch { }
        }
        private void cmb_Particulars_TextChanged(object sender, EventArgs e)
        {
            load_In_Stock_Value();
            load_Hsn_No();
        }
        private void load_Hsn_No()
        {
            if (cmb_category.Text == "MATERIAL")
            {
                //try
                {
                    getconnection();
                    con.Open();
                    
                    cmd = new SqlCommand("SELECT HSN FROM Tb_Purchase_Content_Master WHERE Particulers = '" + cmb_Particulars.Text.ToString() + "'", con);
                    sdr = cmd.ExecuteReader();

                    while (sdr.Read())
                    {
                        txt_Hsn_Ssn.Text = sdr[0].ToString();
                    }
                    sdr.Close();
                }
            //catch { }
            }
        }
        private void cmb_Search_By_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmb_Search_By.Text == "BY CDC NO")
            {
                panel32.Show();
                panel42.Hide();
                panel43.Hide();
                panel20.Hide();
                load_Cdc_No();
            }
            else if (cmb_Search_By.Text == "BY DATE")
            {
                panel32.Hide();
                panel42.Show();
                panel43.Hide();
                panel20.Hide();
                // load_Cdc_No();
            }
        }
        private void load_Cdc_No()
        {
            cmb_Cdc_No.Items.Clear();
            try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT DISTINCT C_Dc_No FROM Tb_CDC_Details WHERE Cancel_Status = 'NA'", con);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    if (sdr[0].ToString() != "NA")
                    {
                        cmb_Cdc_No.Items.Add(sdr[0]);
                    }
                }
                sdr.Close();
            }
            catch { }
            finally { con.Close(); }
        }
        private void cmb_Cdc_No_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_Cdc_Details();
        }
        private void load_Cdc_Details()
        {
            // try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT C_Dc_Date, C_Dc_No, Total_Amt, Total_Items, Paid_Amt FROM Tb_CDC_Details WHERE C_Dc_No = '" + cmb_Cdc_No.Text.ToString() + "'", con);
                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];

                if (dt.Rows.Count == 0)
                {
                    dgv_DC_Show.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_DC_Show.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();

                btn_Save.Text = "Update";
            }
            //  catch { }
        }
        private void rdbtn_By_Date_CheckedChanged(object sender, EventArgs e)
        {
            if(rdbtn_By_Date.Checked == true)
            {
                panel43.Show();
            }
        }
        private void rdbtn_Between_Date_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbtn_Between_Date.Checked == true)
            {
                panel43.Show();
                panel20.Show();
            }
        }
        private void dgv_Filling_Content_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dgv_DC_Show.Rows[e.RowIndex].Cells["sr_no"].Value = (e.RowIndex + 1).ToString();
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (rdbtn_By_Date.Checked == true)
            {
                show_by_Date();
            }
        }
        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if (rdbtn_Between_Date.Checked == true)
            {
                show_Between_Date();
            }
        }
        private void show_by_Date()
        {
            String date = dtp_By_Date.Text.ToString();
            //    try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT C_Dc_Date, C_Dc_No, Total_Amt, Total_Items, Paid_Amt FROM Tb_CDC_Details WHERE C_Dc_Date = '" + date + "' AND C_Name = '" + cmb_Customer_Name.Text.ToString() + "'", con);
                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    dgv_DC_Show.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_DC_Show.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();
            }
            //   catch { }
            //   finally { con.Close(); }
        }
        public void show_Between_Date()
        {
            String start_date = dtp_By_Date.Text.ToString();
            String end_date = dtp_Between_Date.Text.ToString();
           // try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT C_Dc_Date, C_Dc_No, Total_Amt, Total_Items, Paid_Amt FROM Tb_CDC_Details WHERE C_Name = '" + cmb_Customer_Name.Text.ToString() + "' AND C_Dc_Date BETWEEN '" + start_date + "' AND '" + end_date + "'", con);
                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];

                if (dt.Rows.Count == 0)
                {
                    dgv_DC_Show.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_DC_Show.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();
            }
          //  catch { }
           // finally { con.Close(); }
        }
        private void panel54_Paint(object sender, PaintEventArgs e)
        {

        }
        private void dgv_Filling_Content_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgv_DC_Show.Columns["status"].Index)
            {
                dgv_DC_Show.CommitEdit(DataGridViewDataErrorContexts.Commit);
                calculate_all_values_DC();
            }
        }
        private void calculate_all_values_DC()
        {
            int total = 0, quantity= 0, paid=0 ;
            foreach (DataGridViewRow row in dgv_DC_Show.Rows)
            {
                if (Convert.ToBoolean(row.Cells["status"].Value))
                {
                    total = total + Convert.ToInt32(row.Cells["dc_total"].Value);
                    paid = paid + Convert.ToInt32(row.Cells["paid"].Value);
                    quantity++;
                }
            }
            decimal sgst=0, cgst=0, igst=0, gst;
            sgst = Convert.ToDecimal(txt_Sgst_Percent.Text);
            cgst = Convert.ToDecimal(lbl_Cgst_Percent.Text);
            igst = Convert.ToDecimal(txt_Igst_Percent.Text);
            gst = (sgst + cgst);
            lbl_Total_Items.Text = quantity.ToString();
            lbl_Quantity.Text = quantity.ToString();
            lbl_Total_Amt.Text = total.ToString();
            lbl_GST_Amt.Text = (total * gst / 100).ToString();
            lbl_SGST_Amt.Text = (total * sgst / 100).ToString();
            lbl_CGST_Amt.Text = (total * cgst / 100).ToString();
            lbl_IGST_Amt.Text = (total * igst / 100).ToString();
            Decimal total_gst = gst + igst;
            lbl_Net_Amt.Text = (Math.Abs(total + (total * total_gst / 100))).ToString();
            lbl_Paid_Amt.Text = paid.ToString();
            lbl_Balance.Text = (Math.Abs(total - paid)).ToString();
        }
        private void txt_Gst_Percent_TextChanged(object sender, EventArgs e)
        {
            /*if(txt_Sgst_Percent.Text != "")
            {
                lbl_Cgst_Percent.Text = txt_Sgst_Percent.Text;
                txt_Igst_Percent.Enabled = false;
               // txt_Igst_Percent.Text = "0";
                calculate_all_values_DC();
            }
            else
            {
                lbl_Cgst_Percent.Text = "0";
                txt_Igst_Percent.Enabled = true;
            }*/
            lbl_Cgst_Percent.Text = txt_Sgst_Percent.Text;
            if (txt_Sgst_Percent.Text == "")
            {
                txt_Igst_Percent.Enabled = true;
                txt_Igst_Percent.Clear();
            }
            else if (Convert.ToInt32(txt_Sgst_Percent.Text) > 0)
            {
                txt_Igst_Percent.Enabled = false;
                txt_Igst_Percent.Text = (0).ToString();
                calculate_all_values_DC();
            }
        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        private void btn_Search_Click(object sender, EventArgs e)
        {
            if(btn_Search.Text == "Search")
            {
                btn_Save.Text = "Update";
                btn_Search.Text = "Cancel";
                panel21.Hide();
                panel29.Hide();
                cmb_Invoice_No.Show();
                txt_Invoice_No.Hide();
                label22.Hide();
                Panel_Invoice_For.Hide();
                load_invoice_Combo();
                panel57.Show();
                panel72.Show();
            }
            else if (btn_Search.Text == "Cancel")
            {
                btn_Save.Text = "Save";
                btn_Search.Text = "Search";
                cmb_Invoice_No.Hide();
                txt_Invoice_No.Show();
                label22.Show();
                panel57.Hide();
                panel72.Hide();
                Panel_Invoice_For.Show();
            }
        }
        private void load_invoice_Combo()
        {
            cmb_Invoice_No.Items.Clear();
            try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT DISTINCT Invoice_No FROM Tb_Tax_Sell_Details", con);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    cmb_Invoice_No.Items.Add(sdr[0]);
                }
                sdr.Close();
            }
            catch { }
            finally { con.Close(); }
        }
        private void cmb_Invoice_No_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmb_Invoice_No.Text != "")
            {
                table_panel_Entry.Enabled = false;
                panel21.Show();
                //panel21.Dock = DockStyle.Fill;
                dgv_Sales_Particular_Details.Show();
                dgv_Sales_Particular_Details.Dock = DockStyle.Fill;
                load_Invoice_Details();
                panel30.Show();
            }
            else
            {
                panel30.Hide();
            }
        }
        private void load_Invoice_Details()
        {
            //try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT Cust_Name, Sell_Date, Total_Items, Total_Quantity, Sgst_Amt, Cgst_Amt, Igst_Amt, Gst_Amt, Total_Amt, Net_Amt, Status FROM Tb_Tax_Sell_Details WHERE Invoice_No = '" + cmb_Invoice_No.Text + "'", con);
                SqlDataReader sdr1 = cmd.ExecuteReader();
                while (sdr1.Read())
                {
                    cmb_Customer_Name.Text = sdr1[0].ToString();
                    dtp_By_Date.Text = sdr1[1].ToString();
                    lbl_Total_Items.Text = sdr1[2].ToString();
                    lbl_Quantity.Text = sdr1[3].ToString();
                    lbl_SGST_Amt.Text = sdr1[4].ToString();
                    lbl_CGST_Amt.Text = sdr1[5].ToString();
                    lbl_IGST_Amt.Text = sdr1[6].ToString();
                    lbl_GST_Amt.Text = sdr1[7].ToString();
                    lbl_Total_Amt.Text = sdr1[8].ToString();
                    lbl_Net_Amt.Text = sdr1[9].ToString();
                    if(sdr1[10].ToString() == "Particular")
                    {
                        particular_or_DC = "Particular";
                        rdb_Particulars.Checked = true;
                        load_Particular_Details_On_Invoice();
                        panel21.Show();
                        panel29.Hide();
                    }
                    else if (sdr1[10].ToString() == "DC")
                    {
                        particular_or_DC = "DC";
                        rdb_DC_ALL.Checked = true;
                        load_Dc_Details_On_Invoice();
                        panel21.Hide();
                        panel29.Show();
                        dgv_DC_Show.Columns["status"].Visible = false;
                    }
                }
            }
        }
        private void load_Dc_Details_On_Invoice()
        {
            //try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT C_Dc_No, C_Dc_Date, Total_Items,Total_Amt FROM Tb_Tax_Content_DC WHERE Invoice_No = '" + cmb_Invoice_No.Text + "'", con);
                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];

                if (dt.Rows.Count == 0)
                {
                    dgv_DC_Show.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_DC_Show.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();
            }
            //catch { }
            //finally { con.Close(); }
        }
        private void load_Particular_Details_On_Invoice()
        {
            //try
            {
                getconnection();
                con.Open();

                cmd = new SqlCommand("SELECT Gas_Type, Particulers, HSN, Item_No, Sr_No, Rate, Discount, Quantity, Unit, Total, SGST, CGST, IGST, Taxable_Amt, Sell_Status FROM Tb_Tax_Sell_Content_Particular WHERE Invoice_No = '" + cmb_Invoice_No.Text + "'", con);

                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];

                if (dt.Rows.Count == 0)
                {
                    dgv_Sales_Particular_Details.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_Sales_Particular_Details.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();
            }
            //catch { }
            //finally { con.Close(); }
        }
        private void chkbox_Add_New_Product_CheckedChanged(object sender, EventArgs e)
        {
            if(chkbox_Add_New_Product.Checked == true)
            {
                table_panel_Entry.Enabled = true;
            }
        }
        private void panel60_Paint(object sender, PaintEventArgs e)
        {

        }
        private void chk_If_Cancel_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_If_Cancel.Checked == true)
            {
                if (cmb_Invoice_No.Text == "")
                {
                    chkbox_Add_New_Product.Enabled = true;
                    chkbox_Add_New_Product.Show();
                    MessageBox.Show("Select Invoice Number First.!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        cmd = new SqlCommand("UPDATE Tax_Sell_Details SET Status = 'Cancel' WHERE Invoice_No = '" + cmb_Invoice_No.Text.ToString() + "'", con);
                        cmd.ExecuteNonQuery();
                        btn_Save.PerformClick();
                    }
                    else if (result == DialogResult.No)
                    {
                        chkbox_Add_New_Product.Show();
                        chk_If_Cancel.Checked = false;
                    }
                }
            }
        }
        private void panel55_Paint(object sender, PaintEventArgs e)
        {

        }
        private void txt_Igst_Percent_TextChanged(object sender, EventArgs e)
        {
            /*if (txt_Igst_Percent.Text != "")
            {
                txt_Sgst_Percent.Enabled = false;
               // txt_Sgst_Percent.Text = "0";
                calculate_all_values_DC();
            }
            else
            {
                txt_Sgst_Percent.Enabled = true;
            }*/
            if (txt_Igst_Percent.Text == "")
            {
                txt_Sgst_Percent.Enabled = true;
                txt_Sgst_Percent.Clear();
            }
            else if (Convert.ToInt32(txt_Igst_Percent.Text) > 0)
            {
                txt_Sgst_Percent.Enabled = false;
                txt_Sgst_Percent.Text = (0).ToString();
                calculate_all_values_DC();
            }
        }
        private void dt_In_Date_ValueChanged(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmb_Item_No_TextChanged(object sender, EventArgs e)
        {
            if (cmb_Item_No.Text == "NA")
            {
                cmb_cy_Type.Enabled = true;
                cmb_Particulars.Enabled = true;
                txt_Quantity.Enabled = true;
                //txt_Hsn_Ssn.Enabled = true;
            }
            else if (cmb_Item_No.Text != "")
            {
                cmb_cy_Type.Enabled = false;
                cmb_Particulars.Enabled = false;
                txt_Quantity.Enabled = false;
                txt_Hsn_Ssn.Enabled = false;
                load_Details_from_Item_No();
            }
            else
            {
                cmb_cy_Type.Enabled = true;
                cmb_Particulars.Enabled = true;
                txt_Quantity.Enabled = true;
                //txt_Hsn_Ssn.Enabled = true;
            }
        }

        private void btn_Print_Click(object sender, EventArgs e)
        {
            if(btn_Search.Text == "Search")
            {
                print_Id = txt_Invoice_No.Text;
                if(particular_or_DC == "Particular")
                {
                    Report_Tax_Particular form = new Report_Tax_Particular();
                    form.get_ID(print_Id);
                    form.Show();
                }
                else
                {
                    Report_Tax_DC form = new Report_Tax_DC();
                    form.get_ID(print_Id);
                    form.Show();
                }
            }
            else
            {
                print_Id = cmb_Invoice_No.Text;
                if (particular_or_DC == "Particular")
                {
                    Report_Tax_Particular form = new Report_Tax_Particular();
                    form.get_ID(print_Id);
                    form.Show();
                }
                else
                {
                    Report_Tax_DC form = new Report_Tax_DC();
                    form.get_ID(print_Id);
                    form.Show();
                }
            }
        }
        private void load_Details_from_Item_No()
        {
            //try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT Cylinder_Type, HSN, Sr_No, Particulers FROM Tb_Purchase_Content_Master WHERE Part_No = '" + cmb_Item_No.Text.ToString() + "'", con);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    cmb_cy_Type.Text = sdr[0].ToString();
                    txt_Hsn_Ssn.Text = sdr[1].ToString();
                    lbl_Sr_No.Text = sdr[2].ToString();
                    cmb_Particulars.Text = sdr[3].ToString();
                }
                sdr.Close();
                // load_In_Stock_Value();
                txt_Quantity.Text = "1";
                txt_Discount.Text = "0";
                cmb_Unit.Text = "NOS";
                //cmb_Status.Text = "RENT";
            }
            //catch { }
            //finally { con.Close(); }
        }
        private void txt_Total_TextChanged(object sender, EventArgs e)
        {
            calculate_tax_amount();
        }

        private void txt_Rate_TextChanged(object sender, EventArgs e)
        {
            if (txt_Rate.Text != "" && txt_Discount.Text != "" && txt_Quantity.Text != "")
            {
                calculate_total();
            }
            else
            {
                txt_Total.Text = "0";
            }
        }
        private void txt_Discount_TextChanged(object sender, EventArgs e)
        {
            if (txt_Rate.Text != "" && txt_Discount.Text != "" && txt_Quantity.Text != "")
            {
                calculate_total();
            }
            else
            {
                txt_Total.Text = "0";
            }
        }
        private void txt_Quantity_TextChanged(object sender, EventArgs e)
        {
            if (txt_Rate.Text != "" && txt_Discount.Text != "" && txt_Quantity.Text != "")
            {
                calculate_total();
            }
            else
            {
                txt_Total.Text = "0";
            }
        }
        
        //FUNCTION TO CALCULATE TOTAL AMOUNT IN TEXT FIELD
        private void calculate_total()
        {
            int rate, quantity;
            decimal disc;
            rate = Convert.ToInt32(txt_Rate.Text);
            quantity = Convert.ToInt32(txt_Quantity.Text);
            disc = Convert.ToInt32(txt_Discount.Text);
            total = ((rate * quantity) * (100 - disc) / 100);
            txt_Total.Text = total.ToString();
        }
        private void txt_Sgst_TextChanged(object sender, EventArgs e)
        {
            txt_Cgst.Text = txt_Sgst.Text;
            if (txt_Sgst.Text == "")
            {
                txt_Igst.Enabled = true;
                txt_Igst.Clear();
            }
            else if (Convert.ToInt32(txt_Sgst.Text) != 0)
            {
                txt_Igst.Enabled = false;
                txt_Igst.Text = "0";
                calculate_tax_amount();
            }
        }
        private void txt_Igst_TextChanged(object sender, EventArgs e)
        {
            if (txt_Igst.Text == "")
            {
                txt_Sgst.Enabled = true;
                txt_Sgst.Clear();
            }
            else if (Convert.ToInt32(txt_Igst.Text) != 0)
            {
                txt_Sgst.Enabled = false;
                txt_Sgst.Text = (0).ToString();
                calculate_tax_amount();
            }
        }

        /*FUNCTION TO CALCULATE TAX AMOUNT
         * TAX_AMOUNT = TOTAL + (TOTAL * GST%)
         */
        public void calculate_tax_amount()
        {
            decimal tax_Amount;
            sgst = Convert.ToDecimal(txt_Sgst.Text);
            cgst = Convert.ToDecimal(txt_Cgst.Text);
            igst = Convert.ToDecimal(txt_Igst.Text);
            gst_Percent = Convert.ToDecimal(sgst + cgst + igst);
            tax_Amount = (total * (1 + (gst_Percent / 100)));
            txt_Taxable_Amount.Text = (tax_Amount.ToString());
        }
        private void clear_All_Data()
        {
            cmb_Customer_Name.ResetText();
            cmb_Invoice_No.ResetText();
            dt_In_Date.ResetText();
            lbl_Address.ResetText();
            lbl_CGST_Amt.ResetText();
            lblPhone.ResetText();
            lbl_Cgst_Percent.ResetText();
            lbl_Contact_No.ResetText();
            lbl_Contact_Person.ResetText();
            lbl_Contact_Person_Phone.ResetText();
            lbl_GST_Amt.ResetText();
            lbl_GST_No.ResetText();
            lbl_IGST_Amt.ResetText();
            lbl_In_Stock.ResetText();
            lbl_Net_Amt.ResetText();
            lbl_Pur_Date.ResetText();
            lbl_Quantity.ResetText();
            lbl_SGST_Amt.ResetText();
            lbl_Sr_No.ResetText();
            lbl_Total_Amt.ResetText();
            lbl_Total_Items.ResetText();

            if (dgv_Sales_Particular_Details.RowCount > 0)
                {
                    DataTable DT = (DataTable)dgv_Sales_Particular_Details.DataSource;
                    if (DT != null)
                        DT.Clear();
                }
                if (dgv_DC_Show.RowCount > 0)
                {
                    DataTable DT = (DataTable)dgv_DC_Show.DataSource;
                    if (DT != null)
                        DT.Clear();
                }
        }
    }
}
