using System.Globalization;

namespace k_means;

public class Kmeans
{
    private readonly int _k;
    private readonly List<string> _data;
    private List<string> _centroids;

    public Kmeans(int k ,List<string> data)
    {
        _k = k;
        _data = data;
        GenerateCentroids();
    }

    public void DoKmeans()
    {
        
    }

    // indexAndValue to dany centroid z najmniejsza wartosscia
    // trzeba dodac go do tej grupy o danym indeksie co jest centroid 
    private void GroupDistances()
    {
        List<List<double>> allCentroids = new List<List<double>>();
        List<double> distances = new List<double>();
        int index = 0;
        List<double> indexAndValue;
        
        foreach (var centroid in _centroids)
        {
            allCentroids.Add(Distances(centroid));
        }
        
        for (int i = 0; i < _data.Count; i++)
        {
            for (int j = 0; j < allCentroids.Count; j++)
            {
                distances.Add(allCentroids[i][j]);
            }
            indexAndValue = SortWithIndex(distances);
            distances.Clear();
        }
    }

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

    // dorobic unikalnosc liczb losowych
    private void GenerateCentroids()
    {
        _centroids = new List<string>();
        int index;
        for (int i = 0; i < _k; i++)
        {
            index = new Random().Next(_data.Count - 1);
            _centroids.Add(_data[index]);
            _data.RemoveAt(index);
        }
    }

    private List<double> ParseDoubles(string line)
    {
        List<string> values = line.Split(',').ToList();
        values.Remove(values[values.Count - 1]);
        return values.Select(s => double.Parse(s,NumberStyles.Any, CultureInfo.InvariantCulture)).ToList();
    }
}