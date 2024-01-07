using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasItem : MonoBehaviour
{
    [SerializeField] public List<Item.Entry> weapons;
    [SerializeField] public List<Item.Entry> suits;
    [SerializeField] public List<Item.Entry> helmets;
    [SerializeField] public List<Item.Entry> keyItems;
}
