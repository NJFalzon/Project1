using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayCorrection : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tCorrect, tPlayer;
    List<Interactables> link;

    [SerializeField] Texture2D pointer;
    readonly CursorMode cm = CursorMode.Auto;

    int click = -1;
    void Start()
    {
        link = new List<Interactables>();
        link.Add(new Interactables("Lorem Ipsum Sit Amet", "Generic Words to show a full looking text", new bool[4] {true, false, false, true }));
        link.Add(new Interactables("Fusce dapibus ligula justo", "More text because testing", new bool[4] { false, true, false, true }));
    }

    void Update()
    {
        Click();
    }

    void Click()
    {
        int hover1 = TMP_TextUtilities.FindIntersectingLink(tCorrect, Input.mousePosition, null);
        int hover2 = TMP_TextUtilities.FindIntersectingLink(tPlayer, Input.mousePosition, null);
        
        if (hover1 != -1 || hover2 != -1)
        {
            Cursor.SetCursor(pointer, Vector2.zero, cm);
            if (Input.GetMouseButtonDown(0))
            {
                click = hover1 != -1 ? hover1 : hover2;
                print(click);
                transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = link[click].GetTitle();
                transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = link[click].GetText();
            }
        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, cm);
        }
    }
}
