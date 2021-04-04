using System.IO;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreatingTests : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    TMP_TextInfo textInfo;
    List<Interactables> interactables;

    [SerializeField] Texture2D pointer;
    readonly CursorMode cm = CursorMode.Auto;

    Transform EXPLAIN;
    Transform DISSERTATION;

    string greenMark = "><mark=#00ff0050>";
    string yellowMark = "><mark=#ffff0050>";
    string redMark = "><mark=#ff000050>";

    int clickedLink;

    void Start()
    {
        EXPLAIN = GameObject.Find("Explain").transform;
        DISSERTATION = GameObject.Find("Dissertation").transform;

        LoadText();
        text.ForceMeshUpdate();
        textInfo = text.textInfo;

        interactables = SetInteractables();
    }

    void Update()
    {
        ClickingOnHighlights();
    }

    void ClickingOnHighlights()
    {
        int hover = TMP_TextUtilities.FindIntersectingLink(text, Input.mousePosition, null);
        if (hover != -1)
        {
            Cursor.SetCursor(pointer, Vector2.zero, cm);
            if (Input.GetMouseButtonDown(0))
            {
                clickedLink = hover;
                TMP_LinkInfo linkInfo = textInfo.linkInfo[hover];

                SetExplanation();
                ChangeHighlight(KeyCode.Mouse0, linkInfo);
            }

            else if (Input.GetMouseButtonDown(1))
            {
                clickedLink = hover;
                TMP_LinkInfo linkInfo = textInfo.linkInfo[hover];
                
                SetExplanation();
                ChangeHighlight(KeyCode.Mouse1, linkInfo);
            }
        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, cm);
        }
    }

    void SetExplanation()
    {
        Color selected = new Color(0.8f, 1, 0.5f, 1);
        Color unselected = new Color(1, 1, 1, 1);

        EXPLAIN.GetChild(0).GetComponent<TextMeshProUGUI>().text = interactables[clickedLink].GetTitle();
        EXPLAIN.GetChild(1).GetComponent<TMP_InputField>().text = interactables[clickedLink].GetText();

        EXPLAIN.GetChild(3).GetComponent<Image>().color = interactables[clickedLink].GetAnswers()[0] ? selected : unselected;
        EXPLAIN.GetChild(4).GetComponent<Image>().color = interactables[clickedLink].GetAnswers()[1] ? selected : unselected;
        EXPLAIN.GetChild(5).GetComponent<Image>().color = interactables[clickedLink].GetAnswers()[2] ? selected : unselected;
        EXPLAIN.GetChild(6).GetComponent<Image>().color = interactables[clickedLink].GetAnswers()[3] ? selected : unselected;
    }

    void ChangeHighlight(KeyCode mouse, TMP_LinkInfo linkInfo)
    {
        string linkid = linkInfo.GetLinkID();
        string temptxt = linkInfo.textComponent.text;

        if (mouse == KeyCode.Mouse0)
        {
            if (temptxt.Contains(linkid + yellowMark))
            {
                DISSERTATION.GetChild(3).GetComponent<TextMeshProUGUI>().text = temptxt.Replace(linkid + yellowMark, linkid + greenMark);
            }
            else if (temptxt.Contains(linkid + redMark))
            {
                DISSERTATION.GetChild(3).GetComponent<TextMeshProUGUI>().text = temptxt.Replace(linkid + redMark, linkid + greenMark);
            }
        }

        if (mouse == KeyCode.Mouse1)
        {
            if (temptxt.Contains(linkid + yellowMark))
            {
                DISSERTATION.GetChild(3).GetComponent<TextMeshProUGUI>().text = temptxt.Replace(linkid + yellowMark, linkid + redMark);
            }
            else if (temptxt.Contains(linkid + greenMark))
            {
                DISSERTATION.GetChild(3).GetComponent<TextMeshProUGUI>().text = temptxt.Replace(linkid + greenMark, linkid + redMark);
            }
        }
    }

    List<Interactables> CreateListOfInteractables()
    {
        List<Interactables> temp = new List<Interactables>();
        
        for (int i= 0; i < textInfo.linkCount; i++)
        {
            string title = textInfo.linkInfo[i].GetLinkText();
            temp.Add(new Interactables(title, "", new bool[] { false, false, false, false, }));
        }
        return temp;
    }

    public void UpdatingDescription()
    {
        interactables[clickedLink].SetText(EXPLAIN.GetChild(1).GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text);
    }

    public void UpdatingAnswers(int buttonNumber)
    {
        bool[] temp = interactables[clickedLink].GetAnswers();
        temp[buttonNumber] = !temp[buttonNumber];

        interactables[clickedLink].SetAnswers(temp);
        ChangeButtonState(buttonNumber);
    }

    void ChangeButtonState(int buttonNumber)
    {
        Color selected = new Color(0.8f, 1, 0.5f, 1);
        Color unselected = new Color(1,1,1,1);

        Color currentColor = EXPLAIN.GetChild(buttonNumber + 3).GetComponent<Image>().color;

        EXPLAIN.GetChild(buttonNumber + 3).GetComponent<Image>().color = currentColor == unselected ? selected : unselected;
    }

    public void WriteTextFiles()
    {
        string pathText = "Assets/Resources/Corrected.txt";
        string pathList= "Assets/Resources/List.txt";

        File.WriteAllText(pathList, interactables[0].ToString(textInfo.linkInfo[0].GetLinkText()));
        for (int i = 1; i < interactables.Count; i++)
        {
            File.AppendAllText(pathList, interactables[i].ToString(textInfo.linkInfo[i].GetLinkText()));
        }

        File.WriteAllText(pathText, text.text);
    }

    List<Interactables> ReadTextFiles()
    {
        List<Interactables> inter = new List<Interactables>();
        string pathList = "Assets/Resources/List.txt";

        StreamReader reader = new StreamReader(pathList);

        for (int i = 0; i < text.textInfo.linkCount; i++)
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
            }

            tempI.SetAnswers(temp);
            inter.Add(tempI);
        }
        return inter;
    }

    List<Interactables> SetInteractables()
    {
        if (File.Exists("Assets/Resources/List.txt"))
        {
            return ReadTextFiles();
        }
        else
        {
            return CreateListOfInteractables();
        }
    }

    void LoadText()
    {
        string text;
        StreamReader reader;
        if (File.Exists("Assets/Resources/Corrected.txt"))
        {
            reader = new StreamReader("Assets/Resources/Corrected.txt");   
        }
        else
        {
            reader = new StreamReader("Assets/Resources/Default.txt");
        }
        text = reader.ReadToEnd();
        reader.Close();

        DISSERTATION.GetChild(3).GetComponent<TextMeshProUGUI>().text = text;
    }
}
