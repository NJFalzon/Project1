using System.IO;
using TMPro;
using UnityEngine;

public class LoadText : MonoBehaviour
{
    [SerializeField] string path;
    void Start()
    {
        ReadText();
    }

    void ReadText()
    {
        StreamReader reader = new StreamReader(path);
        ApplyText(reader.ReadToEnd());
        reader.Close();
    }

    void ApplyText(string text)
    {
        GetComponent<TextMeshProUGUI>().text = text;
    }
}
