using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System.Linq;

namespace GDA.Interactables
{
    public class SaveNode : ActionNode
    {
        public static Data data;
        public string beaconName;

        protected override void OnStart()
        {
            state = State.Running;
            data = new Data();
            data.Save(Blackboard.factory);
            Save();
            state = State.Success;
        }

        protected override State OnUpdate()
        {
            return state;
        }

        protected override void OnStop() { }

        public static void Save(string fileName = "SaveData.json") //"NewGameData.json"
        {
            string json = JsonUtility.ToJson(data);

            using (StreamWriter writer = new StreamWriter(Application.streamingAssetsPath + Path.AltDirectorySeparatorChar + fileName))
                writer.Write(json);
        }

        [System.Serializable]
        public class Data
        {
            public int sceneID;

            public float posX;
            public float posY;
            public float posZ;
            public float forX;
            public float forY;
            public float forZ;

            public string[] weaponNames;
            public int[] weaponCounts;
            public int weaponIndex;

            public string[] keyItemNames;
            public int[] keyItemCounts;
            public int keyItemIndex;

            public string[] suitsNames;
            public int[] suitsCounts;
            public int suitsIndex;

            public string[] helmetsNames;
            public int[] helmetsCounts;
            public int helmetsIndex;

            public List<string> missions;

            public void Save(IFactory factory)
            {
                sceneID = SceneManager.GetActiveScene().buildIndex;

                posX = factory.getPlayer.transform.position.x;
                posY = factory.getPlayer.transform.position.y;
                posZ = factory.getPlayer.transform.position.z;
                forX = factory.getPlayer.transform.forward.x;
                forY = factory.getPlayer.transform.forward.y;
                forZ = factory.getPlayer.transform.forward.z;

                IHittable player = factory.getPlayer.GetComponent<IHittable>();
                weaponNames = player.getWeapons.GetNames();
                weaponCounts = player.getWeapons.GetCounts();
                weaponIndex = player.getWeapons.index;

                keyItemNames = player.getKeyItems.GetNames();
                keyItemCounts = player.getKeyItems.GetCounts();
                keyItemIndex = player.getKeyItems.index;

                suitsNames = player.getSuits.GetNames();
                suitsCounts = player.getSuits.GetCounts();
                suitsIndex = player.getSuits.index;

                helmetsNames = player.getHelmets.GetNames();
                helmetsCounts = player.getHelmets.GetCounts();
                helmetsIndex = player.getHelmets.index;

                missions = new List<string>();
                foreach (IMission mission in factory.missions.Values)
                    missions.Add(mission.Save());              
            }

            public void Load(IFactory factory)
            {
                //sceneID = SceneManager.GetActiveScene().buildIndex;
                //factory.MovePlayerTo(new Vector3(posX, posY, posZ), new Vector3(forX, forY, forZ));
                IHittable player = factory.getPlayer.GetComponent<IHittable>();

                player.getWeapons.Scroll(-player.getWeapons.index);
                player.getWeapons.SetNamesAndCounts(weaponNames, weaponCounts);
                player.getWeapons.Scroll(weaponIndex);

                player.getKeyItems.Scroll(-player.getKeyItems.index);
                player.getKeyItems.SetNamesAndCounts(keyItemNames, keyItemCounts);
                player.getKeyItems.Scroll(keyItemIndex);

                player.getSuits.Scroll(-player.getSuits.index);
                player.getSuits.SetNamesAndCounts(suitsNames, suitsCounts);
                player.getSuits.Scroll(suitsIndex);

                player.getHelmets.Scroll(-player.getHelmets.index);
                player.getHelmets.SetNamesAndCounts(helmetsNames, helmetsCounts);
                player.getHelmets.Scroll(helmetsIndex);

                foreach (string mission in missions)
                {
                    string[] results = mission.Split('|');
                    factory.missions[results[0]].Load(results);
                }
            }
        }
    }
}
