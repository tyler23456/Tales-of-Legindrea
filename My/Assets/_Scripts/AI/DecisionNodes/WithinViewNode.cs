using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDA.Player
{
    public class WithinViewNode : DecoratorNode
    {
        [SerializeField] float activeDistance = 80f;
        [SerializeField] float fieldOfView = 90f;
        [SerializeField] bool useInnactiveDistance = true;
        [SerializeField] float innactiveDistance = 100f;

        bool active = false;
        
        protected override void OnStart()
        {
            state = State.Running;
        }
        
        protected override State OnUpdate()
        {
            if (WithinLineOfSight())
                active = true;
            else if (!useInnactiveDistance || useInnactiveDistance && Vector3.Distance(board.user.getWeaponAnchors.getVFXPosition, board.target.getStatFXAnchor.getAnchor.position) > innactiveDistance)
                active = false;

            if (active)
            {
                children[0].Update();
                return State.Success;
            }         
            else
                return State.Failure;
        }

        protected bool WithinLineOfSight()
        {
            Vector3 direction = (board.target.getStatFXAnchor.getAnchor.position - board.user.getWeaponAnchors.getVFXPosition).normalized;
            float dotProduct = Vector3.Dot(board.user.getWeaponAnchors.getVFXForward, direction);

            if (dotProduct < Mathf.Cos(fieldOfView))
                return false;

            if (!Physics.Raycast(board.user.getWeaponAnchors.getVFXPosition, direction, out RaycastHit hit, activeDistance) || hit.transform != null && hit.transform.gameObject.name != "Player")
                return false;

            return true;
        }

        protected override void OnStop() { }
    }
}