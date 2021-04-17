using System;
using System.Collections.Generic;
using System.Text;

namespace MINST_with_ANN
{
    public class Net
    {
        public Net(int numberOfLayers, int numberOfInputs, int numberOfOutputs, int numberOfNeuronsInHiddenLayers)
        {
            if (numberOfLayers < 1)
            {
                throw new ArgumentException("Come on, you gotta have at least one layer!");
            }
            if (numberOfInputs < 1)
            {
                throw new ArgumentException("Come on, you gotta have at least one input!");
            }
            if (numberOfOutputs < 1)
            {
                throw new ArgumentException("Come on, you gotta have at least one output!");
            }
            if (numberOfNeuronsInHiddenLayers < 1)
            {
                throw new ArgumentException("Come on, you gotta have at least one neuron in the layer!");
            }

            // use the sigmoid function as our activation function
            Func<float, float> activationFunction = (float x) => 1 / (1 + (float)Math.Exp(-x));

            Layers = new Layer[numberOfLayers];

            // create the layers: the number of neurons and inputs to each layer depends on the layer
            int numberOfNeurons;
            int numberOfInputsForLayer;
            for (int i = 0; i < numberOfLayers; i++)
            {
                if(i == numberOfLayers - 1) // if we're at the output layer
                {
                    numberOfNeurons = numberOfOutputs;
                }
                else
                {
                    numberOfNeurons = numberOfNeuronsInHiddenLayers;
                }

                if(i == 0) // if we're at the input layer
                {
                    numberOfInputsForLayer = numberOfInputs;
                }
                else
                {
                    numberOfInputsForLayer = numberOfNeuronsInHiddenLayers;
                }

                Layers[i] = new Layer(numberOfNeurons, numberOfInputsForLayer, activationFunction);
            }
        }

        public float[] Update(float[] inputs)
        {
            if(inputs.Length != Layers[0].NumberOfInputs)
            {
                throw new ArgumentException("Wrong length of inputs");
            }

            // feed inputs into the input layer
            Layers[0].Update(inputs);

            // feed the previous layer's outputs as inputs to each subsequent layer:
            for (int i = 1; i < Layers.Length; i++)
            {
                Layers[i].Update(Layers[i - 1].Outputs);
            }

            // output of the net equals the output of the last layer
            Outputs = Layers[^1].Outputs;

            return Outputs;
        }

        public Layer[] Layers { get; }

        public float[] Outputs { get; private set; }
    }
}
