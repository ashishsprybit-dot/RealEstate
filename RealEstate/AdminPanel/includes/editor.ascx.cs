using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class include_editor : System.Web.UI.UserControl
{
    #region Properties
    public string Text
    {
        get { return tareaContent.Value; }
        set { tareaContent.Value = value; }
    }

    public string tareaUniqueID
    {
        get
        {
            return tareaContent.UniqueID;
        }
    }
    public string tareaClientID
    {
        get
        {
            return tareaContent.ClientID;
        }
    }
    public string LabelName { get; set; }
    #endregion

  
}
