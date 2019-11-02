using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace convolutiion
{
    public partial class SPanel : Panel
    {
        public EventHandler handle;
        public Bitmap onprint;
        public bool nodraw;
        public SPanel(bool network = false,Bitmap printed = null,bool undraw = false)
        {
            InitializeComponent();
            onprint = printed;
            nodraw = undraw;
            if (network)
            {
                this.Click += (df,dff)=>{
                    try
                    {
    handle(null,null);
                    }
                    catch (Exception)
                    {
                        
                    
                    }
                
                };
            }
        }
        private Color _blc = Color.Black;
  
 
        public Color blc { get{ return this._blc; } set { _blc = value; Invalidate(); } }
        protected override void OnPaint(PaintEventArgs e)
        {

            Graphics g = e.Graphics;
           
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.FillRoundedRectangle(new SolidBrush(_blc), 2, 2, this.Width -2, this.Height -2, 90);
            if (onprint != null)
            {
                g.DrawImage(onprint, new Rectangle(0, 0, Width , Height ));
            }
      }

        private void SPanel_Resize(object sender, EventArgs e)
        {
    
        }
       
    }
}
