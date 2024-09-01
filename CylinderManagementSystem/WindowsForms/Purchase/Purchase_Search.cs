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
    public partial class Purchase_Search : Form
    {
        SqlConnection con;
        SqlDataReader sdr;
        SqlCommand cmd;
        SqlDataAdapter adpt;
        DataTable dt;

        decimal gst_Percent, total;
        decimal sgst, cgst, igst, total_Sgst, total_Cgst, total_Igst;
        int in_Stock_Value = 0, in_stock_temp = 0;
        Boolean edit = false;
        public Purchase_Search()
        {
            InitializeComponent();
            set_textbox_default();
            load_cmb_Supplier_name();
            load_supplier_details();
        }
        public void getconnection()
        {
            string str = ConfigurationManager.ConnectionStrings["CylinderData"].ConnectionString;
            con = new SqlConnection(str);
        }
     

            private void cmb_Supplier_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            label15.Text = cmb_Supplier_Name.Text;

            if (cmb_Supplier_Name.Text != "")
            {

                load_cmb_Invoice_No();
                chk_If_Cancel.Enabled = true;
                load_supplier_details();
            }
            else
            {
                chk_If_Cancel.Enabled = false;
            }


        }

        //LOAD SUPPLIER LIST INTO SUPPLIER COMBO BOX
        public void load_cmb_Supplier_name()
        {
            try
            {
                getconnection();
                con.Open();

                cmd = new SqlCommand("SELECT DISTINCT Supplier_Name FROM Tb_Purchase", con);
                sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    this.cmb_Supplier_Name.Items.Add(sdr[0]);
                }
                sdr.Close();
            }
            catch { }
            finally { con.Close(); }
        }

        //LOAD INVOICE LIST INTO INVOICE COMBO BOX
        public void load_cmb_Invoice_No()
        {
            cmb_Invoice_No.Items.Clear();
            string supp_Name = cmb_Supplier_Name.Text;
            try
            {
                getconnection();
                con.Open();

                cmd = new SqlCommand("SELECT DISTINCT Invoice_No FROM Tb_Purchase WHERE Supplier_Name = '" + supp_Name + "' AND Cancel_Status = 'NA'", con);
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

        //LOAD DETAILS OF SUPPLIER IN RESPECTIVE TEXTBOXES
        public void load_supplier_details()
        {
            try
            {
                getconnection();
                con.Open();

                string supp_Name = cmb_Supplier_Name.Text.ToString();
                cmd = new SqlCommand("SELECT Supp_Address, Supp_Phone, Supp_ConPerson, Supp_GST FROM Tb_Supplier_Master WHERE Supp_CompName = '" + supp_Name + "'", con);
                sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    txt_Address.Text = sdr[0].ToString();
                    txt_Contact.Text = sdr[1].ToString();
                    txt_Contact_Person_Name.Text = sdr[2].ToString();
                    txt_Gst_No.Text = sdr[3].ToString();
                }
                sdr.Close();
            }
            catch { }
            finally { con.Close(); }
        }

        private void cmb_Invoice_No_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            {
                getconnection();
                con.Open();

                string invoice_No = cmb_Invoice_No.Text.ToString();
                cmd = new SqlCommand("SELECT Cylinder_Type, Particulers, HSN, Sr_No, Part_No, Rate, Disc, Quantity, Unit, Total, SGST, CGST, IGST, TaxableAmt, Purchase_Type FROM Tb_Purchase_Content_Master WHERE Invoice_No ='" + invoice_No + "'", con);

                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];

                if (dt.Rows.Count == 0)
                {
                    dgv_Purchase_Details_Group.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_Purchase_Details_Group.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();
            }
            //catch { }
            //finally { con.Close(); }

            //calculate_all_values();
            load_Purchase_Data();
        }

        private void dgv_Purchase_Details_Group_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string value = dgv_Purchase_Details_Group.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

            if (value == "Edit")
            {
                if (btn_Add.Text == "UPDATE")
                {
                    MessageBox.Show("Cannot Add Record While Another is in the Process", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    edit = true;
                    DataGridViewRow row = dgv_Purchase_Details_Group.Rows[e.RowIndex];
                    cmb_category.Text = row.Cells["category"].Value.ToString();
                    cmb_cy_Type.Text = row.Cells["c_type"].Value.ToString();
                    txt_Particular.Text = row.Cells["particular"].Value.ToString();
                    txt_Hsn_Ssn.Text = row.Cells["hsn"].Value.ToString();
                    txt_Serial_No.Text = row.Cells["serial"].Value.ToString();
                    txt_Part_No.Text = row.Cells["part"].Value.ToString();
                    txt_Rate.Text = row.Cells["rate"].Value.ToString();
                    txt_Discount.Text = row.Cells["discount"].Value.ToString();
                    txt_Quantity.Text = row.Cells["quantity"].Value.ToString();
                    cmb_Unit.Text = row.Cells["unit"].Value.ToString();
                    txt_Total.Text = row.Cells["total_"].Value.ToString();
                    txt_Sgst.Text = row.Cells["sgst_"].Value.ToString();
                    txt_Cgst.Text = row.Cells["cgst_"].Value.ToString();
                    txt_Igst.Text = row.Cells["igst_"].Value.ToString();
                    txt_Taxable_Amount.Text = row.Cells["with_gst"].Value.ToString();

                    dgv_Purchase_Details_Group.Rows.RemoveAt(e.RowIndex);
                    table_Panel_Entry.Enabled = true;
                    txt_Part_No.Enabled = false;
                    //txt_Serial_No.Enabled = false;
                    txt_Quantity.Enabled = false;
                    txt_Particular.Enabled = false;
                    cmb_cy_Type.Enabled = false;
                    cmb_category.Enabled = false;
                    cmb_Invoice_No.Enabled = false;

                    in_stock_temp = Convert.ToInt32(txt_Quantity.Text);
                    // update_data_grid(sender, e);
                    btn_Add.Text = "UPDATE";
                    btn_Update.Enabled = false;
                }
            }
            else if (value == "X")
            {
                DialogResult result = MessageBox.Show("Are You Sure.?", "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    if(dgv_Purchase_Details_Group.Rows[e.RowIndex].Cells["update"].Value == null)
                    {
                        string particular = dgv_Purchase_Details_Group.Rows[e.RowIndex].Cells["particular"].Value.ToString();
                        string item = dgv_Purchase_Details_Group.Rows[e.RowIndex].Cells["part"].Value.ToString();
                        getconnection();
                        con.Open();

                        calculate_In_Stock_Value(particular);
                        in_Stock_Value = in_Stock_Value - Convert.ToInt32(dgv_Purchase_Details_Group.Rows[e.RowIndex].Cells["quantity"].Value);
                        cmd = new SqlCommand("UPDATE Tb_Inventory_Master SET In_Stock = '" + in_Stock_Value.ToString() + "' WHERE Particulars = '" + particular + "'", con);
                        cmd.ExecuteNonQuery();

                        cmd = new SqlCommand("DELETE FROM Tb_Purchase_Content_Master WHERE Particulers = '" + particular + "' AND Part_No = '" + item + "'", con);
                        cmd.ExecuteNonQuery();

                        dgv_Purchase_Details_Group.Rows.RemoveAt(e.RowIndex);
                        cmb_Invoice_No.Enabled = false;
                    }
                    else if(dgv_Purchase_Details_Group.Rows[e.RowIndex].Cells["update"].Value.ToString() == "add")
                    {
                        dgv_Purchase_Details_Group.Rows.RemoveAt(e.RowIndex);
                        cmb_Invoice_No.Enabled = false;
                    }
                }
            }
            calculate_all_values();
        }

        //UPDATION OF DATAGRIDVIEW ON CLICK OF "EDIT" & "X" (REMOVE)
        public void update_data_grid(object sender, DataGridViewCellEventArgs e)
        {
            int limit = dgv_Purchase_Details_Group.RowCount;
            int index = e.RowIndex, temp;
            for (int i = index; i < limit; i++)
            {
                temp = Convert.ToInt32(dgv_Purchase_Details_Group.Rows[i].Cells[2].Value);
                dgv_Purchase_Details_Group.Rows[i].Cells[2].Value = (temp - 1).ToString();
            }
            calculate_all_values();
        }

        public void calculate_all_values()
        {
            decimal total_amt = 0, gst_amt = 0, total_gst_amt = 0, total = 0, quantity = 0, total_quantity = 0;

            total_Sgst = total_Cgst = total_Igst = total_amt = gst_amt = total_gst_amt = 0;

            int rowcount = dgv_Purchase_Details_Group.RowCount;
            if (rowcount > 0)
            {
                for (int i = 0; i < rowcount; i++)
                {
                    total = Convert.ToDecimal(dgv_Purchase_Details_Group.Rows[i].Cells["total_"].Value.ToString());
                    cgst = Convert.ToDecimal(dgv_Purchase_Details_Group.Rows[i].Cells["cgst_"].Value.ToString());
                    sgst = Convert.ToDecimal(dgv_Purchase_Details_Group.Rows[i].Cells["sgst_"].Value.ToString());
                    igst = Convert.ToDecimal(dgv_Purchase_Details_Group.Rows[i].Cells["igst_"].Value.ToString());
                    quantity = Convert.ToDecimal(dgv_Purchase_Details_Group.Rows[i].Cells["quantity"].Value.ToString());

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
            lbl_Total_Items.Text = total_quantity.ToString();
            lbl_Sgst_Percent.Text = (total_Sgst).ToString();
            lbl_Cgst_Percent.Text = (total_Cgst).ToString();
            lbl_Igst_Percent.Text = (total_Igst).ToString();
            lbl_Total_Amt.Text = total_amt.ToString();
            lbl_GST_Amt.Text = total_gst_amt.ToString("0.00");
            lbl_Net_Amt.Text = Convert.ToInt64((total_amt + total_gst_amt)).ToString("");

            //CALCULATE SGST, CGST, IGST AMOUNT
            lbl_SGST_Amt.Text = (total_amt * total_Sgst / 100).ToString("00.00");
            lbl_CGST_Amt.Text = (total_amt * total_Cgst / 100).ToString("00.00");
            lbl_IGST_Amt.Text = (total_amt * total_Igst / 100).ToString("00.00");
            lbl_Balance_Amt.Text = (Convert.ToInt64(lbl_Net_Amt.Text) - Convert.ToInt64(txt_Paid_Amt.Text)).ToString();
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
            else if (txt_Particular.Text == "")
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
                if (btn_Add.Text == "ADD")
                {
                    if (cmb_category.Text == "CYLINDER")
                    {
                        int quantity = Convert.ToInt32(txt_Quantity.Text);
                        for (int i = 0; i < quantity; i++)
                        {
                            DataTable table = new DataTable();
                            table = (DataTable)dgv_Purchase_Details_Group.DataSource;
                            DataRow row = table.NewRow();

                            row["Particulers"] = txt_Particular.Text;
                            row["Cylinder_Type"] = cmb_cy_Type.Text;
                            row["HSN"] = txt_Hsn_Ssn.Text;
                            row["Rate"] = txt_Rate.Text;
                            row["Disc"] = txt_Discount.Text;
                            row["Quantity"] = "1";
                            row["Unit"] = cmb_Unit.Text;
                            row["Total"] = (Convert.ToInt32(txt_Total.Text) / quantity).ToString().Trim();
                            row["SGST"] = txt_Sgst.Text;
                            row["CGST"] = txt_Cgst.Text;
                            row["IGST"] = txt_Igst.Text;
                            row["TaxableAmt"] = (Convert.ToDecimal(txt_Taxable_Amount.Text) / quantity).ToString().Trim();
                            row["Purchase_Type"] = cmb_category.Text;
                            row["Sr_No"] = "";
                            row["Part_No"] = "";
                           
                            if (!table.Columns.Contains("update"))
                            {
                                table.Columns.Add("update");
                            }
                            row["update"] = "add";
                            table.Rows.Add(row);
                            table.AcceptChanges();
                        }
                    }
                    else
                    {
                        DataTable table = new DataTable();
                        table = (DataTable)dgv_Purchase_Details_Group.DataSource;
                        DataRow row = table.NewRow();

                        row["Particulers"] = txt_Particular.Text;
                        row["Cylinder_Type"] = cmb_cy_Type.Text;
                        row["HSN"] = txt_Hsn_Ssn.Text;
                        row["Rate"] = txt_Rate.Text;
                        row["Disc"] = txt_Discount.Text;
                        row["Quantity"] = txt_Quantity.Text;
                        row["Unit"] = cmb_Unit.Text;
                        row["Total"] = txt_Total.Text;
                        row["SGST"] = txt_Sgst.Text;
                        row["CGST"] = txt_Cgst.Text;
                        row["IGST"] = txt_Igst.Text;
                        row["TaxableAmt"] = txt_Taxable_Amount.Text;
                        row["Purchase_Type"] = cmb_category.Text;
                        row["Sr_No"] = "NA";
                        row["Part_No"] = "NA";

                        table.Rows.Add(row);
                        table.AcceptChanges();
                    }

                    calculate_all_values();
                    calculate_Balance_Amt();
                    edit = false;
                    btn_Add.Text = "ADD";
                    btn_Update.Enabled = true;
                    clear_Table_Panel();
                }

                else if (btn_Add.Text == "UPDATE")
                {
                    getconnection();
                    con.Open();

                    cmd = new SqlCommand("Update_Purchase_Content_Details", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Purchase_Type", cmb_category.Text.ToString().Trim()); 
                   cmd.Parameters.AddWithValue("@Cust_Supp_Name", cmb_Supplier_Name.Text.ToString().Trim());
                    cmd.Parameters.AddWithValue("@Particulers", txt_Particular.Text.ToString().Trim());
                    cmd.Parameters.AddWithValue("@HSN", txt_Hsn_Ssn.Text.ToString().Trim());
                    cmd.Parameters.AddWithValue("@Rate", txt_Rate.Text.ToString().Trim());
                    cmd.Parameters.AddWithValue("@Sr_No", txt_Serial_No.Text.ToString().Trim());
                    cmd.Parameters.AddWithValue("@Part_No", txt_Part_No.Text.ToString().Trim());
                    cmd.Parameters.AddWithValue("@Disc ", txt_Discount.Text.ToString().Trim());
                    cmd.Parameters.AddWithValue("@Quantity", txt_Quantity.Text.ToString().Trim());
                    cmd.Parameters.AddWithValue("@Unit", cmb_Unit.Text.ToString().Trim());
                    cmd.Parameters.AddWithValue("@Total", txt_Total.Text.ToString().Trim());
                    cmd.Parameters.AddWithValue("@SGST", txt_Sgst.Text.ToString().Trim());
                    cmd.Parameters.AddWithValue("@CGST", txt_Cgst.Text.ToString().Trim());
                    cmd.Parameters.AddWithValue("@IGST", txt_Igst.Text.ToString().Trim());
                    cmd.Parameters.AddWithValue("@TaxableAmt", txt_Taxable_Amount.Text.ToString().Trim());
                    cmd.Parameters.AddWithValue("@Cylinder_Type", cmb_cy_Type.Text.ToString().Trim());
                    cmd.Parameters.AddWithValue("@invoice_No", cmb_Invoice_No.Text.ToString().Trim());

                    cmd.ExecuteNonQuery();

                    txt_Quantity.Enabled = true;
                    txt_Part_No.Enabled = true;
                    txt_Serial_No.Enabled = true;
                    txt_Particular.Enabled = true;
                    cmb_cy_Type.Enabled = true;
                    cmb_category.Enabled = true;

                    if (chkbox_Add_New_Product.Checked == true)
                    {
                        table_Panel_Entry.Enabled = true;
                    }
                    else
                    {
                        table_Panel_Entry.Enabled = false;
                    }

                    DataTable table = new DataTable();
                    table = (DataTable)dgv_Purchase_Details_Group.DataSource;
                    DataRow row = table.NewRow();

                    row["Particulers"] = txt_Particular.Text;
                    row["Cylinder_Type"] = cmb_cy_Type.Text;
                    row["HSN"] = txt_Hsn_Ssn.Text;
                    row["Rate"] = txt_Rate.Text;
                    row["Disc"] = txt_Discount.Text;
                    row["Quantity"] = txt_Quantity.Text;
                    row["Unit"] = cmb_Unit.Text;
                    row["Total"] = txt_Total.Text;
                    row["SGST"] = txt_Sgst.Text;
                    row["CGST"] = txt_Cgst.Text;
                    row["IGST"] = txt_Igst.Text;
                    row["TaxableAmt"] = txt_Taxable_Amount.Text;
                    row["Purchase_Type"] = cmb_category.Text;
                    row["Sr_No"] = "NA";
                    row["Part_No"] = "NA";

                    table.Rows.Add(row);
                    table.AcceptChanges();


                    calculate_all_values();
                    calculate_Balance_Amt();
                    edit = false;
                    btn_Add.Text = "ADD";
                    btn_Update.Enabled = true;
                    clear_Table_Panel();
                }
                
            }

        }

        public void calculate_In_Stock_Value(string particular)
        {
            {
                getconnection();
                con.Open();

                SqlCommand command = new SqlCommand("SELECT In_Stock FROM Tb_Inventory_Master WHERE Particulars = '" + particular + "'", con);
                sdr = command.ExecuteReader();
                while (sdr.Read())
                {
                    in_Stock_Value = Convert.ToInt32(sdr[0]);
                }
                sdr.Close();
            }
        }

        private void add_New_Item(DataGridViewRow row)
        {
            //iNSERT CONTENT IN PURCHASE CONTENT
            //try
            {
                getconnection();
                con.Open();

                cmd = new SqlCommand("Insert_Purchase_Content_Details", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Purchase_Type", row.Cells["category"].Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@Particulers", row.Cells["particular"].Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@Cust_Supp_Name", cmb_Supplier_Name.Text.Trim());
                cmd.Parameters.AddWithValue("@HSN", row.Cells["hsn"].Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@Rate", row.Cells["rate"].Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@Sr_No", row.Cells["serial"].Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@Part_No", row.Cells["part"].Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@Disc ", row.Cells["discount"].Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@Quantity", row.Cells["quantity"].Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@Unit", row.Cells["unit"].Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@Total", row.Cells["total_"].Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@SGST", row.Cells["sgst_"].Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@CGST", row.Cells["cgst_"].Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@IGST", row.Cells["igst_"].Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@TaxableAmt", row.Cells["with_gst"].Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@Cylinder_Type", row.Cells["c_type"].Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@invoice_No", cmb_Invoice_No.Text.ToString().Trim());
                cmd.Parameters.AddWithValue("@cylinder_Status", "EMPTY");

                cmd.ExecuteNonQuery();
                cmb_Invoice_No.Enabled = false;
            }
            //catch { }

            //INSERT/UPDATE INVENTORY
            //try
            {
                getconnection();
                con.Open();

                string particular = row.Cells["particular"].Value.ToString();
                cmd = new SqlCommand("SELECT Particulars FROM Tb_Inventory_Master WHERE Particulars = '" + particular + "'", con);
                sdr = cmd.ExecuteReader();

                if (!sdr.Read())
                {
                    sdr.Close();
                    cmd = new SqlCommand("Insert_Inventory_Master", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Particulars", row.Cells["particular"].Value.ToString());
                    cmd.Parameters.AddWithValue("@Type", row.Cells["category"].Value.ToString());
                    cmd.Parameters.AddWithValue("@In_stock", row.Cells["quantity"].Value.ToString());
                    cmd.Parameters.AddWithValue("@Out_stock", 0.ToString());
                    cmd.Parameters.AddWithValue("@Cylinder_Type", row.Cells["c_type"].Value.ToString());

                    cmd.ExecuteNonQuery();
                }

                else
                {
                    sdr.Close();
                    cmd = new SqlCommand("UPDATE Tb_Inventory_Master SET In_Stock = In_Stock + " + row.Cells["quantity"].Value.ToString() + " WHERE Particulars = '" + row.Cells["particular"].Value.ToString() + "'", con);
                    cmd.ExecuteNonQuery();
                }
            }

            //catch { }
        }
        

        public void set_textbox_default()
        {
            txt_Sgst.Text = txt_Igst.Text = txt_Cgst.Text = txt_Discount.Text = 0.ToString();
            txt_Total.Text = 0.ToString();
            txt_Paid_Amt.Text = 0.ToString();
            txt_Taxable_Amount.Text = 0.ToString();
            cmb_Unit.Text = "NOS";
        }

        private void cmb_category_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_category.Text == "CYLINDER")
            {
                if (btn_Add.Text == "UPDATE")
                {
                    table_Panel_Entry.ColumnStyles[4].SizeType = SizeType.Percent;
                    table_Panel_Entry.ColumnStyles[4].Width = 8;
                    table_Panel_Entry.ColumnStyles[3].SizeType = SizeType.Percent;
                    table_Panel_Entry.ColumnStyles[3].Width = 8;
                }
                else if (btn_Add.Text == "ADD")
                {
                    table_Panel_Entry.ColumnStyles[4].SizeType = SizeType.Percent;
                    table_Panel_Entry.ColumnStyles[4].Width = 0;
                    table_Panel_Entry.ColumnStyles[3].SizeType = SizeType.Percent;
                    table_Panel_Entry.ColumnStyles[3].Width = 0;
                }
                table_Panel_Entry.ColumnStyles[1].SizeType = SizeType.Percent;
                table_Panel_Entry.ColumnStyles[1].Width = 8;
                
                txt_Serial_No.Text = "";
                txt_Part_No.Text = "";
                cmb_cy_Type.Text = "";
                txt_Hsn_Ssn.Text = 40090.ToString();
                txt_Particular.Enabled = true;
                load_cmb_cy_Type();
                load_cmb_Particulars();
            }
            else if (cmb_category.Text == "MATERIAL")
            {


                       
                table_Panel_Entry.ColumnStyles[1].SizeType = SizeType.Percent;
                table_Panel_Entry.ColumnStyles[1].Width = 0;
                table_Panel_Entry.ColumnStyles[4].SizeType = SizeType.Percent;
                table_Panel_Entry.ColumnStyles[4].Width = 0;
                table_Panel_Entry.ColumnStyles[3].SizeType = SizeType.Percent;
                table_Panel_Entry.ColumnStyles[3].Width = 0;
                txt_Serial_No.Text = "NA";
                txt_Part_No.Text = "NA";
                cmb_cy_Type.Text = "NA";
                txt_Hsn_Ssn.Text = "";
                load_cmb_Particulars();
            }
        }

        public void load_cmb_cy_Type()
        {
            cmb_cy_Type.Items.Clear();
            try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT distinct Cylinder_Type FROM Tb_Purchase_Content_Master WHERE Purchase_Type = 'CYLINDER'", con);
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

        //FUNCTION FOR VALUE CHANGE OF TEXT FIELD "RATE" 
        private void txt_Rate_TextChanged(object sender, EventArgs e)
        {
            if (txt_Rate.Text == "" || txt_Discount.Text == "" || txt_Quantity.Text == "")
            {

            }
            else
            {
                calculate_total();
            }
        }

        //FUNCTION FOR VALUE CHANGE OF TEXT FIELD "DISCOUNT"
        private void txt_Discount_TextChanged(object sender, EventArgs e)
        {
            if (txt_Discount.Text == "" || txt_Quantity.Text == "" || txt_Rate.Text == "")
            {

            }
            else
            {
                calculate_total();
            }
        }

        //FUNCTION FOR VALUE CHANGE OF TEXT FIELD "QUANTITY"
        private void txt_Quantity_TextChanged(object sender, EventArgs e)
        {
            if (txt_Quantity.Text == "" || txt_Rate.Text == "" || txt_Discount.Text == "")
            {

            }
            else
            {
                calculate_total();
            }
        }

        //FUNCTIONS TO CLEAR TEXT FIELDS
        public void clear_data()
        {
            txt_Particular.ResetText();
            txt_Serial_No.Clear();
            txt_Part_No.Clear();
            txt_Hsn_Ssn.Clear();
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

            set_textbox_default();

        }

        //FUNCTION TO CALCULATE TOTAL AMOUNT IN TEXT FIELD
        public void calculate_total()
        {
            int rate, quantity;
            decimal disc;
            rate = Convert.ToInt32(txt_Rate.Text);
            quantity = Convert.ToInt32(txt_Quantity.Text);
            disc = Convert.ToInt32(txt_Discount.Text);

            total = ((rate * quantity) * (100 - disc) / 100);
            txt_Total.Text = total.ToString();
        }

        private void txt_Paid_Amt_TextChanged(object sender, EventArgs e)
        {
            if (txt_Paid_Amt.Text != "")
            {
                calculate_Balance_Amt();
            }
        }

        public void calculate_Balance_Amt()
        {
            decimal net, paid, balance;
            net = Convert.ToDecimal(lbl_Net_Amt.Text);
            paid = Convert.ToDecimal(txt_Paid_Amt.Text);
            balance = Convert.ToDecimal(lbl_Balance_Amt.Text); ;
            if (paid > net)
            {
                MessageBox.Show("paid Amount is Greater");
            }
            else
            {
                lbl_Balance_Amt.Text = (net - paid).ToString();
            }
        }

        private void txt_New_Paid_Amt_Leave(object sender, EventArgs e)
        {
            if (txt_New_Paid_Amt.Text != "")
            {
                decimal new_paid, balance, old_paid;
                new_paid = Convert.ToDecimal(txt_New_Paid_Amt.Text);
                old_paid = Convert.ToDecimal(txt_Paid_Amt.Text);
                balance = Convert.ToDecimal(lbl_Balance_Amt.Text);
                txt_Paid_Amt.Text = (old_paid + new_paid).ToString();
                lbl_Balance_Amt.Text = (balance - new_paid).ToString();
                txt_New_Paid_Amt.Text = "";
            }
        }

        private void lbl_Net_Amt_TextChanged(object sender, EventArgs e)
        {
            // calculate_Balance_Amt();
        }

        public void load_cmb_Particulars()
        {
            txt_Particular.Items.Clear();
            try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT distinct Particulers FROM Tb_Purchase_Content_Master WHERE Purchase_Type = '" + cmb_category.Text.ToString() + "'", con);
                sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    txt_Particular.Items.Add(sdr[0]);
                }
                sdr.Close();
            }
            catch { }
            finally { con.Close(); }
        }

        private void dgv_Purchase_Details_Group_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dgv_Purchase_Details_Group.Rows[e.RowIndex].Cells["sr"].Value = (e.RowIndex + 1).ToString();
        }

        private void txt_Particular_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txt_Total_TextChanged(object sender, EventArgs e)
        {
            calculate_tax_amount();
        }

        private void chk_If_Cancel_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_If_Cancel.Checked == true)
            {
                DialogResult result = MessageBox.Show("Are You Sure?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    chkbox_Add_New_Product.Hide();
                    getconnection();
                    con.Open();

                    cmd = new SqlCommand("UPDATE Tb_Purchase_Content_Master SET Cylinder_Status = 'CANCEL' WHERE Invoice_No = '" + cmb_Invoice_No.Text + "'", con);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("UPDATE Tb_Purchase SET Cancel_Status = 'CANCEL' WHERE Invoice_No = '" + cmb_Invoice_No.Text + "'", con);
                    cmd.ExecuteNonQuery();

                    foreach (DataGridViewRow row in dgv_Purchase_Details_Group.Rows)
                    {
                        string particular = row.Cells["particular"].Value.ToString();
                        string item = row.Cells["part"].Value.ToString();

                        calculate_In_Stock_Value(particular);
                        in_Stock_Value = in_Stock_Value - Convert.ToInt32(row.Cells["quantity"].Value);
                        cmd = new SqlCommand("UPDATE Tb_Inventory_Master SET In_Stock = '" + in_Stock_Value.ToString() + "' WHERE Particulars = '" + particular + "'", con);
                        cmd.ExecuteNonQuery();

                        //dgv_Purchase_Details_Group.Rows.RemoveAt(row.Index);
                    }

                    clear_All_Data();
                    clear_data();
                }
                else if (result == DialogResult.No)
                {
                    chk_If_Cancel.Checked = false;
                }
            }
        }

        private void Purchase_Search_Load(object sender, EventArgs e)
        {
            panel_Payment_Reference.Hide();
            panel_Payment_Reference.Hide();
            chk_If_Cancel.Enabled = false;
            Panel_Payment_Mode.Hide();
            table_Panel_Entry.Enabled = false;

            txt_Contact.Enabled = false;
            txt_Contact_Person_Name.Enabled = false;
            txt_Gst_No.Enabled = false;
            dt_In_Date.Enabled = false;
            txt_Address.Enabled = false;
            table_Panel_Entry.ColumnStyles[4].SizeType = SizeType.Percent;
            table_Panel_Entry.ColumnStyles[4].Width = 0;
            table_Panel_Entry.ColumnStyles[3].SizeType = SizeType.Percent;
            table_Panel_Entry.ColumnStyles[3].Width = 0;
        }

        private void chkbox_Add_New_Product_CheckedChanged(object sender, EventArgs e)
        {
            if (chkbox_Add_New_Product.Checked == true)
            {
                table_Panel_Entry.Enabled = true;
            }
            else
            {
                table_Panel_Entry.Enabled = false;
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

        //FUNCTION FOR TEXT CHANGE "SGST"
        private void txt_Sgst_TextChanged(object sender, EventArgs e)
        {
            txt_Cgst.Text = txt_Sgst.Text;
            if (txt_Sgst.Text == "")
            {
                txt_Igst.Enabled = true;
                txt_Igst.Clear();
            }
            else if (Convert.ToInt32(txt_Sgst.Text) > 0)
            {
                txt_Igst.Enabled = false;
                txt_Igst.Text = (0).ToString();
                calculate_tax_amount();
            }
        }

        //FUNCTION FOR TEXT CHANGE "IGST"
        private void txt_Igst_TextChanged(object sender, EventArgs e)
        {
            if (txt_Igst.Text == "")
            {
                txt_Sgst.Enabled = true;
                txt_Sgst.Clear();
            }
            else if (Convert.ToInt32(txt_Igst.Text) > 0)
            {
                txt_Sgst.Enabled = false;
                txt_Sgst.Text = (0).ToString();
                calculate_tax_amount();
            }
        }

        private void btn_Add_TextChanged(object sender, EventArgs e)
        {
            if (btn_Add.Text == "UPDATE")
            {
                table_Panel_Entry.ColumnStyles[4].SizeType = SizeType.Percent;
                table_Panel_Entry.ColumnStyles[4].Width = 8;
                table_Panel_Entry.ColumnStyles[3].SizeType = SizeType.Percent;
                table_Panel_Entry.ColumnStyles[3].Width = 8;
            }
            else if (btn_Add.Text == "ADD")
            {
                table_Panel_Entry.ColumnStyles[4].SizeType = SizeType.Percent;
                table_Panel_Entry.ColumnStyles[4].Width = 0;
                table_Panel_Entry.ColumnStyles[3].SizeType = SizeType.Percent;
                table_Panel_Entry.ColumnStyles[3].Width = 0;
            }
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void cmb_cy_Type_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {
            label15.Text = cmb_Supplier_Name.Text;
        }

        private void txt_New_Paid_Amt_TextChanged(object sender, EventArgs e)
        {

        }

        public void load_Purchase_Data()
        {
            txt_Paid_Amt.Text = 0.ToString();
            try
            {
                getconnection();
                con.Open();

                string Invoice_No = cmb_Invoice_No.Text;
                cmd = new SqlCommand("SELECT SGST_Per,SGST_Amt,CGST_Per,CGST_Amt,IGST_Per,IGST_Amt,Total_Amt,GST_Amt,Net_Amt,Paid_Amt,Balance_Amt,Payment_Mode,Reff_No,Date_Of_Purchase,Total_Items,Cancel_Status FROM Tb_Purchase WHERE Invoice_No =" + Invoice_No + "", con);
                sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    lbl_Sgst_Percent.Text = sdr[0].ToString();
                    lbl_SGST_Amt.Text = sdr[1].ToString();
                    lbl_Cgst_Percent.Text = sdr[2].ToString();
                    lbl_CGST_Amt.Text = sdr[3].ToString();
                    lbl_Igst_Percent.Text = sdr[4].ToString();
                    lbl_IGST_Amt.Text = sdr[5].ToString();
                    lbl_Total_Amt.Text = sdr[6].ToString();
                    lbl_GST_Amt.Text = sdr[7].ToString();
                    lbl_Net_Amt.Text = sdr[8].ToString();
                    txt_Paid_Amt.Text = sdr[9].ToString();
                    lbl_Balance_Amt.Text = sdr[10].ToString();

                    if (sdr[11].ToString() == "Cash")
                    {
                        rdbtn_Cash.Checked = true;
                        rdbtn_Cheque.Checked = false;
                        rdbtn_Other.Checked = false;
                    }
                    else if (sdr[11].ToString() == "Cheque")
                    {
                        rdbtn_Cash.Checked = false;
                        rdbtn_Cheque.Checked = true;
                        rdbtn_Other.Checked = false;
                    }
                    else if (sdr[11].ToString() == "Other")
                    {
                        rdbtn_Cash.Checked = false;
                        rdbtn_Cheque.Checked = false;
                        rdbtn_Other.Checked = true;
                    }
                    else if (sdr[11].ToString() == "NA")
                    {
                        rdbtn_Cash.Checked = false;
                        rdbtn_Cheque.Checked = false;
                        rdbtn_Other.Checked = false;
                    }
                    txt_Reff_No.Text = sdr[12].ToString();
                    dt_In_Date.Value = Convert.ToDateTime(sdr[13]);
                    lbl_Total_Items.Text = sdr[14].ToString();
                    if (sdr[15].ToString() == "CANCEL")
                    {
                        set_Invoice_Cancel();
                    }
                }
                sdr.Close();
            }
            catch { }
            finally { con.Close(); }
        }

        private void set_Invoice_Cancel()
        {
            chk_If_Cancel.Checked = true;
            chkbox_Add_New_Product.Enabled = false;
            dgv_Purchase_Details_Group.Enabled = false;
            panel21.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (cmb_Supplier_Name.Text == "")
            {
                MessageBox.Show("Please Select The Supplier First", "STOP", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else if (dt_In_Date.Text == "")
            {
                MessageBox.Show("Please Select The date First", "STOP", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else if (cmb_Invoice_No.Text == "")
            {
                MessageBox.Show("Please Enter The Invoice No", "STOP", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else if (!itemno())
            {
                MessageBox.Show("Please Enter Serial No and Part No for All Cylinders", "STOP", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                // Ensure the column "update" exists in the DataGridView before proceeding
                if (!dgv_Purchase_Details_Group.Columns.Contains("update"))
                {
                    throw new ArgumentException("Column named 'update' cannot be found.");
                }

                foreach (DataGridViewRow row in dgv_Purchase_Details_Group.Rows)
                {
                    // Check if the row is a new row or a valid data row
                    if (!row.IsNewRow)
                    {
                        var cell = row.Cells["update"];
                        var cellValue = cell.Value;

                        if (cellValue != null && cellValue.ToString() == "add")
                        {
                            add_New_Item(row);
                        }
                        else if (cellValue == null)
                        {
                            // Handle the case where the cell value is null
                            // For example, you could log an error or notify the user
                        }
                    }
                }


                // try
                {
                    getconnection();
                    con.Open();

                    cmd = new SqlCommand("Update_Purchase_Details", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    string payment_Mode = rdbtn_Purchase_Mode();
                    cmd.Parameters.AddWithValue("@Invoice_No", cmb_Invoice_No.Text.Trim());
                    cmd.Parameters.AddWithValue("@Supplier_Name", label15.Text);
                    cmd.Parameters.AddWithValue("@SGST_Per", lbl_Sgst_Percent.Text.Trim());
                    cmd.Parameters.AddWithValue("@SGST_Amt", lbl_SGST_Amt.Text.Trim());
                    cmd.Parameters.AddWithValue("@CGST_Per", lbl_Cgst_Percent.Text.Trim());
                    cmd.Parameters.AddWithValue("@CGST_Amt", lbl_CGST_Amt.Text.Trim());
                    cmd.Parameters.AddWithValue("@IGST_Per", lbl_Igst_Percent.Text.Trim());
                    cmd.Parameters.AddWithValue("@IGST_Amt", lbl_IGST_Amt.Text.Trim());
                    cmd.Parameters.AddWithValue("@total_items", lbl_Total_Items.Text.Trim());
                    cmd.Parameters.AddWithValue("@Total_Amt", lbl_Total_Amt.Text.Trim());
                    cmd.Parameters.AddWithValue("@GST_Amt", lbl_GST_Amt.Text.Trim());
                    cmd.Parameters.AddWithValue("@Net_Amt", lbl_Net_Amt.Text.Trim());
                    cmd.Parameters.AddWithValue("@Paid_Amt", txt_Paid_Amt.Text.Trim());
                    cmd.Parameters.AddWithValue("@Balance_Amt", lbl_Balance_Amt.Text.Trim());
                    cmd.Parameters.AddWithValue("@Payment_Mode", payment_Mode);
                    cmd.Parameters.AddWithValue("@Reff_No", txt_Reff_No.Text.Trim());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data Updated Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    clear_All_Data();
                    clear_Table_Panel();
                    cmb_Invoice_No.Enabled = true;
                    cmb_Supplier_Name.ResetText();
                }
            }
        }

        private bool itemno()
        {
            foreach (DataGridViewRow row in dgv_Purchase_Details_Group.Rows)
            {
                if (row.Cells["part"].Value == "" || row.Cells["serial"].Value == "")
                {
                    return false;
                }
            }
            return true;
        }

        public void clear_All_Data()
        {
            txt_Contact.Clear();
            txt_Contact_Person_Name.Clear();
            txt_Gst_No.Clear();
            dt_In_Date.ResetText();
            txt_Address.Clear();
            cmb_Invoice_No.ResetText();

            if (dgv_Purchase_Details_Group.RowCount > 0)
            {
                DataTable DT = (DataTable)dgv_Purchase_Details_Group.DataSource;
                if (DT != null)
                    DT.Clear();
            }
            txt_Paid_Amt.Text = "0";
            cmb_Supplier_Name.ResetText();
           
            lbl_Total_Items.Text = "0";
            lbl_Total_Amt.Text = "0";
            lbl_Sgst_Percent.Text = "0";
            lbl_SGST_Amt.Text = "0";
            lbl_Net_Amt.Text = "0";
            lbl_Igst_Percent.Text = "0";
            lbl_IGST_Amt.Text = "0";
            lbl_GST_Amt.Text = "0";
            lbl_Cgst_Percent.Text = "0";
            lbl_CGST_Amt.Text = "0";
            lbl_Balance_Amt.Text = "0";
        }

        private void clear_Table_Panel()
        {
            cmb_category.ResetText();
            cmb_cy_Type.ResetText();
            txt_Particular.ResetText();
            cmb_Supplier_Name.ResetText();
            cmb_Invoice_No.ResetText();
            txt_Part_No.Clear();
            txt_Serial_No.Clear();
            txt_Hsn_Ssn.Clear();
            //txt_Cgst.Clear();
            //txt_Sgst.Clear();
            //txt_Igst.Clear();
            txt_Rate.Clear();
            txt_Discount.Clear();
            cmb_Unit.ResetText();
            txt_Total.Clear();
            txt_Taxable_Amount.Clear();
        }

        public string rdbtn_Purchase_Mode()
        {
            if (rdbtn_Cash.Checked)
            {
                txt_Reff_No.Text = "NA";
                return "Cash";
            }

            else if (rdbtn_Cheque.Checked)
                return "Cheque";
            else
                return "Other";
        }
    }
}
