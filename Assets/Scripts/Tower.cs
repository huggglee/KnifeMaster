using System;
using UnityEngine;

public class Tower : MonoBehaviour
{
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Knife"))
    //    {
    //        //other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
    //        other.gameObject.transform.SetParent(transform);
    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Knife"))
        {
            //collision.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            collision.gameObject.GetComponent<Knife>().threw = true;
            collision.gameObject.transform.SetParent(transform);
        }
    }
}
