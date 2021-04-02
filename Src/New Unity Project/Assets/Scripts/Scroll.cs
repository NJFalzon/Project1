using TMPro;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    TextMeshProUGUI text;
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (TMP_TextUtilities.FindIntersectingLine(text, Input.mousePosition, null) > 0)
        {
            if (Input.mouseScrollDelta.y > 0)
            {
                text.pageToDisplay--;
                if (text.pageToDisplay < 1)
                {
                    text.pageToDisplay = 1;
                }
            }

            if (Input.mouseScrollDelta.y < 0)
            {
                text.pageToDisplay++;
                if (text.pageToDisplay > text.textInfo.pageCount)
                {
                    text.pageToDisplay = text.textInfo.pageCount;
                }
            }
        }
    }
}
