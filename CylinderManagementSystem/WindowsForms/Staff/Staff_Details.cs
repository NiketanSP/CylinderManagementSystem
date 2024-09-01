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
    public partial class Staff_Details : Form
    {
        SqlCommand cmd;
        SqlDataReader sdr;
        SqlConnection con;
        DataTable dt;
        SqlDataAdapter adpt;
        string Staff_Cell_ID;
        public void getconnection()
        {
            string str = ConfigurationManager.ConnectionStrings["CylinderData"].ConnectionString;
            con = new SqlConnection(str);
        }
        public Staff_Details()
        {
            InitializeComponent();
            gridload();
        }
        public void gridload()
        {
            try
            {
                getconnection();
                con.Open();

                cmd = new SqlCommand("SELECT Staff_ID,St_Name,St_Phone,St_Mobile,St_Aadhar_No,St_EmContact,St_JoinDate,St_Designation,Date_of_Birth from Tb_Staff_Details", con);

                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];

                if (dt.Rows.Count == 0)
                {
                    dgv_Staff_Master.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_Staff_Master.DataSource = ds.Tables[0];
                }
            }
            catch { }
            finally { con.Close(); }

        }
        public void Load_Name()
        {
            cmb_Search.Items.Clear();
            cmb_Search.ResetText();
            try
            {
                getconnection();
                con.Open();

                cmd = new SqlCommand("SELECT DISTINCT St_Name FROM Tb_Staff_Details", con);
                sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    cmb_Search.Items.Add(sdr[0]);
                }
                sdr.Close();
            }
            catch { }
            finally { con.Close(); }
        }
        public void Load_ID()
        {
            cmb_Search.Items.Clear();
            cmb_Search.ResetText();
            try
            {
                getconnection();
                con.Open();

                cmd = new SqlCommand("SELECT DISTINCT Staff_ID FROM Tb_Staff_Details", con);
                sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    cmb_Search.Items.Add(sdr[0]);
                }
                sdr.Close();
            }
            catch { }
            finally { con.Close(); }
        }
        public void Load_Aadhar()
        {
            cmb_Search.Items.Clear();
            cmb_Search.ResetText();
            try
            {
                getconnection();
                con.Open();

                cmd = new SqlCommand("SELECT DISTINCT St_Aadhar_No FROM Tb_Staff_Details", con);
                sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    cmb_Search.Items.Add(sdr[0]);
                }
                sdr.Close();
            }
            catch { }
            finally { con.Close(); }
        }

        public void gridload_by_Status(string status)
        {
            cmb_Search.Items.Clear();
            cmb_Search.ResetText();
            //try
            {
                getconnection();
                con.Open();
                string Name = cmb_Search.Text.ToString();
                cmd = new SqlCommand("SELECT Staff_ID,St_Name,St_Phone,St_Mobile,St_Aadhar_No,St_EmContact,St_JoinDate,St_Designation,Date_of_Birth FROM Tb_Staff_Details WHERE Staff_Status='" + status.ToUpper() + "'", con);

                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];

                if (dt.Rows.Count == 0)
                {
                    dgv_Staff_Master.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_Staff_Master.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();
            }
            //catch { }
            //finally { con.Close(); }
        }
        private void Staff_Details_Load(object sender, EventArgs e)
        {
            // txt_Name.Enabled = true;
            panel6.Hide();
        }

        private void dgv_Staff_Master_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }


        private void dgv_Staff_Master_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dgv_Staff_Master.Rows[e.RowIndex].Cells["Sr_No"].Value = (e.RowIndex + 1).ToString();
        }

        private void Searc_By_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbl_Search_Select.Text = cmb_Serach_By.Text;
            if (lbl_Search_Select.Text.ToString() == "By Name")
            {
                Load_Name();
                panel6.Show();
            }
            else if (lbl_Search_Select.Text.ToString() == "By Staff ID")
            {
                Load_ID();
                panel6.Show();
            }
            else if (lbl_Search_Select.Text.ToString() == "By Adhar No")
            {
                Load_Aadhar();
                panel6.Show();
            }
            else if (lbl_Search_Select.Text.ToString() == "Active")
            {
                gridload_by_Status("Active");
                panel6.Hide();
            }
            else if (lbl_Search_Select.Text.ToString() == "Deactive")
            {
                gridload_by_Status("Deactive");
                panel6.Hide();
            }
        }

        private void cmb_Search_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_Serach_By.Text == "By Name")
            {
                getconnection();
                con.Open();
                string Name = cmb_Search.Text.ToString();
                cmd = new SqlCommand("SELECT Staff_ID,St_Name,St_Phone,St_Mobile,St_Aadhar_No,St_EmContact,St_JoinDate,St_Designation,Date_of_Birth FROM Tb_Staff_Details WHERE St_Name='" + Name + "'", con);

                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];

                if (dt.Rows.Count == 0)
                {
                    dgv_Staff_Master.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_Staff_Master.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();
            }
            else if (cmb_Serach_By.Text == "By Staff ID")
            {
                getconnection();
                con.Open();
                string ID = cmb_Search.Text.ToString();
                cmd = new SqlCommand("SELECT Staff_ID,St_Name,St_Phone,St_Mobile,St_Aadhar_No,St_EmContact,St_JoinDate,St_Designation,Date_of_Birth FROM Tb_Staff_Details WHERE Staff_ID='" + ID + "'", con);

                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];

                if (dt.Rows.Count == 0)
                {
                    dgv_Staff_Master.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_Staff_Master.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();
            }
            else if (cmb_Serach_By.Text == "By Adhar No")
            {
                getconnection();
                con.Open();
                string Aadhar_No = cmb_Search.Text.ToString();
                cmd = new SqlCommand("SELECT Staff_ID,St_Name,St_Phone,St_Mobile,St_Aadhar_No,St_EmContact,St_JoinDate,St_Designation,Date_of_Birth FROM Tb_Staff_Details WHERE St_Aadhar_No='" + Aadhar_No + "'", con);

                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];

                if (dt.Rows.Count == 0)
                {
                    dgv_Staff_Master.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_Staff_Master.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();
            }
            else if (cmb_Serach_By.Text == "Active")
            {
                getconnection();
                con.Open();
                string Status = cmb_Search.Text.ToString();
                cmd = new SqlCommand("SELECT Staff_ID,St_Name,St_Phone,St_Mobile,St_Aadhar_No,St_EmContact,St_JoinDate,St_Designation,Date_of_Birth FROM Tb_Staff_Details WHERE Status='" + Status + "'", con);

                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];

                if (dt.Rows.Count == 0)
                {
                    dgv_Staff_Master.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_Staff_Master.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();
            }
            else if (cmb_Serach_By.Text == "DeActive")
            {
                getconnection();
                con.Open();
                string Status = cmb_Search.Text.ToString();
                cmd = new SqlCommand("SELECT Staff_ID,St_Name,St_Phone,St_Mobile,St_Aadhar_No,St_EmContact,St_JoinDate,St_Designation,Date_of_Birth FROM Tb_Staff_Details WHERE Status='" + Status + "'", con);

                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];

                if (dt.Rows.Count == 0)
                {
                    dgv_Staff_Master.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_Staff_Master.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();
            }
        }


        private void dgv_Staff_Master_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            string value = dgv_Staff_Master.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

            if (value == "Edit")
            {
                Staff_Master SM = new Staff_Master();

                if (SM.btn_Save.Text == "Update")
                {
                    MessageBox.Show("You Can Update Only One Record At a Time", "STOP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    //DataGridViewRow row = dgv_Staff_Master.Rows[e.RowIndex];
                    //SM.txt_Staff_Search.Text = row.Cells["sid"].Value.ToString();

                    //SM.txt_Staff_ID.Text = row.Cells["sid"].Value.ToString();
                    //dgv_Staff_Master.Rows.RemoveAt(e.RowIndex);
                    //SM.update_data_grid(sender, e);

                    //SM.btn_Save.Text = "Update";
                    string id = dgv_Staff_Master.Rows[e.RowIndex].Cells["sid"].Value.ToString();
                    SM.search_from_Details(id);
                    SM.ShowDialog();
                }

            }
            else if (value == "X")
            {
                Staff_Master SM = new Staff_Master();
                DialogResult result = MessageBox.Show("Are You Sure.?", "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    dgv_Staff_Master.Rows.RemoveAt(e.RowIndex);
                    SM.update_data_grid(sender, e);
                    SM.ShowDialog();
                }
            }


        }


        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dgv_Staff_Master.Rows.Count > 0)
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
                        myRange.Value2 = dgv_Staff_Master.Columns[j].HeaderText;
                    }

                    StartRow++;

                    //Write datagridview content
                    for (i = 0; i < dgv_Staff_Master.Rows.Count; i++)
                    {
                        for (j = 1; j <= 10; j++)
                        {
                            try
                            {
                                Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[StartRow + i, StartCol + j - 1];
                                myRange.Value2 = dgv_Staff_Master[j, i].Value == null ? "" : dgv_Staff_Master[j, i].Value;
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
}

