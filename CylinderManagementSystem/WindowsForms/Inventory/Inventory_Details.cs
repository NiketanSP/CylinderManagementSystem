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
    public partial class Inventory_Details : Form
    {
        SqlConnection con;
        SqlDataReader sdr;
        SqlCommand cmd;
        DataTable dt;
        SqlDataAdapter adpt;
        public void getconnection()
        {
            string str = ConfigurationManager.ConnectionStrings["CylinderData"].ConnectionString;
            con = new SqlConnection(str);
        }
        public Inventory_Details()
        {
            InitializeComponent();
            grid_load_Cylinder();
        }
        private void cmbCyType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_Search_By.Text == "Cylinder Stock")
            {
                panel_Material_By.Hide();
                panel_Dynamic_Change.Hide();
                panel_Cylinder_By.Show();
                dgv_Cylinder_Invetory.Show();
                dgv_Material_Invetory.Hide();
                dgv_View_Cyl_Id.Hide();
                dgv_Cylinder_Invetory.Dock = DockStyle.Fill;
                grid_load_Cylinder();
            }
            else
            {
                panel_Cylinder_By.Hide();
                dgv_View_Cyl_Id.Hide();
                panel_Dynamic_Change.Hide();
                panel_Material_By.Show();
                dgv_Material_Invetory.Show();
                dgv_Material_Invetory.Dock = DockStyle.Fill;
                grid_load_Material();
            }
        }
        private void Inventory_Details_Load(object sender, EventArgs e)
        {
            panel6.Hide();
            panel25.Hide();
            panel7.Hide();
            panel32.Hide();
            panel_Dynamic_Change.Hide();
            panel_Material_By.Hide();
            panel_Cylinder_By.Hide();
            dgv_Material_Invetory.Hide();
            dgv_Cylinder_Invetory.Show();
            dgv_Cylinder_Invetory.Dock = DockStyle.Fill;
            dgv_View_Cyl_Id.Hide();
            grid_load_Cylinder();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (btn_Search.Text == "Search")
            {
                panel6.Show();
                panel25.Show();
                panel7.Show();
                panel32.Show();
                panel34.Hide();
            }
            else if (btn_Search.Text == "Back")
            {
                btn_Search.Text = "Search";
                dgv_View_Cyl_Id.Hide();
            }
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            panel6.Hide();
            panel25.Hide();
            panel7.Hide();
            panel32.Hide();
            panel34.Show();
            dgv_Material_Invetory.Hide();
            dgv_View_Cyl_Id.Hide();
            dgv_Cylinder_Invetory.Show();
        }
        public void hide_Panel_Initial()
        {
            panel_Cylinder_By.Hide();
            panel_Material_By.Hide();
            panel_Dynamic_Change.Hide();
        }
        private void cmb_Cylinder_By_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmb_Cylinder_By.Text == "Type")
            {
                panel_Dynamic_Change.Show();
                btn_Search.Text = "Search";
                dgv_View_Cyl_Id.Hide();
                lbl_cmb_Select.Text = cmb_Cylinder_By.Text;
                load_Dynamic_Combo();
            }
            else if (cmb_Cylinder_By.Text == "Item Number")
            {
                Track_Cylinder form = new Track_Cylinder();
                form.ShowDialog();
                //cmd = new SqlCommand("SELECT Particulers, Purchase_Type, Sr_No, Part_No, Cylinder_Status, Cust_Supp_Name FROM Tb_Purchase_Content_Master WHERE Purchase_Type = 'CYLINDER' AND Cylinder_Status != 'CANCEL'", con);
                //adpt = new SqlDataAdapter(cmd);
                //DataSet ds = new DataSet();
                //adpt.Fill(ds);
                //dt = ds.Tables[0];

                //if (dt.Rows.Count == 0)
                //{
                //    dgv_View_Cyl_Id.DataSource = ds.Tables[0];
                //}
                //else
                //{
                //    dgv_View_Cyl_Id.DataSource = ds.Tables[0];
                //}
                //dgv_View_Cyl_Id.Show();

                //panel_Dynamic_Change.Show();
                //lbl_cmb_Select.Text = cmb_Cylinder_By.Text;
                //load_Dynamic_Combo();
                //dgv_View_Cyl_Id.Dock = DockStyle.Fill;
                //dgv_View_Cyl_Id.Show();
                //btn_Search.Text = "Back";

                //foreach (DataGridViewRow row in dgv_View_Cyl_Id.Rows)
                //{
                //    if (row.Cells["status"].Value.ToString() == "EMPTY")
                //    {
                //        row.Cells["status"].Style.BackColor = Color.LightCoral;
                //    }
                //    else if (row.Cells["status"].Value.ToString() == "FILL")
                //    {
                //        row.Cells["status"].Style.BackColor = Color.LightGreen;
                //    }
                //    else if (row.Cells["status"].Value.ToString().Contains("SUPPLIER"))
                //    {
                //        row.Cells["status"].Style.BackColor = Color.LightSkyBlue;
                //    }
                //    else if (row.Cells["status"].Value.ToString().Contains("CUSTOMER"))
                //    {
                //        row.Cells["status"].Style.BackColor = Color.SandyBrown;
                //    }
                //}
            }
            else if(cmb_Cylinder_By.Text == "In Stock")
            {
                dgv_View_Cyl_Id.Dock = DockStyle.Fill;
                dgv_View_Cyl_Id.Show();
                btn_Search.Text = "Back";
                load_In_Stock("All_Cylinder");
            }
            else if (cmb_Cylinder_By.Text == "Out Stock")
            {
                dgv_View_Cyl_Id.Dock = DockStyle.Fill;
                dgv_View_Cyl_Id.Show();
                btn_Search.Text = "Back";
                load_Out_Stock("All_Cylinder");
            }
        }
        private void load_Dynamic_Combo()
        {
            cmb_Dynamic.Items.Clear();
            cmb_Dynamic.ResetText();
            //try
            {
                getconnection();
                con.Open();
                if(cmb_Cylinder_By.Text == "Type")
                {
                    cmd = new SqlCommand("SELECT DISTINCT Cylinder_Type FROM Tb_Purchase_Content_Master WHERE Purchase_Type = 'CYLINDER'", con);
                }
                else if(cmb_Cylinder_By.Text=="Item Number")
                {
                    cmd = new SqlCommand("SELECT DISTINCT Part_No FROM Tb_Purchase_Content_Master WHERE Purchase_Type = 'CYLINDER' AND Cylinder_Status != 'CANCEL'", con);
                }
                else if (lbl_cmb_Select.Text == "Name")
                {
                    cmd = new SqlCommand("SELECT DISTINCT Particulers FROM Tb_Purchase_Content_Master WHERE Purchase_Type = 'MATERIAL'", con);
                }
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    cmb_Dynamic.Items.Add(sdr[0]);
                }
                sdr.Close();
            }
            //catch { }
        }
        private void cmb_Dynamic_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            {
                getconnection();
                con.Open();
                if (cmb_Cylinder_By.Text == "Type")
                {
                      cmd = new SqlCommand("SELECT Particulars,Type,In_Stock,Out_Stock FROM Tb_Inventory_Master WHERE Cylinder_Type = '" + cmb_Dynamic.Text.ToString() + "'", con);
                      adpt = new SqlDataAdapter(cmd);
                      DataSet ds = new DataSet();
                      adpt.Fill(ds);
                      dt = ds.Tables[0];
                      if (dt.Rows.Count == 0)
                      {
                          dgv_Cylinder_Invetory.DataSource = ds.Tables[0];
                      }
                      else
                      {
                          dgv_Cylinder_Invetory.DataSource = ds.Tables[0];
                      }
                }
                /*else if(cmb_Cylinder_By.Text == "Item Number")
                {
                    cmd = new SqlCommand("SELECT Particulers, Purchase_Type, Sr_No, Part_No, Cylinder_Status FROM Tb_Purchase_Content_Master WHERE Part_No = '" + cmb_Dynamic.Text.ToString() + "'", con);

                    adpt = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adpt.Fill(ds);
                    dt = ds.Tables[0];

                    if (dt.Rows.Count == 0)
                    {
                        dgv_View_Cyl_Id.DataSource = ds.Tables[0];
                    }
                    else
                    {
                        dgv_View_Cyl_Id.DataSource = ds.Tables[0];
                    }
                    dgv_View_Cyl_Id.Show();
                }*/
                else if (lbl_cmb_Select.Text == "Name")
                {
                    CurrencyManager cm = (CurrencyManager)BindingContext[dgv_Material_Invetory.DataSource];
                    cm.SuspendBinding();
                    string dgv_item, cmb_item;
                    cmb_item = cmb_Dynamic.Text.ToString();
                    for (int i = 0; i < dgv_Material_Invetory.Rows.Count; i++)
                    {
                        dgv_item = dgv_Material_Invetory.Rows[i].Cells["m_particular"].Value.ToString();
                        if (dgv_item == cmb_item)
                        {
                            dgv_Material_Invetory.Rows[i].Visible = true;
                        }
                        else
                        {
                            dgv_Material_Invetory.Rows[i].Visible = false;
                        }
                    }
                    cm.ResumeBinding();
                }
            }
            //catch { }
            //finally { con.Close(); }

               /* else if (cmb_Cylinder_By.Text == "Item Number")
                {
                    cmd = new SqlCommand("SELECT DISTINCT Part_No FROM Tb_Purchase_Content_Master WHERE Purchase_Type = 'CYLINDER'", con);
                }

                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    cmb_Dynamic.Items.Add(sdr[0]);
                }
                sdr.Close();*/
            
            //catch { }
        }
        private void cmb_Material_By_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmb_Material_By.Text == "Name")
            {
                panel_Dynamic_Change.Show();
                lbl_cmb_Select.Text = cmb_Material_By.Text;
                load_Dynamic_Combo();
            }
            else if(cmb_Material_By.Text == "In Stock")
            {
                load_Material_Stock("in");
            }
            else if(cmb_Material_By.Text == "Out Stock")
            {
                load_Material_Stock("out");
            }
        }
        public void load_Material_Stock(string str)
        {
            if(str == "in")
            {
                CurrencyManager cm = (CurrencyManager)BindingContext[dgv_Material_Invetory.DataSource];
                cm.SuspendBinding();
                int dgv_item;
                //cmb_item = cmb_Dynamic.Text.ToString();
                for (int i = 0; i < dgv_Material_Invetory.Rows.Count; i++)
                {
                    dgv_item = Convert.ToInt32(dgv_Material_Invetory.Rows[i].Cells["m_in_stock"].Value);
                    if (dgv_item > 0)
                    {
                        dgv_Material_Invetory.Rows[i].Visible = true;
                    }
                    else
                    {
                        dgv_Material_Invetory.Rows[i].Visible = false;
                    }
                }
                cm.ResumeBinding();
            }
            else if(str == "out")
            {
                CurrencyManager cm = (CurrencyManager)BindingContext[dgv_Material_Invetory.DataSource];
                cm.SuspendBinding();
                int dgv_item;
                //cmb_item = cmb_Dynamic.Text.ToString();
                for (int i = 0; i < dgv_Material_Invetory.Rows.Count; i++)
                {
                    dgv_item = Convert.ToInt32(dgv_Material_Invetory.Rows[i].Cells["m_out_stock"].Value);
                    if (dgv_item > 0)
                    {
                        dgv_Material_Invetory.Rows[i].Visible = true;
                    }
                    else
                    {
                        dgv_Material_Invetory.Rows[i].Visible = false;
                    }
                }
                cm.ResumeBinding();
            }
        }
        public void grid_load_Cylinder()
        {
            try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT Particulars,Type,In_Stock,Out_Stock FROM Tb_Inventory_Master WHERE Type='CYLINDER'", con);
                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    dgv_Cylinder_Invetory.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_Cylinder_Invetory.DataSource = ds.Tables[0];
                }
            }
            catch { }
            finally { con.Close(); }
            foreach (DataGridViewRow row in dgv_Cylinder_Invetory.Rows)
            {
                row.Cells["out_stock"].Style.BackColor = Color.LightCoral;
                row.Cells["in_stock"].Style.BackColor = Color.LightGreen;
            }
        }
        public void grid_load_Material()
        {
            try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT Particulars,In_Stock,Out_Stock FROM Tb_Inventory_Master WHERE Type='MATERIAL'", con);
                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    dgv_Material_Invetory.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_Material_Invetory.DataSource = ds.Tables[0];
                }
                load_Serial_No(dgv_Material_Invetory);
            }
            catch { }
            finally { con.Close(); }
        }

        private void dgv_Cylinder_Invetory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Convert.ToInt32(e.ColumnIndex) == dgv_Cylinder_Invetory.Columns["in_stock"].Index)
            {
                //IN STOCK
                string str = dgv_Cylinder_Invetory.Rows[e.RowIndex].Cells["particular"].Value.ToString();
                if (dgv_Cylinder_Invetory.Rows[e.RowIndex].Cells["in_stock"].Value.ToString() == "0")
                {
                    MessageBox.Show("Nothing to Show In Stock", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    load_In_Stock(str);
                    dgv_View_Cyl_Id.Dock = DockStyle.Fill;
                    dgv_View_Cyl_Id.Show();
                    btn_Search.Text = "Back";
                }
            }
            else if (e.ColumnIndex == dgv_Cylinder_Invetory.Columns["out_stock"].Index)
            {
                //OUT STOCK
                string str = dgv_Cylinder_Invetory.Rows[e.RowIndex].Cells["particular"].Value.ToString();
                if (dgv_Cylinder_Invetory.Rows[e.RowIndex].Cells["out_stock"].Value.ToString() == "0")
                {
                    MessageBox.Show("Nothing to Show In Stock", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    load_Out_Stock(str);
                    dgv_View_Cyl_Id.Dock = DockStyle.Fill;
                    dgv_View_Cyl_Id.Show();
                    btn_Search.Text = "Back";
                }
            }
        }
        public void load_Out_Stock(string str)
        {
            //try
            {
                getconnection();
                con.Open();
                if (str == "All_Cylinder")
                {
                    cmd = new SqlCommand("SELECT Particulers, Purchase_Type, Sr_No, Part_No, Cylinder_Status, Cust_Supp_Name FROM Tb_Purchase_Content_Master WHERE Purchase_Type = 'CYLINDER' AND (Cylinder_Status LIKE ('SENT%') OR Cylinder_Status LIKE ('SELL%'))", con);
                }
                else
                {
                    cmd = new SqlCommand("SELECT Particulers, Purchase_Type, Sr_No, Part_No, Cylinder_Status, Cust_Supp_Name FROM Tb_Purchase_Content_Master WHERE Particulers = '" + str + "' AND (Cylinder_Status LIKE ('SENT%') OR Cylinder_Status LIKE ('SELL%'))", con);
                }
                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    dgv_View_Cyl_Id.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_View_Cyl_Id.DataSource = ds.Tables[0];
                }
                dgv_View_Cyl_Id.Show();
            }
           // catch { }
           // finally { con.Close(); }

            foreach (DataGridViewRow row in dgv_View_Cyl_Id.Rows)
            {
                if (row.Cells["status"].Value.ToString().Contains("SUPPLIER"))
                {
                    row.Cells["status"].Style.BackColor = Color.LightSkyBlue;
                }
                else if (row.Cells["status"].Value.ToString().Contains("CUSTOMER"))
                {
                    row.Cells["status"].Style.BackColor = Color.SandyBrown;
                }
            }
        }
        public void load_In_Stock(string str)
        {
          //  try
            {
                getconnection();
                con.Open();
                if(str == "All_Cylinder")
                {
                    cmd = new SqlCommand("SELECT Particulers, Purchase_Type, Sr_No, Part_No, Cylinder_Status,Cust_Supp_Name FROM Tb_Purchase_Content_Master WHERE Cylinder_Status in ('EMPTY','FILL')", con);
                }
                else
                {
                    cmd = new SqlCommand("SELECT Particulers, Purchase_Type, Sr_No, Part_No, Cylinder_Status,Cust_Supp_Name FROM Tb_Purchase_Content_Master WHERE Particulers = '" + str + "' AND Cylinder_Status in ('EMPTY','FILL')", con);
                }
                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    dgv_View_Cyl_Id.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_View_Cyl_Id.DataSource = ds.Tables[0];
                }
                dgv_View_Cyl_Id.Show();
            }
            //    catch { }
            //   finally { con.Close(); }
            foreach (DataGridViewRow row in dgv_View_Cyl_Id.Rows)
            {
                if (row.Cells["status"].Value.ToString() == "EMPTY")
                {
                    row.Cells["status"].Style.BackColor = Color.LightCoral;
                }
                else if (row.Cells["status"].Value.ToString() == "FILL")
                {
                    row.Cells["status"].Style.BackColor = Color.LightGreen;
                }
            }
        }
        private void load_Serial_No(DataGridView grid)
        {
            foreach (DataGridViewRow row in grid.Rows)
            {
                grid.Rows[row.Index].Cells[0].Value = string.Format("{0}  ", row.Index + 1).ToString();
            }
        }
        private void dgv_Cylinder_Invetory_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dgv_Cylinder_Invetory.Rows[e.RowIndex].Cells["sr"].Value = (e.RowIndex + 1).ToString();
        }
        private void dgv_Material_Invetory_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dgv_Material_Invetory.Rows[e.RowIndex].Cells[0].Value = (e.RowIndex + 1).ToString();
        }
        private void dgv_View_Cyl_Id_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dgv_View_Cyl_Id.Rows[e.RowIndex].Cells[0].Value = (e.RowIndex + 1).ToString();
        }
        private void cmb_Dynamic_TextChanged(object sender, EventArgs e)
        {
            if(lbl_cmb_Select.Text == "Item Number")
            {
                CurrencyManager cm = (CurrencyManager)BindingContext[dgv_View_Cyl_Id.DataSource];
                cm.SuspendBinding();
                string dgv_item, cmb_item;
                cmb_item = cmb_Dynamic.Text.ToString();
                for (int i = 0; i < dgv_View_Cyl_Id.Rows.Count; i++)
                {
                    dgv_item = dgv_View_Cyl_Id.Rows[i].Cells["item"].Value.ToString();
                    if (dgv_item.StartsWith(cmb_item))
                    {
                        dgv_View_Cyl_Id.Rows[i].Visible = true;
                    }
                    else
                    {
                        dgv_View_Cyl_Id.Rows[i].Visible = false;
                    }
                }
                cm.ResumeBinding();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (dgv_Cylinder_Invetory.Rows.Count > 0)
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
                    for (j = 0; j <= 4; j++)
                    {
                        Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[StartRow, StartCol + j];
                        myRange.Value2 = dgv_Cylinder_Invetory.Columns[j].HeaderText;
                    }
                    StartRow++;
                    //Write datagridview content
                    for (i = 0; i < dgv_Cylinder_Invetory.Rows.Count; i++)
                    {
                        for (j = 0; j <= 4; j++)
                        {
                            try
                            {
                                Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[StartRow + i, StartCol + j];
                                myRange.Value2 = dgv_Cylinder_Invetory[j, i].Value == null ? "" : dgv_Cylinder_Invetory[j, i].Value;
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
        private void dgv_View_Cyl_Id_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void dgv_Material_Invetory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
    }
}


