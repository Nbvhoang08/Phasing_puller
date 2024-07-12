using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapManager : MonoBehaviour
{
    // Instance tĩnh của lớp này
    private static MapManager _instance;

    // Thuộc tính công khai để truy cập instance
    public static MapManager Instance
    {
        get
        {
            // Kiểm tra xem instance đã được tạo chưa
            if (_instance == null)
            {
                // Tìm instance của lớp trong scene
                _instance = FindObjectOfType<MapManager>();

                // Nếu không tìm thấy, tạo một GameObject mới và gán script
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<MapManager>();
                    singletonObject.name = typeof(MapManager).ToString() + " (Singleton)";
                }
            }

            return _instance;
        }
    }

    // Đảm bảo instance này không bị hủy khi tải scene mới
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    public List<GameObject> Tilemap;
    public GameObject currentMap;
    
    

    void Start()
    {
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
}

