using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace GDA.Independent
{
    public class RunwayBossBehavior : MonoBehaviour
    {
        [SerializeField] GameObject boss;
        [SerializeField] Animator animator;

        void Start()
        {
            
        }

        void Update()
        {
            
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.name != "Player")
                return;

            animator.SetTrigger("toggle");
            animator.GetComponent<AudioSource>().Play();
            
            boss.transform.GetComponent<NavMeshAgent>().enabled = true;
            boss.transform.GetComponent<IHittable>().enabled = true;
            Destroy(gameObject);
        }
    }
}