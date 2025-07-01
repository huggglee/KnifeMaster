using UnityEngine;

public class Finish : MonoBehaviour
{
    public Transform tower;
    public void Spawn(float y)
    {
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Knife"))
        {
            //GameManager.Instance.StartCoroutine(GameManager.Instance.SetState(GameManager.gameState.Win, 0f));
            //GameManager.Instance.SetState(GameManager.gameState.Win);
            KnifeThrower.Instance._currentKnife.GetComponent<Knife>().ThrowToTarget();
            //collision.gameObject.GetComponent<Knife>().threw = true;
            collision.gameObject.transform.SetParent(tower.transform);
        }
    }
}
