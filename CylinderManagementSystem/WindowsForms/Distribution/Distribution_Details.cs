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
    public partial class Distribution_Details : Form
    {
        SqlConnection con;
        SqlDataReader sdr;
        SqlCommand cmd;
        SqlDataAdapter adpt;
        DataTable dt;
        public Distribution_Details()
        {
            InitializeComponent();
        }
        public void getconnection()
        {
            string str = ConfigurationManager.ConnectionStrings["CylinderData"].ConnectionString;
            con = new SqlConnection(str);
        }
        private void rdb_All_CheckedChanged(object sender, EventArgs e)
        {
            if(rdb_Customer.Checked == true)
            {
                lbl_search_by.Text = "CUSTOMER";
                cmb_Customer_Supplier.Text = "";
              load_Regular_Customer_Distribution();
                panel14.Show();
                panel14.Dock = DockStyle.Fill;
                panel29.Hide();
                panel12.Show();
                label3.Text = "CUSTOMER DISTRUBUTION DETAILS";
            }
        }
        private void rdb_Supplier_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_Supplier.Checked == true)
            {
                lbl_search_by.Text = "SUPPLIER";
                cmb_Customer_Supplier.Text = "";
                load_Supplier_Distribution();
                panel14.Hide();
                panel29.Show();
                panel29.Dock = DockStyle.Fill;
                label3.Text = "SUPPLIER DISTRUBUTION DETAILS";
            }
        }
        private void load_Regular_Customer_Distribution()
        {
            cmb_Customer_Supplier.Items.Clear();
            //try
            {
                getconnection();
                con.Open();
                panel12.Show();
                cmd = new SqlCommand("SELECT DISTINCT Cust_Name FROM Tb_Customer_Master WHERE Cust_Status = 'Regular'", con);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    cmb_Customer_Supplier.Items.Add(sdr[0]);
                }
                sdr.Close();
            }
            //catch { }
            //finally { con.Close(); }
        }
        private void load_Walkin_Customer_Distribution()
        {
            cmb_Customer_Supplier.Items.Clear();
            //try
            {
                getconnection();
                con.Open();
                panel12.Show();
                cmd = new SqlCommand("SELECT DISTINCT Cust_Name FROM Tb_Customer_Master WHERE Cust_Status = 'Walk In'", con);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    cmb_Customer_Supplier.Items.Add(sdr[0]);
                }
                sdr.Close();
            }
            //catch { }
            //finally { con.Close(); }
        }
        private void load_Supplier_Distribution()
        {
            cmb_Customer_Supplier.Items.Clear();
            //try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT DISTINCT Supplier_Name FROM Tb_Fill_Master", con);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    cmb_Customer_Supplier.Items.Add(sdr[0]);
                }
                sdr.Close();
            }
            //catch { }
            //finally { con.Close(); }
        }
        private void Distribution_Details_Load(object sender, EventArgs e)
        {
            panel5.Hide();
            panel19.Hide();
            panel27.Hide();
            panel12.Hide();
        }
        private void cmb_Customer_Supplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdb_Customer.Checked == true)
            {
                gridload_By_Customer();
                load_Material_Combo();
                panel5.Show();
            }
            else if (rdb_Supplier.Checked == true)
            {
                gridload_By_Supplier();
                panel5.Show();
            }
            else if (rdb_Walk_In.Checked == true)
            {
                gridload_By_Customer();
                load_Material_Combo();
                panel5.Show();
                
            }
            panel5.Hide();
        }
        private void gridload_By_Customer()
        {
            //try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT C_Dc_No, Particulars, Sr_No, Item_No, Quantity, Sell_Status FROM Tb_CDC_Content_Details WHERE C_Dc_No IN(SELECT C_Dc_No FROM Tb_CDC_Details WHERE C_Name = '" + cmb_Customer_Supplier.Text + "') AND Cy_Type != 'NA' AND Receive_Status = 'Pending'", con);
                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];

                if (dt.Rows.Count == 0)
                {
                    dgv_Cylinder.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_Cylinder.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();
            }
            //catch { }
            //finally { con.Close(); }
            //GRID LOAD MATERIAL
            //try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT C_Dc_No, Particulars, Quantity, Sell_Status FROM Tb_CDC_Content_Details WHERE C_Dc_No IN(SELECT C_Dc_No FROM Tb_CDC_Details WHERE C_Name = '" + cmb_Customer_Supplier.Text + "') AND Cy_Type = 'NA' AND Receive_Status = 'Pending'", con);
                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    dgv_Material.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_Material.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();
            }
            //catch { }
            //finally { con.Close(); }
        }
        private void load_Material_Combo()
        {
            cmb_Material_Name.Items.Clear();
            foreach (DataGridViewRow row in dgv_Material.Rows)
            {
                cmb_Material_Name.Items.Add(row.Cells["particular"].Value.ToString());
            }
        }
        private void gridload_By_Supplier()
        {
            //GRID LOAD CYLINDER 
            //try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT Item_No, Sr_No, Name, Fill_ID FROM Tb_Fill_Content_Master WHERE STATUS LIKE 'SENT%' AND Fill_ID IN (SELECT Fill_ID FROM Tb_Fill_Master WHERE Supplier_Name = '" + cmb_Customer_Supplier.Text.ToString() + "')", con);
                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];

                if (dt.Rows.Count == 0)
                {
                    dgv_Cylinder_Supplier.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_Cylinder_Supplier.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();
            }
            //catch { }
            //finally { con.Close(); }
        }
        private void dgv_Cylinder_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dgv_Cylinder.Rows[e.RowIndex].Cells["sr"].Value = (e.RowIndex + 1).ToString();
        }

        private void dgv_Cylinder_Supplier_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dgv_Cylinder_Supplier.Rows[e.RowIndex].Cells["sr_no"].Value = (e.RowIndex + 1).ToString();
        }
        private void dgv_Material_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dgv_Material.Rows[e.RowIndex].Cells["srno"].Value = (e.RowIndex + 1).ToString();
        }
        private void rbt_Rent_CheckedChanged(object sender, EventArgs e)
        {
            if(rdb_Rent.Checked == true)
            {
                CurrencyManager cm = (CurrencyManager)BindingContext[dgv_Cylinder.DataSource];
                cm.SuspendBinding();
                foreach (DataGridViewRow row in dgv_Cylinder.Rows)
                {
                    if (row.Cells["status"].Value.ToString() == "RENT")
                    {
                        row.Visible = true;
                    }
                    else
                    {
                        row.Visible = false;
                    }
                    cm.ResumeBinding();
                }
                cm = (CurrencyManager)BindingContext[dgv_Material.DataSource];
                cm.SuspendBinding();
                foreach (DataGridViewRow row in dgv_Material.Rows)
                {
                    if (row.Cells["sell_status"].Value.ToString() == "RENT")
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
        }
        private void rdb_Sell_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_Sell.Checked == true)
            {
                CurrencyManager cm = (CurrencyManager)BindingContext[dgv_Cylinder.DataSource];
                cm.SuspendBinding();
                foreach (DataGridViewRow row in dgv_Cylinder.Rows)
                {
                    if (row.Cells["status"].Value.ToString() == "SELL")
                    {
                        row.Visible = true;
                    }
                    else
                    {
                        row.Visible = false;
                    }
                    cm.ResumeBinding();
                }
                cm = (CurrencyManager)BindingContext[dgv_Material.DataSource];
                cm.SuspendBinding();
                foreach (DataGridViewRow row in dgv_Material.Rows)
                {
                    if (row.Cells["sell_status"].Value.ToString() == "SELL")
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
        }
        private void txt_Item_No_TextChanged(object sender, EventArgs e)
        {
            CurrencyManager cm = (CurrencyManager)BindingContext[dgv_Cylinder.DataSource];
            cm.SuspendBinding();
            foreach (DataGridViewRow row in dgv_Cylinder.Rows)
            {
                if (row.Cells["item"].Value.ToString().Contains(txt_Item_No.Text))
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
        private void cmb_Material_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrencyManager cm = (CurrencyManager)BindingContext[dgv_Material.DataSource];
            cm.SuspendBinding();
            foreach (DataGridViewRow row in dgv_Material.Rows)
            {
                if (row.Cells["particular"].Value.ToString() == cmb_Material_Name.Text)
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
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_Walk_In.Checked == true)
            {
                lbl_search_by.Text = "WALK IN CUSTOMER";
                cmb_Customer_Supplier.Text = "";
                load_Walkin_Customer_Distribution();
                panel14.Show();
                panel14.Dock = DockStyle.Fill;
                panel29.Hide();
                panel12.Show();
                label3.Text = "WALK IN CUSTOMER DISTRUBUTION DETAILS";
                if (lbl_search_by.Text == "WALK IN CUSTOMER")
                {
                    if (dgv_Cylinder.RowCount > 0)
                    {
                        DataTable DT = (DataTable)dgv_Cylinder.DataSource;
                        if (DT != null)
                            DT.Clear();
                    }
                    if (dgv_Material.RowCount > 0)
                    {
                        DataTable DT = (DataTable)dgv_Material.DataSource;
                        if (DT != null)
                            DT.Clear();
                    }
                }
            }
        }

        private void cmb_Customer_Supplier_TextChanged(object sender, EventArgs e)
        {
            if (rdb_Customer.Checked == true)
            {
                gridload_By_Customer();
                load_Material_Combo();
                panel5.Show();
            }
            else if (rdb_Supplier.Checked == true)
            {
                gridload_By_Supplier();
                panel5.Show();
            }
            else if (rdb_Walk_In.Checked == true)
            {
                gridload_By_Customer();
                load_Material_Combo();
                panel5.Show();
            }
            panel5.Hide();
        }
    }
}
