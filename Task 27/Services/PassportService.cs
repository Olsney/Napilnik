using System.Data;
using Task_27.Interfaces;
using Task_27.Model;

namespace Task_27.Services;

class PassportService
{
    private readonly DataBase _dataBase;
    private readonly IHashService _hashService;

    public PassportService(DataBase dataBase, IHashService hashService)
    {
        _dataBase = dataBase;
        _hashService = hashService;
    }
    
    public bool? Handle(Passport passport)
    {
        string hash = _hashService.Hash(passport.Info);
        
        DataTable dataTable = _dataBase.GetInfoFromDataTableAboutAccessToVote(hash);

        if (InfoExist(dataTable) == false)
            throw new InvalidOperationException("");

        if (IsVoted(dataTable) == false)
            return false;
        
        return true;
    }
    
    private static bool InfoExist(DataTable dataTable) => 
        dataTable.Rows.Count > 0;

    private static bool IsVoted(DataTable dataTable) => 
        Convert.ToBoolean(dataTable.Rows[0].ItemArray[1]);
}