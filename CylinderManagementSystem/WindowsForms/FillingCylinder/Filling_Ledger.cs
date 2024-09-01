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
    public partial class Filling_Ledger : Form
    {

        SqlConnection con;
        SqlDataReader sdr;
        SqlCommand cmd;
        SqlDataAdapter adpt;
        DataTable dt;
        string id;
        int save_Counter = 0;
        public void getconnection()
        {
            string str = ConfigurationManager.ConnectionStrings["CylinderData"].ConnectionString;
            con = new SqlConnection(str);
        }
        public Filling_Ledger()
        {
            InitializeComponent();
            gridload();
            //Load_FillView();
        }
        public void load_FDC_ID()
        {
            try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT DISTINCT Fill_ID FROM Tb_Fill_Master", con);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    cmb_FDC_ID.Items.Add(sdr[0]);
                }
                sdr.Close();
            }
            catch { }
            finally { con.Close(); }
        }
        public void load_supplier()
        {
            try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT DISTINCT Supp_CompName FROM Tb_Supplier_Master where Status='FILLING' ", con);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    cmb_supplier_name.Items.Add(sdr[0]);
                }
                sdr.Close();
            }
           catch { }
            finally { con.Close(); }
        }
        public void gridload()
        {
           // try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT CONVERT(datetime,Fill_Date,103) as Fill_Date,Supplier_Name,Cylinder_Count,Fill_ID from Tb_Fill_Master WHERE Cancel_status = 'NA'", con);
                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    dgv_Filling_Details.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_Filling_Details.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();
            }
            //catch { }
            //finally { con.Close(); }
        }
        public void gridload_Content()
        {
            //try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT Item_No,Sr_No,Name,Rate,Status,Fill_ID from Tb_Fill_Content_Master WHERE Fill_ID ='" + lbl_Fdc.Text + "'", con); //AND Status LIKE ('SENT%')", con);
                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    dgv_Filling_Content.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_Filling_Content.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();
            }
            //catch { }
            // finally { con.Close(); }
            int count = 0;
            foreach (DataGridViewRow row in dgv_Filling_Content.Rows)
            {
                if (row.Cells["receive_status"].Value.ToString().Contains("RECEIVE"))
                {
                    row.Cells["received"].Value = true;
                    row.ReadOnly = true;
                    count++;
                }
            }
            if (count == dgv_Filling_Content.RowCount)
            {
                panel49.Enabled = false;
            }
            
            load_Gridview_Status();
        }
        private void load_Gridview_Status()
        {
            foreach(DataGridViewRow row in dgv_Filling_Content.Rows)
            {
                row.Cells["status"].Value = true;
              //  row.Cells["status"].Value = "FILL";
            }
        }
        private void Filling_Ledger_Load(object sender, EventArgs e)
        {
            panel40.Hide();
            panel38.Hide();
            panel49.Hide();
            panel28.Show();
            panel2.Show();
            panel19.Hide();
            panel49.Hide();
            panel26.Hide();
            panel12.Hide();
            panel8.Hide();
            if(rdbtn_ByFdc_ID.Checked==true)
            {
                panel31.Show();
            }
            else if(rdbtn_By_Date.Checked==true)
            {
                panel26.Show();
            }
            else if(rdb_Between_Date.Checked == true)
            {
                panel8.Show();
                panel26.Show();
            }
            else if (rdb_Item_No.Checked == true)
            {
                panel40.Show();
            }
            else
            {
                panel8.Hide();
                panel26.Hide();
                panel31.Hide();
            }
            btn_Save.Text = "Export To Excel";
            panel28.Dock = DockStyle.Fill;
            dgv_Filling_Details.Dock = DockStyle.Fill;
        }
        private void dgv_Filling_Details_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string value = dgv_Filling_Details.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            if (value == "VIEW")
            {
                panel38.Show();
                panel49.Show();
                panel2.Hide();
                panel38.Dock = DockStyle.Fill;
                dgv_Filling_Content.Dock = DockStyle.Fill;
                dgv_Filling_Content.Show();
                btn_Save.Text = "Save";
                //show_chkBox();
                string id = dgv_Filling_Details.Rows[e.RowIndex].Cells["fdc_id"].Value.ToString();
                lbl_Fdc.Text= dgv_Filling_Details.Rows[e.RowIndex].Cells["fdc_id"].Value.ToString();
                // item_search();
                 gridload_Content();
                //dgv_Filling_Content.Enabled = false;
                panel33.Hide();
                txt_Supp_DC_No.Focus();
            }
        }
        public void colour_Change()
        {
           // foreach (DataGridViewRow row in dgv_Filling_Content.Rows)
            //    for (int i = 0; i < dgv_Filling_Content.Rows.Count; i++)
            //{
            //    if (dgv_Filling_Content.Rows[i].Cells["receive_status"].Value.ToString() == "RECEIVE FROM SUPPLIER")
            //    {
            //        dgv_Filling_Details.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
            //    }
            //    else
            //    {
            //        dgv_Filling_Details.Rows[i].DefaultCellStyle.BackColor = Color.LightCoral;
            //    }
            //}

        }
        private void dgv_Filling_Details_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dgv_Filling_Details.Rows[e.RowIndex].Cells[1].Value = (e.RowIndex + 1).ToString();

                //if (dgv_Filling_Content.Rows[e.RowIndex].Cells["receive_status"].Value.ToString() == "RECEIVE FROM SUPPLIER")
                //{
                //     dgv_Filling_Details.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGreen;
                //    //dgv_Filling_Details.Rows[e.RowIndex].Cells["total_items"].Style.BackColor = Color.LightGreen;
                //}
                //else if (dgv_Filling_Content.Rows[e.RowIndex].Cells["receive_status"].Value.ToString() == "SENT TO SUPPLIER")
                //{
                //    dgv_Filling_Details.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightCoral;
                //    //dgv_Filling_Details.Rows[e.RowIndex].Cells["total_items"].Style.BackColor = Color.LightCoral;
                //}
        }

        private void dgv_Filling_View_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dgv_Filling_Content.Rows[e.RowIndex].Cells["sr"].Value = (e.RowIndex + 1).ToString();
        }
        private void dtp_By_Date_ValueChanged(object sender, EventArgs e)
        {

        }
        private void cmb_Cylinder_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_supplier();
        }
        private void rdb_ALL_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void rdb_bySupplier_CheckedChanged(object sender, EventArgs e)
        {
            
        }
        private void dt_To_Date_ValueChanged(object sender, EventArgs e)
        {
            if (rdbtn_By_Date.Checked==true)
            {
                show_by_Date();
            }
            else
            {
                show_Between_Date();
            }
        }
        private void dtp_By_Date_ValueChanged_1(object sender, EventArgs e)
        {
            if (rdb_Between_Date.Checked==true)
            {
                show_Between_Date();
            }
            else
            {
                show_by_Date();
            }
        }
        public void show_by_Date()
        {
            String date = dtp_By_Date.Text.ToString();
            //    try
            {
                getconnection();
                con.Open();
                if (Search_By.Text == "BY SUPPLIER")
                {
                    cmd = new SqlCommand("SELECT Fill_Date as Fill_Date,Supplier_Name,Cylinder_Count,Fill_ID from Tb_Fill_Master WHERE Fill_Date = '" + date + "' AND Supplier_Name = '" + cmb_supplier_name.Text + "'", con);
                }
                else
                {
                    cmd = new SqlCommand("SELECT Fill_Date as Fill_Date,Supplier_Name,Cylinder_Count,Fill_ID from Tb_Fill_Master WHERE Fill_Date = '" + date + "'", con);
                }
                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    dgv_Filling_Details.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_Filling_Details.DataSource = ds.Tables[0];
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
            //try
            {
                getconnection();
                con.Open();
                if (Search_By.Text == "BY SUPPLIER")
                {
                    cmd = new SqlCommand("SELECT CONVERT(datetime,Fill_Date,103) as Fill_Date,Supplier_Name,Cylinder_Count,Fill_ID from Tb_Fill_Master WHERE Supplier_Name = '" + cmb_supplier_name.Text + "' AND Fill_Date BETWEEN '" + start_date + "' AND '" + end_date + "'", con);
                }
                else
                {
                    cmd = new SqlCommand("SELECT CONVERT(datetime,Fill_Date,103) as Fill_Date,Supplier_Name,Cylinder_Count,Fill_ID from Tb_Fill_Master WHERE Fill_Date BETWEEN '" + start_date + "' AND '" + end_date + "'", con);
                }
                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    dgv_Filling_Details.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_Filling_Details.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();
            }
            //catch { }
            //finally { con.Close(); }
        }
        private void panel28_Paint(object sender, PaintEventArgs e)
        {

        }
        public void grid_item()
        {
            getconnection();
            con.Open();
            cmd = new SqlCommand("SELECT Item_No,Sr_No,Name,Rate,Status,Fill_ID from Tb_Fill_Content_Master WHERE Item_No = '" + cmb_Search_Item.Text + "' and Fill_ID='"+ lbl_Fdc.Text+ "'", con); //AND Status LIKE ('SENT%')", con);
            adpt = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                dgv_Filling_Content.DataSource = ds.Tables[0];
            }
            else
            {
                dgv_Filling_Content.DataSource = ds.Tables[0];
            }
            adpt.Dispose();
            dt.Dispose();
    }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Search_By.Text=="ALL")
            {
                gridload();
                panel19.Show();
                panel12.Hide();
                load_FDC_ID();
                cmb_FDC_ID.ResetText();
                cmb_supplier_name.ResetText();
            }
            else if(Search_By.Text == "BY SUPPLIER")
            {
                panel12.Show();
                load_supplier();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            btn_Save.Text = "Export To Excel";
            panel38.Hide();
            panel49.Hide();
            panel2.Show();
            panel28.Show();
            panel49.Enabled = true;
            panel28.Dock = DockStyle.Fill;
            dgv_Filling_Details.Dock = DockStyle.Fill;
            dgv_Filling_Details.Show();
        }
        private void cmb_supplier_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            panel19.Show();
            gridload_By_Supplier_Name();
            load_FDC_Id_By_Supplier();
        }
        private void load_FDC_Id_By_Supplier()
        {
            cmb_FDC_ID.Items.Clear();
            try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT DISTINCT Fill_ID FROM Tb_Fill_Master WHERE Supplier_Name = '" + cmb_supplier_name.Text.ToString() +"'", con);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    cmb_FDC_ID.Items.Add(sdr[0]);
                }
                sdr.Close();
            }
            catch { }
            finally { con.Close(); }
        }


        public void load_Item_No()
        {
            getconnection();
            con.Open();
            cmd = new SqlCommand("SELECT DISTINCT Item_No FROM Tb_Fill_Content_Master", con);
            sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                cmb_ITEM_NO.Items.Add(sdr[0]);
            }
            sdr.Close();
        }
    
        private void cmb_FDC_ID_SelectedIndexChanged(object sender, EventArgs e)
        {
            gridload_By_Cdc_No();
        }
        private void gridload_By_Cdc_No()
        {
            try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT CONVERT(datetime,Fill_Date,103) as Fill_Date,Supplier_Name,Cylinder_Count,Fill_ID from Tb_Fill_Master WHERE Fill_ID = '" + cmb_FDC_ID.Text.ToString() + "'", con);
                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    dgv_Filling_Details.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_Filling_Details.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();
            }
            catch { }
            finally { con.Close(); }
        }

        private void gridload_By_Item_No()
        {
            try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT CONVERT(datetime,A.Fill_Date,103) as Fill_Date,A.Supplier_Name,A.Cylinder_Count,A.Fill_ID from Tb_Fill_Master A join Tb_Fill_Content_Master B on A.Fill_ID = B.Fill_ID WHERE B.Item_No = '" + cmb_ITEM_NO.Text.ToString() + "'", con);
                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    dgv_Filling_Details.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_Filling_Details.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();
            }
            catch { }
            finally { con.Close(); }
        }
        private void gridload_By_Supplier_Name()
        {
            try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT CONVERT(datetime,Fill_Date,103) as Fill_Date,Supplier_Name,Cylinder_Count,Fill_ID from Tb_Fill_Master WHERE Supplier_Name = '" + cmb_supplier_name.Text.ToString() + "'", con);
                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    dgv_Filling_Details.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_Filling_Details.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();
            }
            catch { }
            finally { con.Close(); }
        }
        private void btn_Save_Click(object sender, EventArgs e)
        {
            string value;
            if(btn_Save.Text == "Save")
            {
                save_Counter = 0;
                if (txt_Supp_DC_No.Text == "")
                {
                    DialogResult = MessageBox.Show("Please Enter DC No", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (DialogResult == DialogResult.OK)
                    {
                        //dgv_Filling_Content.Enabled = true;
                        txt_Supp_DC_No.Enabled = true;
                        txt_Supp_DC_No.Focus();
                    }
                }
                else
                {
                    getconnection();
                    con.Open();
                    foreach (DataGridViewRow row in dgv_Filling_Content.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells["received"].Value))
                        {
                            if(row.ReadOnly == false)
                            {
                                cmd = new SqlCommand("UPDATE Tb_Fill_Content_Master SET Status = 'RECEIVE FROM SUPPLIER', Supp_Dc_No = '" + txt_Supp_DC_No.Text.ToString() + "', Received_Date = '" + dtp_Received.Text.ToString() + "' WHERE Item_No = '" + row.Cells["item_no"].Value.ToString() + "' AND Fill_ID = '" + row.Cells["fdc_no"].Value.ToString() + "'", con);
                                cmd.ExecuteNonQuery();
                                cmd = new SqlCommand("UPDATE Tb_Inventory_Master SET In_Stock = In_Stock + 1 ,Out_Stock = Out_Stock - 1 WHERE Particulars = (SELECT Particulers FROM Tb_Purchase_Content_Master WHERE Part_No = '" + row.Cells["item_no"].Value.ToString() + "')", con);
                                cmd.ExecuteNonQuery();
                                if (Convert.ToBoolean(row.Cells["STATUS"].Value))
                                {
                                    value = "FILL";
                                }
                                else
                                {
                                    value = "EMPTY";
                                }
                                cmd = new SqlCommand("UPDATE Tb_Purchase_Content_Master SET Cylinder_Status = '" + value + "', Cust_Supp_Name = 'NA' WHERE Part_No = '" + row.Cells["item_no"].Value.ToString() + "'", con);
                                cmd.ExecuteNonQuery();
                                save_Counter++;
                            }
                        }
                    }
                    MessageBox.Show(save_Counter + " Entries Saved SuccessFully.!", "Success");
                    btn_Back.PerformClick();
                    colour_Change();
                    clear_data();
                }
            }
            else if (btn_Save.Text== "Export To Excel")
            {
                if (dgv_Filling_Details.Rows.Count > 0)
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
                        for (j = 1; j <= 5; j++)
                        {
                            Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[StartRow, StartCol + j-1];
                            myRange.Value2 = dgv_Filling_Details.Columns[j].HeaderText;
                        }
                        StartRow++;
                       //Write datagridview content
                        for (i = 1; i < dgv_Filling_Details.Rows.Count; i++)
                        {
                            for (j = 1; j <= 5; j++)
                            {
                                try
                                {
                                    Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[StartRow + i, StartCol + j-1];
                                    myRange.Value2 = dgv_Filling_Details[j, i].Value == null ? "" : dgv_Filling_Details[j, i].Value;
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
        }
        private void clear_data()
        {
            if (dgv_Filling_Content.RowCount > 0)
            {
                DataTable DT = (DataTable)dgv_Filling_Content.DataSource;
                if (DT != null)
                    DT.Clear();
            }
            txt_Supp_DC_No.Clear();
            cmb_Search_Item.ResetText();
        }
        private void dgv_Filling_Content_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }
        private void dgv_Filling_Content_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dgv_Filling_Content.Enabled == false)
            {
                MessageBox.Show("Enter CDC Number First.!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void txt_Supp_DC_No_TextChanged(object sender, EventArgs e)
        {
            if(txt_Supp_DC_No.Text != "")
            {
                dgv_Filling_Content.Enabled = true;
            }
        }
        private void cmb_Search_Item_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {

        }
        private void rdbtn_ByFdc_ID_CheckedChanged(object sender, EventArgs e)
        {
            panel31.Show();
            panel26.Hide();
            panel8.Hide();
        }
        private void rdbtn_By_Date_CheckedChanged(object sender, EventArgs e)
        {
            panel26.Show();
            panel8.Hide();
            panel31.Hide();
        }
        private void rdb_Between_Date_CheckedChanged(object sender, EventArgs e)
        {
            panel26.Show();
            panel8.Show();
            panel31.Hide();
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //if(chk_Select_All.Checked == true)
            {
                for (int i = 0; i < dgv_Filling_Content.RowCount; i++)
                {
                    dgv_Filling_Content.Rows[i].Cells["received"].Value = chk_Select_All.Checked;
                }
            }
            //else if (chk_Select_All.Checked == false)
            //{
            //    for (int i = 0; i < dgv_Filling_Content.RowCount; i++)
            //    {
            //        dgv_Filling_Content.Rows[i].Cells["received"].Value = headerBox.Checked;
            //    }
            //}
        }
        private void cmb_Search_Item_TextChanged(object sender, EventArgs e)
        {
           
        }
        private void panel53_Paint(object sender, PaintEventArgs e)
        {

        }
        private void cmb_Search_Item_TextChanged_1(object sender, EventArgs e)
        {
           
        }
        public void item_search()
        {
            cmb_Search_Item.Items.Clear();
                try
                {
                    getconnection();
                    con.Open();
                    cmd = new SqlCommand("SELECT distinct Item_No from Tb_Fill_Content_Master WHERE Fill_ID ='" + lbl_Fdc.Text + "'", con);
                    sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        cmb_Search_Item.Items.Add(sdr[0]);
                    }
                    sdr.Close();
                }
                catch { }
                finally { con.Close(); }
            //CurrencyManager cm = (CurrencyManager)BindingContext[dgv_Filling_Content.DataSource];
            //cm.SuspendBinding();
            //string dgv_item, cmb_item;
            //cmb_item = cmb_Search_Item.Text.ToString();
            //for (int i = 0; i < dgv_Filling_Content.Rows.Count; i++)
            //{
            //    dgv_item = dgv_Filling_Content.Rows[i].Cells["item_no"].Value.ToString();
            //    if (dgv_item.StartsWith(cmb_item))
            //    {
            //        dgv_Filling_Content.Rows[i].Visible = true;
            //    }
            //    else
            //    {
            //        dgv_Filling_Content.Rows[i].Visible = false;
            //    }
            //}
            //cm.ResumeBinding();
        }
        private void cmb_Search_Item_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            grid_item();
        }
        private void rdb_All_CheckedChanged_1(object sender, EventArgs e)
        {
            if(rdb_All.Checked==true)
            {
                gridload_Content();
                panel33.Hide();
            }
        }
        private void rdb_By_Item_No_CheckedChanged(object sender, EventArgs e)
        {
            if(rdb_By_Item_No.Checked==true)
            {
                item_search();
                panel33.Show();
            }
            //grid_item();
        }
        private void cmb_Search_Item_SelectedIndexChanged_2(object sender, EventArgs e)
        {
            grid_item();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if(rdb_Item_No.Checked == true)
            {
                panel40.Show();
                load_Item_No();
            }
            else
            {
                panel40.Hide();
            }
        }

        private void cmb_ITEM_NO_SelectedIndexChanged(object sender, EventArgs e)
        {
            gridload_By_Item_No();
        }
    }
}
