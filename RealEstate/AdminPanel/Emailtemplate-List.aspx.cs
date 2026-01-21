using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;

public partial class AdminPanel_Emailtemplate_List : AdminAuthentication
{
    #region Private Members
    private string TemplateName = string.Empty;
    #endregion

    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {   
        if (Request.Form.Keys.Count > 0)
        {
            TemplateName = Request["tbxTemplateName"];
            if (!int.TryParse(Request["page"], out CurrentPage))
                CurrentPage = 1;
            if (!string.IsNullOrEmpty(Request["type"]))
                TemplateOperation();
            if (!int.TryParse(Request["hdnRecPerPage"], out RecordPerPage))
                RecordPerPage = 10;
        }
        if (!string.IsNullOrEmpty(Request["sorttype"]))
            SortType = Request["sorttype"];
        else
            SortType = "ASC";

        if (!string.IsNullOrEmpty(Request["sortcol"]))
            SortColumn = Request["sortcol"];
        else
            SortColumn = "ID";
        BindList();

    }
    #endregion

    #region  Bind Html Template List
    private void BindList()
    {
        int TotalRecord = 0;
        DataTable dtblTemplateList = new DataTable();
        EmailTemplateBAL objETemplate = new EmailTemplateBAL();
        objETemplate.ID = 0;
        objETemplate.TemplateName = TemplateName.Trim();
        dtblTemplateList = objETemplate.GetList(ref CurrentPage, RecordPerPage, out TotalRecord, SortColumn, SortType);

        if (dtblTemplateList.Rows.Count > 0)
        {
            rptRecord.Visible = true;
            rptRecord.DataSource = dtblTemplateList;
            rptRecord.DataBind();
            CtrlPage1.Visible = true;
            CtrlPage1.TotalRecord = TotalRecord;
            CtrlPage1.CurrentPage = CurrentPage;
            Utility.Common.BindAdminPagingControl(CurrentPage, RecordPerPage, PagingLimit, TotalRecord, (Repeater)(CtrlPage1.FindControl("rptPaging")));
            trNoRecords.Visible = false;
        }
        else
        {
            rptRecord.Visible = false;
            trNoRecords.Visible = true;
            CtrlPage1.Visible = false;
        }
        if (Request.Form.Keys.Count > 0)
        {
            if (string.IsNullOrEmpty(Request["type"]))
            {
                Response.Write("<script>DisplMsg('" + divMsg.ClientID + "','','')</script>");
            }
            Response.Write(Utility.Common.RenderControl(divList, Utility.Common.RenderControlName.HtmlGeneric));
            Response.Write("<script>page=" + CurrentPage + ";BindCtrl();$('#divTotRec').html('" + TotalRecord + " records');</script>");
            Response.End();
        }
    }
    #endregion

    #region Template Operation
    private void TemplateOperation()
    {
        Utility.Common.DataBaseOperation objOpr = Utility.Common.DataBaseOperation.None;
        string strMsg = string.Empty;
        switch (Request["type"])
        {
            case "remove":
                objOpr = Utility.Common.DataBaseOperation.Remove;
                strMsg = "Selected record(s) has been removed successfully.";
                break;
        }
        EmailTemplateBAL objTemp = new EmailTemplateBAL();
        objTemp.Operations(Convert.ToString(Request["hdnID"]), objOpr);
        Response.Write("<script>DisplMsg('" + divMsg.ClientID + "','" + strMsg + "','alert-message success')</script>");
    }
    #endregion

}