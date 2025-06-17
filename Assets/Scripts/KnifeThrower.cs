using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
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
    public GameObject _currentKnife;
    public UnityAction OnChangeHeight;

    private float _currentHeight;
    private bool isLoading = false;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        _currentHeight = knife.transform.position.y;
    }
    private void Start()
    {
        GameManager.Instance.RegisterOnWin(OnWin);
    }
    void Update()
    {
        //Debug.Log(_currentHeight);
        if (_currentKnife == null)
        {
            SetCurrentHeight(verticalStep);
            //_currentHeight += verticalStep;
            Vector3 spawnPosition = new Vector3(tower.position.x, _currentHeight, 4);
            //_currentKnife = Instantiate(knifePrefab, spawnPosition, Quaternion.identity);
            _currentKnife = SpawnKnife(spawnPosition, Quaternion.Euler(0f, 0f, 0f));
        }
        if (GameManager.Instance.state == GameManager.gameState.Playing && isLoading == false)
        {
            if (_currentKnife.transform.position.y <= tower.transform.localScale.y)
            {

                if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
                {
                    if (_currentKnife.transform.position.y == tower.transform.localScale.y)
                    {
                        GameObject ball = GameObject.FindGameObjectWithTag("Ball");
                        ball.SetActive(false);
                    }
                    _currentKnife.GetComponent<Knife>().Throw();
                    SoundManager.Instance.PlaySound2D("Throw");
                    LatestKnife = _currentKnife.GetComponent<Knife>();
                    _currentKnife = null;
                }
            }
        }
    }

    public void OnWin()
    {
        _currentKnife.GetComponent<Knife>().ThrowToTarget();
        LatestKnife = _currentKnife.GetComponent<Knife>();
    }

    public void SetCurrentHeight(float step)
    {
        _currentHeight += step;
        //OnChangeHeight?.Invoke();
    }

    public float GetCurrentHeight()
    {
        return _currentHeight;
    }

    public void RegisterOnChangeHeight(UnityAction callback)
    {
        OnChangeHeight += callback;
    }

    public GameObject SpawnKnife(Vector3 position, Quaternion rotation, string tag = "Knife")
    {
        return ObjectPooler.Instance.SpawnFromPool(tag, position, rotation);
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
