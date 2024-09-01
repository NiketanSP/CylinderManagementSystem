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
    public partial class Purchase_Ledger : Form
    {
        SqlConnection con;
        SqlDataReader sdr;
        SqlCommand cmd;
        SqlDataAdapter adpt;
        DataTable dt;

        public Purchase_Ledger()
        {
            InitializeComponent();
            gridload_Purchase();
        }

        public void getconnection()
        {
            string str = ConfigurationManager.ConnectionStrings["CylinderData"].ConnectionString;
            con = new SqlConnection(str);
        }

        public void clearcontrol()
        {
            cmb_Supplier_Name.ResetText();
            cmb_Invoice_No.ResetText();
        }
        public void gridload_Purchase()
        {
            // try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT Supplier_Name, Gst_No, Date_Of_Purchase, Invoice_No, Total_Amt, IGST_Per, IGST_Amt, Net_Amt, Paid_Amt, Balance_Amt, Payment_Mode, Reff_No, Gst_Percent,GST_Amt, SGST_Per, SGST_Amt, CGST_Per, CGST_Amt FROM Tb_Purchase WHERE Cancel_Status = 'NA'", con);
                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    dgv_Purchase_Details.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_Purchase_Details.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();
            }
            // catch { }
            // finally { con.Close(); }
            foreach (DataGridViewRow row in dgv_Purchase_Details.Rows)
            {
                dgv_Purchase_Details.Rows[row.Index].Cells["sr_no"].Value = string.Format("{0}  ", row.Index + 1).ToString();
            }
        }
        private void dgv_Purchase_Details_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            String value = dgv_Purchase_Details.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            if (value == "View")
            {
                string invoice_No;
                invoice_No = dgv_Purchase_Details.Rows[e.RowIndex].Cells["invoice"].Value.ToString();
                gridload_Invoice(invoice_No);
                panel14.Show();
                label4.Show();
                panel15.Hide();
                dgv_Invoice_Details.Dock = DockStyle.Fill;
                panel6.Show();
                panel6.Dock = DockStyle.Fill;
                panel3.Hide();
            }
            else
            {
                //MessageBox.Show(e.ColumnIndex.ToString());
            }
        }
        public void gridload_Invoice(string invoice_No)
        {
            try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT Particulers,HSN,Rate,Disc,Quantity,Unit,Total,SGST,CGST,IGST,TaxableAmt FROM Tb_Purchase_Content_Master WHERE Invoice_No = '" + invoice_No + "'", con);
                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    dgv_Invoice_Details.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_Invoice_Details.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();
            }
            catch { }
            finally { con.Close(); }
            decimal rate, disc;
            foreach (DataGridViewRow row in dgv_Invoice_Details.Rows)
            {
                rate = Convert.ToDecimal(row.Cells["rate"].Value);
                disc = Convert.ToDecimal(row.Cells["discount"].Value);
                row.Cells["disc_amt"].Value = (rate * disc / 100).ToString();
            }
        }
        public void grid_invoice()
        {
            try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT Particulers,HSN,Rate,Disc,Quantity,Unit,Total,SGST,CGST,IGST,TaxableAmt FROM Tb_Purchase_Content_Master WHERE Invoice_No = '" + cmb_Invoice_No.Text + "'", con);
                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    dgv_Invoice_Details.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_Invoice_Details.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();
            }
            catch { }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            panel3.Show();
            panel6.Hide();
            label4.Hide();
            panel15.Show();
            panel14.Hide();
        }
        private void Purchase_Ledger_Load(object sender, EventArgs e)
        {
            dgv_Purchase_Details.Dock = DockStyle.Fill;
            panel3.Dock = DockStyle.Fill;
            panel3.Show();
            panel14.Hide();
            panel6.Hide();
            panel20.Hide();
            panel23.Hide();
            panel26.Hide();
            panel22.Hide();
            label4.Hide();
        }
        private void rdbtn_Cheque_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_By_Date.Checked == true)
            {
                panel28.Show();
                panel22.Show();
                panel29.Hide();
                panel23.Hide();
                show_by_Date();
            }
        }
        private void rdbtn_Other_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_Btwn_Date.Checked == true)
            {
                panel28.Show();
                panel23.Hide();
                panel29.Show();
                panel22.Show();
                show_Between_Date();
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void cmb_Supp_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_Supplier_Name.Text == "")
            {
                panel26.Hide();
                panel22.Hide();
            }
            else
            {
                panel26.Show();
                panel22.Show();
                panel23.Show();
                load_cmb_Invoice_No();
                try
                {
                    getconnection();
                    con.Open();
                    cmd = new SqlCommand("SELECT Supplier_Name, Gst_No, Date_Of_Purchase, Invoice_No, Total_Amt, IGST_Per, IGST_Amt, Net_Amt, Paid_Amt, Balance_Amt, Payment_Mode, Reff_No, Gst_Percent,GST_Amt, SGST_Per, SGST_Amt, CGST_Per, CGST_Amt FROM Tb_Purchase WHERE Supplier_Name = '" + cmb_Supplier_Name.Text.ToString() + "' AND Cancel_Status = 'NA'", con);
                    adpt = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adpt.Fill(ds);
                    dt = ds.Tables[0];
                    if (dt.Rows.Count == 0)
                    {
                        dgv_Purchase_Details.DataSource = ds.Tables[0];
                    }
                    else
                    {
                        dgv_Purchase_Details.DataSource = ds.Tables[0];
                    }
                    adpt.Dispose();
                    dt.Dispose();
                }
                catch { }
                finally { con.Close(); }
                clearcontrol();
            }
        }
        private void cmb_Search_By_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_Search_By.Text == "All Record")
            {
                gridload_Purchase();
                panel20.Hide();
                panel23.Hide();
            }
            else if (cmb_Search_By.Text == "Supplier Record")
            {
                load_cmb_Supplier_name();
                panel20.Show();
                panel23.Hide();
            }
            else if (cmb_Search_By.Text == "Invoice Number")
            {
                panel23.Show();
                panel20.Hide();
                panel26.Hide();
                panel28.Hide();
                panel29.Hide();
                cmb_Supplier_Name.Text = "";
                cmb_Invoice_No.Items.Clear();
                string supp_Name = cmb_Supplier_Name.Text;
                try
                {
                    getconnection();
                    con.Open();
                    cmd = new SqlCommand("SELECT DISTINCT Invoice_No FROM Tb_Purchase WHERE Cancel_Status = 'NA'", con);
                    sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        cmb_Invoice_No.Items.Add(sdr[0]);
                    }
                    sdr.Close();
                }
                catch { }
                finally { con.Close(); }
                clearcontrol();
            }
        }
        public void load_cmb_Supplier_name()
        {
            cmb_Supplier_Name.Items.Clear();
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
        //LOAD INVOICE LIST INTO INVOICE COMBO BOX
        public void load_cmb_Invoice_No()
        {
            cmb_Invoice_No.Items.Clear();
            string supp_Name = cmb_Supplier_Name.Text;
            try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT DISTINCT Invoice_No FROM Tb_Purchase WHERE Supplier_Name = '" + supp_Name + "' AND Cancel_Status != 'Cancel'", con);
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
        private void dgv_Purchase_Details_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dgv_Purchase_Details.Rows[e.RowIndex].Cells["sr_no"].Value = (e.RowIndex + 1).ToString();
        }
        private void dgv_Invoice_Details_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dgv_Invoice_Details.Rows[e.RowIndex].Cells["sr"].Value = (e.RowIndex + 1).ToString();
        }
        private void cmb_Invoice_No_TextChanged(object sender, EventArgs e)
        {
            grid_invoice();
            if (cmb_Invoice_No.Text != "")
            {
                panel6.Hide();
                panel3.Show();
            }
            try
            {
                getconnection();
                con.Open();
                int invoice_No = Convert.ToInt32(cmb_Invoice_No.Text);
                cmd = new SqlCommand("SELECT Supplier_Name, Gst_No, Date_Of_Purchase, Invoice_No, Total_Amt, IGST_Per, IGST_Amt, Net_Amt, Paid_Amt, Balance_Amt, Payment_Mode, Reff_No, Gst_Percent,GST_Amt, SGST_Per, SGST_Amt, CGST_Per, CGST_Amt FROM Tb_Purchase WHERE Invoice_No =" + invoice_No + "", con);
                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    dgv_Purchase_Details.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_Purchase_Details.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();
            }
            catch { }
            finally { con.Close(); }
        }
        private void dtp_By_Date_ValueChanged(object sender, EventArgs e)
        {
            if (rdb_By_Date.Checked == true)
            {
                show_by_Date();
            }
            else
            {
                show_Between_Date();
            }
        }
        public void show_by_Date()
        {
            String date = dtp_By_Date.Text.ToString();
            //    try
            {
                getconnection();
                con.Open();
                if (cmb_Search_By.Text == "All Record")
                {
                    cmd = new SqlCommand("SELECT Supplier_Name, Gst_No, Date_Of_Purchase, Invoice_No, Total_Amt, IGST_Per, IGST_Amt, Net_Amt, Paid_Amt, Balance_Amt, Payment_Mode, Reff_No, Gst_Percent,GST_Amt FROM Tb_Purchase WHERE Date_Of_Purchase = '" + date + "' AND Cancel_Status = 'NA'", con);
                }
                else if (cmb_Search_By.Text == "Supplier Record")
                {
                    cmd = new SqlCommand("SELECT Supplier_Name, Gst_No, Date_Of_Purchase, Invoice_No, Total_Amt, IGST_Per, IGST_Amt, Net_Amt, Paid_Amt, Balance_Amt, Payment_Mode, Reff_No, Gst_Percent,GST_Amt FROM Tb_Purchase WHERE Supplier_Name = '" + cmb_Supplier_Name.Text + "' AND Date_Of_Purchase = '" + date + "' AND Cancel_Status = 'NA'", con);
                }
                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    dgv_Purchase_Details.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_Purchase_Details.DataSource = ds.Tables[0];
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
            String end_date = dtp_Btwn_Date.Text.ToString();
            try
            {
                getconnection();
                con.Open();
                if (cmb_Search_By.Text == "All Record")
                {
                    cmd = new SqlCommand("SELECT Supplier_Name, Gst_No, Date_Of_Purchase, Invoice_No, Total_Amt, IGST_Per, IGST_Amt, Net_Amt, Paid_Amt, Balance_Amt, Payment_Mode, Reff_No, Gst_Percent,GST_Amt FROM Tb_Purchase WHERE Cancel_Status = 'NA' AND Date_Of_Purchase BETWEEN '" + start_date + "' AND '" + end_date + "'", con);
                }
                else if (cmb_Search_By.Text == "Supplier Record")
                {
                    cmd = new SqlCommand("SELECT Supplier_Name, Gst_No, Date_Of_Purchase, Invoice_No, Total_Amt, IGST_Per, IGST_Amt, Net_Amt, Paid_Amt, Balance_Amt, Payment_Mode, Reff_No, Gst_Percent,GST_Amt FROM Tb_Purchase WHERE Supplier_Name = '" + cmb_Supplier_Name.Text + "' AND Date_Of_Purchase BETWEEN '" + start_date + "' AND '" + end_date + "'", con);
                }
                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    dgv_Purchase_Details.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_Purchase_Details.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();
            }
            catch { }
            finally { con.Close(); }
        }
        private void dtp_Btwn_Date_ValueChanged(object sender, EventArgs e)
        {
            if (rdb_By_Date.Checked == true)
            {
                show_by_Date();
            }
            else
            {
                show_Between_Date();
            }
        }
        private void cmb_Invoice_No_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearcontrol();
            grid_invoice();
            if (cmb_Invoice_No.Text != "")
            {
                //   panel26.Hide();
                panel28.Hide();
                panel29.Hide();
            }
            else
            {
                // panel26.Show();
                panel28.Show();
                panel29.Show();
                panel23.Show();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (dgv_Purchase_Details.Rows.Count > 0)
            {
                //try
                {
                    Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                    excel.Visible = true;
                    Microsoft.Office.Interop.Excel.Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
                    Microsoft.Office.Interop.Excel.Worksheet sheet1 = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets[1];
                    excel.Columns.ColumnWidth = 15;
                    int StartCol = 1;
                    int StartRow = 1;
                    int j = 0, i = 0;
                    ////Write Headers
                    for (j = 1; j <= 19; j++)
                    {
                        Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[StartRow, StartCol + j];
                        myRange.Value2 = dgv_Purchase_Details.Columns[j].HeaderText;
                    }
                    StartRow++;
                    //Write datagridview content
                    for (i = 0; i < dgv_Purchase_Details.Rows.Count; i++)
                    {
                        for (j = 1; j <= 19; j++)
                        {
                            try
                            {
                                Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[StartRow + i, StartCol + j];
                                myRange.Value2 = dgv_Purchase_Details[j, i].Value == null ? "" : dgv_Purchase_Details[j, i].Value;
                            }
                            catch
                            {
                                ;
                            }
                        }
                    }
                }
                //catch (Exception ex)
                {
                    //   MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("No Record Found to Convert, Access Denied", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
           
        }
    }
}
