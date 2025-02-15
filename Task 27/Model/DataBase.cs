using System.Data;
using System.Reflection;

namespace Task_27.Model;

class DataBase
{
    private const string FileName = "db.sqlite";
    
    private readonly DataTable _dataTableHandler;
    private readonly string _dataBaseDirectory;


    public DataBase()
    {
        _dataBaseDirectory = string.Format("Data Source=" +
                                           Path.GetDirectoryName(Assembly.GetExecutingAssembly()
                                               .Location) + "\\{FileName}");
    }

    public DataTable GetInfoFromDataTableAboutAccessToVote(string hash)
    {
        if (hash == null)
            throw new ArgumentException();

        DataTable dataTable = new DataTable();

        try
        {
            string commandText = $"select * from passports where num='{hash}' limit 1;";

            SQLiteConnection connection = new SQLiteConnection(_dataBaseDirectory);

            connection.Open();

            SQLiteCommand sqLiteCommand = new SQLiteCommand(commandText, connection);
            SQLiteDataAdapter sqLiteDataAdapter = new SQLiteDataAdapter(sqLiteCommand);


            sqLiteDataAdapter.Fill(dataTable);

            connection.Close();
        }
        catch (SQLiteException exception)
        {
            if (exception.ErrorCode != 1)
                throw new SQLiteException();

            throw new FileNotFoundException($"Файл {FileName} не найден. Положите файл в папку вместе с exe.");
        }

        return dataTable;
    }
}