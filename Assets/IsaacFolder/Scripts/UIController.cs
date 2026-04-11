using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject helpUI;
    public void ToggleHelp()
    {
        helpUI.SetActive(!helpUI.activeSelf);
    }
    public void QuitGame()
    {
        SceneManager.LoadScene("MainMenuScreen");
    }
}
