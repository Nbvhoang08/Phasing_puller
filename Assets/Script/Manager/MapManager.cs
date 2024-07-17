using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class MapManager : Singleton<MapManager>
{
   
    public List<GameObject> Tilemap;
  
    public GameObject currentMap;
 

    // Đảm bảo instance này không bị hủy khi tải scene mới
    protected override void Awake()
    {
        SetShouldNotDestroyOnLoad(false);
        base.Awake();
        if (Tilemap == null || Tilemap.Count == 0)
        {
            GameObject[] tilemapArray = GameObject.FindGameObjectsWithTag("Map");
            Tilemap = tilemapArray.ToList();
        }
        UpdateTilemapList();
        for (int i = 0; i < Tilemap.Count; i++)
        {
            if (i == 0)
            {
                Tilemap[i].SetActive(true);
                currentMap = Tilemap[i];
            }
            else
            {
                Tilemap[i].SetActive(false);
            }
        }
    }
    
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeMap(int index)
    {
        for (int i = 0; i < Tilemap.Count; i++)
        {
            if (i == index)
            {
                Tilemap[index].SetActive(true);
                currentMap = Tilemap[index];
            }
            else
            {
                Tilemap[i].SetActive(false);
            }
        }
    }

    public void UpdateTilemapList()
    {
        Tilemap = new List<GameObject>();
        foreach (Transform child in GetAllTransformsInHierarchy())
        {
            if (child.CompareTag("Map"))
            {
                Tilemap.Add(child.gameObject);
            }
        }
    }

    private IEnumerable<Transform> GetAllTransformsInHierarchy()
    {
        var rootObjects = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (var rootObject in rootObjects)
        {
            foreach (var transform in rootObject.GetComponentsInChildren<Transform>(true))
            {
                yield return transform;
            }
        }
    }
}

