using BAL;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Web.UI.WebControls;
using Utility;

public partial class Main_Teacher_List : System.Web.UI.Page
{
    protected string SchoolURL = "";
    TeachersBAL objTeacher = new TeachersBAL();
    public string data = "";

    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form.Keys.Count > 0)
        {
            //if (!int.TryParse(Request["page"], out CurrentPage))
            //    CurrentPage = 1;
            if (!string.IsNullOrEmpty(Request["type"]))
                TeacherOperation();
            //if (!int.TryParse(Request["hdnRecPerPage"], out RecordPerPage))
            //    RecordPerPage = 10;
        }
        //if (!string.IsNullOrEmpty(Request["sorttype"]))
        //    SortType = Request["sorttype"];
        //else
        //    SortType = "ASC";

        //if (!string.IsNullOrEmpty(Request["sortcol"]))
        //    SortColumn = Request["sortcol"];
        //else
        //    SortColumn = "ID";

        SchoolURL = this.Master.RequestedSchoolURL;
        BindList();
        //Master.SelectedSection = AdminPanel_Admin.Section.General;
    }
    #endregion

    #region  Bind User List
    private void BindList()
    {
        objTeacher.FirstName = Request["tbxFname"];
        objTeacher.LastName = Request["tbxLname"];
        objTeacher.EmailID = Request["tbxEmail"];
        int intTotalRecord = 0;
        DataTable dt = new DataTable();
        int CurrentPage = 1;
        dt = objTeacher.GetList(ref CurrentPage, 100000, out intTotalRecord, "ID", "ASC", SchoolURL);
        data = JsonConvert.SerializeObject(dt, Formatting.Indented);
    }
    #endregion

    #region Teacher Operation
    private void TeacherOperation()
    {
        Common.DataBaseOperation objOpr = Common.DataBaseOperation.None;
        string strMsg = string.Empty;
        switch (Request["type"])
        {
            case "remove":
                objOpr = Common.DataBaseOperation.Remove;
                strMsg = "Selected record has been removed successfully.";
                break;
            case "active":
                objOpr = Common.DataBaseOperation.Active;
                strMsg = "Selected record has been changed successfully.";
                break;
            case "inactive":
                objOpr = Common.DataBaseOperation.InActive;
                strMsg = "Selected record has been changed successfully.";
                break;
        }

        objTeacher.Operation(Convert.ToString(Request["hdnID"]), objOpr, Convert.ToInt64(Session["TeacherUserID"]));
        if (Convert.ToString(Request["type"]) == "remove")
            Response.Write("<script>SetTeacherCount();</script>");
        Response.Write(Javascript.DisplayMsg(divMsg.ClientID, strMsg, Javascript.MessageType.Success, true));

        Response.End();
    }
    #endregion

}