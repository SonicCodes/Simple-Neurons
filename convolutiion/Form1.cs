using OpenTK;
using OpenTK.Graphics.OpenGL;
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
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        public Form1()
        {
            InitializeComponent();
        }
      
        private void Form1_Load(object sender, EventArgs e)
        {
       
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
          
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            Hide();
  NNL nnl = new NNL(numericUpDown1.Value,numericUpDown2.Value,numericUpDown4.Value,(int)numericUpDown3.Value);
            nnl.ShowDialog();
            Show();
        }

        private void bunifuCheckbox1_OnChange(object sender, EventArgs e)
        {
            if (bunifuCheckbox1.Checked == false)
            {
                numericUpDown2.Value = 0;
                numericUpDown2.Enabled = false;
            }
            else
            {
                numericUpDown2.Enabled = true;
            }
        }
    }
}
