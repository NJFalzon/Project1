using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{
    Transform DISSERTATION;
    Transform EXPLAIN;

    TMP_Text text;
    CursorMode cm = CursorMode.Auto;
    [SerializeField] Texture2D pointer;

    [SerializeField] TextAsset article1;
    [SerializeField] TextAsset article1List;

    List<Interactables> list = new List<Interactables>();
    void Start()
    {
        DISSERTATION = GameObject.Find("Dissertation").transform;
        EXPLAIN = GameObject.Find("Explain").transform;

        text = DISSERTATION.GetChild(0).GetComponent<TMP_Text>();

        LoadTxtFiles();
    }

    
    void Update()
    {
        ClickingHighlights();
    }

    List<Interactables> ReadList()
    {
        List<Interactables> inter = new List<Interactables>();
        string[] article1list = article1List.text.Split("\n"[0]);
        int listcounter = 0;

        while (listcounter < article1list.Length-1)
        {
            Interactables temp = new Interactables();
            temp.SetTitle(article1list[listcounter++]);
            temp.SetText(article1list[listcounter++]);

            bool[] temp2 = new bool[4];
            temp2[0] = article1list[listcounter++].Equals("true") ? true : false;
            temp2[1] = article1list[listcounter++].Equals("true") ? true : false;
            temp2[2] = article1list[listcounter++].Equals("true") ? true : false;
            temp2[3] = article1list[listcounter++].Equals("true") ? true : false;

            temp.SetAnswers(temp2);
            inter.Add(temp);
        }
        return inter;
    }

    string ReadText()
    {
        string text = article1.text;
        return text;
    }

    void LoadTxtFiles()
    {
        DISSERTATION.GetChild(0).GetComponent<TextMeshProUGUI>().text = ReadText();
        list = ReadList();
    }

    void ClickingHighlights()
    {
        int hover = TMP_TextUtilities.FindIntersectingLink(text, Input.mousePosition, null);

        if (hover != -1)
        {
            Cursor.SetCursor(pointer, Vector2.zero, cm);
            if (Input.GetMouseButtonDown(0))
            {
                Color selected = new Color(0.8f, 1, 0.5f, 1);
                Color unselected = new Color(1, 1, 1, 1);

                EXPLAIN.GetChild(0).GetComponent<TextMeshProUGUI>().text = list[hover].GetTitle();
                EXPLAIN.GetChild(1).GetComponent<TextMeshProUGUI>().text = list[hover].GetText();

                EXPLAIN.GetChild(2).GetComponent<TextMeshProUGUI>().text = DecidingQuestion(hover);

                EXPLAIN.GetChild(3).GetComponent<Image>().color = list[hover].GetAnswers()[0] ? selected : unselected;
                EXPLAIN.GetChild(4).GetComponent<Image>().color = list[hover].GetAnswers()[1] ? selected : unselected;
                EXPLAIN.GetChild(5).GetComponent<Image>().color = list[hover].GetAnswers()[2] ? selected : unselected;
                EXPLAIN.GetChild(6).GetComponent<Image>().color = list[hover].GetAnswers()[3] ? selected : unselected;
            }
        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, cm);
        }
    }

    string DecidingQuestion(int hover)
    {
        string greenMark = "><mark=#00ff0050>";

        TMP_LinkInfo link = text.textInfo.linkInfo[hover];
        string linkid = link.GetLinkID();
        string temptxt = link.textComponent.text;

        if(temptxt.Contains(linkid + greenMark))
        {
            return "The Sentence is: ";
        }

        return "The Sentence is not: ";
    }
}
