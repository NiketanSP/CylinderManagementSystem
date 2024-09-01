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
    public partial class Cylinder_Master : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adpt;
        DataTable dt;

        public Cylinder_Master()
        {
            InitializeComponent();
            loadcmbox();
            gridload();
        }

        public void getconnection()
        {
            string str = ConfigurationManager.ConnectionStrings["CylinderData"].ConnectionString;
            con = new SqlConnection(str);

        }

        public void clearControl()
        {
            txt_Cylinder_Rate.Clear();
            txt_Cylinder_Serial_No.ResetText();
            txt_Cylinder_Name.Clear();
            txt_Cylinder_Part_No.ResetText();
            cmb_Cylinder_Type.ResetText();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txt_Cylinder_Serial_No.Text == "")
            {
                MessageBox.Show("Enter Serial Number", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txt_Cylinder_Part_No.Text == "")
            {
                MessageBox.Show("Enter Part Number", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txt_Cylinder_Name.Text == "")
            {
                MessageBox.Show("Enter Name Of Cylinder", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txt_Cylinder_Rate.Text == "")
            {
                MessageBox.Show("Enter Rate of Cylinder", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (cmb_Cylinder_Type.Text == "")
            {
                MessageBox.Show("Select Type of Cylinder", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {
                    if (btn_Save.Text == "Save")
                {
                    getconnection();
                    con.Open();

                    cmd = new SqlCommand("Insert_Cylinder_Master", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@C_ID", txt_Cylinder_Serial_No.Text.Trim());
                    cmd.Parameters.AddWithValue("@C_P_No", txt_Cylinder_Part_No.Text.Trim());
                    cmd.Parameters.AddWithValue("@C_Name", txt_Cylinder_Name.Text.Trim());
                    cmd.Parameters.AddWithValue("@C_Rate", txt_Cylinder_Rate.Text.Trim());
                    cmd.Parameters.AddWithValue("@C_Type", cmb_Cylinder_Type.Text.Trim());
                  //  cmd.Parameters.AddWithValue("@Com_Phone", txt_Contact.Text.Trim());

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Information Saved.!");
                    clearControl();
                    gridload();
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

        public void loadcmbox()
        {
            cmb_Cylinder_Type.Items.Add("Type1");
            cmb_Cylinder_Type.Items.Add("Type2");
        }

        public void gridload()
        {
            try
            {
                getconnection();
                con.Open();

                cmd = new SqlCommand("SELECT * from Tb_Cylinder_Master", con);

                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];

                if (dt.Rows.Count == 0)
                {
                    dgv_Cylinder_Master.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_Cylinder_Master.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();
            }
            catch { }
            finally { con.Close(); }
        }

        private void dgvCylinder_Master_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dgv_Cylinder_Master.Rows[e.RowIndex].Cells[2].Value = (e.RowIndex + 1).ToString();
        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txt_Cylinder_Serial_No_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
