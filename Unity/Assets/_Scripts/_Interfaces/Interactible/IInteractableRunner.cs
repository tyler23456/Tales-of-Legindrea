using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractableRunner
{
    bool enabled { get; set; }
    bool isUnableToActivate { get; }
    void Activate();
}
