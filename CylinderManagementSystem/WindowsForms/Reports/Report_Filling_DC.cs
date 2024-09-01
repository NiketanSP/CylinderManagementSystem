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
    public partial class Report_Filling_DC : Form
    {
        SqlConnection con;
        SqlDataReader sdr;
        SqlCommand cmd, cmd_update;
        SqlDataAdapter adpt;
        DataTable dt;
        string ID;

        public Report_Filling_DC()
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

        private void CRPT_Filling_DC_Load(object sender, EventArgs e)
        {
            getconnection();
            con.Open();

            cmd = new SqlCommand("select * from Tb_Cylinder_Agency_Master, Tb_Supplier_Master, Tb_Fill_Master, Tb_Fill_Content_Master where Tb_Fill_Master.Fill_ID='" + ID + "' and Tb_Fill_Content_Master.Fill_ID ='" + ID + "' and Tb_Supplier_Master.Supp_CompName=Tb_Fill_Master.Supplier_Name", con);
            adpt = new SqlDataAdapter(cmd);
            dt = new DataTable();

            adpt.Fill(dt);

            CR_Filling_DC report = new CR_Filling_DC();

            report.SetDataSource(dt);

            Crpt_Filling.ReportSource = report;

            Crpt_Filling.Refresh();
        }
    }
}
