
using UnityEngine;

public class MonsterFeedBack : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    
    private string inputKey = "";

    private void Start()
    {
        _particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    public void setInputKey(string key)
    {
        inputKey = key;
    }

    public string GetInputKey()
    {
        return inputKey;
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