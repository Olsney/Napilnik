namespace CleanCode_ExampleTask1;

class Program
{
    static void Main(string[] args)
    {
    }
    
    public static int Clamp(int value, int min, int max)
    {
        if (value < min)
            return min;
        else if (value > max)
            return max;
        else
            return value;
    }
}