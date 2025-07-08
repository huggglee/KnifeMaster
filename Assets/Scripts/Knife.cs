using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Knife : MonoBehaviour
{
    private float throwForce = 28f;
    private float forceBoost = 5f;
    private float timeBoost = 0.5f;
    public bool threw = false;
    public bool isBoost = true;

    private Rigidbody rb;
    private GameObject[] knifes;

    private void Start()
    {
        int levelKnifeSpeed = Data.Instance.GetLevelBoost("K_Knife_Speed");
        int levelEasyFever = Data.Instance.GetLevelBoost("K_Easy_Fever");
        int levelFeverBounce = Data.Instance.GetLevelBoost("K_Fever_Bounce");
        throwForce += levelKnifeSpeed * 0.1f;
        timeBoost += levelEasyFever * 0.1f;
        forceBoost += levelFeverBounce * 0.1f;
        rb = GetComponent<Rigidbody>();
        rb.AddTorque(Vector3.forward * 4f);

    }
    public void Throw()
    {
        KnifeThrower.Instance.isLoading = true;
        rb.constraints |= RigidbodyConstraints.FreezeRotation;
        transform.rotation = Quaternion.Euler(90f, -90f, 0f);
        rb.linearVelocity = Vector3.back * throwForce;
    }
    public void Undo(UnityAction callback)
    {
        //Debug.Log("Undo");
        KnifeThrower.Instance.SetCurrentHeight(-KnifeThrower.Instance.verticalStep);
        transform.DOMoveZ(7f, 0.5f)
            //.SetEase(Ease.InCubic)
            .OnComplete(() =>
            {
                ReturnPool();
                callback?.Invoke();
            });
        //rb.AddForce(new Vector3(0, 0, 10f), ForceMode.Impulse);
        //StartCoroutine(ReturnToPool(1.5f));   
    }

    public void UndoNoForce()
    {
        KnifeThrower.Instance.SetCurrentHeight(-KnifeThrower.Instance.verticalStep);
        ReturnPool();
        //StartCoroutine(ReturnToPool(0f));
        //this.threw = false;
    }


    public void ReturnPool()
    {
        gameObject.transform.SetParent(ObjectPooler.Instance.transform);
        ObjectPooler.Instance.ReturnToPool(gameObject);
        rb.linearVelocity = Vector3.zero;
    }

    private void SetBoost()
    {
        isBoost = false;
    }
    public float GetForceBoost()
    {
        return forceBoost;
    }

    public void ThrowToTarget(Transform target)
    {
        rb.constraints |= RigidbodyConstraints.FreezeRotation;
        transform.rotation = Quaternion.Euler(90f, -90f, 0f);
        Sequence seq = DOTween.Sequence();
        seq.Append(rb.DOMoveZ(transform.position.z + 0.1f, 0.1f).SetEase(Ease.InOutCubic));
        seq.Append(rb.DOMoveZ(transform.position.z, 0.2f));
        seq.Append(rb.DOMoveZ(target.position.z + 2.2f, 0.3f).SetEase(Ease.InQuad));
        seq.OnComplete(() =>
        {
            target.GetComponent<Target>().PlayEffect();
            GameManager.Instance.SetState(GameManager.gameState.Win);
        });
    }

    public void Reset()
    {
        this.threw = false;
        this.isBoost = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Tower"))
        {
            Invoke("SetBoost", timeBoost);
        }
    }
}
