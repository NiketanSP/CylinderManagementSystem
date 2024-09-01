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
    public partial class Filling_Payment : Form
    {
        SqlConnection con;
        SqlDataReader sdr;
        SqlCommand cmd;
        SqlDataAdapter adpt;
        DataTable dt;
        string pay_mode;
        public Filling_Payment()
        {
            InitializeComponent();
        }
        private void Filling_Payment_Load(object sender, EventArgs e)
        {
            load_Supplier_Name();
            rdb_Pending_Invoice_Cust.Checked = true;
            gridload_All_Invoice();
            panel74.Enabled = false;
            panel7.Hide();
            panel6.Hide();
            panel10.Dock = DockStyle.Fill;
        }
        public void getconnection()
        {
            string str = ConfigurationManager.ConnectionStrings["CylinderData"].ConnectionString;
            con = new SqlConnection(str);
        }
        private void gridload_All_Invoice()
        {
            //try
            {
                getconnection();
                con.Open();
                if (rdb_Pending_Invoice_Cust.Checked == true)
                {
                    cmd = new SqlCommand("SELECT Fill_ID, CONVERT(DATE,Fill_Date,103) AS Fill_Date, Net_Amt, Paid_Amt, Balance_Amt FROM Tb_Fill_Master WHERE Balance_Amt > 0", con);
                }
                else if (rdb_Paid_Invoice_Cust.Checked == true)
                {
                    cmd = new SqlCommand("SELECT Fill_ID, CONVERT(DATE,Fill_Date,103) AS Fill_Date, Net_Amt, Paid_Amt, Balance_Amt FROM Tb_Fill_Master WHERE Balance_Amt = 0", con);
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
        private void load_Supplier_Name()
        {
            cmb_Customer_Supplier.Items.Clear();
            //try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT DISTINCT Supplier_Name FROM Tb_Fill_Master", con);
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
        private void cmb_Customer_Supplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_Customer_Supplier.Text == "")
            {
                gridload_All_Invoice();
            }
            else
            {
                gridload_By_Supplier();
            }
        }
        private void gridload_By_Supplier()
        {
            //try
            {
                getconnection();
                con.Open();
                if (rdb_Paid_Invoice_Cust.Checked == true)
                {
                    cmd = new SqlCommand("SELECT Fill_ID, CONVERT(DATE,Fill_Date,103) AS Fill_Date, Net_Amt, Paid_Amt, Balance_Amt FROM Tb_Fill_Master WHERE Balance_Amt = 0 AND Supplier_Name = '" + cmb_Customer_Supplier.Text + "'", con);
                }
                else if (rdb_Pending_Invoice_Cust.Checked == true)
                {
                    cmd = new SqlCommand("SELECT Fill_ID, CONVERT(DATE,Fill_Date,103) AS Fill_Date, Net_Amt, Paid_Amt, Balance_Amt FROM Tb_Fill_Master WHERE Balance_Amt > 0 AND Supplier_Name = '" + cmb_Customer_Supplier.Text + "'", con);
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
        private void rdb_Paid_Invoice_Cust_CheckedChanged(object sender, EventArgs e)
        {
            if (cmb_Customer_Supplier.Text == "")
            {
                gridload_All_Invoice();
            }
            else
            {
                gridload_By_Supplier();
            }
        }
        private void rdb_Pending_Invoice_Cust_CheckedChanged(object sender, EventArgs e)
        {
            if (cmb_Customer_Supplier.Text == "")
            {
                gridload_All_Invoice();
            }
            else
            {
                gridload_By_Supplier();
            }
        }
        private void chk_Paid_Multiple_Cust_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Paid_Multiple.Checked == true)
            {
                panel10.Hide();
                panel6.Show();
                panel7.Show();
                panel7.Dock = DockStyle.Fill;
                panel16.Hide();
                clear_Invoice_Data();
            }
            else
            {
                panel10.Show();
                panel6.Hide();
                panel7.Hide();
                panel10.Dock = DockStyle.Fill;
                panel16.Show();
                clear_Invoice_Data();
            }
        }
        private void clear_Invoice_Data()
        {
            txt_Filling_DC.Clear();
            txt_Balance_Amt.Clear();
            txt_Invoice_Total.Clear();
            txt_Paid_Amt.Clear();
            txt_reference.Clear();
            rdbtn_Cash_Cust.Checked = false;
            rdbtn_Cheque_Cust.Checked = false;
            rdbtn_Other_Cust.Checked = false;
        }
        private void cmb_Customer_Supplier_TextChanged(object sender, EventArgs e)
        {
            if (cmb_Customer_Supplier.Text == "")
            {
                gridload_All_Invoice();
            }
            else
            {
                gridload_By_Supplier();
            }
        }
        private void dgv_All_Invoices_Cust_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgv_All_Invoices_Cust.Columns["action"].Index)
            {
                panel74.Enabled = true;
                txt_Balance_Amt.Text = dgv_All_Invoices_Cust.Rows[e.RowIndex].Cells["balance_amt"].Value.ToString();
                txt_Filling_DC.Text = dgv_All_Invoices_Cust.Rows[e.RowIndex].Cells["in_no"].Value.ToString();
                txt_Invoice_Total.Text = dgv_All_Invoices_Cust.Rows[e.RowIndex].Cells["in_amt"].Value.ToString();
                txt_Filling_DC.Text = dgv_All_Invoices_Cust.Rows[e.RowIndex].Cells["in_no"].Value.ToString();
                dt_Payment_Date.Text = dgv_All_Invoices_Cust.Rows[e.RowIndex].Cells["in_date"].Value.ToString();
            }
        }
        private void dgv_Paid_Multiple_Cust_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            panel74.Enabled = true;
            dgv_Paid_Multiple_Cust.CommitEdit(DataGridViewDataErrorContexts.Commit);
            decimal total = 0, balance = 0;
            foreach (DataGridViewRow row in dgv_Paid_Multiple_Cust.Rows)
            {
                if (Convert.ToBoolean(row.Cells["select"].Value))
                {
                    total = total + Convert.ToDecimal(row.Cells["invoice_amt"].Value);
                    balance = balance + Convert.ToDecimal(row.Cells["balance"].Value);
                }
            }
            txt_Invoice_Total.Text = total.ToString();
            txt_Balance_Amt.Text = balance.ToString();
        }
        private void btn_Paid_Multiple_Cust_Click(object sender, EventArgs e)
        {
            if (chk_Paid_Multiple.Checked == true)
            {
                decimal balance, payment_Amt, remaining_Amt, paid_amt = 0;
                payment_Amt = Convert.ToDecimal(txt_Paid_Amt.Text);
                remaining_Amt = payment_Amt;
                if (txt_Paid_Amt.Text == "")
                {
                    MessageBox.Show("Please Enter Paid Amount");
                }
                else if (pay_mode == "NA")
                {
                    MessageBox.Show("Please Select Payment Modes");
                }
                else
                {
                    if (txt_reference.Text == "")
                    {
                        txt_reference.Text = "NA";
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
                                cmd = new SqlCommand("UPDATE Tb_Fill_Master SET Paid_Amt = Paid_Amt + " + Convert.ToDecimal(row.Cells["balance"].Value) + ", Balance_Amt = 0 WHERE Fill_ID = '" + row.Cells["invoice_no"].Value + "'", con);
                                remaining_Amt = remaining_Amt - balance;
                                paid_amt = balance;
                            }
                            else
                            {
                                cmd = new SqlCommand("UPDATE Tb_Fill_Master SET Paid_Amt = Paid_Amt + " + remaining_Amt + ", Balance_Amt = Balance_Amt - " + remaining_Amt + " WHERE Fill_ID = '" + row.Cells["invoice_no"].Value + "'", con);
                                paid_amt = remaining_Amt;
                                remaining_Amt = 0;
                            }
                            cmd.ExecuteNonQuery();
                            cmd = new SqlCommand("Insert_Filling_Payment", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@Fill_DC_No", row.Cells["invoice_no"].Value.ToString());
                            cmd.Parameters.AddWithValue("@Payment_Date", dt_Payment_Date.Text.ToString());
                            cmd.Parameters.AddWithValue("@Invoice_Amt", row.Cells["invoice_amt"].Value.ToString());
                            cmd.Parameters.AddWithValue("@Paid_Amt", paid_amt.ToString());
                            cmd.Parameters.AddWithValue("@Pay_Mode", pay_mode);
                            cmd.Parameters.AddWithValue("@reference", txt_reference.Text);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Paid Successfully");
                    gridload_By_Supplier();
                    clear_Invoice_Data();
                }
            }
            else
            {
                decimal balance, payment_Amt, remaining_Amt, paid_amt = 0;
                payment_Amt = Convert.ToDecimal(txt_Paid_Amt.Text);
                remaining_Amt = payment_Amt;

                if (txt_Paid_Amt.Text == "")
                {
                    MessageBox.Show("Please Enter Paid Amount");
                }
                else if (pay_mode == "NA")
                {
                    MessageBox.Show("Please Select Payment Modes");
                }
                else
                {
                    if (txt_reference.Text == "")
                    {
                        txt_reference.Text = "NA";
                    }
                    getconnection();
                    con.Open();
                    cmd = new SqlCommand("UPDATE Tb_Fill_Master SET Paid_Amt = Paid_Amt + " + txt_Paid_Amt.Text + ", Balance_Amt = Balance_Amt - " + txt_Paid_Amt.Text + " WHERE Fill_ID = '" + txt_Filling_DC.Text + "'", con);
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("Insert_Filling_Payment", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Fill_DC_No", txt_Filling_DC.Text.ToString());
                    cmd.Parameters.AddWithValue("@Payment_Date", dt_Payment_Date.Text.ToString());
                    cmd.Parameters.AddWithValue("@Invoice_Amt", txt_Invoice_Total.Text.ToString());
                    cmd.Parameters.AddWithValue("@Paid_Amt", txt_Paid_Amt.Text.ToString());
                    cmd.Parameters.AddWithValue("@Pay_Mode", pay_mode);
                    cmd.Parameters.AddWithValue("@reference", txt_reference.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Paid Successfully");
                    gridload_By_Supplier();
                    clear_Invoice_Data();
                }
            }
        }
        private void rdbtn_Other_Cust_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbtn_Other_Cust.Checked == true)
            {
                pay_mode = "Other";
                panel67.Show();
            }
        }
        private void rdbtn_Cheque_Cust_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbtn_Cheque_Cust.Checked == true)
            {
                pay_mode = "Cheque";
                panel67.Show();
            }
        }
        private void rdbtn_Cash_Cust_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbtn_Cash_Cust.Checked == true)
            {
                pay_mode = "Cash";
            }
        }
        private void chk_Sel_All_Fill_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dgv_Paid_Multiple_Cust.RowCount; i++)
            {
                dgv_Paid_Multiple_Cust.Rows[i].Cells["select"].Value = chk_Sel_All_Fill.Checked;
            }
        }

        private void panel55_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel13_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
