using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableBinder : MonoBehaviour
{
    [SerializeField] public List<GameObject> disableReferences;
    [SerializeField] public List<GameObject> enableReferences;
}