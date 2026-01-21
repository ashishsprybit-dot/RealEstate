using BAL;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Web.UI.WebControls;
using Utility;
using DAL;
public partial class AdminPanel_category_order : System.Web.UI.Page
{
    protected long CategoryID = 0;
    QuestionsBAL objQuestionsBAL = new QuestionsBAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (Request["GetSubcategory"] != null)
        {
            GetSubcategory();

        }

        if (Request["SaveOrder"] != null)
        {
            SaveOrderChange();

        }
        //if (Request["cid"] != null)
        //{
        //    CategoryID = Convert.ToInt64(Request["cid"]);
        //    if (CategoryID == 0)
        //    {
        //        Response.Redirect("category-list.aspx");
        //    }
        //}
        BindList();
        BindCategory();
    }

    private void GetSubcategory()
    {
        DataTable dt = new DataTable();
        dt = CategoryListByParentID( Convert.ToInt32(Request["cid"]));

        rptQuestionListOrderChange.DataSource = dt;
        rptQuestionListOrderChange.DataBind();

        Response.Write(Utility.Common.RenderControl(divCategory, Common.RenderControlName.HtmlGeneric));
        Response.End();
    }
    private void BindCategory()
    {
        int CurrentPage = 1;
        CategoryBAL objCategoryBAL = new CategoryBAL();
        int intTotalRecord = 0;
        DataTable dt = new DataTable();
        dt = objCategoryBAL.GetList(ref CurrentPage, 100000, out intTotalRecord, "ID", "ASC");

        DataView dv = new DataView(dt);
        dv.RowFilter = "TreeLevelNo = 1 OR TreeLevelNo = 2";
        dv.Sort = "CategoryTreeID ASC";
        if (dv.Count > 0)
        {
            ddlCategoryOrder.DataSource = dv;
            ddlCategoryOrder.DataTextField = "TreeLevel";
            ddlCategoryOrder.DataValueField = "ID";
            ddlCategoryOrder.DataBind();
        }
        ddlCategoryOrder.Items.Insert(0, new ListItem("-- Select --", "0"));
    }
    private void BindList()
    {
        //int CurrentPage = 1;
        //objQuestionsBAL.Question = "";
        //objQuestionsBAL.CategoryID = CategoryID;
        //int intTotalRecord = 0;
        //DataTable dt = new DataTable();
        //dt = objQuestionsBAL.GetList(ref CurrentPage, 100000, out intTotalRecord, "SequenceNo", "ASC");
        //rptQuestionListOrderChange.DataSource = dt;
        //rptQuestionListOrderChange.DataBind();    

    }

    private void SaveOrderChange()
    {
        CategoryOrderChange(Convert.ToString(Request["Questions"]));
        Response.Write("success");
        Response.End();
    }

    private DataTable CategoryListByParentID(int ParentID)
    {
        DbParameter[] dbParam = new DbParameter[1];
        dbParam[0] = new DbParameter("@ParentID", DbParameter.DbType.Int, 200, ParentID);
        DataTable dt = new DataTable();
        dt = DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "CategoryListByParentID", dbParam);
        return dt;
    }

    private void CategoryOrderChange( string Sequence)
    {
        DbParameter[] dbParam = new DbParameter[] {
            new DbParameter("@Sequence", DbParameter.DbType.VarChar, 500000,Sequence)
        };
        DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "CategoryOrderChange", dbParam);

    }
}