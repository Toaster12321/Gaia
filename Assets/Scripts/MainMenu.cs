using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void PlayNewGame()
    {
        SceneManager.LoadSceneAsync(1);        
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
