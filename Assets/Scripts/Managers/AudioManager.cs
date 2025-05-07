using TMPro;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioSource _UIAudioSource;
        
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(this);
    }

    public void PlayUIClip(AudioClip audioClip)
    {
        _UIAudioSource.PlayOneShot(audioClip);
    }
}
