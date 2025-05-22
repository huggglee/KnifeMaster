using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public KnifeThrower knifeThrower;
    public float offsetY = 5f;
    public float followSpeed = 5f;
    public float zoomSpeed = 3f;
    public float minFOV = 60f;
    public float maxFOV = 100f;
    private float targetFOV;
    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        if (cam != null)
        {
            targetFOV = cam.fieldOfView;
        }
    }
    void Update()
    {
        if (cam != null)
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, zoomSpeed * Time.deltaTime);
        }

        //if (knifeThrower.LatestKnife == null) return;
        float currentHeight = knifeThrower.getCurrentHeight();
        Vector3 targetPos = new Vector3(gameObject.transform.position.x, currentHeight, gameObject.transform.position.z) + new Vector3(0, offsetY, -10);
        Vector3 smoothPos = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, smoothPos.y, transform.position.z);
        if (Input.GetKeyDown(KeyCode.C))
        {
            zoomOut();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            zoomIn();
        }
    }

    public void zoomIn()
    {
        targetFOV = minFOV;
    }

    public void zoomOut()
    {
        targetFOV = maxFOV;
        Invoke("zoomIn", 2f);
    }
}
