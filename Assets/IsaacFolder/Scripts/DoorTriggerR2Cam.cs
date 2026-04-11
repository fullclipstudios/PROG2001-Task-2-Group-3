using UnityEngine;

public class CameraTriggerRoom2 : MonoBehaviour
{
    public Camera mainCamera;
    public Camera room2Camera;

    private void OnTriggerEnter(Collider other) // enter room 2
    {
        if (!other.CompareTag("Player")) return;
        {
        mainCamera.gameObject.SetActive(false);
        room2Camera.gameObject.SetActive(true);
        }
    }
}
