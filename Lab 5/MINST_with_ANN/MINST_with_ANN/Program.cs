using System;

namespace MINST_with_ANN
{
    class Program
    {
        static void Main(string[] args)
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


        }
    }
}
