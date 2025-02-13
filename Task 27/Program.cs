using System.Data;
using System.Reflection;

namespace Task_27;

//Нужно создать хешсервис

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}

public class MessageBox
{
    public static int Show(string введитеСериюИНомерПаспорта)
    {
        throw new NotImplementedException();
    }
}

class Executor
{
    private TextBox passportTextbox; //Вьюшка - условно, текстмешпро
    private TextBox textResult; //Результат, который на вьюшке

    private void checkButton_Click(object sender, EventArgs e)
    {
        if (this.passportTextbox.Text.Trim() == "")  //Домен (модель)
        {
            MessageBox.Show("Введите серию и номер паспорта"); //View
        }
        else
        {
            string rawData = this.passportTextbox.Text.Trim().Replace(" ", string.Empty); //Модель
            if (rawData.Length < 10) //Модель
            {
                this.textResult.Text = "Неверный формат серии или номера паспорта"; //Это из презентора - все строки мы будем брать из презентора
            }
            else
            {
                string commandText = string.Format("select * from passports where num='{0}' limit 1;",
                    (object)Form1.ComputeSha256Hash(rawData)); //Это сервис по хешированию
                string connectionString = string.Format("Data Source=" +
                                                        Path.GetDirectoryName(Assembly.GetExecutingAssembly()
                                                            .Location) + "\\db.sqlite");
                try
                {
                    SQLiteConnection connection = new SQLiteConnection(connectionString);
                    connection.Open();
                    SQLiteDataAdapter sqLiteDataAdapter =
                        new SQLiteDataAdapter(new SQLiteCommand(commandText, connection));
                    DataTable dataTable1 = new DataTable();
                    DataTable dataTable2 = dataTable1;
                    sqLiteDataAdapter.Fill(dataTable2);
                    if (dataTable1.Rows.Count > 0)
                    {
                        if (Convert.ToBoolean(dataTable1.Rows[0].ItemArray[1]))
                            this.textResult.Text = "По паспорту «" + this.passportTextbox.Text +
                                                   "» доступ к бюллетеню на дистанционном электронном голосовании ПРЕДОСТАВЛЕН";
                        else
                            this.textResult.Text = "По паспорту «" + this.passportTextbox.Text +
                                                   "» доступ к бюллетеню на дистанционном электронном голосовании НЕ ПРЕДОСТАВЛЯЛСЯ";
                    }
                    else
                        this.textResult.Text = "Паспорт «" + this.passportTextbox.Text +
                                               "» в списке участников дистанционного голосования НЕ НАЙДЕН";

                    connection.Close();
                }
                catch (SQLiteException ex)
                {
                    if (ex.ErrorCode != 1)
                        return;
                    int num2 = (int)MessageBox.Show("Файл db.sqlite не найден. Положите файл в папку вместе с exe.");
                } //С 44-й по 78 строку все будет лежать в моделе в нескольких классах
            }
        }
    }
}

internal class TextBox
{
    public string Text { get; set; }
}

internal class SQLiteException : Exception
{
    public int ErrorCode { get; set; }
}

internal class SQLiteDataAdapter
{
    public SQLiteDataAdapter(SQLiteCommand sqLiteCommand)
    {
        
    }

    public void Fill(DataTable dataTable2)
    {
        
    }
}

class SQLiteCommand
{
    public SQLiteCommand(string commandText, SQLiteConnection sqLiteConnection)
    {
        
    }
}

class SQLiteConnection
{
    public SQLiteConnection(string connectionString)
    {
    }

    public void Open()
    {
        
    }

    public void Close()
    {
        
    }
}

class Form1
{
    public static string ComputeSha256Hash(string data)
    {
        return data;
    }
}

class PassportTextbox
{
    public string Text { get; set; }
}

class TextResult
{
    public string Text { get; set; }
}