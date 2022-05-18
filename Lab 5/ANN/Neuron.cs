using System;

namespace ANN
{
    class Neuron
    {
        private readonly float[] weights;
        private readonly int numberOfInputs;
        private readonly float learningrate;
        private readonly Func<float, float> activationFunction;
        private readonly Func<float, float> derivationFunction;
        private float errorTerm;
        private float[] Inputs;

        /// <summary>
        /// The error term of this neuron. When set, the weights of the neuron are updated.
        /// </summary>
        public float ErrorTerm
        {
            get => errorTerm;
            set
            {
                errorTerm = value;
                UpdateWeights();
            }
        }

        public float Output { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numberOfInputs">Number of inputs from either input layer or previous layer</param>
        /// <param name="activationFunction">Any activation function, e.g. sigmoid or tanh</param>
        public Neuron(int numberOfInputs, Func<float, float> activationFunction, Func<float, float> derivationFunction, float learningrate)
        {
            this.numberOfInputs = numberOfInputs;
            this.activationFunction = activationFunction;
            this.derivationFunction = derivationFunction;
            this.learningrate = learningrate;

            // set weights:
            weights = new float[numberOfInputs + 1]; // +1 since one weght is used as bias
            Random random = new Random();
            for (int i = 0; i < weights.Length; i++)
            {
                weights[i] = random.Next(-1, 1);
            }
        }

        internal float Update(float[] inputs)
        {
            if (inputs.Length != numberOfInputs)
            {
                throw new ArgumentException("Input vector of incorrect length");
            }

            Inputs = inputs;
            float activation = 0;
            //sum up activation!
            for (int i = 0; i < weights.Length - 1; i++)
            {
                activation += weights[i] * inputs[i];
            }

            //adds the last weight to the activation, it is the bias weight
            activation += weights[weights.Length];

            Output = activationFunction(activation);
            return Output;
        }


        private void UpdateWeights()
        {
            for(int i = 0; i < Inputs.Length; i++)
            {
                weights[i] += learningrate * ErrorTerm * Inputs[i];
            }
            //this is the last weight and input is 1 since weights are 1 more than the inputs
            weights[weights.Length] += learningrate * ErrorTerm * 1;
        }
    }
}
