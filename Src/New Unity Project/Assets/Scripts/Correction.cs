using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class Correction : MonoBehaviour
{
    TextMeshProUGUI correct;
    TextMeshProUGUI student;

    List<Interactables> correctList;
    List<Interactables> answeredList;

    Transform CORRECTEDPAPER;
    Transform ANSWEREDPAPER;
    Transform EXPLANATION;

    [SerializeField] TextAsset article2;
    [SerializeField] TextAsset article2List;

    [SerializeField] Texture2D pointer;
    CursorMode cm = CursorMode.Auto;
    private void Awake()
    {
        correctList = new List<Interactables>();
        answeredList = new List<Interactables>();

        CORRECTEDPAPER = GameObject.Find("CorrectPaper").transform;
        ANSWEREDPAPER = GameObject.Find("AnsweredPaper").transform;
        EXPLANATION = GameObject.Find("Explanation").transform;

        correct = CORRECTEDPAPER.GetChild(1).GetComponent<TextMeshProUGUI>();
        student = ANSWEREDPAPER.GetChild(1).GetComponent<TextMeshProUGUI>();

        correct.text = article2.ToString();

        student.text = TestScript.finishedText;
        answeredList = TestScript.finishedList;
    }

    void Start()
    {
        correct.ForceMeshUpdate();
        student.ForceMeshUpdate();

        correctList = ReadTextFiles();

        EXPLANATION.GetChild(2).GetComponent<TextMeshProUGUI>().text = Correct(); 
    }

    void Update()
    {
        ShowAnswers();
    }

    string Correct()
    {
        string greenMark = "><mark=#00ff0050>";
        string redMark = "><mark=#ff000050>";

        int points = 0;
        int totalPoints = 0;

        for (int i = 0; i < correct.textInfo.linkCount; i++)
        {
            string tempCorrect = correct.textInfo.linkInfo[i].GetLinkID();
            string tempAnswered = student.textInfo.linkInfo[i].GetLinkID();

            if(correct.text.Contains(tempCorrect + greenMark))
            {
                if(student.text.Contains(tempAnswered + greenMark))
                {
                    if (answeredList[i].GetAnswers().SequenceEqual(correctList[i].GetAnswers()))
                    {
                        points++;
                    }
                }
            }

            if(correct.text.Contains(tempCorrect + redMark))
            {
                if (student.text.Contains(tempAnswered + redMark))
                {
                    if (answeredList[i].GetAnswers().SequenceEqual(correctList[i].GetAnswers()))
                    {
                        points++;
                    }
                }
            }
            totalPoints++;
        }

        
            
        

        float mark = (float)points / totalPoints;
        mark *= 100;
        return "You got: " + mark.ToString("##0") + "%";
    }

    List<Interactables> ReadTextFiles()
    {
        List<Interactables> inter = new List<Interactables>();
        string[] article2list = article2List.text.Split("\n"[0]);
        int listcounter = 0;

        while (listcounter < article2list.Length - 1)
        {
            Interactables temp = new Interactables();
            temp.SetTitle(article2list[listcounter++]);
            temp.SetText(article2list[listcounter++]);

            bool[] temp2 = new bool[4];
            temp2[0] = article2list[listcounter++].Equals("true") ? true : false;
            temp2[1] = article2list[listcounter++].Equals("true") ? true : false;
            temp2[2] = article2list[listcounter++].Equals("true") ? true : false;
            temp2[3] = article2list[listcounter++].Equals("true") ? true : false;

            temp.SetAnswers(temp2);
            inter.Add(temp);
        }
        return inter;
    }

    void ShowAnswers()
    {
        int hoverA = TMP_TextUtilities.FindIntersectingLink(correct, Input.mousePosition, null);
        int hoverB = TMP_TextUtilities.FindIntersectingLink(student, Input.mousePosition, null);

        int hover = hoverA != -1 ? hoverA : hoverB != -1 ? hoverB : -1;

        if (hover != -1)
        {
            Cursor.SetCursor(pointer, Vector2.zero, cm);
            if (Input.GetMouseButtonDown(0))
            {
                TMP_LinkInfo linkInfo = correct.textInfo.linkInfo[hover];
                EXPLANATION.GetChild(0).GetComponent<TextMeshProUGUI>().text = linkInfo.GetLinkText();



                string textA = correct.text.Contains(correct.textInfo.linkInfo[hover].GetLinkID() + "><mark=#00ff0050>") 
                                ? "The Correct Sentence is: " : "The Correct Sentence is NOT: ";

                textA += correctList[hover].GetAnswers()[0] ? "Formal, " : "";
                textA += correctList[hover].GetAnswers()[1] ? "Grammatically Correct, " : "";
                textA += correctList[hover].GetAnswers()[2] ? "Relevant, " : "";
                textA += correctList[hover].GetAnswers()[3] ? "Technical " : "";

                string textB = student.text.Contains(correct.textInfo.linkInfo[hover].GetLinkID() + "><mark=#00ff0050>")
                                ? "The Answered Sentence is: " : "The Answered Sentence is NOT: ";

                textB += answeredList[hover].GetAnswers()[0] ? "Formal, " : "";
                textB += answeredList[hover].GetAnswers()[1] ? "Grammatically Correct, " : "";
                textB += answeredList[hover].GetAnswers()[2] ? "Relevant, " : "";
                textB += answeredList[hover].GetAnswers()[3] ? "Technical " : "";

                EXPLANATION.GetChild(1).GetComponent<TextMeshProUGUI>().text = textA + "\n" + textB;
            }
        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, cm);
        }
    }
}
