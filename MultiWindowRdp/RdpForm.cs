using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AxMSTSCLib;

namespace MultiWindowRdp
{
    public partial class RdpForm : Form
    {
        string Server;
        List<Control> masks = new List<Control>();
        Size previousSize;
        Point previousLocation;
        List<bool> ActiveScreens = new List<bool>();
        public RdpForm(List<bool> activeScreens, string server)
        {
            InitializeComponent();
            ActiveScreens = activeScreens;
            Server = server;
        }

        private void RdpForm_Load(object sender, EventArgs e)
        {
            //specify the server the VM is running on
            axMsRdpClient9NotSafeForScripting2.Server = Server;

            //enable relative mouse mode and smart sizing
            axMsRdpClient9NotSafeForScripting2.AdvancedSettings7.RelativeMouseMode = true;
            axMsRdpClient9NotSafeForScripting2.AdvancedSettings7.SmartSizing = true;
            axMsRdpClient9NotSafeForScripting2.AdvancedSettings7.DisplayConnectionBar = true;
            axMsRdpClient9NotSafeForScripting2.AdvancedSettings7.BitmapPeristence = 1;
            axMsRdpClient9NotSafeForScripting2.AdvancedSettings8.EnableAutoReconnect = true;
            axMsRdpClient9NotSafeForScripting2.AdvancedSettings8.RedirectClipboard = true;
            axMsRdpClient9NotSafeForScripting2.OnRequestLeaveFullScreen += AxMsRdpClient9NotSafeForScripting2_OnRequestLeaveFullScreen;
            axMsRdpClient9NotSafeForScripting2.AdvancedSettings.ContainerHandledFullScreen = 1;
            axMsRdpClient9NotSafeForScripting2.Dock = DockStyle.Fill;
            //specify the authentication service - this is required and set the authentication level

            axMsRdpClient9NotSafeForScripting2.AdvancedSettings6.AuthenticationLevel = 2;

            //retrieve the activeX control and enable CredSSP and disable NegotiateSecurity
            MSTSCLib.IMsRdpClientNonScriptable4 Ocx =
                (MSTSCLib.IMsRdpClientNonScriptable4)axMsRdpClient9NotSafeForScripting2.GetOcx();
            Ocx.EnableCredSspSupport = false;



            MSTSCLib.IMsRdpClientNonScriptable5 settt =
                (MSTSCLib.IMsRdpClientNonScriptable5)axMsRdpClient9NotSafeForScripting2.GetOcx();
            if (ActiveScreens.Count > 1)
            {
                settt.UseMultimon = true;
            }
            
            //retrieve the activeX control and disable CredentialsDelegation
            MSTSCLib.IMsRdpExtendedSettings rdpExtendedSettings =
                (MSTSCLib.IMsRdpExtendedSettings)axMsRdpClient9NotSafeForScripting2.GetOcx();
            object True = true;
            rdpExtendedSettings.set_Property("DisableCredentialsDelegation", ref True);

            //connect to the VM
            axMsRdpClient9NotSafeForScripting2.Connect();
        }

        private void AxMsRdpClient9NotSafeForScripting2_OnRequestLeaveFullScreen(object sender, EventArgs e)
        {
            axMsRdpClient9NotSafeForScripting2.FullScreen = false;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.Size = previousSize;
            this.Location = previousLocation;
        }

        const int WM_SYSCOMMAND = 0x0112;
        const int SC_MAXIMIZE = 0xF030;
        const int WM_NCHITTEST = 0x0084;
        const int WM_LBUTTONUP = 0x0202;

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_SYSCOMMAND)
            {
                if ((int)m.WParam == SC_MAXIMIZE)
                {
                    this.FormBorderStyle = FormBorderStyle.None;
                    previousSize = this.Size;
                    previousLocation = this.Location;
                    this.Size = SystemInformation.VirtualScreen.Size;
                    this.Location = SystemInformation.VirtualScreen.Location;
                    axMsRdpClient9NotSafeForScripting2.FullScreen = true;
                    var i = 0;
                    var path = new GraphicsPath();
                    path.AddRectangle(new Rectangle(new Point(0,0), this.Size));
                    foreach(bool active in ActiveScreens)
                    {
                        if (!active)
                        {
                            path.AddRectangle(new Rectangle(new Point(Screen.AllScreens[i].Bounds.X - SystemInformation.VirtualScreen.X, Screen.AllScreens[i].Bounds.Y - SystemInformation.VirtualScreen.Y), Screen.AllScreens[i].Bounds.Size));
                        }
                        i++;
                    }
                    this.Region = new Region(path);
                    return;
                }
            }
            base.WndProc(ref m);
        }


    }
}
