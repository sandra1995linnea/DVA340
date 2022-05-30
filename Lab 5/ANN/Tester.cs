using System.Collections.Generic;

namespace ANN
{
    class Tester
    {
        internal double Test(Net net, List<Data> testingSet)
        {
            int correctAnswers = 0;
            foreach (var example in testingSet)
            {
                float[] output = net.Update(example.Pixles);
                int identifiedNumber = Interprete(output);

                if (identifiedNumber == example.Label)
                {
                    correctAnswers++;
                }
            }
            return (double)correctAnswers / testingSet.Count; 
        }

        private static int Interprete(float[] output)
        {
            float biggst_number = -1;
            int index = 0;
            for (int i = 0; i < output.Length; i++)
            {
                if (output[i] > biggst_number)
                {
                    biggst_number = output[i];
                    index = i;
                }
            }

            return index;
        }
    }
}
