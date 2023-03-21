using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace knn
{
    public class knn
    {
        private List<string> data = File.ReadAllLines("J:\\NAI_ai\\NAI_ai\\knn\\knn\\data\\iris.data").ToList();

        public void predicted_label_knn(List<double> data, List<double> testData, int k)
        {
            List<double> distances = new List<double>();
            double distance = 0;
            foreach (var VARIABLE in data)
            {
                distance = Math.Sqrt(2);
                distances.Add(VARIABLE);
            }
        }
    }
}