namespace Task_27.View;

internal class View
{
    public TextBox PassportInfo;
    public TextBox ResultText;
    private Presenter.Presenter _presenter;

    public void OnButtonClick()
    {
        _presenter.HandleUserInputInfo(PassportInfo.Text);
    }
    
    public string GetUserInputInfo(string infoForUserInput)
    {
        // MessageBox.Show(infoForUserInput);
        return Console.ReadLine();
    }

    public void ShowResultText(string resultText)
    {
        MessageBox.Show(resultText);
    }
}

internal class TextBox
{
    public string Text { get; set; }
}