using System.Data;
using Task_27.Model;

namespace Task_27.Services;

class DataTableHandler
{
    private readonly DataBase _dataBase;

    public DataTableHandler(DataBase dataBase)
    {
        _dataBase = dataBase;
    }
    
    public bool? Handle(DataTable dataTable)
    {
        
        if (InfoExist(dataTable) == false)
        {
            return null;
        }

        if (IsVoted(dataTable) == false)
        {
            return false;
        }
        
        return true;
    }
    
    private static bool InfoExist(DataTable dataTable)
    {
        return dataTable.Rows.Count > 0;
    }

    private static bool IsVoted(DataTable dataTable)
    {
        return Convert.ToBoolean(dataTable.Rows[0].ItemArray[1]);
    }
}