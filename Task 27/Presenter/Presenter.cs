using System.Data;
using Task_27.Model;
using Task_27.Services;

namespace Task_27.Presenter;

class Presenter
{
    private readonly DataBase _dataBase;
    private readonly View.View _passportView;
    private readonly DataTableHandler _dataTableHandler;

    public Presenter(DataBase dataBase, View.View passportView, DataTableHandler DataTableHandler)
    {
        _dataBase = dataBase;
        _passportView = passportView;
        _dataTableHandler = DataTableHandler;
    }

    public void Initialize()
    {
        string userInput = _passportView.GetUserInputInfo("Введите серию и номер паспорта");

        // if (_dataHandler.TryLogInByPassportData(userInput) == false)
        // {
        //     _passportView.ShowResultText("Неверный формат серии или номера паспорта");
        //
        //     return;
        // }
        //
        // _dataHandler.TryLogInByPassportData(userInput);
        //
        // SQLiteConnection connection = _connectionService.Connect();
        //
        // DataTable dataTable = _dataHandler.GetInfoFromDataTableAboutAccessToVote(connection);
        //
        //
        // if (AnyInfoAboutUserExist(dataTable) == false)
        // {
        //     _passportView.ShowResultText(
        //         $"По паспорту «{userInput}» " +
        //         $"в списке участников дистанционного голосования НЕ НАЙДЕН");
        //
        //     return;
        // }
        //
        // if (CanVote(dataTable) == false)
        // {
        //     _passportView.ShowResultText(
        //         $"По паспорту «{userInput}» " +
        //         $"доступ к бюллетеню на дистанционном электронном голосовании НЕ ПРЕДОСТАВЛЯЕТСЯ");
        //
        //     return;
        // }
        //
        // _passportView.ShowResultText(
        //     $"По паспорту «{userInput}» " +
        //     $"доступ к бюллетеню на дистанционном электронном голосовании ПРЕДОСТАВЛЕН");
    }

    private static bool AnyInfoAboutUserExist(DataTable dataTable)
    {
        return dataTable.Rows.Count > 0;
    }

    private static bool CanVote(DataTable dataTable)
    {
        return Convert.ToBoolean(dataTable.Rows[0].ItemArray[1]);
    }

    public void HandleUserInputInfo(string passportInfoText)
    {
        if (_dataBase.FindParticipationInfoByPassportData(passportInfoText) == false)
        {
            _passportView.ShowResultText("Неверный формат серии или номера паспорта");

            return;
        }

        _dataBase.FindParticipationInfoByPassportData(passportInfoText);

        SQLiteConnection connection = _connectionService.Connect();

        DataTable dataTable = _dataBase.GetInfoFromDataTableAboutAccessToVote();
        
        if (_dataTableHandler.Handle(dataTable))
        {
            _passportView.ShowResultText(
                $"По паспорту «{passportInfoText}» " +
                $"в списке участников дистанционного голосования НЕ НАЙДЕН");

            return;
        }

        if (CanVote(dataTable) == false)
        {
            _passportView.ShowResultText(
                $"По паспорту «{passportInfoText}» " +
                $"доступ к бюллетеню на дистанционном электронном голосовании НЕ ПРЕДОСТАВЛЯЕТСЯ");

            return;
        }

        _passportView.ShowResultText(
            $"По паспорту «{passportInfoText}» " +
            $"доступ к бюллетеню на дистанционном электронном голосовании ПРЕДОСТАВЛЕН");
    }
}