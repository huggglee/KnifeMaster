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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Knife"))
        {
            GameManager.Instance.StartCoroutine(GameManager.Instance.SetState(GameManager.gameState.Win, 0f));
            collision.gameObject.GetComponent<Knife>().threw = true;
            collision.gameObject.transform.SetParent(tower.transform);
        }
    }
}
