using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNN
{
    public class NNWebModel
    {
        public List<List<object>> neurons;
        public bool is_learning;
        public NNWebModel(NN nn, Form1 form)
        {
            neurons = new List<List<object>>();
            foreach (Neuron[] layer in nn.Network)
            {
                List<object> model_layer = new List<object>();

                foreach (Neuron neuron in layer)
                {
                    var model_weights = new List<object>();
                    foreach (var weight in neuron.Weights)
                    {
                        model_weights.Add(new {
                            value = weight,
                            element = false
                        });
                    }
                    object model_neuron = new
                    {
                        value = neuron.Value,
                        element = false,
                        weights = model_weights
                    };
                    model_layer.Add(model_neuron);
                }

                neurons.Add(model_layer);

                this.is_learning = form.is_learning;
            }
        }
    }

    public class FileModel
    {
        public string data_main_html_file;
        public string[] website_prefixes;

        public uint nn_layer_count; //only hidden
        public uint nn_neurons_count; //per hidden layer, with bias
        public uint nn_inputs_count;
        public double nn_learning_rate;

        public uint ws_neurons_distance;
        public uint ws_layers_distance;
        public uint ws_neuron_side;
        public uint ws_neuron_border;
        public uint ws_weight_line_width;
        public uint ws_weight_number_size;

        public FileModel_NN_Learning_Kit[] nn_learning_data;
        public uint nn_sleep_between_learning;

        public List<string[]> data_real_url_pathes = new List<string[]>();
    }
    public class FileModel_NN_Learning_Kit
    {
        public double output;
        public double[] inputs;
    }
}
