namespace Perceptron2
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            List<string> testData = File.ReadLines("J:\\NAI_ai\\NAI_ai\\Perceptron2\\Perceptron2\\data\\perceptron.test.data").ToList();
            List<string> data = File.ReadLines("J:\\NAI_ai\\NAI_ai\\Perceptron2\\Perceptron2\\data\\perceptron.data").ToList();
            // mała alfa
            Perceptron perceptron = new Perceptron(testData, data, 0.01, 3);
            perceptron.Learn(3);
        }
    }
}