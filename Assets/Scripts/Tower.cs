using System;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Knife"))
        {
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            other.gameObject.transform.SetParent(transform);
        }
    }
}
