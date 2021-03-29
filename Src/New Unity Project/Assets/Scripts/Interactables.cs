public class Interactables
{
    private string title;
    private string text;
    private bool answer1;
    private bool answer2;
    private bool answer3;
    private bool answer4;

    public Interactables()
    {
        title = "";
        text = "";
        answer1 = false;
        answer2 = false;
        answer3 = false;
        answer4 = false;
    }

    public Interactables(string title, string text,  bool answer1, bool answer2, bool answer3, bool answer4)
    {
        this.title = title;
        this.text = text;
        this.answer1 = answer1;
        this.answer2 = answer2;
        this.answer3 = answer3;
        this.answer4 = answer4;
    }

    public string GetTitle()
    {
        return title;
    }

    public string GetText()
    {
        return text;
    }

    public bool GetAnswer1()
    {
        return answer1;
    }

    public bool GetAnswer2()
    {
        return answer2;
    }

    public bool GetAnswer3()
    {
        return answer3;
    }

    public bool GetAnswer4()
    {
        return answer4;
    }


    public void SetTitle(string title)
    {
        this.title = title;
    }

    public void SetText(string text)
    {
        this.text = text;
    }

    public void SetAnswer1(bool answer1)
    {
        this.answer1 = answer1;
    }

    public void SetAnswer2(bool answer2)
    {
        this.answer2 = answer2;
    }

    public void SetAnswer3(bool answer3)
    {
        this.answer3 = answer3;
    }

    public void SetAnswer4(bool answer4)
    {
        this.answer4 = answer4;
    }
}
