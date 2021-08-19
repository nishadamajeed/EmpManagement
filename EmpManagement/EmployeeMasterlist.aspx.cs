using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Security.Cryptography;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using System.Globalization;

namespace EmpManagement
{
    public partial class EmployeeMasterlist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            try
            {
                ScriptManager scriptManager__1 = ScriptManager.GetCurrent(this.Page);
                scriptManager__1.RegisterPostBackControl(tcPopEmp. FindControl("tbpDocuments").FindControl("btnUploadDoc"));
                scriptManager__1.RegisterPostBackControl(gdvEmployeesDocuments);
            }
            catch
            {
            }

            if (IsPostBack == false)
            {
                mvMain.ActiveViewIndex = 0;
                sett_empManagement();
            }
        }

        private void sett_empManagement()
        {
            ddlSearchIn.SelectedValue = "";
            txtSearchKey.Text = "";
            load_employee_masterlist(true);
        }

        protected void btnLoadEmp_Click(object sender, EventArgs e)
        {
            load_employee_masterlist(false);
        }

        private void load_employee_masterlist(bool isDummy)
        {
            DataSet ds = new DataSet();
            if (isDummy == false)
            {
                DAL dc = new DAL();
                ds = dc.Get_Employees(ddlSearchIn.SelectedValue, txtSearchKey.Text.Trim());
                dc = null;
            }
            if (ds.Tables.Count > 0)
            {
                gdvEmployees.DataSource = ds.Tables[0];
                gdvEmployees.DataBind();
            }
            else
            {
                gdvEmployees.DataSource = null;
                gdvEmployees.DataBind();
            }

            gdvEmployees_Empty.DataSource = null;
            gdvEmployees_Empty.DataBind();
            ds = null;

            if (gdvEmployees.Rows.Count == 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Clear();
                dt.Rows.Clear();
                dt.Columns.Add("EmpId", typeof(string));
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("Email", typeof(string));
                dt.Columns.Add("Designation", typeof(string));
                dt.Columns.Add("Mobile", typeof(string));
                dt.Rows.Add("_", "_", "_", "_", "_");
                gdvEmployees_Empty.DataSource = dt;
                gdvEmployees_Empty.DataBind();
                gdvEmployees_Empty.Visible = true;
                gdvEmployees.Visible = false;
                dt = null;
            }
            else
            {
                gdvEmployees_Empty.Visible = false;
                gdvEmployees.Visible = true;
            }
        }

        protected void btnAddEmp_Click(object sender, EventArgs e)
        {            
            prepare_modal_pop(1);
            imgPop_modalPop.Show();
        }

        private void prepare_modal_pop(int viewState)
        {
            txtKeyValue.Text = "";
            lblDataHeader.Text = "";
            switch (viewState)
            {
                case 1:
                    mvPop.ActiveViewIndex = 0;
                    pnlModalPop_Size.CssClass = "modal-dialog def2";
                    lblDataHeader.Text = "ADD EMPLOYEE";
                    btnUpdateEmp.Text = "Submit";
                    btnDeleteEmp.Visible = false;
                    tcPopEmp.Tabs[1].Visible = false;
                    tcPopEmp.ActiveTabIndex = 0;
                    clear_employee_info();
                    break;
                case 2:
                    mvPop.ActiveViewIndex = 0;
                    pnlModalPop_Size.CssClass = "modal-dialog def2";
                    lblDataHeader.Text = "EMPLOYEE DETAILS";
                    btnUpdateEmp.Text = "Update";
                    btnDeleteEmp.Visible = true;
                    tcPopEmp.Tabs[1].Visible = true;
                    tcPopEmp.ActiveTabIndex = 0;
                    clear_employee_info();
                    clear_employee_document_info();
                    break;
            }
        }

        protected void gdvEmployees_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            prepare_modal_pop(2);
            string empId = "";
            int idx = e.NewSelectedIndex;
            empId = gdvEmployees.DataKeys[idx].Value.ToString();
            txtKeyValue.Text = empId;
            DataSet ds = new DataSet();
            DAL dc = new DAL();
            ds = dc.Get_Employees("1", empId);
            dc = null;

            if (ds.Tables[0].Rows.Count > 0)
            {
                txtEmpId.Text = ds.Tables[0].Rows[0]["EmpId"].ToString();
                txtName.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                txtEmail.Text = ds.Tables[0].Rows[0]["Email"].ToString();
                txtDesignation.Text = ds.Tables[0].Rows[0]["Designation"].ToString();
                txtMobile.Text = ds.Tables[0].Rows[0]["Mobile"].ToString();
            }
            ds = null;
            load_employee_documents(Convert.ToInt32(empId),false);

            imgPop_modalPop.Show();
        }

        private void load_employee_documents(int EmpId, bool isDummy)
        {
            DataSet ds = new DataSet();
            if (isDummy == false)
            {
                DAL dc = new DAL();
                ds = dc.Get_Employees_Documents(EmpId);
                dc = null;
            }
            if (ds.Tables.Count > 0)
            {
                gdvEmployeesDocuments.DataSource = ds.Tables[0];
                gdvEmployeesDocuments.DataBind();
            }
            else
            {
                gdvEmployeesDocuments.DataSource = null;
                gdvEmployeesDocuments.DataBind();
            }

            gdvEmployeesDocuments_Empty.DataSource = null;
            gdvEmployeesDocuments_Empty.DataBind();
            ds = null;

            if (gdvEmployeesDocuments.Rows.Count == 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Clear();
                dt.Rows.Clear();
                dt.Columns.Add("DocId", typeof(string));
                dt.Columns.Add("FileName", typeof(string));
                dt.Columns.Add("FileDesc", typeof(string));
                dt.Columns.Add("CreatedDate", typeof(string));                
                dt.Rows.Add("_", "_", "_", "_");
                gdvEmployeesDocuments_Empty.DataSource = dt;
                gdvEmployeesDocuments_Empty.DataBind();
                gdvEmployeesDocuments_Empty.Visible = true;
                gdvEmployeesDocuments.Visible = false;
                dt = null;
            }
            else
            {
                gdvEmployeesDocuments_Empty.Visible = false;
                gdvEmployeesDocuments.Visible = true;
            }
        }

        private void clear_employee_info()
        {
            txtEmpId.Text = "[Auto Generated]";
            txtName.Text = "";
            txtEmail.Text = "";
            txtDesignation.Text = "";
            txtMobile.Text = "";
        }

        private void clear_employee_document_info()
        {
            fupDocFile = null;
            txtFileDesc.Text = "";
            load_employee_documents(0, true);
        }

        protected void btnUpdateEmp_Click(object sender, EventArgs e)
        {
            submit_employee();
        }

        private void submit_employee()
        {
            if (validate_submit_employee()==false)
            {
                imgPop_modalPop.Show();
                return;
            }

            if (txtKeyValue.Text.Trim().Length == 0)
            {
                DAL dc = new DAL();
                dc.Add_Employee(check_for_illegal_apostrophe(txtName.Text), check_for_illegal_apostrophe(txtEmail.Text) ,
                    check_for_illegal_apostrophe(txtDesignation.Text), check_for_illegal_apostrophe(txtMobile.Text));
                dc = null;
            }
            else
            {
                DAL dc = new DAL();
                dc.Update_Employee(Convert.ToInt32(txtKeyValue.Text), check_for_illegal_apostrophe(txtName.Text), check_for_illegal_apostrophe(txtEmail.Text),
                    check_for_illegal_apostrophe(txtDesignation.Text), check_for_illegal_apostrophe(txtMobile.Text));
                dc = null;
            }
            load_employee_masterlist(false);
            imgPop_modalPop.Hide();
        }

        private bool validate_submit_employee()
        {
            pnlPopError.Controls.Clear();
            bool ret = false;            
            if (txtName.Text.Trim().Length == 0)
            {
                Add_ErrorMessage(pnlPopError, "Please enter the name", false);
            }
            if (txtEmail.Text.Trim().Length == 0)
            {
                Add_ErrorMessage(pnlPopError, "Please enter the email", false);
            }
            else
            {
                if (IsValidEmail(txtEmail.Text) == false)
                {
                    Add_ErrorMessage(pnlPopError, "Please enter the valid email", false);
                }
            }

            if (txtDesignation.Text.Trim().Length == 0)
            {
                Add_ErrorMessage(pnlPopError, "Please enter the designation", false);
            }
            if (txtMobile.Text.Trim().Length == 0)
            {
                Add_ErrorMessage(pnlPopError, "Please enter the mobile", false);
            }            
            else
            {
                if (txtMobile.Text.Trim().Length <10 || txtMobile.Text.Trim().Length > 10)
                {
                    Add_ErrorMessage(pnlPopError, "Invalid mobile (should be 10 digit", false);
                }
                if (IsNumeric(txtMobile.Text) == false)
                {
                    Add_ErrorMessage(pnlPopError, "Invalid mobile", false);
                }
            }
            if (pnlPopError.Controls.Count == 0)
            {
                ret = true;
            }
            return ret;
        }

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {                
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));
                string DomainMapper(Match match)
                {                    
                    var idn = new IdnMapping();                 
                    string domainName = idn.GetAscii(match.Groups[2].Value);
                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }
            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }



        private string check_for_illegal_apostrophe(string textVal)
        {
            string ret = textVal;
            textVal.Replace("'", "''").Trim();
            return textVal;
        }

        protected void btnDeleteEmp_Click(object sender, EventArgs e)
        {            
            DAL dc = new DAL();
            dc.Delete_Employee(Convert.ToInt32( txtKeyValue.Text));
            dc = null;
            load_employee_masterlist(false);
            imgPop_modalPop.Hide();
        }

        protected void btnUploadDoc_Click(object sender, EventArgs e)
        {
            submit_employee_document();
        }

        private void submit_employee_document()
        {
            imgPop_modalPop.Show();
            if (validate_submit_employee_document() == false)
            {
                return;
            }

            string desc = "";
            if (txtFileDesc.Text.Trim().Length > 0)
            {
                desc = txtFileDesc.Text.Substring(0, 10);
            }

            string fileName = "";
            string fileFullName = "";
            string fileFullNameEnc = "";
            string filePath = "";
            string fileFullPath = "";
            fileName = "E_" + txtKeyValue.Text + "_" + desc + "_";
            fileName = fileName + DateTime.Now.Year.ToString("0000");
            fileName = fileName + DateTime.Now.Month.ToString("00");
            fileName = fileName + DateTime.Now.Day.ToString("00");
            fileName = fileName + DateTime.Now.Hour.ToString("00");
            fileName = fileName + DateTime.Now.Minute.ToString("00");
            fileName = fileName + DateTime.Now.Second.ToString("00");
            fileName = fileName + DateTime.Now.Millisecond.ToString("00");

            string fileExtension = Path.GetExtension(fupDocFile.PostedFile.FileName);
                       
            filePath = "~/xml/";
            fileFullPath = Server.MapPath("~/xml/");

            if (File.Exists(fileFullPath + fileName + fileExtension))
            {
                fileName = fileName + DateTime.Now.Ticks;
            }

            fileFullName = fileName + fileExtension;
            fileFullNameEnc = fileName  + "_enc" + fileExtension;

            string input = fileFullPath + fileFullName;
            string output = fileFullPath + fileFullNameEnc;

            fupDocFile.SaveAs(input);
            this.Encrypt(input, output);
            File.Delete(input);

            DAL dc = new DAL();
            dc.Add_Employee_Dcoument(Convert.ToInt32(txtKeyValue.Text), fileFullNameEnc, filePath, check_for_illegal_apostrophe(txtFileDesc.Text));
            dc = null;

            load_employee_documents(Convert.ToInt32(txtKeyValue.Text), false);

            txtFileDesc.Text = "";
        }

        private bool validate_submit_employee_document()
        {
            pnlPopError.Controls.Clear();
            bool ret = false;
            if (fupDocFile.HasFiles == false)
            {
                Add_ErrorMessage(pnlPopError, "Please select the file", false);                
            }
            else
            {
                string errmsg = "";
                if (Check_FileUploadSize_And_FileExtention(fupDocFile,ref errmsg) == false)
                {
                    Add_ErrorMessage(pnlPopError, errmsg, false);
                }
            }
            if (txtFileDesc.Text.Trim().Length ==  0)
            {
                Add_ErrorMessage(pnlPopError, "Please add description", false);
            }

            if (pnlPopError.Controls.Count == 0)
            {
                ret = true;
            }
            return ret;
        }

        public bool Check_FileUploadSize_And_FileExtention(FileUpload fup, ref string errormessage)
        {
            bool isvalidationOk = true;
            bool isValidExt = false;
            string ext = System.IO.Path.GetExtension(fup.FileName);  
            if (ext.Trim().ToLower() == ".pdf" | ext.Trim().ToLower() == ".doc" | ext.Trim().ToLower() == ".docx" | ext.Trim().ToLower() == ".xls" | ext.Trim().ToLower() == ".xlsx")
            {
                isValidExt = true;
            }

            if (isValidExt == false)
            {
                errormessage = "Please upload valid file (pdf, doc, docx, xls, xlsx)";

                isvalidationOk = false;                
                return isvalidationOk;
            }
            return isvalidationOk;
        }

        private void Encrypt(string inputFilePath, string outputfilePath)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (FileStream fsOutput = new FileStream(outputfilePath, FileMode.Create))
                {
                    using (CryptoStream cs = new CryptoStream(fsOutput, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        using (FileStream fsInput = new FileStream(inputFilePath, FileMode.Open))
                        {
                            int data;
                            while ((data = fsInput.ReadByte()) != -1)
                            {
                                cs.WriteByte((byte)data);
                            }
                        }
                    }
                }
            }
        }

        protected void gdvEmployeesDocuments_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            imgPop_modalPop.Show();
            string docId = "";
            int idx = e.RowIndex;
            docId = gdvEmployeesDocuments.DataKeys[idx].Value.ToString();
            DAL dc = new DAL();
            dc.Delete_Employee_Document(Convert.ToInt32(docId));
            dc = null;
            load_employee_documents(Convert.ToInt32(txtKeyValue.Text), false);
        }

        protected void gdvEmployeesDocuments_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            imgPop_modalPop.Show();
            string docId = "";
            int idx = e.NewSelectedIndex;
            docId = gdvEmployeesDocuments.DataKeys[idx].Value.ToString();            
            DataSet ds = new DataSet();
            DAL dc = new DAL();
            ds = dc.Get_Employees_Documents_ByID(Convert.ToInt32(docId));
            dc = null;

            if (ds.Tables[0].Rows.Count > 0)
            {
                string filePath = "";
                filePath = ds.Tables[0].Rows[0]["FilePath"].ToString();
                string fileFullPath = "";
                fileFullPath = Server.MapPath(filePath);
                string fileName = "";
                fileName = ds.Tables[0].Rows[0]["FileName"].ToString(); ;

                string input = fileFullPath + fileName;
                string output = fileFullPath + "_dec" + DateTime.Now.Ticks + fileName ;

                this.Decrypt(input, output);

                Response.Clear();
                //Response.ContentType = fupDocFile.PostedFile.ContentType;
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(output));
                Response.WriteFile(output);
                Response.Flush();
                
                File.Delete(output);
                Response.End();
            }
            ds = null;            
        }

        private void Decrypt(string inputFilePath, string outputfilePath)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (FileStream fsInput = new FileStream(inputFilePath, FileMode.Open))
                {
                    using (CryptoStream cs = new CryptoStream(fsInput, encryptor.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        using (FileStream fsOutput = new FileStream(outputfilePath, FileMode.Create))
                        {
                            int data;
                            while ((data = cs.ReadByte()) != -1)
                            {
                                fsOutput.WriteByte((byte)data);
                            }
                        }
                    }
                }
            }
        }

        private void Add_ErrorMessage(Panel pnlErrorControl, string errorMsg, bool isSuccess)
        {
            string errorHtml = "";
            if (isSuccess == false)
            {
                errorHtml = "<div class='alert alert-danger' role='alert'> " + errorMsg + "</div>";
            }
            else
            {
                errorHtml = "<div class='alert alert-success' role='alert'> " + errorMsg + "</div>";
            }
            HtmlGenericControl divcontrol = new HtmlGenericControl();
            divcontrol.InnerHtml = errorHtml;
            pnlErrorControl.Controls.Add(divcontrol);
        }

        public bool IsNumeric(string text)
        {
            double test;
            return double.TryParse(text, out test);
        }

    }



}