using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANN
{
    class Trainer
    {
        public void Train(Net net, List<Data> trainingSet)
        {

            foreach(var example in trainingSet)
            {
                float[] output = net.Update(example.Pixles);
                float[] expectedOutput = ExpectedOutput(example.Label);

                //float[] errorTerms = errorTerms()
            }

            
        }

        private float[] ExpectedOutput(int label)
        {
            float[] expected = { -1, -1, -1, 1, -1, -1, 1, -1, -1, -1 };
            expected[label] = 1;
            return expected;
        }
    }
}
