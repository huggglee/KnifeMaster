using DG.Tweening;
using System;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public Texture endTexture;
    public Texture startTexture;
    private Tower tower;
    private Rigidbody rb;
    private Tween move;
    private Material material;

    void Awake() 
    {
        tower = GameObject.FindGameObjectWithTag("Tower").GetComponent<Tower>();
        material = GetComponent<Renderer>().material;
        rb = GetComponent<Rigidbody>();
        tower.RegisterOnCollision(Kill);
    }

    public void Init()
    {
        material.mainTexture = startTexture;
        if (move != null)
        {
            move.Kill();
        }   
        move = transform.DOMoveX(-transform.position.x, 1f)
                 .SetLoops(-1, LoopType.Yoyo)
                 .SetLink(gameObject)
                 .SetEase(Ease.InOutQuad);
    }

    public void ReturnToPool()
    {
        move.Kill();
        ObjectPooler.Instance.ReturnToPool(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Knife"))
        {
            //KnifeThrower.Instance.hasCollidedWithObstacle = true;
            KnifeThrower.Instance.UndoKnives(3);
            gameObject.SetActive(false);
        }
    }

    private void Kill()
    {
        float currentHeight = KnifeThrower.Instance.GetCurrentHeight();
        if (transform.position.y < currentHeight)
        {
            material.mainTexture = endTexture;
            if (move != null)
            {
                move.Kill();
                move = null;
            }
        }
    }
}
