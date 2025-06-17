using UnityEngine;

[CreateAssetMenu(fileName = "Level X", menuName = "LevelData")]
public class LevelData : ScriptableObject
{
    public int level;
    public int towerHeight;
    //public int obstacle;
    public Vector3[] spawnPosObstacle;
    public float timer;
}

