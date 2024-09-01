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
    public partial class Filling_Register : Form
    {
        SqlConnection con;
        SqlDataReader sdr;
        SqlCommand cmd, cmd_update;
        SqlDataAdapter adpt;
        DataTable dt;
        string cylinder_type, part_No, id_Month, id_Year, fdc_id;
        int in_Stock_Value, out_Stock_Value, fill_Id = 00;
        public Filling_Register()
        {
            InitializeComponent();
            //load_Fdc_Id();
            load_cmb_Supplier_name();
            //gridload_Show_All();
            load_Cylinder_Count();
            load_cylinder_type();
            load_Item_No();
            load_Fdc_Combo();
            load_Vehicle_Combo();
        }
        public void clearControl()
        {
            cmb_Supplier_Name.ResetText();
            txt_Company_Phone.Clear();
            txt_Contact_Person_Name.ResetText();
            txt_Address.Clear();
            txt_Gst_Number.Clear();
            txt_Contact_Person_Phone.Clear();
            lbl_Net_Amt.Text = "0";
            lbl_Grid_Total.Text = "0";
            lbl_Total.Text = "0";
            cmb_Item_No.ResetText();
            load_Cylinder_Count();
            cmb_Cylinder_Type.ResetText();
            cmb_Vehicle_No.ResetText();
            cmb_FDC_ID.ResetText();
            txt_Fdc_Id.Clear();
        }
        public void getconnection()
        {
            string str = ConfigurationManager.ConnectionStrings["CylinderData"].ConnectionString;
            con = new SqlConnection(str);
        }
        private void load_Vehicle_Combo()
        {
            try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT DISTINCT Vehicle_No FROM Tb_Fill_Master", con);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    cmb_Vehicle_No.Items.Add(sdr[0]);
                }
                sdr.Close();
            }
            catch { }
            finally { con.Close(); }
        }
        public void load_cmb_Supplier_name()
        {
            cmb_Supplier_Name.Items.Clear();
            //  try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT DISTINCT Supp_CompName FROM Tb_Supplier_Master WHERE Status = 'FILLING'", con);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    cmb_Supplier_Name.Items.Add(sdr[0]);
                }
                sdr.Close();
            }
            //  catch { }
            //  finally { con.Close(); }
        }

        public void load_Fdc_Combo()
        {
           // cmb_FDC_ID.Items.Clear();
           try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT DISTINCT Fill_ID FROM Tb_Fill_Master WHERE Cancel_status = 'NA'", con);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    if(!cmb_FDC_ID.Items.Contains(sdr[0]))
                    cmb_FDC_ID.Items.Add(sdr[0]);
                }
                sdr.Close();
            }
            catch(Exception ex) { throw ex; }
            finally { con.Close(); }
        }
        public void Load_Cylinder_View()
        {
            try
            {
                getconnection();
                con.Open();
                string Fill_ID = cmb_FDC_ID.Text.ToString();
                cmd = new SqlCommand("SELECT Item_No,Sr_No,Name,Rate from Tb_Fill_Content_Master WHERE Fill_ID='" + Fill_ID + "'", con);
                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    dgv_Show_Selected.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_Show_Selected.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();
            }
            catch { }
            finally { con.Close(); }
        }
        //LOAD CYLINDER COUNT TO RESPECTIVE LABELS
        public void load_Cylinder_Count()
        {
            try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT count(Cylinder_Status) from Tb_Purchase_Content_Master Where Cylinder_Status = 'FILL'", con);
                sdr = cmd.ExecuteReader();
                sdr.Read();
                lbl_Fill.Text = sdr[0].ToString();
                sdr.Close();
                cmd = new SqlCommand("SELECT count(Cylinder_Status) from Tb_Purchase_Content_Master Where Cylinder_Status = 'Scrap'", con);
                sdr = cmd.ExecuteReader();
                sdr.Read();
                lbl_Scrap.Text = sdr[0].ToString();
                sdr.Close();
                cmd = new SqlCommand("SELECT count(Cylinder_Status) from Tb_Purchase_Content_Master WHERE Purchase_Type = 'CYLINDER'", con);
                sdr = cmd.ExecuteReader();
                sdr.Read();
                lbl_Total.Text = sdr[0].ToString();
                sdr.Close();
                cmd = new SqlCommand("SELECT count(Cylinder_Status) from Tb_Purchase_Content_Master Where Cylinder_Status = 'EMPTY' AND Purchase_Type = 'CYLINDER'", con);
                sdr = cmd.ExecuteReader();
                sdr.Read();
                lbl_Empty.Text = sdr[0].ToString();
                sdr.Close();
            }
            catch { }
            finally { con.Close(); }
        }
        //LOAD DETAILS OF SUPPLIER IN RESPECTIVE TEXTBOXES
        public void load_supplier_details()
        {
            //try
            {
                getconnection();
                con.Open();
                string supp_Name = cmb_Supplier_Name.Text.ToString();
                cmd = new SqlCommand("SELECT Supp_Address, Supp_Phone, Supp_GST, Supp_ConPerson, Supp_CPersonNo FROM Tb_Supplier_Master WHERE Supp_CompName = '" + supp_Name + "'", con);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    txt_Address.Text = sdr[0].ToString();
                    txt_Company_Phone.Text = sdr[1].ToString();
                    txt_Gst_Number.Text = sdr[2].ToString();
                    txt_Contact_Person_Name.Text = sdr[3].ToString();
                    txt_Contact_Person_Phone.Text = sdr[4].ToString();
                }
                sdr.Close();
            }
            //catch { }
            //finally { con.Close(); }
        }
        public void Search_FDC_ID()
        {
            try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT Fill_Date, Supplier_Name, Cylinder_Count, Net_Amt,Vehicle_No FROM Tb_Fill_Master WHERE Fill_ID = '" + cmb_FDC_ID.Text.ToString() + "'", con);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    dt_DC_Date.Value = Convert.ToDateTime(sdr[0]);
                    cmb_Supplier_Name.Text = sdr[1].ToString();
                    lbl_Grid_Total.Text = sdr[2].ToString();
                    lbl_Net_Amt.Text = sdr[3].ToString();
                    cmb_Vehicle_No.Text = sdr[4].ToString();
                }
            }
            catch { }
            gridload_By_Fill_Id();
        }
        private void cmb_Supplier_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (btn_Save.Text == "Save")
            {
                gridload_Supp_Cy();
                load_supplier_details();
                load_cylinder_type();
                load_Item_No();
                if (cmb_Supplier_Name.Text == "")
                {
                    dgv_Show_All.Enabled = false;
                }
                else
                {
                    cmb_Item_No.Enabled = true;
                    cmb_Cylinder_Type.Enabled = true;
                    dgv_Show_All.Enabled = true;
                    load_cylinder_type();
                }
            }
            else if (btn_Save.Text == "Update")
            {
                gridload_Supp_Cy();
                load_supplier_details();
                load_cylinder_type();
                load_Item_No();
                cmb_Item_No.Enabled = true;
                cmb_Cylinder_Type.Enabled = true;
                dgv_Show_All.Enabled = true;
                load_supplier_details();
                load_Fdc_Id_By_Supplier();
            }
        }
        private void gridload_Supp_Cy()
        {
            //try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT  Part_No, Sr_No, P.Particulers, P.Cylinder_Type, S.Rate FROM Tb_Purchase_Content_Master P LEFT JOIN Tb_Supplier_Material_Master S ON S.Particulers = P.Particulers WHERE P.Cylinder_Status= 'EMPTY' AND S.Supp_Name = '" + cmb_Supplier_Name.Text + "'", con);
                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    dgv_Show_All.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_Show_All.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();
            }
            //catch { }
        }
        public void load_Fdc_Id_By_Supplier()
        {
            cmb_FDC_ID.Items.Clear();
            //try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT DISTINCT Fill_ID FROM Tb_Fill_Master WHERE Supplier_Name = '" + cmb_Supplier_Name.Text.ToString() + "' AND Cancel_status = 'NA'", con);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                   // if (!cmb_FDC_ID.Items.Contains(sdr[0]))
                        cmb_FDC_ID.Items.Add(sdr[0]);
                }
                sdr.Close();
            }
            //catch { }
            //finally { con.Close(); }
        }
        public void gridload_By_Fill_Id()
        {
            try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT Item_No, Sr_No, Rate, Name, Cy_Type FROM Tb_Fill_Content_Master WHERE Fill_ID = '" + cmb_FDC_ID.Text.ToString() + "'", con);
                adpt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    dgv_Show_Selected.DataSource = ds.Tables[0];
                }
                else
                {
                    dgv_Show_Selected.DataSource = ds.Tables[0];
                }
                adpt.Dispose();
                dt.Dispose();
            }
            catch { }
            finally { con.Close(); }
            foreach(DataGridViewRow row in dgv_Show_Selected.Rows)
            {
                row.Cells["new_"].Value = "old";
            }
        }
        public void load_cylinder_type()
        {
            cmb_Cylinder_Type.Items.Clear();
            //try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT DISTINCT Cylinder_Type FROM Tb_Inventory_Master WHERE Type = 'CYLINDER'", con);
                //cmd = new SqlCommand("SELECT P.Cylinder_Type Tb_Purchase_Content_Master P LEFT JOIN Tb_Supplier_Material_Master S ON S.Particulers = P.Particulers WHERE P.Cylinder_Status = 'EMPTY' AND S.Supp_Name = '" + cmb_Supplier_Name.Text + "'", con);
                //cmd = new SqlCommand("SELECT DISTINCT Cylinder_Type FROM Tb_Supplier_Material_Master WHERE Supp_Name = '" + cmb_Supplier_Name.Text + "'", con);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    cmb_Cylinder_Type.Items.Add(sdr[0]);
                }
                sdr.Close();
            }
           // catch { }
           // finally { con.Close(); }
        }
        private void cmb_Cylinder_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_Item_No();
            CurrencyManager cm = (CurrencyManager)BindingContext[dgv_Show_All.DataSource];
            cm.SuspendBinding();
            foreach (DataGridViewRow row in dgv_Show_All.Rows)
            {
                if (row.Cells["type"].Value.ToString() == cmb_Cylinder_Type.Text.ToString())
                {
                    row.Visible = true;
                }
                else
                {
                    row.Visible = false;
                }
                cm.ResumeBinding();
            }
            gridload_Supp_Cy();
        }

        public void load_Item_No()
        {
            cmb_Item_No.Text = "";
            cmb_Item_No.Items.Clear();
            try
            {
                getconnection();
                con.Open();
                string cylinder_type = cmb_Cylinder_Type.Text.ToString();
                if (cmb_Cylinder_Type.Text == "")
                {
                    //cmd = new SqlCommand("SELECT DISTINCT Part_No FROM Tb_Purchase_Content_Master WHERE Purchase_Type = 'Cylinder' AND Cylinder_Status = 'EMPTY'", con);
                  //  cmd = new SqlCommand("SELECT DISTINCT Part_No FROM Tb_Purchase_Content_Master WHERE Cylinder_Type IN (SELECT DISTINCT Cylinder_Type FROM Tb_Supplier_Material_Master WHERE Supp_Name = '" + cmb_Supplier_Name.Text + "') AND Cylinder_Status = 'EMPTY'", con);
                    cmd = new SqlCommand("SELECT DISTINCT A.Part_No FROM Tb_Purchase_Content_Master A RIGHT JOIN Tb_Supplier_Material_Master B ON A.Particulers  = B.Particulers where B.Supp_Name='" + cmb_Supplier_Name.Text + "' and A.Cylinder_Status = 'EMPTY'",con);
                }
                else
                {
                    //cmd = new SqlCommand("SELECT DISTINCT Part_No FROM Tb_Purchase_Content_Master WHERE Cylinder_Type = '" + cylinder_type + "' AND Cylinder_Status = 'EMPTY'", con);
                 //   cmd = new SqlCommand("SELECT DISTINCT Part_No FROM Tb_Purchase_Content_Master WHERE Cylinder_Type = '" + cmb_Cylinder_Type.Text + "' AND Cylinder_Status = 'EMPTY'", con);
                    cmd = new SqlCommand("SELECT DISTINCT A.Part_No FROM Tb_Purchase_Content_Master A RIGHT JOIN Tb_Supplier_Material_Master B ON A.Particulers  = B.Particulers where B.Supp_Name='" + cmb_Supplier_Name.Text + "' and A.Cylinder_Status = 'EMPTY' and A.Cylinder_Type = '" + cmb_Cylinder_Type.Text + "'", con);
                }
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    cmb_Item_No.Items.Add(sdr[0]);
                }
                sdr.Close();
            }
            catch { }
            finally { con.Close(); }
        }
        //public void gridload_Show_All()
        //{
        //    // CurrencyManager cm = (CurrencyManager)BindingContext[dgv_Show_All.DataSource];
        //    // cm.ResumeBinding();
        //    try
        //    {
        //        getconnection();
        //        con.Open();

        //        cylinder_type = cmb_Cylinder_Type.Text.ToString();
        //        if (cylinder_type == "")
        //        {
        //            cmd = new SqlCommand("SELECT  Part_No, Sr_No, Particulers, Rate, Cylinder_Type FROM Tb_Purchase_Content_Master WHERE Cylinder_Status = 'EMPTY' AND Purchase_Type = 'CYLINDER'", con);
        //        }
        //        else
        //        {
        //            cmd = new SqlCommand("SELECT  Part_No, Sr_No, Particulers, Rate, Cylinder_Type FROM Tb_Purchase_Content_Master WHERE  Cylinder_Type = '" + cylinder_type + "' AND Cylinder_Status = 'EMPTY'", con);
        //        }

        //        adpt = new SqlDataAdapter(cmd);
        //        DataSet ds = new DataSet();
        //        adpt.Fill(ds);
        //        dt = ds.Tables[0];

        //        if (dt.Rows.Count == 0)
        //        {
        //            dgv_Show_All.DataSource = ds.Tables[0];
        //        }
        //        else
        //        {
        //            dgv_Show_All.DataSource = ds.Tables[0];
        //        }
        //        adpt.Dispose();
        //        dt.Dispose();
        //    }
        //    catch { }
        //    finally { con.Close(); }
        //}
        private void cmb_Item_No_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrencyManager cm = (CurrencyManager)BindingContext[dgv_Show_All.DataSource];
            cm.SuspendBinding();
            for (int i = 1; i < dgv_Show_All.Rows.Count; i++)
            {
                if (dgv_Show_All.Rows[i].Cells["item_no"].Value.ToString() == cmb_Item_No.Text)
                {
                    dgv_Show_All.Rows[i].Visible = true;
                }
                else
                {
                    dgv_Show_All.Rows[i].Visible = false;
                }
                cm.ResumeBinding();
            }
        }
        private void dgv_Show_All_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dgv_Show_All.Rows[e.RowIndex].Cells[0].Value = (e.RowIndex + 1).ToString();
        }
        private void cmb_Item_No_TextChanged(object sender, EventArgs e)
        {
            CurrencyManager cm = (CurrencyManager)BindingContext[dgv_Show_All.DataSource];
            cm.SuspendBinding();
            string dgv_item, cmb_item;
            cmb_item = cmb_Item_No.Text.ToString();
            for (int i = 0; i < dgv_Show_All.Rows.Count; i++)
            {
                dgv_item = dgv_Show_All.Rows[i].Cells["item_no"].Value.ToString();
                if (dgv_item.StartsWith(cmb_item))
                {
                    dgv_Show_All.Rows[i].Visible = true;
                }
                else
                {
                    dgv_Show_All.Rows[i].Visible = false;
                }
            }
            cm.ResumeBinding();
        }
        private void dgv_Show_Selected_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dgv_Show_Selected.Rows[e.RowIndex].Cells["sr_col"].Value = (e.RowIndex + 1).ToString();
        }
        private void Filling_Register_Load(object sender, EventArgs e)
        {
            cmb_Supplier_Name.Enabled = false;
            panel28.Hide();
            btn_Print.Hide();
            panel14.Hide();
            panel12.Hide();
            panel44.Hide();
            chb_Add_New.Enabled = false;
            chk_Cancel.Enabled = false;
          //  panel11.Hide();
            dgv_Show_All.Enabled = false;
            cmb_Cylinder_Type.Enabled = false;
            cmb_Item_No.Enabled = false;
            txt_Company_Phone.Enabled = false;
            txt_Contact_Person_Name.Enabled = false;
            txt_Contact_Person_Phone.Enabled = false;
            txt_Address.Enabled = false;
            txt_Gst_Number.Enabled = false;
           // btn_Print.Hide();
            dgv_Show_All.Dock = DockStyle.Fill;
        }
       
        private void btn_Search_FDC_Click(object sender, EventArgs e)
        {

        }
        private void txt_Serach_FDC_TextChanged(object sender, EventArgs e)
        {
            Search_FDC_ID();
        }
        private void chb_Add_New_CheckedChanged(object sender, EventArgs e)
        {
            if (chb_Add_New.Checked == true)
            {
                panel22.Show();
                btn_Add.Text= "Update";
                panel29.Show();
                panel28.Show();
                panel39.Show();
                panel44.Hide();
                cmb_FDC_ID.Enabled = false;
            }
            else
            {
                panel22.Hide();
                panel28.Hide();
                panel29.Hide();
                panel39.Hide();
                panel44.Show();
                btn_Add.Text= "Select";
            }
        }
        private void panel11_Paint(object sender, PaintEventArgs e)
        {

        }
        private void dgv_Show_All_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ////if (e.ColumnIndex == dgv_Show_All.Columns["button"].Index)
            ////{
            //    if (btn_Save.Text == "Save")
            //    {
            //        DataGridViewRow row = dgv_Show_All.Rows[e.RowIndex];
            //        bool val = true;

            //        //if (Convert.ToBoolean(row.Cells["chkbox_Add"].Value))
            //        row.Cells["chkbox_Add"].Value = true;
            //        {
            //            foreach (DataGridViewRow row_select in dgv_Show_Selected.Rows)
            //            {
            //                if (row_select.Cells["item"].Value.ToString() == row.Cells["item_no"].Value.ToString())
            //                {
            //                    val = false;
            //                }
            //            }
            //            if (val)
            //            {
            //                var index = dgv_Show_Selected.Rows.Add();
            //                dgv_Show_Selected.Rows[index].Cells["item"].Value = row.Cells["item_no"].Value;
            //                dgv_Show_Selected.Rows[index].Cells["sr"].Value = row.Cells["serial_no"].Value;
            //                dgv_Show_Selected.Rows[index].Cells["name"].Value = row.Cells["Name_"].Value;
            //                dgv_Show_Selected.Rows[index].Cells["rate"].Value = row.Cells["rate_"].Value;
            //                dgv_Show_Selected.Rows[index].Cells["new_"].Value = "new";
            //            }
            //        }
            //    }
            //    else if (btn_Save.Text == "Update")
            //    {
            //        DataGridViewRow row = dgv_Show_All.Rows[e.RowIndex];
            //        bool val = true;

            //        //if (Convert.ToBoolean(row.Cells["chkbox_Add"].Value))
            //        row.Cells["chkbox_Add"].Value = true;
            //        {
            //            foreach (DataGridViewRow row_select in dgv_Show_Selected.Rows)
            //            {
            //                if (row_select.Cells["item"].Value == row.Cells["item_no"].Value)
            //                {
            //                    val = false;
            //                }
            //            }
            //            if (val)
            //            {
            //                DataTable table = new DataTable();
            //                table = (DataTable)dgv_Show_Selected.DataSource;
            //                DataRow rw = table.NewRow();
            //                rw["Sr_No"] = row.Cells["serial_no"].Value.ToString();
            //                rw["Item_No"] = row.Cells["item_no"].Value.ToString();
            //                rw["Rate"] = row.Cells["rate_"].Value.ToString();
            //                rw["Name"] = row.Cells["Name_"].Value.ToString();
            //                if(!table.Columns.Contains("new_"))
            //                {
            //                    table.Columns.Add("new_");
            //                }
            //                rw["new_"] = "new";
            //                table.Rows.Add(rw);
            //                table.AcceptChanges();
            //            }
            //        }
            //    }
            //}
            //update_lbl_Total();
        }
        private void cmb_Serach_ID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmb_FDC_ID.Text !="")
            {
                txt_Fdc_Id.Text = cmb_FDC_ID.Text;
                Search_FDC_ID();
                btn_Add.Text = "Update";
                chb_Add_New.Enabled = true;
                chk_Cancel.Enabled = true;
                lbl_Fdc_Id_Print.Text = cmb_FDC_ID.Text;
            }
            else
            {
                txt_Fdc_Id.Text = "";
                chb_Add_New.Enabled =false;
                chk_Cancel.Enabled = false;
            }
        }
        private void label15_Click(object sender, EventArgs e)
        {
           
        }
        private void panel33_Paint(object sender, PaintEventArgs e)
        {

        }
        private void panel34_Paint(object sender, PaintEventArgs e)
        {

        }
        private void txt_Contact_Person_Name_TextChanged(object sender, EventArgs e)
        {

        }
        private void lblGstNumber_Click(object sender, EventArgs e)
        {

        }
        private void lblAddress_Click(object sender, EventArgs e)
        {

        }
        private void lblContactPersonPhone_Click(object sender, EventArgs e)
        {

        }
        private void lblContactPersonName_Click(object sender, EventArgs e)
        {

        }
        private void lblCompanyPhoneNumber_Click(object sender, EventArgs e)
        {

        }
        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void lblSupplierInDate_Click(object sender, EventArgs e)
        {

        }
        private void label14_Click(object sender, EventArgs e)
        {

        }
        private void panel43_Paint(object sender, PaintEventArgs e)
        {

        }
        private void panel10_Paint(object sender, PaintEventArgs e)
        {

        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        private void dt_In_Date_ValueChanged(object sender, EventArgs e)
        {
            
        }
        private void txt_Gst_Number_TextChanged(object sender, EventArgs e)
        {

        }
        private void txt_Contact_Person_Phone_TextChanged(object sender, EventArgs e)
        {

        }
        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }
        private void panel11_Paint_1(object sender, PaintEventArgs e)
        {

        }
        private void panel28_Paint(object sender, PaintEventArgs e)
        {

        }
        private void panel23_Paint(object sender, PaintEventArgs e)
        {

        }
        private void label10_Click(object sender, EventArgs e)
        {

        }
        private void label3_Click(object sender, EventArgs e)
        {

        }
        private void panel28_Paint_1(object sender, PaintEventArgs e)
        {

        }
        private void panel14_Paint(object sender, PaintEventArgs e)
        {

        }
        private void panel12_Paint(object sender, PaintEventArgs e)
        {

        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void panel28_Paint_2(object sender, PaintEventArgs e)
        {

        }
        private void label12_Click(object sender, EventArgs e)
        {

        }
        private void panel26_Paint(object sender, PaintEventArgs e)
        {

        }
        private void panel38_Paint(object sender, PaintEventArgs e)
        {

        }
        private void panel44_Paint(object sender, PaintEventArgs e)
        {

        }
        private void chk_Cancel_CheckedChanged(object sender, EventArgs e)
        {
            if(chk_Cancel.Checked == true)
            {
                DialogResult result = MessageBox.Show("Are You Sure?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if(result == DialogResult.Yes)
                {
                    getconnection();
                    con.Open();
                   cmd = new SqlCommand("update Tb_Fill_Master set Cancel_status ='Cancel' WHERE Fill_ID = '" + cmb_FDC_ID.Text.ToString() + "'", con);
                    cmd.ExecuteNonQuery();
                    clearControl();
                    update_stock_on_Cancel();
                    MessageBox.Show("FDC Cancelled Successfully.!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    chk_Cancel.Checked = false;
                }
            }
        }
        private void update_stock_on_Cancel()
        {
          //  try
            {
                getconnection();
                con.Open();
                foreach(DataGridViewRow row in dgv_Show_Selected.Rows)
                {
                    cmd = new SqlCommand("UPDATE Tb_Purchase_Content_Master SET Cylinder_Status ='EMPTY', Cust_Supp_Name = 'NA' WHERE Part_No = '" + row.Cells["item"].Value.ToString() + "'", con);
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("UPDATE Tb_Inventory_Master SET In_Stock = In_Stock + 1 , Out_Stock = Out_Stock - 1 WHERE Particulars = (SELECT Particulers FROM Tb_Purchase_Content_Master WHERE Part_No = '" + row.Cells["item"].Value.ToString() + "')", con);
                    cmd.ExecuteNonQuery();
                }
            }
          //  catch { }
        }
        private void btn_Search_Click(object sender, EventArgs e)
        {
            if (btn_Search.Text == "Search FDC")
            {
                txt_Fdc_Id.Hide();
                panel28.Hide();
                panel22.Hide();
                panel44.Show();
                panel39.Hide();
                panel29.Hide();
                cmb_FDC_ID.Show();
                panel14.Show();
                clearControl();
                txt_Address.Enabled = false;
                txt_Company_Phone.Enabled = false;
                txt_Contact_Person_Phone.Enabled = false;
                // txt_Fdc_Id.Enabled = false;
                txt_Gst_Number.Enabled = false;
                //cmb_Supplier_Name.Enabled = false;
                txt_Contact_Person_Name.Enabled = false;
                dt_DC_Date.Enabled = false;
                btn_Search.Text = "Cancel";
                panel12.Show();
                panel25.Dock = DockStyle.Fill;
                panel25.Show();
                btn_Save.Text = "Update";
                chb_Add_New.Enabled = false;
                chk_Cancel.Enabled = false;
                load_Fdc_Combo();
                if (dgv_Show_Selected.RowCount > 0)
                {
                    dgv_Show_Selected.Rows.Clear();
                }
            }
            else
            {
                lbl_Fdc_Id_Print.Text = txt_Fdc_Id.Text;
                panel39.Show();
                panel28.Show();
                panel44.Hide();
                panel12.Hide();
                panel29.Show();
                panel22.Show();
                dt_DC_Date.Enabled = true;
                cmb_FDC_ID.ResetText();
                btn_Search.Text = "Search FDC";
                btn_Save.Text = "Save";
                btn_Add.Text = "Select";
                panel14.Hide();
          //      load_Fdc_Id();
                txt_Fdc_Id.Show();
                cmb_FDC_ID.Hide();
         /*       txt_Address.Enabled = true;
                dt_DC_Date.Enabled = true;
                txt_Company_Phone.Enabled = true;
                txt_Contact_Person_Name.Enabled = true;
                txt_Company_Phone.Enabled = true;
                txt_Contact_Person_Phone.Enabled = true;
                // txt_Fdc_Id.Enabled = true;
                txt_Gst_Number.Enabled = true;*/
                cmb_Supplier_Name.Enabled = true;
                clearControl();
                //gridload_Show_All();
                gridload_Supp_Cy();
                if (dgv_Show_Selected.RowCount > 0)
                {
                    DataTable DT = (DataTable)dgv_Show_Selected.DataSource;
                    if (DT != null)
                        DT.Clear();
                }
            }
        }
        private void panel9_Paint_1(object sender, PaintEventArgs e)
        {

        }
        private void panel43_Paint_1(object sender, PaintEventArgs e)
        {

        }
        private void cmb_Cylinder_Type_TextChanged(object sender, EventArgs e)
        {
            // gridload_Show_All();
            gridload_Supp_Cy();
        }
        private void txt_Fdc_Id_TextChanged(object sender, EventArgs e)
        {
            if(txt_Fdc_Id.Text=="")
            {
                cmb_Supplier_Name.Enabled = false;
            }
            else
            {
                cmb_Supplier_Name.Enabled = true;
            }
        }
        public void add_to_filling()
        {
            if (btn_Save.Text == "Save")
            {
                dt = new DataTable();
                bool val;
                foreach (DataGridViewRow row in dgv_Show_All.Rows)
                {
                    val = true;
                    if (Convert.ToBoolean(row.Cells["chkbox_Add"].Value))
                    {
                        for (int i = 0; i < dgv_Show_Selected.RowCount; i++)
                        {
                            if (dgv_Show_Selected.Rows[i].Cells["item"].Value.ToString() == row.Cells["item_no"].Value.ToString())
                            {
                                val = false;
                            }
                        }
                        if (val)
                        {
                            var index = dgv_Show_Selected.Rows.Add();
                            dgv_Show_Selected.Rows[index].Cells["item"].Value = row.Cells["item_no"].Value;
                            dgv_Show_Selected.Rows[index].Cells["sr"].Value = row.Cells["serial_no"].Value;
                            dgv_Show_Selected.Rows[index].Cells["name"].Value = row.Cells["Name_"].Value;
                            dgv_Show_Selected.Rows[index].Cells["rate"].Value = row.Cells["rate_"].Value;
                            dgv_Show_Selected.Rows[index].Cells["type_"].Value = row.Cells["type"].Value;
                            dgv_Show_Selected.Rows[index].Cells["new_"].Value = "new";
                        }
                    }
                }
            }
            else if (btn_Save.Text == "Update")
            {
                foreach (DataGridViewRow dgvrow in dgv_Show_All.Rows)
                {
                    DataGridViewRow row = dgvrow;
                    bool val = true;
                    if (Convert.ToBoolean(row.Cells["chkbox_Add"].Value))
                    {
                        foreach (DataGridViewRow row_select in dgv_Show_Selected.Rows)
                        {
                            if (row_select.Cells["item"].Value == row.Cells["item_no"].Value)
                            {
                                val = false;
                            }
                        }
                        if (val)
                        {
                            DataTable table = new DataTable();
                            table = (DataTable)dgv_Show_Selected.DataSource;
                            set_Table(table);
                            DataRow rw = table.NewRow();
                            rw["Sr_No"] = row.Cells["serial_no"].Value.ToString();
                            rw["Item_No"] = row.Cells["item_no"].Value.ToString();
                            rw["Cy_Type"] = row.Cells["type"].Value.ToString();
                            rw["Name"] = row.Cells["Name_"].Value.ToString();
                            rw["Rate"] = row.Cells["rate_"].Value.ToString();
                            rw["new_"] = "new";
                            table.Rows.Add(rw);
                            table.AcceptChanges();
                        }
                    }
                }
            }
            update_lbl_Total();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            add_to_filling();
            btn_Save.Enabled = true;
        }
        private void btn_Print_Click(object sender, EventArgs e)
        {
            if(btn_Save.Text == "Save")
            {
                Report_Filling_DC form = new Report_Filling_DC();
                form.get_ID(lbl_Fdc_Id_Print.Text);
                form.Show();
                gridload_Supp_Cy();
                dgv_Show_Selected.Rows.Clear();
           //     load_Fdc_Id();
                clearControl();
            }
            else
            {
                Report_Filling_DC form = new Report_Filling_DC();
                form.get_ID(lbl_Fdc_Id_Print.Text);
                form.Show();
                if (dgv_Show_Selected.RowCount > 0)
                {
                    DataTable DT = (DataTable)dgv_Show_Selected.DataSource;
                    if (DT != null)
                        DT.Clear();
                }
                gridload_Supp_Cy();
                cmb_FDC_ID.Enabled = true;
                clearControl();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if(button2.Text=="Search By Id")
            {
                panel28.Show();
                button2.Text = "Cancel Search";
            }
            else
            {
                panel28.Hide();
                button2.Text = "Search By Id";
            }
        }
        private void cmb_Vehicle_No_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lbl_Grid_Total_Click(object sender, EventArgs e)
        {

        }

        private void txt_Fdc_Id_Leave(object sender, EventArgs e)
        {
            getconnection();
            con.Open();
            cmd = new SqlCommand("SELECT Fill_ID FROM Tb_Fill_Master WHERE Fill_ID='" + txt_Fdc_Id.Text + "'", con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                MessageBox.Show("FDC ID Already Exist");
            }
        }
        private void btn_Save_Click(object sender, EventArgs e)
        {
            if(btn_Save.Text == "Save")
            {
                if (cmb_Supplier_Name.Text == "")
                {
                   
                    MessageBox.Show("Please Select Values.!", "" , MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                }
                else if (cmb_Vehicle_No.Text == "")
                {
                    
                    MessageBox.Show("Please Enter Vehicle Number.!", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    
                }
                else if (lbl_Grid_Total.Text == "0")
                {
                    
                    MessageBox.Show("Please Select Cylinder.!", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                }
                else
                {
                    //if (cmb_Cylinder_Type.Text == "")
                    try
                    {
                        btn_Save.Enabled = true;
                        calculate_Fill_Id();
                        getconnection();
                        con.Open();
                        cmd = new SqlCommand("Insert_Fill_Details", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", fill_Id);
                        cmd.Parameters.AddWithValue("@Fill_ID", txt_Fdc_Id.Text.ToString());
                        cmd.Parameters.AddWithValue("@Fill_Date", dt_DC_Date.Text.ToString());
                        cmd.Parameters.AddWithValue("@Supplier_Name", cmb_Supplier_Name.Text.Trim());
                        cmd.Parameters.AddWithValue("@Cylinder_Count", lbl_Grid_Total.Text.ToString());
                        cmd.Parameters.AddWithValue("@Net_Amt", lbl_Net_Amt.Text.ToString());
                        cmd.Parameters.AddWithValue("@cancel_status", "NA");
                        cmd.Parameters.AddWithValue("@paid_amt", "0");
                        cmd.Parameters.AddWithValue("@balance_amt", lbl_Net_Amt.Text.ToString());
                        cmd.Parameters.AddWithValue("@paid_by", "NA");
                        cmd.Parameters.AddWithValue("@reference", "NA");
                        cmd.Parameters.AddWithValue("@supp_invoice_no", "NA");
                        cmd.Parameters.AddWithValue("@Vehicle_No", cmb_Vehicle_No.Text.ToString());
                        cmd.ExecuteNonQuery();
                        foreach (DataGridViewRow row in dgv_Show_Selected.Rows)
                        {
                            cmd = new SqlCommand("Insert_Fill_Content", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@Fill_ID", txt_Fdc_Id.Text.ToString());
                            cmd.Parameters.AddWithValue("@Item_No", row.Cells["item"].Value.ToString());
                            cmd.Parameters.AddWithValue("@Sr_No", row.Cells["sr"].Value.ToString());
                            cmd.Parameters.AddWithValue("@Name", row.Cells["name"].Value.ToString());
                            cmd.Parameters.AddWithValue("@Rate", row.Cells["rate"].Value.ToString());
                            cmd.Parameters.AddWithValue("@Cy_Type", row.Cells["type_"].Value.ToString());
                            cmd.Parameters.AddWithValue("@Status", "SENT TO SUPPLIER");
                            cmd.Parameters.AddWithValue("@supp_Dc_No", "NA");
                            cmd.Parameters.AddWithValue("@received_Date", "NA");
                            cmd.ExecuteNonQuery();
                            cmd_update = new SqlCommand("update Tb_Purchase_Content_Master set Cylinder_Status = 'SENT TO FILLING SUPPLIER', Cust_Supp_Name = '" + cmb_Supplier_Name.Text + "' Where Part_No = '" + row.Cells["item"].Value.ToString() + "'", con);
                            cmd_update.ExecuteNonQuery();
                            string particular = row.Cells["name"].Value.ToString();
                            cmd = new SqlCommand("UPDATE Tb_Inventory_Master SET In_Stock = In_Stock +-1 , Out_Stock = Out_Stock + 1 WHERE Particulars = '" + particular + "'", con);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch { }
                    finally { con.Close(); }
                    // DialogResult result = MessageBox.Show("Data Saved SuccessFully. Do You Want to Print DC?", "Success", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    MessageBox.Show("Data Saved Successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //if (result == DialogResult.Yes)
                   // {
                     //   Report_Filling_DC form = new Report_Filling_DC();
                      //  form.get_ID(lbl_Fdc_Id_Print.Text);
                      //  form.Show();
                      //  gridload_Supp_Cy();
                       // dgv_Show_Selected.Rows.Clear();
                        //  load_Fdc_Id();
                     //   clearControl();
                  //  }
                  //  else
                  //  {
                        clearControl();
                        gridload_Supp_Cy();
                        dgv_Show_Selected.Rows.Clear();
                        //load_Fdc_Id();
                   // }
                    //gridload_Show_All();  
                }
            }
            else if(btn_Save.Text == "Update")
            {
                try
                {
                    getconnection();
                    con.Open();
                    cmd = new SqlCommand("Update_Fill_Details", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Fill_ID", cmb_FDC_ID.Text.ToString());
                    cmd.Parameters.AddWithValue("@Fill_Date", dt_DC_Date.Text.ToString());
                    cmd.Parameters.AddWithValue("@count", lbl_Grid_Total.Text.ToString());
                    cmd.Parameters.AddWithValue("@Net_Amt", lbl_Net_Amt.Text.ToString());
                    cmd.Parameters.AddWithValue("@paid_amt", "0");
                    cmd.Parameters.AddWithValue("@balance_amt", lbl_Net_Amt.Text.ToString());
                    cmd.Parameters.AddWithValue("@paid_by", "NA");
                    cmd.Parameters.AddWithValue("@reference", "NA");
                    cmd.Parameters.AddWithValue("@supp_invoice_no", "NA");
                    cmd.Parameters.AddWithValue("@Vehicle_No", cmb_Vehicle_No.Text.ToString());
                    cmd.ExecuteNonQuery();
                    foreach (DataGridViewRow row in dgv_Show_Selected.Rows)
                    {
                        if(row.Cells["new_"].Value == "new")
                        {
                            cmd = new SqlCommand("Insert_Fill_Content", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@Fill_ID", txt_Fdc_Id.Text.ToString());
                            cmd.Parameters.AddWithValue("@Item_No", row.Cells["item"].Value.ToString());
                            cmd.Parameters.AddWithValue("@Sr_No", row.Cells["sr"].Value.ToString());
                            cmd.Parameters.AddWithValue("@Name", row.Cells["name"].Value.ToString());
                            cmd.Parameters.AddWithValue("@Rate", row.Cells["rate"].Value.ToString());
                            cmd.Parameters.AddWithValue("@Cy_Type", row.Cells["type_"].Value.ToString());
                            cmd.Parameters.AddWithValue("@Status", "SENT TO SUPPLIER");
                            cmd.Parameters.AddWithValue("@supp_Dc_No", "NA");
                            cmd.Parameters.AddWithValue("@received_Date", "NA");
                            cmd.ExecuteNonQuery();
                            cmd_update = new SqlCommand("update Tb_Purchase_Content_Master set Cylinder_Status = 'SENT TO FILLING SUPPLIER' , Cust_Supp_Name = '" + cmb_Supplier_Name.Text + "' Where Part_No = '" + row.Cells["item"].Value.ToString() + "'", con);
                            cmd_update.ExecuteNonQuery();
                            cmd = new SqlCommand("UPDATE Tb_Inventory_Master SET In_Stock = In_Stock - 1, Out_Stock = Out_Stock + 1  WHERE Particulars = '" + row.Cells["name"].Value.ToString() + "'", con);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                catch { }
                finally { con.Close(); }
                DialogResult result = MessageBox.Show("Data Updated SuccessFully. Do You Want to Print DC?", "Success", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    Report_Filling_DC form = new Report_Filling_DC();
                    form.get_ID(lbl_Fdc_Id_Print.Text);
                    form.Show();
                    if (dgv_Show_Selected.RowCount > 0)
                    {
                        DataTable DT = (DataTable)dgv_Show_Selected.DataSource;
                        if (DT != null)
                            DT.Clear();
                    }
                    gridload_Supp_Cy();
                    cmb_FDC_ID.Enabled = true;
                    clearControl();
                }
                else
                {
                    if (dgv_Show_Selected.RowCount > 0)
                    {
                        DataTable DT = (DataTable)dgv_Show_Selected.DataSource;
                        if (DT != null)
                            DT.Clear();
                    }
                    gridload_Supp_Cy();
                    cmb_FDC_ID.Enabled = true;
                    btn_Print.Show();
                    btn_Save.Enabled = true;
                    clearControl();
                }
            }
        }
        public void calculate_In_Stock_Value(string particular)
        {
            {
                getconnection();
                con.Open();
                SqlCommand command = new SqlCommand("SELECT In_Stock, Out_Stock FROM Tb_Inventory_Master WHERE Particulars = '" + particular + "'", con);
                sdr = command.ExecuteReader();
                while (sdr.Read())
                {
                    in_Stock_Value = Convert.ToInt32(sdr[0]);
                    out_Stock_Value = Convert.ToInt32(sdr[1]);
                }
                sdr.Close();
            }
        }
        private void btn_Add_Click(object sender, EventArgs e)
        {
                add_to_filling();
        }
        public void set_Table(DataTable tb)
        {
            if (tb.Columns.Contains("new_"))
            { return; }
            else
            { tb.Columns.Add("new_"); }
        }
        private void dgv_Show_Selected_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (btn_Save.Text == "Save")
            {
                if (dgv_Show_Selected.Columns["x"].Index == e.ColumnIndex)
                {
                    dgv_Show_Selected.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    if(Convert.ToBoolean(dgv_Show_Selected.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) == true)
                    {
                        DialogResult result = MessageBox.Show("Are You Sure?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                        if (result == DialogResult.Yes)
                        {
                            foreach (DataGridViewRow row in dgv_Show_All.Rows)
                            {
                                if (row.Cells["item_no"].Value.ToString() == dgv_Show_Selected.Rows[e.RowIndex].Cells["item"].Value.ToString())
                                {
                                    if (Convert.ToBoolean(row.Cells["chkbox_Add"].Value))
                                    {
                                        row.Cells["chkbox_Add"].Value = false;
                                    }
                                }
                            }
                            dgv_Show_Selected.Rows.RemoveAt(e.RowIndex);
                            update_lbl_Total();
                        }
                        else
                        {
                            dgv_Show_Selected.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = false;
                        }
                    }
                }
            }
            else if (btn_Save.Text == "Update")
            {
                //string value = dgv_Show_Selected.Rows[e.RowIndex].Cells["x"].Value.ToString();
                if (dgv_Show_Selected.Columns["x"].Index == e.ColumnIndex)
                {
                    dgv_Show_Selected.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    if(Convert.ToBoolean(dgv_Show_Selected.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) == true)
                    {
                        DialogResult result = MessageBox.Show("Are You Sure?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                        if (result == DialogResult.Yes)
                        {
                            //try
                            {
                                getconnection();
                                con.Open();
                                cmd = new SqlCommand("UPDATE Tb_Purchase_Content_Master SET Cylinder_Status = 'EMPTY', Cust_Supp_Name = 'NA' WHERE Part_No = '" + dgv_Show_Selected.Rows[e.RowIndex].Cells["item"].Value.ToString() + "'", con);
                                cmd.ExecuteNonQuery();
                                cmd = new SqlCommand("UPDATE Tb_Inventory_Master SET In_Stock = In_Stock + 1, Out_Stock = Out_Stock -1 WHERE Particulars = (SELECT Particulers FROM Tb_Purchase_Content_Master WHERE Part_No = '" + dgv_Show_Selected.Rows[e.RowIndex].Cells["item"].Value.ToString() + "')", con);
                                cmd.ExecuteNonQuery();
                                cmd = new SqlCommand("DELETE FROM Tb_Fill_Content_Master WHERE Fill_ID = '" + cmb_FDC_ID.Text.ToString() + "' AND Item_No = '" + dgv_Show_Selected.Rows[e.RowIndex].Cells["item"].Value.ToString() + "'", con);
                                cmd.ExecuteNonQuery();
                            }
                            //catch { }
                            dgv_Show_Selected.Rows.RemoveAt(e.RowIndex);
                            update_lbl_Total();
                            cmb_FDC_ID.Enabled = false;
                        }
                        else
                        {
                            dgv_Show_Selected.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = false;
                        }
                    }
                }
            }
            update_lbl_Total();
        }
        public void calculate_Fill_Id()
        {
            //try
            {
                getconnection();
                con.Open();
                cmd = new SqlCommand("SELECT MAX(ID) FROM Tb_Fill_Master", con);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    string val = sdr[0].ToString();
                    if (val == "")
                    {
                        fill_Id = 1;
                    }
                    else
                    {
                        fill_Id = (Convert.ToInt32(val) + 1);
                    }
                }
            }
           // catch { }
        }
        public void update_lbl_Total()
        {
            lbl_Grid_Total.Text = dgv_Show_Selected.RowCount.ToString();
            int net_Amt = 0;
            foreach (DataGridViewRow row in dgv_Show_Selected.Rows)
            {
                net_Amt = net_Amt + Convert.ToInt32(row.Cells["rate"].Value);
            }
            lbl_Net_Amt.Text = net_Amt.ToString();
        }
        //public void load_Fdc_Id()
        //{
        //    calculate_Fill_Id();
        //    id_Month = DateTime.Now.ToString("MMM");
        //    id_Year = DateTime.Now.ToString("yyyy");
        //    fdc_id = id_Month+"-"+id_Year+"-"+fill_Id.ToString();
        //    txt_Fdc_Id.Text = fdc_id;
        //    lbl_Fdc_Id_Print.Text = fdc_id;
        //}
    }
}

/*CurrencyManager currencyManager1 = (CurrencyManager)BindingContext[MyGrid.DataSource];
currencyManager1.SuspendBinding();
MyGrid.Rows[5].Visible = false;
currencyManager1.ResumeBinding();*/