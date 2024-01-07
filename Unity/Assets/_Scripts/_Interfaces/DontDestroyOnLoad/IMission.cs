using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMission
{
    string getName { get; }
    string getFirstTask { get; }
    bool hasTasks { get; }
    string getMissionCompletedTask { get; }
    bool ContainsTask(string taskName);
    void Initialize();
    void RemoveFirst();
    void Remove(string name);
    string Save();
    void Load(string[] mission);
}
