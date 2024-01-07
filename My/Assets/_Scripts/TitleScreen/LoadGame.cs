using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GDA.TitleScreen
{
    public class LoadGame : MonoBehaviour
    {
        public void NewGame()
        {
            SceneManager.LoadSceneAsync(1);
        }

        public void Load()
        {
            
        }

        public void Exit()
        {
            
        }
    }
}