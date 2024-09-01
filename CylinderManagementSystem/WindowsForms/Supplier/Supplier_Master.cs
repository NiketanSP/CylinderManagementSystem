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
    public partial class Supplier_Master : Form
    {
        SqlCommand cmd;
        SqlDataReader sdr;
        SqlConnection con;
        DataTable dt;
        SqlDataAdapter adpt;
        string status;

        public Supplier_Master()
        {
            InitializeComponent();
            Load_Supplier();
            load_cmb_Cylinder_type();
            load_Particulers();
            gridload_Master();
            gridload_Material();
        }

        public void getconnection()
        {
            string str = ConfigurationManager.ConnectionStrings["CylinderData"].ConnectionString;
            con = new SqlConnection(str);
        }
        public void clearcontrols()
        {
            txt_Supp_Name.Clear();
            txt_Address.Clear();
            txt_Company_Phone.Clear();
            txt_Gst_Number.Clear();
            txt_Contact_Person_Name.Clear();
            txt_Contact_Person_Phone.Clear();
            txt_Account_Number.Clear();
            txt_Ifsc_Code.Clear();
            cmb_Bank.ResetText();
            cmb_Search_Supplier.ResetText();
            
            gridload_Material();
        }
        public void clearfilling()
        {
            cmb_cy_Type.ResetText();
            cmb_Particulers.ResetText();
            txt_Rate.Clear();
        }

        public void Load_Supplier()
        {
            cmb_Search_Supplier.Items.Clear();
            //try
            {
                getconnection();
                con.Open();
                if(rdb_Filling_Supplier.Checked == false && rdb_Reguler_Supplier.Checked == false)
                {
                    cmd = new SqlCommand("SELECT DISTINCT Supp_CompName FROM Tb_Supplier_Master", con);
                }
                else
                {
                    cmd = new SqlCommand("SELECT DISTINCT Supp_CompName FROM Tb_Supplier_Master WHERE Status = '" + status +"'", con);
                }
                sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    cmb_Search_Supplier.Items.Add(sdr[0]);
                }
                sdr.Close();
            }
           // catch { }
            //finally { con.Close(); }
        }
        public void gridload_Master()
        {
          
           //try
            {
                getconnection();
                con.Open();

                cmd = new SqlCommand("SELECT Supp_InDate, Supp_CompName, Supp_Address, Supp_Phone, Supp_GST, Supp_AccNo, Supp_IFSC, Status, Supp_Bank,Supp_ConPerson, Supp_CPersonNo FROM Tb_Supplier_Master", con);

                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];

                if (dt.Rows.Count == 0)
                {
                    dgv_Supplier_Master.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_Supplier_Master.DataSource = ds.Tables[0];
                }
            }
            //catch { }
            //finally { con.Close(); }

        }

        public void gridload_Material()
        {
            dgv_Supplier_Material.Show();
           // try
            {
                getconnection();
                con.Open();

                if (txt_Supp_Name.Text == "" && cmb_Search_Supplier.Text == "")
                {
                    cmd = new SqlCommand("SELECT Cylinder_Type, Particulers, Rate,Rate_For,Revised_Date,Supp_Name from Tb_Supplier_Material_Master ", con);
                }
                else
                {
                    cmd = new SqlCommand("SELECT Cylinder_Type, Particulers, Rate,Rate_For,Revised_Date,Supp_Name from Tb_Supplier_Material_Master where Supp_Name = '" + cmb_Search_Supplier.Text + "' OR Supp_Name = '" + txt_Supp_Name.Text + "'", con);
                }

                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];

                if (dt.Rows.Count == 0)
                {
                    dgv_Supplier_Material.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_Supplier_Material.DataSource = ds.Tables[0];
                }
            }
           // catch { }
           // finally { con.Close(); }

        }

        public void Load_Supplier_Data()
        {
            txt_Supp_Name.Text = cmb_Search_Supplier.Text;
            //try
            {
                getconnection();
                con.Open();

                string Supplier_Name = cmb_Search_Supplier.Text.ToString();
                cmd = new SqlCommand("SELECT Supp_InDate,Supp_Address,Supp_Phone,Supp_GST,Supp_ConPerson,Supp_CPersonNo,Supp_AccNo,Supp_IFSC,Supp_Bank FROM Tb_Supplier_Master  WHERE Supp_CompName = '" + Supplier_Name + "'", con);
                sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    dt_In_Date.Text = sdr[0].ToString();
                    txt_Address.Text = sdr[1].ToString();
                    txt_Company_Phone.Text = sdr[2].ToString();
                    txt_Gst_Number.Text = sdr[3].ToString();
                    txt_Contact_Person_Name.Text = sdr[4].ToString();
                    txt_Contact_Person_Phone.Text = sdr[5].ToString();
                    txt_Account_Number.Text = sdr[6].ToString();
                    txt_Ifsc_Code.Text = sdr[7].ToString();
                    cmb_Bank.Text = sdr[8].ToString();
                }
                sdr.Close();

                gridload_On_Supp();
            }
            //catch { }
           //finally { con.Close(); }
        }

        private void gridload_On_Supp()
        {
            //try
            //{
            //    getconnection();
            //    con.Open();

            //    string Supplier_Name = cmb_Search_Supplier.Text.ToString();
            //    cmd = new SqlCommand("SELECT Supp_InDate, Supp_CompName, Supp_Address, Supp_Phone, Supp_GST, Supp_AccNo, Supp_IFSC, Status, Supp_Bank FROM Tb_Supplier_Master  WHERE Supp_CompName = '" + Supplier_Name + "'", con);

            //    adpt = new SqlDataAdapter(cmd);
            //    DataSet ds = new DataSet();
            //    adpt.Fill(ds);
            //    dt = ds.Tables[0];

            //    if (dt.Rows.Count == 0)
            //    {
            //        dgv_Supplier_Master.DataSource = ds.Tables[0];
            //    }
            //    else
            //    {
            //        dgv_Supplier_Master.DataSource = ds.Tables[0];
            //    }
            //    adpt.Dispose();
            //    dt.Dispose();
            //}
            //catch { }
            //finally { con.Close(); }
            CurrencyManager cm = (CurrencyManager)BindingContext[dgv_Supplier_Master.DataSource];
            cm.SuspendBinding();
            for (int i = 1; i < dgv_Supplier_Master.Rows.Count; i++)
            {
                if (dgv_Supplier_Master.Rows[i].Cells["Name"].Value.ToString() == cmb_Search_Supplier.Text)
                {
                    dgv_Supplier_Master.Rows[i].Visible = true;
                }
                else
                {
                    dgv_Supplier_Master.Rows[i].Visible = false;
                }
                cm.ResumeBinding();
            }
        }
        

        public void load_cmb_Cylinder_type()
        {
            try
            {
                getconnection();
                con.Open();

                cmd = new SqlCommand("SELECT DISTINCT Cylinder_Type FROM Tb_Inventory_Master", con);
                sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    cmb_cy_Type.Items.Add(sdr[0]);
                }
                sdr.Close();
            }
            catch { }
            finally { con.Close(); }
        }


        public void load_Particulers()
        {
            cmb_Particulers.Text = "";
            cmb_Particulers.Items.Clear();
           try
            {
                getconnection();
                con.Open();

                //string cylinder_type = cmb_cy_Type.Text.ToString();
                cmd = new SqlCommand("SELECT DISTINCT Particulars FROM Tb_Inventory_Master WHERE Cylinder_Type ='" + cmb_cy_Type.Text + "'", con);
                sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    cmb_Particulers.Items.Add(sdr[0]);
                }
                sdr.Close();
            }
            catch { }
           finally { con.Close(); }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txt_Address.Text == "" && btn_Save.Text=="Save")
            {
                MessageBox.Show("Please Enter The Address", "STOP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txt_Company_Phone.Text == "" && btn_Save.Text == "Save")
            {
                MessageBox.Show("Please Enter Valid Phone No", "STOP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txt_Gst_Number.Text == "" && btn_Save.Text == "Save")
            {
                MessageBox.Show("Please Enter Valid GST Number", "STOP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txt_Account_Number.Text == "" && btn_Save.Text == "Save")
            {
                MessageBox.Show("Please Enter The Account No.", "STOP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txt_Ifsc_Code.Text == "" && btn_Save.Text == "Save")
            {
                MessageBox.Show("Please Enter The IFSC Code", "STOP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (cmb_Bank.Text == "" && btn_Save.Text == "Save")
            {
                MessageBox.Show("Please Enter Bank Name", "STOP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (txt_Contact_Person_Name.Text == "")
                {
                    txt_Contact_Person_Name.Text = "NA";
                }
                if (txt_Contact_Person_Phone.Text == "")
                {
                    txt_Contact_Person_Phone.Text = "NA";
                }

               // try
                {
                    if(btn_Save.Text == "Save")
                    {
                        getconnection();
                        con.Open();


                        cmd = new SqlCommand("Insert_Supplier_Master", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Supp_InDate", dt_In_Date.Text.Trim());
                        cmd.Parameters.AddWithValue("@Supp_CompName", txt_Supp_Name.Text.Trim());
                        cmd.Parameters.AddWithValue("@Supp_Address", txt_Address.Text.Trim());
                        cmd.Parameters.AddWithValue("@Supp_Phone", txt_Company_Phone.Text.Trim());
                        cmd.Parameters.AddWithValue("@Supp_GST", txt_Gst_Number.Text.Trim());
                        cmd.Parameters.AddWithValue("@Supp_ConPerson", txt_Contact_Person_Name.Text.Trim());
                        cmd.Parameters.AddWithValue("@Supp_CPersonNo", txt_Contact_Person_Phone.Text.Trim());
                        cmd.Parameters.AddWithValue("@Supp_AccNo", txt_Account_Number.Text.Trim());
                        cmd.Parameters.AddWithValue("@Supp_IFSC", txt_Ifsc_Code.Text.Trim());
                        cmd.Parameters.AddWithValue("@Supp_Bank", cmb_Bank.Text.Trim());
                        cmd.Parameters.AddWithValue("@status", status);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Data Saved Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                       // gridload_Master();

                    }
                   /* if (rdb_Filling_Supplier.Checked == true && btn_Save.Text == "Save")
                    {
                         getconnection();
                         con.Open();
                        foreach (DataGridViewRow row in dgv_Supplier_Material.Rows)
                        {
                            cmd = new SqlCommand("Insert_Supplier_Material", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                           
                            cmd.Parameters.AddWithValue("@Supp_Name", row.Cells["name_"].Value.ToString());
                            cmd.Parameters.AddWithValue("@Cylinder_Type", row.Cells["type"].Value.ToString());
                            cmd.Parameters.AddWithValue("@Particulers", row.Cells["particuler"].Value.ToString());
                            cmd.Parameters.AddWithValue("@Rate", row.Cells["rate"].Value.ToString());
                            cmd.Parameters.AddWithValue("@Rate_For", row.Cells["year"].Value.ToString());
                            cmd.Parameters.AddWithValue("@Revised_Date", row.Cells["date"].Value.ToString());
                            cmd.ExecuteNonQuery();
                            
                            //gridload_Master();
                        }

                    }*/

                    else if (btn_Save.Text == "Update")
                    {
                        panel13.Enabled = true;
                        getconnection();
                        con.Open();

                        cmd = new SqlCommand("Update_Supplier_Master", con);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Supp_InDate", dt_In_Date.Text.Trim());
                        cmd.Parameters.AddWithValue("@Supp_CompName", txt_Supp_Name.Text.Trim());
                        cmd.Parameters.AddWithValue("@Supp_Address ", txt_Address.Text.Trim());
                        cmd.Parameters.AddWithValue("@Supp_Phone ", txt_Company_Phone.Text.Trim());
                        cmd.Parameters.AddWithValue("@Supp_GST", txt_Gst_Number.Text.Trim());
                        cmd.Parameters.AddWithValue("@Supp_ConPerson", txt_Contact_Person_Name.Text.Trim());
                        cmd.Parameters.AddWithValue("@Supp_CPersonNo", txt_Contact_Person_Phone.Text.Trim());
                        cmd.Parameters.AddWithValue("@Supp_AccNo ", txt_Account_Number.Text.Trim());
                        cmd.Parameters.AddWithValue("@Supp_IFSC ", txt_Ifsc_Code.Text.Trim());
                        cmd.Parameters.AddWithValue("@Supp_Bank", cmb_Bank.Text.Trim());
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Data Updated Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // clearcontrols();
                       // gridload_Master();
                        btn_Save.Text = "Save";
                    }
                    //else if(btn_Save.Text == "Update" && rdb_Filling_Supplier.Checked==true)
                    //{
                    //    getconnection();
                    //    con.Open();
                        
                    //    cmd = new SqlCommand("Update_Supplier_Master", con);
                    //    cmd.CommandType = CommandType.StoredProcedure;

                    //    cmd.Parameters.AddWithValue("@Supp_InDate", dt_In_Date.Text.Trim());
                    //    cmd.Parameters.AddWithValue("@Supp_CompName", txt_Supp_Name.Text.Trim());
                    //    cmd.Parameters.AddWithValue("@Supp_Address ", txt_Address.Text.Trim());
                    //    cmd.Parameters.AddWithValue("@Supp_Phone ", txt_Company_Phone.Text.Trim());
                    //    cmd.Parameters.AddWithValue("@Supp_GST", txt_Gst_Number.Text.Trim());
                    //    cmd.Parameters.AddWithValue("@Supp_ConPerson", txt_Contact_Person_Name.Text.Trim());
                    //    cmd.Parameters.AddWithValue("@Supp_CPersonNo", txt_Contact_Person_Phone.Text.Trim());
                    //    cmd.Parameters.AddWithValue("@Supp_AccNo ", txt_Account_Number.Text.Trim());
                    //    cmd.Parameters.AddWithValue("@Supp_IFSC ", txt_Ifsc_Code.Text.Trim());
                    //    cmd.Parameters.AddWithValue("@Supp_Bank", cmb_Bank.Text.Trim());
                    //    cmd.ExecuteNonQuery();

                    //    MessageBox.Show("Data Updated Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    clearfilling();
                    //    btn_Save.Text = "Save";
                    //}

                }
                //   catch { }
                //  finally { con.Close(); }
                gridload_Master();
                gridload_Material();
                Load_Supplier();
                clearcontrols();
            }
     }
              

        private void txtGSTNumber_TextChanged(object sender, EventArgs e)
        {

        }

        private void Supplier_Master_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'cylinder_ManagementDataSet4.Tb_Supplier_Master' table. You can move, or remove it, as needed.
            //   this.tb_Supplier_MasterTableAdapter.Fill(this.cylinder_ManagementDataSet4.Tb_Supplier_Master);
            //   cmb_Supp_Name.Hide();
            cmb_Search_Supplier.Hide();
            dgv_Supplier_Material.Hide();
            panel16.Hide();
            load_Bank_Combo();

            txt_Supp_Name.Enabled = false;
            txt_Ifsc_Code.Enabled = false;
            txt_Gst_Number.Enabled = false;
            txt_Contact_Person_Phone.Enabled = false;
            txt_Contact_Person_Name.Enabled = false;
            txt_Company_Phone.Enabled = false;
            txt_Address.Enabled = false;
            txt_Account_Number.Enabled = false;
            dt_In_Date.Enabled = false;
            cmb_Bank.Enabled = false;
            rdb_Filling_Supplier.Checked = false;
            rdb_Reguler_Supplier.Checked = false;
        }

        private void load_Bank_Combo()
        {
            try
            {
                getconnection();
                con.Open();

                cmd = new SqlCommand("SELECT DISTINCT Supp_Bank FROM Tb_Supplier_Master", con);
                sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    cmb_Bank.Items.Add(sdr[0]);
                }
                sdr.Close();
            }
            catch { }
            finally { con.Close(); }
        }

        //public void update_data_grid(object sender, DataGridViewCellEventArgs e)
        //{
        //    int limit = dgv_Supplier_Master.RowCount;
        //    int index = e.RowIndex, temp;
        //    for (int i = index; i < limit; i++)
        //    {
        //        temp = Convert.ToInt32(dgv_Supplier_Master.Rows[i].Cells[2].Value);
        //        dgv_Supplier_Master.Rows[i].Cells[0].Value = (temp - 1).ToString();
        //    }
        //}

        //public void update_Material_Grid(object sender, DataGridViewCellEventArgs e)
        //{

        //    int limit = dgv_Supplier_Material.RowCount;
        //    int index = e.RowIndex, temp;
        //    for (int i = index; i < limit; i++)
        //    {
        //        temp = Convert.ToInt32(dgv_Supplier_Material.Rows[i].Cells[2].Value);
        //        dgv_Supplier_Material.Rows[i].Cells[0].Value = (temp - 1).ToString();
        //    }
        //}

        private void DGVSupplier_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string value = dgv_Supplier_Master.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

            if (value == "Edit")
            {
                if (btn_Save.Text == "Update")
                {
                    MessageBox.Show("You Can Update Only One Record At a Time", "STOP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    DataGridViewRow row = dgv_Supplier_Master.Rows[e.RowIndex];
                    dt_In_Date.Text = row.Cells["InDate"].Value.ToString();
                    txt_Supp_Name.Text = row.Cells["Name"].Value.ToString();
                    cmb_Search_Supplier.Text = row.Cells["Name"].Value.ToString();
                    txt_Address.Text = row.Cells["Address"].Value.ToString();
                    txt_Company_Phone.Text = row.Cells["Phone"].Value.ToString();
                    txt_Gst_Number.Text = row.Cells["GST"].Value.ToString();
                    txt_Contact_Person_Name.Text = row.Cells["ConPerson"].Value.ToString();
                    txt_Contact_Person_Phone.Text = row.Cells["Mobile"].Value.ToString();
                    txt_Account_Number.Text = row.Cells["AccNo"].Value.ToString();
                    txt_Ifsc_Code.Text = row.Cells["IFSC"].Value.ToString();
                    cmb_Bank.Text = row.Cells["bank"].Value.ToString();
                    if (row.Cells["supp_type"].Value.ToString() == "REGULAR")
                    {
                        rdb_Reguler_Supplier.Checked = true;
                        panel16.Hide();
                    }
                    else if (row.Cells["supp_type"].Value.ToString() == "FILLING")
                    {
                        rdb_Filling_Supplier.Checked = true;

                    }
                    panel13.Enabled = false;
                    txt_Supp_Name.Enabled = false;
                    dgv_Supplier_Master.Rows.RemoveAt(e.RowIndex);
                    //update_data_grid(sender, e);
                    btn_Save.Text = "Update";
                }
                
            }
            else if (value == "Remove")
            {
                DialogResult result = MessageBox.Show("Are You Sure.?", "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    getconnection();
                    con.Open();

                    cmd = new SqlCommand("DELETE FROM Tb_Supplier_Master WHERE Supp_CompName = '" + dgv_Supplier_Master.Rows[e.RowIndex].Cells["Name"].Value.ToString() + "'", con);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("DELETE FROM Tb_Supplier_Material_Master WHERE Supp_Name = '" + dgv_Supplier_Master.Rows[e.RowIndex].Cells["Name"].Value.ToString() + "'", con);
                    cmd.ExecuteNonQuery();

                    dgv_Supplier_Master.Rows.RemoveAt(e.RowIndex);
                    //update_data_grid(sender, e);
                }
            }
        }

        private void DGVSupplier_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dgv_Supplier_Master.Rows[e.RowIndex].Cells["Sr_No"].Value = (e.RowIndex + 1).ToString();
        }

        private void lbl_Search_Supplier_Click(object sender, EventArgs e)
        {
            if (lbl_Search_Supplier.Text == "Search")
            {
                if(rdb_Filling_Supplier.Checked == false && rdb_Reguler_Supplier.Checked == false)
                {
                    MessageBox.Show("Please Select Supplier Type");
                }
                else if(btn_Save.Text == "Update")
                {
                    MessageBox.Show("Please Update Current Record");
                }
                else
                {
                    btn_Save.Text = "Update";
                    txt_Supp_Name.Hide();
                    cmb_Search_Supplier.Show();
                    lbl_Search_Supplier.Text = "Cancel";
                }
            }
            else
            {
                txt_Supp_Name.Clear();
                cmb_Search_Supplier.Hide();
                txt_Supp_Name.Show();
                lbl_Search_Supplier.Text = "Search";
                btn_Save.Text = "Save";
            }
        }

        private void cmb_Serach_Supplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            btn_Save.Text = "Update";
            if (rdb_Filling_Supplier.Checked == true)
            {
                txt_Supp_Name.Text = cmb_Search_Supplier.Text;
                Load_Supplier_Data();
                gridload_Material();
            }
            else if (rdb_Reguler_Supplier.Checked == true)
            {
                txt_Supp_Name.Text = cmb_Search_Supplier.Text;
                Load_Supplier_Data();
            }
        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            gridload_Master();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if(rdb_Reguler_Supplier.Checked == true)
            {
                status = "REGULAR";
                panel14.Show();
                panel16.Hide();
                dgv_Supplier_Master.Show();
                dgv_Supplier_Material.Hide();
                Load_Supplier();

                CurrencyManager cm = (CurrencyManager)BindingContext[dgv_Supplier_Master.DataSource];
                cm.SuspendBinding();
                for (int i = 0; i < dgv_Supplier_Master.Rows.Count; i++)
                {
                    if (dgv_Supplier_Master.Rows[i].Cells["supp_type"].Value.ToString() == "REGULAR")
                    {
                        dgv_Supplier_Master.Rows[i].Visible = true;
                    }
                    else
                    {
                        dgv_Supplier_Master.Rows[i].Visible = false;
                    }
                    cm.ResumeBinding();
                }


                txt_Supp_Name.Enabled = true;
                txt_Ifsc_Code.Enabled = true;
                txt_Gst_Number.Enabled = true;
                txt_Contact_Person_Phone.Enabled = true;
                txt_Contact_Person_Name.Enabled = true;
                txt_Company_Phone.Enabled = true;
                txt_Address.Enabled = true;
                txt_Account_Number.Enabled = true;
                dt_In_Date.Enabled = true;
                cmb_Bank.Enabled = true;
            }
        }

        private void rdb_Filling_Supplier_CheckedChanged(object sender, EventArgs e)
        {
            if(rdb_Filling_Supplier.Checked == true)
            {
                status = "FILLING";
                panel16.Show();
                panel14.Show();
                dgv_Supplier_Material.Show();
                dgv_Supplier_Master.Show();
                gridload_Material();
                panel14.Enabled = true;
                Load_Supplier();

                CurrencyManager cm = (CurrencyManager)BindingContext[dgv_Supplier_Master.DataSource];
                cm.SuspendBinding();
                for (int i = 0; i < dgv_Supplier_Master.Rows.Count; i++)
                {
                    if (dgv_Supplier_Master.Rows[i].Cells["supp_type"].Value.ToString() == "FILLING")
                    {
                        dgv_Supplier_Master.Rows[i].Visible = true;
                    }
                    else
                    {
                        dgv_Supplier_Master.Rows[i].Visible = false;
                    }
                    cm.ResumeBinding();
                }
                
                txt_Supp_Name.Enabled = true;
                txt_Ifsc_Code.Enabled = true;
                txt_Gst_Number.Enabled = true;
                txt_Contact_Person_Phone.Enabled = true;
                txt_Contact_Person_Name.Enabled = true;
                txt_Company_Phone.Enabled = true;
                txt_Address.Enabled = true;
                txt_Account_Number.Enabled = true;
                dt_In_Date.Enabled = true;
                cmb_Bank.Enabled = true;

            }
            else
            {
                dgv_Supplier_Material.Hide();
            }
            //dgv_Supplier_Master.Hide();
            //dgv_Supplier_Material.Show();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if(btn_Add.Text == "Add")
            {
                if(txt_Supp_Name.Text == "" && cmb_Search_Supplier.Text == "")
                {
                    MessageBox.Show("Please Select Supplier Fisrt");
                }
                else
                { 
                    // try
                    {
                        getconnection();
                        con.Open();

                        cmd = new SqlCommand("Insert_Supplier_Material", con);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Supp_Name", txt_Supp_Name.Text.ToString());
                        cmd.Parameters.AddWithValue("@Cylinder_Type", cmb_cy_Type.Text.ToString());
                        cmd.Parameters.AddWithValue("@Particulers", cmb_Particulers.Text.ToString());
                        cmd.Parameters.AddWithValue("@Rate", txt_Rate.Text.ToString());
                        cmd.Parameters.AddWithValue("@Rate_For", dt_Rate_For.Text.ToString());
                        cmd.Parameters.AddWithValue("@Revised_Date", dt_Revised_Date.Text.ToString());
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Data Added Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        gridload_Material();
                        clearfilling();
                        //clearcontrols();
                    }
                }
             }

            else if (btn_Add.Text == "Update")
            {
                panel24.Show();
                if (txt_Contact_Person_Name.Text == "")
                {
                    txt_Contact_Person_Name.Text = "NA";
                }
                if (txt_Contact_Person_Phone.Text == "")
                {
                    txt_Contact_Person_Phone.Text = "NA";
                }
                // try
                {
                    getconnection();
                    con.Open();

                    cmd = new SqlCommand("Update_Supplier_Material", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Supp_Name", txt_Supp_Name.Text.ToString());
                    cmd.Parameters.AddWithValue("@Cylinder_Type", cmb_cy_Type.Text.ToString());
                    cmd.Parameters.AddWithValue("@Particulers", cmb_Particulers.Text.ToString());
                    cmd.Parameters.AddWithValue("@Rate", txt_Rate.Text.ToString());
                    cmd.Parameters.AddWithValue("@Rate_For", dt_Rate_For.Text.ToString());
                    cmd.Parameters.AddWithValue("@Revised_Date", dt_Revised_Date.Text.ToString());
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Data Updated Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    gridload_Material();
                    clearfilling();
                    btn_Add.Text = "Add";
                    //clearcontrols();
                }
            }

        }

        private void cmb_Type_No_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_Particulers();
        }

        private void cmb_Particulers_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void dgv_Supplier_Material_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string value = dgv_Supplier_Material.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

            if (value == "Edit")
            {
                if (btn_Add.Text == "Update")
                {
                    MessageBox.Show("You Can Update Only One Record At a Time", "STOP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    DataGridViewRow row = dgv_Supplier_Material.Rows[e.RowIndex];
                   // dt_Rate_For.Text = row.Cells["year"].Value.ToString();
                    dt_Revised_Date.Text = row.Cells["date"].Value.ToString();
                    cmb_cy_Type.Text = row.Cells["type"].Value.ToString();
                    cmb_Particulers.Text = row.Cells["particuler"].Value.ToString();
                    txt_Rate.Text = row.Cells["rate"].Value.ToString();
                    txt_Supp_Name.Text = row.Cells["name_"].Value.ToString();
                    cmb_Search_Supplier.Text = row.Cells["name_"].Value.ToString();
                    dgv_Supplier_Material.Rows.RemoveAt(e.RowIndex);

                    btn_Add.Text = "Update";
                }

            }
            else if (value == "Remove")
            {
                DialogResult result = MessageBox.Show("Are You Sure.?", "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    getconnection();
                    con.Open();

                    cmd = new SqlCommand("DELETE FROM Tb_Supplier_Material_Master WHERE Supp_Name = '" + dgv_Supplier_Material.Rows[e.RowIndex].Cells["name_"].Value.ToString() + "' AND Particulers = '" + dgv_Supplier_Material.Rows[e.RowIndex].Cells["particuler"].Value.ToString() + "'", con); 
                    cmd.ExecuteNonQuery();
                    dgv_Supplier_Material.Rows.RemoveAt(e.RowIndex);
                }
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void panel24_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel28_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dt_Revised_Date_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void dgv_Supplier_Material_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dgv_Supplier_Material.Rows[e.RowIndex].Cells["Sr"].Value = (e.RowIndex + 1).ToString();
        }

        private void panel16_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txt_Supp_Name_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
