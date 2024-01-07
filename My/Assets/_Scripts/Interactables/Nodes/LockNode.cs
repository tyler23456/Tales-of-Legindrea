using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDA.Interactables
{
    public class LockNode : DecoratorNode
    {
        [SerializeField] bool usesComponent = false;
        [SerializeField] string missionName = "Default";
        [SerializeField] string taskName = "Default";
        [SerializeField] string maximumTaskName = "";

        protected override void OnStart() 
        {
            if (usesComponent)
            {
                missionName = board.userLock.missionName;
                taskName = board.userLock.taskName;
                maximumTaskName = board.userLock.maximumTaskName;
            }

            state = State.Running;
            IMission mission = Blackboard.factory.missions[missionName];
            if (maximumTaskName != "" && mission.hasTasks && !mission.ContainsTask(maximumTaskName))
                state = State.Failure;
            else if (taskName == "" && !mission.hasTasks || taskName != "" && mission.hasTasks && !mission.ContainsTask(taskName) || taskName != "" && !mission.hasTasks)
                state = State.Running;
            else
                state = State.Failure;
        }
        
        protected override State OnUpdate()
        {
            if (state == State.Failure || state == State.Success)
                return state;

            return children[0].Update();

        }

        protected override void OnStop() { }
    }
}
