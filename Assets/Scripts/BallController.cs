using System;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float bounceForce = 10f;
    private Rigidbody rb;

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
                    Debug.Log("Boost");
                    rb.linearVelocity = new Vector3(0, bounceForce + knife.ForceBoost, 0);
                }
                else
                {
                    rb.linearVelocity = new Vector3(0, bounceForce, 0);
                }
            }
            else
            {
                KnifeThrower.instance.undoKnives();
                //knife.Undo();
            }
        }
    }
}
