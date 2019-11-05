
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace convolutiion
{
    public partial class mtrfrm : MetroFramework.Forms.MetroForm
    {
        public mtrfrm(List<Double[]> inpt,List<Double[]> outp)
        {
            InitializeComponent();
            inp = inpt;
            this.outp = outp;
            int cntr = 0;
            DialogResult = DialogResult.Abort;
            foreach (var item in inpt)
            {
                Color clr = Color.Black;
                if (outp[cntr][0] > 0.5)
                {
                    clr = Color.Green;
                }
                else
                {
                    clr = Color.Blue;
                }
                SPanel pane = new SPanel(true);
                pane.handle += (fg, sd) => { panel1.Controls.Remove(pane); };
                pane.blc = clr;
                panel1.Controls.Add(pane);
                pane.Size = new Size(15, 15);
                pane.Location = new Point((int)(item[0] * Width),(int)( item[1] * Height));
            
        
                cntr += 1; 
            }
        }
        public void vdl()
        {

        }
      
        private void mtrfrm_Load(object sender, EventArgs e)
        {

        }
        public List<Double[]> inp= new List<double[]>();
        public Bitmap img;
        public List<Double[]> outp=new List<double[]>();
        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {





            if (panel1.Controls.Count < 0)
            {
                MessageBox.Show("Please insert some data before running started");
                return;
            }
            DialogResult = DialogResult.OK;
            img = new Bitmap(panel1.Width, panel1.Height);
            panel1.DrawToBitmap(img, panel1.ClientRectangle);
            Close();
        }

        private void panel1_Click(object sender, EventArgs e)
        {

        }
        Thread th;
        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            List<Double> input = new List<double>();
            List<Double> output = new List<double>();
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {SPanel pane = new SPanel(true);
            pane.handle += (fg, sd) => {  panel1.Controls.Remove(pane); };
                pane.blc = Color.Green;
                panel1.Controls.Add(pane);
                pane.Size = new Size(15,15);
                pane.Location = e.Location;
                input.Add((double)e.Location.X / (double)Width); input.Add((double)e.Location.Y / (double)Height);
                output.Add(1);
                inp.Add(input.ToArray());
                outp.Add(output.ToArray());
            } 
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                SPanel pane = new SPanel(true);
                pane.handle += (fg, sd) => { panel1.Controls.Remove(pane); };
                pane.blc = Color.Blue;
                panel1.Controls.Add(pane);
                pane.Size = new Size(15, 15);
                pane.Location = e.Location;
                input.Add((double)e.Location.X / (double)Width); input.Add((double)e.Location.Y / (double)Height);
                output.Add(0);
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
            if (count >= 10)
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
                        pane.blc = Color.Green;
                        panel1.Controls.Add(pane);
                        pane.Size = new Size(15, 15);
                        pane.Location = e.Location;
                        input.Add((double)e.Location.X / (double)Width); input.Add((double)e.Location.Y / (double)Height);
                        output.Add(1);
                        inp.Add(input.ToArray());
                        outp.Add(output.ToArray());
                    }
                    if (e.Button == System.Windows.Forms.MouseButtons.Right)
                    {
                        SPanel pane = new SPanel(true);
                        pane.handle += (fg, sd) => { panel1.Controls.Remove(pane); };
                        pane.blc = Color.Blue;
                        panel1.Controls.Add(pane);
                        pane.Size = new Size(15, 15);
                        pane.Location = e.Location;
                        input.Add((double)e.Location.X / (double)Width); input.Add((double)e.Location.Y / (double)Height);
                        output.Add(0);
                        inp.Add(input.ToArray());
                        outp.Add(output.ToArray());
                    }
                }
                count = 0;
             
            }
            
            count += 1;
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            inp.Clear();
            outp.Clear();
            panel1.Controls.Clear();
        }
    }
}
