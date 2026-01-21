using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;
using Utility;

public partial class AdminPanel_Banner_List : AdminAuthentication
{
    #region Private Members
    private string Name = string.Empty;
    protected int intCount = 0;
    #endregion

    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    { 
        if (RecordPerPage == 10) RecordPerPage = 100;
        if (!string.IsNullOrEmpty(Request["resequence"]))
        {
            UpdateBannerSequence();
        }
        if (Request.Form.Keys.Count > 0)
        {
            Name = Request["tbxName"];
            if (!int.TryParse(Request["page"], out CurrentPage))
                CurrentPage = 1;
            if (!string.IsNullOrEmpty(Request["type"]))
                BannerOperation();
            if (!int.TryParse(Request["hdnRecPerPage"], out RecordPerPage))
                RecordPerPage = 100;
        }
        if (!string.IsNullOrEmpty(Request["sorttype"]))
            SortType = Request["sorttype"];
        else
            SortType = "ASC";

        if (!string.IsNullOrEmpty(Request["sortcol"]))
            SortColumn = Request["sortcol"];
        else
            SortColumn = "Name";
        BindList();
        //Master.SelectedSection = admin_Admin.Section.Dashboard;
    }
    #endregion

    #region  Bind Banner List
    private void BindList()
    {
        int TotalRecord = 0;
        DataTable dtblBannerList = new DataTable();
        BannerBAL objBannerBAL = new BannerBAL();
        dtblBannerList = objBannerBAL.GetList();
        if (dtblBannerList.Rows.Count > 0)
        {
            rptRecord.Visible = true;
            rptRecord.DataSource = dtblBannerList;
            intCount = dtblBannerList.Rows.Count;
            rptRecord.DataBind();
            
            trNoRecords.Visible = false;
        }
        else
        {
            rptRecord.Visible = false;
            trNoRecords.Visible = true;
             
        }
        if (Request.Form.Keys.Count > 0)
        {
            if (string.IsNullOrEmpty(Request["type"]))
            {
                Response.Write("<script>DisplMsg('" + divMsg.ClientID + "','','')</script>");
            }
            Response.Write(Utility.Common.RenderControl(divList, Utility.Common.RenderControlName.HtmlGeneric));
            Response.Write("<script>page=" + CurrentPage + ";BindCtrl();$('#divTotRec').html('" + TotalRecord + " records');$('#hdnCount').val('" + intCount + "');SetAddBannerLink(); SortTable();</script>");
            Response.End();
        }
    }
    #endregion

    #region Banner Operation
    private void BannerOperation()
    {
        Utility.Common.DataBaseOperation objOpr = Utility.Common.DataBaseOperation.None;
        string strMsg = string.Empty;
        switch (Request["type"])
        {
            case "remove":
                objOpr = Common.DataBaseOperation.Remove;
                strMsg = "Selected record(s) has been removed successfully.";
                break;
            case "active":
                objOpr = Common.DataBaseOperation.Active;
                strMsg = "Selected record(s) has been changed successfully.";
                break;
            case "inactive":
                objOpr = Common.DataBaseOperation.InActive;
                strMsg = "Selected record(s) has been changed successfully.";
                break;


        }
        BannerBAL objTemp = new BannerBAL();
        if (!string.IsNullOrEmpty(Request["ID"]))
        {
            objTemp.Operation(Convert.ToString(Request["ID"]), objOpr);
            Response.Write(Javascript.DisplayMsg(divMsg.ClientID, strMsg, Javascript.MessageType.Success, true));
        }
        else
        {
            objTemp.Operation(Convert.ToString(Request["hdnID"]), objOpr);
            Response.Write(Javascript.DisplayMsg(divMsg.ClientID, strMsg, Javascript.MessageType.Success, true));
        }
    }
    #endregion

    #region update sequence
    private void UpdateBannerSequence()
    {
        BannerBAL objBannerBAL = new BannerBAL();
        objBannerBAL.UpdateBannerSequence(Convert.ToString(Request["BannerID"]).Trim(), Convert.ToString(Request["Seq"]).Trim());
        BindList();
        Response.Write(Common.RenderControl(divList, Common.RenderControlName.HtmlGeneric));
        Response.End();
    }
    #endregion
}