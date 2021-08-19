using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmpManagement
{
    public partial class Common : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            set_menu(Page.Title);
        }

        private void set_menu(string PageTitle)
        {
            string menuItems = "";
            switch (PageTitle)
            {               
                case "Employee Management":
                    menuItems = "<li class='nav-item'><a class='nav-link active' href='/EmployeeMasterlist.aspx'>Employee Management</a></li>";
                    break;
            }            
            menuLiteral.Text = "<ul class='nav nav-pills'>" +  menuItems + "</ul>";
        }
    }
}