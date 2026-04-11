using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadRoseScene()
    {
        SceneManager.LoadScene("RoseScene");
    }
}