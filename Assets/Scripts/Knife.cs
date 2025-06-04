using System;
using System.Collections;
using UnityEngine;

public class Knife : MonoBehaviour
{
    public float throwForce = 15f;
    public float ForceBoost = 5f;
    public bool threw = false;
    public bool isBoost = true;
    public float timeBoost = 0.5f;

    private Rigidbody rb;
    private GameObject[] knifes;

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
        Invoke("SetBoost", timeBoost);
    }
    public void Undo()
    {
        rb.AddForce(new Vector3(0, 0, 10f), ForceMode.Impulse);
        KnifeThrower.Instance.SetCurrentHeight();
        //Destroy(gameObject, 1.5f);
        //ObjectPooler.instance.ReturnToPool(gameObject);
        StartCoroutine(ReturnToPool(1.5f));
    }

    public void UndoNoForce()
    {
        KnifeThrower.Instance.SetCurrentHeight();
        StartCoroutine(ReturnToPool(0f));
    }

    IEnumerator ReturnToPool(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.transform.SetParent(ObjectPooler.instance.transform);
        //yield return null;    
        ObjectPooler.instance.ReturnToPool(gameObject);
        rb.linearVelocity = Vector3.zero;
    }
    private void SetBoost()
    {
        isBoost = false;
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Finish"))
    //    {
    //        //knifes = GameObject.FindGameObjectsWithTag("Knife");
    //        //foreach (GameObject knife in knifes)
    //        //{
    //        //    Knife knifeScript = knife.GetComponent<Knife>();
    //        //    knifeScript.UndonoForce();
    //        //}
    //        //ScreenManager.Instance.ActiveScreen("StartPanel");
    //        GameManager.Instance.StartCoroutine(GameManager.Instance.SetState(GameManager.gameState.Win,0.3f));
    //        //LevelManager.Instance.LoadNextLevel();
    //    }
    //}
}
