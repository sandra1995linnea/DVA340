using System;

namespace ANN
{
    class Neuron
    {
        private readonly int numberOfInputs;
        private readonly float learningrate;
        private readonly Func<float, float> activationFunction;
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

        public float[] Weights { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numberOfInputs">Number of inputs from either input layer or previous layer</param>
        /// <param name="activationFunction">Any activation function, e.g. sigmoid or tanh</param>
        /// <param name="learningrate">  </param>
        public Neuron(int numberOfInputs, Func<float, float> activationFunction, float learningrate)
        {
            this.numberOfInputs = numberOfInputs;
            this.activationFunction = activationFunction;
            this.learningrate = learningrate;

            // set weights:
            Weights = new float[numberOfInputs + 1]; // +1 since one weght is used as bias
            Random random = new Random();
            for (int i = 0; i < Weights.Length; i++)
            {
                Weights[i] = random.Next(-1, 1);
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
            for (int i = 0; i < Weights.Length - 1; i++)
            {
                activation += Weights[i] * inputs[i];
            }

            //adds the last weight to the activation, it is the bias weight
            activation += Weights[Weights.Length];

            Output = activationFunction(activation);
            return Output;
        }

        private void UpdateWeights()
        {
            for(int i = 0; i < Inputs.Length; i++)
            {
                Weights[i] += learningrate * ErrorTerm * Inputs[i];
            }
            //this is the last weight and input is 1 since Weights are 1 more than the inputs
            Weights[Weights.Length] += learningrate * ErrorTerm * 1;
        }
    }
}
