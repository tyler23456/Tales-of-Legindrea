using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

namespace GDA.Interactables
{
    public class SceneLoader : MonoBehaviour, ISceneLoader
    {
        IMenu menu;

        public bool isSceneLoading { get; protected set; } = false;
        public Func<bool> onPredicate { get; set; } = () => false;
        public Action onCompleted { get; set; } = () => { };

        void Start()
        {
            menu = GameObject.Find("/DontDestroyOnLoad").GetComponent<IMenu>();
        }

        public void Load(int sceneID)
        {
            menu.StartCoroutine(LoadSceneAsync(sceneID));
        }

        IEnumerator LoadSceneAsync(int sceneID)
        {
            isSceneLoading = true;
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);
            float progress = 0;

            IPanel loadingScreen = menu.CreatePanel();
            loadingScreen.anchor = menu.topLeftAnchor;
            loadingScreen.position = new Vector2Int(0, 0);
            loadingScreen.size = new Vector2Int(1920, 1080);
            loadingScreen.border = new Color(0f, 0f, 0f, 1f);
            loadingScreen.fill = new Color(0f, 0f, 0f, 1f);
            loadingScreen.text = "loading...";
            loadingScreen.textAlignment = TextAlignmentOptions.Center;

            IProgressBar progressBar = menu.CreateProgressBar();
            progressBar.position = new Vector2Int(360, -700);
            progressBar.size = new Vector2Int(1200, 30);
            progressBar.border = menu.blue;
            progressBar.fill = Color.red;

            loadingScreen.onShow = () => loadingScreen.Draw(progressBar);
            loadingScreen.Show(menu.parent2);

            while (!operation.isDone)
            {
                progress = operation.progress / 0.9f;
                progressBar.value = progress;
                progressBar.Refresh();
                loadingScreen.text = "loading..." + "   " + ((int)(progress * 100f)).ToString() + "%";
                yield return null;
            };

            onCompleted.Invoke();
            loadingScreen.Hide();
            isSceneLoading = false;
        }
    }
}