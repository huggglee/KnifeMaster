using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class KnifeThrower : MonoBehaviour
{
    public static KnifeThrower Instance;

    public GameObject knifePrefab;
    public GameObject ball;
    public Transform tower;
    public Knife LatestKnife;
    public float verticalStep = 1f;
    public float throwForce = 10f;
    public GameObject knife;

    private float _currentHeight;
    public GameObject _currentKnife;
    private bool isLoading = false;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        _currentHeight = knife.transform.position.y;
    }
    void Update()
    {
        //Debug.Log(_currentHeight);
        if (_currentKnife == null)
        {
            _currentHeight += verticalStep;
            Vector3 spawnPosition = new Vector3(tower.position.x, _currentHeight, 4);
            //_currentKnife = Instantiate(knifePrefab, spawnPosition, Quaternion.identity);
            _currentKnife = SpawnKnife(spawnPosition, Quaternion.Euler(0f, 0f, 0f));
        }
        if (GameManager.Instance.state == GameManager.gameState.Playing && isLoading == false)
        {
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                _currentKnife.GetComponent<Knife>().Throw();
                LatestKnife = _currentKnife.GetComponent<Knife>();
                _currentKnife = null;
            }
        }
    }

    public void SetCurrentHeight()
    {
        _currentHeight -= verticalStep;
    }
    public void ResetHeight()
    {
        _currentHeight = 0f;
    }
    public float GetCurrentHeight()
    {
        return _currentHeight;
    }

    public GameObject SpawnKnife(Vector3 position, Quaternion rotation, string tag = "Knife")
    {
        return ObjectPooler.instance.SpawnFromPool(tag, position, rotation);
    }

    public void UndoKnives()
    {
        StartCoroutine(UndoKnivesCoroutine(3));
    }

    private IEnumerator UndoKnivesCoroutine(int numberOfKnives)
    {
        isLoading = true;
        _currentKnife.SetActive(false);
        _currentHeight += verticalStep;
        //isLoading = true;
        yield return new WaitForSeconds(0.5f);
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
        Vector3 BallPosition = new Vector3(0f, _currentHeight + 5f, 2f);
        GameManager.Instance.StartCoroutine(GameManager.Instance.SetState(GameManager.gameState.Playing, 0.2f));
        ball.GetComponent<BallController>().Respawn(BallPosition);
        isLoading = false;
    }
}
