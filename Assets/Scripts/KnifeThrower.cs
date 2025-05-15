using UnityEngine;

public class KnifeThrower: MonoBehaviour
{
    public GameObject knifePrefab;
    public Transform tower;
    public float verticalStep = 2f;
    public float throwForce = 10f;
    public Knife LatestKnife;

    private float _currentHeight = 0f;


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _currentHeight += verticalStep;

            Vector3 spawnPosition = new Vector3(tower.position.x, _currentHeight, 2);
            GameObject knife = Instantiate(knifePrefab, spawnPosition, Quaternion.identity);
            knife.GetComponent<Rigidbody>().linearVelocity = Vector3.back * throwForce;
            LatestKnife = knife.GetComponent<Knife>();
        }
    }
}
