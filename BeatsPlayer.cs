using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BeatsPlayer : MonoBehaviour
{
    public AudioClip beat;
    public float volumeScale = 1f;
    
    private AudioSource audioSource;
    private IBeatsPlayerCallback _callback;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void InvokePlaySound(float beatrate, IBeatsPlayerCallback callback)
    {
        _callback = callback;
        InvokeRepeating("playSound", 0f, beatrate);
    }

    public void StopRepeating()
    {
        _callback = null;
        CancelInvoke("playSound");
    }
    
    private void playSound()
    {
        if (audioSource != null && beat != null)
        {
            audioSource.PlayOneShot(beat, volumeScale);
            if (_callback != null)
            {
                _callback.beatsPlayed();
            }
        }
    }
    
}