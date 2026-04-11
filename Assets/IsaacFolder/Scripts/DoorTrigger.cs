using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    public Camera mainCamera;
    public Camera room2Camera;

    private void OnTriggerEnter(Collider other) // enter room 2
    {
        if (other.CompareTag("Player"))
        {
        mainCamera.gameObject.SetActive(false);
        room2Camera.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other) // leave room 2
    {
        if (other.CompareTag("Player"))
        {
        mainCamera.gameObject.SetActive(false);
        room2Camera.gameObject.SetActive(true);
        }
    }
}
