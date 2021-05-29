using TMPro;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class TestScript : MonoBehaviour
{
    int hover;
    TMP_Text text;
    
    Transform THESIS;
    Transform QUESTIONS;

    List<Interactables> list;

    CursorMode cm = CursorMode.Auto;
    [SerializeField] Texture2D pointer;

    public static string finishedText;
    public static List<Interactables> finishedList;

    [SerializeField] TextAsset article2;

    void Start()
    {
        THESIS   = GameObject.Find("Thesis").transform;
        QUESTIONS = GameObject.Find("Questions").transform;

        text = THESIS.GetComponentInChildren<TMP_Text>();
        LoadDefaultThesis();
        text.ForceMeshUpdate();
        
        list = InstantiateList();
    }
    
    void Update()
    {
        ClickingHighlightedText();
    }

    void LoadDefaultThesis()
    {
        THESIS.GetComponentInChildren<TextMeshProUGUI>().text = article2.ToString();
    }

    List<Interactables> InstantiateList()
    {
        List<Interactables> inter = new List<Interactables>();

        for (int i = 0; i < text.textInfo.linkCount; i++)
        {
            string title = text.textInfo.linkInfo[i].GetLinkText();
            inter.Add(new Interactables(title, "", new bool[4] { false, false, false, false })) ;
        }
        return inter;
    }

    void ClickingHighlightedText()
    {
        int hover = TMP_TextUtilities.FindIntersectingLink(text, Input.mousePosition, null);
        
        if (hover != -1)
        {
            Cursor.SetCursor(pointer, Vector2.zero, cm);
            KeyCode key = Input.GetMouseButtonDown(0) ? KeyCode.Mouse0 : Input.GetMouseButtonDown(1) 
                    ? KeyCode.Mouse1 : KeyCode.None;

            if (key == KeyCode.Mouse0 || key == KeyCode.Mouse1) 
            {
                this.hover = hover;
                
                ChangeHighlight(key, hover);
                SetQuestion(hover);
            }
        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, cm);
        }
    }
    

    void ChangeHighlight(KeyCode key, int hover)
    {
        TMP_LinkInfo linkInfo = text.textInfo.linkInfo[hover];
        string linkid = text.textInfo.linkInfo[hover].GetLinkID();
        string temptxt = linkInfo.textComponent.text;

        string greenMark = "><mark=#00ff0050>";
        string yellowMark = "><mark=#ffff0050>";
        string redMark = "><mark=#ff000050>";

        if (key == KeyCode.Mouse0)
        {
            if (temptxt.Contains(linkid + yellowMark))
            {
                THESIS.GetChild(0).GetComponent<TextMeshProUGUI>().text = temptxt.Replace(linkid + yellowMark, linkid + greenMark);
            }
            else if (temptxt.Contains(linkid + redMark))
            {
                THESIS.GetChild(0).GetComponent<TextMeshProUGUI>().text = temptxt.Replace(linkid + redMark, linkid + greenMark);
            }
        }

        if (key == KeyCode.Mouse1)
        {
            if (temptxt.Contains(linkid + yellowMark))
            {
                THESIS.GetChild(0).GetComponent<TextMeshProUGUI>().text = temptxt.Replace(linkid + yellowMark, linkid + redMark);
            }
            else if (temptxt.Contains(linkid + greenMark))
            {
                THESIS.GetChild(0).GetComponent<TextMeshProUGUI>().text = temptxt.Replace(linkid + greenMark, linkid + redMark);
            }
        }
    }

    void SetQuestion(int hover)
    {
        Color selected = new Color(0.8f, 1, 0.5f, 1);
        Color unselected = new Color(1, 1, 1, 1);

        QUESTIONS.GetChild(0).GetComponent<TextMeshProUGUI>().text = list[hover].GetTitle();

        QUESTIONS.GetChild(1).GetComponent<TextMeshProUGUI>().text = DecidingQuestion(hover);

        QUESTIONS.GetChild(2).GetComponent<Image>().color = list[hover].GetAnswers()[0] ? selected : unselected;
        QUESTIONS.GetChild(3).GetComponent<Image>().color = list[hover].GetAnswers()[1] ? selected : unselected;
        QUESTIONS.GetChild(4).GetComponent<Image>().color = list[hover].GetAnswers()[2] ? selected : unselected;
        QUESTIONS.GetChild(5).GetComponent<Image>().color = list[hover].GetAnswers()[3] ? selected : unselected;
    }

    string DecidingQuestion(int hover)
    {
        string greenMark = "><mark=#00ff0050>";

        TMP_LinkInfo link = text.textInfo.linkInfo[hover];
        string linkid = link.GetLinkID();
        string temptxt = link.textComponent.text;

        if (temptxt.Contains(linkid + greenMark))
        {
            return "The Sentence is: ";
        }

        return "The Sentence is not: ";
    }

    public void UpdatingAnswers(int buttonNumber)
    {
        bool[] temp = list[hover].GetAnswers();
        temp[buttonNumber] = !temp[buttonNumber];

        list[hover].SetAnswers(temp);
        ChangeButtonState(buttonNumber);
    }

    void ChangeButtonState(int buttonNumber)
    {
        Color selected = new Color(0.8f, 1, 0.5f, 1);
        Color unselected = new Color(1, 1, 1, 1);

        Color currentColor = QUESTIONS.GetChild(buttonNumber + 2).GetComponent<Image>().color;

        QUESTIONS.GetChild(buttonNumber + 2).GetComponent<Image>().color = currentColor == unselected ? selected : unselected;
    }

    public void Finish()
    {
        finishedText = THESIS.GetComponentInChildren<TextMeshProUGUI>().text;
        finishedList = list;

        SceneManager.LoadScene("Corrections");
    }
}
