using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
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
    public partial class test2d : MetroFramework.Forms.MetroForm
    {
        public  void SetDoubleBuffered(System.Windows.Forms.Control c)
        {
        
            if (System.Windows.Forms.SystemInformation.TerminalServerSession)
                return;

            System.Reflection.PropertyInfo aProp =
                  typeof(System.Windows.Forms.Control).GetProperty(
                        "DoubleBuffered",
                        System.Reflection.BindingFlags.NonPublic |
                        System.Reflection.BindingFlags.Instance);

            aProp.SetValue(c, true, null);
        }
       NeuronDotNet.Core.Backpropagation.BackpropagationNetwork network;
       public test2d(NeuronDotNet.Core.Backpropagation.BackpropagationNetwork net)
        {
            network = net;
            InitializeComponent();
        }
       Bitmap plat;
     
        private void test2d_Load(object sender, EventArgs e)
        {
            SetDoubleBuffered(panel1);
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.UserPaint |
                          ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.ResizeRedraw |
                          ControlStyles.ContainerControl |
                          ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.SupportsTransparentBackColor
                          , true);
            this.UpdateStyles();
            plat = new Bitmap(Width,Height);
     List<HeatPoint> pt = new List<HeatPoint>();
            int x = 0;
            int y = 0;
            int mh = 0;
            while (true)
            {
                mh += 1;
                if (y > panel1.Height)
                {
                   break;
                }
                if (x  > panel1.Width)
                {
                    x = 0;
                    y += 9;
                    continue;
                }
                if (network.Run(new double[] { (double)x / (double)Width, (double)y / (double)Height })[0] < 0.5)
                {
                   pt.Add(new HeatPoint(x, y,70));
               }
                x += 9;
            }
           
                   Heatmap hm = new Heatmap();
                   hm.CreateIntensityMask(plat, pt);
                 plat = Heatmap.Colorize(plat, 255);
        }
        public List<Double[]> inp = new List<double[]>();
        public Bitmap img;
        public List<Double[]> outp = new List<double[]>();
        public void slk()
        {

        }
        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            randomize();
        }Random e = new Random();
        public void randomize()
        {
            panel1.Controls.Clear();
            for (int i = 0; i < 70; i++)
            {
                List<Double> input = new List<double>();
                List<Double> output = new List<double>();
                Point pnt = new Point(e.Next(0, panel1.Width - 10), e.Next(0, panel1.Height - 10));
                SPanel pane = new SPanel(true);
                pane.handle += (fg, sd) => { panel1.Controls.Remove(pane); };
                pane.blc = Color.White;
                SetDoubleBuffered(pane);
                panel1.Controls.Add(pane);
                pane.Size = new Size(15, 15);
                pane.Location = pnt;
                input.Add(pnt.X); input.Add(pnt.Y); output.Add(0);
                inp.Add(input.ToArray());
                outp.Add(output.ToArray());
            }
        }
        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            List<Double> input = new List<double>();
            List<Double> output = new List<double>();
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                SPanel pane = new SPanel(true);
                pane.handle += (fg, sd) => { panel1.Controls.Remove(pane); };
                pane.blc = Color.White;
                panel1.Controls.Add(pane);
                pane.Size = new Size(15, 15);
                pane.Location = e.Location;
                input.Add(e.Location.X); input.Add(e.Location.Y); output.Add(0);
                inp.Add(input.ToArray());
                outp.Add(output.ToArray());
            }
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                SPanel pane = new SPanel(true);
                pane.handle += (fg, sd) => { panel1.Controls.Remove(pane); };
                pane.blc = Color.White;
                panel1.Controls.Add(pane);
                pane.Size = new Size(15, 15);
                pane.Location = e.Location;
                input.Add(e.Location.X); input.Add(e.Location.Y); output.Add(1);
                inp.Add(input.ToArray());
                outp.Add(output.ToArray());
            }
         
        }
        private delegate void cl();
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            up = 0;
        }
        int up = 0;
        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            up = 1;

        }
        int count = 0;
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (count >= 25)
            {
                if (up == 1)
                {

                }
                else
                {
                    List<Double> input = new List<double>();
                    List<Double> output = new List<double>();
                    if (e.Button == System.Windows.Forms.MouseButtons.Left)
                    {
                        SPanel pane = new SPanel(true);
                        pane.handle += (fg, sd) => { panel1.Controls.Remove(pane); };
                        pane.blc = Color.White;
                        panel1.Controls.Add(pane);
                        pane.Size = new Size(15, 15);
                        pane.Location = e.Location;
                        input.Add(e.Location.X); input.Add(e.Location.Y); output.Add(0);
                        inp.Add(input.ToArray());
                        outp.Add(output.ToArray());
                    }
                    if (e.Button == System.Windows.Forms.MouseButtons.Right)
                    {
                        SPanel pane = new SPanel(true);
                        pane.handle += (fg, sd) => { panel1.Controls.Remove(pane);

                        inp.IndexOf(new double[] { });
                        
                        };
                        pane.blc = Color.White;
                        panel1.Controls.Add(pane);
                        pane.Size = new Size(15, 15);
                        pane.Location = e.Location;
                        input.Add(e.Location.X); input.Add(e.Location.Y); output.Add(1);
                        inp.Add(input.ToArray());
                        outp.Add(output.ToArray());
                    }
                }
                count = 0;

            }

            count += 1;
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {

      
                foreach (SPanel item in panel1.Controls)
                {
                    double output = network.Run(new double[] { (double)item.Left / (double)Width, (double)item.Top / (double)Height })[0];
                    if (output < 0.5)
                    {
                        item.blc = Color.Blue;
                    }
                    else
                    {
                        item.blc = Color.Green;
                    }

                  
                }
     

     
        }
        public Color GetColor(int rangeStart, int rangeEnd, int actualValue)
        {
            if (rangeStart >= rangeEnd) return Color.Black;

            int max = rangeEnd - rangeStart; // make the scale start from 0
            int value = actualValue - rangeStart; // adjust the value accordingly

            int green = (255 * value) / max; // calculate green (the closer the value is to max, the greener it gets)
            int red = 255 - green; // set red as inverse of green

            return Color.FromArgb(255,(Byte)0, (Byte)red, (Byte)green);
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
        }
        bool shn = false;
        private void metroButton4_Click(object sender, EventArgs e)
        {
            if (shn)
            {
                metroButton4.Text = "Show Blue-Green Map";
                panel1.BackgroundImage = null;
                shn = false;
            }
            else
            {
                metroButton4.Text = "Hide Blue-Green Map";
                panel1.BackgroundImage = plat;
                shn = true;
            }
        }
    }
}
