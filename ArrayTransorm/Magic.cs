using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayTransorm
{
    class Magic
    {
        public static String[] ParseFile(String str)
        {
            //clean up the file, removes unnecessary chars;
            String cleanedUp = str.Replace("[", "").Replace("]", "").Replace("{", "").Replace("}", "").Replace("\n", "").Replace(" ", "").Replace(":","").Replace(";","");
            if (cleanedUp[cleanedUp.Length - 1] == ',') {
                cleanedUp = cleanedUp.Substring(0, cleanedUp.Length - 1);
            }
            String[] parsedArray = cleanedUp.Split(',');
            return parsedArray;
        }
        public static double[] ToArray(String[] values) {
            double[] parsed_values = new double[values.Length];
            for (int i = 0; i < values.Length; i++) {
                try
                {
                    parsed_values[i] = Convert.ToDouble(values[i]);
                }
                catch
                {
                    parsed_values[i] = Double.NaN;
                }
            }
            return parsed_values;
        }
        public static Double Map(double n, double start1, double stop1, double start2, double stop2) {
            return ((n - start1) / (stop1 - start1)) * (stop2 - start2) + start2;
        }
        public static double[] MapArray(double[] values, double start, double stop) {
            double min = values.Min();
            double max = values.Max();
            double[] mapped_array = new double[values.Length];
            for (int i = 0; i < values.Length; i++) {
                mapped_array[i] = Magic.Map(values[i], min, max, start, stop);
            }
            return mapped_array;
        }
        public static Double CalculateStandardDeviation(double[] values) {
            double average = values.Average();
            double deviationBase = 0;
            for (int i = 0; i < values.Length; i++) {
                deviationBase += Math.Pow((average - values[i]), 2);
            }
            double standardDeviation = Math.Sqrt(deviationBase / values.Length);
            return standardDeviation;
        }
        public static String JoinArray(double[] arr) {
            return String.Join(",", arr.AsEnumerable());
        }
    }
}
