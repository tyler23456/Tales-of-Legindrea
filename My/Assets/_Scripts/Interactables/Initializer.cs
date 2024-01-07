using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GDA.Interactables
{
    public class Initializer : MonoBehaviour
    {
        bool isFirstTime = true;

        // Update is called once per frame
        void Update()
        {
            if (!isFirstTime)
                return;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;

            isFirstTime = false;
            GameObject.Find("/DontDestroyOnLoad").GetComponent<ISceneLoader>().Load(1);
        }
    }
}