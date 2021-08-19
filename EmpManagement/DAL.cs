using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EmpManagement
{
    public class DAL
    {
        public  string ConnectionString = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;

        #region "Database Connections"

        private void Execute_Query(string Query_)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand(Query_, con);
            cmd.ExecuteNonQuery();
            con.Close();
            cmd = null;
        }

        private SqlDataReader Get_DataReader(string Query_)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand(Query_, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            con.Close();
            cmd = null;
            return dr;
        }

        private DataSet Get_DataSet(string Query_)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand(Query_, con);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            con.Open();
            DataSet ds = new DataSet();
            da.Fill(ds, "employees");
            con.Close();
            return ds;
        }

        #endregion "Database Connections"

        #region "Employee Management"

        public void Add_Employee(string Name, string Email, string Designation, string Mobile)
        {
            string qry = "";
            qry = "INSERT INTO Employee (Name, Email, Designation, Mobile) VALUES ('" + Name + "', '" + Email + "', '" + Designation + "', '" + Mobile + "')";
            Execute_Query(qry);
        }

        public void Update_Employee(int EmpId, string Name, string Email, string Designation, string Mobile)
        {
            string qry = "";
            qry = "UPDATE Employee SET Name = '" + Name + "', Email  = '" + Email + "', Designation = '" + Designation + "', Mobile = '" + Mobile + "' WHERE EmpId =  " + EmpId;
            Execute_Query(qry);
        }

        public void Delete_Employee(int EmpId)
        {
            Delete_Employee_Document_By_EmpID(EmpId);
            string qry = "";            
            qry = "DELETE Employee WHERE EmpId =  " + EmpId;
            Execute_Query(qry);
        }

        public DataSet Get_Employees(string SearchIn, string SearchKey)
        {
            string qry = "";
            qry = "SELECT EmpId, Name, Email, Designation, Mobile FROM Employee ";
            string condi = "";
            if (SearchIn.Trim().Length > 0 && SearchKey.Trim().Length > 0)
            {
                switch (SearchIn)
                {
                    case "1":
                        if (IsNumeric(SearchKey) == true)
                        {
                            condi = " WHERE (EmpId = " + SearchKey + ") ";
                        }
                        break;
                    case "2":
                        condi = " WHERE (Name LIKE N'%" + SearchKey + "%') ";
                        break;
                    case "3":
                        condi = " WHERE (Email LIKE N'%" + SearchKey + "%') ";
                        break;
                    case "4":
                        condi = " WHERE (Designation LIKE N'%" + SearchKey + "%') ";
                        break;
                    case "5":
                        condi = " WHERE (Mobile LIKE N'%" + SearchKey + "%') ";
                        break;
                }
            }
            if (condi.Trim().Length > 0)
            {
                qry = qry + condi;
            }
            DataSet ds = new DataSet();
            ds = Get_DataSet(qry);
            return ds;
        }

        #endregion "Employee Management"


        #region "Employee Document Management"

        public void Add_Employee_Dcoument(int EmpId, string FileName, string FilePath, string FileDesc)
        {
            string qry = "";
            qry = "INSERT INTO Employee_Details (EmpId, FileName, FilePath, FileDesc, CreatedDate) VALUES ('" + EmpId + "', '" + FileName + "', '" + FilePath + "', '" + FileDesc + "', '" + DateTime.Now + "')";
            Execute_Query(qry);
        }

        public void Delete_Employee_Document(int DocId)
        {
            string qry = "";            
            qry = "DELETE Employee_Details WHERE DocId =  " + DocId;
            Execute_Query(qry);
        }

        public void Delete_Employee_Document_By_EmpID(int EmpId)
        {
            string qry = "";
            qry = "DELETE Employee_Details WHERE EmpId =  " + EmpId;
            Execute_Query(qry);
        }

        public DataSet Get_Employees_Documents(int EmpId)
        {
            string qry = "";
            qry = "SELECT DocId, FileName, FilePath, FileDesc, CreatedDate FROM Employee_Details WHERE EmpId = " + EmpId + " ";            
            DataSet ds = new DataSet();
            ds = Get_DataSet(qry);
            return ds;
        }

        public DataSet Get_Employees_Documents_ByID(int DocId)
        {
            string qry = "";
            qry = "SELECT FileName, FilePath FROM Employee_Details WHERE DocId = " + DocId + " ";
            DataSet ds = new DataSet();
            ds = Get_DataSet(qry);
            return ds;
        }

        #endregion "Employee Document Management"

        public bool IsNumeric(string text)
        {
            double test;
            return double.TryParse(text, out test);
        }

    }
}