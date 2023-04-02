using System.Collections.Generic;
using System.Globalization;

namespace Perceptron
{
    public class Perceptron
    {
        private readonly List<string> _testData;
        private readonly List<string> _data;
        private readonly double _alfa;
        private List<double> _weights = new List<double>();
        private int _delta;

        public Perceptron(List<string> testData, List<string> data, double alfa, int delta)
        {
            _testData = testData;
            _data = data;
            _alfa = alfa;
            _delta = delta;

        }
        
        private void Learn()
        {
            int entryValue;
            int exitValue;
            List<List<double>> tmp = ConvertToDoubleDataOnly(_data);
            // "Iris-virginica" - 1
            // "Iris-versicolor" - 0
            FillWeights();
            for (int i = 0; i < tmp.Count; i++)
            {
                entryValue = "Iris-virginica" == ConvertToStringDataOnly(_data)[i] ? 1 : 0;
                exitValue = Net(_delta, i);
                _weights = CountWeight(_weights, _alfa, entryValue,exitValue, tmp[i]);   
            }
        }
        
        private void FillWeights()
        {
            foreach (var line in ConvertToDoubleDataOnly(_data))
            {
                for (int i = 0; i < line.Count; i++)
                {
                    _weights.Add(new Random().NextDouble());
                }
            }
        }

        private int Net(int delta, int lineNumber)
        {
            double sum = 0;
            List<double> weightTmp = _weights;
            List<double> tmpDoubles = ConvertToDoubleDataOnly(_data)[lineNumber];

            for (int i = 0; i < tmpDoubles.Count; i++)
            {
                sum += tmpDoubles[i] * weightTmp[i];
            }
            sum += (-1) * delta;
            if (sum >= 0)
            {
                return 1;
            }
            return 0;
        }

        // w = w`
        // predict exitvalue
        // entry value to nazwa z pliku reprezentowalna albo 1 albo 0
        private List<double> CountWeight(List<double> weights, double alfa, int entryValue, double exitValue, List<double> doubles)
        {
            List<double> tmp = weights;
            List<double> tmpDoubles = doubles;
            for (int i = 0; i < tmpDoubles.Count; i++)
            {
                tmpDoubles.Insert(i, tmpDoubles[i]*(alfa*(entryValue - exitValue)));
                tmp.Insert(i, tmp[i] + tmpDoubles[i]);
            }
            return tmp;
        }
        
        private void IterationError()
        {
            
        }

        private List<List<double>> ConvertToDoubleDataOnly(List<string> data2)
        {
            List<List<double>> tmpLists = new List<List<double>>();
            foreach (var line in data2)
            { 
                List<string> values = line.Split(',').ToList();
                values.Remove(values[values.Count - 1]);
                tmpLists.Add(values.Select(s => double.Parse(s,NumberStyles.Any, CultureInfo.InvariantCulture)).ToList());
            }
            return tmpLists;
        }

        private List<string> ConvertToStringDataOnly(List<string> data2)
        {
            List<string> tmpLists = new List<string>();
            foreach (var line in data2)
            { 
                List<string> values = line.Split(',').ToList();
                tmpLists.Add(values[values.Count - 1]);
            }
            return tmpLists;
        }
    }
}