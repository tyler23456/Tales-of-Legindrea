using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] public bool destroyGameObject = true;
    [SerializeField] public List<Entry> weapons;
    [SerializeField] public List<Entry> suits;
    [SerializeField] public List<Entry> helmets;
    [SerializeField] public List<Entry> keyItems;


    [System.Serializable]
    public class Entry
    {
        [SerializeField] public string name;
        [SerializeField] public int count;
    }
}

