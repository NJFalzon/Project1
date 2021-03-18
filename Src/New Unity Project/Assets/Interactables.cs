using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Interactables : MonoBehaviour
{
    [SerializeField] string boardTitle;
    [SerializeField] string boardText;
    [SerializeField] string question;
    [SerializeField] string answer1;
    [SerializeField] string answer2;
    [SerializeField] string answer3;
    [SerializeField] string answer4;
    
    public void Clicked()
    {
        GameObject chalk = GameObject.Find("ChalkBoard");
        if (chalk.transform.GetChild(0) == null)
        {
            print("Something");
        }
        chalk.transform.GetChild(0).gameObject.GetComponent<Text>().text = boardTitle;
        chalk.transform.GetChild(1).gameObject.GetComponent<Text>().text = boardText;

        GameObject quiz = GameObject.Find("Quiz");
        quiz.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = question;
        quiz.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = answer1;
        quiz.transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>().text = answer2;
        quiz.transform.GetChild(3).GetComponentInChildren<TextMeshProUGUI>().text = answer3;
        quiz.transform.GetChild(4).GetComponentInChildren<TextMeshProUGUI>().text = answer4;
    }
}
