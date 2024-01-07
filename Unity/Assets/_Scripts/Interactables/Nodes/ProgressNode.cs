using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

namespace GDA.Interactables
{
    public class ProgressNode : ActionNode
    {
        [SerializeField] bool usesComponent = false;
        [SerializeField] string missionName = "Default";
        [SerializeField] string taskName = "";
        
        protected override void OnStart()
        {
            if (state == State.Success)
                return;

            if (usesComponent)
            {
                missionName = board.progress.missionName;
                taskName = board.progress.taskName;
            }

            IMission mission = Blackboard.factory.missions[missionName];

            if (!mission.ContainsTask(taskName))
                return;

            GameObject obj = GameObject.Find("/DontDestroyOnLoad/Canvas/Display1");
            foreach (Transform t in obj.transform)
                if (t.tag == "Replaceable")
                    Destroy(t.gameObject);

            string completedTask = "";

            if (taskName == "")
            {
                completedTask = mission.getFirstTask;
                mission.RemoveFirst();           
            }         
            else
            {
                completedTask = taskName;
                mission.Remove(taskName);
            }
            
            GameObject notification = GameObject.Instantiate(Blackboard.factory.uIElements["Notification"], Blackboard.menu.parent1);
            notification.tag = "Replaceable";
            TMP_Text title = notification.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
            TMP_Text description = notification.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>();
            GameObject.Destroy(notification, 8f);
            AudioSource audioSource = Blackboard.factory.getAudioSource;
                             
            if (mission.hasTasks)
            {
                title.text = "Task Completed: " + completedTask;
                description.text = "Now " + mission.getFirstTask;
                audioSource.PlayOneShot(Blackboard.factory.generalSFX["TaskComplete"]);                  
            }
            else
            {
                title.text = "Mission Completed: " + missionName;
                description.text = mission.getMissionCompletedTask;
                audioSource.PlayOneShot(Blackboard.factory.generalSFX["MissionComplete"]);
            }          
        }

        protected override State OnUpdate()
        {
            state = State.Success;
            return state;
        }

        protected override void OnStop() { }
    }
}