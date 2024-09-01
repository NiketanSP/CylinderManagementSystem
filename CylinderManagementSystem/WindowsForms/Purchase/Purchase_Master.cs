using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Configuration;
using System.Data.SqlClient;

namespace CylinderManagementSystem
{
    public partial class Purchase_Master : Form
    {
        SqlConnection con;
        SqlDataReader sdr;
        SqlCommand cmd;
        SqlDataAdapter adpt;

        decimal gst_Percent, total;
        decimal sgst, cgst, igst, total_Sgst, total_Cgst, total_Igst;
        int in_Stock_Value = 0;
        string payment_Mode;
        public Purchase_Master()
        {
            InitializeComponent();
            hide_Panels();
            set_textbox_default();
            load_cmb_Supplier_name();
            load_Particular();
            load_cylinder_type();
            load_Unit_Combo();
        }
        public void getconnection()
        {
            string str = ConfigurationManager.ConnectionStrings["CylinderData"].ConnectionString;
            con = new SqlConnection(str);
        }
        private void Purchase_Master_Load(object sender, EventArgs e)
        {
           
           // load_Particular();
            panel27.Hide();
            cmb_Unit.Text ="Nos";
            Panel_Payment_Mode.Hide();
            panel_Payment_Reference.Hide();
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
        private void txt_Total_TextChanged(object sender, EventArgs e)
        {
            calculate_tax_amount();
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
        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }
        //FUNCTION FOR TEXT CHANGE OF "COMBO BOX MATERIAL TYPE"
        private void cmb_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_category.Text == "CYLINDER")
            {
                load_cylinder_type();
                cmb_cy_Type.Text = "";
                txt_Hsn_Ssn.Text = 40090.ToString();
                cmb_cy_Type.Enabled = true;
                txt_Particular.Enabled = true;
                load_Particular();
            }
            else if (cmb_category.Text == "MATERIAL")
            {
              
                cmb_cy_Type.Text = "NA";
                cmb_cy_Type.Enabled = false;
                load_Particular();
                txt_Hsn_Ssn.Text = "";
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
        //DATAGRIDVIEW CELL CONTENT CLICK
        private void dgv_Purchase_Details_Group_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string value = dgv_Purchase_Details_Group.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            if (value == "Edit")
            {
                if(btn_Add.Text == "UPDATE")
                {
                    MessageBox.Show("Cannot Add Record While Another is in the Process", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    DataGridViewRow row = dgv_Purchase_Details_Group.Rows[e.RowIndex];
                    cmb_cy_Type.Text = row.Cells[3].Value.ToString();
                    txt_Particular.Text = row.Cells[4].Value.ToString();
                    txt_Hsn_Ssn.Text = row.Cells[5].Value.ToString();
                    txt_Rate.Text = row.Cells[8].Value.ToString();
                    txt_Discount.Text = row.Cells[9].Value.ToString();
                    txt_Quantity.Text = row.Cells[10].Value.ToString();
                    cmb_Unit.Text = row.Cells[11].Value.ToString();
                    txt_Total.Text = row.Cells[12].Value.ToString();
                    txt_Sgst.Text = row.Cells[13].Value.ToString();
                    txt_Cgst.Text = row.Cells[14].Value.ToString();
                    txt_Igst.Text = row.Cells[15].Value.ToString();
                    txt_Taxable_Amount.Text = row.Cells[16].Value.ToString();
                    dgv_Purchase_Details_Group.Rows.RemoveAt(e.RowIndex);
                    //    update_data_grid(sender, e);
                    calculate_all_values();
                    btn_Add.Text = "UPDATE";
                }
            }
            else if (value == "X")
            {
                DialogResult result = MessageBox.Show("Are You Sure.?", "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    dgv_Purchase_Details_Group.Rows.RemoveAt(e.RowIndex);
                    //      update_data_grid(sender, e);
                    calculate_all_values();
                }
            }
        }
        private void lbl_Total_Amt_Click(object sender, EventArgs e)
        {

        }
        //ADD BUTTON CLICK FUNCTION ()ADDING TEXTFIELD VALUES TO DATAGRIDVIEW
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
                // MessageBox.Show("Enter Rate");
                txt_Rate.Text = "0";
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
                if (cmb_category.Text == "CYLINDER")
                {
                    int quantity = Convert.ToInt32(txt_Quantity.Text);
                    for (int i = 0; i < quantity; i++)
                    {
                        int index = dgv_Purchase_Details_Group.Rows.Add();
                        dgv_Purchase_Details_Group.Rows[index].Cells[3].Value = cmb_cy_Type.Text.Trim();
                        dgv_Purchase_Details_Group.Rows[index].Cells[4].Value = txt_Particular.Text.Trim();
                        dgv_Purchase_Details_Group.Rows[index].Cells[5].Value = txt_Hsn_Ssn.Text.Trim();
                        //dgv_Purchase_Details_Group.Rows[index].Cells[6].Value = txt_Serial_No.Text.Trim();
                       // dgv_Purchase_Details_Group.Rows[index].Cells[7].Value = txt_Part_No.Text.Trim();
                        dgv_Purchase_Details_Group.Rows[index].Cells[8].Value = txt_Rate.Text.Trim();
                        dgv_Purchase_Details_Group.Rows[index].Cells[9].Value = txt_Discount.Text.Trim();
                        dgv_Purchase_Details_Group.Rows[index].Cells[10].Value = "1";
                        dgv_Purchase_Details_Group.Rows[index].Cells[11].Value = cmb_Unit.Text.Trim();
                        dgv_Purchase_Details_Group.Rows[index].Cells[12].Value = (Convert.ToInt32(txt_Total.Text) / quantity).ToString().Trim();
                        dgv_Purchase_Details_Group.Rows[index].Cells[13].Value = txt_Sgst.Text.Trim();
                        dgv_Purchase_Details_Group.Rows[index].Cells[14].Value = txt_Cgst.Text.Trim();
                        dgv_Purchase_Details_Group.Rows[index].Cells[15].Value = txt_Igst.Text.Trim();
                        dgv_Purchase_Details_Group.Rows[index].Cells[16].Value = (Convert.ToDecimal(txt_Taxable_Amount.Text) / quantity).ToString().Trim();
                        dgv_Purchase_Details_Group.Rows[index].Cells[17].Value = cmb_category.Text.Trim();
                    }
                }
                else
                {
                    int index = dgv_Purchase_Details_Group.Rows.Add();
                    dgv_Purchase_Details_Group.Rows[index].Cells[2].Value = (index + 1).ToString();
                    dgv_Purchase_Details_Group.Rows[index].Cells[3].Value = cmb_cy_Type.Text.Trim();
                    dgv_Purchase_Details_Group.Rows[index].Cells[4].Value = txt_Particular.Text.Trim();
                    dgv_Purchase_Details_Group.Rows[index].Cells[5].Value = txt_Hsn_Ssn.Text.Trim();
                    dgv_Purchase_Details_Group.Rows[index].Cells[6].Value = "NA";
                    dgv_Purchase_Details_Group.Rows[index].Cells[7].Value = "NA";
                    dgv_Purchase_Details_Group.Rows[index].Cells[8].Value = txt_Rate.Text.Trim();
                    dgv_Purchase_Details_Group.Rows[index].Cells[9].Value = txt_Discount.Text.Trim();
                    dgv_Purchase_Details_Group.Rows[index].Cells[10].Value = txt_Quantity.Text.Trim();
                    dgv_Purchase_Details_Group.Rows[index].Cells[11].Value = cmb_Unit.Text.Trim();
                    dgv_Purchase_Details_Group.Rows[index].Cells[12].Value = txt_Total.Text.Trim();
                    dgv_Purchase_Details_Group.Rows[index].Cells[13].Value = txt_Sgst.Text.Trim();
                    dgv_Purchase_Details_Group.Rows[index].Cells[14].Value = txt_Cgst.Text.Trim();
                    dgv_Purchase_Details_Group.Rows[index].Cells[15].Value = txt_Igst.Text.Trim();
                    dgv_Purchase_Details_Group.Rows[index].Cells[16].Value = txt_Taxable_Amount.Text.Trim();
                    dgv_Purchase_Details_Group.Rows[index].Cells[17].Value = cmb_category.Text.Trim();
                }
                clear_data();
                calculate_all_values();
            }
            cmb_category.Focus();
            btn_Add.Text = "ADD";
         // load_Particular();
        }
        private void txt_Particular_TextChanged(object sender, EventArgs e)
        {

        }
        //FUNCTION TO HIDE SERIAL_NO & PART_NO FIELDS 
        public void hide_Panels()
        {
            if (cmb_cy_Type.Text == "")
            {
                
            }
            Panel_Payment_Mode.Hide();
            panel_Payment_Reference.Hide();
        }
        //SETTING DEFAULT VALUES OF TEXSTBOXES
        public void set_textbox_default()
        {
            txt_Sgst.Text = txt_Cgst.Text = 6.ToString();
            txt_Igst.Text = txt_Discount.Text = 0.ToString();
            txt_Total.Text = 0.ToString();
            txt_Taxable_Amount.Text = 0.ToString();
            cmb_Unit.Text = "NOS";
        }
        //UPDATION OF DATAGRIDVIEW ON CLICK OF "EDIT" & "X" (REMOVE)
     /*   public void update_data_grid(object sender, DataGridViewCellEventArgs e)
        {
            int limit = dgv_Purchase_Details_Group.RowCount;
            int index = e.RowIndex, temp;
            for (int i = index; i < limit; i++)
            {
                temp = Convert.ToInt32(dgv_Purchase_Details_Group.Rows[i].Cells[0].Value);
                dgv_Purchase_Details_Group.Rows[i].Cells[0].Value = (temp - 1).ToString();
            }
            calculate_all_values();
        }*/
        public void calculate_total_sgst()
        {

        }
        public void calculate_total_igst()
        {

        }
        public void calculate_all_values()
        {
            decimal total_amt = 0, gst_amt = 0, total_gst_amt = 0, total = 0, quantity=0, total_quantity=0;
            total_Sgst = total_Cgst = total_Igst = total_amt = gst_amt = total_gst_amt = 0;
            int rowcount = dgv_Purchase_Details_Group.RowCount;
            if (rowcount > 0)
            {
                for (int i = 0; i < rowcount; i++)
                {
                    total = Convert.ToDecimal(dgv_Purchase_Details_Group.Rows[i].Cells[12].Value.ToString());
                    cgst = Convert.ToDecimal(dgv_Purchase_Details_Group.Rows[i].Cells[14].Value.ToString());
                    sgst = Convert.ToDecimal(dgv_Purchase_Details_Group.Rows[i].Cells[13].Value.ToString());
                    igst = Convert.ToDecimal(dgv_Purchase_Details_Group.Rows[i].Cells[15].Value.ToString());
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
            lbl_GST_Amt.Text = total_gst_amt.ToString();
            lbl_Net_Amt.Text = Convert.ToInt64((total_amt + total_gst_amt)).ToString();
            //CALCULATE SGST, CGST, IGST AMOUNT
            lbl_SGST_Amt.Text = (total_amt * total_Sgst / 100).ToString();
            lbl_CGST_Amt.Text = (total_amt * total_Cgst / 100).ToString();
            lbl_IGST_Amt.Text = (total_amt * total_Igst / 100).ToString();
            lbl_Gst_Percent.Text = (total_Cgst + total_Sgst).ToString();
        }
        private void txt_Paid_Amt_TextChanged(object sender, EventArgs e)
        {
            calculate_Balance_Amt();
        }


        private void cmb_Supplier_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_supplier_details();
            load_Particular();
          
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
            else if (txt_Invoice_No.Text == "")
            {
                MessageBox.Show("Please Enter The Invoice No", "STOP", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else if (!itemno())
            {
                MessageBox.Show("Please Enter Serial No and Part No for All Cylinders", "STOP", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                if(txt_Paid_Amt.Text == "")
                {
                    txt_Paid_Amt.Text = "0";
                    lbl_Balance_Amt.Text = lbl_Net_Amt.Text.ToString();
                }
                if(rdbtn_Cash.Checked == false && rdbtn_Cheque.Checked == false && rdbtn_Other.Checked == false)
                {
                    payment_Mode = "NA";
                }
                if(txt_Reff_No.Text == "")
                {
                    txt_Reff_No.Text = "NA";
                }
                // try
                {
                    getconnection();
                    con.Open();
                    cmd = new SqlCommand("Insert_Purchase_Details", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Supplier_Name", cmb_Supplier_Name.Text.Trim());
                    cmd.Parameters.AddWithValue("@Date_Of_Purchase", dt_In_Date.Text.ToString());
                    cmd.Parameters.AddWithValue("@Invoice_No", txt_Invoice_No.Text.Trim());
                    cmd.Parameters.AddWithValue("@Gst_No", txt_Gst_No.Text.Trim());
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
                    cmd.Parameters.AddWithValue("@Gst_Perecent", lbl_Gst_Percent.Text);
                    cmd.Parameters.AddWithValue("@Cancel_status", "NA");
                    cmd.ExecuteNonQuery();
                    //   MessageBox.Show("Data Saved Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clear_data();
                }
                // catch { MessageBox.Show("Purchase Error"); }
                //try
                {
                    getconnection();
                    con.Open();
                    for (int i = 0; i < dgv_Purchase_Details_Group.Rows.Count; i++)
                    {
                        cmd = new SqlCommand("Insert_Purchase_Content_Details", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Purchase_Type", dgv_Purchase_Details_Group.Rows[i].Cells["category"].Value.ToString());
                        cmd.Parameters.AddWithValue("@Particulers", dgv_Purchase_Details_Group.Rows[i].Cells["particulars"].Value.ToString());
                        cmd.Parameters.AddWithValue("@HSN", dgv_Purchase_Details_Group.Rows[i].Cells["hsn"].Value.ToString());
                        cmd.Parameters.AddWithValue("@Rate", dgv_Purchase_Details_Group.Rows[i].Cells["rate"].Value.ToString());
                        cmd.Parameters.AddWithValue("@Sr_No", dgv_Purchase_Details_Group.Rows[i].Cells["serial"].Value.ToString());
                        cmd.Parameters.AddWithValue("@Part_No", dgv_Purchase_Details_Group.Rows[i].Cells["part"].Value.ToString());
                        cmd.Parameters.AddWithValue("@Disc ", dgv_Purchase_Details_Group.Rows[i].Cells["discount"].Value.ToString());
                        cmd.Parameters.AddWithValue("@Quantity", dgv_Purchase_Details_Group.Rows[i].Cells["quantity"].Value.ToString());
                        cmd.Parameters.AddWithValue("@Unit", dgv_Purchase_Details_Group.Rows[i].Cells["unit"].Value.ToString());
                        cmd.Parameters.AddWithValue("@Total", dgv_Purchase_Details_Group.Rows[i].Cells["total_"].Value.ToString());
                        cmd.Parameters.AddWithValue("@SGST", dgv_Purchase_Details_Group.Rows[i].Cells["sgst_"].Value.ToString());
                        cmd.Parameters.AddWithValue("@CGST", dgv_Purchase_Details_Group.Rows[i].Cells["cgst_"].Value.ToString());
                        cmd.Parameters.AddWithValue("@IGST", dgv_Purchase_Details_Group.Rows[i].Cells["igst_"].Value.ToString());
                        cmd.Parameters.AddWithValue("@TaxableAmt", dgv_Purchase_Details_Group.Rows[i].Cells["with_gst"].Value.ToString());
                        cmd.Parameters.AddWithValue("@Cylinder_Type", dgv_Purchase_Details_Group.Rows[i].Cells["c_type"].Value.ToString());
                        cmd.Parameters.AddWithValue("@invoice_No", txt_Invoice_No.Text.ToString().Trim()); 
                        if (dgv_Purchase_Details_Group.Rows[i].Cells["category"].Value.ToString() == "MATERIAL")
                        {
                            cmd.Parameters.AddWithValue("@cylinder_Status", "NA");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@cylinder_Status", "EMPTY");
                        }
                        cmd.Parameters.AddWithValue("@Cust_Supp_Name", cmb_Supplier_Name.Text.ToString().Trim());
                        cmd.ExecuteNonQuery();
                        //MessageBox.Show("Data Saved Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                // catch { MessageBox.Show("Purchase content Error"); }
                //  try
                {
                    foreach (DataGridViewRow row in dgv_Purchase_Details_Group.Rows)
                    {
                       // Boolean value = true;
                        getconnection();
                        con.Open();
                        string particular = row.Cells[4].Value.ToString();
                        cmd = new SqlCommand("SELECT Particulars FROM Tb_Inventory_Master WHERE Particulars = '" + particular + "'", con);
                        sdr = cmd.ExecuteReader();
                        /*if (!sdr.Read())
                        {
                            value = false;
                        }*/
                        if (!sdr.Read())
                        {
                            sdr.Close();
                            cmd = new SqlCommand("Insert_Inventory_Master", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@Particulars", row.Cells["particulars"].Value.ToString());
                            cmd.Parameters.AddWithValue("@Type", row.Cells["category"].Value.ToString());
                            cmd.Parameters.AddWithValue("@In_stock", row.Cells["quantity"].Value.ToString());
                            cmd.Parameters.AddWithValue("@Out_stock", 0.ToString());
                            cmd.Parameters.AddWithValue("@Cylinder_Type", row.Cells["c_type"].Value.ToString());
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            sdr.Close();
                            calculate_In_Stock_Value(particular);
                            in_Stock_Value = in_Stock_Value + Convert.ToInt32(row.Cells[10].Value);
                            cmd = new SqlCommand("UPDATE Tb_Inventory_Master SET In_Stock = '" + in_Stock_Value.ToString() + "' WHERE Particulars = '" + particular + "'", con);
                            cmd.ExecuteNonQuery();
                        }
                        load_Particular();
                    }
                    sdr.Close();
                    MessageBox.Show("Data Saved Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clear_All_Data();
                    Panel_Payment_Mode.Hide();
                }
                //  catch { MessageBox.Show("Inventory Error"); }
            }
        }
        private bool itemno()
        {
            foreach (DataGridViewRow row in dgv_Purchase_Details_Group.Rows)
            {
                if (row.Cells["part"].Value == null || row.Cells["serial"].Value == null)
                {
                    return false;
                }
            }
            return true;
        }
        public void calculate_Balance_Amt()
        {
            decimal net, paid;
            net = Convert.ToDecimal(lbl_Net_Amt.Text);
             if (txt_Paid_Amt.Text != "")
            {
                paid = Convert.ToDecimal(txt_Paid_Amt.Text);
            }
            else
            {
                paid = 0;
            }
            if (paid > net)
            {
                MessageBox.Show("Paid Amount is Greater than Net Amount");
            }
            else
            {
                lbl_Balance_Amt.Text = (net - paid).ToString();
            }
        }
        private void rdbtn_Cash_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbtn_Cash.Checked == true)
            {
                panel_Payment_Reference.Hide();
                payment_Mode = "CASH";
            }
        }
        private void rdbtn_Cheque_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbtn_Cheque.Checked == true)
            {
                panel_Payment_Reference.Show();
                payment_Mode = "CHEQUE";
            }
            else if (rdbtn_Cheque.Checked == false)
            {
                panel_Payment_Reference.Hide();
            }
        }
        private void rdbtn_Other_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbtn_Other.Checked == true)
            {
                panel_Payment_Reference.Show();
                payment_Mode = "OTHER";
            }
            else if (rdbtn_Other.Checked == false)
            {
                panel_Payment_Reference.Hide();
            }
        }
        public void load_cmb_Supplier_name()
        {
            try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT DISTINCT Supp_CompName FROM Tb_Supplier_Master", con);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    cmb_Supplier_Name.Items.Add(sdr[0]);
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
        private void txt_Invoice_No_Leave(object sender, EventArgs e)
        {
            try
            {
                getconnection();
                con.Open();
                string supp_Name = cmb_Supplier_Name.Text.ToString();
                cmd = new SqlCommand("SELECT Invoice_No FROM Tb_Purchase WHERE Supplier_Name = '" + supp_Name + "' AND Invoice_No = '" + txt_Invoice_No.Text.ToString() + "'", con);
                sdr = cmd.ExecuteReader();
                if(sdr.Read())
                {
                    MessageBox.Show("Invoice Number Already Inwarded.!");
                    txt_Invoice_No.Focus();
                }
                sdr.Close();
            }
            catch { }
            finally { con.Close(); }
        }
        private void dgv_Purchase_Details_Group_RowPostPaint_1(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dgv_Purchase_Details_Group.Rows[e.RowIndex].Cells["sr"].Value = (e.RowIndex + 1).ToString();
        }
        private void cmb_cy_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
          load_Particular();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Supplier_Master fm = new Supplier_Master();
       //     fm.MdiParent = this;
            fm.ShowDialog();
        }
        private void cmb_Supplier_Name_TextChanged(object sender, EventArgs e)
        {
           // load_Particular();
        }
        private void cmb_cy_Type_TextChanged(object sender, EventArgs e)
        {
            // load_Particular();
        }
        private void txt_Particular_SelectedIndexChanged(object sender, EventArgs e)
        {
          //  load_Particular();
        }

        private void txt_Invoice_No_TextChanged(object sender, EventArgs e)
        {

        }

        public void load_supplier_details()
        {
            //try
            {
                getconnection();
                con.Open();
                string supp_Name = cmb_Supplier_Name.Text.ToString();
                cmd = new SqlCommand("SELECT Supp_Address, Supp_CPersonNo, Supp_ConPerson, Supp_GST FROM Tb_Supplier_Master WHERE Supp_CompName = '" + supp_Name + "'", con);
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
            //catch { }
            //finally { con.Close(); }
        }

        public void load_Particular()
        {
            txt_Particular.ResetText();
            txt_Particular.Items.Clear();
            try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT DISTINCT Particulers FROM Tb_Supplier_Material_Master WHERE Cylinder_Type ='" + cmb_cy_Type.Text + "'", con);
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
        public void load_cylinder_type()
        {
            cmb_cy_Type.Items.Clear();
            try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT DISTINCT Cylinder_Type FROM Tb_Supplier_Material_Master WHERE Supp_Name = '" + cmb_Supplier_Name.Text + "'", con);
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
        public void clear_All_Data()
        {
            dgv_Purchase_Details_Group.Rows.Clear();
            clear_data();
            cmb_Supplier_Name.ResetText();
            txt_Invoice_No.Clear();
            txt_Gst_No.Clear();
            txt_Contact.Clear();
            txt_Contact_Person_Name.Clear();
            txt_Address.Clear();
            dt_In_Date.ResetText();
            txt_Gst_No.Clear();
            rdbtn_Cash.ResetText();
            rdbtn_Cheque.ResetText();
            rdbtn_Other.ResetText();
            cmb_cy_Type.ResetText();
            txt_Hsn_Ssn.Clear();
            cmb_category.ResetText();
            txt_Paid_Amt.Text = "0";
            lbl_Total_Items.Text = "0";
            lbl_Total_Amt.Text = "0";
            lbl_Sgst_Percent.Text = "0";
            lbl_SGST_Amt.Text = "0";
            lbl_Net_Amt.Text = "0";
            lbl_Igst_Percent.Text = "0";
            lbl_IGST_Amt.Text = "0";
            lbl_Gst_Percent.Text = "0";
            lbl_GST_Amt.Text = "0";
            lbl_Cgst_Percent.Text = "0";
            lbl_CGST_Amt.Text = "0";
            lbl_Balance_Amt.Text = "0";
        }
    }
}
