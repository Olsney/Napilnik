namespace Task_27.Model;

internal class SQLiteException : Exception
{
    public int ErrorCode { get; set; }
}