using System;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private Material material;
    private void Start()
    {
        material = GetComponent<Renderer>().material;
        gameObject.transform.position = new Vector3(transform.position.x, transform.localScale.y/2-0.5f, transform.position.z);
        Vector2 tiling = new Vector2(1f, gameObject.transform.localScale.y / 12);
        material.mainTextureScale = tiling;
    }   


    public void ReSpawn()
    {

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
