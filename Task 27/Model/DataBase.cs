using System.Data;
using System.Reflection;
using Task_27.Interfaces;

namespace Task_27.Model;

class DataBase
{
    private const int MinRawDataLength = 10;

    private readonly IHashService _hashService;
    private readonly DataTable _dataTableHandler;

    private string _dataBaseDirectory;

    public DataBase(IHashService hashService)
    {
        _hashService = hashService;

        _dataBaseDirectory = string.Format("Data Source=" +
                                           Path.GetDirectoryName(Assembly.GetExecutingAssembly()
                                               .Location) + "\\db.sqlite");
    }

    public DataTable GetInfoFromDataTableAboutAccessToVote(string rawData)
    {
        if (rawData == null)
            throw new ArgumentException();
        
        string commandText = $"select * from passports where num='{HashUserData(rawData)}' limit 1;";

        SQLiteConnection connection = new SQLiteConnection(_dataBaseDirectory);
        
        connection.Open();
        
        SQLiteCommand sqLiteCommand = new SQLiteCommand(commandText, connection);
        SQLiteDataAdapter sqLiteDataAdapter = new SQLiteDataAdapter(sqLiteCommand);
        
        DataTable dataTable = new DataTable();

        sqLiteDataAdapter.Fill(dataTable);

        connection.Close();

        return dataTable;
    }

    public bool FindParticipationInfoByPassportData(string rawData)
    {
        if (rawData.Contains(rawData) == false)
            return false;

        if (rawData == null)
            throw new ArgumentException();

        string passportData = rawData.Replace(" ", string.Empty);
        

        if (passportData.Length < MinRawDataLength)
            return false;

        HashUserData(passportData);

        return true;
    }

    private string HashUserData(string rawData)
    {
        return _hashService.Hash(rawData);
    }
}