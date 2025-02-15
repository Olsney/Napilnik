using System.Data;
using System.Reflection;
using Task_27.Interfaces;
using Task_27.Services;

namespace Task_27.Model;

class DataBase
{
    private readonly IHashService _hashService;
    private readonly RawToPassportDataHandler _rawToPassportDataHandler;
    private readonly DataTable _dataTableHandler;
    private readonly string _dataBaseDirectory;


    public DataBase(IHashService hashService, RawToPassportDataHandler rawToPassportDataHandler)
    {
        _hashService = hashService;
        _rawToPassportDataHandler = rawToPassportDataHandler;

        _dataBaseDirectory = string.Format("Data Source=" +
                                           Path.GetDirectoryName(Assembly.GetExecutingAssembly()
                                               .Location) + "\\db.sqlite");
    }

    public DataTable GetInfoFromDataTableAboutAccessToVote(string rawData)
    {
        if (rawData == null)
            throw new ArgumentException();

        string passportUserData = HandleData(rawData);

        if (passportUserData == null)
            throw new ArgumentException();
        
        string commandText = $"select * from passports where num='{HashUserData(passportUserData)}' limit 1;";

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
        if (rawData == null)
            return false;

        if (_rawToPassportDataHandler.Handle(rawData) == null)
            return false;

        return true;
    }

    private string HandleData(string rawData) => 
        _rawToPassportDataHandler.Handle(rawData);

    private string HashUserData(string rawData) => 
        _hashService.Hash(rawData);
}