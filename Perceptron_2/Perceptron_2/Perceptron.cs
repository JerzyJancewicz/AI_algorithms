using System.Globalization;

namespace Perceptron2
{
    public class Perceptron
    {
        private readonly List<string> _testData;
        private readonly List<string> _data;
        private readonly List<List<double>> _doubleData;
        private readonly double _alfa;
        private List<double> _weights = new List<double>();
        private double _bias;
        private int _accuracy;

        public Perceptron(List<string> testData, List<string> data, double alfa, double bias)
        {
            _testData = testData;
            _data = data;
            _alfa = alfa;
            _bias = bias;
            _doubleData = ConvertToDoubleDataOnly(_data);
            FillWeights();
        }
        
        // albo jak sie nie nauczy albo przez maxEpok
        public void Learn(int maxEpoch)
        {
            int actualOutput;
            int expectedOutput;
            List<double> weights = _weights;
            double bias = _bias;
            List<List<double>> tmpDouble = _doubleData;
            
            // "Iris-virginica" - 1
            // "Iris-versicolor" - 0
            List<string> tmpString = ConvertToStringDataOnly(_data);
            for (int j = 0; j < maxEpoch; j++)
            {
                for (int i = 0; i < tmpString.Count; i++)
                {
                    expectedOutput = tmpString[i] == "Iris-virginica" ? 1 : 0;
                    actualOutput = Net(bias, i, weights);

                    if (actualOutput != expectedOutput)
                    {
                        weights = CountWeight(_alfa, expectedOutput, actualOutput, tmpDouble[i], weights);
                        bias = CountBias(_alfa, expectedOutput, actualOutput, bias);
                    }
                    else
                    {
                        _accuracy++;
                    }
                }
            }
            _weights = weights;
            _bias = bias;
            Console.WriteLine("accuracy: "+ _accuracy/maxEpoch);
        }
        
        private void FillWeights()
        {
            for (int i = 0; i < _doubleData[0].Count; i++)
            {
                _weights.Add(new Random().NextDouble());
            }
        }

        private int Net(double bias, int lineNumber, List<double> weights)
        {
            double sum = 0;
            List<double> weightTmp = weights;
            List<double> tmpDoubles = _doubleData[lineNumber];

            for (int i = 0; i < tmpDoubles.Count; i++)
            {
                sum += weightTmp[i] * tmpDoubles[i];
            }
            sum -= bias;
            if (sum >= 0)
            {
                return 1;
            }
            return 0;
        }

        // w = w`
        // predict exitvalue
        // entry value to nazwa z pliku reprezentowalna albo 1 albo 0
        private List<double> CountWeight(double alfa, int expectedOutput, double actualOutput, List<double> doubles, List<double> weights)
        {
            List<double> tmp = weights;
            List<double> tmpDoubles = doubles;
            for (int i = 0; i < doubles.Count; i++)
            {
                tmpDoubles[i] = (tmpDoubles[i]*alfa*(expectedOutput - actualOutput));
                tmp[i] = (tmp[i] + tmpDoubles[i]);
            }
            
            return tmp;
        }

        private double CountBias(double alpha, int expectedOutput, double actualOutput, double bias)
        {
            double newBias = bias - alpha * (expectedOutput - actualOutput);
            return newBias;
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