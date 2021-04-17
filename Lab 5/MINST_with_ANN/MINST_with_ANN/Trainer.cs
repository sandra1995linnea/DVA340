using System;

namespace MINST_with_ANN
{
    public static class Trainer
    {
        /// <summary>
        /// Trains the net. Stops when it is good enough or has been trained for maxIterations
        /// </summary>
        /// <param name="net">The net to train</param>
        /// <param name="trainingSets">Training data</param>
        /// <param name="validationSets">Data used to see how well the net is</param>
        /// <param name="maxIterations">Maximum number of iterations of the training data</param>
        /// <param name="approvalLimit">Between 0 and 1: when this percentage has been reached, training will stop</param>
        public static bool Train(Net net, Data[] trainingSets, Data[] validationSets, int maxIterations, float approvalLimit)
        {
            float percentage;
            for(int i = 0; i < maxIterations; i++)
            {
                foreach (var set in trainingSets)
                {
                    net.Train(set.Pixels, set.Label);
                }

                percentage = Test(net, validationSets);
                Console.WriteLine((percentage * 100.0).ToString("N1") + "% correct");

                if (percentage >= approvalLimit)
                {
                    Console.WriteLine("Approval limit reached!");
                    return true;
                }
            }

            Console.WriteLine("Approval limit NOT reached");
            return false;
        }

        /// <summary>
        /// Returns proportion of data sets that were correctly identified
        /// </summary>
        public static float Test(Net net, Data[] validationSets)
        {
            int correct = 0;

            foreach(var set in validationSets)
            {
                if(net.IdentifyNumber(set.Pixels) == set.Label)
                {
                    correct++;
                }
            }
            return (float)correct / validationSets.Length;
        }
    }
}
