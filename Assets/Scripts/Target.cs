using UnityEngine;

public class Target : MonoBehaviour
{
    public void Spawn(float y)
    {
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }
}
