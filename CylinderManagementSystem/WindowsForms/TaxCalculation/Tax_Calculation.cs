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
    public partial class Tax_Calculation : Form
    {
        SqlConnection con;
        SqlDataReader sdr;
        SqlCommand cmd;
        SqlDataAdapter adpt;
        DataTable dt;

        decimal total_Purchase = 0, sgst_Purchase = 0, cgst_Purchase = 0, igst_Purchase = 0, net_Purchase = 0, gst_Purchase = 0;
        decimal total_Sale = 0, sgst_Sale = 0, cgst_Sale = 0, igst_Sale = 0, net_Sale = 0, gst_Sale = 0;
        string month;

        private void Tax_Calculation_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }
        private void cmb_Date_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public Tax_Calculation()
        {
            InitializeComponent();
        }
        public void getconnection()
        {
            string str = ConfigurationManager.ConnectionStrings["CylinderData"].ConnectionString;
            con = new SqlConnection(str);
        }
       private void dtp_Btwn_Date_ValueChanged(object sender, EventArgs e)
        {
            month = dtp_End_Date.Value.ToString("MMMM").ToUpper();
            load_Purchase();
            load_Sell();
            calculate_Diffrence();
            calculate_Payable_Gst();
            calculate_Payable_Igst();
        }
        private void dtp_By_Date_ValueChanged(object sender, EventArgs e)
        {
            month = dtp_End_Date.Value.ToString("MMMM").ToUpper();
            load_Purchase();
            load_Sell();
            calculate_Diffrence();
            calculate_Payable_Gst();
            calculate_Payable_Igst();
        }
        private void load_Purchase()
        {
            total_Purchase = 0; sgst_Purchase = 0; cgst_Purchase = 0; igst_Purchase = 0; net_Purchase = 0; gst_Purchase = 0;
            getconnection();
            con.Open();

            cmd = new SqlCommand("select Total_Amt, SGST_Amt, CGST_Amt, GST_Amt, IGST_Amt, Net_Amt from Tb_Purchase where CONVERT(datetime,Date_Of_Purchase,103) between  CONVERT(datetime,'" + dtp_Start_Date.Text + "',103) AND CONVERT(datetime,'" + dtp_End_Date.Text + "',103)", con);
            sdr = cmd.ExecuteReader();
            
            while (sdr.Read())
            {
                total_Purchase = total_Purchase + Convert.ToDecimal(sdr[0]);
                sgst_Purchase = sgst_Purchase + Convert.ToDecimal(sdr[1]);
                cgst_Purchase = cgst_Purchase + Convert.ToDecimal(sdr[2]);
                gst_Purchase = gst_Purchase + Convert.ToDecimal(sdr[3]);
                igst_Purchase = igst_Purchase + Convert.ToDecimal(sdr[4]);
                net_Purchase = net_Purchase + Convert.ToDecimal(sdr[5]);
            }
            lbl_Total_Purchase.Text = total_Purchase.ToString();
            lbl_Sgst_Purchase.Text = sgst_Purchase.ToString();
            lbl_Cgst_Purchase.Text = cgst_Purchase.ToString();
            lbl_Gst_Purchase.Text = gst_Purchase.ToString();
            lbl_Igst_Purchase.Text = igst_Purchase.ToString();
            lbl_Taxable_Purchase.Text = net_Purchase.ToString();
        }
        private void load_Sell()
        {
            total_Sale = 0; sgst_Sale = 0; cgst_Sale = 0; igst_Sale = 0; net_Sale = 0; gst_Sale = 0;
            getconnection();
            con.Open();
            cmd = new SqlCommand("select Total_Amt, SGST_Amt, CGST_Amt, GST_Amt, IGST_Amt, Net_Amt from Tb_Tax_Sell_Details where CONVERT(datetime,Sell_Date,103) between  CONVERT(datetime,'" + dtp_Start_Date.Text + "',103) AND CONVERT(datetime,'" + dtp_End_Date.Text + "',103)", con);
            sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                total_Sale = total_Sale + Convert.ToDecimal(sdr[0]);
                sgst_Sale = sgst_Sale + Convert.ToDecimal(sdr[1]);
                cgst_Sale = cgst_Sale + Convert.ToDecimal(sdr[2]);
                gst_Sale = gst_Sale + Convert.ToDecimal(sdr[3]);
                igst_Sale = igst_Sale + Convert.ToDecimal(sdr[4]);
                net_Sale = net_Sale + Convert.ToDecimal(sdr[5]);
            }
            lbl_Total_Sale.Text = total_Sale.ToString();
            lbl_Sgst_Sale.Text = sgst_Sale.ToString();
            lbl_Cgst_Sale.Text = cgst_Sale.ToString();
            lbl_Gst_Sale.Text = gst_Sale.ToString();
            lbl_Igst_Sale.Text = igst_Sale.ToString();
            lbl_Taxable_Sale.Text = net_Sale.ToString();
        }
        private void calculate_Diffrence()
        {
            lbl_Total_diff.Text = (total_Sale - total_Purchase).ToString();
            lbl_Sgst_Diff.Text = (sgst_Sale - sgst_Purchase).ToString();
            lbl_Cgst_Diff.Text = (cgst_Sale - cgst_Purchase).ToString();
            lbl_Igst_Diff.Text = (igst_Sale - igst_Purchase).ToString();
            lbl_Gst_Diff.Text = (gst_Sale - gst_Purchase).ToString();
            lbl_Taxable_Diff.Text = (net_Sale - net_Purchase).ToString();
        }
        private void calculate_Payable_Gst()
        {
            decimal gst;
            gst = Convert.ToDecimal(lbl_Gst_Diff.Text);
            if(gst < 0)
            {
                lbl_Payble_Gst.Text = month + " GST/CONSUMPTION RS :";
            }
            else
            {
                lbl_Payble_Gst.Text = month + " PAYABLE GST RS";
            }
            lbl_Gst_Amt.Text = Math.Abs(gst).ToString();
        }
        private void calculate_Payable_Igst()
        {
            decimal igst;
            igst = Convert.ToDecimal(lbl_Igst_Diff.Text);
            if (igst < 0)
            {
                lbl_Payble_Igst.Text = month + " IGST/ CONSUMPTION RS ";
            }
            else
            {
                lbl_Payble_Igst.Text = month + " PAYABLE IGST RS";
            }
            lbl_Igst_Amt.Text = Math.Abs(igst).ToString();
        }
    }
}
