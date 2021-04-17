using System;

namespace MINST_with_ANN
{
    public class Layer
    {
        private readonly Neuron[] neurons;

        public Layer(int numberOfNeurons, int numberOfInputs, Func<float, float> activationFunction)
        {
            NumberOfInputs = numberOfInputs;
            neurons = new Neuron[numberOfNeurons];

            for(int i = 0; i < neurons.Length; i++)
            {
                neurons[i] = new Neuron(numberOfInputs, activationFunction);
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
    }
}
