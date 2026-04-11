using UnityEngine;

public class CameraTriggerMain : MonoBehaviour
{
    public Camera mainCamera;
    public Camera room2Camera;

    private void OnTriggerEnter(Collider other) // enter Main
    {
        if (!other.CompareTag("Player")) return;
        {
        mainCamera.gameObject.SetActive(true);
        room2Camera.gameObject.SetActive(false);
        }
    }
}
