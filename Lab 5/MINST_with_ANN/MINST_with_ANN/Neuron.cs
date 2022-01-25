using System;

namespace MINST_with_ANN
{
    class Neuron
    {
        private const float DECAY_RATE = (float)0.0001; // proportion of weight which is decayed away each update

        private readonly Func<float, float> activationFunction;
        private readonly Func<float, float> derivativeOfActivationFunction;
        private readonly float learningRate;

        public Neuron(int numberOfInputs, Func<float, float> activationFunction, Func<float, float> derivativeOfActivationFunction, float learningRate)
        {
            this.activationFunction = activationFunction;
            this.derivativeOfActivationFunction = derivativeOfActivationFunction;
            this.learningRate = learningRate;

            Weights = new float[numberOfInputs + 1];

            Random random = new Random();
            float rand;

            // give weights random small values:
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

            // backpropagation requires the neuron to remember its previous inputs
            Inputs = inputs;

            // sum upp total activation, start by adding the "w0" weight
            float activation = Weights[0];

            for(int i = 0; i < inputs.Length; i++)
            {
                activation += inputs[i] * Weights[i + 1];
            }

            Output = activationFunction(activation);
            return Output;
        }

        /// <summary>
        /// Calculates error term, assuming this neuron is in the output layer
        /// This will also update weights
        /// </summary>
        /// <param name="expectedOutput"></param>
        internal void CalculateErrorTerm(float expectedOutput)
        {
            ErrorTerm = (expectedOutput - Output) * derivativeOfActivationFunction(Output);

            UpdateWeight();
        }

        /// <summary>
        /// Calculates error term, assuming this neuron is NOT in the output layer
        /// This will also update weights
        /// </summary>
        /// <param name="layer"></param>
        internal void CalculateErrorTermInHiddenLayer(float sumErrorTermsTimesWeights)
        {
            ErrorTerm = derivativeOfActivationFunction(Output) * sumErrorTermsTimesWeights;

            UpdateWeight();
        }

        /// <summary>
        /// Updates weights according to error term and past inputs
        /// </summary>
        private void UpdateWeight()
        {
            // update the first weight, which always has 1 as input:
            Weights[0] += learningRate * ErrorTerm;

            // use weight decay
        //    Weights[0] *= 1 - DECAY_RATE;

            // for all other weights:
            for (int i = 1; i < Weights.Length; i++)
            {
                Weights[i] += learningRate * ErrorTerm * Inputs[i - 1];

                // use weight decay
          //      Weights[i] *= 1 - DECAY_RATE;
            }
        }

        public float[] Weights { get; }
        public int NumberOfInputs => Weights.Length - 1;
        public float Output { get; private set; }
        public float[] Inputs { get; private set; }
        public float ErrorTerm { get; private set; }
    }
}
