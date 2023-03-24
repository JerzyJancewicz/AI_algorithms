namespace knn
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            List<string> data = File.ReadAllLines("J:\\NAI_ai\\NAI_ai\\knn2\\knn2\\data\\iris.data").ToList();
            List<string> testData = File.ReadAllLines("J:\\NAI_ai\\NAI_ai\\knn2\\knn2\\data\\iris.test.data").ToList();
            Knn knn = new Knn(data, testData, 3);
            knn.ShowKnnAccuracy();
            Console.WriteLine();
            
            UserInterface userInterface = new UserInterface();
            while (true)
            {
                Console.WriteLine(knn.ConsoleKnnClassification(userInterface.UserInput()));
                Console.WriteLine();
            }
        }
    }
}