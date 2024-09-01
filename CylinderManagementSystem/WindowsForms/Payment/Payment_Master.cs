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
    public partial class Payment_Master : Form
    {
        SqlConnection con;
        SqlDataReader sdr;
        SqlCommand cmd;
        SqlDataAdapter adpt;
        DataTable dt;
        string pay_mode = "NA", invoice;
        public Payment_Master()
        {
            InitializeComponent();
        }
        public void getconnection()
        {
            string str = ConfigurationManager.ConnectionStrings["CylinderData"].ConnectionString;
            con = new SqlConnection(str);
        }
        private void Payment_Master_Load(object sender, EventArgs e)
        {
            panel20.Dock = DockStyle.Fill;
            panel56.Dock = DockStyle.Fill;
            load_Cust_Combo();
            load_Supp_Combo();
            panel49.Enabled = false;
            panel21.Enabled = false;
            panel59.Enabled = false;
            panel60.Enabled = false;
            panel23.Hide();
            panel6.Visible = false;
            panel24.Hide();
            panel40.Hide();
            panel44.Hide();
            panel102.Hide();
            panel104.Hide();
            panel78.Hide();
            panel67.Hide();
            chk_Paid_Multiple_Cust.Enabled = false;
            chk_Paid_Multiple_Supplier.Enabled = false;
            rdb_Pending_Supplier.Checked = true;
            rdb_Pending_Invoice_Cust.Checked = true;
            gridload_All_Invoice_Cust();
            gridload_All_Invoice_Supp();
        }
        private void load_Cust_Combo()
        {
            cmb_Customer_Supplier.Items.Clear();
            //try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT DISTINCT Cust_Name FROM Tb_Tax_Sell_Details", con);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    cmb_Customer_Supplier.Items.Add(sdr[0]);
                }
                sdr.Close();
            }
            //catch { }
            //finally { con.Close(); }
        }
        private void load_Supp_Combo()
        {
            cmb_Supplier_Name.Items.Clear();
            //try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT DISTINCT Supplier_Name FROM Tb_Purchase", con);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    cmb_Supplier_Name.Items.Add(sdr[0]);
                }
                sdr.Close();
            }
            //catch { }
            //finally { con.Close(); }
        }
        private void cmb_Customer_Supplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmb_Customer_Supplier.Text!="")
            {
                panel49.Enabled = true;
                panel21.Enabled = true;
                rdb_Pending_Invoice_Cust.Enabled = true;
                rdb_Paid_Invoice_Cust.Enabled = true;
                gridload_Invoice_By_Cust();
            }
            else
            {
                rdb_Pending_Invoice_Cust.Enabled = false;
                rdb_Paid_Invoice_Cust.Enabled = false;
                gridload_All_Invoice_Cust();
            }
        }
        private void gridload_Invoice_By_Cust()
        {
            //try
            {
                getconnection();
                con.Open();
                if (rdb_Pending_Invoice_Cust.Checked == true)
                {
                    cmd = new SqlCommand("SELECT Invoice_No, Sell_Date, Net_Amt, Paid_Amt, Balance_Amt FROM Tb_Tax_Sell_Details WHERE Cust_Name = '" + cmb_Customer_Supplier.Text.ToString() + "' AND Balance_Amt > 0", con);
                }
                else if (rdb_Paid_Invoice_Cust.Checked == true)
                {
                    cmd = new SqlCommand("SELECT Invoice_No, Sell_Date, Net_Amt, Paid_Amt, Balance_Amt FROM Tb_Tax_Sell_Details WHERE Cust_Name = '" + cmb_Customer_Supplier.Text.ToString() + "' AND Balance_Amt = 0", con);
                }
                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    dgv_All_Invoices_Cust.DataSource = ds.Tables[0];
                    dgv_Paid_Multiple_Cust.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_All_Invoices_Cust.DataSource = ds.Tables[0];
                    dgv_Paid_Multiple_Cust.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();
            }
            //catch { }
            //finally { con.Close(); }
        }
        private void gridload_All_Invoice_Cust()
        {
            //try
            {
                getconnection();
                con.Open();

                if (rdb_Pending_Invoice_Cust.Checked == true)
                {
                    cmd = new SqlCommand("SELECT Invoice_No, Sell_Date, Net_Amt, Paid_Amt, Balance_Amt FROM Tb_Tax_Sell_Details WHERE Balance_Amt > 0", con);
                }
                else if (rdb_Paid_Invoice_Cust.Checked == true)
                {
                    cmd = new SqlCommand("SELECT Invoice_No, Sell_Date, Net_Amt, Paid_Amt, Balance_Amt FROM Tb_Tax_Sell_Details WHERE Balance_Amt = 0", con);
                }
                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    dgv_All_Invoices_Cust.DataSource = ds.Tables[0];
                    dgv_Paid_Multiple_Cust.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_All_Invoices_Cust.DataSource = ds.Tables[0];
                    dgv_Paid_Multiple_Cust.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();
            }
            //catch { }
            //finally { con.Close(); }
        }

        private void rdb_Pending_Invoice_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_Pending_Invoice_Cust.Checked == true)
            {
                gridload_Invoice_By_Cust();
                chk_Paid_Multiple_Cust.Enabled = true;
                //gridload_Invoice_details(invoice);
            }
            else
            {
                chk_Paid_Multiple_Cust.Enabled = false;
            }
        }
        private void rdb_Paid_Invoice_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_Paid_Invoice_Cust.Checked == true)
            {
                gridload_Invoice_By_Cust();
                chk_Paid_Multiple_Supplier.Enabled = false;
                panel35.Hide();
                panel44.Hide();
                panel48.Hide();
                panel40.Hide();          
            }
            else
            {
                panel35.Show();
                panel44.Show();
                panel48.Show();
                panel40.Show();
            }
        }
        private void gridload_Paid_Invoice()
        {
            //try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT Invoice_No, Sell_Date, Net_Amt, Paid_Amt, Balance_Amt FROM Tb_Tax_Sell_Details WHERE Cust_Name = '" + cmb_Customer_Supplier.Text.ToString() + "' AND Balance_Amt = 0", con);
                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    dgv_All_Invoices_Cust.DataSource = ds.Tables[0];
                    dgv_Paid_Multiple_Cust.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_All_Invoices_Cust.DataSource = ds.Tables[0];
                    dgv_Paid_Multiple_Cust.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();
            }
            //catch { }
            //finally { con.Close(); }
        }
        private void chk_Paid_Multiple_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Paid_Multiple_Cust.Checked == true)
            {
                panel20.Hide();
                panel23.Show();
                panel23.Dock = DockStyle.Fill;
                rdb_Paid_Invoice_Cust.Checked = false;
                tabControl1.Dock = DockStyle.Fill;
                rdb_Paid_Invoice_Cust.Enabled = false;
            }
            else
            {
                panel23.Hide();
                panel20.Show();
                panel20.Dock = DockStyle.Fill;
                panel10.Show();
                rdb_Paid_Invoice_Cust.Enabled = true;
                //gridload_Invoice_By_Cust();
            }
        }
        private void dgv_Paid_Multiple_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgv_Paid_Multiple_Cust.CommitEdit(DataGridViewDataErrorContexts.Commit);
            decimal total = 0;
            foreach (DataGridViewRow row in dgv_Paid_Multiple_Cust.Rows)
            {
                if (Convert.ToBoolean(row.Cells["select"].Value))
                {
                    total = total + Convert.ToDecimal(row.Cells["balance"].Value);
                }
            }
            txt_Select_Invoice_Total.Text = total.ToString();
        }

        private void btn_Paid_Multiple_Click(object sender, EventArgs e)
        {
            //CUSTOMER MULTIPLE PAID
            decimal balance, payment_Amt, remaining_Amt, paid_amt = 0;
            payment_Amt = Convert.ToDecimal(txt_Paid_Multiple_Cust.Text);
            remaining_Amt = payment_Amt;
            if (txt_Paid_Multiple_Cust.Text == "")
            {
                MessageBox.Show("Please Enter Paid Amount");
            }
            else if(pay_mode == "NA")
            {
                MessageBox.Show("Please Select Payment Modes");
            }
            else
            {
                if (txt_reference_Multiple_Cust.Text == "")
                {
                    txt_reference_Multiple_Cust.Text = "NA";
                }
                getconnection();
                con.Open();
                foreach (DataGridViewRow row in dgv_Paid_Multiple_Cust.Rows)
                {
                    if (Convert.ToBoolean(row.Cells["select"].Value) && remaining_Amt > 0)
                    {
                        balance = Convert.ToDecimal(row.Cells["balance"].Value);
                        if (remaining_Amt - balance >= 0)
                        {
                            //Balance amt = 0, paid = paid +balance
                            cmd = new SqlCommand("UPDATE Tb_Tax_Sell_Details SET Paid_Amt = Paid_Amt + " + Convert.ToDecimal(row.Cells["balance"].Value) + ", Balance_Amt = 0 WHERE Invoice_No = '" + row.Cells["invoice_no"].Value + "'", con);
                            remaining_Amt = remaining_Amt - balance;
                            paid_amt = balance;
                        }
                        else
                        {
                            cmd = new SqlCommand("UPDATE Tb_Tax_Sell_Details SET Paid_Amt = Paid_Amt + " + remaining_Amt + ", Balance_Amt = Balance_Amt - " + remaining_Amt + " WHERE Invoice_No = '" + row.Cells["invoice_no"].Value + "'", con);
                            paid_amt = remaining_Amt;
                            remaining_Amt = 0;
                        }
                        cmd.ExecuteNonQuery();
                        cmd = new SqlCommand("Insert_Payment_Details", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Cust_Invoice_No", row.Cells["invoice_no"].Value.ToString());
                        cmd.Parameters.AddWithValue("@Supp_Invoice_No", "NA");
                        cmd.Parameters.AddWithValue("@Payment_Date", dt_Payment_Date_Cust.Text.ToString());
                        cmd.Parameters.AddWithValue("@Invoice_Amt", row.Cells["invoice_amt"].Value.ToString());
                        cmd.Parameters.AddWithValue("@Paid_Amt", paid_amt.ToString());
                        cmd.Parameters.AddWithValue("@Pay_Mode", pay_mode);
                        cmd.Parameters.AddWithValue("@reference", txt_reference_Multiple_Cust.Text);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Paid Succesfully");
                gridload_Invoice_By_Cust();
                clear_paid_data();
            }
        }
        private void clear_paid_data()
        {
            dt_Payment.ResetText();
            dt_Payment_Date_Cust.ResetText();
            dt_single_Pay.ResetText();
            txt_Invoice_No_Single.Clear();
            txt_invoice_total_Single.Clear();
            txt_paid_Amt_Single.Clear();
            txt_Paid_Multiple_Cust.Clear();
            txt_Paid_multiple_Supp.Clear();
            txt_reference_Multiple_Cust.Clear();
            txt_Reference_Single.Clear();
            txt_Reference_Supp.Clear();
            txt_Select_Invoice_Total.Clear();
            txt_Total_Supp.Clear();
            rdbtn_Cash_Cust.Checked = false;
            rdbtn_Cheque_Cust.Checked = false;
            rdbtn_Other_Cust.Checked = false;
            rdb_cash.Checked = false;
            rdb_Cash_Supp.Checked = false;
            rdb_Cheque.Checked = false;
            rdb_Cheque_Supp.Checked = false;
            rdb_Others.Checked = false;
            rdb_Other_Supp.Checked = false;
        }
        private void dgv_All_Invoices_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == dgv_All_Invoices_Cust.Columns["action"].Index)
            {
                lbl_Supp_Cust.Text = "Customer";
                txt_invoice_total_Single.Text = dgv_All_Invoices_Cust.Rows[e.RowIndex].Cells["balance_amt"].Value.ToString();
                invoice = dgv_All_Invoices_Cust.Rows[e.RowIndex].Cells["in_no"].Value.ToString();
                txt_Invoice_No_Single.Text = invoice;
                gridload_Invoice_Cust(invoice);
            }
        }
        private void gridload_Invoice_Supp(string invoice)
        {
            //try
            {
                dgv_Invoice_Details.Columns["supp_cust_invoice"].DataPropertyName = "Supp_Invoice_No";
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT Supp_Invoice_No, Payment_Date, Invoice_Amt, Paid_Amt, Reference FROM Tb_Payment_Master WHERE Supp_Invoice_No = '" + invoice + "'", con);
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
        }
        private void gridload_Invoice_Cust(string invoice)
        {
            //try
            {
                dgv_Invoice_Details.Columns["supp_cust_invoice"].DataPropertyName = "Cust_Invoice_No";
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT Cust_Invoice_No, Payment_Date, Invoice_Amt, Paid_Amt, Reference FROM Tb_Payment_Master WHERE Cust_Invoice_No = '" + invoice + "'", con);
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
        }
        private void rdbtn_Cash_CheckedChanged(object sender, EventArgs e)
        {
            if(rdbtn_Cash_Cust.Checked == true)
            {
                pay_mode = "Cash";
                panel67.Hide();
            }
        }
        private void rdbtn_Cheque_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbtn_Cheque_Cust.Checked == true)
            {
                pay_mode = "Cheque";
                panel67.Show();
            }
        }
        private void rdbtn_Other_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbtn_Other_Cust.Checked == true)
            {
                pay_mode = "Other";
                panel67.Show();
            }
       }
        public void clear_control()
        {
            txt_Invoice_No_Single.Clear();
            txt_invoice_total_Single.Clear();
            txt_paid_Amt_Single.Clear();
        }
        private void btn_Paid_Single_Click(object sender, EventArgs e)
        {
            decimal balance, payment_Amt, remaining_Amt, paid_amt = 0;
            payment_Amt = Convert.ToDecimal(txt_paid_Amt_Single.Text);
            remaining_Amt = payment_Amt;
            if (txt_paid_Amt_Single.Text == "")
            {
                MessageBox.Show("Please Enter Paid Amount");
            }
            else if (pay_mode == "NA")
            {
                MessageBox.Show("Please Select Payment Modes");
            }
            else
            {
                if (txt_Reference_Single.Text == "")
                {
                    txt_Reference_Single.Text = "NA";
                }
                getconnection();
                con.Open();
                if(lbl_Supp_Cust.Text == "Customer")
                {
                    cmd = new SqlCommand("UPDATE Tb_Tax_Sell_Details SET Paid_Amt = Paid_Amt + " + txt_paid_Amt_Single.Text + ", Balance_Amt = Balance_Amt - " + txt_paid_Amt_Single.Text + " WHERE Invoice_No = '" + txt_Invoice_No_Single.Text + "'", con);
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("Insert_Payment_Details", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Cust_Invoice_No", txt_Invoice_No_Single.Text.ToString());
                    cmd.Parameters.AddWithValue("@Supp_Invoice_No", "NA");
                    cmd.Parameters.AddWithValue("@Payment_Date", dt_single_Pay.Text.ToString());
                    cmd.Parameters.AddWithValue("@Invoice_Amt", txt_invoice_total_Single.Text.ToString());
                    cmd.Parameters.AddWithValue("@Paid_Amt", txt_paid_Amt_Single.Text.ToString());
                    cmd.Parameters.AddWithValue("@Pay_Mode", pay_mode);
                    cmd.Parameters.AddWithValue("@reference", txt_Reference_Single.Text);
                    cmd.ExecuteNonQuery();
                }
                else if (lbl_Supp_Cust.Text == "Supplier")
                {
                    cmd = new SqlCommand("UPDATE Tb_Purchase SET Paid_Amt = Paid_Amt + " + txt_paid_Amt_Single.Text + ", Balance_Amt = Balance_Amt - " + txt_paid_Amt_Single.Text + " WHERE Invoice_No = '" + txt_Invoice_No_Single.Text + "'", con);
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("Insert_Payment_Details", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Supp_Invoice_No", txt_Invoice_No_Single.Text.ToString());
                    cmd.Parameters.AddWithValue("@Cust_Invoice_No", "NA");
                    cmd.Parameters.AddWithValue("@Payment_Date", dt_single_Pay.Text.ToString());
                    cmd.Parameters.AddWithValue("@Invoice_Amt", txt_invoice_total_Single.Text.ToString());
                    cmd.Parameters.AddWithValue("@Paid_Amt", txt_paid_Amt_Single.Text.ToString());
                    cmd.Parameters.AddWithValue("@Pay_Mode", pay_mode);
                    cmd.Parameters.AddWithValue("@reference", txt_Reference_Single.Text);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Paid Succesfully");
                gridload_All_Invoice_Supp();
                dgv_Invoice_Details.Refresh();
                clear_control();
            }
        }
        private void rdb_cash_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_cash.Checked == true)
            {
                pay_mode = "Cash";
                panel44.Hide();
            }
        }
        private void rdb_Cheque_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_Cheque.Checked == true)
            {
                pay_mode = "Cheque";
                panel44.Show();
            }
        }
        private void cmb_Supplier_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmb_Supplier_Name.Text!="")
            {
                gridload_Invoice_By_Supp();
                panel59.Enabled = true;
                panel60.Enabled = true;
                rdb_Paid_Supplier.Enabled = true;
                rdb_Pending_Supplier.Enabled = true;
            }
            else
            {
                gridload_All_Invoice_Supp();
                rdb_Paid_Supplier.Enabled = false;
                rdb_Pending_Supplier.Enabled = false;
            }
        }
        private void chk_Paid_Multiple_Supplier_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Paid_Multiple_Supplier.Checked == true)
            {
                panel6.Visible = true;
                panel56.Hide();
                panel6.Dock = DockStyle.Fill;
                //rdb_Paid_Supplier.Checked = false;
                //panel10.Hide();
                //rdb_Paid_Supplier.Enabled = false;
            }
            else
            {
                panel6.Visible = false;
                panel56.Visible = true;
                panel56.Dock = DockStyle.Fill;
                panel10.Show();
            }
        }
        private void rdb_Pending_Supplier_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_Pending_Supplier.Checked == true)
            {
                gridload_Invoice_By_Supp();
                chk_Paid_Multiple_Supplier.Enabled = true;
            }
        }
        private void gridload_All_Invoice_Supp()
        {
            //try
            {
                getconnection();
                con.Open();
                if (rdb_Pending_Supplier.Checked == true)
                {
                    cmd = new SqlCommand("SELECT Invoice_No, Date_Of_Purchase, Net_Amt, Paid_Amt, Balance_Amt FROM Tb_Purchase WHERE Supplier_Name = '" + cmb_Supplier_Name.Text + "' AND Balance_Amt > 0", con);
                }
                else if(rdb_Paid_Supplier.Checked == true)
                {
                    cmd = new SqlCommand("SELECT Invoice_No, Date_Of_Purchase, Net_Amt, Paid_Amt, Balance_Amt FROM Tb_Purchase WHERE Supplier_Name = '" + cmb_Supplier_Name.Text + "' AND Balance_Amt = 0", con);
                }
                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    dgv_All_Supplier.DataSource = ds.Tables[0];
                    dgv_Paid_Multiple_Supp.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_All_Supplier.DataSource = ds.Tables[0];
                    dgv_Paid_Multiple_Supp.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();
            }
            //catch { }
            //finally { con.Close(); }
        }
        private void gridload_Invoice_By_Supp()
        {
            //try
            {
                getconnection();
                con.Open();
                if (rdb_Pending_Supplier.Checked == true)
                {
                    cmd = new SqlCommand("SELECT Invoice_No, Date_Of_Purchase, Net_Amt, Paid_Amt, Balance_Amt FROM Tb_Purchase WHERE Supplier_Name = '" + cmb_Supplier_Name.Text + "' AND Balance_Amt > 0", con);
                }
                else if (rdb_Paid_Supplier.Checked == true)
                {
                    cmd = new SqlCommand("SELECT Invoice_No, Date_Of_Purchase, Net_Amt, Paid_Amt, Balance_Amt FROM Tb_Purchase WHERE Supplier_Name = '" + cmb_Supplier_Name.Text + "' AND Balance_Amt = 0", con);
                }
                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    dgv_All_Supplier.DataSource = ds.Tables[0];
                    dgv_Paid_Multiple_Supp.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_All_Supplier.DataSource = ds.Tables[0];
                    dgv_Paid_Multiple_Supp.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();
            }
            //catch { }
            //finally { con.Close(); }
        }
        private void rbt_Paid_Supplier_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_Paid_Supplier.Checked == true)
            {
                gridload_Invoice_By_Supp();
                chk_Paid_Multiple_Supplier.Enabled = false;
                panel35.Hide();
                panel44.Hide();
                panel48.Hide();
                panel40.Hide();
            }
            else
            {
                panel35.Show();
                panel44.Show();
                panel48.Show();
                panel40.Show();
            }
        }

        private void dgv_All_Supplier_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgv_All_Supplier.Columns["action2"].Index)
            {
                lbl_Supp_Cust.Text = "Supplier";
                txt_invoice_total_Single.Text = dgv_All_Supplier.Rows[e.RowIndex].Cells["balance_amt2"].Value.ToString();
                invoice = dgv_All_Supplier.Rows[e.RowIndex].Cells["invoice_no2"].Value.ToString();
                txt_Invoice_No_Single.Text = invoice;
                gridload_Invoice_Supp(invoice);
            }
        }

        private void dgv_Paid_Multiple_Supp_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgv_Paid_Multiple_Supp.CommitEdit(DataGridViewDataErrorContexts.Commit);
            decimal total = 0;
            foreach (DataGridViewRow row in dgv_Paid_Multiple_Supp.Rows)
            {
                if (Convert.ToBoolean(row.Cells[0].Value))
                {
                    total = total + Convert.ToDecimal(row.Cells[5].Value);
                }
            }
            txt_Total_Supp.Text = total.ToString();
        }

        private void txt_Paid_Multiple_Cust_TextChanged(object sender, EventArgs e)
        {
            if (txt_Paid_Multiple_Cust.Text == "")
            {
                panel78.Hide();
                panel67.Hide();
            }
            else
            {
                panel78.Show();
            }

        }

        private void rdb_Cash_Supp_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_Cash_Supp.Checked == true)
            {
                pay_mode = "Cash";
                panel102.Hide();
            }
            else
            {
                panel102.Show();
            }
        }

        private void rdb_Cheque_Supp_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_Cheque_Supp.Checked == true)
            {
                pay_mode = "Cheque";
                panel102.Show();
            }
        }

        private void rdb_Other_Supp_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_Other_Supp.Checked == true)
            {
                pay_mode = "Other";
                panel102.Show();
            }
        }

        private void btn_Paid_Supp_Click(object sender, EventArgs e)
        {
            decimal balance, payment_Amt, remaining_Amt, paid_amt = 0;
            payment_Amt = Convert.ToDecimal(txt_Paid_multiple_Supp.Text);
            remaining_Amt = payment_Amt;

            if (txt_Paid_multiple_Supp.Text == "")
            {
                MessageBox.Show("Please Enter Paid Amount");
            }
            else if (pay_mode == "NA")
            {
                MessageBox.Show("Please Select Payment Modes");
            }
            else
            {
                if (txt_Reference_Supp.Text == "")
                {
                    txt_Reference_Supp.Text = "NA";
                }

                getconnection();
                con.Open();

                foreach (DataGridViewRow row in dgv_Paid_Multiple_Supp.Rows)
                {
                    if (Convert.ToBoolean(row.Cells["select2"].Value) && remaining_Amt > 0)
                    {
                        balance = Convert.ToDecimal(row.Cells["balance2"].Value);
                        if (remaining_Amt - balance >= 0)
                        {
                            //Balance amt = 0, paid = paid +balance
                            cmd = new SqlCommand("UPDATE Tb_Purchase SET Paid_Amt = Paid_Amt + " + Convert.ToDecimal(row.Cells["balance2"].Value) + ", Balance_Amt = 0 WHERE Invoice_No = '" + row.Cells["invoice2"].Value + "'", con);
                            remaining_Amt = remaining_Amt - balance;
                            paid_amt = balance;
                        }

                        else
                        {
                            cmd = new SqlCommand("UPDATE Tb_Purchase SET Paid_Amt = Paid_Amt + " + remaining_Amt + ", Balance_Amt = Balance_Amt - " + remaining_Amt + " WHERE Invoice_No = '" + row.Cells["invoice2"].Value + "'", con);
                            paid_amt = remaining_Amt;
                            remaining_Amt = 0;
                        }

                        cmd.ExecuteNonQuery();

                        cmd = new SqlCommand("Insert_Payment_Details", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        
                        cmd.Parameters.AddWithValue("@Supp_Invoice_No", row.Cells["invoice2"].Value.ToString());
                        cmd.Parameters.AddWithValue("@Cust_Invoice_No", "NA");
                        cmd.Parameters.AddWithValue("@Payment_Date", dt_Payment.Text.ToString());
                        cmd.Parameters.AddWithValue("@Invoice_Amt", row.Cells["in_amt2"].Value.ToString());
                        cmd.Parameters.AddWithValue("@Paid_Amt", paid_amt.ToString());
                        cmd.Parameters.AddWithValue("@Pay_Mode", pay_mode);
                        cmd.Parameters.AddWithValue("@reference", txt_Reference_Supp.Text);
                        cmd.ExecuteNonQuery();

                    }
                }
                MessageBox.Show("Paid Succesfully");
                //gridload_All_Invoice_Supp();
                gridload_Invoice_By_Supp();
            }
        }

        private void txt_paid_Amt_Single_TextChanged(object sender, EventArgs e)
        {
            if(txt_paid_Amt_Single.Text=="")
            {
                panel40.Hide();
                panel44.Hide();
            }
            else
            {
                panel40.Show();
            }
        }

        private void panel58_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel61_Paint(object sender, PaintEventArgs e)
        {
            cmb_Customer_Supplier.Text = "";
        }

        private void txt_Paid_multiple_Supp_TextChanged(object sender, EventArgs e)
        {
            if(txt_Paid_multiple_Supp.Text!="")
            {
                panel104.Show();
            }
            else
            {
                panel104.Hide();
                panel102.Hide();
            }

        }

        private void panel16_Paint(object sender, PaintEventArgs e)
        {
            cmb_Supplier_Name.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 2)
            {
                panel34.Hide();
                Filling_Payment fp = new Filling_Payment();
                fp.TopLevel = false;
                fp.Parent = panel135;
                fp.Show();
            }
            else if (tabControl1.SelectedIndex == 1 || tabControl1.SelectedIndex == 0)
            {
                panel34.Show();
            }
        }

        private void cmb_Supplier_Name_TextChanged(object sender, EventArgs e)
        {
            if (cmb_Supplier_Name.Text != "")
            {
                gridload_Invoice_By_Supp();
                panel59.Enabled = true;
                panel60.Enabled = true;
                rdb_Paid_Supplier.Enabled = true;
                rdb_Pending_Supplier.Enabled = true;
            }
            else
            {
                gridload_All_Invoice_Supp();
                rdb_Paid_Supplier.Enabled = false;
                rdb_Pending_Supplier.Enabled = false;
            }
        }

        private void cmb_Customer_Supplier_TextChanged(object sender, EventArgs e)
        {
            if (cmb_Customer_Supplier.Text != "")
            {
                panel49.Enabled = true;
                panel21.Enabled = true;
                rdb_Pending_Invoice_Cust.Enabled = true;
                rdb_Paid_Invoice_Cust.Enabled = true;
                gridload_Invoice_By_Cust();
            }
            else
            {
                rdb_Pending_Invoice_Cust.Enabled = false;
                rdb_Paid_Invoice_Cust.Enabled = false;
                gridload_All_Invoice_Cust();
            }
        }

        private void chk_Sel_All_supp_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dgv_Paid_Multiple_Supp.RowCount; i++)
            {
                dgv_Paid_Multiple_Supp.Rows[i].Cells["select2"].Value = chk_Sel_All_supp.Checked;
            }
        }

        private void chk_Sel_All_Cust_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dgv_Paid_Multiple_Cust.RowCount; i++)
            {
                dgv_Paid_Multiple_Cust.Rows[i].Cells["select"].Value = chk_Sel_All_Cust.Checked;
            }
        }

        private void btn_Print_Out_Cust_Click(object sender, EventArgs e)
        {
            Report_Outstanding_Cust form = new Report_Outstanding_Cust();
            form.get_ID(cmb_Customer_Supplier.Text);
            form.Show();
        }

        private void panel20_Paint(object sender, PaintEventArgs e)
        {
          
        }

        private void rdb_Others_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_Others.Checked == true)
            {
                pay_mode = "Other";
                panel44.Show();
            }
        }
    }
}

