public class Interactables
{
    private string title;
    private string text;
    private bool[] answers;

    public Interactables()
    {
        title = "";
        text = "";
        answers = new bool[4];
    }

    public Interactables(string title, string text, bool[] answers)
    {
        this.title = title;
        this.text = text;
        this.answers = answers;
    }

    public string GetTitle()
    {
        return title;
    }

    public string GetText()
    {
        return text;
    }

    public bool[] GetAnswers()
    {
        return answers;
    }


    public void SetTitle(string title)
    {
        this.title = title;
    }

    public void SetText(string text)
    {
        this.text = text;
    }

    public void SetAnswers(bool[] answers)
    {
        this.answers = answers;
    }
}
