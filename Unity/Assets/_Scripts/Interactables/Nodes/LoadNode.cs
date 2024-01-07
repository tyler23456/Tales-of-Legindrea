using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace GDA.Interactables
{
    public class LoadNode : ActionNode
    {
        public static SaveNode.Data data;

        protected override void OnStart()
        {
            state = State.Running;
            data = new SaveNode.Data();
            Load();
            data.Load(Blackboard.factory);
            state = State.Success;
        }

        protected override State OnUpdate()
        {
            return state;
        }

        protected override void OnStop() { }

        public static void Load(string fileName = "SaveData.json")
        {
            string json = string.Empty;

            if (!File.Exists(Application.streamingAssetsPath + Path.AltDirectorySeparatorChar + fileName))
                return;

            using (StreamReader reader = new StreamReader(Application.streamingAssetsPath + Path.AltDirectorySeparatorChar + fileName))
                json = reader.ReadToEnd();

            data = JsonUtility.FromJson<SaveNode.Data>(json);

        }
    }
}
