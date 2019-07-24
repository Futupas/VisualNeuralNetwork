using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNN
{
    public class NN
    {
        public Neuron[][] Network;
        public Random random = new Random();
        protected readonly int layers_count;
        protected readonly int neurons_count;
        protected readonly int inputs_count;
        protected readonly double learning_rate;
        public NN(int layers_count, int neurons_count, int inputs_count, double learning_rate)
        {
            this.layers_count = layers_count;
            this.neurons_count = neurons_count;
            this.inputs_count = inputs_count;
            this.learning_rate = learning_rate;

            this.Network = new Neuron[layers_count + 2][];
            Network[0] = new Neuron[inputs_count];
            for (int i = 1; i < Network.Length - 1; i++)
            {
                Network[i] = new Neuron[neurons_count];
            }
            Network[Network.Length - 1] = new Neuron[1];

            for (int j = 0; j < Network[0].Length; j++) //i - layer, j - neuron in layer
            {
                Network[0][j] = new Neuron(false, 0, random);
            }
            for (int i = 1; i < Network.Length - 1; i++) // for each hidden layer
            {
                Network[i][0] = new Neuron(true, 0, random); //bias neuron (first in all hidden layers
                //Network[i][0] = new Neuron(true, Network[i - 1].Length); //bias neuron (first in all hidden layers
                for (int j = 1; j < Network[i].Length; j++) //i - layer, j - neuron in layer
                {
                    Network[i][j] = new Neuron(false, Network[i - 1].Length, random);
                }
            }
            Network[Network.Length - 1][0] = new Neuron(false, Network[Network.Length - 2].Length, random); //out neuron


        }
        public double GetResult(double[] inputs)
        {
            Network[0][0].Value = 1;
            for (int i = 1; i < Network[0].Length; i++)
            {
                Network[0][i].Value = inputs[i - 1];
            }
            for (int i = 1; i < Network.Length; i++)
            {
                double[] previous_layer_values = new double[Network[i - 1].Length];
                for (int j = 0; j < Network[i - 1].Length; j++) //i - layer, j - neuron in layer
                {
                    previous_layer_values[j] = Network[i - 1][j].Value; // setting previous layer's values
                }
                for (int j = 0; j < Network[i].Length; j++) //i - layer, j - neuron in layer
                {
                    Network[i][j].Value = Network[i][j].GetResult(previous_layer_values);
                }
            }

            return Network[Network.Length - 1][0].Value;
        }

        public void Teach2(double[] inputs, double expected_output)
        {
            double actual_output = this.GetResult(inputs);
            // output neuron
            ref Neuron out_neuron = ref this.Network[Network.Length - 1][0];
            out_neuron.weights_delta = out_neuron.Value * (1 - out_neuron.Value) * (expected_output - actual_output);
            for (int i = 0; i < out_neuron.Weights.Length; i++)
            {
                double prev_neuron_value = Network[Network.Length - 2][i].Value;
                out_neuron.Weights[i] += (learning_rate * out_neuron.weights_delta * prev_neuron_value);
            }
            // hidden layers
            for (int i = Network.Length - 2; i >= 1; i--) // от предпоследнего до второго (первого скрытого)
            {
                ref Neuron[] layer = ref Network[i];
                for (int j = 0; j < layer.Length; j++) // each neuron in layer
                {
                    ref Neuron neuron = ref layer[j];
                    //if (neuron.is_bias) continue;
                    double weights_delta = 0;
                    for (int ej = 0; ej < Network[i + 1].Length; ej++) // for each neuron in next layer
                    {
                        ref Neuron neuron_in_next_layer = ref Network[i + 1][ej];
                        if (neuron_in_next_layer.is_bias) continue;
                        weights_delta += (neuron_in_next_layer.weights_delta * neuron_in_next_layer.Weights[j]);
                    }
                    neuron.weights_delta = weights_delta;

                    for (int wj = 0; wj < neuron.Weights.Length; wj++)
                    {
                        neuron.Weights[wj] += (learning_rate * weights_delta * Network[i - 1][wj].Value);
                    }
                }
            }

        }

        public void Teach(double[] inputs, double expected_output)
        {
            double actual_output = this.GetResult(inputs);
            // output neuron
            ref Neuron out_neuron = ref this.Network[Network.Length - 1][0];
            out_neuron.weights_delta = expected_output - actual_output;

            for (int i = Network.Length - 2; i >= 1; i--)
            {
                ref Neuron[] layer = ref Network[i];
                ref Neuron[] next_layer = ref Network[i + 1];
                for (int j = 0; j < layer.Length; j++)
                {
                    double new_w_delta = 0;
                    for (int nj = 0; nj < next_layer.Length; nj++)
                    {
                        if (next_layer[nj].is_bias) continue;
                        new_w_delta += (next_layer[nj].weights_delta * next_layer[nj].Weights[j]);
                    }
                    layer[j].weights_delta = new_w_delta;
                }
            }

            for (int i = 1; i < Network.Length; i++)
            {
                ref Neuron[] layer = ref Network[i];
                ref Neuron[] prev_layer = ref Network[i - 1];
                for (int j = 0; j < layer.Length; j++)
                {
                    ref Neuron neuron = ref layer[j];
                    if (neuron.is_bias) continue;

                    for (int wj = 0; wj < neuron.Weights.Length; wj++)
                    {
                        neuron.Weights[wj] =
                            neuron.Weights[wj] + (neuron.weights_delta * neuron.Value * (1 - neuron.Value) * prev_layer[wj].Value * learning_rate);
                    }

                }
            }


        }


        protected static double Derivative(double sigmoid_x) // derivative of sigmoid function
        {
            //double sigmoid_x = 1 / (1 + Math.Exp(x * -1));
            return sigmoid_x * (1 - sigmoid_x);
        }


    }

    public class Neuron
    {
        public double[] Weights; //input 
        public double Value;
        public bool is_bias = false;
        public double weights_delta;
        public double x = 0;
        public double GetResult(double[] values)
        {
            if (this.is_bias)
            {
                return 1;
            }
            else
            {
                double x = 0;
                for (int i = 0; i < values.Length; i++)
                {
                    x += (values[i] * this.Weights[i]);
                }
                this.x = x;
                return this.ActivationFunction(x);
            }
        }

        public double ActivationFunction(double x) // Sigmoid
        {
            return 1 / (1 + Math.Exp(x * -1));
        }

        public Neuron(bool is_bias, double[] input_weights, Random random)
        {
            this.is_bias = is_bias;
            this.Weights = this.is_bias ? new double[0] : input_weights;
        }
        public Neuron(bool is_bias, int input_weights_count, Random random)
        {
            this.is_bias = is_bias;
            if (this.is_bias)
            {
                this.Weights = new double[0];
            }
            else
            {
                this.Weights = new double[input_weights_count];
                for (int i = 0; i < Weights.Length; i++)
                {
                    //this.Weights[i] = (random.NextDouble()) - .5;

                    //this.Weights[i] = (random.NextDouble() * .8) - .4;
                    //this.Weights[i] += this.Weights[i] >= 0 ? .1 : -.1;

                    this.Weights[i] = (random.NextDouble() * .5) + .1;
                }
            }

        }
    }
}
