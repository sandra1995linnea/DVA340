using System;
using System.Collections.Generic;

namespace ANN
{
    class Layer
    {
        protected readonly List<Neuron> neurons;
        protected readonly Func<float, float> derivationFunction;

        public Layer(int nrOfNeurons, int nrOfInputs, Func<float, float> activationFunction, Func<float, float> derivationFunction, float learningrate)
        {
            neurons = new List<Neuron>();

            for(int i = 0; i < nrOfNeurons; i++)
            {
                neurons.Add(new Neuron(nrOfInputs, activationFunction, learningrate));
            }

            this.derivationFunction = derivationFunction;
        }

        internal float DownStreamErrors(int j)
        {
            float sum = 0;
            foreach (var neuron in neurons)
            {
                sum += neuron.ErrorTerm * neuron.Weights[j];
            }
            return sum;
        }

        /// <summary>
        /// Updates each neuron with the new data and returns the data from the neurons
        /// </summary>
        /// <param name="data">Data, either from input layer or the previous layer</param>
        /// <returns>Output data </returns>
        internal float[] Update(float[] data)
        {
            float[] outputs = new float[neurons.Count];
            for(int i = 0; i < neurons.Count; i++)
            {
                outputs[i] = neurons[i].Update(data);
            }

            return outputs;
        }

    }
}
