using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    TMP_TextInfo textInfo;

    string greenMark = "><mark=#00ff0050>";
    string yellowMark = "><mark=#ffff0050>";
    string redMark = "><mark=#ff000050>";

    List<Interactables> interactables = new List<Interactables>();

    void Start()
    {
        textInfo = text.textInfo;
        //interactables.Add(new Interactables("Lorem Ipsum sit Amet", ))
    }
    void Update()
    {
        int click = TMP_TextUtilities.FindIntersectingLink(text, Input.mousePosition, null);
        if (click != -1)
        {
            TMP_LinkInfo linkInfo = textInfo.linkInfo[click];
            string linkid = linkInfo.GetLinkID();
            string temptxt = linkInfo.textComponent.text;


            if (Input.GetMouseButtonDown(0))
            {
                if (temptxt.Contains(linkid + yellowMark))
                {
                    transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = temptxt.Replace(linkid + yellowMark, linkid + greenMark);
                }
                else if (temptxt.Contains(linkid + redMark))
                {
                    transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = temptxt.Replace(linkid + redMark, linkid + greenMark);
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                if (temptxt.Contains(linkid + yellowMark))
                {
                    transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = temptxt.Replace(linkid + yellowMark, linkid + redMark);
                }
                else if (temptxt.Contains(linkid + greenMark))
                {
                    transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = temptxt.Replace(linkid + greenMark, linkid + redMark);
                }
            }
        }
    }
}
