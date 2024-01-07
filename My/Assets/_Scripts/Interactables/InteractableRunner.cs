using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDA.Interactables
{
    public class InteractableRunner : Runner, IInteractableRunner
    {
        static bool isFirstInstance = true;

        public static IFactory factory;
        public static IMenu menu;

        public bool isUnableToActivate => tree.rootNode.state == UserNode.State.Running || SceneNode.isSceneLoading || Time.timeScale <= 0.1;

        protected new void Start()
        {
            base.Start();

            if (!isFirstInstance)
                return;

            isFirstInstance = true;

            menu = Blackboard.menu;
            factory = Blackboard.factory;
        }

        protected new void Update()
        {
            tree.Update();
        }

        public void Activate()
        {
            tree.rootNode.state = UserNode.State.Running;
        }
    }
}
