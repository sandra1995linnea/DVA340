using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANN
{
    internal class HiddenLayer : Layer
    {
        public HiddenLayer(int nrOfNeurons, int nrOfInputs, Func<float, float> activationFunction, Func<float, float> derivationFunction, float learningrate) : 
            base(nrOfNeurons, nrOfInputs, activationFunction, derivationFunction, learningrate)
        {
        }

        private void Backpropogate(float[] expectedOutput)
        {
            for(int i = 0; i < neurons.Count; i++)
            {
                neurons[i].ErrorTerm = derivationFunction(neurons[i].Output) * 
            }
        }
    }


}
