using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;
using Utility;

public partial class AdminPanel_Category_Modify : AdminAuthentication
{
    #region Private Members
    string strImageName = string.Empty;
    protected string strSchools = "";

    private int _ID = 0;
    CategoryBAL objCategoryBAL = new CategoryBAL();
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
        objCategoryBAL.ID = ID;

        if (Request.Form.Keys.Count > 0)
        {
            SaveInfo();
        }
        else
        {
            BindCategoryDropdown();
            BindSchools();
            if (objCategoryBAL.ID != 0)
            {
                spnHeader.InnerHtml = "Edit Category";
                BindControls();
            }
            else
            {
                spnHeader.InnerHtml = "Add Category";
            }
        }
        Master.SelectedSection = AdminPanel_Admin.Section.General;

    }
    #endregion

    #region Bind Controls
    private void BindSchools()
    {
        SchoolBAL objSchool = new SchoolBAL();
        objSchool.Name = "";
        int intTotalRecord = 0;
        DataTable dt = new DataTable();
        dt = objSchool.GetList(ref CurrentPage, 10000, out intTotalRecord, "Name", "ASC");
        if (dt.Rows.Count > 0)
        {
            rptSchools.DataSource = dt;
            rptSchools.DataBind();
        }

    }
    private void BindCategoryDropdown()
    {
        DataTable dt = new DataTable();
        DataTable dtCategory = new DataTable();
        DataRow dr;
        dtCategory.Columns.Add("TreeSubListSpace");
        dtCategory.Columns.Add("ID");

        DataTable dtChild = new DataTable();

        dt = objCategoryBAL.CategoryListALL();
        DataView dv = new DataView(dt);
        dv.RowFilter = "TreeLevelNo<3";
        dt = dv.ToTable();

        DataTable dtParent = new DataTable();
        DataView dvParent = new DataView(dt);
        dvParent.RowFilter = "ParentID=0";
        dvParent.Sort = "SequenceNo ASC";

        dtParent = dvParent.ToTable();

        for (int i = 0; i < dtParent.Rows.Count; i++)
        {
            dr = dtCategory.NewRow();
            dr["TreeSubListSpace"] = Convert.ToString(dtParent.Rows[i]["TreeSubListSpace"]);
            dr["ID"] = Convert.ToString(dtParent.Rows[i]["ID"]);
            dtCategory.Rows.Add(dr);

            dtChild = new DataTable();
            DataView dvChild = new DataView(dt);
            dvChild.RowFilter = "ParentID=" + Convert.ToString(dtParent.Rows[i]["ID"]);
            dvChild.Sort = "LevelNo ASC";
            dtChild = dvChild.ToTable();


            for (int j = 0; j < dtChild.Rows.Count; j++)
            {
                dr = dtCategory.NewRow();
                dr["TreeSubListSpace"] = Convert.ToString(dtChild.Rows[j]["TreeSubListSpace"]);
                dr["ID"] = Convert.ToString(dtChild.Rows[j]["ID"]);
                dtCategory.Rows.Add(dr);
            }

        }



        if (dv.Count > 0)
        {
            ddlParentCategory.DataSource = dtCategory;
            ddlParentCategory.DataTextField = "TreeSubListSpace";
            ddlParentCategory.DataValueField = "ID";
            ddlParentCategory.DataBind();
        }
        ddlParentCategory.Items.Insert(0, new ListItem("-- Select --", "0"));
        //int intTotalRecord = 0;
        //HomeGroupsBAL objHomeGroups = new HomeGroupsBAL();
        //DataTable dt = new DataTable();
        //dt = objHomeGroups.GetList(ref CurrentPage, 10000, out intTotalRecord, "KGCKEY", "ASC");
        //if (dt.Rows.Count > 0)
        //{            rptPages.DataSource = dt;
        //    rptPages.DataBind();
        //}
    }

    private void BindControls()
    {
        DataTable dt = new DataTable();
        objCategoryBAL.ID = ID;
        dt = objCategoryBAL.CategoryDetailsByID();

        if (dt.Rows.Count > 0)
        {
            ddlParentCategory.Value = Convert.ToString(dt.Rows[0]["ParentID"]).Trim();
            tbxCategoryName.Value = Convert.ToString(dt.Rows[0]["Name"]).Trim();
            tbxDescription.Value = Convert.ToString(dt.Rows[0]["Description"]).Trim();
            if (Convert.ToString(dt.Rows[0]["CLT_Version"]) != string.Empty)
                ddlLayout.Value = Convert.ToString(dt.Rows[0]["CLT_Version"]).Trim();

            if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["ImageName"])))
            {
                hdnImage.Value = Convert.ToString(dt.Rows[0]["ImageName"]);
                //imgImage.Src = Config.VirtualDir + "thumb.aspx?path=" + Config.CMSFiles + Convert.ToString(dt.Rows[0]["ImageName"]) + "&height=70";
                imgImage.Src = Config.VirtualDir + Config.CMSFiles + Convert.ToString(dt.Rows[0]["ImageName"]);
                imgImage.Visible = true;
            }
            else
            {
                imgImage.Visible = false;
                hdnImage.Value = string.Empty;
            }

            strSchools = Convert.ToString(dt.Rows[0]["CategorySchools"]).Trim();
        }
    }
    #endregion

    #region Save Information
    private void SaveInfo()
    {
        strImageName = "";
        if (Request.Files[fupdImage.UniqueID] != null)
        {
            if (Request.Files[fupdImage.UniqueID].ContentLength > 0)
            {
                strImageName = System.IO.Path.GetExtension(fupdImage.PostedFile.FileName);
                if (IsImage(fupdImage.PostedFile.FileName.ToLower()) == false)
                {
                    Response.Write(Common.ShowMessage("Please upload only .JPG, .JPEG, .PNG, .BMP, .GIF image files.", "alert-message error", divMsg.ClientID));
                }
                strImageName = "Icon_" + Convert.ToString(DateTime.Now.Day) + Convert.ToString(DateTime.Now.Month) + Convert.ToString(DateTime.Now.Year) + Convert.ToString(DateTime.Now.Date.Hour) + Convert.ToString(DateTime.Now.Minute) + Convert.ToString(DateTime.Now.Second) + Convert.ToString(DateTime.Now.Ticks) + strImageName;

                Request.Files[fupdImage.UniqueID].SaveAs(Request.PhysicalApplicationPath + "/" + Config.CMSFiles + strImageName);
            }
            else
            {
                if (!string.IsNullOrEmpty(Request[hdnImage.UniqueID]))
                {
                    strImageName = Request[hdnImage.UniqueID];
                }
                else
                {
                    strImageName = string.Empty;
                }
            }

        }
        objCategoryBAL.ImageName = strImageName;


        objCategoryBAL.ID = ID;
        objCategoryBAL.Name = Request.Form[tbxCategoryName.UniqueID].Trim();
        objCategoryBAL.Description = Request.Form[tbxDescription.UniqueID].Trim();
        objCategoryBAL.ParentID = Request.Form[ddlParentCategory.UniqueID].Trim();
        // objCategoryBAL.ImageName = "";
        objCategoryBAL.CategoryMetaTitle = "";
        objCategoryBAL.CategoryMetaKeyword = "";
        objCategoryBAL.CategoryMetaDescription = "";
        objCategoryBAL.CLT_Version = Convert.ToInt32(Request[ddlLayout.UniqueID]);

        string SchoolCategory = Convert.ToString(Request[hdnSchools.UniqueID]);

        if (ID.ToString() == objCategoryBAL.ParentID && objCategoryBAL.ParentID != "0")
        {
            ShowMessage("You can not select same category as parent category.", "alert alert-danger error", divMsg.ClientID);
        }
        else
        {
            switch (objCategoryBAL.Save(SchoolCategory))
            {
                case 0:
                    ShowMessage("Duplicate Category Name found.", "alert alert-danger error", divMsg.ClientID);
                    break;
                default:
                    ShowMessage("Category information has been saved successfully.", "alert alert-success", divMsg.ClientID);
                    Response.Write(Common.ScriptStartTag + "parent.ScrollTop();" + Common.ScriptEndTag);
                    Response.Write(Common.ScriptStartTag + "parent.window.setTimeout(\"parent.window.location.href = 'category-list.aspx'\",2000);" + Common.ScriptEndTag);
                    break;
            }
        }
        Response.End();
    }
    #endregion

    #region General

    private void ShowMessage(string strMessage, string strMessageType, string divMessage)
    {
        Response.Write("<link href='" + Config.VirtualDir + "style/style.css' rel='Stylesheet' type='text/css' />");
        Response.Write("<script type='text/javascript' language='javascript' src='" + Config.VirtualDir + "js/jquery-1.10.2.min.js'></script>");
        Response.Write("<script type='text/javascript' language='javascript' src='" + Config.VirtualDir + "js/general.js'></script>");
        Response.Write("<script type='text/javascript'>var virtualDir = '" + Config.VirtualDir + "';</script>");
        Response.Write("$(document.ready(function {");
        Response.Write(Common.ScriptStartTag + "parent.$('#" + divMessage + "').show();" + Common.ScriptEndTag);
        Response.Write(Common.ScriptStartTag + "parent.$('#" + divMessage + "').html('" + strMessage + "');" + Common.ScriptEndTag);
        Response.Write(Common.ScriptStartTag + "parent.$('#" + divMessage + "').attr('class', '" + strMessageType + "');" + Common.ScriptEndTag);
        Response.Write(Javascript.ScriptStartTag + "window.setTimeout(\"parent.$('#" + divMessage + "').fadeOut(600);\",5000)" + Javascript.ScriptEndTag);
        Response.Write("});");
    }
    private void DeleteGalleryFile(string strImage)
    {
        if (!string.IsNullOrEmpty(strImage))
        {
            if (Common.IsFileExist(Config.CMSFiles, strImage))
            {
                Common.FileDelete(Config.CMSFiles, strImage);
            }
        }
    }
    public static bool IsImage(string strImageName)
    {
        bool blnReturnValue = false;
        switch (System.IO.Path.GetExtension(strImageName).ToLower())
        {
            case ".jpg":
            case ".jpeg":
            case ".gif":
            case ".png":
            case ".bmp":
                blnReturnValue = true;
                break;
        }
        return blnReturnValue;
    }

    #endregion
}