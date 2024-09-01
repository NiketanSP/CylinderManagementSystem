using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace CylinderManagementSystem
{
    public partial class Company_Details : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader sdr;
        SqlDataAdapter adpt;
        DataTable dt;

        public void getconnection()
        {
            string str = ConfigurationManager.ConnectionStrings["CylinderData"].ConnectionString;
            con = new SqlConnection(str);
        }

        public Company_Details()
        {
            InitializeComponent();
            //maxId();
          
            //load_Company_Details();
        }

        private void load_Company_Details()
        {
            try
            {
                getconnection();
                con.Open();

                cmd = new SqlCommand("Select * from Tb_Cylinder_Agency_Master", con);
                sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    btn_Save.Text = "Update";
                    txt_Company_ID.Text = sdr[0].ToString();
                    txt_Company_Name.Text = sdr[1].ToString();
                    txt_Company_Name.Enabled = false;
                    txt_Depo_Address.Text = sdr[2].ToString();
                    txt_Office_Address.Text = sdr[3].ToString();
                    txt_Phone.Text = sdr[4].ToString();
                    txt_Mobile.Text = sdr[5].ToString();
                    txt_Gst_No.Text = sdr[6].ToString();
                    txt_Pan_No.Text = sdr[7].ToString();
                    txt_Acc_No.Text = sdr[8].ToString();
                    txt_Ifsc_Code.Text = sdr[9].ToString();
                    txt_Email.Text = sdr[12].ToString();
                    txt_Website.Text = sdr[13].ToString();
                    txt_Bank_Name.Text = sdr[11].ToString();

                    byte[] logo = (byte[])sdr[10];
                    MemoryStream ms = new MemoryStream(logo);
                    ms.Seek(0, SeekOrigin.Begin);
                    picbox_Logo.Image = Image.FromStream(ms);

                    btn_Select_Logo.Text = "Update Logo";
                }
                sdr.Close();
            }
            catch { }
        }

        public void clearControl()
        {
            txt_Acc_No.Clear();
            txt_Office_Address.Clear();
            txt_Gst_No.Clear();
            txt_Company_ID.Clear();
            txt_Ifsc_Code.Clear();
            txt_Mobile.Clear();
            txt_Company_Name.Clear();
            txt_Pan_No.Clear();
            txt_Phone.Clear();
        }

        public void maxId()
        {
            getconnection();
            con.Open();
            cmd = new SqlCommand("SELECT MAX(Com_ID) FROM Tb_Cylinder_Agency_Master", con);

            sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                string val = sdr[0].ToString();
                if (val == "")
                {
                    txt_Company_ID.Text = "1";
                }
                else
                {
                    int p = Convert.ToInt32(val);
                    txt_Company_ID.Text = (++p).ToString();
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txt_Company_Name.Text == "")
            {
                MessageBox.Show("Please Enter Name Of Company", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txt_Office_Address.Text == "")
            {
                MessageBox.Show("Please Enter Valid Information", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txt_Mobile.Text == "")
            {
                MessageBox.Show("Please Enter Mobile Number", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txt_Gst_No.Text == "")
            {
                MessageBox.Show("Please Enter GST Number", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txt_Pan_No.Text == "")
            {
                MessageBox.Show("Please Enter PAN Number", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txt_Bank_Name.Text == "")
            {
                MessageBox.Show("Please Enter Bank Name", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txt_Acc_No.Text == "")
            {
                MessageBox.Show("Please Enter Account Number", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (txt_Ifsc_Code.Text == "")
            {
                MessageBox.Show("Please Enter IFSC Code", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else 
            {
                if (txt_Depo_Address.Text == "")
                {
                    txt_Depo_Address.Text = "NA";
                }
                if (txt_Email.Text == "")
                {
                    txt_Email.Text = "NA";
                }
                if (txt_Phone.Text == "")
                {
                    txt_Phone.Text = "NA";
                }
                if (txt_Website.Text == "")
                {
                    txt_Website.Text = "NA";
                }
                // try
                {
                    Image img = picbox_Logo.Image;
                    byte[] arr;
                    ImageConverter converter = new ImageConverter();
                    arr = (byte[])converter.ConvertTo(img, typeof(byte[]));
                    
                    if (btn_Save.Text == "Save")
                    {
                        getconnection();
                        con.Open();

                        cmd = new SqlCommand("Insert_Cylinder_Agency_Master", con);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Com_ID", txt_Company_ID.Text.Trim());
                        cmd.Parameters.AddWithValue("@Com_Name", txt_Company_Name.Text.Trim());
                        cmd.Parameters.AddWithValue("@Com_Depo_Address", txt_Depo_Address.Text.Trim());
                        cmd.Parameters.AddWithValue("@Com_Office_Address", txt_Office_Address.Text.Trim());
                        cmd.Parameters.AddWithValue("@Com_Phone", txt_Phone.Text.Trim());
                        cmd.Parameters.AddWithValue("@Com_Mobile", txt_Mobile.Text.Trim());
                        cmd.Parameters.AddWithValue("@Com_GST_No", txt_Gst_No.Text.Trim());
                        cmd.Parameters.AddWithValue("@Com_Pan_No", txt_Pan_No.Text.Trim());
                        cmd.Parameters.AddWithValue("@Com_AccNo", txt_Acc_No.Text.Trim());
                        cmd.Parameters.AddWithValue("@Com_IFSC", txt_Ifsc_Code.Text.Trim());
                        cmd.Parameters.AddWithValue("@Com_Logo", arr);
                        cmd.Parameters.AddWithValue("@Com_Bank", txt_Bank_Name.Text.Trim());
                        cmd.Parameters.AddWithValue("@Com_Email", txt_Email.Text.Trim());
                        cmd.Parameters.AddWithValue("@Com_Website", txt_Website.Text.Trim());


                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Data Saved Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clearControl();
                        maxId();
                        load_Company_Details();
                    }
                    else if (btn_Save.Text == "Update")
                    {
                        getconnection();
                        con.Open();

                        cmd = new SqlCommand("Update_Agency_Master", con);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Com_ID", txt_Company_ID.Text.Trim());
                        cmd.Parameters.AddWithValue("@Com_Name", txt_Company_Name.Text.Trim());
                        cmd.Parameters.AddWithValue("@Com_Depo_Address", txt_Depo_Address.Text.Trim());
                        cmd.Parameters.AddWithValue("@Com_Office_Address", txt_Office_Address.Text.Trim());
                        cmd.Parameters.AddWithValue("@Com_Phone", txt_Phone.Text.Trim());
                        cmd.Parameters.AddWithValue("@Com_Mobile", txt_Mobile.Text.Trim());
                        cmd.Parameters.AddWithValue("@Com_GST_No", txt_Gst_No.Text.Trim());
                        cmd.Parameters.AddWithValue("@Com_Pan_No", txt_Pan_No.Text.Trim());
                        cmd.Parameters.AddWithValue("@Com_AccNo", txt_Acc_No.Text.Trim());
                        cmd.Parameters.AddWithValue("@Com_IFSC", txt_Ifsc_Code.Text.Trim());
                        cmd.Parameters.AddWithValue("@logo", arr);
                        cmd.Parameters.AddWithValue("@Com_Bank", txt_Bank_Name.Text.Trim());
                        cmd.Parameters.AddWithValue("@Com_Email", txt_Email.Text.Trim());
                        cmd.Parameters.AddWithValue("@Com_Website", txt_Website.Text.Trim());


                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Data Updated Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clearControl();
                        maxId();
                        load_Company_Details();
                    }
                }
               // catch
                {
                    
                }
               // finally
                {
                    con.Close();
                }
            }
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lblMobile_Click(object sender, EventArgs e)
        {

        }

        private void btn_Select_Logo_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog fd = new OpenFileDialog();
                fd.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp;*.png)|*.jpg; *.jpeg; *.gif; *.bmp;*.png";

                if (fd.ShowDialog() == DialogResult.OK)
                {
                    picbox_Logo.Image = new Bitmap(fd.FileName);
                    lbl_Logo_Path.Text = fd.FileName;
                }
                btn_Select_Logo.Text = "Update Logo";
            }
            catch { MessageBox.Show("Select Proper Image"); }
        }

        private void chk_Address_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void txt_Office_Address_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txt_Mobile_TextChanged(object sender, EventArgs e)
        {
            //int val;
            //if (!(int.TryParse(txt_Mobile.Text, out val)) && txt_Mobile.Text != "")
            //{
            //    txt_Mobile.Text = txt_Mobile.Text.Substring(0, txt_Mobile.Text.Length - 1);
            //    txt_Mobile.SelectionStart = txt_Mobile.Text.Length;
            //}
        }

        private void txt_Phone_TextChanged(object sender, EventArgs e)
        {
            //int val;
            //if (!(int.TryParse(txt_Phone.Text, out val)) && txt_Phone.Text != "")
            //{
            //    txt_Phone.Text = txt_Phone.Text.Substring(0, txt_Phone.Text.Length - 1);
            //    txt_Phone.SelectionStart = txt_Phone.Text.Length;
            //}
        }

        private void Company_Details_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            txt_Company_ID.Text = "1";
            load_Company_Details();
        }
    }
}
