using System;

namespace ANN
{
    internal class HiddenLayer : Layer
    {
        private readonly Layer nextlayer;

        public HiddenLayer(int nrOfNeurons, int nrOfInputs, Func<float, float> activationFunction, Func<float, float> derivationFunction, float learningrate, Layer nextlayer) : 
            base(nrOfNeurons, nrOfInputs, activationFunction, derivationFunction, learningrate)
        {
            this.nextlayer = nextlayer;
        }

        internal void Backpropogate()
        {
            for(int j = 0; j < neurons.Count; j++)
            {
                neurons[j].ErrorTerm = derivationFunction(neurons[j].Output) * nextlayer.DownStreamErrors(j);
            }
        }
    }
}
