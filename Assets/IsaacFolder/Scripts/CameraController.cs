using UnityEngine;

public class Room2Camera : MonoBehaviour
{
    public Transform target;

    void Update()
    {
        Vector3 direction = target.position - transform.position;

        transform.rotation = Quaternion.LookRotation(direction);
    }
}
