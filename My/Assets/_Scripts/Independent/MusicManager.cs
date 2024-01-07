using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDA.Independent
{
    public class MusicManager : MonoBehaviour
    {
        [SerializeField] AudioSource audioSource1;
        [SerializeField] AudioSource audioSource2;

        Queue<AudioSource> audioSources; 

        [SerializeField] float repeatPointInSeconds;

        // Start is called before the first frame update
        void Start()
        {
            audioSource1.Play();

            audioSources = new Queue<AudioSource>();
            audioSources.Enqueue(audioSource1);
            audioSources.Enqueue(audioSource2);
        }

        // Update is called once per frame
        void Update()
        {
            if (audioSources.Peek().time >= repeatPointInSeconds)
            {
                audioSources.Enqueue(audioSources.Dequeue());
                audioSources.Peek().Play();
            }
        }
    }
}