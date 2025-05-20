using System.Collections;
using UnityEngine;

public class KnifeThrower : MonoBehaviour
{
    public static KnifeThrower instance;

    public GameObject knifePrefab;
    public Transform tower;
    public float verticalStep = 1f;
    public float throwForce = 10f;
    public Knife LatestKnife;

    private float _currentHeight = 0f;
    private GameObject _currentKnife;
    //private bool isLoading = false;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Update()
    {
        if (_currentKnife == null )
        {
            _currentHeight += verticalStep;
            Debug.Log(_currentHeight);
            Vector3 spawnPosition = new Vector3(tower.position.x, _currentHeight, 4);
            //_currentKnife = Instantiate(knifePrefab, spawnPosition, Quaternion.identity);
            _currentKnife = SpawnKnife(spawnPosition, Quaternion.Euler(0f, 0f, 0f));
        }
        if (Input.GetMouseButtonDown(0))
        {
            _currentKnife.GetComponent<Knife>().Throw();
            LatestKnife = _currentKnife.GetComponent<Knife>();
            _currentKnife = null;
        }
    }

    public void setCurrentHeight()
    {
        _currentHeight -= verticalStep;
    }
    public float getCurrentHeight()
    {
        return _currentHeight;
    }

    public GameObject SpawnKnife(Vector3 position,Quaternion rotation, string tag = "Knife")
    {
        return ObjectPooler.instance.SpawnFromPool(tag, position, rotation);
    }

    public void undoKnives()
    {
        StartCoroutine(undoKnivesCoroutine(3));
    }

    private IEnumerator undoKnivesCoroutine(int numberOfKnives)
    {
        _currentKnife.SetActive(false);
        //isLoading = true;
        LatestKnife.Undo();
        yield return new WaitForSeconds(0.4f);

        int count = tower.childCount;
        int startIndex = Mathf.Max(0, count - numberOfKnives);

        for (int i = count - 1; i >= startIndex; i--)
        {
            Transform child = tower.GetChild(i);
            Knife knife = child.GetComponent<Knife>();
            if (knife != null)
            {
                knife.Undo();
                yield return new WaitForSeconds(0.4f);
            }
        }
        yield return new WaitForSeconds(0.4f);
        _currentKnife.transform.position = new Vector3(_currentKnife.transform.position.x, _currentHeight, _currentKnife.transform.position.z);
        _currentKnife.SetActive(true);
        //isLoading = false;
    }
}
