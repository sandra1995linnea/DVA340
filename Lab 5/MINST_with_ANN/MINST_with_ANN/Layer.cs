using System;

namespace MINST_with_ANN
{
    public class Layer
    {
        private readonly Neuron[] neurons;
        
        public Layer(int numberOfNeurons, int numberOfInputs, Func<float, float> activationFunction, 
            Func<float, float> derivativeOfActivationFunction, float learningRate)
        {
            NumberOfInputs = numberOfInputs;

            neurons = new Neuron[numberOfNeurons];

            for(int i = 0; i < neurons.Length; i++)
            {
                neurons[i] = new Neuron(numberOfInputs, activationFunction, derivativeOfActivationFunction, learningRate);
            }
            Outputs = new float[numberOfNeurons];
        }

        public float[] Update(float[] inputs)
        {
            for (int i = 0; i < neurons.Length; i++)
            {
                Outputs[i] = neurons[i].Update(inputs);
            }
            return Outputs;
        }

        public float[] Outputs { get; private set; }
        public int NumberOfInputs { get; }

        /// <summary>
        /// Calculates error terms, assuming this layer is an output layer.
        /// This will also update weights
        /// </summary>
        /// <param name="expectedOutput"></param>
        internal void CalculateErrorTerms(float[] expectedOutput)
        {
            for (int i = 0; i < neurons.Length; i++)
            {
                neurons[i].CalculateErrorTerm(expectedOutput[i]);
            }
        }

        /// <summary>
        /// Calculates error terms, assuming this layer is NOT an output layer.
        /// This will also update weights
        /// </summary>
        /// <param name="layer"></param>
        internal void CalculateErrorTerms(Layer layer)
        {
            float sumErrorTermsTimesWeights;
            for (int i = 0; i < neurons.Length; i++)
            {
                sumErrorTermsTimesWeights = 0;

                // loop through all neurons in the downstream layer, summing up the "error term * weight"
                for(int j = 0; j < layer.neurons.Length; j++)
                {
                    sumErrorTermsTimesWeights += layer.neurons[j].ErrorTerm * layer.neurons[j].Weights[i + 1];
                }

                neurons[i].CalculateErrorTermInHiddenLayer(sumErrorTermsTimesWeights);
            }
        }
    }
}
