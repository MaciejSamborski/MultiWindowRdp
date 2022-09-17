using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace MultiWindowRdp
{
    public partial class MainForm : Form
    {

        List<bool> ActiveScreens = new List<bool>();
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            
            var i= 1;
            Screen.AllScreens.ToList().ForEach(display =>
            {
                this.ActiveScreens.Add(true);
                var button = new Button { Name = $"dpbutton{i++}", Text = $"{i - 1}" };
                double widthScale = (double)groupBox1.Width / SystemInformation.VirtualScreen.Width;
                double heightScale = (double)groupBox1.Height / SystemInformation.VirtualScreen.Height;
                button.Top = (int)Math.Floor(display.Bounds.Location.Y * heightScale + SystemInformation.VirtualScreen.Y * heightScale*-1);
                button.Left = (int)Math.Floor(display.Bounds.Location.X * widthScale + SystemInformation.VirtualScreen.X * widthScale*-1);
                button.Width = (int)Math.Floor(display.Bounds.Width * widthScale);
                button.Height = (int)Math.Floor(display.Bounds.Height * heightScale);
                int ii = i - 2;
                button.BackColor = Color.LightBlue;
                button.MouseClick += (a,b) => { ActiveScreens[ii] = !ActiveScreens[ii]; if(ActiveScreens[ii]) button.BackColor = Color.LightBlue; else button.BackColor = Color.Gray; };
                groupBox1.Controls.Add(button);
            }
            );
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IPAddress finalIp;
            var valid = IPAddress.TryParse(textBox1.Text,out finalIp);
            if (valid)
            {
                var rdpForm = new RdpForm(ActiveScreens, finalIp.ToString());
                rdpForm.Text = finalIp.ToString();
                rdpForm.Show();
            }
            else
                MessageBox.Show("Invalid IP Adress");
            
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back)
                return;
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == '.'))
            {
                e.Handled = true;
                return;
            }
            var parts = (((TextBox)sender).Text + e.KeyChar).Split('.');
            bool valid = true;
            foreach(string part in parts) { if(part != string.Empty) if (Int32.Parse(part) > 255) valid = false; };
            if (!valid || parts.Length>4)
                e.Handled = true;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}