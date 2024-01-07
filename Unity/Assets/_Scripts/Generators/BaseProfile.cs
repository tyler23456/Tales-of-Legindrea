using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace GDA.Generators
{
    [ExecuteInEditMode]
    public class BaseProfile : MonoBehaviour
    {
        [SerializeField] BoundsInt mapArea = new BoundsInt(0, 0, 0, 20, 0, 20);
        [SerializeField] Vector2Int minimumRoomSize = new Vector2Int(5, 5);      
        [SerializeField] [Range(1, 1000)] int tileLength = 5;      
        [SerializeField] [Range(0, 100)] int roomOffset = 0;
        [SerializeField] bool generate = false;
        [SerializeField] bool delete = false;

        void Update()
        {
            if (delete == true)
                for (int i = this.transform.childCount; i > 0; --i)
                    DestroyImmediate(this.transform.GetChild(0).gameObject);

            delete = false;

            if (generate == false)
                return;

            generate = false;

            for (int i = this.transform.childCount; i > 0; --i)
                DestroyImmediate(this.transform.GetChild(0).gameObject);

            mapArea.size = new Vector3Int(mapArea.size.x, 1, mapArea.size.z);

            BinarySpacePartitioner binarySpacePartitioner = new BinarySpacePartitioner();
            RoomSizeGenerator roomSizeGenerator = new RoomSizeGenerator();
            BoundsToPositionConverter boundsToPositionConverter = new BoundsToPositionConverter();
            CorridorGenerator corridorGenerator = new CorridorGenerator();

            ThemeManager floorThemes = new ThemeManager();
            ThemeManager ceilingThemes = new ThemeManager();
            ThemeManager wallThemes = new ThemeManager();

            TileInstantiator tileInstantiator = new TileInstantiator();
            WallInstantiator wallInstantiator = new WallInstantiator();
            CornerInstantiator cornerInstantiator = new CornerInstantiator();


            GameObject[] largeFloorPool = Resources.LoadAll<GameObject>("LargeFloors");
            GameObject[] smallFloorPool = Resources.LoadAll<GameObject>("SmallFloors");
            GameObject[] ceilingPool = Resources.LoadAll<GameObject>("Ceilings");
            GameObject[] wallPool = Resources.LoadAll<GameObject>("Walls");
            GameObject[] halfWallPool = Resources.LoadAll<GameObject>("CrewWalls");
            GameObject[] cornerPool = Resources.LoadAll<GameObject>("CornerPillars");

            floorThemes.Add(largeFloorPool);
            ceilingThemes.Add(ceilingPool);
            wallThemes.Add(wallPool);

            List<GameObject> corners = new List<GameObject>();
            corners.Add(cornerPool[Random.Range(0, cornerPool.Length)]);


            List<BoundsInt> rooms = binarySpacePartitioner.Generate(mapArea, minimumRoomSize.x, minimumRoomSize.y);
            rooms = roomSizeGenerator.Generate(rooms, 2, 2, 3, 4);

            List<HashSet<Vector2Int>> roomSetList = boundsToPositionConverter.Generate(rooms, roomOffset);
            HashSet<Vector2Int> roomSet = new HashSet<Vector2Int>();
            roomSetList.ForEach((i) => roomSet.UnionWith(i));

            
            HashSet<Vector2Int> corridorSet = corridorGenerator.Generate(rooms);
            HashSet<Vector2Int> overlapSet = roomSet.Intersect(corridorSet).ToHashSet();
            HashSet<Vector2Int> corridorOnlySet = corridorSet.Except(roomSet).ToHashSet();
            HashSet<Vector2Int> floorSet = roomSet.Union(corridorSet).ToHashSet();

            List<HashSet<Vector2Int>> roomOnlySetList = new List<HashSet<Vector2Int>>();
            roomSetList.ForEach((i) => roomOnlySetList.Add(new HashSet<Vector2Int>(i)));
            roomOnlySetList.ForEach((i) => i.ExceptWith(corridorSet));


            roomSetList.ForEach((i) => tileInstantiator.Instantiate(i, floorThemes.Next(2), transform));
            roomSetList.ForEach((i) => tileInstantiator.Instantiate(i, ceilingThemes.Next(1), transform));           
            roomSetList.ForEach((i) => wallInstantiator.Instantiate(i, floorSet, wallThemes.Next(1), wallThemes.Next(1), transform));
            roomSetList.ForEach((i) => cornerInstantiator.Instantiate(i, floorSet, corners[0], transform));

            tileInstantiator.Instantiate(corridorOnlySet, floorThemes.Next(2), transform);
            tileInstantiator.Instantiate(corridorOnlySet, ceilingThemes.Next(1), transform);
            wallInstantiator.Instantiate(corridorOnlySet, floorSet, wallThemes.Next(1), wallThemes.Next(1), transform);
            cornerInstantiator.Instantiate(corridorOnlySet, floorSet, corners[0], transform);
        }
    }
}