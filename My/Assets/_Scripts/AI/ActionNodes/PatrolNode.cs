using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDA.Player
{
    public class PatrolNode : WaitNode
    {
        [SerializeField] float speed = 5f;
        [SerializeField] float radius = 3f;
        [SerializeField] float distanceThreshold = 1f;

        List<Vector3> waypoints = new List<Vector3>();

        int index;
        Vector3 origin;
        bool isFirstStart = true;

        Vector3 targetPosition = Vector3.zero;

        protected override void OnStart()
        {
            base.OnStart();

            if (board.animator == null)
                board.agent.speed = speed;

            if (!isFirstStart)
                return;

            isFirstStart = false;
            index = 0;
            origin = board.agent.transform.position;

            board.speed = 1f;

            waypoints.Add(new Vector3(radius, 0f, 0f));
            waypoints.Add(new Vector3(-radius, 0f, 0f));
            waypoints.Add(new Vector3(0f, 0f, radius));
            waypoints.Add(new Vector3(0f, 0f, -radius));

            targetPosition = origin + waypoints[index];
            board.agent.SetDestination(targetPosition);
            board.agent.updatePosition = true;
            board.agent.updateRotation = true;
        }

        protected override State OnUpdate()
        {
            state = base.OnUpdate();

            float remainingDistance = Vector3.Distance(targetPosition, board.gameObject.transform.position);
            
            if (remainingDistance < distanceThreshold)
            {
                index++;
                index = index % waypoints.Count;
                targetPosition = origin + waypoints[index];
                board.agent.SetDestination(targetPosition);               
            }

            board.user.getRotation.Forward(board.agent.desiredVelocity);
            if (board.animator == null)
                board.user.getPosition.Add(board.agent.desiredVelocity);
            else
                board.user.getAnimations.AddVelocityAndSetSpeed(board.agent.desiredVelocity * 1f, 1f);
           
            return state;
        }

        protected override void OnStop() 
        {
            base.OnStop();
        }
    }
}