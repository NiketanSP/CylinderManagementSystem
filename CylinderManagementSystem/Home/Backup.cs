using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace CylinderManagementSystem
{
    public partial class Backup : Form
    {
        SqlConnection con;
        public Backup()
        {
            InitializeComponent();
        }
        public void getconnection()
        {
            string str = ConfigurationManager.ConnectionStrings["CylinderData"].ConnectionString;
            con = new SqlConnection(str);
        }

        private void Backup_Load(object sender, EventArgs e)
        {
           // con is the connection string
            //getconnection();
            //con.Open();
            //string str1 = "BACKUP DATABASE Cylinder_Management TO DISK = 'D:\\backupfile.Bak' WITH FORMAT,MEDIANAME = 'MyBackup',NAME = 'MyBackup';";
            //SqlCommand cmd2 = new SqlCommand(str1, con);
            //cmd2.ExecuteNonQuery();
            //MessageBox.Show("success");
            //con.Close();

            getconnection();
            con.Open();
            SqlCommand cmd = new SqlCommand();
            SqlDataReader sdr;
            cmd.CommandText = @"BACKUP DATABASE Cylinder_Management TO DISK ='D:\Cylinder_Management_DataBackup.bak'";

            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            sdr = cmd.ExecuteReader();
            con.Close();
            MessageBox.Show("Database Backup Successfully.");
        }

    }
}
