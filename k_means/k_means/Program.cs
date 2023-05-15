namespace k_means
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Wybierz ilosc centroidow");
            int i = int.Parse(Console.ReadLine() ?? string.Empty);
            
            List<string> data = File.ReadAllLines("J:\\NAI_ai\\NAI_ai\\k_means\\k_means\\iris.data").ToList();
            Kmeans kmeans = new Kmeans(i, data);
            kmeans.DoKmeans();
        }
    }
}