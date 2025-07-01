using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager Instance;
    private Dictionary<string, GameObject> screenDatas = new();
    [SerializeField] Transform screenParent;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        //LoadData();
        //activeScreen("Home");
    }

    public void LoadData()
    {
        int childlCount = screenParent.childCount;
        GameObject[] childs = new GameObject[childlCount];
        for (int i = 0; i < childlCount; i++)
        {
            childs[i] = screenParent.GetChild(i).gameObject;
            //Debug.Log("Panel " + i + ": " + childs[i].name);
            ScreenController sc = childs[i].GetComponent<ScreenController>();
            screenDatas[sc.key] = childs[i];
        }
    }

    public void ActiveScreen(string key)
    {
        screenDatas[key].GetComponent<ScreenController>().Active();
    }

    public IEnumerator ActiveScreen(string key, float time)
    {
        yield return new WaitForSeconds(time);
        screenDatas[key].GetComponent<ScreenController>().Active();
    }

    public void InactiveScreen(string key)
    {
        screenDatas[key].GetComponent<ScreenController>().Inactive();
    }
    public IEnumerator InactiveScreen(string key, float time)
    {
        yield return new WaitForSeconds(time);
        screenDatas[key].GetComponent<ScreenController>().Inactive();
    }
}
