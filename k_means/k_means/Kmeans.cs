using System.Globalization;

namespace k_means;

public class Kmeans
{
    private readonly int _k;
    private readonly List<string> _data;
    private List<string> _centroids;
    private double _sum;
    private Dictionary<int, List<List<double>>> _groupedLines;

    public Kmeans(int k ,List<string> data)
    {
        _k = k;
        _data = data;
        GenerateCentroids();
    }

    // accumulate all functions
    // making algorithm until 2 groups would be the same
    public void DoKmeans()
    {
        Dictionary<int, List<List<double>>> tmpDictionary = new Dictionary<int, List<List<double>>>();
        bool haveSameNumberOfLists = false;
        while (!haveSameNumberOfLists)
        {
            GroupDistances();
            _centroids = NewCentroids(_groupedLines);
            haveSameNumberOfLists = AreInTheSameGroups(_groupedLines, tmpDictionary);
            tmpDictionary = _groupedLines;
            Console.WriteLine(_sum);
            _sum = 0;
        }
        ShowSquads();
    }

    private void ShowSquads()
    {
        foreach (var pair in _groupedLines)
        {
            int groupIndex = pair.Key;
            List<List<double>> groupList = pair.Value;

            Console.WriteLine("Grupa " + groupIndex + " sklad: " + groupList.Count);
        }        }
    }

    // checking dictionaries if the number of Lists in each group is the same as in previous dictionary
    private bool AreInTheSameGroups(Dictionary<int, List<List<double>>> firstDictionary, Dictionary<int, List<List<double>>> secondDictionary)
    {
        bool haveSameNumberOfLists = true;

        foreach (var pair in firstDictionary)
        {
            int key = pair.Key;
            List<List<double>> firstGroupLists = pair.Value;

            if (secondDictionary.TryGetValue(key, out List<List<double>> secondGroupLists))
            {
                if (firstGroupLists.Count != secondGroupLists.Count)
                {
                    haveSameNumberOfLists = false;
                    break;
                }
            }
            else
            {
                haveSameNumberOfLists = false;
                break;
            }
        }
        return haveSameNumberOfLists;
    }

    // grouping distances and adding them to dictionary in the correct order and form 
    private void GroupDistances()
    {
        List<List<double>> allCentroids = new List<List<double>>();
        List<double> distances = new List<double>();
        _groupedLines = new Dictionary<int, List<List<double>>>();

        foreach (var centroid in _centroids)
        {
            allCentroids.Add(Distances(centroid));
        }
        
        for (int i = 0; i < _data.Count; i++)
        {
            for (int j = 0; j < allCentroids.Count; j++)
            {
                distances.Add(allCentroids[j][i]);
            }
            var indexAndValue = SortWithIndex(distances);
            _sum += indexAndValue[0];
            
            int centroidIndex = (int)indexAndValue[1];
            if (!_groupedLines.ContainsKey(centroidIndex))
            {
                _groupedLines[centroidIndex] = new List<List<double>>();
            }
            _groupedLines[centroidIndex].Add(ParseDoubles(_data[i]));
            distances.Clear();
        }
    }

    // making new centroids (new points)
    private List<string> NewCentroids(Dictionary<int, List<List<double>>> dictionary)
    {
        List<string> newCentroids = new List<string>();
        double sum = 0;
        foreach (var pair in dictionary)
        {
            List<List<double>> lines = pair.Value;
            var newPoints = new List<double>();
            for (int i = 0; i < lines[0].Count; i++)
            {
                foreach (var t in lines)
                {
                    sum += t[i];
                }
                newPoints.Add(sum/lines.Count);
                sum = 0;
            }
            newCentroids.Add(string.Join(",", newPoints.Select(x => x.ToString(CultureInfo.InvariantCulture))));
        }
        return newCentroids;
    }

    // returns List of doubles that contains 
    // index 0: value
    // index 1: index
    // of the lowest number from distances
    private List<double> SortWithIndex(List<double> distances)
    {
        List<Tuple<double, int>> tmp = new List<Tuple<double, int>>();
        List<double> indexAndValue = new List<double>();
        for (int i = 0; i < distances.Count; i++)
        {
            tmp.Add(Tuple.Create(distances[i], i));
        }
        tmp.Sort((x, y) => x.Item1.CompareTo(y.Item1));
        indexAndValue.Add(tmp[0].Item1);
        indexAndValue.Add(tmp[0].Item2);
        return indexAndValue;
    }

    // counting euclidean distance from points from "line" by function "ParseDoubles"
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

    // generating "_k" random (uniq) centroids from data 
    private void GenerateCentroids()
    {
        _centroids = new List<string>();
        HashSet<int> numbers = new HashSet<int>();
        Random random = new Random();
        while (numbers.Count < _k)
        {
            int randomNumber = random.Next(0, _data.Count - 32);
            numbers.Add(randomNumber);
        }
        foreach (int number in numbers)
        {
            _centroids.Add(_data[number]);
            _data.RemoveAt(number);
        }
    }

    // Parsing doubles and changing data to correct form
    private List<double> ParseDoubles(string line)
    {
        List<string> values = line.Split(',').ToList();
        values.Remove(values[values.Count - 1]);
        return values.Select(s => double.Parse(s,NumberStyles.Any, CultureInfo.InvariantCulture)).ToList();
    }
}