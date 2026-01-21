using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;
using System.Threading;
using System.Globalization;
using System.Net;
using System.IO;
public partial class AdminPanel_Dashboard : AdminAuthentication
{
   


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            AdminBAL objAdmin = new AdminBAL();
            DataTable dt = new DataTable();

            if (Session["UserID"] != null)
            {
                dt = objAdmin.GetAdminDetails(Convert.ToInt32(Session["UserID"]));
            }
            else
            {
                dt = objAdmin.GetAdminDetails(AdminAuthentication.AdminID);
            }
            
        }
        if (Session["AdminType"] != null)
        {
            AdminType = Convert.ToInt32(Session["AdminType"]);
        }
    
       
    }
  
  

}