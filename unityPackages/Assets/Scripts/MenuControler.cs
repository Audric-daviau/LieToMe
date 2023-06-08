using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControler : MonoBehaviour
{
    public void playGame(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }

    public void SettingsScene(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }

    public void exitGame()
    {
        Application.Quit();
    }
}