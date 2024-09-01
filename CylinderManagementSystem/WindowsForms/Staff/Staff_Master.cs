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
    public partial class Staff_Master : Form
    {
        SqlCommand cmd;
        SqlDataReader sdr;
        SqlConnection con;
        DataTable dt;
        SqlDataAdapter adpt;
        public decimal salary, Per_Day, Per_Hour, Work_Days, Work_Hours;
        string status = "ACTIVE";
        public void getconnection()
        {
            string str = ConfigurationManager.ConnectionStrings["CylinderData"].ConnectionString;
            con = new SqlConnection(str);
        }
        public Staff_Master()
        {
            InitializeComponent();
            maxId();
            gridload();
            Load_Designation();
            Load_Bank();
            dt_Job_Resignation_Date.Text = "";
        }
        public void clearcontrols()
        {
            txt_Name.Clear();
            txt_Address.Clear();
            txt_Phone_No.Clear();
            txt_Mobile_No.Clear();
            txt_Aadhaar_No.Clear();
            txt_Emergency_Contact.Clear();
            cmb_Designation.ResetText();
            txt_Current_Age.Clear();
            txt_Acc_No.Clear();
            txt_IFSC.Clear();
            cmb_Bank.ResetText();
            txt_Monthly_Salary.Clear();
            txt_Working_Hours.Clear();
            txt_working_Min.Clear();
            txt_PerHour.Clear();
            txt_PerDay.Clear();
            txt_Reason.Clear();
            txt_Total_Wdays.Clear();
            dt_Joining_Date.ResetText();
            dt_Date_of_Birth.ResetText();
        }
        public void Load_Designation()
        {
            try
            {
                getconnection();
                con.Open();

                cmd = new SqlCommand("SELECT DISTINCT St_Designation FROM Tb_Staff_Details", con);
                sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    cmb_Designation.Items.Add(sdr[0]);
                }
                sdr.Close();
            }
            catch { }
            finally { con.Close(); }
        }
        public void Load_Bank()
        {
            try
            {
                getconnection();
                con.Open();

                cmd = new SqlCommand("SELECT DISTINCT Id FROM Tb_Staff_Details", con);
                sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    txt_Staff_Search.Items.Add(sdr[0]);
                }
                sdr.Close();
            }
            catch { }
            finally { con.Close(); }
        }
        public void load_id()
        {
            try
            {
                getconnection();
                con.Open();

                cmd = new SqlCommand("SELECT DISTINCT St_ID FROM Tb_Staff_Details", con);
                sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    txt_Staff_Search.Items.Add(sdr[0]);
                }
                sdr.Close();
            }
            catch { }
            finally { con.Close(); }
        }
        public void gridload()
        {
            try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT * from Tb_Staff_Details", con);
                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];

                if (dt.Rows.Count == 0)
                {
                    dgv_Staff_Master.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_Staff_Master.DataSource = ds.Tables[0];
                }
            }
            catch { }
            finally { con.Close(); }
        }
        public void maxId()
        {
            getconnection();
            con.Open();
            cmd = new SqlCommand("SELECT MAX(Staff_ID) FROM Tb_Staff_Details", con);
            sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                string val = sdr[0].ToString();
                if (val == "")
                {
                    txt_Staff_ID.Text = "1";
                }
                else
                {
                    int p = Convert.ToInt32(val);
                    p += 1;
                    txt_Staff_ID.Text = p.ToString();
                }
            }
        }
        public void Search_ID()
        {
            try
            {
                getconnection();
                con.Open();
                string Staff_Cell_ID = txt_Staff_Search.Text.ToString();
                cmd = new SqlCommand("SELECT St_Name,St_Address,St_Phone,St_Mobile,St_Aadhar_No,St_EmContact,St_JoinDate,St_Designation,Date_of_Birth,Account_No,IFSC,Bank,Current_Age from Tb_Staff_Details WHERE Staff_ID='" + Staff_Cell_ID+"'", con);
            }
            catch { }
            finally { con.Close(); }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txt_Name.Text == "")
            {
                MessageBox.Show("Please Enter Name", "STOP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txt_Address.Text == "")
            {
                MessageBox.Show("Please Enter Address", "STOP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txt_Mobile_No.Text.Length !=10)
            {
                MessageBox.Show("Please Enter  Valid Mobile No", "STOP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txt_Aadhaar_No.Text.Length !=12)
            {
                MessageBox.Show("Please Enter  Valid Aadhar Number", "STOP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (cmb_Designation.Text == "")
            {
                MessageBox.Show("Please Select Designation", "STOP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txt_Acc_No.Text == "")
            {
                MessageBox.Show("Please Enter Account No", "STOP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (cmb_Bank.Text == "")
            {
                MessageBox.Show("Please Select Bank", "STOP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txt_IFSC.Text == "")
            {
                MessageBox.Show("Please Enter IFSC Code", "STOP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (txt_Monthly_Salary.Text == "")
                {
                    txt_Monthly_Salary.Text = "NA";
                }
                if (txt_Working_Hours.Text == "")
                {
                    txt_Working_Hours.Text = "NA";
                }
                if (txt_Reason.Text == "")
                {
                    txt_Reason.Text = "NA";
                }
                if (txt_PerDay.Text == "")
                {
                    txt_PerDay.Text = "NA";
                }
                if (txt_PerHour.Text == "")
                {
                    txt_PerHour.Text = "NA";
                }
                if (txt_Phone_No.Text == "")
                {
                    txt_Phone_No.Text = "NA";
                }
                if (txt_Emergency_Contact.Text == "")
                {
                    txt_Emergency_Contact.Text = "NA";
                }
                //try
                {
                    if(btn_Save.Text=="Save")
                    {
                        getconnection();
                        con.Open();
                        cmd = new SqlCommand("Insert_Staff_Details", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Staff_ID", txt_Staff_ID.Text.Trim());
                        cmd.Parameters.AddWithValue("@St_Name", txt_Name.Text.Trim());
                        cmd.Parameters.AddWithValue("@St_Address", txt_Address.Text.Trim());
                        cmd.Parameters.AddWithValue("@St_Phone", txt_Phone_No.Text.Trim());
                        cmd.Parameters.AddWithValue("@St_Mobile", txt_Mobile_No.Text.Trim());
                        cmd.Parameters.AddWithValue("@St_Aadhar_No", txt_Aadhaar_No.Text.Trim());
                        cmd.Parameters.AddWithValue("@StEmContact", txt_Emergency_Contact.Text.Trim());
                        cmd.Parameters.AddWithValue("@St_JoinDate", dt_Joining_Date.Text.Trim());
                        cmd.Parameters.AddWithValue("@St_Designation", cmb_Designation.Text.Trim());
                        cmd.Parameters.AddWithValue("@St_Salary", txt_Monthly_Salary.Text.Trim());
                        cmd.Parameters.AddWithValue("@w_days", txt_Total_Wdays.Text.Trim());
                        cmd.Parameters.AddWithValue("@St_WHours", txt_Working_Hours.Text.Trim());
                        cmd.Parameters.AddWithValue("@w_min", txt_working_Min.Text.Trim());
                        cmd.Parameters.AddWithValue("@Per_Day_Salary", txt_PerDay.Text.Trim());
                        cmd.Parameters.AddWithValue("@Per_Hour_Salary", txt_PerHour.Text.Trim());
                        if(status == "DEACTIVE")
                        {
                            cmd.Parameters.AddWithValue("@St_ResignDate", dt_Job_Resignation_Date.Text);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@St_ResignDate", "NA");
                        }
                        cmd.Parameters.AddWithValue("@St_Reason", txt_Reason.Text.Trim());
                        cmd.Parameters.AddWithValue("@Date_of_Birth", dt_Date_of_Birth.Text.Trim());
                        cmd.Parameters.AddWithValue("@Account_No", txt_Acc_No.Text.Trim());
                        cmd.Parameters.AddWithValue("@IFSC", txt_IFSC.Text.Trim());
                        cmd.Parameters.AddWithValue("@Bank", cmb_Bank.Text.Trim());
                        cmd.Parameters.AddWithValue("@Current_Age", txt_Current_Age.Text.Trim());
                        cmd.Parameters.AddWithValue("@status", status);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Data Saved Successfully", "Information status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clearcontrols();
                        maxId();
                        gridload();
                    }
                    else if(btn_Save.Text=="Update")
                    {
                        getconnection();
                        con.Open();
                        cmd = new SqlCommand("Update_Staff_Details", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Staff_ID", txt_Staff_Search.Text.Trim());
                        cmd.Parameters.AddWithValue("@St_Name", txt_Name.Text.Trim());
                        cmd.Parameters.AddWithValue("@St_Address", txt_Address.Text.Trim());
                        cmd.Parameters.AddWithValue("@St_Phone", txt_Phone_No.Text.Trim());
                        cmd.Parameters.AddWithValue("@St_Mobile", txt_Mobile_No.Text.Trim());
                        cmd.Parameters.AddWithValue("@St_Aadhar_No", txt_Aadhaar_No.Text.Trim());
                        cmd.Parameters.AddWithValue("@StEmContact", txt_Emergency_Contact.Text.Trim());
                        cmd.Parameters.AddWithValue("@St_JoinDate", dt_Joining_Date.Text.Trim());
                        cmd.Parameters.AddWithValue("@St_Designation", cmb_Designation.Text.Trim());
                        cmd.Parameters.AddWithValue("@St_Salary", txt_Monthly_Salary.Text.Trim());
                        cmd.Parameters.AddWithValue("@w_days", txt_Total_Wdays.Text.Trim());
                        cmd.Parameters.AddWithValue("@St_WHours", txt_Working_Hours.Text.Trim());
                        cmd.Parameters.AddWithValue("@w_min", txt_working_Min.Text.Trim());
                        cmd.Parameters.AddWithValue("@Per_Day_Salary", txt_PerDay.Text.Trim());
                        cmd.Parameters.AddWithValue("@Per_Hour_Salary", txt_PerHour.Text.Trim());
                        //cmd.Parameters.AddWithValue("@St_ResignDate", dt_Job_Resignation_Date.Text.Trim());
                        if (status == "DEACTIVE")
                        {
                            cmd.Parameters.AddWithValue("@St_ResignDate", dt_Job_Resignation_Date.Text);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@St_ResignDate", "NA");
                        }
                        if (txt_Reason.Text == "")
                        {
                            txt_Reason.Text = "NA";
                        }
                        cmd.Parameters.AddWithValue("@St_Reason", txt_Reason.Text.Trim());
                        cmd.Parameters.AddWithValue("@Date_of_Birth", dt_Date_of_Birth.Text.Trim());
                        cmd.Parameters.AddWithValue("@Account_No", txt_Acc_No.Text.Trim());
                        cmd.Parameters.AddWithValue("@IFSC", txt_IFSC.Text.Trim());
                        cmd.Parameters.AddWithValue("@Bank", cmb_Bank.Text.Trim());
                        cmd.Parameters.AddWithValue("@Current_Age", txt_Current_Age.Text.Trim());
                        cmd.Parameters.AddWithValue("@status", status);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Data Updated Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clearcontrols();
                        gridload();
                        txt_Staff_Search.ResetText();
                    }
                }
               //catch
                {
                    //MessageBox.Show("ERROR");
                }
             //finally
                {
                    con.Close();
                }
            }
        }
        private void lblReason_Click(object sender, EventArgs e)
        {

        }
        private void lblJobResignationDate_Click(object sender, EventArgs e)
        {

        }
        private void lblWorkingHours_Click(object sender, EventArgs e)
        {

        }
        private void lblMonthlySalary_Click(object sender, EventArgs e)
        {

        }
        private void lblDesignation_Click(object sender, EventArgs e)
        {

        }
        private void lblJoiningDate_Click(object sender, EventArgs e)
        {

        }
        private void lblEmergencyContact_Click(object sender, EventArgs e)
        {

        }
        private void lblAadhaarNumber_Click(object sender, EventArgs e)
        {

        }
        private void txtReason_TextChanged(object sender, EventArgs e)
        {

        }
        private void lblMobile_Click(object sender, EventArgs e)
        {

        }
        private void lblPhone_Click(object sender, EventArgs e)
        {

        }
        private void lblAddress_Click(object sender, EventArgs e)
        {

        }
        private void lblStaffName_Click(object sender, EventArgs e)
        {

        }
        private void txtStaffID_TextChanged(object sender, EventArgs e)
        {

        }
        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }
        private void txtAddress_TextChanged(object sender, EventArgs e)
        {

        }
        private void txtPhone_TextChanged(object sender, EventArgs e)
        {

        }
        private void txtMobile_TextChanged(object sender, EventArgs e)
        {

        }
        private void txtAadhaarNumber_TextChanged(object sender, EventArgs e)
        {
           
        }
        private void txtEmergencyContact_TextChanged(object sender, EventArgs e)
        {

        }
        private void dtJoiningDate_ValueChanged(object sender, EventArgs e)
        {

        }
        private void txtDesignation_TextChanged(object sender, EventArgs e)
        {

        }
        private void txtMonthlySalary_TextChanged(object sender, EventArgs e)
        {
            if(txt_Monthly_Salary.Text != "" && txt_Total_Wdays.Text != "")
            {
                calculate_per_Day_Sal();
            }
        }
        private void txt_Total_Wdays_TextChanged(object sender, EventArgs e)
        {
            if (txt_Monthly_Salary.Text != "" && txt_Total_Wdays.Text != "")
            {
                calculate_per_Day_Sal();
            }
        }
        private void txtWorkingHours_TextChanged(object sender, EventArgs e)
        {
            if (txt_Working_Hours.Text != "" && txt_PerDay.Text != "" && txt_working_Min.Text != "")
            {
                calculate_per_Hour_Sal();
            }
        }
        private void calculate_per_Day_Sal()
        {
            decimal month, W_day;
           month = Convert.ToDecimal(txt_Monthly_Salary.Text);
            W_day = Convert.ToDecimal(txt_Total_Wdays.Text);

            txt_PerDay.Text = (month / (W_day*5)).ToString(".00");
        }
        private void calculate_per_Hour_Sal()
        {
            decimal day, w_hour, hour, min;
            day = Convert.ToDecimal(txt_PerDay.Text);
            hour = Convert.ToDecimal(txt_Working_Hours.Text);
            min = Convert.ToDecimal(txt_working_Min.Text);
            w_hour = Convert.ToDecimal(hour + (min / 60));

            txt_PerHour.Text = (day / w_hour).ToString(".00");         
        }
        private void dtJobResignationDate_ValueChanged(object sender, EventArgs e)
        {

        }
        private void lblStaffID_Click(object sender, EventArgs e)
        {

        }
        private void tbStaffDetailsBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }
        public void update_data_grid(object sender, DataGridViewCellEventArgs e)
        {
            int limit = dgv_Staff_Master.RowCount;
            int index = e.RowIndex, temp;
            for (int i = index; i < limit; i++)
            {
                temp = Convert.ToInt32(dgv_Staff_Master.Rows[i].Cells[2].Value);
                dgv_Staff_Master.Rows[i].Cells[0].Value = (temp - 1).ToString();
            }
        }
        private void dgv_Staff_Master_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dgv_Staff_Master.Rows[e.RowIndex].Cells["Sr_No"].Value = (e.RowIndex + 1).ToString();
        }

        private void dgv_Staff_Master_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string value = dgv_Staff_Master.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

            if (value == "EDIT")
            {
                if(btn_Save.Text=="Update")
                {
                    MessageBox.Show("You Can Update Only One Record At a Time", "STOP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    DataGridViewRow row = dgv_Staff_Master.Rows[e.RowIndex];
                    txt_Staff_ID.Text = row.Cells["sid"].Value.ToString();
                    txt_Name.Text = row.Cells["Name"].Value.ToString();
                    txt_Address.Text = row.Cells["Address"].Value.ToString();
                    txt_Phone_No.Text = row.Cells["Phone"].Value.ToString();
                    txt_Mobile_No.Text = row.Cells["Mobile"].Value.ToString();
                    txt_Aadhaar_No.Text = row.Cells["Aadhar"].Value.ToString();
                    txt_Emergency_Contact.Text = row.Cells["HomeCont"].Value.ToString();
                    dt_Date_of_Birth.Text = row.Cells["DOB"].Value.ToString();
                    txt_Acc_No.Text = row.Cells["AccNo"].Value.ToString();
                    txt_IFSC.Text = row.Cells["IFSC"].Value.ToString();
                    cmb_Bank.Text = row.Cells["Bank"].Value.ToString();
                    dt_Joining_Date.Text = row.Cells["JoinDate"].Value.ToString();
                    cmb_Designation.Text = row.Cells["Designation"].Value.ToString();
                    dt_Job_Resignation_Date.Text = row.Cells["ResignDate"].Value.ToString();
                    txt_Reason.Text = row.Cells["Reason"].Value.ToString();
                    dgv_Staff_Master.Rows.RemoveAt(e.RowIndex);
                    update_data_grid(sender, e);
                    btn_Save.Text = "Update";
                }
            }
            else if (value == "X")
            {
                DialogResult result = MessageBox.Show("Are You Sure.?", "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    dgv_Staff_Master.Rows.RemoveAt(e.RowIndex);
                    update_data_grid(sender, e);
                }
            }
        }
        private void Staff_Master_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            load_id();
            if (txt_Name.Text == "")
            {
                dt_Joining_Date.Enabled = false;
                dt_Date_of_Birth.Enabled = false;
                txt_Address.Enabled = false;
                txt_Phone_No.Enabled = false;
                txt_Mobile_No.Enabled = false;
                txt_Aadhaar_No.Enabled = false;
                txt_Emergency_Contact.Enabled = false;
                cmb_Designation.Enabled = false;
                txt_Current_Age.Enabled = false;
                txt_Acc_No.Enabled = false;
                txt_IFSC.Enabled = false;
                cmb_Bank.Enabled = false;
                txt_Monthly_Salary.Enabled = false;
                txt_Working_Hours.Enabled = false;
                txt_working_Min.Enabled = false;
                txt_PerHour.Enabled = false;
                txt_PerDay.Enabled = false;
                txt_Reason.Enabled = false;
                txt_Total_Wdays.Enabled = false;
                panel11.Hide();
                panel22.Hide();
            }
            else
            {
                txt_Name.Enabled = false;
                dt_Joining_Date.Enabled = true;
                dt_Date_of_Birth.Enabled = true;
                txt_Address.Enabled = true;
                txt_Phone_No.Enabled = true;
                txt_Mobile_No.Enabled = true;
                txt_Aadhaar_No.Enabled = true;
                txt_Emergency_Contact.Enabled = true;
                cmb_Designation.Enabled = true;
                txt_Current_Age.Enabled = true;
                txt_Acc_No.Enabled = true;
                txt_IFSC.Enabled = true;
                cmb_Bank.Enabled = true;
                txt_Monthly_Salary.Enabled = true;
                txt_Working_Hours.Enabled = true;
                txt_working_Min.Enabled = true;
                txt_PerHour.Enabled = true;
                txt_PerDay.Enabled = true;
                txt_Reason.Enabled = true;
                txt_Total_Wdays.Enabled = true;
            }
            Search_ID();
        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }
        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }
        private void Search_Click(object sender, EventArgs e)
        {

        }
        private void lbl_Staff_Search_Click(object sender, EventArgs e)
        {
          
        }
        private static int CalculateAge(DateTime dateofBirth)
        {
            int age = 0;
            age = DateTime.Now.Year - dateofBirth.Year;
            if (DateTime.Now.DayOfYear < dateofBirth.DayOfYear)
                age = age - 1;
            return age;
        }
        private void txt_Current_Age_TextChanged(object sender, EventArgs e)
        {

        }
        private void txt_Monthly_Salary_Leave(object sender, EventArgs e)
        {
            
        }
        private void txt_PerHour_Leave(object sender, EventArgs e)
        {
           
        }
        private void button1_Click(object sender, EventArgs e)
        {
         //   Autosal();
        }
        private void txt_Aadhaar_No_Leave(object sender, EventArgs e)
        {
            try
            {
                getconnection();
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from Tb_Staff_Details where St_Aadhar_No = @St_Aadhar_No", con);
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@St_Aadhar_No";
                param.Value = txt_Aadhaar_No.Text;
                cmd.Parameters.Add(param);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    MessageBox.Show("Duplicate Record!!!");
                }
            }
            catch { }
            finally { con.Close(); }
        }
        private void txt_PerDay_TextChanged(object sender, EventArgs e)
        {
            if (txt_Working_Hours.Text != "" && txt_PerDay.Text != "" && txt_working_Min.Text != "")
            {
                calculate_per_Hour_Sal();
            }
        }
        private void txt_working_Min_TextChanged(object sender, EventArgs e)
        {
            if (txt_Working_Hours.Text != "" && txt_PerDay.Text != "" && txt_working_Min.Text != "")
            {
                calculate_per_Hour_Sal();
            }
        }
        private void btn_Status_Click(object sender, EventArgs e)
        {
            if(btn_Status.Text == "DEACTIVE")
            {
                DialogResult result = MessageBox.Show("Are You Sure.?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if(result == DialogResult.Yes)
                {
                    status = "DEACTIVE";
                    btn_Save.PerformClick();
                }
            }
            else if(btn_Status.Text == "ACTIVE")
            {
                DialogResult result = MessageBox.Show("Are You Sure.?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    status = "ACTIVE";
                    btn_Save.PerformClick();
                }
            }
        }
        private void _TextChanged(object sender, EventArgs e)
        {
            //if(txt_Staff_ID.Text=="")
            //{
            //    panel7.Show();
            //}
            //else
            //{
            //    panel7.Hide();
            //}
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (btn_Search.Text == "Search")
            {
                txt_Name.Enabled = false;
                panel11.Show();
                panel7.Hide();
                txt_Staff_Search.Focus();
                btn_Status.Text = "DEACTIVE";
                btn_Save.Text = "Update";
                txt_Staff_Search.ResetText();
            }
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            txt_Name.Enabled = true;
            panel7.Show();
            panel11.Hide();
            btn_Status.Text = "ACTIVE";
            btn_Save.Text = "Save";
            panel22.Hide();
            clearcontrols();
        }
        private void txt_Name_TextChanged(object sender, EventArgs e)
        {
            if(txt_Name.Text== "")
            {
                dt_Joining_Date.Enabled = false;
                dt_Date_of_Birth.Enabled = false;
                txt_Address.Enabled = false;
                txt_Phone_No.Enabled = false;
                txt_Mobile_No.Enabled = false;
                txt_Aadhaar_No.Enabled = false;
                txt_Emergency_Contact.Enabled = false;
                cmb_Designation.Enabled = false;
                txt_Current_Age.Enabled = false;
                txt_Acc_No.Enabled = false;
                txt_IFSC.Enabled = false;
                cmb_Bank.Enabled = false;
                txt_Monthly_Salary.Enabled = false;
                txt_Working_Hours.Enabled = false;
                txt_working_Min.Enabled = false;
                txt_PerHour.Enabled = false;
                txt_PerDay.Enabled = false;
                txt_Reason.Enabled = false;
                txt_Total_Wdays.Enabled = false;
            }
            else
            {
                dt_Joining_Date.Enabled = true;
                dt_Date_of_Birth.Enabled = true;
                txt_Address.Enabled = true;
                txt_Phone_No.Enabled = true;
                txt_Mobile_No.Enabled = true;
                txt_Aadhaar_No.Enabled = true;
                txt_Emergency_Contact.Enabled = true;
                cmb_Designation.Enabled = true;
                txt_Current_Age.Enabled = true;
                txt_Acc_No.Enabled = true;
                txt_IFSC.Enabled = true;
                cmb_Bank.Enabled = true;
                txt_Monthly_Salary.Enabled = true;
                txt_Working_Hours.Enabled = true;
                txt_working_Min.Enabled = true;
                txt_PerHour.Enabled = true;
                txt_PerDay.Enabled = true;
                txt_Reason.Enabled = true;
                txt_Total_Wdays.Enabled = true;
            }
        }
        private void txt_Name_TextChanged_1(object sender, EventArgs e)
        {
            if (txt_Name.Text == "")
            {
                dt_Joining_Date.Enabled = false;
                dt_Date_of_Birth.Enabled = false;
                txt_Address.Enabled = false;
                txt_Phone_No.Enabled = false;
                txt_Mobile_No.Enabled = false;
                txt_Aadhaar_No.Enabled = false;
                txt_Emergency_Contact.Enabled = false;
                cmb_Designation.Enabled = false;
                txt_Current_Age.Enabled = false;
                txt_Acc_No.Enabled = false;
                txt_IFSC.Enabled = false;
                cmb_Bank.Enabled = false;
                txt_Monthly_Salary.Enabled = false;
                txt_Working_Hours.Enabled = false;
                txt_working_Min.Enabled = false;
                txt_PerHour.Enabled = false;
                txt_PerDay.Enabled = false;
                txt_Reason.Enabled = false;
                txt_Total_Wdays.Enabled = false;
            }
            else
            {
                dt_Joining_Date.Enabled = true;
                dt_Date_of_Birth.Enabled = true;
                txt_Address.Enabled = true;
                txt_Phone_No.Enabled = true;
                txt_Mobile_No.Enabled = true;
                txt_Aadhaar_No.Enabled = true;
                txt_Emergency_Contact.Enabled = true;
                cmb_Designation.Enabled = true;
                txt_Current_Age.Enabled = true;
                txt_Acc_No.Enabled = true;
                txt_IFSC.Enabled = true;
                cmb_Bank.Enabled = true;
                txt_Monthly_Salary.Enabled = true;
                txt_Working_Hours.Enabled = true;
                txt_working_Min.Enabled = true;
                txt_PerHour.Enabled = true;
                txt_PerDay.Enabled = true;
                txt_Reason.Enabled = true;
                txt_Total_Wdays.Enabled = true;
            }
        }
        private void dt_Date_of_Birth_ValueChanged_1(object sender, EventArgs e)
        {
            DateTime Today = dt_Date_of_Birth.Value;
            int age = CalculateAge(Today);
            txt_Current_Age.Text = age.ToString();
        }
        private void txt_Staff_ID_TextChanged(object sender, EventArgs e)
        {

        }
        private void dt_Date_of_Birth_ValueChanged(object sender, EventArgs e)
        {
           
        }

        private void txt_Mobile_No_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_Staff_Search_SelectedIndexChanged(object sender, EventArgs e)
        {
            Staff_Data();
        }

        private void txt_Serach_Staff_TextChanged(object sender, EventArgs e)
        {
            
        }
        public void Staff_Data()
        {
            //try
            {
                getconnection();
                con.Open();

                string Staff_ID = txt_Staff_Search.Text.ToString();
                cmd = new SqlCommand("SELECT St_Name,St_Address,St_Phone,St_Mobile,St_Aadhar_No,St_EmContact,St_JoinDate,St_Designation,St_Salary,St_WHours,Date_of_Birth,Account_No,IFSC,Bank,Current_Age,Per_Day_Salary, Per_Hour_Salary, St_WDays, St_WMin, Staff_Status FROM Tb_Staff_Details  WHERE Staff_ID = '" + Staff_ID + "'", con);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    txt_Name.Text = sdr[0].ToString();
                    txt_Address.Text = sdr[1].ToString();
                    txt_Phone_No.Text = sdr[2].ToString();
                    txt_Mobile_No.Text = sdr[3].ToString();
                    txt_Aadhaar_No.Text = sdr[4].ToString();
                    txt_Emergency_Contact.Text = sdr[5].ToString();
                    dt_Joining_Date.Text = sdr[6].ToString();
                    cmb_Designation.Text = sdr[7].ToString();
                    txt_Monthly_Salary.Text = sdr[8].ToString();
                    txt_Working_Hours.Text = sdr[9].ToString();
                    dt_Date_of_Birth.Text = sdr[10].ToString();
                    txt_Acc_No.Text = sdr[11].ToString();
                    txt_IFSC.Text = sdr[12].ToString();
                    cmb_Bank.Text = sdr[13].ToString();
                    txt_Current_Age.Text = sdr[14].ToString();
                    txt_PerDay.Text = sdr[15].ToString();
                    txt_PerHour.Text = sdr[16].ToString();
                    txt_Total_Wdays.Text = sdr[17].ToString();
                    txt_working_Min.Text = sdr[18].ToString();
                    if(sdr[19].ToString() == "ACTIVE")
                    {
                        btn_Status.Text = "DEACTIVE";
                    }
                    else if(sdr[19].ToString() == "DEACTIVE")
                    {
                        btn_Status.Text = "ACTIVE";
                    }
                }
                sdr.Close();
                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];

                if (dt.Rows.Count == 0)
                {
                    dgv_Staff_Master.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_Staff_Master.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();
              
            }
            //catch { }
            //finally { con.Close(); }
        }
        private void txt_Staff_Search_TextChanged(object sender, EventArgs e)
        {
            Staff_Data();
            panel22.Show();
        }
        public void search_from_Details(string id)
        {
            txt_Staff_Search.Text = id;
        }
    }
}
