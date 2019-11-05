
   using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Security;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.VisualBasic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.Windows.Forms;

namespace convolutiion
{
 

    public class Heatmap
    {
        public Heatmap()
        {
        }
        public Bitmap CreateIntensityMask(Bitmap bSurface, List<HeatPoint> aHeatPoints)
        {
            Graphics DrawSurface = Graphics.FromImage(bSurface);
            DrawSurface.Clear(Color.White);

            foreach (HeatPoint DataPoint in aHeatPoints)
                DrawHeatPoint(DrawSurface, DataPoint, 15);

            return bSurface;
        }

        public void DrawHeatPoint(Graphics Canvas, HeatPoint HeatPoint, int Radius)
        {
            List<Point> CircumferencePointsList = new List<Point>();
            Point CircumferencePoint;
            Point[] CircumferencePointsArray;
            float fRatio = (float)(1.0F / (double)byte.MaxValue);
            int bHalf = (int)(byte.MaxValue / (double)2);
            int iIntensity = HeatPoint.Intensity - ((HeatPoint.Intensity - bHalf) * 2);
            float fIntensity = iIntensity * fRatio;

            for (double i = 0; i <= 360; i += 10)
            {
                CircumferencePoint = new Point();
                CircumferencePoint.X = Convert.ToInt32(HeatPoint.X + Radius * Math.Cos(ConvertDegreesToRadians(i)));
                CircumferencePoint.Y = Convert.ToInt32(HeatPoint.Y + Radius * Math.Sin(ConvertDegreesToRadians(i)));
                CircumferencePointsList.Add(CircumferencePoint);
            }

            CircumferencePointsArray = CircumferencePointsList.ToArray();
            PathGradientBrush GradientShaper = new PathGradientBrush(CircumferencePointsArray);
            ColorBlend GradientSpecifications = new ColorBlend(3);
            GradientSpecifications.Positions = new float[3] { 0, fIntensity, 1 };
            GradientSpecifications.Colors = new Color[3] { Color.FromArgb(0, Color.White), Color.FromArgb(HeatPoint.Intensity, Color.Black), Color.FromArgb(HeatPoint.Intensity, Color.Black) };
            GradientShaper.InterpolationColors = GradientSpecifications;
            Canvas.FillPolygon(GradientShaper, CircumferencePointsArray);
        }

        private double ConvertDegreesToRadians(double degrees)
        {
            double radians = (Math.PI / 180) * degrees;
            return (radians);
        }

        public static Bitmap Colorize(Bitmap Mask, byte Alpha)
        {
            Bitmap Output = new Bitmap(Mask.Width, Mask.Height, PixelFormat.Format32bppArgb);
            Graphics Surface = Graphics.FromImage(Output);
            Surface.Clear(Color.Transparent);
            ColorMap[] Colors = CreatePaletteIndex(Alpha);
            ImageAttributes Remapper = new ImageAttributes();
            Remapper.SetRemapTable(Colors);
            Surface.DrawImage(Mask, new Rectangle(0, 0, Mask.Width, Mask.Height), 0, 0, Mask.Width, Mask.Height, GraphicsUnit.Pixel, Remapper);
            return Output;
        }
        private static ColorMap[] CreatePaletteIndex(byte Alpha)
        {
            Bitmap bit = new Bitmap(256,1);
            Graphics gp = Graphics.FromImage(bit);
            gp.FillRectangle(new LinearGradientBrush(new Point(0, 0), new Point(256, 0), Color.DarkSlateBlue, Color.ForestGreen), new Rectangle(0, 0, 256, 1));
            ColorMap[] OutputMap = new ColorMap[256];
            Bitmap Palette = bit;

            for (int X = 0; X <= 255; X++)
            {
                OutputMap[X] = new ColorMap();
                OutputMap[X].OldColor = Color.FromArgb(X, X, X);
                OutputMap[X].NewColor = Color.FromArgb(Alpha, Palette.GetPixel(X, 0));
            }

            return OutputMap;
        }
    }

    public struct HeatPoint
    {
        public int X;
        public int Y;
        public int Intensity;
        public HeatPoint(int iX, int iY, int bIntensity)
        {
            X = iX;
            Y = iY;
            Intensity = bIntensity;
        }
    }

}
