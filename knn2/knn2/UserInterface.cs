namespace knn;

public class UserInterface
{
    public string UserInput()
    {
        Console.WriteLine("Entre vector :");
        Console.WriteLine("vector format should be like in the following example:number1,number2,...");
        Console.WriteLine("double numbers format should be like in the following example: 5.7");
        string str = Console.ReadLine();
        return str;
    }
    
}