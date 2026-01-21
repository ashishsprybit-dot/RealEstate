using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class admin_includes_ListPagePagging : System.Web.UI.UserControl
{
    #region Member Variables
    private int _TotalRecord = 0;
    private int _CurrentPage = 1;
    #endregion

    #region Public Properties
    public int CurrentPage
    {
        get { return _CurrentPage; }
        set { _CurrentPage = value; }
    }
    #endregion

    #region Public Properties
    public int TotalRecord
    {
        get { return _TotalRecord; }
        set { _TotalRecord = value; }
    }
    #endregion
}
