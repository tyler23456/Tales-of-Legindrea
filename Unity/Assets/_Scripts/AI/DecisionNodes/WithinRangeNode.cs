using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDA.Player
{
    public class WithinRangeNode : DecoratorNode
    {
        [SerializeField] float activeDistance = 15f;
        [SerializeField] float innactiveDistance = 20f;

        bool isActive = false;
        bool hasAlerted = false;

        protected override void OnStart()
        {
            state = State.Running;
        }

        protected override State OnUpdate()
        {
            if (board.user.getStatFXAnchor.CheckSphere(activeDistance, ~LayerMask.NameToLayer("Player")))
            {
                isActive = true;
            }
            else if (!board.user.getStatFXAnchor.CheckSphere(innactiveDistance, ~LayerMask.NameToLayer("Player")))
            {
                hasAlerted = false;
                isActive = false;
            }

            if (isActive && !hasAlerted)
            {
                hasAlerted = true;
                RectTransform rect = GameObject.Instantiate(Blackboard.factory.uIElements["Alert"], GameObject.Find("DontDestroyOnLoad/Canvas/Display1").transform).GetComponent<RectTransform>();
                rect.position = board.user.getStatFXAnchor.getAnchor.position;
                GameObject.Destroy(rect.gameObject, 5f);
            }

            if (isActive)
                return children[0].Update();
            else
                return State.Failure;
        }

        protected override void OnStop() { }
    }
}