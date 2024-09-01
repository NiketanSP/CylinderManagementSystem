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
    public partial class Report_Tax_DC : Form
    {
        SqlConnection con;
        SqlDataReader sdr;
        SqlCommand cmd, cmd_update;
        SqlDataAdapter adpt;
        DataTable dt;
        string ID;

        public Report_Tax_DC()
        {
            InitializeComponent();
        }

        public void getconnection()
        {
            string str = ConfigurationManager.ConnectionStrings["CylinderData"].ConnectionString;
            con = new SqlConnection(str);
        }

        public void get_ID(string id)
        {
            ID = id;
        }

        private void Report_Tax_DC_Load(object sender, EventArgs e)
        {

            getconnection();
            con.Open();

            cmd = new SqlCommand("select * from Tb_Cylinder_Agency_Master, Tb_Customer_Master, Tb_Tax_Content_DC, Tb_Tax_Sell_Details where Tb_Tax_Content_DC.Invoice_No='" + ID + "' and Tb_Tax_Sell_Details.Invoice_No ='" + ID + "' and Tb_Customer_Master.Cust_Name = Tb_Tax_Sell_Details.Cust_Name", con);
            adpt = new SqlDataAdapter(cmd);
            dt = new DataTable();

            adpt.Fill(dt);

            CRPT_Tax_DC report = new CRPT_Tax_DC();

            report.SetDataSource(dt);

            Crpt_Tax_DC.ReportSource = report;

            Crpt_Tax_DC.Refresh();
        }
    }
}
