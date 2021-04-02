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
        link.Add(new Interactables("Lorem Ipsum Sit Amet", "Generic Words to show a full looking text", new bool[4] { true, false, false, true }));
        link.Add(new Interactables("Fusce dapibus ligula justo", "More text because testing", new bool[4] { false, true, false, true }));
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
        bool[] temp = link[click].GetAnswers();
        temp[0] = !temp[0];
        link[click].SetAnswers(temp);
    }

    public void Answer2()
    {
        bool[] temp = link[click].GetAnswers();
        temp[1] = !temp[1];
        link[click].SetAnswers(temp);
    }

    public void Answer3()
    {
        bool[] temp = link[click].GetAnswers();
        temp[2] = !temp[2];
        link[click].SetAnswers(temp);
    }

    public void Answer4()
    {
        bool[] temp = link[click].GetAnswers();
        temp[3] = !temp[3];
        link[click].SetAnswers(temp);
    }
}
