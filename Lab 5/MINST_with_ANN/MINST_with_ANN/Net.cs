using System;

namespace MINST_with_ANN
{
    public class Net
    {
        private static float Sigmoid(float x) => 1 / (1 + (float)Math.Exp(-x));
        private static float DerivativeOfSigmoid(float x)
        {
            float sig = Sigmoid(x);
            return sig * (1 - sig);
        }

        private readonly Func<float, float> activationFunction;
        private readonly Func<float, float> derivativeOfActivationFunction;

        public Net(int numberOfLayers, int numberOfInputs, int numberOfOutputs, int numberOfNeuronsInHiddenLayers, float learningRate)
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
                throw new ArgumentException("Come on, you gotta have at least one neuron in the hidden layer!");
            }

            // use the sigmoid function as our activation function
            activationFunction = Sigmoid;
            derivativeOfActivationFunction = DerivativeOfSigmoid;

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

                Layers[i] = new Layer(numberOfNeurons, numberOfInputsForLayer, activationFunction, derivativeOfActivationFunction, learningRate);
            }
        }

        /// <summary>
        /// Runs inputs through the neural network to generate output from the net
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        public float[] Update(float[] inputs)
        {
            if(inputs.Length != Layers[0].NumberOfInputs)
            {
                throw new ArgumentException("Wrong length of inputs!");
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

        /// <summary>
        /// Runs one backpropagation run, given the inputs and expected outputs,
        /// in order to update the weights of each neuron
        /// </summary>
        /// <param name="inputs"></param>
        /// <param name="expectedOutput"></param>
        public void BackProp(float[] inputs, float[] expectedOutput)
        {
            if(inputs.Length != Layers[0].NumberOfInputs)
            {
                throw new ArgumentException("Wrong number of inputs!");
            }
            if (expectedOutput.Length != Layers[^1].Outputs.Length)
            {
                throw new ArgumentException("Wrong number of expected outputs!");
            }

            // calculate outputs
            Update(inputs);

            // calculate error terms for output layer:
            // this will also update weights
            Layers[^1].CalculateErrorTerms(expectedOutput);

            // calculate error terms for all other layers:
            // this will also update weights
            for(int i = Layers.Length - 2; i >= 0; i--) // start at the second to last layer and loop backwards
            {
                Layers[i].CalculateErrorTerms(Layers[i + 1]);
            }
        }

        /// <summary>
        /// Returns an int as identified by the neural network
        /// </summary>
        /// <param name="inputs">Pixel data</param>
        /// <returns>The identified number, 0 to 9</returns>
        public int IdentifyNumber(float[] inputs)
        {
            Update(inputs);

            int index = 0;
            float highestOutput = Outputs[0];

            for (int i = 1; i < Outputs.Length; i++)
            {
                if(Outputs[i] > highestOutput)
                {
                    index = i;
                    highestOutput = Outputs[i];
                }
            }
            return index;
        }

        /// <summary>
        /// Calculates expected output from the net and runs backprop once
        /// </summary>
        /// <param name="inputs"></param>
        /// <param name="expectedNumber"></param>
        public void Train(float[] inputs, int expectedNumber)
        {
            // sets all values of the array 'expected' to 0 except the one we expect, it is set to 1
            float[] expected = new float[10];
            expected[expectedNumber] = 1;

            BackProp(inputs, expected);
        }

        public Layer[] Layers { get; }

        public float[] Outputs { get; private set; }
    }
}
