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

    public void HandleUserInputInfo(string passportInfoText)
    {
        if (_dataBase.FindParticipationInfoByPassportData(passportInfoText) == false)
        {
            _passportView.ShowResultText("Неверный формат серии или номера паспорта");

            return;
        }

        _dataBase.FindParticipationInfoByPassportData(passportInfoText);

        bool? isVoted = _dataTableHandler.Handle(passportInfoText);
        
        if (isVoted == null)
        {
            _passportView.ShowResultText(
                $"По паспорту «{passportInfoText}» " +
                $"в списке участников дистанционного голосования НЕ НАЙДЕН");

            return;
        }

        if (isVoted == false)
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