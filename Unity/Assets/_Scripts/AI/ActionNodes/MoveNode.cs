using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDA.Player
{
    public class MoveNode : WaitNode
    {
        [SerializeField] Vector3 relativeDirection;
        [SerializeField] float speed = 10f;
        [SerializeField] float minimumDistance = 15f;

        protected override void OnStart()
        {
            base.OnStart();
            board.speed = speed;
            board.agent.updatePosition = true;
            board.agent.updateRotation = true;

            if (board.animator == null)
                board.agent.speed = speed;
        }

        protected override State OnUpdate()
        {
            state = base.OnUpdate();
            Vector3 targetPosition = board.target.getStatFXAnchor.getAnchor.position;
            board.agent.SetDestination(targetPosition);

            float remainingDistance = Vector3.Distance(targetPosition, board.user.getStatFXAnchor.getAnchor.position);
            if (remainingDistance < minimumDistance)
            {
                if (board.animator == null) { board.agent.speed = 0f; }
                else {  board.user.getAnimations.AddVelocityAndSetSpeed(board.agent.desiredVelocity * 2f, 0f); }
            }             
            else
            {          
                if (board.animator == null) { board.user.getPosition.Add(board.agent.desiredVelocity * speed); }
                else { board.user.getAnimations.AddVelocityAndSetSpeed(board.agent.desiredVelocity * 2f, 2f); }                  
            }           
            return state;
        }

        protected override void OnStop() 
        {
            base.OnStop();
        }
    }
}