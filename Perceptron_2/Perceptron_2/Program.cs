namespace Perceptron2
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            //List<string> testData = File.ReadLines("J:\\NAI_ai\\NAI_ai\\Perceptron2\\Perceptron2\\data\\perceptron.test.data").ToList();
            //List<string> data = File.ReadLines("J:\\NAI_ai\\NAI_ai\\Perceptron2\\Perceptron2\\data\\perceptron.data").ToList();
            List<string> testDataLap = File.ReadLines("C:\\Users\\jance\\NAI_Projekty\\NAI_ai\\Perceptron_2\\Perceptron_2\\data\\perceptron.test.data").ToList();
            List<string> dataLap = File.ReadLines("C:\\Users\\jance\\NAI_Projekty\\NAI_ai\\Perceptron_2\\Perceptron_2\\data\\perceptron.data").ToList();
            // mała alfa
            Perceptron perceptron = new Perceptron(testDataLap, dataLap, 0.01, 0.5);
            perceptron.Learn(1);
        }
    }
}