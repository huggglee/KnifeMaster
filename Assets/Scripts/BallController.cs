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
        if (collision.gameObject.CompareTag("Knife") || collision.gameObject.CompareTag("StaticKnife"))
        {
            Knife knife = collision.gameObject.GetComponent<Knife>();
            if (knife.threw == true)
            {
                if (knife.isBoost)
                {
                    rb.linearVelocity = new Vector3(0, bounceForce + knife.GetForceBoost(), 0);
                    GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
                    cam.GetComponent<CameraFollower>().ZoomOut();
                }
                else
                {
                    rb.linearVelocity = new Vector3(0, bounceForce, 0);
                }
            }
            else
            {
                //GameManager.Instance.StartCoroutine(GameManager.Instance.SetState(GameManager.gameState.Waiting, 0f));
                GameManager.Instance.SetState(GameManager.gameState.Waiting);
                gameObject.transform.localScale = new Vector3(1f, 1f, 0.4f);
                Vector3 size = collision.gameObject.GetComponent<Renderer>().bounds.extents;
                gameObject.GetComponent<Collider>().enabled = false;
                gameObject.transform.position = collision.transform.position - new Vector3(0f, 0f, size.z);
                gameObject.transform.SetParent(collision.transform);
                rb.isKinematic = true;

                KnifeThrower.Instance.UndoKnives(3);
                KnifeThrower.Instance.isUndo = true;
            }
        }
    }

    public void Respawn(Vector3 position)
    {
        gameObject.transform.SetParent(null);
        gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        gameObject.transform.position = position;
        gameObject.GetComponent<Collider>().enabled = true;
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        gameObject.SetActive(true);
    }
}
