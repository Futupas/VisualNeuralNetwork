﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.WebSockets;
using Newtonsoft.Json;
using System.IO;

namespace VNN
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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
                if (Website == null)
                {
                    console1.AppendText("website is not created. try get data from fie first.\n");
                    return;
                }
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
            System.Diagnostics.Process.Start(DATA.website_prefixes[0]);
            console1.AppendText("web page opened\n");
            //System.Diagnostics.Process.Start("http://192.168.0.111:2778");
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
            string json_data = File.ReadAllText(fpath);
            DATA = JsonConvert.DeserializeObject<FileModel>(json_data);
            console1.AppendText("data received\n");
            Network = new NN((int)DATA.nn_layer_count, (int)DATA.nn_neurons_count, (int)DATA.nn_inputs_count, DATA.nn_learning_rate);
            console1.AppendText("NN created\n");

            if (website_started)
            {
                try
                {
                    Website.Stop();
                }
                finally
                {
                    this.website_started = false;
                    console1.AppendText("website is stopped\n");
                }
            }
                
            Website = new PanWebsite(DATA.website_prefixes, WebsiteLife);
            Website.onWebSocketMessage = OnWebSocketMessage;
            console1.AppendText("website created\n");

            learning_task = new Task(() => {
                while (true)
                {
                    if (this.is_learning)
                    {
                        foreach (var learning_data in DATA.nn_learning_data)
                        {
                            Network.Teach(learning_data.inputs, learning_data.output);
                            Website.WebSocketSend(JsonConvert.SerializeObject(new NNWebModel(Network, this)));
                            if (DATA.nn_sleep_between_learning > 0)
                            {
                                Thread.Sleep((int)DATA.nn_sleep_between_learning);
                            }
                        }
                    }
                    
                }
            });
            learning_task.Start();
        }

        
    }
}
