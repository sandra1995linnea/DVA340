using System;
using System.Collections.Generic;

namespace ANN
{
    public class Net
    {
        private readonly List<Layer> layers;
        private const int NR_OF_LAYERS = 4;
        private const int NR_OF_NEURONS = 20;
        private const int NR_OF_INPUTS = 5;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nrOfLayers">Number of layers including hidden layer</param>
        /// <param name="nrOfNeurons">Number of neurons in each layer, equals number of outputs in output layer</param>
        /// <param name="nrOfInputs">Number of inputs in the first layer</param>
        public Net(int nrOfLayers, int nrOfNeurons, int nrOfInputs)
        {
            nrOfLayers = NR_OF_LAYERS;
            nrOfNeurons = NR_OF_NEURONS;
            nrOfInputs = NR_OF_INPUTS;
            // TODO create layers!
            layers = new List<Layer>();

            // first layer:
            layers.Add(new Layer(nrOfNeurons, nrOfInputs, Sigmoid));

            // the rest of hidden layers:
            for (int i = 1; i < nrOfLayers - 1; i++)
            {
                layers.Add(new Layer(nrOfNeurons, nrOfNeurons, Sigmoid));
            }

            // output layer:
            layers.Add(new Layer(nrOfNeurons, nrOfNeurons, Sigmoid));
        }

        private float Sigmoid(float x) => (float)(1 / (1 + Math.Exp(-x)));

        /// <summary>
        /// Updates the layers. Puts input into the first layer and gets out outputs from the last layer
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns>output data</returns>
        public float[] Update(float[] inputs)
        {
            var data = inputs;

            foreach(var layer in layers)
            {
                data = layer.Update(data);
            }
            return data;
        }
    }
}
