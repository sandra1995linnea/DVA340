using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANN
{
    class Program
    {
        static void Main(string[] args)
        {
            List <Data> allData = Data.ReadFile();
            //creates a net. number of inputs is the same amount as the nr of pixles
            Net net = new Net(4, 10, allData[0].Pixles.Length, (float)0.04);

            int trainingSize = (int)(allData.Count * 0.7);
            int validationSize = (int)(allData.Count * 0.1);
            int testingSize = allData.Count - trainingSize - validationSize;

            List<Data> trainingSet = allData.GetRange(0, trainingSize).ToList();
            List<Data> validationSet = allData.GetRange(trainingSize, validationSize).ToList();
            List<Data> testingSet = allData.GetRange(validationSize, testingSize).ToList();

            Trainer train = new Trainer();
            Tester tester = new Tester();

            // TODO do this many times
            train.Train(net, trainingSet);

            double proportionOfCorrectAnswers = tester.Test(net, testingSet);
            if (proportionOfCorrectAnswers > 0.8)
            {

            }


            proportionOfCorrectAnswers = tester.Test(net, validationSet);

            Console.WriteLine("After training the net reached " + (proportionOfCorrectAnswers * 100).ToString() + "%");
        }
    }
}
