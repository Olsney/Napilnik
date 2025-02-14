using Task_27.Model;

namespace Task_27.Interfaces;

interface IConnectionService
{
    SQLiteConnection Connect();
}