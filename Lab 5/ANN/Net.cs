using System;
using System.Collections.Generic;

namespace ANN
{
    public class Net
    {
        private readonly List<Layer> layers;


        internal void Backpropogate(float[] expectedOutput)
        {
            (layers[layers.Count - 1] as OutputLayer).Backpropogate(expectedOutput);
            
            for(int i = layers.Count - 2; i >= 0; i--)
            {
                (layers[i] as HiddenLayer).Backpropogate();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nrOfLayers">Number of layers including hidden layer</param>
        /// <param name="nrOfNeuronsPerLayer">Number of neurons in each layer, equals number of outputs in output layer</param>
        /// <param name="nrOfInputs">Number of inputs in the first layer</param>
        public Net(int nrOfLayers, int nrOfNeuronsPerLayer, int nrOfInputs, float learningrate)
        {
            
            //create layers!
            // create output layer first and put it last in the list, then create hidden layers and put it in front of output layer
            layers = new List<Layer>();

            layers.Insert(0, new OutputLayer(nrOfNeuronsPerLayer, nrOfInputs, Sigmoid, Derivation_Sigmoid, learningrate));

            for (int i = 1; i < nrOfLayers; i++)
            {
                layers.Insert(0, new HiddenLayer(nrOfNeuronsPerLayer, nrOfInputs, Sigmoid, Derivation_Sigmoid, learningrate, layers[0]));
            }
        }

        private float Sigmoid(float x) => (float)(1 / (1 + Math.Exp(-x)));

        private float Derivation_Sigmoid(float x) => Sigmoid(x) * (1 - Sigmoid(x));

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
