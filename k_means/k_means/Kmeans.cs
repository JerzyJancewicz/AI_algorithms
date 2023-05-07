using System.Globalization;

namespace k_means;

public class Kmeans
{
    private int _k;
    private readonly List<string> _data;
    private List<List<double>> centroids;

    public Kmeans(int k ,List<string> data)
    {
        _k = k;
        _data = data;
        GenerateCentroids();
    }

    private void GenerateCentroids()
    {
        centroids = new List<List<double>>();
        for (int i = 0; i < _k; _k++)
        {
            centroids.Add(ParseDoubles(_data[new Random().Next(_data.Count - 1)]));
        }
    }

    private List<double> ParseDoubles(string line)
    {
        List<string> values = line.Split(',').ToList();
        values.Remove(values[values.Count - 1]);
        return values.Select(s => double.Parse(s,NumberStyles.Any, CultureInfo.InvariantCulture)).ToList();
    }
    
}