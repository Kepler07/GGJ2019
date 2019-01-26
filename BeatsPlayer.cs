using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BeatsPlayer : MonoBehaviour
{
    public AudioClip beat;
    public float volumeScale = 1f;
    
    private AudioSource audioSource;
    private IBeatsCallback _callback;
    private float timeBeforeValidate = 0.1f;
    private float timeAfterValidate = 0.1f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void InvokePlaySound(float beatrate)
    {
        if (!IsInvoking("playSoundRepeating"))
        {
            InvokeRepeating("playSoundRepeating", 0f, beatrate);    
        }
    }

    public void SetCallback(IBeatsCallback callback, float timeBefore, float timeAfter)
    {
        _callback = callback;
        timeBeforeValidate = timeBefore;
        timeAfterValidate = timeAfter;
    }

    public void RemoveCallback()
    {
        _callback = null;
    }
    
    private void playSoundRepeating()
    {
        beforeSound();
        StartCoroutine("playSoundCoroutine");
    }

    private IEnumerator playSoundCoroutine()
    {
        yield return new WaitForSecondsRealtime(timeBeforeValidate);
        if (audioSource != null && beat != null)
        {
            audioSource.PlayOneShot(beat, volumeScale);
        }
        yield return new WaitForSecondsRealtime(timeAfterValidate);
        afterSound();
    }

    private void beforeSound()
    {
        if (_callback != null)
        {
            _callback.beforeSound();
        }
    }

    private void afterSound()
    {
        if (_callback != null)
        {
            _callback.afterSound();
        }
    }
    
}