namespace CleanCode_ExampleTask2;

class Program
{
    static void Main(string[] args)
    {
    }
    
    public static int Find(int[] array, int element)
    {
        for (int i = 0; i < array.Length; i++)
            if (array[i] == element)
                return i;

        return -1;
    }
}