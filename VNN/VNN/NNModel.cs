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
        public NNWebModel(NN nn)
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
            }
        }
    }
}
