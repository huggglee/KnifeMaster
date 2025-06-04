using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    private Vector3 startPos = new Vector3(15f, 7f, 0f);
    private Vector3 playPos = new Vector3(9f, 5f, 11f);
    private Vector3 targetPos;

    private Vector3 startEuler = new Vector3(0f, -90f, 0f); 
    private Vector3 playEuler = new Vector3(0f, -140f, 0f);    
    private Quaternion startRot;
    private Quaternion playRot;
    private Quaternion targetRot;

    public KnifeThrower knifeThrower;
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

        startRot = Quaternion.Euler(startEuler);
        playRot = Quaternion.Euler(playEuler);
    }
    void Update()
    {
        if (cam != null)
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, zoomSpeed * Time.deltaTime);
        }

        if (GameManager.Instance.state == GameManager.gameState.onLoad)
        {
            targetPos = startPos;
            targetRot = startRot;
        }
        else if (GameManager.Instance.state == GameManager.gameState.Playing || GameManager.Instance.state == GameManager.gameState.Waiting)
        {
            float currentHeight = knifeThrower.GetCurrentHeight();
            targetPos = playPos+ new Vector3(0f, currentHeight,0f);
            targetRot = playRot;
        }
        transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, followSpeed * Time.deltaTime);
    }

    public void ZoomIn()
    {
        targetFOV = minFOV;
    }

    public void ZoomOut()
    {
        targetFOV = maxFOV;
        Invoke("ZoomIn", 2f);
    }
}
