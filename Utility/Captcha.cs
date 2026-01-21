using System;
using System.Drawing;
using System.Drawing.Drawing2D;

public class Captcha
{
    public Bitmap MakeCaptchaImage(string txt, int width, int hight, string fontFamilyName)
    {
        SizeF ef2;
        Font font;
        Bitmap image = new Bitmap(width, hight);
        Graphics graphics = Graphics.FromImage(image);
        graphics.SmoothingMode = SmoothingMode.HighQuality;
        RectangleF rect = new RectangleF(0f, 0f, (float) width, (float) hight);
        Brush brush = new HatchBrush(HatchStyle.Percent90 , Color.FromArgb(65, 63, 63), Color.FromArgb(65, 63, 63));
         
        graphics.FillRectangle(brush, rect);
        float emSize = width + 2;
        do
        {
            emSize--;
            font = new Font(fontFamilyName, emSize, FontStyle.Bold, GraphicsUnit.Pixel);
            ef2 = graphics.MeasureString(txt, font);
        }
        while (ef2.Width > width);
        StringFormat format = new StringFormat {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };
        GraphicsPath path = new GraphicsPath();
        path.AddString(txt, font.FontFamily, 1, font.Size, rect, format);
        Random random = new Random();
        PointF[] destPoints = new PointF[] { new PointF(((float) random.Next(width)) / 4f, ((float) random.Next(hight)) / 4f), new PointF(width - (((float) random.Next(width)) / 4f), ((float) random.Next(hight)) / 4f), new PointF(((float) random.Next(width)) / 4f, hight - (((float) random.Next(hight)) / 4f)), new PointF(width - (((float) random.Next(width)) / 4f), hight - (((float) random.Next(hight)) / 4f)) };
        Matrix matrix = new Matrix();
        path.Warp(destPoints, rect, matrix, WarpMode.Perspective, 0f);
        brush = new HatchBrush(HatchStyle.Percent90, Color.White, Color.White);
        graphics.FillPath(brush, path);
        path.Dispose();
        brush.Dispose();
        font.Dispose();
        graphics.Dispose();
        return image;
    }
}

