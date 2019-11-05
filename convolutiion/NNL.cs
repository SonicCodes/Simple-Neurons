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

using convolutiion.Properties;
using NeuronDotNet.Core.Backpropagation;

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
        drfrm dfrm = null;
       List<Double[]> inp = new List<double[]>();
       List<Double[]> outpt = new List<double[]>();
        public NNL(decimal inp, decimal epch, decimal outp,int layers,string name)
        {
         
            layno = layers;
            epc = (int)epch;
            npu = (int)inp;
            opt = (int)outp;

            InitializeComponent(); Text = name; Refresh();
            label6.Text += ((int)epch).ToString();
            valf =acti.Sigmoid;
            for (int i = 0; i < layers; i++)
            {
                this.layers.Add(4);
            }
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 3;
            shrit();
            metroButton2.Click += (gd, gh) => {
                PictureBox pic = new PictureBox();
                drraw = true;
            
                tabPage2.Controls.Add(pic);  
                pic.Location = listBox1.Location;
              
                pic.Size = listBox1.Size;
                pic.BringToFront();
                mtrfrm mtf = new mtrfrm(this.inp,this.outpt);
              if(mtf.ShowDialog()== DialogResult.OK)
                {
  pic.Image = mtf.img;
                materialRaisedButton1.Visible = false;
                rawin = mtf.inp;
                rawout = mtf.outp;   repopulatedata();
                }
              


            };
            comboBox1.Items.Clear();
            comboBox1.DataSource = Enum.GetValues(typeof(acti));
        }
     
     public   NeuronDotNet.Core.Backpropagation.BackpropagationNetwork netamp;

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
              LinearLayer inputLayer = new LinearLayer(npu);
      
              ActivationLayer last = null;
              for (int i = 0; i < layers.Count; i++)
              {

                  ActivationLayer hiddenLayer = null;
                  switch (valf)
	{
		case acti.Sigmoid:
                           hiddenLayer = new SigmoidLayer(layers[0]);
 break;
case acti.tanh:
 hiddenLayer = new TanhLayer(layers[0]);
 break;
case acti.Logarith:
                           hiddenLayer = new LogarithmLayer(layers[0]);
 break;
case acti.Sine:
                           hiddenLayer = new SineLayer(layers[0]);
 break;
case acti.Linear:
                           hiddenLayer = new LinearLayer(layers[0]);
 break;
default:
 break;
	}
                  if (last == null)
                  {
                      new BackpropagationConnector(inputLayer, hiddenLayer);
                  }
                  else
                  {
                      new BackpropagationConnector(last, hiddenLayer);
                  }

                  last = hiddenLayer;
              }
              ActivationLayer outputLayer = new SigmoidLayer(opt);
              if (last != null)
              {
                  new BackpropagationConnector(last, outputLayer);
              }
              else
              {
                  new BackpropagationConnector(inputLayer,outputLayer);
              }
           
              netamp = new BackpropagationNetwork(inputLayer, outputLayer);
         
          
        
             
     
          }
        Type curfun = typeof(NeuronDotNet.Core.Backpropagation.SigmoidLayer);
        TableLayoutPanel flay = null;
        TableLayoutPanel llay = null;   public acti valf;
        public enum acti
        {
            Sigmoid=0, tanh=1,Logarith=2,Sine=3,Linear=4
        }
     
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
   
            int lay0 =0;
         

                List<List<double[]>> rev = weights;

                foreach (object[][] item in rec)
                {
                    if (lay0 == 0)
                    {
                        lay0 += 1;
                        continue;
                    }
                    int nrn = 0;
                    foreach (object[] neuron in item)
                    {
                        #region f's
  bool fcon =(bool) neuron[0];
                        Rectangle floc =(Rectangle) neuron[1];
                        Color bcl = Color.White;
                        try
                        {
                            bcl = (Color)neuron[2];
                        }
                        catch (Exception)
                        {

                        }
                        Point fmc = floc.Location;
                        fmc.X += floc.Width /2;
                        fmc.Y += floc.Height /2;
                     
                        #endregion
                        if (fcon == false)
                        {
                            continue;
                        }
#region back layer iteration

                        object[][] blayer = (object[][]) rec[lay0 - 1];
                        int bneu = 0;
                        foreach (object[] beu in blayer)
                        {
                            bool bneuron = (bool)beu[0];
                            if (bneuron == false)
                            {
                                continue;
                            }
                            Rectangle brec = (Rectangle)beu[1];
                            Point bmc = brec.Location;
                            bmc.X += brec.Width / 2;
                            bmc.Y += brec.Height / 2;
                            Color fcol = Color.White;
                            try
                            {
                                fcol = (Color)beu[2];
                            }
                            catch (Exception)
                            {
                                
                            }
                            fcol = Color.FromArgb(255,(byte)( fcol.R - 20),(byte)( fcol.G - 20),(byte)( fcol.B - 20));
                            float flt = float.Parse(getweight(lay0,nrn,bneu).ToString())*3;
                            if (flt > 4f)
                            {
                                flt = 4f;
                            }
                            grph.DrawLine(new Pen(new System.Drawing.Drawing2D.LinearGradientBrush(fmc, bmc, fcol, bcl),flt ), fmc, bmc);
                
                            bneu += 1;
                        }
#endregion
                        nrn += 1;
                    }
                    lay0 += 1;
                }
       
    
    foreach (object[][] item in rec)
    {
        foreach (object[] neuron in item)
        {
            bool intg =(bool) neuron[0];
            Color blc = Color.Black;
            try
            {
                blc = (Color)neuron[2];
            }
            catch (Exception)
            {
                
               
            }
            Rectangle recb = (Rectangle)neuron[1];
            if (intg == true)
            {
                grph.FillRoundedRectangle(new Pen(blc).Brush, recb.X,recb.Y,recb.Width,recb.Height,90);
            }

        }

    }
            return new Object[] {bmp,rec };
        }
      
     
   
       List<double> losses = new List<double>();
       int jtgo = 1;
       double getweight(int layer,int neuron,int whc)
       {
           try
           {
    return  weights[layer][neuron][whc];
           }
           catch (Exception)
           {

               return 0.01f;   
           }
     
       }
       public void shlose(double inpt)
       {
           Invoke(new vdcal(() => { cartesianChart1.AxisX[0].Labels.Add(jtgo.ToString());
           cartesianChart1.Series[0].Values.Add(inpt); })
           );
          
          
           jtgo += 1;
       }
        bool drraw = false;
        private void NNL_Load(object sender, EventArgs e)
        {
            listBox1.BackColor = BackColor;
            netamp.JitterNoiseLimit = (double)numericUpDown1.Value;
            if (npu == 2)
            {
  if (opt == 1)
            {
                metroButton2.Visible = true;
                materialRaisedButton6.Size = new Size(274, 39);
            }
            }
          
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
        public double test(double[] net)
        {
            return netamp.Run(net)[0];
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
            {


                metroProgressBar1.Value = 5;
                runm();
            }
            else
            {
                metroProgressBar1.Maximum = epc;
           
            runm();
            }
 
    }
        public void shim(){
            try
            {
      dfrm.shw(netamp);
            }
            catch (Exception)
            {

            
            }
      
        }
      
        List<List<Double[]>> weights = new List<List<double[]>>();
        Thread calcthread;
        
        #region not neeeded
private unsafe void runm()
        {
            try
            {
                dfrm.Close();
                dfrm.Dispose();
            }
            catch (Exception)
            {


            }
            if (drraw)
            {
                dfrm = new drfrm(rawin, rawout);
                dfrm.Show();
            }
            materialRaisedButton2.Hide();
            materialRaisedButton5.Show();
            try
            {
       netamp.EndEpochEvent -= netamp_EndEpochEvent;
            }
            catch (Exception)
            {
                
           
            }
     
            netamp.EndEpochEvent += netamp_EndEpochEvent;
            calcthread = new Thread(new ThreadStart(() => {


                re = 0;
                ret = 0;
                prog = 0;
                rek = 0;
                    runepch(epc);
                

                  
                    //for amount
                
                Invoke(new vdcal(() => {      stop();}));
           
            }));
            calcthread.Start();
            label8.Text = "The Model is still running";
        }
int re = 0;
int ret = 0;
int prog = 0;
int rek = 0;
unsafe void netamp_EndEpochEvent(object sender, NeuronDotNet.Core.TrainingEpochEventArgs e)
{
    double rn = netamp.MeanSquaredError;

    if (re > 1000)
    {
        shlose(rn);
        re = 0;
    }
    if (rek > 50)
    {
        rek = 0;
        
        shim();
        Invoke(new vdcal(() => { metroProgressBar1.Value = prog; })); 
        materialLabel1.Invoke(new vdcal(() =>
        {
            materialLabel1.Text = "Epoch :" + metroProgressBar1.Value.ToString() + " , Loss :" + rn.ToString();

        }));
        weights.Clear(); 
        foreach (var item in netamp.Layers)
        {
            List<Double[]> dbl = new List<Double[]>();
            foreach (var neur in item.Neurons)
            {
                List<double> dblh = new List<double>();
                foreach (var nerspan in neur.TargetSynapses)
                {
                    dblh.Add(nerspan.Weight);
                }
                dbl.Add(dblh.ToArray());
            }
            weights.Add(dbl);
        }
        updatevis();
    }
    rek += 1; re += 1;

    prog += 1;
}
        private void stop()
        {      success();
        try
        {
            dfrm.Close();
            dfrm.Dispose();
        }
        catch (Exception)
        {


        }
            cartesianChart1.Series.Clear();
            cartesianChart1.AxisX.Clear();
            cartesianChart1.AxisY.Clear();

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

            metroProgressBar1.Value = 0;
            try
            {
 
    netamp.StopLearning();   calcthread.Abort();
            }
            catch (Exception)
            {
            }
        
            materialRaisedButton2.Show();
            materialRaisedButton5.Hide();
         }
        private void success()
        {

            metroProgressBar1.Value = 0;
         
        materialTabControl1.SelectedIndex = 2;
             
            

         
            label8.Text = "Click the button to predict";
            panel1.Visible = true;
          
            materialRaisedButton2.Show();
                  materialRaisedButton5.Hide();
        
        }
     private void repopulatedata()
        {
            listBox1.Items.Clear();
             datam = rawout.Count;
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
        public void runepch(int cf){
NeuronDotNet.Core.TrainingSet train = new NeuronDotNet.Core.TrainingSet(npu,opt);
            int ind  = 0;
foreach (var item in rawin)
	{
		    train.Add(new NeuronDotNet.Core.TrainingSample(item,rawout[ind]));
            ind += 1;
	}
             

netamp.Learn(train,cf);
        }
        private void materialRaisedButton2_Click(object sender, EventArgs e)
     {
        
        run();
           }
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

                valf = (acti)comboBox1.SelectedIndex;
                try
                {
                    if (calcthread.IsAlive == true)
                    {
                        calcthread.Abort();
                        shrit();
                        materialRaisedButton2_Click(null, null);
                    }
                    else
                    {
                        shrit();
                      
                    }
                }
                catch (Exception)
                {

                    shrit();
                 
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
        
         
            weights.Clear();
            foreach (var item in netamp.Layers)
            {
                List<Double[]> dbl = new List<Double[]>();
                foreach (var neur in item.Neurons)
                {
                    List<double> dblh = new List<double>();
                    foreach (var nerspan in neur.SourceSynapses)
                    {
                        dblh.Add(nerspan.Weight);
                    }
                    dbl.Add(dblh.ToArray());
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
              

            
                foreach (var item in netamp.Run(sfrm.inp))
                {
                      Label lab = new Label();
                lab.BackColor = Color.Black;
                lab.ForeColor = Color.White;
                lab.Text = item.ToString();
                lab.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);

                flowLayoutPanel1.Controls.Add(lab);
                }
                MetroFramework.Animation.ExpandAnimation expanim = new MetroFramework.Animation.ExpandAnimation();
                expanim.Start(panel1,new Size(435, 270),MetroFramework.Animation.TransitionType.EaseInOutCubic,13);
        
            } 
        
        }

        private void materialRaisedButton7_Click(object sender, EventArgs e)
        {
        
            MetroFramework.Animation.ExpandAnimation expanim = new MetroFramework.Animation.ExpandAnimation();
            expanim.Start(panel1, new Size(445, 134), MetroFramework.Animation.TransitionType.EaseInOutCubic, 13);
        
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
                layers.Add(4);shrit();
             
                
        
        
              
            }


            try
            {
                if (calcthread.IsAlive == true)
                {
                    calcthread.Abort();
                    shrit();
                    materialRaisedButton2_Click(null, null);
                }
                else
                {
                    shrit();

                }
            }
            catch (Exception)
            {

                shrit();

            }
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

        private void metroButton1_Click(object sender, EventArgs e)
        {
            test2d frm = new test2d(netamp);
            frm.ShowDialog();
        }
   
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
     
        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {

       double dbl = double.Parse(((string)comboBox2.Items[comboBox2.SelectedIndex]));
       netamp.SetLearningRate(dbl);

           
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
        
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "CSV FILES(csv)|*.csv";
         
            if (opf.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    foreach (String item in System.IO.File.ReadLines(opf.FileName))
                    {
                        String[] str = item.Split(",".ToCharArray());
                        double[] inps = new double[str.Length];
                        double[] ops = new double[str.Length];
                        if(str.Length - opt != npu ){
                            throw new Exception();
                        }
                         
                        int inf = 0;
                        foreach (string dbl in str)
                        { double db = double.Parse(dbl);
                            
                            if (inf >= npu - 1)
                            {
                                try
                                {
                                    ops[inf - npu] = db;
                                }
                                catch (Exception)
                                {

                                    ops[inf - npu + 1] = db;
                                }
                                continue;
                            }
                            inps[inf] = db;
                            inf += 1;
                        }
                        rawin.Add(inps);
                        rawout.Add(ops);
                        repopulatedata();
                    }
                }
                catch (Exception)
                {
                 
                    MessageBox.Show("please use a compitable CSV format without a column names and like "+
                        "|1.0,6.7,0");
                }
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            netamp.JitterNoiseLimit = (double)numericUpDown1.Value;
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
        
        }

    }
}
