using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherAnimation : MonoBehaviour
{
    [SerializeField] Animator animator;
    public Animation getAnimator => GetComponent<Animation>();
}
