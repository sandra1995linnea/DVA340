using System;
using System.Collections.Generic;

namespace ANN
{
    class Layer
    {
        private readonly List<Neuron> neurons;
        public Layer(int nrOfNeurons, int nrOfInputs, Func<float, float> activationFunction)
        {
            neurons = new List<Neuron>();

            for(int i = 0; i < nrOfNeurons; i++)
            {
                neurons.Add(new Neuron(nrOfInputs, activationFunction));
            }
        }

        /// <summary>
        /// Updates each neuron with the new data and returns the data from the neurons
        /// </summary>
        /// <param name="data">Data, either from input layer or the previous layer</param>
        /// <returns>Output data </returns>
        internal float[] Update(float[] data)
        {
            // TODO!
            float[] outputs = new float[neurons.Count];
            for(int i = 0; i < neurons.Count; i++)
            {
                outputs[i] = neurons[i].Update(data);
            }

            return outputs;
        }

    }
}
