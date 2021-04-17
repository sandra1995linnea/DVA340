using System;

namespace MINST_with_ANN
{
    class Program
    {
        private const int NEURONS_IN_HIDDEN_LAYER = 24;
        private const float LEARNING_RATE = (float)0.03;

        static void Main(string[] args)
        {
            Console.WriteLine("Running training...");

            var allData = Data.ReadFile();

            int trainingSetSize = (int)(allData.Count * 0.7);
            int validationSetSize = (int)(allData.Count * 0.1);
            int testingSetSize = allData.Count - trainingSetSize - validationSetSize;

            var trainingSet = new Data[trainingSetSize];
            var validationSet = new Data[validationSetSize];
            var testingSet = new Data[testingSetSize];

            for (int i = 0; i < trainingSetSize; i++)
            {
                trainingSet[i] = allData[i];
            }

            for (int i = 0; i < validationSetSize; i++)
            {
                validationSet[i] = allData[trainingSetSize + i];
            }

            for (int i = 0; i < testingSetSize; i++)
            {
                testingSet[i] = allData[trainingSetSize + validationSetSize + i];
            }

            var net = new Net(3, trainingSet[0].Pixels.Length, 10, NEURONS_IN_HIDDEN_LAYER, LEARNING_RATE);

            bool result = Trainer.Train(net, trainingSet, validationSet, 20, (float)0.8);
            
            if (!result)
            {
                return;
            }

            Console.WriteLine("Testing...");

            float percentage = Trainer.Test(net, testingSet);

            Console.WriteLine((percentage * 100.0).ToString("N1") + "% correct");
        }
    }
}
