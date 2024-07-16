using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "MapData", menuName = "MapData", order = 0)]
public class MapData : ScriptableObject
{
    // Start is called before the first frame update
    public GameObject[] mapArray;
   
    // Update is called once per frame
    public List<GameObject> Map
    {
        get => new List<GameObject>(mapArray);
        set => mapArray = value.ToArray();
    }

}
