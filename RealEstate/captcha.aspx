<%@ Page Language="C#" AutoEventWireup="true" ClassName="Captcha_Control"  %>
<%@ Import Namespace="System.Drawing" %>
<%@ Import Namespace="Utility" %>
<script runat="server">
    protected void Page_Load(object sender, EventArgs e){       

            if (Session["CaptchaText"] == null || string.IsNullOrEmpty(Convert.ToString(Session["CaptchaText"])))
            {
                Session["CaptchaText"] = "iBnikp";
            }
            Session["CaptchaText"] = Common.RandomString(6);
            Captcha ci = new Captcha();
            System.Drawing.Bitmap objbm = ci.MakeCaptchaImage((string)Session["CaptchaText"], 120, 35, "Times New Roman");
            objbm.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            objbm.Dispose();
            Response.End();
    }
</script>