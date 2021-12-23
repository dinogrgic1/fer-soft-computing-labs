using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace GeneticAlgorithm
{
    public class FunctionData
    {
        public Dictionary<List<double>, double> Data { get; }

        public FunctionData(string filepath, int numInput)
        {
            Data = TryParseInput(filepath, numInput);
        }

        private Dictionary<List<double>, double> TryParseInput(string filepath, int numInput)
        {
            var dict = new Dictionary<List<double>, double>();
            try
            {
                foreach (string line in File.ReadLines(filepath))
                {
                    var read = line.Split('\t');
                    var input = new List<double>();
                    
                    int i;
                    for (i = 0; i < numInput; i++)
                    {
                        input.Add(double.Parse(read[i], NumberStyles.Float, CultureInfo.InvariantCulture));
                    }

                    var output = double.Parse(read[i], NumberStyles.Float, CultureInfo.InvariantCulture);
                    dict.Add(input, output);
                }
            }
            
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            return dict;
        }
        
    }
}