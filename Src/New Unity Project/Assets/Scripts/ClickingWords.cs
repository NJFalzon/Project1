﻿using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClickingWords : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    List<Interactables> link;

    [SerializeField] Texture2D pointer;
    [SerializeField] Texture2D cursor;
    CursorMode cm = CursorMode.Auto;

    private void Start()
    {
        link = new List<Interactables>();
        link.Add(new Interactables("Lorem Ipsum Sit Amet", "Generic Words to show a full looking text", "Is this pretty?", "Yes", "No", "Maybe", "Trash"));
        link.Add(new Interactables("Fusce dapibus ligula justo", "More text because testing", "Is not this pretty?", "Stun", "No", "Gag", "Fun"));
    }

    private void Update()
    {
        int click = TMP_TextUtilities.FindIntersectingLink(text, Input.mousePosition, null);
        if (click != -1)
        {
            Cursor.SetCursor(pointer, Vector2.zero, cm);
            if (Input.GetMouseButtonDown(0))
            {
                transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = link[click].GetTitle();
                transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = link[click].GetText();
                transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = link[click].GetQuestion();
                transform.GetChild(3).GetComponentInChildren<TextMeshProUGUI>().text = link[click].GetAnswer1();
                transform.GetChild(4).GetComponentInChildren<TextMeshProUGUI>().text = link[click].GetAnswer2();
                transform.GetChild(5).GetComponentInChildren<TextMeshProUGUI>().text = link[click].GetAnswer3();
                transform.GetChild(6).GetComponentInChildren<TextMeshProUGUI>().text = link[click].GetAnswer4();
            }
        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, cm);
        }
    }
}