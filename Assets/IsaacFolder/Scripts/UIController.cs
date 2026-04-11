using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject helpUI;
    public void ToggleHelp()
    {
        helpUI.SetActive(!helpUI.activeSelf);
    }
    public void HideHelp()
    {
        helpUI.SetActive(false);
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("MainMenuScreen"); //double check scene name
    }
}
