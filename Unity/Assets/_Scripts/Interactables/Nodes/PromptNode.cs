using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDA.Interactables
{
    public class PromptNode : CompositeNode
    {
        static IMenu menu;
        static IPanel window;
        static IButton option;
        static ITypeWriter typeWriter;
        static bool isFirstInstance = true;

        [SerializeField]
        [TextArea(5, 20)]
        string body;
        [SerializeField] string animationParameter = "";
        [SerializeField] bool playAudioSource = false;

        List<UserNode> promptChildren;
        List<UserNode> otherChildren;

        bool hasDisplayedOptions = false;
        bool hasSelectedOption = false;

        protected override void OnStart()
        {
            if (isFirstInstance)
            {
                isFirstInstance = false;
                menu = Blackboard.menu;

                window = menu.CreatePanel();
                window.anchor = menu.topRightAnchor;
                window.position = new Vector2Int(-824, -24);
                window.size = new Vector2Int(800, 300);

                option = menu.CreateButton();
                option.size = new Vector2Int(400, 50);
                option.border = menu.blue;
                option.fill = menu.dark;
                option.onShowPosition = (i) => new Vector2Int(0, -324 - 62 * i);
                option.textAlignment = TMPro.TextAlignmentOptions.Center;

                typeWriter = GameObject.Find("/DontDestroyOnLoad").GetComponent<ITypeWriter>();
            }

            state = State.Running;

            promptChildren = new List<UserNode>();
            otherChildren = new List<UserNode>();

            foreach (UserNode child in children)
                if (child is PromptNode)
                    promptChildren.Add(child);           
                else
                    otherChildren.Add(child);

            hasDisplayedOptions = false;
            hasSelectedOption = false;

            option.onShowText = (i) => promptChildren[i].nodeName;
            option.onClick = (i) => promptChildren[i].state = State.Running;
            option.onClick += (i) => childIndex = i;
            option.onClick += (i) => hasSelectedOption = true;

            window.onShow = () => { };
            window.text = body;
            window.Show(menu.parent1);

            typeWriter.ResetEvents();

            if (animationParameter != "")
            {
                typeWriter.onScroll = () => board.animator.SetBool(animationParameter, true);
                typeWriter.onPaused = () => board.animator.SetBool(animationParameter, false);
            }

            if (playAudioSource)
            {
                board.audioSource.ignoreListenerPause = true;
                board.audioSource.Play();
            }

            typeWriter.onBegin = BeginPrompt;
            typeWriter.onEnd = EndPrompt;

            typeWriter.onEndPredicate = () => promptChildren.Count == 0;

            typeWriter.SetText(window.body);

            if (!typeWriter.isActive)
            {
                typeWriter.Write();
                menu.StartCoroutine(RotateTowardPlayer());
            }    
        }

        IEnumerator RotateTowardPlayer()
        {
            float startTime = Time.time;
            float duration = 2f;
            if (animationParameter != "")
            {
                Vector3 targetDirection = (Blackboard.factory.getPlayer.transform.position - board.animator.transform.position).normalized;
                targetDirection.y = 0f;
                while (Time.time - startTime <= duration)
                {
                    board.animator.transform.forward = Vector3.Slerp(board.animator.transform.forward, targetDirection, 6f * Time.unscaledDeltaTime);
                    yield return new WaitForEndOfFrame();
                }
            }
        }

        protected override State OnUpdate()
        {
            if (typeWriter.isDoneScrolling && !hasDisplayedOptions)
            {
                window.onShow = () => window.DrawRange(option, promptChildren.Count);
                window.Show(menu.parent1);
                hasDisplayedOptions = true;
            }

            if (promptChildren.Count > 0 && hasSelectedOption)
                return promptChildren[childIndex].Update();

            if (otherChildren.Count > 0 && !typeWriter.isActive)
                return otherChildren[0].Update();

            if (children.Count == 0)
                state = State.Success;

            return state;
        }

        void BeginPrompt()
        {
            GameObject obj = GameObject.Find("/DontDestroyOnLoad/Canvas/Display1");
            foreach (Transform t in obj.transform)
                if (t.tag == "Replaceable")
                    GameObject.Destroy(t.gameObject);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
            AudioListener.pause = true;
        }

        void EndPrompt()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1f;
            AudioListener.pause = false;
            window.Hide();
            //state = State.Failure;
        }

        void ResetState()
        {
            
        }

        protected override void OnStop() { }
    }
}