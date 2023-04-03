using System.Globalization;

namespace Perceptron2
{
    public class Perceptron
    {
        private readonly List<string> _testData;
        private readonly List<string> _data;
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
            FillWeights();
        }
        
        // albo jak sie nie nauczy albo przez maxEpok
        public void Learn(int maxEpok)
        {
            int actualOutput;
            int expectedOutput;
            float sumAccuracy = 0;
            List<List<double>> tmpDouble = ConvertToDoubleDataOnly(_data);
            
            // "Iris-virginica" - 1
            // "Iris-versicolor" - 0
            List<string> tmpString = ConvertToStringDataOnly(_data);
            for (int j = 0; j < maxEpok; j++)
            {
                for (int i = 0; i < tmpString.Count; i++)
                {
                    if (tmpString[i] == "Iris-virginica")
                    {
                        expectedOutput = 1;
                    }
                    else
                    {
                        expectedOutput = 0;
                    }
                   // Console.WriteLine(expectedOutput);
                    // aktualizowac delte
                    actualOutput = Net(_bias, i);
                    
                    if (actualOutput != expectedOutput)
                    {
                        _weights = CountWeight(_alfa, expectedOutput, actualOutput, tmpDouble[i]);
                        _bias = CountBias(_alfa, expectedOutput, actualOutput);
                    }
                    else
                    {
                        _accuracy++;
                    }
                    
                    //Console.WriteLine(_accuracy);
                    //Console.WriteLine(tmpString.Count);
                    //Console.WriteLine("accuracy: "+ (float)_accuracy/tmpString.Count * 100);
                }
                sumAccuracy += (float)_accuracy / maxEpok;
                //Console.WriteLine(_accuracy);
                _accuracy = 0;
            }
            Console.WriteLine("accuracy: "+ sumAccuracy/tmpString.Count);
        }
        
        private void FillWeights()
        {
            for (int i = 0; i < ConvertToDoubleDataOnly(_data)[0].Count; i++)
            {
                _weights.Add(new Random().NextDouble());
            }
        }

        private int Net(double bias, int lineNumber)
        {
            double sum = 0;
            List<double> weightTmp = _weights;
            List<double> tmpDoubles = ConvertToDoubleDataOnly(_data)[lineNumber];

            for (int i = 0; i < tmpDoubles.Count; i++)
            {
                sum += tmpDoubles[i] * weightTmp[i];
            }
            Console.WriteLine("prze"+sum);
            sum += (-1) * bias;
            Console.WriteLine("po"+sum);
            if (sum >= 0)
            {
                return 1;
            }
            return 0;
        }

        // w = w`
        // predict exitvalue
        // entry value to nazwa z pliku reprezentowalna albo 1 albo 0
        private List<double> CountWeight(double alfa, int expectedOutput, double actualOutput, List<double> doubles)
        {
            List<double> tmp = _weights;
            List<double> tmpDoubles = doubles;
            for (int i = 0; i < doubles.Count; i++)
            {
                tmpDoubles[i] = (tmpDoubles[i]*(alfa*(actualOutput - expectedOutput)));
                tmp[i] = (tmp[i] + tmpDoubles[i]);
            }
            return tmp;
        }

        private double CountBias(double alpha, int expectedOutput, double actualOutput)
        {
            double newBias = _bias - alpha * (actualOutput - expectedOutput);
            //Console.WriteLine(newBias);
            return newBias;
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