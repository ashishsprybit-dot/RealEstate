using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Drawing.Drawing2D;

public partial class On_Fly_Thumb : System.Web.UI.Page
{
    int Width = 0;
    int Height = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        string image = string.Empty;

        if (Request["path"] != null && File.Exists(Server.MapPath(Request["path"])))
            image = Request["path"];
        else
            image = "noimage.jpg";

        if (Request["width"] != null)
            Width = Int32.Parse(Request["width"]);

        if (Request["height"] != null)
            Height = Int32.Parse(Request["height"]);

        string Path = Server.MapPath(Request.ApplicationPath) + "\\" + image.Replace('/', '\\');

        if(!File.Exists(Path))
        {
            Path = Server.MapPath(Request.ApplicationPath) + "\\images\\user.jpg" ;
        }

        Bitmap bmp = null;
        if (Request["percentage"] != null)
        {
            bmp = CreateThumbnailInPercentage(Path, Convert.ToInt32(Request["percentage"]));
        }
        else
        {
             bmp = CreateThumbnail(Path, Width, Height);
        }

        if (bmp == null)
        {
            this.ErrorResult();
            return;
        }

        // Put user code to initialize the page here
        EncoderParameters ep = new EncoderParameters();
        ep.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);
        //newBitMap.Save(Response.OutputStream, getImageCodeInfo("image/jpeg"), ep);
        Response.ContentType = "image/jpeg";
        bmp.Save(Response.OutputStream, getImageCodeInfo("image/jpeg"), ep);
        bmp.Dispose();
    }

    /// Creates a resized bitmap from an existing image on disk.
    /// Call Dispose on the returned Bitmap object
    private Bitmap CreateThumbnail(string lcFilename, int lnWidth, int lnHeight)
    {
        System.Drawing.Bitmap bmpOut = null;
        try
        {
            Bitmap loBMP = new Bitmap(lcFilename);
            int upWidth = loBMP.Width;
            int upHeight = loBMP.Height;
            int newX = 0, newY = 0;  // To set the new top left drawing position on the image canvas
            decimal reDuce = 0;
            int newWidth = 0, newHeight = 0;

            newHeight = getHeight(upWidth, upHeight);
            if (Height > 0)
                newHeight = Height;
            else
                Height = newHeight;
            if (Width > 0)
                newWidth = Width;
            else
            {
                Width = getWidth(upWidth, upHeight);
                newWidth = Width;
            }

            bmpOut = new Bitmap(newWidth, newHeight);
            if (upWidth > upHeight)  //Landscape picture
            {
                if (Request["autoheight"] == null)
                {
                    reDuce = Convert.ToDecimal((newWidth * 1.0) / upWidth);
                    newHeight = Convert.ToInt32(upHeight * reDuce);
                    newY = Convert.ToInt32((Height - newHeight) / 2);
                    newX = 0;
                }
            }
            else if (upWidth < upHeight)  //Portrait picture
            {
                reDuce = Convert.ToDecimal((newHeight * 1.0) / upHeight);
                newWidth = Convert.ToInt32(upWidth * reDuce);
                newX = Convert.ToInt32((Width - newWidth) / 2);
                newY = 0;
            }
            else if (upWidth == upHeight)  //Square picture
            {
                reDuce = Convert.ToDecimal((newHeight * 1.0) / upHeight);
                newWidth = Convert.ToInt32(upWidth * reDuce);
                newX = Convert.ToInt32((Width - newWidth) / 2);
                newY = Convert.ToInt32((Height - newHeight) / 2);
            }

            //Graphics g = Graphics.FromImage(bmpOut);

            //g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            ////g.FillRectangle(Brushes.White, 0, 0, lnNewWidth, lnNewHeight);
            //g.DrawImage(loBMP, newX, newY, newWidth, newHeight);
            //loBMP.Dispose();
            //g.Dispose();

            Graphics g = Graphics.FromImage(bmpOut);
            g.Clear(Color.White);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            g.DrawImage(loBMP, newX, newY, newWidth, newHeight);
            loBMP.Dispose();
            g.Dispose();

        }
        catch { return null; }
        return bmpOut;
    }

    // Return image height based on main image and requested width.
    private int getHeight(int iwidth, int iheight)
    {
        double result = 0;
        if (iheight > Height)
        {
            result = (Width * iheight);
            result = (result / iwidth);
            return Convert.ToInt32(result);
        }
        return iheight;
    }

    // Return image width based on main image and requested height.
    private int getWidth(int iwidth, int iheight)
    {
        double result = 0;
        if (iwidth > Width)
        {
            result = Height * iwidth;
            result = result / iheight;
            return Convert.ToInt32(result);
        }
        return iwidth;
    }

    /// <summary>
    /// Returns the first ImageCodeInfo instance with the specified mime type. Some people try to get the ImageCodeInfo instance by index - sounds rather fragile to me.
    /// </summary>
    /// <param name="mimeType"></param>
    /// <returns></returns>
    private static ImageCodecInfo getImageCodeInfo(string mimeType)
    {
        ImageCodecInfo[] info = ImageCodecInfo.GetImageEncoders();
        foreach (ImageCodecInfo ici in info)
            if (ici.MimeType.Equals(mimeType, StringComparison.OrdinalIgnoreCase))
                return ici;
        return null;
    }

    private void ErrorResult()
    {
        Response.Clear();
        Response.StatusCode = 404;
        Response.End();
    }

    private Bitmap CreateThumbnailInPercentage(string lcFilename, int Percent)
    {
        float nPercent = ((float)Percent / 100);
        Bitmap imgPhoto = new Bitmap(lcFilename);
        int sourceWidth = imgPhoto.Width;
        int sourceHeight = imgPhoto.Height;
        int sourceX = 0;
        int sourceY = 0;

        int destX = 0;
        int destY = 0;
        int destWidth = (int)(sourceWidth * nPercent);
        int destHeight = (int)(sourceHeight * nPercent);

        Bitmap bmPhoto = new Bitmap(destWidth, destHeight);
        bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

        Graphics grPhoto = Graphics.FromImage(bmPhoto);
       // grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
        grPhoto.Clear(Color.White);
        grPhoto.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        grPhoto.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
        grPhoto.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
        grPhoto.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

        grPhoto.DrawImage(imgPhoto,
            new Rectangle(destX, destY, destWidth, destHeight),
            new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
            GraphicsUnit.Pixel);

        grPhoto.Dispose();
        return bmPhoto;

      
    }
}
