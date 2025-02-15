using Task_27.Model;
using Task_27.Services;

namespace Task_27.Presenter;

class Presenter
{
    private readonly View.View _passportView;
    private readonly PassportService _passportService;

    public Presenter(View.View passportView, PassportService passportService)
    {
        _passportView = passportView;
        _passportService = passportService;
    }

    public void HandleUserInputInfo(string passportInfoText)
    {
        Passport passport = new Passport(passportInfoText);

        bool? isVoted = _passportService.Handle(passport);
        
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