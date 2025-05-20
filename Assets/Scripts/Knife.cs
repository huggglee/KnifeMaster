using System;
using System.Collections;
using UnityEngine;

public class Knife : MonoBehaviour
{
    public float throwForce = 10f;
    public float ForceBoost = 5f;
    public bool threw = false;
    public bool isBoost = true;
    public float timeBoost = 0.5f;

    private Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddTorque(Vector3.forward * 3f);
    }
    public void Throw()
    {
        rb.constraints |= RigidbodyConstraints.FreezeRotation;
        transform.rotation= Quaternion.Euler(Vector3.zero);
        rb.linearVelocity = Vector3.back * throwForce;
        Invoke("setBoost", timeBoost);
    }
    public void Undo()
    {
        rb.AddForce(new Vector3(0, 0, 10f), ForceMode.Impulse);
        //rb.constraints &= ~RigidbodyConstraints.FreezePositionY;
        //rb.useGravity = true;
        KnifeThrower.instance.setCurrentHeight();
        Debug.Log("undo" + gameObject.activeInHierarchy);
        //Destroy(gameObject, 1.5f);
        //ObjectPooler.instance.ReturnToPool(gameObject);
        StartCoroutine("ReturnToPool");
    }


    IEnumerator ReturnToPool()
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.transform.SetParent(ObjectPooler.instance.transform);
        yield return null;
        ObjectPooler.instance.ReturnToPool(gameObject);
    }
    private void setBoost()
    {
        isBoost = false;
    }
    
}
