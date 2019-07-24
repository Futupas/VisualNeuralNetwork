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
                    case "get_data":
                        object data = new
                        {
                            neurons_distance = DATA.ws_neurons_distance,
                            layers_distance = DATA.ws_layers_distance,
                            neuron_side = DATA.ws_neuron_side,
                            neuron_border = DATA.ws_neuron_border,
                            weight_line_width = DATA.ws_weight_line_width,
                            weight_number_size = DATA.ws_weight_number_size
                        };
                        return PanResponse.ReturnJson(data);
                        break;
                    default:
                        break;
                }

            }

            if (File.Exists(DATA.data_main_html_file))
                return PanResponse.ReturnHtml(DATA.data_main_html_file);
            else
                return PanResponse.ReturnCode(404);
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