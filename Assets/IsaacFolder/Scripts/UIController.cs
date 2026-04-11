using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject helpUI;
    public SpiderController spider; // connect to spider controller Reset

    public void ToggleHelp()
    {
        helpUI.SetActive(!helpUI.activeSelf);
    }
    public void HideHelp()
    {
        helpUI.SetActive(false);
    }

    public void Reset() // Reset button
    {
        spider.Reset();
    }
    public void QuitGame()
    {
        SceneManager.LoadScene("MainMenuScreen"); //double check scene name
    }

}
