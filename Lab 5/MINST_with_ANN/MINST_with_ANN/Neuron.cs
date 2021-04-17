using System;

namespace MINST_with_ANN
{
    class Neuron
    {
        private readonly Func<float, float> activationFunction;

        public Neuron(int numberOfInputs, Func<float, float> activationFunction)
        {
            this.activationFunction = activationFunction;
            Weights = new float[numberOfInputs + 1];

            // TODO set weights to small random numbers
            Random random = new Random();

            float rand;

            for(int i = 0; i < Weights.Length; i++)
            {
                rand = random.Next(1000); // random 0 to 1000
                rand /= 5000; // random 0 to 0.2
                Weights[i] = rand - (float)0.1; // random -0.1 to 0.1
            }
        }

        public float Update(float[] inputs)
        {
            if (inputs.Length != NumberOfInputs)
            {
                throw new ArgumentException("Wrong number of inputs!");
            }

            // sum upp total activation, start by adding the "w0" weight
            float activation = Weights[0];

            for(int i = 0; i < inputs.Length; i++)
            {
                activation += inputs[i] * Weights[i + 1];
            }

            Output = activationFunction(activation);
            return Output;
        }

        public float[] Weights { get; }
        public int NumberOfInputs => Weights.Length - 1;
        public float Output { get; private set; }
    }
}
