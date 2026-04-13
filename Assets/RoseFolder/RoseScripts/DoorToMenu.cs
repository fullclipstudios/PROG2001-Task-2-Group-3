using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorToMenu : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("MainMenuScene");
        }
    }
}