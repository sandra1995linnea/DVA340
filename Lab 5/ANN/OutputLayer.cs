using System;

namespace ANN
{
    internal class OutputLayer : Layer
    {
        public OutputLayer(int nrOfNeurons, int nrOfInputs, Func<float, float> activationFunction, Func<float, float> derivationFunction, float learningrate) : 
            base(nrOfNeurons, nrOfInputs, activationFunction, derivationFunction, learningrate)
        {
        }

        internal void Backpropogate(float[] expectedOutput)
        {
            for(int i = 0; i < neurons.Count; i++)
            {
                neurons[i].ErrorTerm = (expectedOutput[i] - neurons[i].Output) * derivationFunction(neurons[i].Output);
            }
        }
    }
}
