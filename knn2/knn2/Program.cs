namespace knn
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            List<string> data = File.ReadAllLines("C:\\Users\\jance\\NAI_Projekty\\NAI_ai\\knn2\\knn2\\data\\iris.data").ToList();
            List<string> testData = File.ReadAllLines("C:\\Users\\jance\\NAI_Projekty\\NAI_ai\\knn2\\knn2\\data\\iris.test.data").ToList();
            List<string> data2 = File.ReadAllLines("C:\\Users\\jance\\NAI_Projekty\\NAI_ai\\knn2\\knn2\\data\\wdbc.data").ToList();
            List<string> testData2 = File.ReadAllLines("C:\\Users\\jance\\NAI_Projekty\\NAI_ai\\knn2\\knn2\\data\\wdbc.test.data").ToList();
            Knn knn = new Knn(data2, testData2, 3);
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