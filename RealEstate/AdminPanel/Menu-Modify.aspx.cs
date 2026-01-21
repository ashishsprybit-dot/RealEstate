using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;

public partial class AdminPanel_Menu_Modify : AdminAuthentication
{
    #region declaration
    CMSBAL objCMSBAL = new CMSBAL();
    MenuBAL objMenuBAL = new MenuBAL();
    #endregion

    #region Private Members
    private int _ID = 0;
    protected int intPosition = 0;
    #endregion

    #region Public Members
    public new int ID
    {
        get
        {
            return _ID;
        }
    }
    #endregion

    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    { 
        Int32.TryParse(Request["id"], out _ID);
        objMenuBAL.ID = ID;
        if (Request.Form.Keys.Count > 0)
        {
            SaveInfo();
        }
        else
        {
            BindModule();
            BindMenu();
            if (ID != 0)
            {
                BindControls();
            }
        }
        if (!IsPostBack)
        {

            if (string.IsNullOrEmpty(Request["id"]))
            {
                rdNone.Checked = true;
                hdnLinkType.Value = "0";
                hdnShowInMenu.Value = "1";
                rdShowMenuYes.Checked = true;
            }
        }

    }
    #endregion

    #region Bind Controls
    private void BindMenu()
    {
        DataTable dt = new DataTable();
        if (ID != 0)
        {
            objMenuBAL.ID = ID;
            dt = objMenuBAL.MenuDropdown("edit");
        }
        else
        {
            objMenuBAL.ID = 0;
            dt = objMenuBAL.MenuDropdown("add");
        }



        DataView dv = new DataView(dt);

        dv.RowFilter = "TreeLevelNo<2";
        if (dv.Count > 0)
        {
            ddlParentMenu.DataSource = dv;
            ddlParentMenu.DataTextField = "MenuName";
            //ddlParentMenu.DataTextField = "TreeList";
            ddlParentMenu.DataValueField = "ID";
            ddlParentMenu.DataBind();
        }
        ddlParentMenu.Items.Insert(0, new ListItem("Select Parent Menu", "0"));

    }
    private void BindModule()
    {
        DataTable dt = new DataTable();
        dt = objCMSBAL.GetAllCMSList();
        DataView dv = new DataView(dt);
        dv.Sort = "PageTitle ASC";

        if (dt.Rows.Count > 0)
        {
            rptModuleList.DataSource = dv;
            rptModuleList.DataBind();
        }


    }

    private void BindControls()
    {
        if (ID > 0)
        {
            DataTable dt = new DataTable();
            objMenuBAL.ID = ID;
            dt = objMenuBAL.GetList();
            if (dt.Rows.Count > 0)
            {
                tbxMenuTitle.Value = Convert.ToString(dt.Rows[0]["Name"]);
                tbxExternalMenuLink.Value = Convert.ToString(dt.Rows[0]["ExternalLinkURL"]);
                tbxMenuURL.Value = Convert.ToString(dt.Rows[0]["MenuURL"]);
                ddlParentMenu.Value = Convert.ToString(dt.Rows[0]["ParentID"]);
                if (Convert.ToInt16(dt.Rows[0]["LinkType"]) == 0)
                {
                    rdNone.Checked = true;
                    hdnLinkType.Value = "0";
                }
                else if (Convert.ToInt16(dt.Rows[0]["LinkType"]) == 1)
                {
                    rdCMS.Checked = true;
                    hdnLinkType.Value = "1";
                }
                else if (Convert.ToInt16(dt.Rows[0]["LinkType"]) == 2)
                {
                    rdExternal.Checked = true;
                    hdnLinkType.Value = "2";
                }

                if (Convert.ToInt16(dt.Rows[0]["ShowInMenu"]) == 1)
                {
                    rdShowMenuYes.Checked = true;
                    hdnShowInMenu.Value = "1";
                }
                else
                {
                    rdShowMenuNo.Checked = true;
                    hdnShowInMenu.Value = "0";
                }
                if (Convert.ToString(dt.Rows[0]["CMSID"]) == "-1")
                {
                    hdnStaticUrl.Value = Convert.ToString(dt.Rows[0]["StaticUrl"]);
                }

                hdnCMSID.Value = Convert.ToString(dt.Rows[0]["CMSID"]);
                intPosition = Convert.ToInt32(dt.Rows[0]["Position"]);

            }
           
        }
    }
    #endregion

    #region Save Information
    private void SaveInfo()
    {
        objMenuBAL.ID = ID;
        objMenuBAL.Name = Convert.ToString(Request[tbxMenuTitle.UniqueID]).Trim();
        objMenuBAL.ParentID = Convert.ToInt32(Request[ddlParentMenu.UniqueID]);
        objMenuBAL.ExternalLink = false;
        objMenuBAL.MenuURL = Convert.ToString(Request[tbxMenuURL.UniqueID]).Trim();

        if (Convert.ToInt32(Request[hdnLinkType.UniqueID]) == 0)
        {
            objMenuBAL.StaticURL = string.Empty;
            objMenuBAL.ExternalLinkURL = string.Empty;
            objMenuBAL.CMSID = 0;
        }
        else if (Convert.ToInt32(Request[hdnLinkType.UniqueID]) == 1)
        {
            if (Convert.ToInt32(Request[hdnCMSID.UniqueID]) == -1)
            {
                objMenuBAL.StaticURL = Convert.ToString(Request[tbxCMSMenuLink.UniqueID]).Trim();
                objMenuBAL.CMSID = -1;
            }
            else
            {
                objMenuBAL.StaticURL = string.Empty;
                objMenuBAL.CMSID = Convert.ToInt32(Request[hdnCMSID.UniqueID]);
            }
        }
        else if (Convert.ToInt32(Request[hdnLinkType.UniqueID]) == 2)
        {
            objMenuBAL.StaticURL = string.Empty;
            objMenuBAL.ExternalLinkURL = Convert.ToString(Request[tbxExternalMenuLink.UniqueID]).Trim();
            objMenuBAL.CMSID = 0;
        }


        objMenuBAL.ShowInMenu = Convert.ToInt32(Request[hdnShowInMenu.UniqueID]);
        objMenuBAL.LinkType = Convert.ToInt32(Request[hdnLinkType.UniqueID]);
        objMenuBAL.Position = Convert.ToInt32(Convert.ToString(Request["rdoPosition"]));

        switch (objMenuBAL.Save())
        {
            case -1:
                Response.Write(Javascript.DisplayMsg(divMsg.ClientID, "Menu already exists. So please try another Menu.", Javascript.MessageType.Error, true));
                break;
            default:
                Response.Write(Javascript.ScriptStartTag + "window.setTimeout(\"window.location.href='menu-list.aspx'\",2000)" + Javascript.ScriptEndTag);
                Response.Write(Javascript.DisplayMsg(divMsg.ClientID, "Menu information has been saved successfully.", Javascript.MessageType.Success, true));

                break;
        }
        
        Response.End();


    }
    #endregion
}