using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDA.Independent
{
    public class AudioSourceManager : MonoBehaviour
    {
        [SerializeField] AudioSource audioSource;

        void Play()
        {
            audioSource.Play();
        }
    }
}