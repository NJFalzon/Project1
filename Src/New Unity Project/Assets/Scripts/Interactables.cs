public class Interactables
{
    private string title;
    private string text;
    private string question;
    private string answer1;
    private string answer2;
    private string answer3;
    private string answer4;

    public Interactables()
    {
        title = "";
        text = "";
        question = "";
        answer1 = "";
        answer2 = "";
        answer3 = "";
        answer4 = "";
    }

    public Interactables(string title, string text, string question, string answer1, string answer2, string answer3, string answer4)
    {
        this.title = title;
        this.text = text;
        this.question = question;
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

    public string GetQuestion()
    {
        return question;
    }

    public string GetAnswer1()
    {
        return answer1;
    }

    public string GetAnswer2()
    {
        return answer2;
    }

    public string GetAnswer3()
    {
        return answer3;
    }

    public string GetAnswer4()
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

    public void SetQuestion(string question)
    {
        this.question = question;
    }

    public void SetAnswer1(string answer1)
    {
        this.answer1 = answer1;
    }

    public void SetAnswer2(string answer2)
    {
        this.answer2 = answer2;
    }

    public void SetAnswer3(string answer3)
    {
        this.answer3 = answer3;
    }

    public void SetAnswer4(string answer4)
    {
        this.answer4 = answer4;
    }
}
