using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANN
{
    internal class OutputLayer : Layer
    {
        public OutputLayer(int nrOfNeurons, int nrOfInputs, Func<float, float> activationFunction, Func<float, float> derivationFunction, float learningrate) : 
            base(nrOfNeurons, nrOfInputs, activationFunction, derivationFunction, learningrate)
        {
        }

        private void Backpropogate(float[] expectedOutput)
        {
            for(int i = 0; i < neurons.Count; i++)
            {
                neurons[i].ErrorTerm = (expectedOutput[i] - neurons[i].Output) * derivationFunction(neurons[i].Output);
            }
            
        }
    }
}
