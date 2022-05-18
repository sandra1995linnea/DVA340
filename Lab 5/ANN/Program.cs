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

        }
    }
}
