using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace CylinderManagementSystem
{
    public partial class Customer_Master : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adpt;
        DataTable dt;
        SqlDataReader sdr;
        public Customer_Master()
        {
            InitializeComponent();
            gridload();
            Load_Customer();
        }
        public void getconnection()
        {
            string str = ConfigurationManager.ConnectionStrings["CylinderData"].ConnectionString;
            con = new SqlConnection(str);
        }
        public void clearControl()
        {
            dt_In_Date.ResetText();
            txt_Address.Clear();
            txt_Customer_Phone.Clear();
            txt_Contact_Person_Name.Clear();
            txt_Contact_Person_Phone.Clear();
            txt_GST_Number.Clear();
            txt_Name.Clear();
            cmb_Search_Customer.ResetText();
        }
        public void Load_Customer()
        {
                try
                {
                    getconnection();
                    con.Open();
                    cmd = new SqlCommand("SELECT DISTINCT Cust_Name FROM Tb_Customer_Master", con);
                    sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        cmb_Search_Customer.Items.Add(sdr[0]);
                    }
                    sdr.Close();
                }
                catch { }
                finally { con.Close(); }
        }
        public void Cust_Data(string name)
        {
            cmb_Search_Customer.Text = name;
            txt_Name.Text = name;
            //try
            {
                getconnection();
                con.Open();
                string Customer_Name = cmb_Search_Customer.Text.ToString();
                cmd = new SqlCommand("SELECT C_InDate, Cust_Address, Cust_PhoneNo, Cust_GSTNo, Cust_CPerson, CP_PhoneNo, Cust_Name FROM Tb_Customer_Master  WHERE Cust_Name = '" + name + "'", con);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    dt_In_Date.Text = sdr[0].ToString();
                    txt_Address.Text = sdr[1].ToString();
                    txt_Customer_Phone.Text = sdr[2].ToString();
                    txt_GST_Number.Text = sdr[3].ToString();
                    txt_Contact_Person_Name.Text = sdr[4].ToString();
                    txt_Contact_Person_Phone.Text = sdr[5].ToString();
                }
                sdr.Close();
              /*  cmd = new SqlCommand("SELECT Cust_Name, Cust_GSTNo, Cust_PhoneNo, Cust_Status FROM Tb_Customer_Master  WHERE Cust_Name = '" + Customer_Name + "'", con);

                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];

                if (dt.Rows.Count == 0)
                {
                    dgv_Customer_Master.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_Customer_Master.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();*/
            }
           // catch { }
            //finally { con.Close(); }
        }
        public void gridload()
        {
            try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT Cust_Name, Cust_Address, Cust_PhoneNo, Cust_GSTNo, Cust_Status, C_InDate, Cust_CPerson, CP_PhoneNo from Tb_Customer_Master", con);
                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    dgv_Customer_Master.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_Customer_Master.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();
            }
            catch { }
            finally { con.Close(); }
        }
        public void update_data_grid(object sender, DataGridViewCellEventArgs e)
        {
            int limit = dgv_Customer_Master.RowCount;
            int index = e.RowIndex, temp;
            for (int i = index; i < limit; i++)
            {
                temp = Convert.ToInt32(dgv_Customer_Master.Rows[i].Cells[2].Value);
                dgv_Customer_Master.Rows[i].Cells[0].Value = (temp - 1).ToString();
            }
        }
        private void dgv_Customer_Master_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dgv_Customer_Master.Rows[e.RowIndex].Cells[2].Value = (e.RowIndex + 1).ToString();
        }
        private void dgv_Customer_Master_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string value = dgv_Customer_Master.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

            if (value == "Edit")
            {
                if (btn_Save.Text == "Update")
                {
                    MessageBox.Show("You Can Update Only One Record At a Time", "STOP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    DataGridViewRow row = dgv_Customer_Master.Rows[e.RowIndex];
                    string name = row.Cells["name"].Value.ToString();
                    Cust_Data(name);
                    /*dt_In_Date.Text = row.Cells["indate"].Value.ToString();
                    txt_Name.Text = row.Cells["name"].Value.ToString();
                    txt_Address.Text = row.Cells["address"].Value.ToString();
                    txt_Customer_Phone.Text = row.Cells["phone"].Value.ToString();
                    txt_GST_Number.Text = row.Cells["gst"].Value.ToString();
                    txt_Contact_Person_Name.Text = row.Cells["cperson"].Value.ToString();
                    txt_Contact_Person_Phone.Text = row.Cells["mobile"].Value.ToString();*/
                    dgv_Customer_Master.Rows.RemoveAt(e.RowIndex);
                    btn_Save.Text = "Update";
                }
            }
            else if (value == "Remove")
            {
                DialogResult result = MessageBox.Show("Are You Sure?", "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    getconnection();
                    con.Open();
                    cmd = new SqlCommand("DELETE FROM Tb_Customer_Master WHERE Cust_Name = '" + dgv_Customer_Master.Rows[e.RowIndex].Cells["name"].Value.ToString() + "'", con);
                    cmd.ExecuteNonQuery();
                    dgv_Customer_Master.Rows.RemoveAt(e.RowIndex);
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (txt_Name.Text == "")
            {
                MessageBox.Show("Enter Name Of Customer", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txt_Address.Text == "")
            {
                MessageBox.Show("Enter Address Of Customer", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if(txt_Customer_Phone.Text == "")
            {
                MessageBox.Show("Enter Phone Number", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (txt_GST_Number.Text == "")
                {
                    txt_GST_Number.Text = "NA";
                }
                if (txt_Contact_Person_Name.Text == "")
                {
                    txt_Contact_Person_Name.Text = "NA";
                }
                if (txt_Contact_Person_Phone.Text.Length != 10)
                {
                    txt_Contact_Person_Phone.Text = "NA";
                }
                //try
                {
                    if (btn_Save.Text == "Save")
                    {
                        getconnection();
                        con.Open();
                        cmd = new SqlCommand("Insert_Customer_Master", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@C_InDate", dt_In_Date.Text.Trim());
                        cmd.Parameters.AddWithValue("@Cust_Name", txt_Name.Text.Trim());
                        cmd.Parameters.AddWithValue("@Cust_Address", txt_Address.Text.Trim());
                        cmd.Parameters.AddWithValue("@Cust_PhoneNo", txt_Customer_Phone.Text.Trim());
                        cmd.Parameters.AddWithValue("@Cust_GSTNo", txt_GST_Number.Text.Trim());
                        cmd.Parameters.AddWithValue("@Cust_CPerson", txt_Contact_Person_Name.Text.Trim());
                        cmd.Parameters.AddWithValue("@CP_PhoneNo", txt_Contact_Person_Phone.Text.Trim());
                        cmd.Parameters.AddWithValue("@Cust_Status", "Regular");
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Data Saved Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clearControl();
                        gridload();
                    }
                    else if (btn_Save.Text == "Update")
                    {
                        getconnection();
                        con.Open();
                        cmd = new SqlCommand("Update_Customer_Master", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@C_InDate", dt_In_Date.Text.Trim());
                        cmd.Parameters.AddWithValue("@Cust_Name", txt_Name.Text.Trim());
                        cmd.Parameters.AddWithValue("@Cust_Address ", txt_Address.Text.Trim());
                        cmd.Parameters.AddWithValue("@Cust_PhoneNo", txt_Customer_Phone.Text.Trim());
                        cmd.Parameters.AddWithValue("@Cust_GSTNo", txt_GST_Number.Text.Trim());
                        cmd.Parameters.AddWithValue("@Cust_CPerson", txt_Contact_Person_Name.Text.Trim());
                        cmd.Parameters.AddWithValue("@CP_PhoneNo", txt_Contact_Person_Phone.Text.Trim());
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Data Updated Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clearControl();
                        gridload();
                        cmb_Search_Customer.ResetText();
                        btn_Save.Text = "Save";
                    }
                }
                //catch
                {
                    //MessageBox.Show("Error");
                }
                //finally
                {
                    con.Close();
                }
            }
        }
        private void lbl_Search_Customer_Click(object sender, EventArgs e)
        {
            if (lbl_Search_Customer.Text == "Search")
            {
                txt_Name.Hide();
                cmb_Search_Customer.Show();
                lbl_Search_Customer.Text = "Cancel";
                btn_Save.Text = "Update";
            }
            else
            {
                cmb_Search_Customer.Hide();
                txt_Name.Show();
                lbl_Search_Customer.Text = "Search";
                btn_Save.Text = "Save";
                gridload();
                clearControl();
                cmb_Search_Customer.ResetText();
            }
       }
        private void Customer_Master_Load(object sender, EventArgs e)
        {
            cmb_Search_Customer.Hide();
        }
        private void cmb_Serach_Customer_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_Name.Text = cmb_Search_Customer.Text;
            Cust_Data(txt_Name.Text);
        }
        private void lblPhoneNumber_Click(object sender, EventArgs e)
        {

        }
        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
        private void panel20_Paint(object sender, PaintEventArgs e)
        {

        }
        private void panel18_Paint(object sender, PaintEventArgs e)
        {

        }
        private void panel11_Paint(object sender, PaintEventArgs e)
        {

        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void panel12_Paint(object sender, PaintEventArgs e)
        {

        }
        private void txt_Address_TextChanged(object sender, EventArgs e)
        {

        }
        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
