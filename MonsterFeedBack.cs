
using UnityEngine;

public class MonsterFeedBack : MonoBehaviour
{
    public string keyString;
    private ParticleSystem _particleSystem;

    private void Start()
    {
        _particleSystem = GetComponentInChildren<ParticleSystem>();
        
    }

    public void sizeUp()
    {
        transform.localScale += new Vector3(0.5f,0.5f,0.5f);
    }

    public void sizeDown()
    {
        transform.localScale -= new Vector3(0.5f,0.5f,0.5f);
    }

    public void pop()
    {
        if (_particleSystem != null)
        {
            _particleSystem.Play();    
        }
    }
}