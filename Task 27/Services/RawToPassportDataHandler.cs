namespace Task_27.Services;

public class RawToPassportDataHandler
{
    private const int MinRawDataLength = 10;
    
    public string Handle(string rawData)
    {
        if (rawData == null)
            throw new ArgumentException();

        string passportData = rawData.Replace(" ", string.Empty);
        
        if (passportData.Length < MinRawDataLength)
            return null;
        
        return passportData;
    }
}