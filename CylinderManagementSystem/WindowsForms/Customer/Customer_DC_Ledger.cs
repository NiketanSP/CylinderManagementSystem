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
    public partial class Customer_DC_Ledger : Form
    {
        SqlConnection con;
        SqlDataReader sdr;
        SqlCommand cmd;
        SqlDataAdapter adpt;
        DataTable dt;
        string cdc_No;
        int save_Counter = 0;

        public Customer_DC_Ledger()
        {
            InitializeComponent();
            gridload_Cdc_Details();
        }

        public void getconnection()
        {
            string str = ConfigurationManager.ConnectionStrings["CylinderData"].ConnectionString;
            con = new SqlConnection(str);
        }
        private void dgv_CDC_Details_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string value = dgv_CDC_Details.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            if (value == "View")
            {
                cdc_No = dgv_CDC_Details.Rows[e.RowIndex].Cells["c_dc_no"].Value.ToString();
                lbl_Cdc_No.Text= dgv_CDC_Details.Rows[e.RowIndex].Cells["c_dc_no"].Value.ToString();
                //Customer_DC cust_dc = new Customer_DC();
                //  cust_dc.search_Cdc_Id(id);
                //  cust_dc.ShowDialog();
                gridload_Cdc_Content();
                load_CDC_Item_Combo();
                load_Cust_Dc_No();
                panel30.Hide();
                panel2.Hide();
                dgv_Content_Details.Show();
                dgv_Content_Details.Dock = DockStyle.Fill;
                panel46.Show();
                panel37.Show();
                panel49.Show();
                btn_Save.Text = "Save";
                dgv_Content_Details.Enabled = false;
            }
        }
        public void gridload_Cdc_Details()
        {
            //try
            {
                getconnection();
                con.Open();
                if (cmb_search.Text == "ALL" || cmb_search.Text == "")
                {
                    cmd = new SqlCommand("SELECT C_Dc_Date, C_Dc_No, C_Name, Total_Amt, Paid_Amt,Balance_Amt, Remark,Pay_Mode, Total_Items FROM Tb_CDC_Details WHERE Status = 'NA'", con);
                }
                else if (cmb_search.Text == "BY CUSTOMER")
                {
                    cmd = new SqlCommand("SELECT C_Dc_Date, C_Dc_No, C_Name, Total_Amt, Paid_Amt,Balance_Amt, Remark,Pay_Mode, Total_Items FROM Tb_CDC_Details WHERE C_Name = '" + cmb_Customer.Text.ToString() + "' AND Status = 'NA'", con);
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
            //catch { }
            // finally { con.Close(); }
        }

        private void Customer_DC_Ledger_Load(object sender, EventArgs e)
        {
            panel20.Hide();
            panel19.Hide();
            panel23.Hide();
            panel5.Hide();
            panel38.Hide();
            panel12.Hide();
            panel37.Hide();
            cmb_Cust_DC_No.Visible = false;
            txt_Cust_Dc_No.Visible = true;
            panel30.Show();
            panel30.Dock = DockStyle.Fill;
            panel49.Hide();
            panel46.Hide();
            dgv_CDC_Details.Dock = DockStyle.Fill;
            btn_Save.Text = "Export To Excel";
        }
        private void load_CDC_Combo()
        {
            cmb_CDC_Id.Items.Clear();
            cmb_CDC_Id.ResetText();
            try
            {
                getconnection();
                con.Open();
                if (cmb_search.Text == "ALL")
                {
                    cmd = new SqlCommand("SELECT DISTINCT C_Dc_No FROM Tb_CDC_Details WHERE Status = 'NA'", con);
                }
                else if (cmb_search.Text == "BY CUSTOMER")
                {
                    cmd = new SqlCommand("SELECT DISTINCT C_Dc_No FROM Tb_CDC_Details WHERE C_Name = '" + cmb_Customer.Text.ToString() + "' AND Status = 'NA'", con);
                }
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    cmb_CDC_Id.Items.Add(sdr[0]);
                }
                sdr.Close();
            }
            catch { }
            finally { con.Close(); }
        }
        private void load_Customer_Combo()
        {
            cmb_Customer.Items.Clear();
            try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT DISTINCT Cust_Name FROM Tb_Customer_Master", con);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    cmb_Customer.Items.Add(sdr[0]);
                }
                sdr.Close();
            }
            catch { }
            finally { con.Close(); }
        }

        private void cmb_Customer_SelectedIndexChanged(object sender, EventArgs e)
        {
            panel19.Show();
            load_CDC_Combo();
            gridload_Cdc_Details();
        }

        private void cmb_search_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_search.Text == "ALL")
            {
                panel12.Hide();
                panel5.Hide();
                panel20.Hide();
                panel23.Hide();
                panel38.Hide();
                gridload_Cdc_Details();
                load_CDC_Combo();
            }
            else if (cmb_search.Text == "BY CUSTOMER")
            {
                panel5.Hide();
                panel20.Hide();
                panel23.Hide();
                panel38.Hide();
                panel19.Hide();
                panel12.Show();
                gridload_Cdc_Details();
                load_Customer_Combo();
            }
        }
        private void cmb_CDC_Id_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT C_Dc_Date, C_Dc_No, C_Name, Total_Amt, Paid_Amt, Pay_Mode, Remark, Total_Items,Balance_Amt FROM Tb_CDC_Details WHERE C_Dc_No = '" + cmb_CDC_Id.Text.ToString() + "' AND Status = 'NA'", con);
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
        private void rdb_By_Date_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_By_Date.Checked == true)
            {
                panel28.Show();
                panel5.Show();
                //panel29.Hide();
                show_by_Date();
            }
        }
        private void rdb_Btwn_Date_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_Btwn_Date.Checked == true)
            {
                panel28.Show();
                panel20.Show();
                panel5.Show();
                //  panel29.Show();
                show_Between_Date();
            }
        }
        public void show_by_Date()
        {
            String date = dtp_By_Date.Text.ToString();
            try
            {
                getconnection();
                con.Open();
                if (cmb_search.Text == "BY CUSTOMER")
                {
                    cmd = new SqlCommand("SELECT C_Dc_Date, C_Dc_No, C_Name, Total_Amt, Paid_Amt, Remark, Total_Items FROM Tb_CDC_Details WHERE C_Name = '" + cmb_Customer.Text.ToString() + "' AND C_Dc_Date = '" + date + "' AND Status = 'NA'", con);
                }
                else
                {
                    cmd = new SqlCommand("SELECT C_Dc_Date, C_Dc_No, C_Name, Total_Amt, Paid_Amt, Remark, Total_Items FROM Tb_CDC_Details WHERE C_Dc_Date = '" + date + "' AND Status = 'NA'", con);
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

        public void show_Between_Date()
        {
            String start_date = dtp_By_Date.Text.ToString();
            String end_date = dtp_Btwn_Date.Text.ToString();
            try
            {
                getconnection();
                con.Open();
                if (cmb_search.Text == "BY CUSTOMER")
                {
                    cmd = new SqlCommand("SELECT C_Dc_Date, C_Dc_No, C_Name, Total_Amt, Paid_Amt, Remark, Total_Items FROM Tb_CDC_Details WHERE C_Name = '" + cmb_Customer.Text.ToString() + "' AND Status = 'NA' AND C_Dc_Date BETWEEN '" + start_date + "' AND '" + end_date + "'", con);
                }
                else
                {
                    cmd = new SqlCommand("SELECT C_Dc_Date, C_Dc_No, C_Name, Total_Amt, Paid_Amt, Remark, Total_Items FROM Tb_CDC_Details WHERE Status = 'NA' AND C_Dc_Date BETWEEN '" + start_date + "' AND '" + end_date + "'", con);
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
        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
        private void dtp_By_Date_ValueChanged(object sender, EventArgs e)
        {
            if (rdb_By_Date.Checked == true)
            {
                show_by_Date();
            }
            else if (rdb_Btwn_Date.Checked == true)
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
            else if (rdb_Btwn_Date.Checked == true)
            {
                show_Between_Date();
            }
        }
        private void rdbtn_Material_CheckedChanged(object sender, EventArgs e)
        {
            CurrencyManager cm = (CurrencyManager)BindingContext[dgv_Content_Details.DataSource];
            cm.SuspendBinding();
            foreach (DataGridViewRow row in dgv_Content_Details.Rows)
            {
                if (row.Cells["item"].Value.ToString() == "NA")
                {
                    row.Visible = true;
                }
                else
                    row.Visible = false;
            }
            cm.ResumeBinding();
        }
        private void rdbtn_Cylinder_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbtn_Cylinder.Checked == true)
            {
                panel26.Show();
                load_CDC_Item_Combo();
                CurrencyManager cm = (CurrencyManager)BindingContext[dgv_Content_Details.DataSource];
                cm.SuspendBinding();
                foreach (DataGridViewRow row in dgv_Content_Details.Rows)
                {
                    if (row.Cells[4].Value.ToString() == "NA")
                    {
                        row.Visible = false;
                    }
                    else
                        row.Visible = true;
                }
                cm.ResumeBinding();
            }
            else
            {
                panel26.Hide();
            }
        }
        public void gridload_Cdc_Content()
        {
            receive_count();
            pending_count();
            //try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT C_Dc_No,Cy_Type, Particulars, Sr_No, Item_No, Rate, Sell_Status,Receive_Status,Cust_Dc_No FROM Tb_CDC_Content_Details WHERE C_Dc_No = '" + cdc_No + "'", con);
                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    dgv_Content_Details.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_Content_Details.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();
                int count = 0;
                foreach (DataGridViewRow row in dgv_Content_Details.Rows)
                {
                    if (row.Cells["receive_status"].Value.ToString().Contains("Received"))
                    {
                        row.Cells["received"].Value = true;
                        row.ReadOnly = true;
                        count++;
                    }
                    if (row.Cells["status"].Value.ToString() == "SELL")
                    {
                        row.DefaultCellStyle.BackColor = Color.LightGray;
                        row.ReadOnly = true;
                    }
                    if(row.Cells["cust_dc"].Value.ToString() == "")
                    {
                        row.Cells["cust_dc"].Value = "Pending";
                    }
                }
                if (count == dgv_Content_Details.RowCount)
                {
                  //  panel49.Enabled = false;
                }
            }
            //catch { }
            // finally { con.Close(); }
            foreach (DataGridViewRow row in dgv_Content_Details.Rows)
            {
                row.Cells["condition"].Value = "EMPTY";
            }
        }
        public void receive_count()
        {
            getconnection();
            con.Open();
            
                cmd = new SqlCommand("SELECT count(Receive_Status) FROM Tb_CDC_Content_Details where Receive_Status='Received' and C_Dc_No ='" +lbl_Cdc_No.Text + "'", con);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    lbl_Receive.Text = sdr[0].ToString();
                }
                sdr.Close();
        }
        public void pending_count()
        {
            getconnection();
            con.Open();
            cmd = new SqlCommand("SELECT count(Receive_Status) FROM Tb_CDC_Content_Details where Receive_Status='Pending' and C_Dc_No ='" + lbl_Cdc_No.Text + "'", con);
            sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                lbl_Pending.Text = sdr[0].ToString();
            }
            sdr.Close();
        }
        public void load_CDC_Item_Combo()
        {
            cmb_CDC_Item_No.Items.Clear();
            try
            {
                getconnection();
                con.Open();

                cmd = new SqlCommand("SELECT DISTINCT Item_No FROM Tb_CDC_Content_Details WHERE C_Dc_No = '" + cdc_No + "' and Receive_Status = 'Pending'", con);
                sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    if (sdr[0].ToString() != "NA")
                    {
                        cmb_CDC_Item_No.Items.Add(sdr[0]);
                    }
                }
                sdr.Close();
            }
            catch { }
            finally { con.Close(); }
        }

        private void cmb_CDC_Item_No_TextChanged(object sender, EventArgs e)
        {
            CurrencyManager cm = (CurrencyManager)BindingContext[dgv_Content_Details.DataSource];
            cm.SuspendBinding();
            string dgv_item, cmb_item;
            cmb_item = cmb_CDC_Item_No.Text.ToString();
            for (int i = 0; i < dgv_Content_Details.Rows.Count; i++)
            {
                dgv_item = dgv_Content_Details.Rows[i].Cells["item"].Value.ToString();
                if (dgv_item.StartsWith(cmb_item))
                {
                    dgv_Content_Details.Rows[i].Visible = true;
                }
                else
                {
                    dgv_Content_Details.Rows[i].Visible = false;
                }
            }
            cm.ResumeBinding();
        }
        private void dgv_Content_Details_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dgv_Content_Details.Rows[e.RowIndex].Cells["sr"].Value = (e.RowIndex + 1).ToString();
        }
        private void dgv_Content_Details_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (btn_Save.Text == "Save")
            {
                save_Counter = 0;
                // try
                {
                    getconnection();
                    con.Open();
                    foreach (DataGridViewRow row in dgv_Content_Details.Rows)
                    {
                        if (row.ReadOnly == false)
                        {
                            if (Convert.ToBoolean(row.Cells["received"].Value))
                            {
                                cmd = new SqlCommand("UPDATE Tb_CDC_Content_Details SET Receive_Status = 'Received', Cust_Dc_No = '" + txt_Cust_Dc_No.Text.ToString() + "', Received_Date = '" + dateTimePicker1.Text.ToString() + "' WHERE Item_No = '" + row.Cells["item"].Value.ToString() + "' AND C_Dc_No = '" + row.Cells["cdcno"].Value.ToString() + "'", con);
                                cmd.ExecuteNonQuery();

                                cmd = new SqlCommand("UPDATE Tb_Inventory_Master SET In_Stock = In_Stock + 1 ,Out_Stock = Out_Stock - 1 WHERE Particulars = '" + row.Cells["particular"].Value.ToString() + "'", con);
                                cmd.ExecuteNonQuery();

                                cmd = new SqlCommand("UPDATE Tb_Purchase_Content_Master SET Cylinder_Status = '" + row.Cells["condition"].Value.ToString() + "', Cust_Supp_Name = 'NA' WHERE Part_No = '" + row.Cells["item"].Value.ToString() + "'", con);
                                cmd.ExecuteNonQuery();

                                save_Counter++;
                            }
                        }
                    }
                    MessageBox.Show(save_Counter + " Entries Saved SuccessFully.!", "Success");
                    btn_Back.PerformClick();
                }
                //catch { }
                con.Close();
                txt_Cust_Dc_No.ResetText();
                cmb_CDC_Item_No.ResetText();
            }
            else if (btn_Save.Text == "Export To Excel")
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
                        for (j = 1; j <= 9; j++)
                        {
                            Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[StartRow, StartCol + j - 1];
                            myRange.Value2 = dgv_CDC_Details.Columns[j].HeaderText;
                        }
                        StartRow++;
                        //Write datagridview content
                        for (i = 0; i < dgv_CDC_Details.Rows.Count; i++)
                        {
                            for (j = 1; j <= 9; j++)
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
        private void chk_If_Cancel_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void button3_Click(object sender, EventArgs e)
        {
            clearcontrol();
            panel37.Hide();
            panel30.Show();
            panel2.Show();
            panel30.Dock = DockStyle.Fill;
            panel49.Hide();
            panel46.Hide();
            txt_Cust_Dc_No.ResetText();
            cmb_CDC_Item_No.ResetText();
            panel49.Enabled = true;
            dgv_CDC_Details.Dock = DockStyle.Fill;
            btn_Save.Text = "Save";
        }
        public void clearcontrol()
        {
            cmb_search.ResetText();
            cmb_Customer.ResetText();
        }
        private void panel30_Paint(object sender, PaintEventArgs e)
        {

        }
        private void panel10_Paint(object sender, PaintEventArgs e)
        {

        }
        private void label10_Click(object sender, EventArgs e)
        {

        }
        private void panel40_Paint(object sender, PaintEventArgs e)
        {

        }
        private void panel49_Paint(object sender, PaintEventArgs e)
        {

        }
        private void panel37_Paint(object sender, PaintEventArgs e)
        {

        }
        private void txt_Cust_Dc_No_TextChanged(object sender, EventArgs e)
        {
            if (txt_Cust_Dc_No.Text != "")
            {
                dgv_Content_Details.Enabled = true;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }
        private void cmb_Search_By_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_Search_By.Text == "CDC ID")
            {
                panel38.Show();
                panel23.Hide();
                panel5.Hide();
                panel20.Hide();
            }
            else if (cmb_Search_By.Text == "DATE WISE")
            {
                panel38.Hide();
                panel23.Show();
            }
        }
        private void btn_Show_Click(object sender, EventArgs e)
        {
            panel30.Hide();
            panel2.Hide();
            dgv_Content_Details.Show();
            dgv_Content_Details.Dock = DockStyle.Fill;
            panel46.Show();
            panel37.Show();
            panel49.Show();
            btn_Save.Text = "Save";
            load_Cust_Dc_No();
            //try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT Cy_Type, Particulars, Sr_No, Item_No, Rate, Sell_Status,Receive_Status,C_Dc_No,'NA' as Cust_Dc_No FROM Tb_CDC_Content_Details WHERE Receive_Status = 'Pending'", con);
                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    dgv_Content_Details.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_Content_Details.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();
            }
            foreach (DataGridViewRow row in dgv_Content_Details.Rows)
            {
                row.Cells["condition"].Value = "EMPTY";
            }

            load_Item_Combo();
        }
        public void load_Item_Combo()
        {
            cmb_CDC_Item_No.Items.Clear();
            try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT DISTINCT Item_No FROM Tb_CDC_Content_Details WHERE Receive_Status = 'Pending'", con);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    if (sdr[0].ToString() != "NA")
                    {
                        cmb_CDC_Item_No.Items.Add(sdr[0]);
                    }
                }
                sdr.Close();
            }
            catch { }
            finally { con.Close(); }
        }
        private void btn_Search_Click(object sender, EventArgs e)
        {
            if(cmb_Cust_DC_No.Visible == true)
            {
                cmb_Cust_DC_No.Visible = false;
                txt_Cust_Dc_No.Visible = true;
            }
            else
            {
                cmb_Cust_DC_No.Visible = true;
                txt_Cust_Dc_No.Visible = false;
            }
        }
        public void load_Cust_Dc_No()
        {
            cmb_Cust_DC_No.Items.Clear();
            try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT DISTINCT Cust_Dc_No FROM Tb_CDC_Content_Details", con);
                sdr = cmd.ExecuteReader();
               while (sdr.Read())
                {
                    if (sdr[0].ToString() != "")
                    {
                        cmb_Cust_DC_No.Items.Add(sdr[0]);
                    }
                }
                sdr.Close();
            }
            catch { }
            finally { con.Close(); }
        }
        private void cmb_Cust_DC_No_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT C_Dc_No,Cy_Type, Particulars, Sr_No, Item_No, Rate, Sell_Status,Receive_Status,Cust_Dc_No FROM Tb_CDC_Content_Details WHERE Cust_Dc_No = '" + cmb_Cust_DC_No.Text + "'", con);
                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    dgv_Content_Details.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_Content_Details.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();
                cmd = new SqlCommand("SELECT Received_Date FROM Tb_CDC_Content_Details WHERE Cust_Dc_No = '" + cmb_Cust_DC_No.Text + "'", con);
                SqlDataReader sdr1 = cmd.ExecuteReader();
                if(sdr1.Read() && sdr1[0] != null)
                {
                    dateTimePicker1.Value = Convert.ToDateTime(sdr1[0]);
                }
                sdr1.Close();
            }
            dgv_Content_Details.Enabled = true;
            foreach (DataGridViewRow row in dgv_Content_Details.Rows)
            {
                if (row.Cells["receive_status"].Value.ToString().Contains("Received"))
                {
                    row.Cells["received"].Value = true;
                    row.ReadOnly = true;
                }
                if (row.Cells["status"].Value.ToString() == "SELL")
                {
                    row.DefaultCellStyle.BackColor = Color.LightGray;
                    row.ReadOnly = true;
                }
                if (row.Cells["cust_dc"].Value.ToString() == "")
                {
                    row.Cells["cust_dc"].Value = "Pending";
                }
            }
        }

        private void lbl_Cdc_No_Click(object sender, EventArgs e)
        {

        }
    }
}
