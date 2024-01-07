using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace GDA.Interactables
{
    public class RandomItemNode : ActionNode
    {
        bool isFirstTime = true;

        [SerializeField] string sfx = "";
        [SerializeField] List<ItemProbability> itemProbabilities;

        List<(string, int)> items = new List<(string, int)>();
        List<float> probabilities = new List<float>();

        protected override void OnStart()
        {
            if (isFirstTime)
            {
                foreach (ItemProbability itemProbability in itemProbabilities)
                {
                    items.Add((itemProbability.name, itemProbability.count));
                    probabilities.Add(itemProbability.probability);
                }
                isFirstTime = false;
            }

            state = State.Running;

            (string name, int count) item = WeightedDecision.Generate(items, probabilities);

            GameObject obj = GameObject.Find("/DontDestroyOnLoad/Canvas/Display1");
            foreach (Transform t in obj.transform)
                if (t.tag == "Replaceable")
                    Destroy(t.gameObject);

            IHittable player = GameObject.Find("/DontDestroyOnLoad").GetComponent<IFactory>().getPlayer.GetComponent<IHittable>();
            player.getWeapons.Add(item.name, item.count);

            board.audioSource.Play();
        }
        
        protected override State OnUpdate()
        {
            state = State.Success;
            return state;
        }

        protected override void OnStop() 
        {
            if (board.renderer != null) { board.renderer.enabled = false; }
            board.interactableRunner.enabled = false;
            board.collider.enabled = false;
            if (board.gameObject.TryGetComponent(out BoxCollider col)) { col.enabled = false; };

            foreach (Transform t in board.gameObject.transform)
                t.gameObject.SetActive(false);

            Destroy(board.gameObject, 3f);
        }

        [System.Serializable]
        public class ItemProbability
        {
            [SerializeField] public string name;
            [SerializeField] public int count;
            [SerializeField] public float probability;
        }

        public static class WeightedDecision
        {
            static float weightSum = 0f;

            public static T Generate<T>(List<T> decisions, List<float> weights)
            {
                weightSum = weights.Sum();

                float randomNumber = UnityEngine.Random.Range(0f, 1f);

                float weightAccumulator = 0;
                float normalizedWeight;

                for (int i = 0; i <= decisions.Count; i++)
                {
                    normalizedWeight = weights[i] / weightSum;
                    weightAccumulator += normalizedWeight;

                    if (randomNumber <= weightAccumulator)
                        return decisions[i];
                }
                return decisions.Last();
            }
        }
    }
}
