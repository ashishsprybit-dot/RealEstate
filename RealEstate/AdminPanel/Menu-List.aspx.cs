using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;
using Utility;

public partial class AdminPanel_Menu_List : System.Web.UI.Page
{
    MenuBAL objMenuBAL = new MenuBAL();

    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
         
        if (!string.IsNullOrEmpty(Request["SetLevel"]))
        {
            SetSubMenuLevel();
        }
        if (!string.IsNullOrEmpty(Request["ChangeStatus"]))
        {
            Operation();
        }

        if (!string.IsNullOrEmpty(Request["resequence"]))
        {
            UpdateMenuSequence();
        }
        if (!IsPostBack)
        {
            BindList();
        }

    }
    #endregion

    #region  Bind Menu List
    private void BindList()
    {
        DataTable dt = new DataTable();
        objMenuBAL.ID = 0;
        objMenuBAL.ParentID = 0;
        dt = objMenuBAL.GetList();
        DataView dv = new DataView(dt);
        dv.RowFilter = "ParentID = 0";
        dv.Sort = "SequenceNo ASC";
        if (dv.Count > 0)
        {
            rptRecord.DataSource = dv;
            rptRecord.DataBind();
            rptRecord.Visible = true;
            trNoRecords.Visible = false;
        }
        else
        {
            rptRecord.Visible = false;
            trNoRecords.Visible = true;
        }
    }
    #endregion

    #region Menu Operation
    private void Operation()
    {
        Common.DataBaseOperation ObjOpr = Common.DataBaseOperation.None;
        string strMsg = string.Empty;
        switch (Request["type"])
        {
            case "remove":
                ObjOpr = Common.DataBaseOperation.Remove;
                strMsg = "Selected record(s) has been removed successfully.";
                break;
            case "active":
                ObjOpr = Common.DataBaseOperation.Active;
                strMsg = "Selected record(s) has been changed successfully.";
                break;
            case "inactive":
                ObjOpr = Common.DataBaseOperation.InActive;
                strMsg = "Selected record(s) has been changed successfully.";
                break;
        }
        MenuBAL ObjMenuBAL = new MenuBAL();
        int result = ObjMenuBAL.Operation(Convert.ToString(Request["ID"]), ObjOpr);
        if (result == -1)
        {
            Response.Write("refexist");
        }
        else
        {
            BindList();
            Response.Write(Common.RenderControl(divList, Common.RenderControlName.HtmlGeneric));
        }
        Response.End();
    }
    #endregion

    #region update sequence
    private void UpdateMenuSequence()
    {
        objMenuBAL.UpdateMenuSequence(Convert.ToString(Request["MenuID"]).Trim(), Convert.ToString(Request["Seq"]).Trim());
        BindList();
        Response.Write(Common.RenderControl(divList, Common.RenderControlName.HtmlGeneric));
        Response.End();
    }
    #endregion

    #region GetSubMenu
    protected DataTable GetSubMenu(int PID)
    {
        DataTable dt = new DataTable();
        objMenuBAL.ParentID = PID;
        if (PID != 0)
        {
            dt = objMenuBAL.GetList();
            DataView dv = new DataView(dt);
            dv.Sort = "LevelNo ASC";
            dt = dv.ToTable();
        }
        return dt;
    }
    #endregion
    private void SetSubMenuLevel()
    {
        objMenuBAL.LevelNo = Convert.ToInt32(Request["LevelNo"]);
        objMenuBAL.ParentID = Convert.ToInt32(Request["ParentID"]);
        objMenuBAL.ID = Convert.ToInt32(Request["MenuID"]);

        objMenuBAL.SetSubMenuLevel(Convert.ToString(Request["LevelType"]).Trim());

        BindList();
        Response.Write(Common.RenderControl(divList, Common.RenderControlName.HtmlGeneric));
        Response.End();
    }
}