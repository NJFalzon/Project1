using TMPro;
using UnityEngine;

public class FlipPage : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    public void Previous()
    {
        text.pageToDisplay--;
        if (text.pageToDisplay < 1)
        {
            text.pageToDisplay = text.textInfo.pageCount;
        }
    }

    public void Next()
    {
        text.pageToDisplay++;
        if(text.pageToDisplay > text.textInfo.pageCount)
        {
            text.pageToDisplay = 1;
        }
    }
}
