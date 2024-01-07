using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDA.DontDestroyOnLoad
{
    public class SkyboxUpdater : MonoBehaviour
    {
        [SerializeField]
        Material material;
        [SerializeField]
        float speed = 1f;

        void Update()
        {
            material.SetFloat("_Rotation", material.GetFloat("_Rotation") + Time.deltaTime * speed);
        }
    }
}