namespace Task_27.View;

internal class View
{
    private TextBox _passportInfo;
    private TextBox _resultText;
    
    private Presenter.Presenter _presenter;

    public void ShowResultText(string resultText) => 
        MessageBox.Show(resultText);

    private void OnButtonClick() => 
        _presenter.HandleUserInputInfo(_passportInfo.Text);
}