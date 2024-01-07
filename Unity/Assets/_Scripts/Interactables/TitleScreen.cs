using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;

namespace GDA.Interactables
{
    public class TitleScreen : MonoBehaviour
    {
        GameObject obj;
        IMenu menu;
        IFactory factory;
        string fileName = "SaveData.json";

        void Start()
        {
            obj = GameObject.Find("/DontDestroyOnLoad");
            menu = obj.GetComponent<IMenu>();
            factory = obj.GetComponent<IFactory>();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
        }

        public void NewGame()
        {
            LoadNode.Load("NewGameData.json");
            LoadNode.data.Load(factory);

            Blackboard.teleportDestinations.Clear();
            menu.StartCoroutine(SceneNodeWithoutTeleportation.LoadSceneAsync(2, "HQBeacon"));
            gameObject.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1f;
        }

        public void Load()
        {
            if (!File.Exists(Application.streamingAssetsPath + Path.AltDirectorySeparatorChar + fileName))
                return;

            LoadNode.Load(fileName);
            LoadNode.data.Load(factory);

            Blackboard.teleportDestinations.Clear();
            SceneNodeWithoutTeleportation.targetPosition = new Vector3(LoadNode.data.posX, LoadNode.data.posY, LoadNode.data.posZ);
            SceneNodeWithoutTeleportation.targetForward = new Vector3(LoadNode.data.forX, LoadNode.data.forY, LoadNode.data.forZ);
            menu.StartCoroutine(SceneNodeWithoutTeleportation.LoadSceneAsync(LoadNode.data.sceneID, ""));
            gameObject.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1f;
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
