using TMPro;
using UnityEngine;

public class StudentStatic : MonoBehaviour
{
    void Awake()
    {
        GetComponent<TextMeshProUGUI>().text = SwitchScene.thesis;
    }
}
