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
    public partial class SignUp : Form
    {
        SqlConnection con;
        SqlDataReader sdr;
        SqlCommand cmd;
        SqlDataAdapter adpt;
        public SignUp()
        {
            InitializeComponent();
            load_Staff_ID();
        }
        public void clearControl()
        {
            cmb_Emp_ID.ResetText();
            txt_Designation.Clear();
            txt_Contact_No.Clear();
            cmb_User_Type.ResetText();
            txt_User_Id.Clear();
            txt_Password.Clear();
            txt_Confirm_Pass.Clear();
        }
        public void load_Staff_ID()
        {
            {
                try
                {
                    getconnection();
                    con.Open();
                    cmd = new SqlCommand("SELECT DISTINCT Staff_ID FROM Tb_Staff_Details", con);
                    sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        cmb_Emp_ID.Items.Add(sdr[0]);
                    }
                    sdr.Close();
                }
                catch { }
                finally { con.Close(); }
            }
        }
        public void Staff_Data()
        {
            //try
            {
                getconnection();
                con.Open();
                string Staff_ID = cmb_Emp_ID.Text.ToString();
                cmd = new SqlCommand("SELECT St_Designation,St_Mobile FROM Tb_Staff_Details  WHERE Staff_ID = '" + Staff_ID + "'", con);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    txt_Designation.Text = sdr[0].ToString();
                    txt_Contact_No.Text = sdr[1].ToString();
                }
                sdr.Close();
            }
        }
        public void getconnection()
        {
            string str = ConfigurationManager.ConnectionStrings["CylinderData"].ConnectionString;
            con = new SqlConnection(str);
        }
        private void SignUp_Load(object sender, EventArgs e)
        {

        }
        private void lbl_Pur_Date_Click(object sender, EventArgs e)
        {

        }
        private void dt_In_Date_ValueChanged(object sender, EventArgs e)
        {

        }
        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (cmb_Emp_ID.Text == "" && btn_Save.Text == "Save")
            {
                MessageBox.Show("Please Select ID", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txt_Designation.Text == "")
            {
                MessageBox.Show("Enter Designation", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txt_Contact_No.Text == "")
            {
                MessageBox.Show("Enter Contact", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (cmb_User_Type.Text == "")
            {
                MessageBox.Show("Select Type", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txt_User_Id.Text == "")
            {
                MessageBox.Show("Enter user id", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txt_Password.Text == "")
            {
                MessageBox.Show("Enter Password", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txt_Confirm_Pass.Text == "")
            {
                MessageBox.Show("Enter Confirm pass", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txt_Confirm_Pass.Text != txt_Password.Text)
            {
                MessageBox.Show("Password and Confirmation Password do not match", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    if (btn_Save.Text == "SIGN UP")
                    {
                        getconnection();
                        con.Open();
                        cmd = new SqlCommand("Insert_SignUp_Details", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@SignUp_Date", dt_S_Up_Date.Text.Trim());
                        cmd.Parameters.AddWithValue("@Name", cmb_Emp_ID.Text.Trim());
                        cmd.Parameters.AddWithValue("@Designation", txt_Designation.Text.Trim());
                        cmd.Parameters.AddWithValue("@Contact_No", txt_Contact_No.Text.Trim());
                        cmd.Parameters.AddWithValue("@User_Type", cmb_User_Type.Text.Trim());
                        cmd.Parameters.AddWithValue("@User_ID", txt_User_Id.Text.Trim());
                        cmd.Parameters.AddWithValue("@Password", txt_Password.Text.Trim());
                        cmd.Parameters.AddWithValue("@Confirm_Password", txt_Confirm_Pass.Text.Trim());
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("User Successfully Created", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clearControl();
                        this.Close();
                       
                      
                    }
                }
                catch { }
            }
        }

        private void txt_Authorize_Code_Leave(object sender, EventArgs e)
        {
            if (txt_Authorize_Code.Text == "NTECH111" && cmb_User_Type.Text == "ADMIN")
            {
                panel1.Hide();
              
            }
            else if (txt_Authorize_Code.Text == "NTECH555" && cmb_User_Type.Text == "USER")
            {
                panel1.Hide();
            }
            else
            {
                panel1.Show();
                MessageBox.Show("Invalid Authorization Code", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void cmb_Emp_ID_SelectedIndexChanged(object sender, EventArgs e)
        {
            Staff_Data();
        }
        private void txt_Authorize_Code_TextChanged(object sender, EventArgs e)
        {

        }
        private void txt_User_Id_Leave(object sender, EventArgs e)
        {
            getconnection();
            con.Open();

            cmd = new SqlCommand("SELECT User_ID FROM Tb_SignUp_Details WHERE User_ID = '" + txt_User_Id.Text + "'", con);
            sdr = cmd.ExecuteReader();

            if(sdr.Read())
            {
                MessageBox.Show("User Already Exist");
                txt_User_Id.Focus();
            }
        }
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Home form = new Home();
            form.panel1.Show();
            this.Close();
        }
        private void SignUp_FormClosed(object sender, FormClosedEventArgs e)
        {
        }
        private void SignUp_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
    }
}
