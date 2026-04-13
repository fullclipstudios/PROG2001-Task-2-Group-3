using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class navControl : MonoBehaviour
{
        public void LoadJamesScene()
    {
        SceneManager.LoadScene("JamesScene"); //double check scene name
    }

        public void LoadRoseScene()
    {
        SceneManager.LoadScene("RoseScene"); //double check scene name
    }

        public void LoadIsaacScene()
    {
        SceneManager.LoadScene("IsaacGameScreen"); //double check scene name
    }
}
