using System;

namespace MINST_with_ANN
{
    class Program
    {
        private const int NEURONS_IN_HIDDEN_LAYER = 12; // TODO
        private const float LEARNING_RATE = (float)0.05;

        static void Main()
        {
            Console.WriteLine("Hello World!");

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

         //   int number = net.IdentifyNumber(trainingSet[0].Pixels);

            net.Train(trainingSet[0].Pixels, trainingSet[0].Label);

        }
    }
}
