
using System.Collections;
using UnityEngine;

public class MonsterFeedBack : MonoBehaviour
{
    
    public AudioClip soundToValidate;

    private AudioSource audioSource;
    private ParticleSystem _particleSystem;

    private string inputKey = "";
    private bool _hasAlreadyPopUp = false;

    private void Start()
    {
        _particleSystem = GetComponentInChildren<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
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
        _hasAlreadyPopUp = false;
        transform.localScale -= new Vector3(0.5f,0.5f,0.5f);
    }

    public void pop()
    {
        if (_particleSystem != null && !_hasAlreadyPopUp)
        {
            _hasAlreadyPopUp = true;
            _particleSystem.Play();
            PlaySoundValidation();
        }
    }

    public void PlaySoundValidation()
    {
        audioSource.PlayOneShot(soundToValidate);
    }
}
