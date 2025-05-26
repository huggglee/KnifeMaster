using UnityEngine;

public class Finish : MonoBehaviour
{
    public Transform tower;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = new Vector3(transform.position.x, tower.transform.localScale.y, transform.position.z);
    }   

    // Update is called once per frame
    void Update()
    {
        
    }
}
