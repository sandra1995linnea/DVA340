using System.Collections.Generic;

namespace ANN
{
    class Trainer
    {
        public void Train(Net net, List<Data> trainingSet)
        {
            foreach(var example in trainingSet)
            {
                net.Update(example.Pixles);
                float[] expectedOutput = ExpectedOutput(example.Label);
                net.Backpropogate(expectedOutput);
            }
        }

        private float[] ExpectedOutput(int label)
        {
            float[] expected = { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };
            expected[label] = 1;
            return expected;
        }
    }
}
