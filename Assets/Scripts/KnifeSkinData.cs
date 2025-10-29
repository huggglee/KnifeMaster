using UnityEngine;

[CreateAssetMenu(fileName = "Knife_X", menuName = "KnifeSkinData")]
public class KnifeSkinData : ScriptableObject
{
    public int skinId;
    public string tag;
    public int cost;
    public Sprite sprite;
}
