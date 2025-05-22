using System;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float bounceForce = 10f;
    private Rigidbody rb;
    private bool isBoost;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Knife"))
        {
            Knife knife = collision.gameObject.GetComponent<Knife>();
            if (knife.threw == true)
            {
                if (knife.isBoost)
                {
                    rb.linearVelocity = new Vector3(0, bounceForce + knife.ForceBoost, 0);
                    GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
                    cam.GetComponent<CameraFollower>().zoomOut();
                }
                else
                {
                    rb.linearVelocity = new Vector3(0, bounceForce, 0);
                }
            }
            else
            {
                gameObject.transform.localScale = new Vector3(1f, 1f, 0.4f);
                Vector3 size = collision.gameObject.GetComponent<Renderer>().bounds.extents;
                gameObject.GetComponent<Collider>().enabled = false;
                gameObject.transform.position = collision.transform.position - new Vector3(0f, 0f, size.z);
                gameObject.transform.SetParent(collision.transform);
                rb.isKinematic = true;

                KnifeThrower.instance.undoKnives();
                //knife.Undo();
            }
        }
    }

    public void RespawnBall(Vector3 position)
    {
        gameObject.transform.SetParent(null);
        gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        gameObject.transform.position = position;
        gameObject.GetComponent<Collider>().enabled = true;
        rb.isKinematic = false;

    }
}
