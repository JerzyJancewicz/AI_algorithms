
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace knn
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            List<string> data = File.ReadAllLines("J:\\NAI_ai\\NAI_ai\\knn\\knn\\data\\iris.data").ToList();
            List<string> testData = File.ReadAllLines("J:\\NAI_ai\\NAI_ai\\knn\\knn\\data\\iris.test.data").ToList();
            Knn knn = new Knn();
            knn.PredictedLabelKnn(data, testData, 3);
        }
    }
}