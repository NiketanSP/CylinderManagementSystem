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
    public partial class Track_Cylinder : Form
    {
        SqlConnection con;
        SqlDataReader sdr;
        SqlCommand cmd;
        DataTable dt;
        SqlDataAdapter adpt;

        public Track_Cylinder()
        {
            InitializeComponent();
        }

        public void getconnection()
        {
            string str = ConfigurationManager.ConnectionStrings["CylinderData"].ConnectionString;
            con = new SqlConnection(str);
        }

        private void Track_Cylinder_Load(object sender, EventArgs e)
        {
            load_Item_No();
            panel7.Hide();
            panel27.Hide();
            panel10.Hide();
            panel14.Hide();
            panel13.Hide();
            panel16.Show();
            panel27.Show();
            panel28.Show();
        }

        private void load_Item_No()
        {
            cmb_Item_No.Items.Clear();
            //try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT DISTINCT Part_No FROM Tb_Purchase_Content_Master WHERE Purchase_Type = 'CYLINDER' AND Cylinder_Status != 'CANCEL'", con);
                
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    cmb_Item_No.Items.Add(sdr[0]);
                }
                sdr.Close();
            }
            //catch { }
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            load_Cylinder_Details();
            load_Cylinder_Tracking();
            panel7.Show();
            panel7.Hide();
            panel9.Show();
            panel10.Show();
            panel13.Show();
            panel14.Show();
            panel28.Hide();
            //panel14.Show();
            //panel15.Show();
            panel16.Show();
        }

        //private void load_Filling_Count()
        //{
        //    //try
        //    {
        //        getconnection();
        //        con.Open();
        //        cmd = new SqlCommand("SELECT COUNT(Item_No)FROM Tb_Fill_Content_Master WHERE Item_No = '" + cmb_Item_No.Text + "'", con);

        //        sdr = cmd.ExecuteReader();
        //        while (sdr.Read())
        //        {
        //            lbl_fill_qty.Text = "FILL : " + sdr[0].ToString();
        //        }
        //        sdr.Close();

        //        cmd = new SqlCommand("SELECT COUNT(Item_No)FROM Tb_CDC_Content_Details WHERE Item_No = '" + cmb_Item_No.Text + "'", con);

        //        sdr = cmd.ExecuteReader();
        //        while (sdr.Read())
        //        {
        //            lbl_rent_qty.Text = "RENT : " + sdr[0].ToString();
        //        }
        //    }
        //}

        private void load_Cylinder_Details()
        {
            //try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT A.Sr_No, A.Particulers, A.Cylinder_Type, B.Supplier_Name, B.Date_Of_Purchase, B.Invoice_No, A.Cylinder_Status, A.Cust_Supp_Name FROM Tb_Purchase_Content_Master A JOIN Tb_Purchase B ON A.Invoice_No = B.Invoice_No WHERE A.Part_No = '" + cmb_Item_No.Text + "'", con);

                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    lbl_Serial.Text = sdr[0].ToString();
                    lbl_Particular.Text = sdr[1].ToString();
                    lbl_Gas.Text = sdr[2].ToString();
                    lbl_Supplier_Name.Text = sdr[3].ToString();
                    lbl_Date.Text = sdr[4].ToString();
                    lbl_Invoice_No.Text = sdr[5].ToString();
                    lbl_Supp_Name.Text = sdr[7].ToString();
                    lbl_status.Text = sdr[6].ToString();
                    if(sdr[7].ToString() != "NA")
                    {
                        lbl_Stock.Text = "OUT STOCK";
                        tableLayoutPanel1.BackColor = Color.LightCoral;
                        tableLayoutPanel1.ForeColor = Color.White;
                    }
                    else
                    {
                        lbl_Stock.Text = "IN STOCK";
                        tableLayoutPanel1.BackColor = Color.LightGreen;
                        tableLayoutPanel1.ForeColor = Color.Black;
                    }
                }
                sdr.Close();
            }
            //catch { }
        }

        private void load_Cylinder_Tracking()
        {
            //try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("get_Cylinder_Details", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@item_no", cmb_Item_No.Text.Trim());

                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];

                if (dt.Rows.Count == 0)
                {
                    dgv_Cylinder_Details.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_Cylinder_Details.DataSource = ds.Tables[0];
                }
            }

        }

        private void cmb_Item_No_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_Cylinder_Details();
            load_Cylinder_Tracking();
            panel7.Hide();
            panel9.Show();
            panel10.Show();
            panel13.Show();
            panel14.Show();
            //panel14.Show();
            //panel15.Show();
            panel16.Show();
        }

        private void dtp_date1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtp_date2_ValueChanged(object sender, EventArgs e)
        {
            filter_date();
        }

        private void filter_date()
        {
            CurrencyManager cm = (CurrencyManager)BindingContext[dgv_Cylinder_Details.DataSource];
            cm.SuspendBinding();
            foreach(DataGridViewRow row in dgv_Cylinder_Details.Rows)
            {
                if (Convert.ToDateTime(row.Cells["date"].Value).Date > dtp_date1.Value.Date && Convert.ToDateTime(row.Cells["date"].Value).Date < dtp_date2.Value.Date)
                {
                    row.Visible = true;
                }
                else
                {
                    row.Visible = false;
                }
                cm.ResumeBinding();
            }
            
        }

        private void btn_Apply_Filter_Click(object sender, EventArgs e)
        {
            filter_date();
        }

        private void panel12_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel11_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lbl_Supp_Name_Click(object sender, EventArgs e)
        {

        }

        private void panel13_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel14_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel7.Show();
        }
    }
}
