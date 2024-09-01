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
    public partial class Asset_Master : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adpt;
        DataTable dt;
        SqlDataReader sdr;

        public Asset_Master()
        {
            InitializeComponent();
            //loadComboBox();
            //gridload();
            //load_Asset_Name();
        }

        public void getconnection()
        {
            string str = ConfigurationManager.ConnectionStrings["CylinderData"].ConnectionString;
            con = new SqlConnection(str);
        }

        public void clearControl()
        {
            txt_Name_Of_Asset.Clear();
            txt_Purchase_Cost.Clear();
            dt_Purchase_Date.ResetText();
            cmb_Asset_Type.ResetText();
        }
        public void load_Data()
        {
            {
                try
                {
                    getconnection();
                    con.Open();

                    string Asset_Name = cmb_Serach_Asset.Text.ToString();
                    cmd = new SqlCommand("SELECT Purchase_Date, Purchase_Cost, Asset_Type, Asset_Name FROM Tb_Asset_Details  WHERE Asset_Name = '" + Asset_Name + "'", con);
                    sdr = cmd.ExecuteReader();

                    while (sdr.Read())
                    {
                        dt_Purchase_Date.Text = sdr[0].ToString();
                        txt_Purchase_Cost.Text = sdr[1].ToString();
                        cmb_Asset_Type.Text = sdr[2].ToString();
                    }
                    sdr.Close();

                    adpt = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adpt.Fill(ds);
                    dt = ds.Tables[0];

                    if (dt.Rows.Count == 0)
                    {
                        dgv_Asset_Master.DataSource = ds.Tables[0];
                    }
                    else
                    {
                        dgv_Asset_Master.DataSource = ds.Tables[0];
                    }
                    adpt.Dispose();
                    dt.Dispose();
                }
                catch { }
                finally { con.Close(); }
            }
        }
       

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txt_Name_Of_Asset.Text == "" && btn_Save.Text=="Save")
            {
                MessageBox.Show("Enter Name of Asset", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txt_Purchase_Cost.Text == "")
            {
                MessageBox.Show("Enter Cost", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (cmb_Asset_Type.Text == "")
            {
                MessageBox.Show("Select Asset Type", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {
                    if (btn_Save.Text == "Save")
                    {
                        getconnection();
                        con.Open();

                        cmd = new SqlCommand("Insert_Asset_Details", con);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Purchase_Date", dt_Purchase_Date.Text.Trim());
                        cmd.Parameters.AddWithValue("@Asset_Name", txt_Name_Of_Asset.Text.Trim());
                        cmd.Parameters.AddWithValue("@Purchase_Cost", txt_Purchase_Cost.Text.Trim());
                        cmd.Parameters.AddWithValue("@Asset_Type", cmb_Asset_Type.Text.Trim());

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Data Save Successfully","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        clearControl();
                        gridload();
                    }
                   else if(btn_Save.Text == "Update")
                    {
                        getconnection();
                        con.Open();

                        cmd = new SqlCommand("Update_Asset_Master", con);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Purchase_Date", dt_Purchase_Date.Text.Trim());
                        cmd.Parameters.AddWithValue("@Asset_Name", txt_Name_Of_Asset.Text.Trim());
                        cmd.Parameters.AddWithValue("@Purchase_Cost", txt_Purchase_Cost.Text.Trim());
                        cmd.Parameters.AddWithValue("@Asset_Type", cmb_Asset_Type.Text.Trim());
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Data Updated Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clearControl();
                        gridload();
                        btn_Save.Text = "Save";
                        txt_Name_Of_Asset.Enabled = true;
                        cmb_Serach_Asset.ResetText();
                    }

                }
                catch
                {
                    MessageBox.Show("Error");
                }
                finally
                {
                    con.Close();
                }
            }
        }


        public void loadComboBox()
        {
            cmb_Asset_Type.Items.Add("New");
            cmb_Asset_Type.Items.Add("Used");
        }

        public void gridload()
        {
            try
            {
                getconnection();
                con.Open();

                cmd = new SqlCommand("SELECT Purchase_Date,Asset_Name,Purchase_Cost,Asset_Type FROM Tb_Asset_Details", con);

                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];

                if (dt.Rows.Count == 0)
                {
                    dgv_Asset_Master.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_Asset_Master.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();
            }
            catch { }
            finally { con.Close(); }
        }

        private void dgv_Asset_Master_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dgv_Asset_Master.Rows[e.RowIndex].Cells[2].Value = (e.RowIndex + 1).ToString();
        }

        private void dgv_Asset_Master_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string value = dgv_Asset_Master.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

            if (value == "Edit")
            {
                if(btn_Save.Text=="Update")
                {
                    MessageBox.Show("please update selected record  ", "STOP",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    DataGridViewRow row = dgv_Asset_Master.Rows[e.RowIndex];
                    txt_Name_Of_Asset.Text = row.Cells["name"].Value.ToString();
                    txt_Purchase_Cost.Text = row.Cells["cost"].Value.ToString();
                    dt_Purchase_Date.Text = row.Cells["purchase"].Value.ToString();
                    cmb_Asset_Type.Text = row.Cells["type"].Value.ToString();
                    dgv_Asset_Master.Rows.RemoveAt(e.RowIndex);
                    btn_Save.Text = "Update";
                    txt_Name_Of_Asset.Enabled = false;
                }
               
            }
            else if (value == "Remove")
            {
                DialogResult result = MessageBox.Show("Are You Sure?", "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    getconnection();
                    con.Open();

                    cmd = new SqlCommand("DELETE FROM Tb_Asset_Details WHERE Asset_Name = '" + dgv_Asset_Master.Rows[e.RowIndex].Cells["name"].Value.ToString() + "'", con);
                    cmd.ExecuteNonQuery();
                    dgv_Asset_Master.Rows.RemoveAt(e.RowIndex);
                }
            }

        }

        private void Asset_Master_Load(object sender, EventArgs e)
        {
            cmb_Serach_Asset.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        public void load_Asset_Name()
        {
                try
                {
                    getconnection();
                    con.Open();

                cmd = new SqlCommand("SELECT DISTINCT Asset_Name FROM Tb_Asset_Details", con);
                    sdr = cmd.ExecuteReader();

                    while (sdr.Read())
                    {
                        cmb_Serach_Asset.Items.Add(sdr[0]);
                    }
                    sdr.Close();
                }
                catch { }
                finally { con.Close(); }
            }

        private void cmb_Serach_Asset_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_Name_Of_Asset.Text = cmb_Serach_Asset.Text;
            load_Data();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (btn_Search.Text == "Search")
            {
                txt_Name_Of_Asset.Hide();
                cmb_Serach_Asset.Show();
                btn_Search.Text = "Cancel";
                cmb_Serach_Asset.Focus();
                btn_Save.Text = "Update";
            }
            else
            {
                cmb_Serach_Asset.Hide();
                txt_Name_Of_Asset.Show();
                btn_Search.Text = "Search";
                btn_Save.Text = "Save";
            }
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dgv_Asset_Master.Rows.Count > 0)
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
                    for (j = 2; j <= 6; j++)
                    {
                        Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[StartRow, StartCol + j-2];
                        myRange.Value2 = dgv_Asset_Master.Columns[j].HeaderText;
                    }

                    StartRow++;

                    //Write datagridview content
                    for (i = 0; i < dgv_Asset_Master.Rows.Count; i++)
                    {
                        for (j = 2; j <= 6; j++)
                        {
                            try
                            {
                                Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[StartRow + i, StartCol + j-2];
                                myRange.Value2 = dgv_Asset_Master[j, i].Value == null ? "" : dgv_Asset_Master[j, i].Value;
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

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txt_Name_Of_Asset_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void dt_Purchase_Date_ValueChanged(object sender, EventArgs e)
        {

        }

        private void cmb_Asset_Type_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
   
   
}
