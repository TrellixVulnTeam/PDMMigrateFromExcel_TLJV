using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using eDrawingHostControl;

namespace edrawingsPrint
{
    public partial class Form1 : Form
    {
        eDwHost hostContainer = null;        
        
        public Form1()
        {
            InitializeComponent();
            if(null == hostContainer) {
                hostContainer = new eDwHost();
            }
            this.Controls.Add(hostContainer);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (hostContainer != null)                
                {
                    //((Control)hostContainer).Location = new Point(0, 0);
                    //((Control)hostContainer).Size = new System.Drawing.Size(this.Size.Width, this.Size.Height);
                    ((Control)hostContainer).Hide();
                    dynamic emvControl = hostContainer.GetOcx();
                    emvControl.OpenDoc(@"C:\CDI Controlled Documents\Drawings\Part Drawings- Controlled\ft13801.slddrw", false, false, true, "");

                    emvControl.SetPageSetupOptions(EModelView.EMVPrintOrientation.eLandscape, 1, 0, 0, 1, 0, "pdfAutoSave", 0, 0, 0, 0);
                    emvControl.Print5(false, @"drawing.pdf", true, false, true, 1, 0, 0, 0, true, 0, 0, "");
                    
                }
            }
            finally
            {
                //if(hostContainer != null)
                //{
                //    hostContainer.Dispose();
                //}
            }

        }
    }

    partial class eDwHost : System.Windows.Forms.AxHost
    {
        public eDwHost()
            : base("{22945A69-1191-4DCF-9E6F-409BDE94D101}")
        {
            //InitializeComponent();
        }
        private dynamic ocx;
        protected override void AttachInterfaces()
        {
            base.AttachInterfaces();
            try
            {
                if (IntPtr.Size == 8) //64 bit
                {
                    // "Forced compiler error! This code can never work in 32-bit processes since it depends on an ActiveX contronl which is only available in 64-bit."
                    this.ocx = (dynamic)base.GetOcx();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n\r\n" + ex.StackTrace, "Exception loading eModelViewControl");
            }
        }

        public new dynamic GetOcx()
        {
            return (dynamic)base.GetOcx();
        }
    }
}
