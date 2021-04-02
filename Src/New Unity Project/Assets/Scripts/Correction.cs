using TMPro;
using UnityEngine;

public class Correction : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI correct, student;

    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = "You got" + (Corrections() / correct.textInfo.lineCount);
    }

    int Corrections()
    {
        int counter = 0;
        int c = 0;
        do
        {
            if (correct.textInfo.linkInfo[counter].Equals(student.textInfo.linkInfo[counter]))
            {
                c++;
            }
            counter++;
        } while (correct.textInfo.linkCount >= counter-1);

        return c;
    }
}
