using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace GDA.DontDestroyOnLoad
{
    [CreateAssetMenu(fileName = "newMission", menuName = "Missions/Mission")]
    public class Mission : ScriptableObject, IMission
    {
        [SerializeField] new string name = "defaultMission";
        [SerializeField] List<string> _tasks;
        [SerializeField] string missionCompletedTask;

        List<string> tasks = new List<string>();

        public string getName => name;
        public string getFirstTask => tasks[0];
        public bool hasTasks => tasks.Count > 0;
        public string getMissionCompletedTask => missionCompletedTask;

        public void Initialize()
        {
            tasks = new List<string>();
            foreach (string task in _tasks)
                tasks.Add(task);
        }

        public bool ContainsTask(string taskName)
        {
            return tasks.Contains(taskName);
        }

        public void RemoveFirst()
        {
            tasks.RemoveAt(0);
        }

        public void Remove(string name)
        {
            tasks.Remove(name);
        }

        public string Save()
        {
            List<string> mission = new List<string>() { name };

            foreach (string task in tasks)
                mission.Add(task);

            return string.Join('|', mission);
        }

        public void Load(string[] mission)
        {
            tasks = new List<string>();
            mission = mission.Skip(1).ToArray();
            
            foreach (string task in mission)
                tasks.Add(task);
        }
    }
}