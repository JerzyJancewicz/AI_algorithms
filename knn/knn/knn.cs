using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace knn
{
    public class Knn
    {
        public void PredictedLabelKnn(List<string> data, List<string> testData, int k)
        {
            List<string> values = new List<string>();
            List<double> distances = new List<double>();
            double distance = 0;
            foreach (string line in testData)
            {
                values = line.Split(',').ToList();
                values.Remove(values[values.Count - 1]);
                foreach (string line2 in data)
                {
                    distance = Math.Sqrt(2);
                    distances.Add(distance);
                }
                
                //KNearest(k, distances);
                // metoda k najblizszych
                // wyswietlenie jaki gatunek
                // distance = Math.Sqrt(2);
            }
        }

        private List<double> KNearest(int k , List<double> distances)
        {
            List<double> tmp = new List<double>();
            distances.Sort();
            for (int i = 0; i < k; i++)
            {
                tmp.Add(distances[i]);
            }
            return tmp;
        }
        
    }
}