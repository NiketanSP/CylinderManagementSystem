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
    public partial class Expense_Master : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader sdr;
        SqlDataAdapter adpt;
        DataTable dt;
        string Exp_Type;
        public Expense_Master()
        {
            InitializeComponent();
            gridload();
        }
        public void getconnection()
        {
            string str = ConfigurationManager.ConnectionStrings["CylinderData"].ConnectionString;
            con = new SqlConnection(str);
        }
        public void clearControl()
        {
            txt_Exp_Amount.Clear();
            cmb_Exp_Description.ResetText();
            cmb_Person_Name.ResetText();
            cmb_Vehicle_No.ResetText();
            txt_Wages_Amount.Clear();
            cmb_Wages_Type.ResetText();
            txt_Wages_Description.Clear();
            cmb_Search_Fual_Expenses.ResetText();
            cmb_Wages_To.ResetText();
        }
        public void Load_Name()
        {
            cmb_Search_Names.Items.Clear();
            cmb_Search_Names.ResetText();
            try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT DISTINCT Person_Name FROM Tb_Expenses_Master", con);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    cmb_Search_Names.Items.Add(sdr[0]);
                }
                sdr.Close();
            }
            catch { }
            finally { con.Close(); }
        }
        public void Load_Wages_Name()
        {
            cmb_Search_Wages_Names.Items.Clear();
            cmb_Search_Wages_Names.ResetText();
            try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT DISTINCT Person_Name FROM Tb_Expenses_Master", con);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    cmb_Search_Wages_Names.Items.Add(sdr[0]);
                }
                sdr.Close();
            }
            catch { }
            finally { con.Close(); }
        }
        public void Load_Vehicle()
        {
            cmb_Search_vehicle.Items.Clear();
            cmb_Search_vehicle.ResetText();
            try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT DISTINCT Vehicle_No FROM Tb_Expenses_Master", con);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    cmb_Search_vehicle.Items.Add(sdr[0]);
                }
                sdr.Close();
            }
            catch { }
            finally { con.Close(); }
        }
         public void show_by_Date()
        {
            String date = dt_By_Date.Text.ToString();
            try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT Ex_Date,Ex_Type,Vehicle_No,Person_Name,Ex_Amount,Ex_Description FROM Tb_Expenses_Master WHERE Ex_Date = '" + date + "'", con);
                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    dgv_Expense_Master.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_Expense_Master.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();
            }
            catch { }
            finally { con.Close(); }
        }
        public void show_Btw_Date_by_vehicle()
        {
            String start_date = dt_By_Date.Text.ToString();
            String end_date = dt_Btw_Date.Text.ToString();
            String vehicle = cmb_Search_vehicle.Text.ToString();
            try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT Ex_Date,Ex_Type,Vehicle_No,Person_Name,Ex_Amount,Ex_Description,Wages_Type FROM Tb_Expenses_Master WHERE Ex_Date BETWEEN '" + start_date + "' AND '" + end_date + "' AND  Vehicle_No= '" + vehicle + "'", con);
                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    dgv_Expense_Master.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_Expense_Master.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();
            }
            catch { }
            finally { con.Close(); }
        }
        public void show_Btw_Date_by_name()
        {
            String start_date = dt_By_Date.Text.ToString();
            String end_date = dt_Btw_Date.Text.ToString();
            String name = cmb_Search_Names.Text.ToString();
            try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT Ex_Date,Ex_Type,Vehicle_No,Person_Name,Ex_Amount,Ex_Description,Wages_Type FROM Tb_Expenses_Master WHERE Ex_Date BETWEEN '" + start_date + "' AND '" + end_date + "' AND  Person_Name= '"+ name +"'", con);
                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    dgv_Expense_Master.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_Expense_Master.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();
            }
            catch { }
            finally { con.Close(); }
        }
        public void show_name_by_Date()
        {
            String name = cmb_Search_Names.Text.ToString();
            String date = dt_By_Date.Text.ToString();
            try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT Ex_Date,Ex_Type,Vehicle_No,Person_Name,Ex_Amount,Ex_Description,Wages_Type FROM Tb_Expenses_Master WHERE Ex_Date = '" + date + "' AND Person_Name = '"+ name +"'", con);
                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    dgv_Expense_Master.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_Expense_Master.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();
            }
            catch { }
            finally { con.Close(); }
        }
        public void show_vehicle_by_Date()
        {
            String vehicle = cmb_Search_vehicle.Text.ToString();
            String date = dt_By_Date.Text.ToString();
            try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT Ex_Date,Ex_Type,Vehicle_No,Person_Name,Ex_Amount,Ex_Description,Wages_Type FROM Tb_Expenses_Master WHERE Ex_Date = '" + date + "' AND Vehicle_No = '" + vehicle + "'", con);
                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    dgv_Expense_Master.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_Expense_Master.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();
            }
            catch { }
            finally { con.Close(); }
        }
        public void show_wages_by_Date()
        {
            String name = cmb_Search_wages.Text.ToString();
            String date = dt_By_Date.Text.ToString();
            try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT Ex_Date,Ex_Type,Vehicle_No,Person_Name,Ex_Amount,Ex_Description,Wages_Type FROM Tb_Expenses_Master WHERE Ex_Date = '" + date + "' AND Wages_Type = '" + name + "'", con);
                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    dgv_Expense_Master.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_Expense_Master.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();
            }
            catch { }
            finally { con.Close(); }
        }
        public void show_wages_Btw_Date()
        {
            String start_date = dt_By_Date.Text.ToString();
            String end_date = dt_Btw_Date.Text.ToString();
            String name = cmb_Search_wages.Text.ToString();
            try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT Ex_Date,Ex_Type,Vehicle_No,Person_Name,Ex_Amount,Ex_Description,Wages_Type FROM Tb_Expenses_Master WHERE Ex_Date BETWEEN '" + start_date + "' AND '" + end_date + "' AND  Wages_Type= '" + name + "'", con);
                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    dgv_Expense_Master.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_Expense_Master.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();
            }
            catch { }
            finally { con.Close(); }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (btn_Save.Text == "Save")
            {
                if (rdb_Fuel.Checked == true)
                {
                    if (dt_Exp_Date.Text == "")
                    {
                        MessageBox.Show("Please Enter Date", "STOP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (txt_Exp_Amount.Text == "")
                    {
                        MessageBox.Show("Please Enter Amount", "STOP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (cmb_Exp_Description.Text == "")
                    {
                        MessageBox.Show("Please Enter Or Select Description", "STOP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (cmb_Person_Name.Text == "")
                    {
                        MessageBox.Show("Please Enter Or Select Person Name", "STOP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        if (cmb_Vehicle_No.Text == "")
                        {
                            MessageBox.Show("Please Enter Or Select Vehicle No", "STOP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            getconnection();
                            con.Open();
                            cmd = new SqlCommand("Insert_Expenses_Details", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@Ex_Date", dt_Exp_Date.Text.Trim());
                            cmd.Parameters.AddWithValue("@Ex_Amount", txt_Exp_Amount.Text.Trim());
                            cmd.Parameters.AddWithValue("@Ex_Description", cmb_Exp_Description.Text.Trim());
                            cmd.Parameters.AddWithValue("@Person_Name", cmb_Person_Name.Text.Trim());
                            cmd.Parameters.AddWithValue("@Vehicle_No", cmb_Vehicle_No.Text.Trim());
                            cmd.Parameters.AddWithValue("@Ex_Type", Exp_Type);
                            cmd.Parameters.AddWithValue("@Wages_Type", "NA");
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Data Saved Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            clearControl();
                            gridload();
                        }
                    }
                }
                else if (rdb_Other.Checked == true && btn_Save.Text == "Save")
                {
                    if (cmb_Vehicle_No.Text == "")
                    {
                        cmb_Vehicle_No.Text = "NA";
                    }
                    else if (txt_Exp_Amount.Text == "")
                    {
                        MessageBox.Show("Please Enter Amount", "STOP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        getconnection();
                        con.Open();
                        cmd = new SqlCommand("Insert_Expenses_Details", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Ex_Date", dt_Exp_Date.Text.Trim());
                        cmd.Parameters.AddWithValue("@Ex_Amount", txt_Exp_Amount.Text.Trim());
                        cmd.Parameters.AddWithValue("@Ex_Description", cmb_Exp_Description.Text.Trim());
                        cmd.Parameters.AddWithValue("@Person_Name", cmb_Person_Name.Text.Trim());
                        cmd.Parameters.AddWithValue("@Vehicle_No", cmb_Vehicle_No.Text.Trim());
                        cmd.Parameters.AddWithValue("@Ex_Type", Exp_Type);
                        cmd.Parameters.AddWithValue("@Wages_Type", "NA");
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Data Saved Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clearControl();
                        gridload();
                    }
                }
                else if (rdb_Wages.Checked == true && btn_Save.Text == "Save")
                {
                    if (cmb_Vehicle_No.Text == "")
                    {
                        cmb_Vehicle_No.Text = "NA";
                    }
                    else if (cmb_Wages_Type.Text == "")
                    {
                        MessageBox.Show("Please Select Wages Type", "STOP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (txt_Wages_Amount.Text == "")
                    {
                        MessageBox.Show("Please Select Amount", "STOP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (cmb_Wages_To.Text == "")
                    {
                        MessageBox.Show("Please Select Person Name", "STOP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        getconnection();
                        con.Open();
                        cmd = new SqlCommand("Insert_Expenses_Details", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Ex_Date", dt_Exp_Date.Text.Trim());
                        cmd.Parameters.AddWithValue("@Wages_Type", cmb_Wages_Type.Text.Trim());
                        cmd.Parameters.AddWithValue("@Ex_Amount", txt_Wages_Amount.Text.Trim());
                        cmd.Parameters.AddWithValue("@Ex_Description", txt_Wages_Description.Text.Trim());
                        cmd.Parameters.AddWithValue("@Person_Name", cmb_Wages_To.Text.Trim());
                        cmd.Parameters.AddWithValue("@Vehicle_No", cmb_Vehicle_No.Text.Trim());
                        cmd.Parameters.AddWithValue("@Ex_Type", Exp_Type);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Data Saved Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clearControl();
                        gridload();
                    }
                }
            }
            else if (btn_Save.Text == "Update")
            {
                if (rdb_Fuel.Checked == true || rdb_Other.Checked == true)
                {
                    if (cmb_Vehicle_No.Text == "")
                    {
                        MessageBox.Show("Please Enter Or Select Vehicle No", "STOP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        getconnection();
                        con.Open();
                        cmd = new SqlCommand("Update_Expenses_Details", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", lbl_id.Text.Trim());
                        cmd.Parameters.AddWithValue("@Ex_Date", dt_Exp_Date.Text.Trim());
                        cmd.Parameters.AddWithValue("@Ex_Amount", txt_Exp_Amount.Text.Trim());
                        cmd.Parameters.AddWithValue("@Ex_Description", cmb_Exp_Description.Text.Trim());
                        cmd.Parameters.AddWithValue("@Person_Name", cmb_Person_Name.Text.Trim());
                        cmd.Parameters.AddWithValue("@Vehicle_No", cmb_Vehicle_No.Text.Trim());
                        cmd.Parameters.AddWithValue("@Ex_Type", Exp_Type);
                        cmd.Parameters.AddWithValue("@Wages_Type", "NA");
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Data Updated Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clearControl();
                        gridload();
                    }
                }
                else if (rdb_Wages.Checked == true && btn_Save.Text == "Save")
                {
                    getconnection();
                    con.Open();
                    cmd = new SqlCommand("Update_Expenses_Details", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", lbl_id.Text.Trim());
                    cmd.Parameters.AddWithValue("@Ex_Date", dt_Exp_Date.Text.Trim());
                    cmd.Parameters.AddWithValue("@Wages_Type", cmb_Wages_Type.Text.Trim());
                    cmd.Parameters.AddWithValue("@Ex_Amount", txt_Wages_Amount.Text.Trim());
                    cmd.Parameters.AddWithValue("@Ex_Description", txt_Wages_Description.Text.Trim());
                    cmd.Parameters.AddWithValue("@Person_Name", cmb_Wages_To.Text.Trim());
                    cmd.Parameters.AddWithValue("@Vehicle_No", cmb_Vehicle_No.Text.Trim());
                    cmd.Parameters.AddWithValue("@Ex_Type", Exp_Type);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data Updated Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clearControl();
                    gridload();
                }
                btn_Save.Text = "Save";
            }
        }
        public void gridload()
        {
            try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT Exp_ID,Ex_Date,Ex_Type,Vehicle_No,Person_Name,Ex_Amount,Ex_Description,Wages_Type FROM Tb_Expenses_Master", con);
                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    dgv_Expense_Master.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_Expense_Master.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();
            }
            catch { }
            finally { con.Close(); }
            clearControl();
        }
        public void load_wages()
        {
            try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT Ex_Date,Ex_Type,Person_Name,Ex_Amount,Ex_Description,Wages_Type FROM Tb_Expenses_Master WHERE Ex_Type='WAGES'", con);
                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    dgv_Expense_Master.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_Expense_Master.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();
            }
            catch { }
            finally { con.Close(); }
        }
        public void load_Fuel()
        {
            try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT Ex_Date,Ex_Type,Vehicle_No,Person_Name,Ex_Amount,Ex_Description FROM Tb_Expenses_Master WHERE Ex_Type='FUEL'", con);
                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    dgv_Expense_Master.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_Expense_Master.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();
            }
            catch { }
            finally { con.Close(); }
        }
       
        private void dgv_Expense_Master_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dgv_Expense_Master.Rows[e.RowIndex].Cells[2].Value = (e.RowIndex + 1).ToString();
        }
        private void Expense_Master_Load(object sender, EventArgs e)
        {
            panel2.Hide();
            panel29.Hide();
            panel11.Hide();
            panel29.Size = new Size(309, 229);
            panel11.Hide();
            panel25.Hide();
            panel37.Hide();
            panel40.Hide();
            panel46.Hide();
            panel49.Hide();
            rdb_Name_By_Date.Enabled = false;
            rdb_Name_Btw_Date.Enabled = false;
            rdb_Vehicle_By_Date.Enabled = false;
            rdb_Vehicle_Btw_Date.Enabled = false;
            rdb_Wages_By_Date.Enabled = false;
            rdb_Wages_Btw_Date.Enabled = false;
            load_All_Combo();
        }
        private void load_All_Combo()
        {
            //try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT DISTINCT Vehicle_No FROM Tb_Expenses_Master", con);
                sdr = cmd.ExecuteReader();
                while(sdr.Read())
                {
                    cmb_Vehicle_No.Items.Add(sdr[0].ToString());
                }
                sdr.Close();
                cmd = new SqlCommand("SELECT DISTINCT Person_Name FROM Tb_Expenses_Master", con);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    cmb_Person_Name.Items.Add(sdr[0].ToString());
                    cmb_Wages_To.Items.Add(sdr[0].ToString());
                }
                sdr.Close();
                cmd = new SqlCommand("SELECT DISTINCT Ex_Description FROM Tb_Expenses_Master", con);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    cmb_Exp_Description.Items.Add(sdr[0].ToString());
                }
                sdr.Close();
                cmd = new SqlCommand("SELECT DISTINCT Wages_Type FROM Tb_Expenses_Master", con);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    cmb_Wages_Type.Items.Add(sdr[0].ToString());
                }
                sdr.Close();
            }
           // catch { }
        }
        private void button1_Click(object sender, EventArgs e)
        {
           if(btn_Search.Text=="Search")
            {
                panel2.Show();
                panel5.Hide();
                panel43.Hide();
                btn_Search.Text = "Cancel";
               // btn_Save.Text = "Update";
            }
           else
            {
                panel2.Show();
                panel5.Show();
                btn_Search.Text = "Search";
                btn_Save.Text = "Save";
                cmb_Search_Names.ResetText();
                cmb_Search_vehicle.ResetText();
                cmb_Search_wages.ResetText();
                cmb_Search_Fual_Expenses.ResetText();
            }
        }
        private void cmb_Exp_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }
        private void dgv_Expense_Master_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string value = dgv_Expense_Master.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            if (value == "Edit")
            {
                panel5.Show();
                if (btn_Save.Text == "Update")
                {
                    MessageBox.Show("Cannot Add Record While Another is in the Process", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    DataGridViewRow row = dgv_Expense_Master.Rows[e.RowIndex];

                    if(row.Cells["type"].Value.ToString() == "FUEL")
                    {
                        rdb_Fuel.Checked = true;
                        dt_Exp_Date.Text = row.Cells["date"].Value.ToString();
                        txt_Exp_Amount.Text = row.Cells["amount"].Value.ToString();
                        cmb_Exp_Description.Text = row.Cells["description"].Value.ToString();
                        cmb_Person_Name.Text = row.Cells["name"].Value.ToString();
                        cmb_Vehicle_No.Text = row.Cells["vehicle_no"].Value.ToString();
                        lbl_id.Text = row.Cells["id"].Value.ToString();
                        dgv_Expense_Master.Rows.RemoveAt(e.RowIndex);
                    }
                    else if (row.Cells["type"].Value.ToString() == "WAGES")
                    {
                        rdb_Wages.Checked = true;
                        dt_Exp_Date.Text = row.Cells["date"].Value.ToString();
                        cmb_Wages_Type.Text = row.Cells["wages_type"].Value.ToString();
                        txt_Wages_Amount.Text = row.Cells["amount"].Value.ToString();
                        txt_Wages_Description.Text = row.Cells["description"].Value.ToString();
                        cmb_Wages_To.Text = row.Cells["name"].Value.ToString();
                        lbl_id.Text = row.Cells["id"].Value.ToString();
                        dgv_Expense_Master.Rows.RemoveAt(e.RowIndex);
                    }
                    else if (row.Cells["type"].Value.ToString() == "OTHER")
                    {
                        rdb_Other.Checked = true;
                        dt_Exp_Date.Text = row.Cells["date"].Value.ToString();
                        txt_Exp_Amount.Text = row.Cells["amount"].Value.ToString();
                        cmb_Exp_Description.Text = row.Cells["description"].Value.ToString();
                        cmb_Person_Name.Text = row.Cells["name"].Value.ToString();
                        cmb_Vehicle_No.Text = row.Cells["vehicle_no"].Value.ToString();
                        lbl_id.Text = row.Cells["id"].Value.ToString();
                        dgv_Expense_Master.Rows.RemoveAt(e.RowIndex);
                    }
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
                    cmd = new SqlCommand("DELETE FROM Tb_Expenses_Master WHERE Exp_ID = " + Convert.ToInt64(dgv_Expense_Master.Rows[e.RowIndex].Cells["id"].Value), con);
                    cmd.ExecuteNonQuery();
                    dgv_Expense_Master.Rows.RemoveAt(e.RowIndex);
                }
            }
        }
        private void rdb_By_Date_CheckedChanged(object sender, EventArgs e)
        {
           
        }
        private void rdb_Btwn_Date_CheckedChanged(object sender, EventArgs e)
        {
            if(rdb_Other.Checked==true)
            {
                clearControl();
                panel29.Size = new Size(309, 229);
                panel29.Show();
                panel34.Hide();
                Exp_Type = "OTHER";
            }
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            clearControl();
            panel29.Size = new Size(309, 229);
            panel34.Show();
            panel29.Hide();
            Exp_Type = "WAGES";
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            clearControl();
            panel29.Size = new Size(309, 283);
            panel34.Hide();
            panel29.Show();
            Exp_Type = "FUEL";
        }
        private void panel23_Paint(object sender, PaintEventArgs e)
        {

        }
        private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
        {
            panel11.Show();
            panel40.Hide();
            panel43.Hide();
            load_Fuel();
            clearControl();
        }
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            clearControl();
            panel11.Hide();
             panel37.Hide();
            panel25.Hide();
            panel46.Hide();
            panel49.Hide();
            panel40.Hide();
            panel43.Hide();
        }
        private void radioButton2_CheckedChanged_1(object sender, EventArgs e)
        {
            clearControl();
            panel40.Show();
            panel43.Show();
            Load_Wages_Name();
            panel11.Hide();
            panel37.Hide();
            panel25.Hide();
            panel46.Hide();
            panel49.Hide();
            load_wages();
        }
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmb_Search_Fual_Expenses.Text== "BY NAME")
            {
                panel25.Show();
                panel46.Hide();
                panel49.Hide();
                panel37.Hide();
                Load_Name();
            }
            else if (cmb_Search_Fual_Expenses.Text == "BY VEHICLE NO")
            {
                panel25.Hide();
                panel37.Show();
                panel46.Hide();
                panel49.Hide();
                Load_Vehicle();
            }
            else if (cmb_Search_Fual_Expenses.Text == "BY DATE")
            {
                panel25.Hide();
                panel37.Hide();
                panel46.Show();
                panel49.Show();
            }
            else if (cmb_Search_Fual_Expenses.Text == "ALL")
            {
                getconnection();
                con.Open();
                string Name = cmb_Search_Names.Text.ToString();
                cmd = new SqlCommand("SELECT Ex_Date,Ex_Type,Vehicle_No,Person_Name,Ex_Amount,Ex_Description FROM Tb_Expenses_Master WHERE Ex_Type='FUEL'", con);
                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    dgv_Expense_Master.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_Expense_Master.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();
            }
        }
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_Search_Fual_Expenses.Text == "BY NAME")
            {
                getconnection();
                con.Open();
                string Name = cmb_Search_Names.Text.ToString();
                cmd = new SqlCommand("SELECT Ex_Date,Ex_Type,Vehicle_No,Person_Name,Ex_Amount,Ex_Description FROM Tb_Expenses_Master WHERE Person_Name='" + Name + "'", con);
                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    dgv_Expense_Master.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_Expense_Master.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();
            }
            rdb_Name_By_Date.Enabled = true;
            rdb_Name_Btw_Date.Enabled = true;
        }
        private void cmb_Search_vehicle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_Search_Fual_Expenses.Text == "BY VEHICLE NO")
            {
                getconnection();
                con.Open();
                string Vehicle = cmb_Search_vehicle.Text.ToString();
                cmd = new SqlCommand("SELECT Ex_Date,Ex_Type,Vehicle_No,Person_Name,Ex_Amount,Ex_Description FROM Tb_Expenses_Master WHERE Vehicle_No ='" + Vehicle + "'", con);
                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    dgv_Expense_Master.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_Expense_Master.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();
            }
            rdb_Vehicle_By_Date.Enabled = true;
            rdb_Vehicle_Btw_Date.Enabled = true;
        }
        private void cmb_Search_wages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_Search_wages.Text == "SALARY")
            {
                panel43.Show();
                getconnection();
                con.Open();
                string Salary = cmb_Search_wages.Text.ToString();
                cmd = new SqlCommand("SELECT Ex_Date,Ex_Type,Person_Name,Ex_Amount,Ex_Description,Wages_Type FROM Tb_Expenses_Master WHERE Wages_Type='" + Salary + "'", con);
                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    dgv_Expense_Master.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_Expense_Master.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();
            }
            if (cmb_Search_wages.Text == "ADVANCED")
            {
                getconnection();
                con.Open();
                string Advanced = cmb_Search_wages.Text.ToString();
                cmd = new SqlCommand("SELECT Ex_Date,Ex_Type,Person_Name,Ex_Amount,Ex_Description,Wages_Type FROM Tb_Expenses_Master WHERE Wages_Type='" + Advanced + "'", con);
                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    dgv_Expense_Master.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_Expense_Master.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();
            }
            if (cmb_Search_wages.Text == "CREDIT")
            {
                getconnection();
                con.Open();
                string Credit = cmb_Search_wages.Text.ToString();
                cmd = new SqlCommand("SELECT Ex_Date,Ex_Type,Person_Name,Ex_Amount,Ex_Description,Wages_Type FROM Tb_Expenses_Master WHERE Wages_Type='" +Credit + "'", con);
                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    dgv_Expense_Master.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_Expense_Master.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();
            }
            if (cmb_Search_wages.Text == "BONUS")
            {
                getconnection();
                con.Open();
                string Bonus = cmb_Search_wages.Text.ToString();
                cmd = new SqlCommand("SELECT Ex_Date,Ex_Type,Person_Name,Ex_Amount,Ex_Description,Wages_Type FROM Tb_Expenses_Master WHERE Wages_Type='" + Bonus + "'", con);
                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    dgv_Expense_Master.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_Expense_Master.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();
            }
            if (cmb_Search_wages.Text == "INCENTIVE")
            {
                getconnection();
                con.Open();
                string Incentive = cmb_Search_wages.Text.ToString();
                cmd = new SqlCommand("SELECT Ex_Date,Ex_Type,Person_Name,Ex_Amount,Ex_Description,Wages_Type FROM Tb_Expenses_Master WHERE Wages_Type='" + Incentive + "'", con);
                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    dgv_Expense_Master.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_Expense_Master.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();
            }
            if (cmb_Search_wages.Text == "ALL")
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT Ex_Date,Ex_Type,Person_Name,Ex_Amount,Ex_Description,Wages_Type FROM Tb_Expenses_Master WHERE Ex_Type = 'WAGES'", con);
                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    dgv_Expense_Master.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_Expense_Master.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();
            }
            rdb_Wages_By_Date.Enabled = true;
            rdb_Wages_Btw_Date.Enabled = true;
       }
        private void rdb_Name_By_Date_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_Name_By_Date.Checked == true)
            {
                panel46.Show();
                panel49.Hide();
                show_name_by_Date();
            }
            else
            {
                show_by_Date();
            }
        }
        private void dt_By_Date_ValueChanged(object sender, EventArgs e)
        {
            if (rdb_Name_By_Date.Checked == true)
            {
                show_name_by_Date();
            }
           else if(rdb_Vehicle_By_Date.Checked == true)
            {
                show_vehicle_by_Date();
            }
            else if(rdb_Wages_By_Date.Checked==true)
            {
                show_wages_by_Date();
            }
            else if(cmb_Search_Fual_Expenses.Text=="BY DATE")
            {
                show_by_Date();
            }
        }
        private void rdb_Name_Btw_Date_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_Name_Btw_Date.Checked == true)
            {
                panel49.Show();
                panel46.Show();
                show_Btw_Date_by_name();
            }
        }
        private void dt_Btw_Date_ValueChanged(object sender, EventArgs e)
        {
            if(rdb_Name_Btw_Date.Checked == true)
            {
                show_Btw_Date_by_name();
            }
            else if(rdb_Vehicle_Btw_Date.Checked == true)
            {
                show_Btw_Date_by_vehicle();
            }
            else if(rdb_Wages_Btw_Date.Checked==true)
            {
                show_wages_Btw_Date();
            }
        }
        private void rdb_Vehicle_By_Date_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_Vehicle_By_Date.Checked == true)
            {
                panel46.Show();
                panel49.Hide();
                show_vehicle_by_Date();
            }
        }
        private void rdb_Vehicle_Btw_Date_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_Vehicle_Btw_Date.Checked == true)
            {
                panel49.Show();
                panel46.Show();
                show_Btw_Date_by_vehicle();
            }
        }
        private void rdb_Wages_By_Date_CheckedChanged(object sender, EventArgs e)
        {
            if(rdb_Wages_By_Date.Checked==true)
            {
                panel46.Show();
                show_wages_by_Date();
            }
        }
        private void rdb_Wages_Btw_Date_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_Wages_Btw_Date.Checked == true)
            {
                panel46.Show();
                panel49.Show();
                show_wages_Btw_Date();
            }
        }
        private void cmb_Serch_Names_SelectedIndexChanged(object sender, EventArgs e)
        {
            {
                getconnection();
                con.Open();
                string Name = cmb_Search_Wages_Names.Text.ToString();
                cmd = new SqlCommand("SELECT Ex_Date,Ex_Type,Vehicle_No,Person_Name,Ex_Amount,Ex_Description,Wages_Type FROM Tb_Expenses_Master WHERE Person_Name='" + Name + "'", con);
                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    dgv_Expense_Master.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_Expense_Master.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();
            }
            cmb_Search_Wages_Names.ResetText();
        }
        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }
        private void txt_Exp_Amount_TextChanged(object sender, EventArgs e)
        {

        }
    }
 }