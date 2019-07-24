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
        PanWebsite Website;
        private bool website_started;
        public PanResponse WebsiteLife(PanRequest request)
        {
            if (request.Address.Length >= 1)
            {
                switch (request.Address[0])
                {
                    case "get_nn":
                        return PanResponse.ReturnJson(new NNWebModel(Network));
                        break;
                    case "nn":
                        if (File.Exists("nn.html"))
                            return PanResponse.ReturnHtml("nn.html");
                        else
                            return PanResponse.ReturnHtml(@"D:\PROJECTS\VisualNeuralNetwork\localhost\nn.html");
                        break;
                    default:
                        break;
                }

            }
            if (File.Exists("index.html"))
                return PanResponse.ReturnHtml("index.html");
            else
                return PanResponse.ReturnHtml(@"D:\PROJECTS\VisualNeuralNetwork\localhost\index.html");
        }

        public void OnWebSocketMessage(WebSocket ws, string message)
        {
            Invoke(new Action(() =>
            {
                console1.AppendText($"->{message}\n");
            }));
        }
    }
}