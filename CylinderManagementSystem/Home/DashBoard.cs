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
using System.Windows.Forms.DataVisualization.Charting;

namespace CylinderManagementSystem
{
    public partial class DashBoard : Form
    {
        SqlConnection con;
        SqlDataReader sdr;
        SqlCommand cmd;
        SqlDataAdapter adpt;
        DataTable dt;
        public DashBoard()
        {
            InitializeComponent();
        }
        public void getconnection()
        {
            string str = ConfigurationManager.ConnectionStrings["CylinderData"].ConnectionString;
            con = new SqlConnection(str);
        }
        private void btn_Load_Click(object sender, EventArgs e)
        {
            
        }
        private void DashBoard_Load(object sender, EventArgs e)
        {
            load_Fill_Chart();
            load_Empty_Chart();
        }
        private void load_Empty_Chart()
        {
            chart_Empty.Series["in_stock"].ChartType = SeriesChartType.Pie;
            //Add some datapoints so the series. in this case you can pass the values to this method
            // try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT Particulers, COUNT(Particulers) FROM Tb_Purchase_Content_Master WHERE Purchase_Type = 'CYLINDER' AND Cylinder_Status = 'EMPTY' GROUP BY Particulers", con);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    chart_Empty.Series["in_stock"].Points.AddXY(sdr[0].ToString(), sdr[1].ToString());
                }
                con.Close();
            }
            // catch { }
        }
        private void load_Fill_Chart()
        {
           // chart_Dashboard.Series.Clear();
            //chart_Dashboard.Legends.Clear();

            //Add a new Legend(if needed) and do some formating
            //chart_Dashboard.Legends.Add("MyLegend");
            //chart_Dashboard.Legends[0].LegendStyle = LegendStyle.Table;
            //chart_Dashboard.Legends[0].Docking = Docking.Bottom;
            //chart_Dashboard.Legends[0].Alignment = StringAlignment.Center;
            //chart_Dashboard.Legends[0].Title = "IN STOCK";
            //chart_Dashboard.Legends[0].BorderColor = Color.Black;

            //Add a new chart-series
            //string seriesname = "in_stock";
            //chart_Dashboard.Series.Add(seriesname);
            //chart_Dashboard.Series[seriesname].IsValueShownAsLabel = true;
            //set the chart-type to "Pie"
            chart_Fill.Series["in_stock"].ChartType = SeriesChartType.Pie;
            //Add some datapoints so the series. in this case you can pass the values to this method
            // try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT Particulers, COUNT(Particulers) FROM Tb_Purchase_Content_Master WHERE Purchase_Type = 'CYLINDER' AND Cylinder_Status = 'FILL' GROUP BY Particulers", con);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    chart_Fill.Series["in_stock"].Points.AddXY(sdr[0].ToString(), sdr[1].ToString());
                }
                con.Close();
            }
            // catch { }
        }
        private void chart_Dashboard_MouseHover(object sender, EventArgs e)
        {
            
        }
        private void chart_Dashboard_Click(object sender, EventArgs e)
        {

        }
        private void chart_Dashboard_MouseClick(object sender, MouseEventArgs e)
        {
            HitTestResult hit = chart_Fill.HitTest(e.X, e.Y, ChartElementType.DataPoint);

            if (hit.PointIndex >= 0 && hit.Series != null)
            {
                DataPoint dp = chart_Fill.Series[0].Points[hit.PointIndex];
                //label1.Text = "Value #" + hit.PointIndex + " = " + dp.XValue;
                MessageBox.Show(dp.ToString());
            }
            
        }
    }
}
