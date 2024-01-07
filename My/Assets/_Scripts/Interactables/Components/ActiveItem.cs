using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDA.Interactables
{
    public class ActiveItem : MonoBehaviour
    {
        static IHittable player;

        [SerializeField] public string suit;
        [SerializeField] public string helmet;
        [SerializeField] public string keyItem;

        bool isActive = true;

        void Start()
        {
            player = GameObject.Find("/DontDestroyOnLoad").GetComponent<IFactory>().getPlayer.GetComponent<IHittable>();

            foreach (Transform t in transform)
                t.gameObject.SetActive(false);

            isActive = true;
        }

        void Update()
        {
            if (!isActive || suit != "" && player.getKeyItems.Count(suit) != 0 || helmet != "" && player.getKeyItems.Count(helmet) != 0|| keyItem != "" && player.getKeyItems.Count(keyItem) != 0)
                return;

            foreach (Transform t in transform)
                t.gameObject.SetActive(true);

            isActive = false;
        }
    }
}