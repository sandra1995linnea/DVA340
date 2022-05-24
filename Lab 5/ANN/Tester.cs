using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANN
{
    class Tester
    {
        internal double Test(Net net, List<Data> testingSet)
        {
            foreach (var example in testingSet)
            {
                float[] output = net.Update(example.Pixles);

                // find which output is biggest
            }
            return 0; //TODO!
        }

    }
}
