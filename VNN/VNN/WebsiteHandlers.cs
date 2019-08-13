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
                        return PanResponse.ReturnJson(new NNWebModel(Network, this));
                        break;
                    case "nn_start_learning":
                        this.is_learning = true;
                        return PanResponse.ReturnJson(new NNWebModel(Network, this));
                        break;
                    case "nn_stop_learning":
                        this.is_learning = false;
                        return PanResponse.ReturnJson(new NNWebModel(Network, this));
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
                    case "get_result_nn":
                        if (this.is_learning)
                        {
                            return PanResponse.ReturnCode(500, "Neural network is learning. Stop learning to get result.");
                        }
                        else
                        {
                            try
                            {
                                string input_string = request.Data["inputs"];
                                string[] s_inputs = input_string.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                                double[] inputs = new double[s_inputs.Length];
                                for (int i = 0; i < s_inputs.Length; i++)
                                {
                                    inputs[i] = double.Parse(s_inputs[i]);
                                }
                                double result = Network.GetResult(inputs);
                                return PanResponse.ReturnJson(new NNWebModel(Network, this));
                            }
                            catch(Exception ex)
                            {
                                return PanResponse.ReturnCode(500, $"Incorrect inputs. Details:\nMessage:{ex.Message}\nStactTrace:{ex.StackTrace}\nStringException:{ex.ToString()}");
                            }
                        }
                    case "get_result_number":
                        if (this.is_learning)
                        {
                            return PanResponse.ReturnCode(500, "Neural network is learning. Stop learning to get result.");
                        }
                        else
                        {
                            try
                            {
                                string input_string = request.Data["inputs"];
                                string[] s_inputs = input_string.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                                double[] inputs = new double[s_inputs.Length];
                                for (int i = 0; i < s_inputs.Length; i++)
                                {
                                    inputs[i] = double.Parse(s_inputs[i]);
                                }
                                double result = Network.GetResult(inputs);
                                return PanResponse.ReturnContent(result.ToString());
                            }
                            catch (Exception ex)
                            {
                                return PanResponse.ReturnCode(500, $"Incorrect inputs. Details:\nMessage:{ex.Message}\nStactTrace:{ex.StackTrace}\nStringException:{ex.ToString()}");
                            }
                        }
                        break;
                    default:
                        string request_path_segment = request.Address[0];
                        List<string[]> json_records = DATA.data_real_url_pathes.FindAll((string[] e) => { return e[0] == request_path_segment; });
                        if (json_records.Count == 0) return PanResponse.ReturnCode(404);
                        string real_path = json_records[0][1];
                        return PanResponse.ReturnFile(real_path);
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