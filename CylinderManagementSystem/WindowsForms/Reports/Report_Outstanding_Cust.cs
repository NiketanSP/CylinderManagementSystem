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
    public partial class Report_Outstanding_Cust : Form
    {
        SqlConnection con;
        SqlDataReader sdr;
        SqlCommand cmd, cmd_update;
        SqlDataAdapter adpt;
        DataTable dt;
        string Name;

        public Report_Outstanding_Cust()
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
            Name = id;
        }

        private void Report_Outstanding_Cust_Load(object sender, EventArgs e)
        {
            getconnection();
            con.Open();

            cmd = new SqlCommand("select * from Tb_Cylinder_Agency_Master, Tb_Customer_Master, Tb_Tax_Sell_Details where Tb_Customer_Master.Cust_Name='" + Name + "' and Tb_Tax_Sell_Details.Cust_Name ='" + Name + "' and Tb_Tax_Sell_Details.Balance_Amt > 0", con);
            adpt = new SqlDataAdapter(cmd);
            dt = new DataTable();

            adpt.Fill(dt);

            CRPT_Outstanding_Customer report = new CRPT_Outstanding_Customer();

            report.SetDataSource(dt);

            crystalReportViewer1.ReportSource = report;

            crystalReportViewer1.Refresh();
        }
    }
}
