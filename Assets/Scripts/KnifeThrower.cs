using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class KnifeThrower : MonoBehaviour
{
    public static KnifeThrower Instance;

    //public GameObject knifePrefab;
    public GameObject ball;
    public Transform tower;
    public Knife LatestKnife;
    public float verticalStep = 1f;
    public float throwForce = 10f;
    public GameObject knife;
    public GameObject _currentKnife;
    public UnityAction OnChangeHeight;
    public bool isLoading = false;
    public bool hasCollidedWithObstacle = false;
    public bool isUndo = false;

    private Coroutine undoCoroutine;
    private Queue<Knife> knivesToUndo = new Queue<Knife>();
    private float _currentHeight;
    private bool hasReleasedTouchAfterStart = false;

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
        //GameManager.Instance.RegisterOnWin(OnWin);
    }
    void Update()
    {
        //Debug.Log(isLoading);
        //Debug.Log(_currentHeight);
        if (_currentKnife == null)
        {
            SetCurrentHeight(verticalStep);
            Vector3 spawnPosition = new Vector3(tower.position.x, _currentHeight, 6);
            _currentKnife = SpawnKnife(spawnPosition, Quaternion.Euler(90f, -90f, 0f));
        }
        if (GameManager.Instance.state == GameManager.gameState.Playing && !isLoading && !hasCollidedWithObstacle && !isUndo)
        {
            if (!hasReleasedTouchAfterStart)
            {
                if (Input.touchCount == 0)
                {
                    hasReleasedTouchAfterStart = true;
                }
                return;
            }
            if (_currentKnife.transform.position.y <= tower.transform.localScale.y)
            {

                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);
                    if (touch.phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject())
                    {
                        if (_currentKnife.transform.position.y == tower.transform.localScale.y)
                        {
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
    }

    public void OnGameStart()
    {
        hasReleasedTouchAfterStart = false;
    }

    public void ResetState()
    {
        if (undoCoroutine != null)
        {
            StopCoroutine(undoCoroutine);
            undoCoroutine = null;
        }
        isLoading = false;
        hasCollidedWithObstacle = false;
        isUndo = false;
        knivesToUndo.Clear();
    }
    //public void OnWin()
    //{
    //    _currentKnife.GetComponent<Knife>().ThrowToTarget();
    //    //LatestKnife = _currentKnife.GetComponent<Knife>();
    //}

    public void SetCurrentHeight(float step)
    {
        _currentHeight += step;
        //OnChangeHeight?.Invoke();
    }

    public float GetCurrentHeight()
    {
        return _currentHeight;
    }

    public void ResetCurrentHeight()
    {
        knivesToUndo.Clear();
        _currentHeight = 0;
    }

    public void RegisterOnChangeHeight(UnityAction callback)
    {
        OnChangeHeight += callback;
    }

    public GameObject SpawnKnife(Vector3 position, Quaternion rotation, string tag = "Knife")
    {
        GameObject gameObject = ObjectPooler.Instance.SpawnFromPool(tag, position, rotation);
        gameObject.GetComponent<Knife>().Reset();
        return gameObject;
    }
    public void UndoKnives(int numberOfKnives)
    {
        if (undoCoroutine != null)
        {
            StopCoroutine(undoCoroutine);
            undoCoroutine = null;
        }
        undoCoroutine = StartCoroutine(UndoKnivesCoroutine(3));
    }
    private IEnumerator UndoKnivesCoroutine(int numberOfKnives)
    {
        knivesToUndo.Clear();
        //if (!isUndo)
        //{
            yield return new WaitForSeconds(0.5f);
            if (_currentKnife != null)
                _currentKnife.SetActive(false);
            int count = tower.childCount;
            int startIndex = Mathf.Max(0, count - numberOfKnives);

            for (int i = count - 1; i >= startIndex; i--)
            {
                Transform child = tower.GetChild(i);
                Knife knife = child.GetComponent<Knife>();
                if (knife != null)
                    knivesToUndo.Enqueue(knife);
            }
            UndoNextKnife();
        //}
    }
    private void UndoNextKnife()
    {
        if (knivesToUndo.Count > 0)
        {
            Knife current = knivesToUndo.Dequeue();
            current.Undo(UndoNextKnife);
        }
        else
        {
            FinishUndoSequence();
        }
    }

    private void FinishUndoSequence()
    {
        if (_currentKnife != null)
        {
            _currentKnife.transform.position = new Vector3(
                _currentKnife.transform.position.x,
                _currentHeight,
                _currentKnife.transform.position.z
            );
            _currentKnife.SetActive(true);
        }

        if (ball.transform.parent)
        {
            Vector3 BallPosition = new Vector3(0f, _currentHeight + 5f, 2f);
            //GameManager.Instance.StartCoroutine(GameManager.Instance.SetState(GameManager.gameState.Playing, 0.2f));
            ball.GetComponent<BallController>().Respawn(BallPosition);
        }
        //GameManager.Instance.SetState(GameManager.gameState.Playing);
        ResetState();
    }
}
