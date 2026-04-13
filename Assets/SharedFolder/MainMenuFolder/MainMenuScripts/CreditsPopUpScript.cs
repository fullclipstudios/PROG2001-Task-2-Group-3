using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsPopUp : MonoBehaviour
{
    public GameObject creditsUI;

    public void ShowHelp()
    {
        creditsUI.SetActive(true);
    }
    public void HideHelp()
    {
        creditsUI.SetActive(false);
    }
}
