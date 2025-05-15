using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public KnifeThrower knifeThrower;
    public float offsetY = 5f;
    public float followSpeed = 5f;

    void Update()
    {
        if (knifeThrower.LatestKnife == null) return;

        Vector3 targetPos = knifeThrower.LatestKnife.transform.position + new Vector3(0, offsetY, -10);
        Vector3 smoothPos = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);
        if (smoothPos.y > transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, smoothPos.y, transform.position.z);
        }
    }
}
