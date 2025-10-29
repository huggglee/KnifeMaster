using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Splines;
using UnityEngine.UI;

public class KnifeSkinItem : MonoBehaviour
{
    private int skinIndex;
    private string skinTag;
    private Button btn;
    public UnityAction onChangeSkin;

    private void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(() => OnClick());
    }
    public void Setup(Sprite icon, int index, string tag)
    {
        skinIndex = index;
        skinTag = tag;
        Transform chil = gameObject.transform.GetChild(0);
        Image img = chil.GetComponent<Image>();
        if (img != null)
            img.sprite = icon;
    }

    private void OnClick()
    {
        PlayerPrefs.SetInt("SelectedKnifeSkin", skinIndex);
        PlayerPrefs.SetString("SelectedKnifeSkin", skinTag);
        KnifeThrower.Instance.ChangeSkin(PlayerPrefs.GetString("SelectedKnifeSkin"));
    }

    public void RegisterOnChangeSkin(UnityAction callback)
    {
        onChangeSkin += callback;
    }
}
