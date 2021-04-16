using System.Collections.Generic;
using System.IO;
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

        StreamReader reader = new StreamReader("Assets/Resources/Correct.txt");
        correct.text = reader.ReadToEnd();
        reader.Close();

        reader = new StreamReader("Assets/Resources/List.txt");
        while (!reader.EndOfStream)
        {
            Interactables temp = new Interactables();
            correctList.Add(temp.ToInteractables(reader));
        }
        reader.Close();        

        student.text = TestScript.finishedText;
        answeredList = TestScript.finishedList;
    }

    void Start()
    {
        correct.ForceMeshUpdate();
        student.ForceMeshUpdate();

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
                    points++;
                }
            }

            if(correct.text.Contains(tempCorrect + redMark))
            {
                if (student.text.Contains(tempAnswered + redMark))
                {
                    points++;
                }
            }
            totalPoints++;
        }

        for(int i = 0; i < correct.textInfo.linkCount; i++)
        {
            int2 temp = correctList[i].Compare(answeredList[i]);
            points += temp.x;
            totalPoints += temp.y;
        }

        float mark = (float)points / totalPoints;
        mark *= 100;
        return "You got: " + mark.ToString("00") + "%";
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
