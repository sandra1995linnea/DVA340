using System.Collections.Generic;
using System.IO;

namespace MINST_with_ANN
{
    /// <summary>
    /// Represents input (the image) and the desired output (which number)
    /// </summary>
    public class Data
    {
        public static List<Data> ReadFile()
        {
            List<Data> data = new List<Data>();
            string name = "F:/Mälardalens Högskola/DVA340/Lab 5/assignment5.csv";
            string[] lines = File.ReadAllLines(name);

            for (int i = 1; i < lines.Length; i++)
            {
                string[] subStrings = lines[i].Split(',');

                // read the label, i.e. what number this data set was showing
                int label = int.Parse(subStrings[0]);

                float[] pixels = new float[subStrings.Length - 1];

                for (int j = 1; j < subStrings.Length; j++)
                {
                    // get the input data as floats in the range 0..1
                    pixels[j - 1] = int.Parse(subStrings[j]) / (float)255.0;
                }
                data.Add(new Data(label, pixels));
            }

            return data;
        }

        public Data(int label, float[] pixels)
        {
            Label = label;
            Pixels = pixels;
        }

        public int Label { get; }
        public float[] Pixels { get; }
    }
}
