using BAL;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Web.UI.WebControls;
using Utility;
public partial class AdminPanel_Skill_order : System.Web.UI.Page
{
    protected long SubHeadingID = 0;
    protected long QuestionID = 0;
    protected long CategoryID = 0;
    QuestionSubHeadingSkillsBAL objQuestionSubHeadingSkillsBAL = new QuestionSubHeadingSkillsBAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["SaveOrder"] != null)
        {
            SaveOrderChange();

        }
        if (Request["sid"] != null)
        {
            SubHeadingID = Convert.ToInt64(Request["sid"]);
            QuestionID = Convert.ToInt64(Request["qid"]);
            CategoryID = Convert.ToInt64(Request["cid"]);
            if (SubHeadingID == 0)
            {
                Response.Redirect("category-list.aspx");
            }
        }
        BindList();
    }
    private void BindList()
    {
        int CurrentPage = 1;
        objQuestionSubHeadingSkillsBAL.Skill = "";
        objQuestionSubHeadingSkillsBAL.SubHeadingID = SubHeadingID;
        int intTotalRecord = 0;
        DataTable dt = new DataTable();
        dt = objQuestionSubHeadingSkillsBAL.GetList(ref CurrentPage, 100000, out intTotalRecord, "SequenceNo", "ASC");
        rptQuestionListOrderChange.DataSource = dt;
        rptQuestionListOrderChange.DataBind();    

    }

    private void SaveOrderChange()
    {
        objQuestionSubHeadingSkillsBAL.QuestionSubHeadingSkillOrderChange(Convert.ToInt32(Request["sid"]), Convert.ToString(Request["Subheadings"]));
        Response.Write("success");
        Response.End();
    }
}