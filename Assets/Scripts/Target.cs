using UnityEngine;

public class Target : MonoBehaviour
{
    public ParticleSystem winEffect;
    public void Spawn(float y)
    {
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
        winEffect.transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }
    public void PlayEffect()
    {
        winEffect.Play();
    }
}
