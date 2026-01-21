using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using BAL;
using Utility;
using DAL;

public partial class controlpanel_popupform : AdminAuthentication
{
    private new int ID;
    protected string Category = "";
    protected string FoodItems = "";
    private void BindEnquiry()
    {
        ContactUsBAL sbal = new ContactUsBAL();
        DataTable contactUSByID = new DataTable();
        sbal.ID = this.ID;
        contactUSByID = sbal.GetContactUSByID();
        if (contactUSByID.Rows.Count > 0)
        {
            rptContactUs.DataSource = contactUSByID;
            rptContactUs.DataBind();
            divContactUs.Visible = true;
        }
    }
     protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToString(Request["type"]) == "enquiry")
        {
            int.TryParse(Request["id"], out this.ID);
            rptContactUs.Visible = true;
            BindEnquiry();
            h4head.InnerText = "Contact Us enquiry";
        }
       

    }

}
