using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace GDA.Interactables
{
    public class SceneNode : ActionNode
    {
        public static bool isSceneLoading { get; protected set; } = false;

        [SerializeField] protected int sceneID;
        [SerializeField] protected string destinationName;

        protected override void OnStart()
        {
            Blackboard.teleportDestinations.Clear();
            Blackboard.menu.StartCoroutine(LoadSceneAsync(sceneID, destinationName));
        }

        protected override State OnUpdate()
        {
            return State.Success;
        }

        protected override void OnStop() { }

        public static IEnumerator LoadSceneAsync(int sceneID, string destinationName)
        {
            IFactory factory = GameObject.Find("DontDestroyOnLoad").GetComponent<IFactory>();
            IHittable player = factory.getPlayer.GetComponent<IHittable>();
            player.getTeleportation.Dissolve();

            while (player.getTeleportation.isActive)
                yield return null;

            isSceneLoading = true;
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);
            float progress = 0;

            IMenu menu = GameObject.Find("DontDestroyOnLoad").GetComponent<IMenu>();

            IPanel loadingScreen = menu.CreatePanel();
            loadingScreen.anchor = menu.topLeftAnchor;
            loadingScreen.position = new Vector2Int(0, 0);
            loadingScreen.size = new Vector2Int(1920, 1080);
            loadingScreen.border = new Color(0f, 0f, 0f, 1f);
            loadingScreen.fill = new Color(0f, 0f, 0f, 0f);
            loadingScreen.text = "loading...";
            loadingScreen.textAlignment = TextAlignmentOptions.Center;

            IProgressBar progressBar = menu.CreateProgressBar();
            progressBar.position = new Vector2Int(360, -700);
            progressBar.size = new Vector2Int(1200, 30);
            progressBar.border = menu.blue;
            progressBar.fill = Color.red;

            loadingScreen.onShow = () => loadingScreen.Draw(progressBar);
            loadingScreen.Show(menu.parent2);

            while (!operation.isDone || Blackboard.teleportDestinations.Count == 0)
            {
                progress = operation.progress / 0.9f;
                progressBar.value = progress;
                progressBar.Refresh();
                loadingScreen.text = "loading..." + "   " + ((int)(progress * 100f)).ToString() + "%";
                yield return null;
            };

            factory.MovePlayerTo(Blackboard.teleportDestinations[destinationName].position, Blackboard.teleportDestinations[destinationName].forward);
            loadingScreen.Hide();
            isSceneLoading = false;
            player.getTeleportation.Materialize();
        }
    }
}

