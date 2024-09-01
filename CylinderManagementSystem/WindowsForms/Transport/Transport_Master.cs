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
    public partial class Transport_Master : Form
    {
        SqlCommand cmd;
        SqlDataReader sdr;
        SqlConnection con;
        DataTable dt;
        SqlDataAdapter adpt;
        public void getconnection()
        {
            string str = ConfigurationManager.ConnectionStrings["CylinderData"].ConnectionString;
            con = new SqlConnection(str);
        }
        public void clearcontrols()
        {
            txt_Name.Clear();
            txt_Address.Clear();
            txt_Company_Phone.Clear();
            txt_Gst_No.Clear();
            txt_Contact_Person_Name.Clear();
            txt_Contact_Person_Phone.Clear();
            txt_Account_Number.Clear();
            txt_Ifsc_Code.Clear();
        }

        public void Load_Transport()
        {

            try
            {
                getconnection();
                con.Open();

                cmd = new SqlCommand("SELECT DISTINCT T_Name FROM Tb_Transport_Master", con);
                sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    cmb_Serach_transport.Items.Add(sdr[0]);
                }
                sdr.Close();
            }
            catch { }
            finally { con.Close(); }
        }

        public void Load_Transport_Data()
        {
            try
            {
                getconnection();
                con.Open();

                string Transport_Name = cmb_Serach_transport.Text.ToString();
                cmd = new SqlCommand("SELECT T_InDate,T_Address,T_PhoneNo,T_GSTNo,T_CPerson,CP_Phone,T_AccountDetails,T_IFSC_Code,T_Name FROM Tb_Transport_Master  WHERE T_Name = '" + Transport_Name + "'", con);
                sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    dt_In_Date.Text = sdr[0].ToString();
                    txt_Address.Text = sdr[1].ToString();
                    txt_Company_Phone.Text = sdr[2].ToString();
                    txt_Gst_No.Text = sdr[3].ToString();
                    txt_Contact_Person_Name.Text = sdr[4].ToString();
                    txt_Contact_Person_Phone.Text = sdr[5].ToString();
                    txt_Account_Number.Text = sdr[6].ToString();
                    txt_Ifsc_Code.Text = sdr[7].ToString();
                }
                sdr.Close();
                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];

                if (dt.Rows.Count == 0)
                {
                    dgv_Transport_Master.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_Transport_Master.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();
            }
            catch { }
            finally { con.Close(); }
        }

        public void gridload()
        {
            try
            {
                getconnection();
                con.Open();

                cmd = new SqlCommand("SELECT T_InDate,T_Name,T_Address,T_PhoneNo,T_GSTNo,T_CPerson,CP_Phone,T_AccountDetails,T_IFSC_Code FROM Tb_Transport_Master", con);

                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];

                if (dt.Rows.Count == 0)
                {
                    dgv_Transport_Master.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_Transport_Master.DataSource = ds.Tables[0];
                }
            }
            catch { }
            finally { con.Close(); }

        }
        public Transport_Master()
        {
            InitializeComponent();
            gridload();
            Load_Transport();
        }

       
        private void Transport_Master_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'cylinder_ManagementDataSet5.Tb_Transport_Master' table. You can move, or remove it, as needed.
            //this.tb_Transport_MasterTableAdapter.Fill(this.cylinder_ManagementDataSet5.Tb_Transport_Master);
            cmb_Serach_transport.Hide();
        }

        private void dgv_Transport_Master_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dgv_Transport_Master.Rows[e.RowIndex].Cells["Sr_No"].Value = (e.RowIndex + 1).ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txt_Name.Text == "")
            {
                MessageBox.Show("Please Enter The Name", "STOP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txt_Address.Text == "")
            {
                MessageBox.Show("Please Enter The Address", "STOP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (txt_Company_Phone.Text == "")
                {
                    txt_Company_Phone.Text = "NA";
                }
                if (txt_Gst_No.Text == "")
                {
                    txt_Gst_No.Text = "NA";
                }
                if (txt_Contact_Person_Name.Text == "")
                {
                    txt_Contact_Person_Name.Text = "NA";
                }
                if (txt_Contact_Person_Phone.Text == "")
                {
                    txt_Contact_Person_Phone.Text = "NA";
                }
                if (txt_Account_Number.Text == "")
                {
                    txt_Account_Number.Text = "NA";
                }
                if (txt_Ifsc_Code.Text == "")
                {
                    txt_Ifsc_Code.Text = "NA";
                }
                try
                {
                    if (btn_Save.Text == "Save")
                    {
                        getconnection();
                        con.Open();

                        cmd = new SqlCommand("Insert_Transport_Master", con);
                        cmd.CommandType = CommandType.StoredProcedure;

                        dt_In_Date.Value.ToString("dd-mm-yyyy");
                        cmd.Parameters.AddWithValue("@T_InDate", dt_In_Date.Text.Trim());
                        cmd.Parameters.AddWithValue("@T_Name", txt_Name.Text.Trim());
                        cmd.Parameters.AddWithValue("@T_Address", txt_Address.Text.Trim());
                        cmd.Parameters.AddWithValue("@T_PhoneNo", txt_Company_Phone.Text.Trim());
                        cmd.Parameters.AddWithValue("@T_GSTNo", txt_Gst_No.Text.Trim());
                        cmd.Parameters.AddWithValue("@T_CPerson", txt_Contact_Person_Name.Text.Trim());
                        cmd.Parameters.AddWithValue("@CP_Phone", txt_Contact_Person_Phone.Text.Trim());
                        cmd.Parameters.AddWithValue("@T_AccountDetails", txt_Account_Number.Text.Trim());
                        cmd.Parameters.AddWithValue("@T_IFSC_Code", txt_Ifsc_Code.Text.Trim());

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Data Saved Successfully", "Information status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clearcontrols();
                        gridload();

                    }
                    else if (btn_Save.Text == "Update")
                    {
                        getconnection();
                        con.Open();

                        cmd = new SqlCommand("Update_Transport_Master", con);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@T_InDate ", dt_In_Date.Text.Trim());
                        cmd.Parameters.AddWithValue("@T_Name", cmb_Serach_transport.Text.Trim());
                        cmd.Parameters.AddWithValue("@T_Address ", txt_Address.Text.Trim());
                        cmd.Parameters.AddWithValue("@T_PhoneNo  ", txt_Company_Phone.Text.Trim());
                        cmd.Parameters.AddWithValue("@T_GSTNo", txt_Gst_No.Text.Trim());
                        cmd.Parameters.AddWithValue("@T_CPerson", txt_Contact_Person_Name.Text.Trim());
                        cmd.Parameters.AddWithValue("@CP_Phone", txt_Contact_Person_Phone.Text.Trim());
                        cmd.Parameters.AddWithValue("@T_AccountDetails ", txt_Account_Number.Text.Trim());
                        cmd.Parameters.AddWithValue("@T_IFSC_Code ", txt_Ifsc_Code.Text.Trim());
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Data Updated Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clearcontrols();
                        gridload();
                        btn_Save.Text = "Save";
                        cmb_Serach_transport.ResetText();
                    }
                }

                catch { }
            }
        }

        private void lbl_Search_Transport_Click(object sender, EventArgs e)
        {

            if (lbl_Search_Transport.Text == "Search")
            {
                txt_Name.Hide();
                cmb_Serach_transport.Show();
                lbl_Search_Transport.Text = "Cancel";
                btn_Save.Text = "Update";
            }
            else
            {
                cmb_Serach_transport.Hide();
                txt_Name.Show();
                lbl_Search_Transport.Text = "Search";
                btn_Save.Text = "Save";
            }
            clearcontrols();
        }

        public void update_data_grid(object sender, DataGridViewCellEventArgs e)
        {
            int limit = dgv_Transport_Master.RowCount;
            int index = e.RowIndex, temp;
            for (int i = index; i < limit; i++)
            {
                temp = Convert.ToInt32(dgv_Transport_Master.Rows[i].Cells[2].Value);
                dgv_Transport_Master.Rows[i].Cells[0].Value = (temp - 1).ToString();
            }
        }
        private void cmb_Serach_transport_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_Name.Text = cmb_Serach_transport.Text;
            Load_Transport_Data();
        }

        private void dgv_Transport_Master_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string value = dgv_Transport_Master.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

            if (value == "Edit")
            {
                if (btn_Save.Text == "Update")
                {
                    MessageBox.Show("You Can Update Only One Record At a Time", "STOP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    DataGridViewRow row = dgv_Transport_Master.Rows[e.RowIndex];
                    dt_In_Date.Text = row.Cells["InDate"].Value.ToString();
                    txt_Name.Text = row.Cells["Name"].Value.ToString();
                    txt_Address.Text = row.Cells["address"].Value.ToString();
                    txt_Company_Phone.Text = row.Cells["Phone"].Value.ToString();
                    txt_Gst_No.Text = row.Cells["GST"].Value.ToString();
                    txt_Contact_Person_Name.Text = row.Cells["ConPerson"].Value.ToString();
                    txt_Contact_Person_Phone.Text = row.Cells["Mobile"].Value.ToString();
                    txt_Account_Number.Text = row.Cells["AccNo"].Value.ToString();
                    txt_Ifsc_Code.Text = row.Cells["IFSC"].Value.ToString();
                    dgv_Transport_Master.Rows.RemoveAt(e.RowIndex);
                    update_data_grid(sender, e);

                    btn_Save.Text = "Update";
                    cmb_Serach_transport.Text = txt_Name.Text;
                }
                
            }
            else if (value == "Remove")
            {
                DialogResult result = MessageBox.Show("Are You Sure.?", "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    getconnection();
                    con.Open();

                    string Transport_Name = dgv_Transport_Master.Rows[e.RowIndex].Cells["Name"].Value.ToString();
                    cmd = new SqlCommand("DELETE FROM Tb_Transport_Master  WHERE T_Name = '" + Transport_Name + "'", con);
                    cmd.ExecuteNonQuery();
                    dgv_Transport_Master.Rows.RemoveAt(e.RowIndex);
                    update_data_grid(sender, e);
                    con.Close();
                }
            }
        }

        private void txt_Address_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
