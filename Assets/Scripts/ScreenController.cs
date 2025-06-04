using UnityEngine;

public class ScreenController : MonoBehaviour
{
    public string key;
    public void Active()
    {
        gameObject.SetActive(true);
    }

    public void Inactive()
    {
        //Debug.Log("Inactive");
        gameObject.SetActive(false);
    }
}
