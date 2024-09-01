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
    public partial class Tax_Sales_Ledger : Form
    {
        SqlConnection con;
        SqlDataReader sdr;
        SqlCommand cmd;
        SqlDataAdapter adpt;
        DataTable dt;

        string customer_name, sell_type;
        public Tax_Sales_Ledger()
        {
            InitializeComponent();
        }

        public void getconnection()
        {
            string str = ConfigurationManager.ConnectionStrings["CylinderData"].ConnectionString;
            con = new SqlConnection(str);
        }

        private void Tax_Sales_Ledger_Load(object sender, EventArgs e)
        {
            panel19.Hide();
            panel23.Hide();
            panel38.Hide();
            panel12.Hide();
            panel27.Hide();
            panel29.Hide();
            panel9.Dock = DockStyle.Fill;
            dgv_CDC_Details.Dock = DockStyle.Fill;
            gridload_Invoice_Details();
        }

        private void cmb_search_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void cmb_Customer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_Customer.Text != "")
            {
                customer_name = cmb_Customer.Text;
                panel19.Show();
                //load_Invoice_Content_By_Customer();
                CurrencyManager cm = (CurrencyManager)BindingContext[dgv_CDC_Details.DataSource];
                cm.SuspendBinding();
                for (int i = 0; i < dgv_CDC_Details.Rows.Count; i++)
                {
                    if (dgv_CDC_Details.Rows[i].Cells["customer"].Value.ToString() == cmb_Customer.Text)
                    {
                        dgv_CDC_Details.Rows[i].Visible = true;
                    }
                    else
                    {
                        dgv_CDC_Details.Rows[i].Visible = false;
                    }
                    cm.ResumeBinding();
                }
            }
            else
            {
                panel19.Hide();
                panel38.Hide();
                panel23.Hide();
                rdb_Btwn_Date.Checked = false;
                rdb_Btwn_Date.Checked = false;
                rdb_Dc_Invoices.Checked = false;
                rdb_Par_Invoices.Checked = false;
            }

        }

        private void load_Invoice_Content_By_Customer()
        {
            // try
            {
                getconnection();
                con.Open();

                cmd = new SqlCommand("SELECT Invoice_No,Cust_Name,Sell_Date ,Total_Quantity, Sgst_Amt, Cgst_Amt, Igst_Amt, Gst_Amt, Total_Amt, Net_Amt FROM Tb_Tax_Sell_Details WHERE Cust_Name = '" + cmb_Customer.Text.ToString() + "'", con); //Status,Total_Items

                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];

                if (dt.Rows.Count == 0)
                {
                    dgv_CDC_Details.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_CDC_Details.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();

                dgv_CDC_Details.Text = "Update";
            }
            //  catch { }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_Dc_Invoices.Checked == true && rdb_All.Checked == true)
            {
                sell_type = "DC";
                panel23.Show();

                CurrencyManager cm = (CurrencyManager)BindingContext[dgv_CDC_Details.DataSource];
                cm.SuspendBinding();
                foreach (DataGridViewRow row in dgv_CDC_Details.Rows)
                {
                    if (row.Cells["type"].Value.ToString() == "DC")
                    {
                        row.Visible = true;
                    }
                    else
                    {
                        row.Visible = false;
                    }

                    cm.ResumeBinding();
                }
            }
            else if (rdb_Dc_Invoices.Checked == true && rdb_Customer.Checked == true)
            {
                panel23.Show();

                CurrencyManager cm = (CurrencyManager)BindingContext[dgv_CDC_Details.DataSource];
                cm.SuspendBinding();
                foreach (DataGridViewRow row in dgv_CDC_Details.Rows)
                {
                    if (row.Cells["type"].Value.ToString() == "DC" && row.Cells["customer"].Value.ToString() == cmb_Customer.Text)
                    {
                        row.Visible = true;
                    }
                    else
                    {
                        row.Visible = false;
                    }
                }
                cm.ResumeBinding();
            }
           /* else if (rdb_Dc_Invoices.Checked == true)
            {
                panel23.Show();
            }*/
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_Par_Invoices.Checked == true && rdb_All.Checked == true)
            {
                sell_type = "Particular";
                panel23.Show();
                CurrencyManager cm = (CurrencyManager)BindingContext[dgv_CDC_Details.DataSource];
                cm.SuspendBinding();
                foreach (DataGridViewRow row in dgv_CDC_Details.Rows)
                {
                    if (row.Cells["type"].Value.ToString() == "Particular")
                    {
                        row.Visible = true;
                    }
                    else
                    {
                        row.Visible = false;
                    }
                    cm.ResumeBinding();
                }
            }
            else if(rdb_Par_Invoices.Checked == true && rdb_Customer.Checked == true)
            {
                CurrencyManager cm = (CurrencyManager)BindingContext[dgv_CDC_Details.DataSource];
                cm.SuspendBinding();
                foreach (DataGridViewRow row in dgv_CDC_Details.Rows)
                {
                    if (row.Cells["type"].Value.ToString() == "Particular" && row.Cells["customer"].Value.ToString() == cmb_Customer.Text)
                    {
                        row.Visible = true;
                    }
                    else
                    {
                        row.Visible = false;
                    }
                }
                cm.ResumeBinding();
            }
           /* else
            {
                panel23.Hide();
                panel38.Hide();
            }*/

        }

        private void rdb_By_Date_CheckedChanged(object sender, EventArgs e)
        {
            panel5.Show();
            panel20.Hide();
            panel38.Show();
            show_by_Date();
        }

        private void rdb_Btwn_Date_CheckedChanged(object sender, EventArgs e)
        {
            panel38.Show();
            panel5.Show();
            panel20.Show();
            show_Between_Date();
        }

        private void gridload_Invoice_Details()
        {
            // try
            {
                getconnection();
                con.Open();

                cmd = new SqlCommand("SELECT Invoice_No,Cust_Name,Sell_Date ,Total_Quantity, Sgst_Amt, Cgst_Amt, Igst_Amt, Gst_Amt, Total_Amt, Net_Amt, Status FROM Tb_Tax_Sell_Details", con); //Status,Total_Items

                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];

                if (dt.Rows.Count == 0)
                {
                    dgv_CDC_Details.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_CDC_Details.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();

                dgv_CDC_Details.Text = "Update";
            }
            //  catch { }
        }

        private void load_Customer_Combo()
        {
            cmb_Customer.Items.Clear();
            // try
            {
                getconnection();
                con.Open();

                cmd = new SqlCommand("SELECT DISTINCT Cust_Name FROM Tb_Tax_Sell_Details", con);
                sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    cmb_Customer.Items.Add(sdr[0]);
                }
            }
            //  catch { }
        }

        private void rdb_All_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_All.Checked == true)
            {
                panel19.Show();
                panel12.Hide();
                panel23.Hide();
                panel5.Hide();
                panel20.Hide();
                gridload_Invoice_Details();
            }
        }

        private void dgv_CDC_Details_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string value = dgv_CDC_Details.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            string invoice_No = dgv_CDC_Details.Rows[e.RowIndex].Cells["invoice_no"].Value.ToString();
            if (value == "View")
            {
                dgv_DC_Show.Dock = DockStyle.Fill;
                if (dgv_CDC_Details.Rows[e.RowIndex].Cells["type"].Value.ToString() == "Particular")
                {
                    load_Invoice_Content(invoice_No);
                    panel27.Dock = DockStyle.Fill;
                    panel27.Show();
                    panel37.Show();
                    panel7.Show();
                }
                else if (dgv_CDC_Details.Rows[e.RowIndex].Cells["type"].Value.ToString() == "DC")
                {
                    load_DC_Content(invoice_No);
                    panel29.Dock = DockStyle.Fill;
                    panel29.Show();
                    panel37.Show();
                    //panel28.Show();
                }
            }
        }

        private void load_DC_Content(string invoice_No)
        {
            //try
            {
                getconnection();
                con.Open();

                cmd = new SqlCommand("SELECT C_Dc_No, C_Dc_Date,Total_Items,Total_Amt FROM Tb_Tax_Content_DC WHERE Invoice_No = '" + invoice_No + "'", con);

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

        private void load_Invoice_Content(string invoice_No)
        {
            //try
            {
                getconnection();
                con.Open();

                cmd = new SqlCommand("SELECT Particulers,HSN,Rate,Discount,Quantity,Unit,Total,SGST,CGST,IGST,Taxable_Amt FROM Tb_Tax_Sell_Content_Particular WHERE Invoice_No = '" + invoice_No + "'", con);

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
            //catch { }
            //finally { con.Close(); }

            decimal rate, disc;
            foreach (DataGridViewRow row in dgv_Invoice_Details.Rows)
            {
                rate = Convert.ToDecimal(row.Cells["rate"].Value);
                disc = Convert.ToDecimal(row.Cells["discount"].Value);
                row.Cells["disc_amt"].Value = (rate * disc / 100).ToString();
            }
        }

        private void dgv_Invoice_Details_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dgv_Invoice_Details.Rows[e.RowIndex].Cells["sr"].Value = (e.RowIndex + 1).ToString();
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

        public void show_by_Date()
        {
            String date = dtp_By_Date.Text.ToString();
            //    try
            {
                getconnection();
                con.Open();

                if (rdb_All.Checked == true)
                {
                    cmd = new SqlCommand("SELECT Invoice_No,Cust_Name,Sell_Date ,Total_Quantity, Sgst_Amt, Cgst_Amt, Igst_Amt, Gst_Amt, Total_Amt, Net_Amt,Status FROM Tb_Tax_Sell_Details WHERE Sell_Date = '" + date + "' AND Status = '" + sell_type + "'", con);
                }
                else if (rdb_Customer.Checked == true)
                {
                    cmd = new SqlCommand("SELECT Invoice_No,Cust_Name,Sell_Date ,Total_Quantity, Sgst_Amt, Cgst_Amt, Igst_Amt, Gst_Amt, Total_Amt, Net_Amt,Status FROM Tb_Tax_Sell_Details WHERE Sell_Date = '" + date + "' AND Status = '" + sell_type + "' AND Cust_Name = '" + customer_name + "'", con);
                }

                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];

                if (dt.Rows.Count == 0)
                {
                    dgv_CDC_Details.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_CDC_Details.DataSource = ds.Tables[0];
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
                if (rdb_All.Checked == true)
                {
                    cmd = new SqlCommand("SELECT Invoice_No,Cust_Name,Sell_Date ,Total_Quantity, Sgst_Amt, Cgst_Amt, Igst_Amt, Gst_Amt, Total_Amt, Net_Amt,Status FROM Tb_Tax_Sell_Details WHERE Status = '" + sell_type + " AND Sell_Date BETWEEN '" + start_date + "' AND '" + end_date + "'", con);
                }
                else if (rdb_Customer.Checked == true)
                {
                    cmd = new SqlCommand("SELECT Invoice_No,Cust_Name,Sell_Date ,Total_Quantity, Sgst_Amt, Cgst_Amt, Igst_Amt, Gst_Amt, Total_Amt, Net_Amt,Status FROM Tb_Tax_Sell_Details WHERE Cust_Name = '" + customer_name + " AND Status = '" + sell_type + " AND Sell_Date BETWEEN '" + start_date + "' AND '" + end_date + "'", con);
                }
                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];

                if (dt.Rows.Count == 0)
                {
                    dgv_CDC_Details.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_CDC_Details.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();
            }
            catch { }
            finally { con.Close(); }
        }

        private void rdb_Customer_CheckedChanged(object sender, EventArgs e)
        {
            if(rdb_Customer.Checked == true)
            {
                panel12.Show();
                panel19.Hide();
                panel23.Hide();
                panel5.Hide();
                panel20.Hide();
                load_Customer_Combo();
            }
        }

        private void panel19_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_Back_Click(object sender, EventArgs e)
        {
            panel37.Hide();
            panel9.Show();
            panel2.Show();
            panel9.Dock = DockStyle.Fill;
            panel27.Hide();
            dgv_CDC_Details.Dock = DockStyle.Fill;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel31.Hide();
            panel9.Show();
            panel2.Show();
            panel9.Dock = DockStyle.Fill;
            panel29.Hide();
            dgv_CDC_Details.Dock = DockStyle.Fill;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(button2.Text == "Export To Excel")
            {
                if (dgv_CDC_Details.Rows.Count > 0)
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
                        for (j = 1; j <= 10; j++)
                        {
                            Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[StartRow, StartCol + j - 1];
                            myRange.Value2 = dgv_CDC_Details.Columns[j].HeaderText;
                        }

                        StartRow++;

                        //Write datagridview content
                        for (i = 0; i < dgv_CDC_Details.Rows.Count; i++)
                        {
                            for (j = 1; j <= 10; j++)
                            {
                                try
                                {
                                    Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[StartRow + i, StartCol + j - 1];
                                    myRange.Value2 = dgv_CDC_Details[j, i].Value == null ? "" : dgv_CDC_Details[j, i].Value;
                                }
                                catch
                                {

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
        }

        private void dgv_DC_Show_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dgv_DC_Show.Rows[e.RowIndex].Cells["sr_no"].Value = (e.RowIndex + 1).ToString();
        }
    }
}
