using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANN
{
    class Neuron
    {
        private readonly float[] weights;
        private readonly int numberOfInputs;
        private readonly Func<float, float> activationFunction;
        private float output;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numberOfInputs">Number of inputs from either input layer or previous layer</param>
        /// <param name="activationFunction">Any activation function, e.g. sigmoid or tanh</param>
        public Neuron(int numberOfInputs, Func<float, float> activationFunction)
        {
            this.numberOfInputs = numberOfInputs;
            this.activationFunction = activationFunction;

            // set weights:
            weights = new float[numberOfInputs + 1]; // +1 since one weght is used as bias
            Random random = new Random();
            for(int i = 0; i < weights.Length; i++)
            {
                weights[i] = random.Next(-1, 1);
            }
        }

        internal float Update(float[] inputs)
        {
            if(inputs.Length != numberOfInputs)
            {
                throw new ArgumentException("Input vector of incorrect length");
            }

            float activation = 0;
            //sum up activation!
            for(int i = 0; i < weights.Length - 1; i++)
            {
                activation += weights[i] * inputs[i];
            }

            //adds the last weight to the activation, it is the bias weight
            activation += weights[weights.Length];

            output = activationFunction(activation);
            return output;
        }
    }
}
