using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace convolutiion
{
    public partial class drfrm : MetroFramework.Forms.MetroForm
    {
        public drfrm(   List<double[]> ins,   List<double[]> ous)
        {
            this.ins = ins;
            this.ous = ous;
            InitializeComponent();        this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.UserPaint |
                          ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.ResizeRedraw |
                          ControlStyles.ContainerControl |
                          ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.SupportsTransparentBackColor
                          , true);
            this.UpdateStyles();
        }
        List<double[]> ins = new List<double[]>();
        List<double[]> ous = new List<double[]>(); 
        Bitmap plat;
        public  void shwh(NeuronDotNet.Core.Backpropagation.BackpropagationNetwork net)
        {

            plat = new Bitmap(Width, Height);
            List<HeatPoint> pt = new List<HeatPoint>();
            int x = 0;
            int y = 0;
            int mh = 0;
            while (true)
            {
                mh += 7;
                if (y > Height)
                {
                    break;
                }
                if (x > Width)
                {
                    x = 0;
                    y += 7;
                    continue;
                }
                if (net.Run(new double[] { (double)x / ((double)Width), (double)y / ((double)Height) })[0] < 0.5)
                {
                    pt.Add(new HeatPoint(x, y, 120));
                }
                x += 1;
            }
        }
  
        public unsafe void shw(NeuronDotNet.Core.Backpropagation.BackpropagationNetwork net)
        {
          
            plat = new Bitmap(Width , Height );
            List<HeatPoint> pt = new List<HeatPoint>();
            int x = 0;
            int y = 0;
            int mh = 0;
            while (true)
            {
                mh += 1;
                if (y > Height)
                {
                    break;
                }
                if (x > Width)
                {
                    x = 0;
                    y += 20;
                    continue;
                }
                double dbl = net.Run(new double[] { (double)x / ((double)Width ), (double)y / ((double)Height) })[0];
                if (dbl < 0.5)
                {
                    pt.Add(new HeatPoint(x, y, (byte)(190 + (int)((dbl*5.00)*30))));
                }
                x += 20;
            }

            Heatmap hm = new Heatmap();
            hm.CreateIntensityMask(plat, pt);
            plat = Heatmap.Colorize(plat, 255);
            Invalidate();
        }
        private void drfrm_Load(object sender, EventArgs e)
        {

        }

        private void drfrm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode= System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
           
            int cl=0;
       
            if (plat != null)
            {
                e.Graphics.DrawImage(plat, new Rectangle(0, 0, Width, Height), new Rectangle(0, 0, Width, Height), GraphicsUnit.Pixel);
            }
              foreach (var item in ins)
            {
                if(ous[cl][0] > 0.5){
                    e.Graphics.FillEllipse(Brushes.DarkGreen, new Rectangle(((int)(item[0] * Width)),(int)( item[1] * Height),10,10));
                }else{
                         e.Graphics.FillEllipse(Brushes.DarkBlue, new Rectangle(((int)(item[0] * Width)),(int)( item[1] * Height),10,10));
                }
                    
           
                    cl += 1;
            }
         

          
        }
    }
}
