using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UI;

public class KnifeSkinManager : MonoBehaviour
{
    public GameObject knifeItemPrefab;
    public Transform contentParent;

    private KnifeSkinData[] knifeDatas;
    private string path = "Sprites/KnifeSkins";
    void Awake()
    {
        knifeDatas = Resources.LoadAll<KnifeSkinData>(path);
    }

    void Start()
    {
        LoadItems();
    }

    void LoadItems()
    {

        foreach (KnifeSkinData knifeSkinData in knifeDatas)
        {
            GameObject item = Instantiate(knifeItemPrefab, contentParent);
            item.GetComponent<KnifeSkinItem>().Setup(knifeSkinData.sprite, knifeSkinData.skinId,knifeSkinData.tag); 
        }
    }
}
