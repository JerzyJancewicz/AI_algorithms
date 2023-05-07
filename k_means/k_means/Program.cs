namespace k_means
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            List<string> data = File.ReadAllLines("J:\\NAI_ai\\NAI_ai\\k_means\\k_means\\iris.data").ToList();
            Kmeans kmeans = new Kmeans(4, data);
        }
    }
}