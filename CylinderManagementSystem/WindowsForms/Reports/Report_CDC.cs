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
    public partial class Report_CDC : Form
    {
        SqlConnection con;
        SqlDataReader sdr;
        SqlCommand cmd, cmd_update;
        SqlDataAdapter adpt;
        DataTable dt;
        string ID;

        public Report_CDC()
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

        private void CDC_Report_Load(object sender, EventArgs e)
        {
            getconnection();
            con.Open();
            
            cmd = new SqlCommand("select * from Tb_Cylinder_Agency_Master, Tb_Customer_Master, Tb_CDC_Details, Tb_CDC_Content_Details where Tb_CDC_Details.C_Dc_No='" + ID + "' and Tb_CDC_Content_Details.C_Dc_No='" + ID + "' and Tb_Customer_Master.Cust_Name = Tb_CDC_Details.C_Name", con);
            adpt = new SqlDataAdapter(cmd);
            dt = new DataTable();

            adpt.Fill(dt);

            CRPT_CDC report = new CRPT_CDC();

            report.SetDataSource(dt);

            Crpt_CDC.ReportSource = report;

            Crpt_CDC.Refresh(); ;
        }
    }
}
