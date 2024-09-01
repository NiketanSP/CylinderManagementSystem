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
using System.Configuration;
using System.Data.SqlClient;

namespace CylinderManagementSystem
{
    public partial class Home : Form
    {
        private int childFormNumber = 0;
        SqlConnection con;
        SqlDataReader sdr;
        SqlCommand cmd;
        SqlDataAdapter adpt;
        DataTable dt;
        String user;
        public Home()
        {
            InitializeComponent();
        }
       
        public void getconnection()
        {
            string str = ConfigurationManager.ConnectionStrings["CylinderData"].ConnectionString;
            con = new SqlConnection(str);
        }
       
      
        private void agencyMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCollection formsList = Application.OpenForms;
            for (int i = formsList.Count - 1; i > 0; i--)
            {
                if (formsList[i].Name != "Home" && formsList[i].Name != "DashBoard")
                {
                    formsList[i].Close();
                }
            }
            Company_Details fm = new Company_Details();
            fm.WindowState = FormWindowState.Normal;
            fm.MdiParent = this;
            fm.Show();
        }
        private void assetMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCollection formsList = Application.OpenForms;
            for (int i = formsList.Count - 1; i > 0; i--)
            {
                if (formsList[i].Name != "Home" && formsList[i].Name != "DashBoard")
                {
                    formsList[i].Close();
                }
            }
            Asset_Master fm = new Asset_Master();
            fm.MdiParent = this;
            fm.Show();
        }
        private void staffMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCollection formsList = Application.OpenForms;
            for (int i = formsList.Count - 1; i > 0; i--)
            {
                if (formsList[i].Name != "Home" && formsList[i].Name != "DashBoard")
                {
                    formsList[i].Close();
                }
            }
            Staff_Master fm = new Staff_Master();
            fm.MdiParent = this;
            fm.Show();
        }
        private void customerMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCollection formsList = Application.OpenForms;
            for (int i = formsList.Count - 1; i > 0; i--)
            {
                if (formsList[i].Name != "Home" && formsList[i].Name != "DashBoard")
                {
                    formsList[i].Close();
                }
            }
            Customer_Master fm = new Customer_Master();
            fm.MdiParent = this;
            fm.Show();
        }
        private void supplierMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCollection formsList = Application.OpenForms;
            for (int i = formsList.Count - 1; i > 0; i--)
            {
                if (formsList[i].Name != "Home" && formsList[i].Name != "DashBoard")
                {
                    formsList[i].Close();
                }
            }
           Supplier_Master fm = new Supplier_Master();
            fm.MdiParent = this;
            fm.Show();
        }
        private void transportMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCollection formsList = Application.OpenForms;
            for (int i = formsList.Count - 1; i > 0; i--)
            {
                if (formsList[i].Name != "Home" && formsList[i].Name != "DashBoard")
                {
                    formsList[i].Close();
                }
            }
            Transport_Master fm = new Transport_Master();
            fm.MdiParent = this;
            fm.Show();
        }
        private void newPurchaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCollection formsList = Application.OpenForms;
            for (int i = formsList.Count - 1; i > 0; i--)
            {
                if (formsList[i].Name != "Home" && formsList[i].Name != "DashBoard")
                {
                    formsList[i].Close();
                }
            }
            Purchase_Master fm = new Purchase_Master();
            fm.MdiParent = this;
            fm.Show();
        }
        private void searchPurchseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //FormCollection formsList = Application.OpenForms;
            //for (int i = formsList.Count - 1; i > 0; i--)
            //{
            //    if (formsList[i].Name != "Home")
            //    {
            //        formsList[i].Close();
            //    }
            //}
            Purchase_Search fm = new Purchase_Search();
            fm.MdiParent = this;
            fm.Show();
        }
        private void purchaseLedgerToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }
        private void inventoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }
        private void fillingLedgerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
        private void fillingActivityToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }
        private void salesLedgerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ;
        }
        private void challanSalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //FormCollection formsList = Application.OpenForms;
            //for (int i = formsList.Count - 1; i > 0; i--)
            //{
            //    if (formsList[i].Name != "Home" && formsList[i].Name != "DashBoard")
            //    {
            //        formsList[i].Close();
            //    }
            //}
            Customer_DC fm = new Customer_DC();
            fm.MdiParent = this;
            fm.Show();
        }
        private void dcToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void taxableSalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //FormCollection formsList = Application.OpenForms;
            //for (int i = formsList.Count - 1; i > 0; i--)
            //{
            //    if (formsList[i].Name != "Home" && formsList[i].Name != "DashBoard")
            //    {
            //        formsList[i].Close();
            //    }
            //}
            Tax_Sales fm = new Tax_Sales();
            fm.MdiParent = this;
            fm.Show();
        }
        private void issueCylinderToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void expencesMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //FormCollection formsList = Application.OpenForms;
            //for (int i = formsList.Count - 1; i > 0; i--)
            //{
            //    if (formsList[i].Name != "Home" && formsList[i].Name != "DashBoard")
            //    {
            //        formsList[i].Close();
            //    }
            //}
            Expense_Master fm = new Expense_Master();
            fm.MdiParent = this;
            fm.Show();
        }
        private void distributionDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
        private void recivedFilledCylindersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //FormCollection formsList = Application.OpenForms;
            //for (int i = formsList.Count - 1; i > 0; i--)
            //{
            //    if (formsList[i].Name != "Home" && formsList[i].Name != "DashBoard")
            //    {
            //        formsList[i].Close();
            //    }
            //}
            Filling_Ledger fm = new Filling_Ledger();
            fm.MdiParent = this;
            fm.Show();
        }
        private void taxSaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //FormCollection formsList = Application.OpenForms;
            //for (int i = formsList.Count - 1; i > 0; i--)
            //{
            //    if (formsList[i].Name != "Home" && formsList[i].Name != "DashBoard")
            //    {
            //        formsList[i].Close();
            //    }
            //}
            Tax_Sales_Ledger fm = new Tax_Sales_Ledger();
            fm.MdiParent = this;
            fm.Show();
        }
        private void purchaseLedgerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //FormCollection formsList = Application.OpenForms;
            //for (int i = formsList.Count - 1; i > 0; i--)
            //{
            //    if (formsList[i].Name != "Home" && formsList[i].Name != "DashBoard")
            //    {
            //        formsList[i].Close();
            //    }
            //}
            Purchase_Ledger fm = new Purchase_Ledger();
            fm.MdiParent = this;
            fm.Show();
        }
        private void challanLedgerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //FormCollection formsList = Application.OpenForms;
            //for (int i = formsList.Count - 1; i > 0; i--)
            //{
            //    if (formsList[i].Name != "Home" && formsList[i].Name != "DashBoard")
            //    {
            //        formsList[i].Close();
            //    }
            //}
            Customer_DC_Ledger fm = new Customer_DC_Ledger();
            fm.MdiParent = this;
            fm.Show();
        }
        private void deliverdCylindersToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void reportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //FormCollection formsList = Application.OpenForms;
            //for (int i = formsList.Count - 1; i > 0; i--)
            //{
            //    if (formsList[i].Name != "Home" && formsList[i].Name != "DashBoard")
            //    {
            //        formsList[i].Close();
            //    }
            //}
            Distribution_Details fm = new Distribution_Details();
            fm.MdiParent = this;
            fm.Show();
        }
        private void viewDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //FormCollection formsList = Application.OpenForms;
            //for (int i = formsList.Count - 1; i > 0; i--)
            //{
            //    if (formsList[i].Name != "Home" && formsList[i].Name != "DashBoard")
            //    {
            //        formsList[i].Close();
            //    }
            //}
            Staff_Details fm = new Staff_Details();
            fm.MdiParent = this;
            fm.Show();
        }
        private void taxCalculationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //FormCollection formsList = Application.OpenForms;
            //for (int i = formsList.Count - 1; i > 0; i--)
            //{
            //    if (formsList[i].Name != "Home" && formsList[i].Name != "DashBoard")
            //    {
            //        formsList[i].Close();
            //    }
            //}
            Tax_Calculation fm = new Tax_Calculation();
            fm.MdiParent = this;
            fm.Show();
        }
        private void pAYMENTREGISTERToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //FormCollection formsList = Application.OpenForms;
            //for (int i = formsList.Count - 1; i > 0; i--)
            //{
            //    if (formsList[i].Name != "Home" && formsList[i].Name != "DashBoard")
            //    {
            //        formsList[i].Close();
            //    }
            //}
            Payment_Master fm = new Payment_Master();
            fm.MdiParent = this;
            fm.Show();
        }
        private void nEWFILLINGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //FormCollection formsList = Application.OpenForms;
            //for (int i = formsList.Count - 1; i > 0; i--)
            //{
            //    if (formsList[i].Name != "Home" && formsList[i].Name != "DashBoard")
            //    {
            //        formsList[i].Close();
            //    }
            //}
            Filling_Register fm = new Filling_Register();
            fm.MdiParent = this;
            fm.Show();
        }
        private void bACKUPDATAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            getconnection();
            con.Open();
            SqlCommand cmd = new SqlCommand();
            SqlDataReader sdr;
            cmd.CommandText = @"BACKUP DATABASE New_Cylinder_Final_Updated_2021 TO DISK ='D:\Software Daily Backup\Backup.bak'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            sdr = cmd.ExecuteReader();
            con.Close();
            MessageBox.Show("Database Backup Successfully.");
        }
        private void Home_Load(object sender, EventArgs e)
        {
            // panel2.Hide();
            panel1.Hide();
            menuStrip1.Hide();
        }
        public void show_login_btn()
        {
            panel21.Show();
        }
        private void dASHBOARDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCollection formsList = Application.OpenForms;
            for (int i = formsList.Count - 1; i > 0; i--)
            {
                if (formsList[i].Name != "Home" && formsList[i].Name != "DashBoard")
                {
                    formsList[i].Close();
                }
            }
            DashBoard fm = new DashBoard();
            fm.MdiParent = this;
            fm.Show();
        }
        private void link_Signup_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           
        }
        private void btn_Login_Click(object sender, EventArgs e)
        {
            //try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT User_Type FROM Tb_SignUp_Details WHERE User_ID = '" + txt_Username.Text + " ' AND Password = '" + txt_Password.Text + "'", con);
                sdr = cmd.ExecuteReader();
                if(sdr.Read())
                {
                    user = sdr[0].ToString();
                    start_Home(user);
                    MessageBox.Show("Login SuccessFully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DashBoard d = new DashBoard();
                    d.MdiParent = this;
                    d.Show();
                    txt_Username.Clear();
                    txt_Password.Clear();
                }
                else
                {
                    DialogResult result = MessageBox.Show("User Not Found", "Warning", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                    if(result == DialogResult.Retry)
                    {
                        txt_Username.Clear();
                        txt_Password.Clear();
                        txt_Username.Focus();
                    }
                    else
                    {
                        txt_Username.Clear();
                        txt_Password.Clear();
                        panel1.Hide();
                        panel21.Show();
                    }
                }
            }
            //catch { }
        }
        private void start_Home(string user)
        {
            if(user == "ADMIN")
            {
                panel1.Hide();
                panel2.Show();
                panel21.Show();
                btn_login_out.Text = "LOGOUT";
                menuStrip1.Show();
            }
            else if(user == "USER")
            {
                panel1.Hide();
                purchseRegisterToolStripMenuItem.Visible = false;
                pAYMENTREGISTERToolStripMenuItem.Visible = false;
                agencyMasterToolStripMenuItem.Visible = false;
                staffMasterToolStripMenuItem.Visible = false;
                taxCalculationToolStripMenuItem.Visible = false;
                panel2.Show();
                panel21.Show();
                btn_login_out.Text = "LOGOUT";
                menuStrip1.Show();
            }
        }
        private void label2_Click(object sender, EventArgs e)
        {
            panel1.Hide();
            panel21.Show();
            SignUp form = new SignUp();
            form.Show();
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.ntechsolutionservice.com");
        }
        private void btn_login_out_Click(object sender, EventArgs e)
        {
            panel2.Show();
            panel21.Show();
            btn_login_out.Text = "LOGOUT";
            menuStrip1.Show();
            //if (btn_login_out.Text == "LOGIN")
            //{
            //    panel1.Show();
            //    panel21.Hide();
            //    txt_Username.Focus();
            //    btn_login_out.Text = "LOGOUT";
            //}
            //else if (btn_login_out.Text == "LOGOUT")
            //{
            //    DialogResult result = MessageBox.Show("Are You Sure?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //    if (result == DialogResult.Yes)
            //    {
            //        FormCollection formsList = Application.OpenForms;
            //        for (int i = formsList.Count - 1; i > 0; i--)
            //        {
            //            if (formsList[i].Name != "Home")
            //            {
            //                formsList[i].Close();
            //            }
            //        }
            //        menuStrip1.Hide();
            //        btn_login_out.Text = "LOGIN";
            //    }
            //}
        }
        private void txt_Password_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }






        private void txt_Password_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                btn_Login.PerformClick();
            }
        }

        private void tRACKCYLINDERToolStripMenuItem_Click(object sender, EventArgs e)
        {
           

            Inventory_Details fm = new Inventory_Details();
            fm.MdiParent = this;
            fm.Show();
        }

        private void tRACKCYLINDERToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormCollection formsList = Application.OpenForms;
            for (int i = formsList.Count - 1; i > 0; i--)
            {
                if (formsList[i].Name != "Home" && formsList[i].Name != "DashBoard")
                {
                    formsList[i].Close();
                }
            }
            Track_Cylinder fm = new Track_Cylinder();
            fm.MdiParent = this;
            fm.Show();
        }
    }
}
