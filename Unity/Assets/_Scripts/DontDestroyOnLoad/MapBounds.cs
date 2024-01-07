using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDA.DontDestroyOnLoad
{
    public class MapBounds : MonoBehaviour
    {   
        [SerializeField]
        Transform forward;
        [SerializeField]
        Transform back;
        [SerializeField]
        Transform left;
        [SerializeField]
        Transform right;
        [SerializeField]
        Transform up;
        [SerializeField]
        Transform down;

        [SerializeField]
        Vector3 extents;

        void OnValidate()
        {
            forward.position = transform.position + Vector3.forward * extents.z;
            back.position = transform.position + Vector3.back * extents.z;
            left.position = transform.position + Vector3.left * extents.x;
            right.position = transform.position + Vector3.right * extents.x;
            up.position = transform.position + Vector3.up * extents.y;
            down.position = transform.position + Vector3.down * extents.y;

            forward.localScale = new Vector3(extents.x, extents.y, 0f) * 2f;
            back.localScale = new Vector3(extents.x, extents.y, 0f) * 2f;
            left.localScale = new Vector3(0f, extents.y, extents.z) * 2f;
            right.localScale = new Vector3(0f, extents.y, extents.z) * 2f;
            up.localScale = new Vector3(extents.x, 0f, extents.z) * 2f;
            down.localScale = new Vector3(extents.x, 0f, extents.z) * 2f;
        }
    }
}