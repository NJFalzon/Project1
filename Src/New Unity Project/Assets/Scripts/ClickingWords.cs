using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClickingWords : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    List<Interactables> link;

    [SerializeField] Texture2D pointer;
    readonly CursorMode cm = CursorMode.Auto;

    int click = -1;

    private void Start()
    {
        link = new List<Interactables>();
        link.Add(new Interactables("Lorem Ipsum Sit Amet", "Generic Words to show a full looking text", false, false, false, false));
        link.Add(new Interactables("Fusce dapibus ligula justo", "More text because testing", false, false, false, false));
    }

    private void Update()
    {
        int hover = TMP_TextUtilities.FindIntersectingLink(text, Input.mousePosition, null);
        if (hover != -1)
        {
            Cursor.SetCursor(pointer, Vector2.zero, cm);
            if (Input.GetMouseButtonDown(0))
            {
                click = hover;
                transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = link[click].GetTitle();
                transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = link[click].GetText();
            }
        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, cm);
        }
    }

    public void Answer1()
    {
        link[click].SetAnswer1(!link[click].GetAnswer1());
        print(link[click].GetAnswer1());
    }

    public void Answer2()
    {
        link[click].SetAnswer2(!link[click].GetAnswer2());
        print(link[click].GetAnswer2());
    }

    public void Answer3()
    {
        link[click].SetAnswer3(!link[click].GetAnswer3());
        print(link[click].GetAnswer3());
    }

    public void Answer4()
    {
        link[click].SetAnswer4(!link[click].GetAnswer4());
        print(link[click].GetAnswer4());
    }
}
