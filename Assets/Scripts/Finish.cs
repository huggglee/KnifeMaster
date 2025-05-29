using UnityEngine;

public class Finish : MonoBehaviour
{
    public Transform tower;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //transform.position = new Vector3(transform.position.x, tower.transform.localScale.y, transform.position.z);
    }   

    void Update()
    {
        
    }
    
    public void Spawn(float y)
    {
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }
}
