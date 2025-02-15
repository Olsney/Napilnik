namespace Task_27.Model;

public class Passport
{
    private const int MinPassportLength = 10;

    public Passport(string rawData)
    {
        string data = rawData.Trim().Replace(" ", string.Empty);
        
        
        if (data.Length < MinPassportLength)
            throw new ArgumentException("Неверный формат серии или номера паспорта");

        Info = data;
    }
    
    public string Info { get; }
}