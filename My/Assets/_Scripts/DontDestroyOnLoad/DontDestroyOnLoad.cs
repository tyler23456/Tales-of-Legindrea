using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDA.DontDestroyOnLoad
{
    public class DontDestroyOnLoad : MonoBehaviour
    {
        void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}