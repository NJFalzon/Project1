using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public static string thesis;
    public void Switch(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void TakeThesis(TextMeshProUGUI text)
    {
        thesis = text.text.ToString();
        print(thesis);
    }
}
