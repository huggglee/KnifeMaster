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
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Knife"))
        {
            rb.linearVelocity = new Vector3(0, bounceForce, 0);
        }
    }
}
