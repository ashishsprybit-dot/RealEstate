using BAL;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Web.UI.WebControls;
using Utility;
using System.Linq;

public partial class AdminPanel_Category_List : AdminAuthentication
{
    CategoryBAL objCategoryBAL = new CategoryBAL();
    public string data = "";

    public int CLT_Version = 1;


    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form.Keys.Count > 0)
        {
            if (!int.TryParse(Request["page"], out CurrentPage))
                CurrentPage = 1;
            if (!string.IsNullOrEmpty(Request["type"]))
                CategoryOperation();
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
        Master.SelectedSection = AdminPanel_Admin.Section.General;
    }
    #endregion

    #region  Bind Category List
    private void BindList()
    {

        objCategoryBAL.Name = "";
        int intTotalRecord = 0;
        DataTable dt = new DataTable();
        dt = objCategoryBAL.GetList(ref CurrentPage, 100000, out intTotalRecord, SortColumn, SortType);

        CLT_Version = CltVersionGet(dt);
        DataTable dtResult = dt.Clone();
        DataRow dr;
        if (dt.Rows.Count > 0)
        {
            DataTable dtParentCategory = dt.AsEnumerable()
                         .Where(r => r.Field<Int32>("ParentID") == 0)
                         .OrderBy(r => r.Field<Int32>("SequenceNo"))
                         .CopyToDataTable();

            for (int i = 0; i < dtParentCategory.Rows.Count; i++)
            {
                dr = dtResult.NewRow();
                GetRowFromTable(dr, dtParentCategory.Rows[i]);
                dtResult.Rows.Add(dr);

                DataTable dtSecond = null;

                var rows = dt.AsEnumerable()
                         .Where(r => r.Field<Int32>("ParentID") == Convert.ToInt32(dtParentCategory.Rows[i]["ID"]))
                         .OrderBy(r => r.Field<Int32>("LevelNo"));
                //.CopyToDataTable();

                if (rows.Any())
                    dtSecond = rows.CopyToDataTable();
                else
                    dtSecond = new DataTable();



                for (int j = 0; j < dtSecond.Rows.Count; j++)
                {
                    dr = dtResult.NewRow();
                    GetRowFromTable(dr, dtSecond.Rows[j]);
                    dtResult.Rows.Add(dr);


                    DataTable dtThird = new DataTable();
                    DataView dvThird = new DataView(dt);
                    dvThird.RowFilter = "ParentID=" + Convert.ToInt32(dtSecond.Rows[j]["ID"]);
                    dvThird.Sort = "LevelNo ASC";
                    dtThird = dvThird.ToTable();

                    for (int k = 0; k < dtThird.Rows.Count; k++)
                    {
                        dr = dtResult.NewRow();
                        GetRowFromTable(dr, dtThird.Rows[k]);
                        dtResult.Rows.Add(dr);
                    }
                }

            }

        }



        data = JsonConvert.SerializeObject(dtResult, Formatting.Indented);
    }


    public int CltVersionGet(DataTable dt)
    {
        if (dt == null || dt.Rows.Count == 0)
            return 1; // Return 1 if the DataTable is empty or null

        // Check if any row contains CLT_Version = 2
        return dt.Select("CLT_Version = 2").Length > 0 ? 2 : 1;
    }






    private DataRow GetRowFromTable(DataRow dr, DataRow drTable)
    {
        dr["RowNumber"] = Convert.ToString(drTable["RowNumber"]);
        dr["ID"] = Convert.ToString(drTable["ID"]);
        dr["ParentID"] = Convert.ToString(drTable["ParentID"]);
        dr["Name"] = Convert.ToString(drTable["Name"]);
        dr["URL"] = Convert.ToString(drTable["URL"]);
        dr["Description"] = Convert.ToString(drTable["Description"]);
        dr["ImageName"] = Convert.ToString(drTable["ImageName"]);
        if (!string.IsNullOrEmpty( Convert.ToString(drTable["SequenceNo"])))
        {
            dr["SequenceNo"] = Convert.ToString(drTable["SequenceNo"]);
        }
        else
        {
            dr["SequenceNo"] = "0";
        }
        dr["LevelNo"] = Convert.ToString(drTable["LevelNo"]);
        dr["TreeLevelNo"] = Convert.ToString(drTable["TreeLevelNo"]);
        dr["CatName"] = Convert.ToString(drTable["CatName"]);
        dr["Questions"] = Convert.ToString(drTable["Questions"]);
        dr["TreeList"] = Convert.ToString(drTable["TreeList"]);
        dr["TreeLevel"] = Convert.ToString(drTable["TreeLevel"]);
        dr["TreeSubList"] = Convert.ToString(drTable["TreeSubList"]);
        dr["HasChild"] = Convert.ToString(drTable["HasChild"]);

        dr["CategoryMetaTitle"] = Convert.ToString(drTable["CategoryMetaTitle"]);
        dr["CategoryMetaKeyword"] = Convert.ToString(drTable["CategoryMetaKeyword"]);
        dr["CategoryMetaDescription"] = Convert.ToString(drTable["CategoryMetaDescription"]);
        dr["CreatedOn"] = Convert.ToString(drTable["CreatedOn"]);
        dr["Status"] = Convert.ToString(drTable["Status"]);
        dr["CLT_Version_Text"] = Convert.ToString(drTable["CLT_Version_Text"]);





        //dr["RowNumber"] = Convert.ToString(dtParentCategory.Rows[i]["RowNumber"]);
        //dr["ID"] = Convert.ToString(dtParentCategory.Rows[i]["ID"]);
        //dr["ParentID"] = Convert.ToString(dtParentCategory.Rows[i]["ParentID"]);
        //dr["Name"] = Convert.ToString(dtParentCategory.Rows[i]["Name"]);
        //dr["URL"] = Convert.ToString(dtParentCategory.Rows[i]["URL"]);
        //dr["Description"] = Convert.ToString(dtParentCategory.Rows[i]["Description"]);
        //dr["ImageName"] = Convert.ToString(dtParentCategory.Rows[i]["ImageName"]);
        //dr["SequenceNo"] = Convert.ToString(dtParentCategory.Rows[i]["SequenceNo"]);
        //dr["LevelNo"] = Convert.ToString(dtParentCategory.Rows[i]["LevelNo"]);
        //dr["TreeLevelNo"] = Convert.ToString(dtParentCategory.Rows[i]["TreeLevelNo"]);
        //dr["CatName"] = Convert.ToString(dtParentCategory.Rows[i]["CatName"]);
        //dr["Questions"] = Convert.ToString(dtParentCategory.Rows[i]["Questions"]);
        //dr["TreeList"] = Convert.ToString(dtParentCategory.Rows[i]["TreeList"]);
        //dr["TreeLevel"] = Convert.ToString(dtParentCategory.Rows[i]["TreeLevel"]);
        //dr["TreeSubList"] = Convert.ToString(dtParentCategory.Rows[i]["TreeSubList"]);
        //dr["HasChild"] = Convert.ToString(dtParentCategory.Rows[i]["HasChild"]);

        return dr;
    }
    #endregion

    #region Category Operation
    private void CategoryOperation()
    {
        Common.DataBaseOperation objOpr = Common.DataBaseOperation.None;
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

      int result = objCategoryBAL.Operation(Convert.ToString(Request["hdnID"]), objOpr);

        if (result == -3)
        {
            Response.Write(Javascript.DisplayMsg(divMsg.ClientID, "Rating already given for selected category question(s). You can not deactivate it.", Javascript.MessageType.Error, true));
        }
        else if (result == -2)
        {
            Response.Write(Javascript.DisplayMsg(divMsg.ClientID, "Rating already given for next level category question(s). You can not activate it.", Javascript.MessageType.Error, true));
        }
        else if (result == -1)
        {
            Response.Write(Javascript.DisplayMsg(divMsg.ClientID, "Rating already given for selected category question(s). You can not delete it.", Javascript.MessageType.Error, true));
        }
        else
        {
            Response.Write(Javascript.DisplayMsg(divMsg.ClientID, strMsg, Javascript.MessageType.Success, true));
        }
        Response.End();
    }
    #endregion

}