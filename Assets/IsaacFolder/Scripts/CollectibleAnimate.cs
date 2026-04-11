using UnityEngine;

public class Rotator : MonoBehaviour
{
    void Update() // Basic Z axis spin
    {
        transform.Rotate(new Vector3(0, 0, 30) * Time.deltaTime);
    }
}
