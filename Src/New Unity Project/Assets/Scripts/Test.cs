using TMPro;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI text;
    [SerializeField] TMP_StyleSheet styleSheet;
    TMP_TextInfo textInfo;

    void Start()
    {
        textInfo = text.textInfo;
    }
    void Update()
    {
        int click = TMP_TextUtilities.FindIntersectingLink(text, Input.mousePosition, null);
        if (click != -1)
        {
            TMP_LinkInfo linkInfo = textInfo.linkInfo[click];
            linkInfo.textComponent.textStyle = styleSheet.GetStyle(2558);
        }
    }
}
