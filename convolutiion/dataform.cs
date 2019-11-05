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
    public partial class dataform : MetroFramework.Forms.MetroForm
     {
        public Double[] inp;
        public Double[] outp;
        public dataform(int amnt,int omnt,bool check = false)
        {
            InitializeComponent();
            inp = new Double[amnt];
            outp = new Double[omnt];
            if (check)
            {
                flowLayoutPanel2.Hide();
                label2.Hide();
                materialRaisedButton1.Location = new Point(47, 357);
                this.Size = new Size(188, 428);
            }
            for (int i = 0; i < amnt; i++)
            {  

                NumericUpDown upd = new NumericUpDown();  
                TrackBar track = new TrackBar();
                track.Width = upd.Width;
                upd.Maximum = 100;
                upd.Minimum = -1;
                upd.DecimalPlaces = 3;
                flowLayoutPanel1.Controls.Add(upd);
            
            }
            for (int i = 0; i < omnt; i++)
            {
                NumericUpDown upd = new NumericUpDown();
                TrackBar track = new TrackBar();
                track.Width = upd.Width;
                upd.Maximum = 100;
                upd.Minimum = -1;
                upd.DecimalPlaces = 3;
                flowLayoutPanel2.Controls.Add(upd);
            }
        }

        private void dataform_Load(object sender, EventArgs e)
        {

        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            int count = 0;
            foreach (var item in flowLayoutPanel1.Controls)
            {
                try
                {
     inp[count] = (double)((NumericUpDown)item).Value;
                count++;    
                }
                catch (Exception)
                {
                    
               }
           
            }
             count = 0;
            foreach (var item in flowLayoutPanel2.Controls)
            {
                try
                {
    outp[count] = (double)((NumericUpDown)item).Value;
                count++;   
                }
                catch (Exception)
                {
                    
                  
                }
            
            }
            Close();
        }
    }
}
