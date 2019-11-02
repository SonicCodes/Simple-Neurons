using LiveCharts;
using LiveCharts.Wpf;

using MathNet.Numerics.LinearAlgebra;
using System;
using MathNet.Numerics.Statistics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AwesomeShapeControl;
using convolutiion.Properties;

namespace convolutiion
{
    public partial class NNL : MetroFramework.Forms.MetroForm
    {
        int npu = 0;
        int opt = 0;
       
        int epc = 0;
        int layno = 0;
        Matrix<Double> syn0;
        Matrix<Double> syn1;
        Matrix<Double> inp;
        Matrix<Double> outpt;
        public NNL(decimal inp, decimal epch, decimal outp,int layers)
        {
            layno = layers;
            epc = (int)epch;
            npu = (int)inp;
            opt = (int)outp;
          
            InitializeComponent();
            label6.Text += ((int)epch).ToString();

            for (int i = 0; i < layers; i++)
            {
                this.layers.Add(4);
            }
            comboBox1.SelectedIndex = 0;
            shrit();

        }

        Accord.Neuro.Learning.ResilientBackpropagationLearning learner;

        Accord.Neuro.ActivationNetwork activ;
        Microsoft.VisualBasic.PowerPacks.ShapeContainer shp = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
        List<int> layers = new List<int>();
        public void shrit(bool all = true)
        {
List<int> referee = new List<int>();
foreach (var item in layers)
{
    referee.Add(item);
}
referee.Add(opt);

activ = new Accord.Neuro.ActivationNetwork(new Accord.Neuro.ActivationFunctions.GaussianFunction(), npu, referee.ToArray());
       
        
            activ.Randomize();

            learner = new Accord.Neuro.Learning.ResilientBackpropagationLearning(activ);
            activ.SetActivationFunction(new Accord.Neuro.ActivationFunctions.GaussianFunction());
          }
        TableLayoutPanel flay = null;
        TableLayoutPanel llay = null;

        List<Line> slmp = new List<Line>();
        Object[] generatemage(int inputlength,int[] hly,int output){
            Bitmap bmp = new Bitmap(516, 425);
            Graphics grph = Graphics.FromImage(bmp);
            List<Object[]> rec = new List<Object[]>();
                    int hidenstart = 45;
            int ttl = hly.Count()*50;
            int hidenend = ttl + 10;
            int avwidth = bmp.Width - 130;
            int neuwidth =0;
            int neh = 0;
                  if (hly.Count() > 3)
	{
		  neuwidth = avwidth / (hly.Count() + 16);
	}else{
                  neuwidth = 30;
            }
            int ttlleft=100+(hly.Count()*(neuwidth+15))+100;
            int ttltop;

            int inpstart = (bmp.Width/2)-(ttlleft/2);
            int echei = 50;
            int curinp = (425/2)-(((echei+5)*inputlength)/2);
           
           
            int inpfinish = inpstart + 90;
            List<Object[]> inps = new List<Object[]>();
            for (int i = 0; i < inputlength; i++)
            {
                Random rand = new Random(1);
                Color cl = Color.FromArgb(255, rand.Next(0, 200), rand.Next(0, 200), rand.Next(0, 200));
                grph.FillRoundedRectangle(new Pen(cl).Brush, inpstart, curinp, echei, echei, 90);
                inps.Add(new object[]{true,new Rectangle(inpstart, curinp, echei, echei),cl});
                curinp += echei+5;
            }
            rec.Add(inps.ToArray()); 
           
    
      

            int hidleft = inpfinish;
            for (int i = 0; i < hly.Count(); i++)
            {
                curinp = (425 / 2) - ((((neuwidth + 5) * hly[i]) / 2));
                neh = (425 - 16) / hly.Count();
                if (neh > neuwidth)
                {
                    neh = neuwidth;
                }
                else
                {
                    neuwidth = neh;
                }
               inps = new List<Object[]>();
                 for (int ik = 0; ik < hly[i]; ik++)
                 {
                     Random rand = new Random(ik*2);
                     Color cl = Color.FromArgb(255, rand.Next(0, 200), rand.Next(0, 200), rand.Next(0, 200));
                     grph.FillRoundedRectangle(new Pen(cl).Brush, hidleft, curinp, neuwidth, neh, 90);
                     inps.Add(new object[] { true, new Rectangle(hidleft, curinp, neuwidth, neh) ,cl});
                     curinp += neuwidth + 5;
                 }
                 grph.DrawImage(Resources.addneuron,hidleft, curinp, neuwidth, neh);
                 inps.Add(new object[] { false, new Rectangle(hidleft, curinp, neuwidth, neh) });
                 curinp += neuwidth + 5;
                 grph.DrawImage(Resources.deletelayer, hidleft, curinp, neuwidth, neh);
                 inps.Add(new object[] { false, new Rectangle(hidleft, curinp, neuwidth, neh) });
                 curinp += neuwidth + 5;

                 rec.Add(inps.ToArray());
                 hidleft += neuwidth + 16;
             
            }
            int optstart = hidleft+15;
    curinp = (425 / 2) - (((echei + 5) * output) / 2);
    inps = new List<Object[]>();
    int ing = 0;
    for (int i = 0; i < output; i++)
    { 
     
        Random rand = new Random(1);
        Color cl = Color.FromArgb(255, rand.Next(0, 200), rand.Next(0, 200), rand.Next(0, 200));
        grph.FillRoundedRectangle(new Pen(cl).Brush, optstart, curinp, echei, echei, 90); 
        if(ing == 0){
            grph.DrawImage(Resources.addneuron, optstart+8, curinp+8, echei-8, echei-8);
     
            }
              inps.Add(new object[]{true,new Rectangle(optstart, curinp, echei, echei),cl});
        curinp += echei + 5;
        ing += 1;
    }
    rec.Add(inps.ToArray());
   
    grph.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
    grph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
    int wlay = 1;
            int lay0 =0;
    foreach (List<double[]> weightlayer in weights)
    {
        int cneu = 0;
        foreach (var neuron in weightlayer)
        {
      
            Rectangle relo = ((Rectangle)((object[])rec[wlay][cneu])[1]);
            Color mthr =Color.Black;
            try
            {
mthr = ((Color)((object[])rec[wlay][cneu])[2]);
            }
            catch (Exception)
            {
                
            
            }
           
            Point rel = ((Rectangle)((object[])rec[wlay][cneu])[1]).Location;
            rel.Y += relo.Height / 2;
            rel.X += relo.Width / 2;
            int cweight = 0;
            foreach (var item in neuron)
            {
                Rectangle tool = ((Rectangle)((object[])rec[lay0][cweight])[1]);
                Color father = Color.Black;
                try
                {father = ((Color)((object[])rec[lay0][cneu])[2]);

                }
                catch (Exception)
                {
                    
            
                } 
                Point to = ((Rectangle)((object[])rec[lay0][cweight])[1]).Location;
                to.Y += tool.Height / 2;
                to.X += tool.Width / 2;
                grph.DrawLine(new Pen(new System.Drawing.Drawing2D.LinearGradientBrush(to,rel,father,mthr),0.6f+(float)item*2), to, rel);
                   cweight += 1;
           
            }
            cneu += 1;
         }
       
        wlay += 1; lay0 = wlay - 1;
    }
            return new Object[] {bmp,rec };
        }
      
     
   
       List<double> losses = new List<double>();
       int jtgo = 1;
       public void shlose(double inpt)
       {
           Invoke(new vdcal(() => { cartesianChart1.AxisX[0].Labels.Add(jtgo.ToString());
           cartesianChart1.Series[0].Values.Add(inpt); })
           );
          
          
           jtgo += 1;
       }
        private void NNL_Load(object sender, EventArgs e)
        {
            materialRaisedButton3_Click(null,null);
            
            materialTabControl1.SelectedIndex = 0;
           
  cartesianChart1.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Loss",
                    Values = new ChartValues<double> {}
                }
            };
  cartesianChart1.AxisX.Add(new Axis
  {
      Title = "per Steps",
      Labels = new List<string>()
  });
  cartesianChart1.AxisY.Add(new Axis
           {
               Title = "Loss",

           });
  object[] dta = generatemage(npu, layers.ToArray(), opt);
  pictureBox1.Image = ((Bitmap)dta[0]);
  pictureBox1.Tag = dta[1];
            shlose(0);
            
        }
        void updatevis()
        {
            object[] dta = generatemage(npu, layers.ToArray(), opt);
            pictureBox1.Image = ((Bitmap)dta[0]);
            pictureBox1.Tag = dta[1];
        }
      
        int datam = 0;
        delegate void vdcal();
        void run(){
            if (datam <= 1)
            {
                MessageBox.Show("No available data to train the model");

                return;
            }
            else
            {

            }
            if (epc <= 0)
            {  bunifuProgressBar1.Maximum_Value = 5;
                bunifuProgressBar1.Value = 5;
                runm();
            }
            else
            {
           bunifuProgressBar1.Maximum_Value = epc;
           
            runm();
            }
 
    }
      
        List<List<Double[]>> weights = new List<List<double[]>>();
        Thread calcthread;
        #region not neeeded
private void runm()
        {
            materialRaisedButton2.Hide();
            materialRaisedButton5.Show();
            calcthread = new Thread(new ThreadStart(() => {
                if (epc <= 0)
                {int re = 0; int rek = 0; int definete = 0;
                    while (true)
                    {

                        rawin.ToArray();
                        rawout.ToArray();
                   double rn = learner.RunEpoch(inp.ToColumnArrays(), outpt.ToColumnArrays());
              
                        if (re > 200)
                        {
                            materialLabel1.Invoke(new vdcal(() =>
                            {
                                materialLabel1.Text = "Epoch :" + definete.ToString() + " , Loss :" + rn.ToString();

                            })); shlose(rn);
                            re = 0;
                        }
                        definete += 1;
                        if (rek > 50)
                        {
                            rek = 0;
                           weights.Clear();
                            foreach (var item in activ.Layers)
                            { List<Double[]> dbl = new List<Double[]>();
                            foreach (var neur in item.Neurons)
                            {
                                dbl.Add(neur.Weights);
                            }
weights.Add(dbl);
                            }
                            updatevis();
                        }
                        re += 1;
                        rek += 1;
Thread.Sleep(2);
                    }
                }
                else
                {
                    int re = 0;
                    int ret = 0;
                    int rek = 0;
                    for (int i = 0; i < epc; i++)
                    {

                        double rn = learner.RunEpoch(inp.ToColumnArrays(), outpt.ToColumnArrays());
                        if (re > 100)
                        {
shlose(rn);
                            re = 0;
                        }
                        if (rek > 50)
                        {
                            rek = 0;
                             weights.Clear();
                            foreach (var item in activ.Layers)
                            { List<Double[]> dbl = new List<Double[]>();
                            foreach (var neur in item.Neurons)
                            {
                                dbl.Add(neur.Weights);
                            }
weights.Add(dbl);
                            }
                            updatevis();
                        }
                        rek += 1; re += 1;
                        materialLabel1.Invoke(new vdcal(() => {
                            materialLabel1.Text = "Epoch :" + bunifuProgressBar1.Value.ToString() + " , Loss :" + rn.ToString();
               
                        }));
                    
Invoke(new vdcal(() => { bunifuProgressBar1.Value += 1; }));
Thread.Sleep(2);
                  }
                    //for amount
                }
                Invoke(new vdcal(() => {      stop();}));
           
            }));
            calcthread.Start();
            label8.Text = "The Model is still running";
        }
        private void stop()
        {      success();
       
            cartesianChart1.Series.Clear();
            cartesianChart1.AxisX.Clear();
            cartesianChart1.AxisY.Clear();
            NNL_Load(null, null);
            bunifuProgressBar1.Value = 0;
            try
            {
    calcthread.Abort();
            }
            catch (Exception)
            {
            }
        
            materialRaisedButton2.Show();
            materialRaisedButton5.Hide();
         }
        private void success()
        {
            materialRaisedButton3_Click(null,null);
            bunifuProgressBar1.Value = 0;
            materialTabControl1.SelectedTab = tabPage3;
            label8.Text = "Click the button to predict";
            bunifuCards1.Visible = true;
          
            materialRaisedButton2.Show();
                  materialRaisedButton5.Hide();
        
        }
     private void repopulatedata()
        {
            listBox1.Items.Clear();
            for (int im = 0; im < rawin.Count; im++)
            {
                datam += 1;
              inp = Matrix<Double>.Build.Random(npu, im+1);
            outpt = Matrix<Double>.Build.Random(opt, im+1);
            for (int i = 0; i < rawin[im].Count(); i++)
            {
                inp[i, im] = rawin[im][i];
            }
            for (int i = 0; i < rawout[im].Count(); i++)
            {
                outpt[i, im] = rawout[im][i];
            }
       
            }

            if (rawin.Count < 1)
            {

            }
            else
            {

                label5.Hide();
                for (int i = 0; i < rawin.Count; i++)
                {
                    string inptstr = "";
                         string uts = "";
                         int count = 0;
                    foreach (var item in rawin[i])
                    {
                        inptstr += "   "+item.ToString();
                        count += 1;
                    }
                    count = 0;
                       foreach (var item in rawout[i])
                    {
                        uts += "  " + item.ToString();
                        count += 1;
                    }
                    listBox1.Items.Add(inptstr + " | " + uts);
                }
            }
        }
        private void materialRaisedButton2_Click(object sender, EventArgs e)
     {
    
            run();}
List<Double[]> rawin = new List<double[]>();
        List<Double[]> rawout = new List<double[]>();
private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            dataform sfrm = new dataform(npu, opt);
            sfrm.ShowDialog();
            if (sfrm.DialogResult == DialogResult.OK)
            {
                rawin.Add(sfrm.inp);
                rawout.Add(sfrm.outp);

                repopulatedata();
               
            }

        }

        private void materialRaisedButton5_Click(object sender, EventArgs e)
        {
            stop();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (rawin.Count == 0)
            {
                e.Cancel = true;
            }
            if(listBox1.SelectedItem == null){
                e.Cancel = true;
            }
        }
 private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
  
            rawin.RemoveAt(listBox1.SelectedIndex);
            rawout.RemoveAt(listBox1.SelectedIndex);
          

                repopulatedata();

        }

        private void NNL_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
    calcthread.Abort();
            }
            catch (Exception)
            { }
        
        }
        #endregion
        
        #region combo's
private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                switch (comboBox1.SelectedIndex)
                {
                    case 0:
                        activ.SetActivationFunction(new Accord.Neuro.SigmoidFunction());
                        break;
                    case 1:
                        activ.SetActivationFunction(new Accord.Neuro.RectifiedLinearFunction());
                        break;
                    case 2:
                        activ.SetActivationFunction(new Accord.Neuro.ActivationFunctions.BernoulliFunction());
                        break;
                    case 3:
                        activ.SetActivationFunction(new Accord.Neuro.ActivationFunctions.GaussianFunction());
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
                
            
            }
           
        }

        #endregion
#region not needed 2
private void materialRaisedButton3_Click(object sender, EventArgs e)
        {
            activ.Randomize();
         
            weights.Clear();
            foreach (var item in activ.Layers)
            {
                List<Double[]> dbl = new List<Double[]>();
                foreach (var neur in item.Neurons)
                {
                    dbl.Add(neur.Weights);
                }

                weights.Add(dbl);
            }
            
        }

        private void materialRaisedButton6_Click(object sender, EventArgs e)
        {
              dataform sfrm = new dataform(npu, opt,true);
            sfrm.ShowDialog();
            if (sfrm.DialogResult == DialogResult.OK)
            {
                flowLayoutPanel1.Controls.Clear();
              

            
                foreach (var item in     activ.Compute(sfrm.inp))
                {
                      Label lab = new Label();
                lab.BackColor = Color.Black;
                lab.ForeColor = Color.White;
                lab.Text = item.ToString();
                flowLayoutPanel1.Controls.Add(lab);
                }
                bunifuCards1.Size = new Size(435, 250);
            } 
        
        }

        private void materialRaisedButton7_Click(object sender, EventArgs e)
        {
            bunifuCards1.Size = new Size(435, 119);
         
        }

   
#endregion

        private void tabPage1_Paint(object sender, PaintEventArgs e)
        {
         
        }

        void changelay(int layer, bool add,bool deletelayer=false,bool addl = false)
        {
            if (!addl)
            {
                if (!deletelayer)
                {
                    if (add)
                    {
                        if (layers[layer] == 1)
                        {
                            layers.RemoveAt(layer);
                        }
                        else
                        {
                            layers[layer] -= 1;

                        }

                    }
                    else
                    {
                        layers[layer] += 1;
                    }
                }
                else
                {
                    layers.RemoveAt(layer);
                }
            }
            else
            {
                layers.Add(4);
            }
         
           
            shrit(); materialRaisedButton3_Click(null, null);
            object[] dta = generatemage(npu, layers.ToArray(), opt);
            pictureBox1.Image = ((Bitmap)dta[0]);
            pictureBox1.Tag = dta[1];
        }
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            List<object[]> gh = (List<object[]>)pictureBox1.Tag;
            List<object[]> data = new List<object[]>();
            foreach (var item in gh)
            {
                data.Add(item);
            }
   
            data.RemoveAt(0);
            int lay = 0;

            foreach (var item in data)
            {
                int neuc = 0;
                foreach (var neuron in item)
                {
                    Rectangle relo = ((Rectangle)((object[])data[lay][neuc])[1]);
                    if (relo.Contains(e.Location))
                    {
                        if (lay == (data.Count - 1))
                        {
                            if (neuc == 0)
                            {
  changelay(lay, false,false,true);
                            }
                          
                        }
                        else
                        {
 if (neuc == item.Length - 2)
                        {
                            changelay(lay, false);
                        }
                        else if (neuc == item.Length - 1)
                        {
                            changelay(lay, false,true);
                        }else
                        {
                            changelay(lay, true);
                        }
                        }
                       
                    }
                     neuc += 1;
                }
                lay += 1;
            }
        }

    }
}
