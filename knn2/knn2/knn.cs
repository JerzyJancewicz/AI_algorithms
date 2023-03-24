using System.Globalization;

namespace knn
{
    public class Knn
    {
        private List<string> _data;
        private List<string> _testData;
        private int _k;
        private string _species = "";
        public Knn(List<string> data, List<string> testData, int k)
        {
            _k = k;
            _testData = testData;
            _data = data;
        }
        
        /**
         * Shows the accuracy of an algorithm
         */
        public void ShowKnnAccuracy()
        {
            int countAccuracy = 0;
            foreach (string line in _testData)
            {
                _species = line.Split(',').ToList()[line.Split(',').ToList().Count - 1];
                List<Tuple<double,int>> kN = KNearest(_k, Distances(line));
                
                List<string> kNearest = new List<string>();
                // adding k nearest species (strings)
                for (int i = 0; i < _k; i++)
                {
                    kNearest.Add(_data[kN[i].Item2].Split(',')[line.Split(',').Length - 1]);
                }
                if (_species == MostCommon(kNearest))
                {
                    countAccuracy++;
                }
            }
            Console.WriteLine((double)countAccuracy/_testData.Count*100+" %");
        }

        /**
         * returns species for (string) input point
         */
        public string ConsoleKnnClassification(string point)
        {
            point += ",tmp";
            List<Tuple<double,int>> kN = KNearest(_k, Distances(point));
            List<string> kNearest = new List<string>();
            for (int i = 0; i < _k; i++)
            {
                kNearest.Add(_data[kN[i].Item2].Split(',')[point.Split(',').Length - 1]);
            }
            return MostCommon(kNearest);
        }

        /**
         * returns List of doubles that contains all distances between point and all points in _data file
         */
        private List<double> Distances(string line)
        {
            double distance;
            List<double> distances = new List<double>();
            foreach (string line2 in _data)
            {
                // euclidean distance for infinite dimensions
                distance = Math.Sqrt(ParseDoubles(line2).Zip(ParseDoubles(line), (a, b) => Math.Pow(a - b, 2)).Sum());
                distances.Add(distance);
            }
            return distances;
        }

        /**
         * Split variables and remove species from string list and then convert string list to double list
         */
        private List<double> ParseDoubles(string line)
        {
            List<string> values = line.Split(',').ToList();
            values.Remove(values[values.Count - 1]);
            return values.Select(s => double.Parse(s,NumberStyles.Any, CultureInfo.InvariantCulture)).ToList();
        }

        /**
         * returns Tuple List that contains k nearest points (lowest values) and indexes of those points from a file
         * Item1 - value  Item2 - index
         */
        private List<Tuple<double, int>> KNearest(int k, List<double> distances)
        {
            List<Tuple<double, int>> tmp = new List<Tuple<double, int>>();
            for (int i = 0; i < distances.Count; i++)
            {
                tmp.Add(Tuple.Create(distances[i], i));
            }
            tmp.Sort((x, y) => x.Item1.CompareTo(y.Item1));
            List<Tuple<double, int>> kClosest = tmp.Take(k).ToList();
            return kClosest;
        }

        /**
         * returns most common string from strings
         */
        private string MostCommon(List<string> strings)
        {
            string mostCommonString = strings
                .GroupBy(s => s)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .First();
            return mostCommonString;
        }
    }
}