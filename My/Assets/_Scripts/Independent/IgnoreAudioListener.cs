using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreAudioListener : MonoBehaviour
{
    [SerializeField] List<AudioSource> audioSources;

    // Start is called before the first frame update
    void Start()
    {
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.ignoreListenerPause = true;
            audioSource.ignoreListenerVolume = true;
        }        
    }
}
