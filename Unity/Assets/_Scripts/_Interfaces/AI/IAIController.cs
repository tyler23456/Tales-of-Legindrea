using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAIController
{
    public Vector3[] wayPoints { get; }
    public IHittable hittable { get; }
    public Transform target { get; }
    public Transform fleePoint { get; }
    public float accumulator { get; set; }
    public float speed { get; set; }
    public float viewRadius { get; }
    public float viewDistance { get; }
}
