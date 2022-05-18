using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANN
{
    class Data
    {
        public static List<Data> ReadFile()
        {
            List<Data> data = new List<Data>();
            string name = "F:/Mälardalens Högskola/DVA340/Lab 5/assignment5.csv";
            string[] lines = File.ReadAllLines(name);

            for(int i = 1; i < lines.Length; i++)
            {
                string[] substrings = lines[i].Split(',');

                //read the label i.e what number this data set was showing
                int label = int.Parse(substrings[0]);
                float[] pixles = new float[substrings.Length - 1];

                for(int j = 1; j < substrings.Length; j++)
                {
                    //get the input data as floats in the range 0..1
                    pixles[j - 1] = int.Parse(substrings[j]) / (float)255.0; // each pixle has a value between 0 and 255
                }
                data.Add(new Data(label, pixles));
            }
            return data;
        }

        private Data(int label, float[] pixles)
        {
            Label = label;
            Pixles = pixles;
        }

        public int Label { get; }
        public float[] Pixles { get; }
    }
}
