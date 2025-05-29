using System;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private Material material;
    private void Start()
    {
    }   

    public void Spawn(float height)
    {
        material = GetComponent<Renderer>().material;
        gameObject.transform.localScale = new Vector3(transform.localScale.x,height,transform.localScale.z);
        gameObject.transform.position = new Vector3(transform.position.x, transform.localScale.y / 2 - 0.5f, transform.position.z);
        Vector2 tiling = new Vector2(1f, gameObject.transform.localScale.y / 12);
        material.mainTextureScale = tiling;
    }
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
