using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.WebSockets;
using System.IO;

namespace VNN
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Website = new PanWebsite("http://192.168.0.111:2778/", WebsiteLife);
            Website.onWebSocketMessage = OnWebSocketMessage;
            website_started = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //
        }

        private void BtnStartWebsite_Click(object sender, EventArgs e)
        {
            if (!website_started)
            {
                Website.Start();
                website_started = true;
                console1.AppendText("website started\n");
            }
            else
            {
                MessageBox.Show("Website is already started!");
            }
        }

        private void BtnStopWebsite_Click(object sender, EventArgs e)
        {
            if (website_started)
            {
                Website.Stop();
                website_started = false;
                console1.AppendText("website stopped\n");
            }
            else
            {
                MessageBox.Show("Website is already stopped!");
            }
        }

        private void BtnOpenHtml_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://192.168.0.111:2778");
        }

        private void BtnSendMsgUsingWebsocket_Click(object sender, EventArgs e)
        {
            string message = tbMessageToWebsocket.Text;
            Website.WebSocketSend(message);
            console1.AppendText($"<-{message}\n");
        }

        private void BtnSelectFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            string fpath = openFileDialog1.FileName;
            Network = new NN(4, 3, 2, .1);
            MessageBox.Show(fpath);
        }
    }
}
