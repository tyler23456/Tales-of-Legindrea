using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDA.Generators
{
    public class TileInstantiator
    {
        static List<Vector2Int> pointList = new List<Vector2Int>();
        static GameObject obj;
        static Vector3 position = Vector3.zero;
        static int i = 0;

        public void Instantiate(HashSet<Vector2Int> pointSet, List<GameObject> tiles, Transform parent)
        {
            foreach (Vector2Int p in pointSet)
            {
                i = Random.Range(0, tiles.Count);

                position.x = p.x * 5f;
                position.z = p.y * 5f;

                obj = GameObject.Instantiate(tiles[i], position, Quaternion.identity);
                obj.transform.parent = parent;
            }           
        }
    }
}