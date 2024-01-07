using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface ISceneLoader
{
    bool isSceneLoading { get; }
    Func<bool> onPredicate { get; set; }
    Action onCompleted { get; set; }
    void Load(int sceneID);
}
