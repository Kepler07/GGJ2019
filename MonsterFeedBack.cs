
using System.Collections;
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

    public void sizeUp(float timeToWait)
    {
        transform.localScale += new Vector3(0.5f,0.5f,0.5f);
        StartCoroutine(sizeDown(timeToWait));
    }

    IEnumerator sizeDown(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
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