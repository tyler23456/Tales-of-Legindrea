using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDA.Interactables
{
    public class LightToggle : MonoBehaviour , IActivator
    {
        [SerializeField] Material material;
        [SerializeField] List<AudioSource> audioSources;

        float targetVolume = 1f;
        float targetPitch = 1f;

        float duration = 3f;
        float startTime = 0f;

        public void Activate()
        {
            material.SetColor("_EmissionColor", new Color(1f, 1f, 1f));
            RenderSettings.ambientGroundColor = new Color(1f, 1f, 1f);
            RenderSettings.ambientEquatorColor = new Color(0.5f, 0.5f, 0.5f);
            RenderSettings.ambientSkyColor = new Color(0.2f, 0.2f, 0.2f);
            targetVolume = 1f;
            targetPitch = 1f;
            startTime = Time.time;
        }

        public void Deactivate()
        {
            material.SetColor("_EmissionColor", new Color(0f, 0f, 0f));
            RenderSettings.ambientGroundColor = new Color(1f, 1f, 1f) / 3f;
            RenderSettings.ambientEquatorColor = new Color(0.5f, 0.5f, 0.5f) / 3f;
            RenderSettings.ambientSkyColor = new Color(0.2f, 0.2f, 0.2f) / 3f;
            targetVolume = 0f;
            targetPitch = 0f;
            startTime = Time.time;
        }

        void Update()
        {
            if (Time.time - startTime > duration)
                return;

            foreach (AudioSource audioSource in audioSources)
            {
                audioSource.volume = Mathf.Lerp(audioSource.volume, targetVolume, 0.01f);
                audioSource.pitch = Mathf.Lerp(audioSource.pitch, targetPitch, 0.01f);
            }
        }
    }
}