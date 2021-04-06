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

    List<Interactables> list = new List<Interactables>();
    void Start()
    {
        DISSERTATION = GameObject.Find("Dissertation").transform;
        EXPLAIN = GameObject.Find("Explain").transform;

        text = DISSERTATION.GetChild(3).GetComponent<TMP_Text>();

        LoadTxtFiles();
    }

    
    void Update()
    {
        ClickingHighlights();
    }

    List<Interactables> ReadList()
    {
        List<Interactables> inter = new List<Interactables>();
        string pathList = "Assets/Resources/List.txt";

        StreamReader reader = new StreamReader(pathList);

        while (!reader.EndOfStream){
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
            }

            tempI.SetAnswers(temp);
            inter.Add(tempI);
        }

        return inter;
    }

    string ReadText()
    {
        StreamReader reader = new StreamReader("Assets/Resources/Corrected.txt");
        string text = reader.ReadToEnd();
        reader.Close();

        return text;
    }

    void LoadTxtFiles()
    {
        DISSERTATION.GetChild(3).GetComponent<TextMeshProUGUI>().text = ReadText();
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
