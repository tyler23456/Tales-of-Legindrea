using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDA.Interactables
{
    public class ActiveRange : MonoBehaviour
    {
        static IMission mission;

        [SerializeField] string missionName;
        [SerializeField] string minimumTaskName;
        [SerializeField] string MaximumTaskName;

        bool isActive = true;

        void Start()
        {
            mission = GameObject.Find("/DontDestroyOnLoad").GetComponent<IFactory>().missions[missionName];

            foreach (Transform t in gameObject.transform)
                t.gameObject.SetActive(false);

            isActive = true;
        }

        void Update()
        {
            if (!isActive || minimumTaskName != "" && mission.ContainsTask(minimumTaskName) || MaximumTaskName != "" && !mission.ContainsTask(MaximumTaskName))
                return;

            foreach (Transform t in gameObject.transform)
                t.gameObject.SetActive(true);

            isActive = false;
        }
    }
}