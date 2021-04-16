using System.Collections.Generic;
using System.IO;
using Unity.Mathematics;
using UnityEngine;

public class Interactables : MonoBehaviour
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

    public string ToString(string title)
    {
        string txt = title + "\n" + text + "\n" + Boolstring();
        
        return txt;
    }

    string Boolstring()
    {
        string[] temp = new string[4];

        for (int n = 0; n < temp.Length; n++)
        {
            if (answers[n])
            {
                temp[n] = "true";
            }
            else
            {
                temp[n] = "false";
            }
        }

        return temp[0] + "\n" + temp[1] + "\n" + temp[2] + "\n" + temp[3] + "\n";
    }

    public Interactables ToInteractables(StreamReader reader)
    {
        Interactables tempI = new Interactables();

        tempI.SetTitle(reader.ReadLine());
        tempI.SetText(reader.ReadLine());

        bool[] temp = new bool[4];
        for (int j = 0; j < temp.Length; j++)
        {
            if (reader.ReadLine() == "true")
            {
                temp[j] = true;
            }
            else
            {
                temp[j] = false;
            }
            tempI.SetAnswers(temp);
        }
        
        return tempI;
    }

    public int2 Compare(Interactables b)
    {
        int2 points= new int2(0,0);
        for (int i = 0; i<4; i++)
        {
            if(answers[i] == b.answers[i])
            {
                points.x++;
            }
            points.y++;
        }
        return points;
    }
}
