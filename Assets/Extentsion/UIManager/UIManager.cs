using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
    // Start is called before the first frame update
    Dictionary<System.Type, UICanvas> canvasActives = new Dictionary<System.Type, UICanvas>();
    Dictionary<System.Type, UICanvas> canvasPrefabs = new Dictionary<System.Type, UICanvas>();
    [SerializeField] Transform parent;

    private void Awake()
    {
        // Load tất cả các đối tượng UICanvas từ thư mục Resources/UI
        UICanvas[] prefabs = Resources.LoadAll<UICanvas>("UI");

        // Kiểm tra xem có tải được các đối tượng UI hay không
        if (prefabs.Length == 0)
        {
            Debug.LogError("Không tải được bất kỳ đối tượng UICanvas nào từ thư mục Resources/UI.");
            return;
        }

        // Duyệt qua tất cả các đối tượng UICanvas vừa tải
        for (int i = 0; i < prefabs.Length; i++)
        {
            // Kiểm tra xem đối tượng có null hay không
            if (prefabs[i] == null)
            {
                Debug.LogError($"Đối tượng UICanvas tại vị trí {i} bị null.");
                continue;
            }

            // Thêm từng UICanvas vào dictionary canvasPrefabs
            // Sử dụng kiểu của UICanvas làm key và đối tượng UICanvas làm value
            if (!canvasPrefabs.ContainsKey(prefabs[i].GetType()))
            {
                canvasPrefabs.Add(prefabs[i].GetType(), prefabs[i]);
                Debug.Log($"Đã tải và thêm UICanvas: {prefabs[i].name}");
            }
            else
            {
                Debug.LogWarning($"UICanvas với key {prefabs[i].GetType()} đã tồn tại trong dictionary.");
            }
        }

        // Kiểm tra cuối cùng xem dictionary có được thêm đầy đủ các đối tượng không
        if (canvasPrefabs.Count == 0)
        {
            Debug.LogError("Không có đối tượng UICanvas nào được thêm vào dictionary.");
        }
        else
        {
            Debug.Log($"Đã thêm tổng cộng {canvasPrefabs.Count} đối tượng UICanvas vào dictionary.");
        }
    }


    public T OpenUI<T>() where T : UICanvas
    {
        T canvas = GetUI<T>();
        canvas.Setup();
        canvas.Open();
        return canvas ;
    }
    //dong canvas sau time
    public void CloseUI<T>(float time) where T : UICanvas
    {
        if(IsUILoaded<T>())
        {
            canvasActives[typeof(T)].Close(time);
        }
    }
    //dong canvas truc tiep
    public void CloseUIDirectly<T>() where T : UICanvas
    {
        if (OpenUI<T>())
        {
            canvasActives[typeof(T)].CloseDirectly();
        }
    }
    //kiem tra canvas da dc tao chua 
    public bool IsUILoaded<T>() where T : UICanvas
    {
        return canvasActives.ContainsKey(typeof(T)) && canvasActives[typeof(T)];
    }
    //kiem tra canvas da dc mo chua 
    public bool IsUIOpened<T>() where T : UICanvas
    {
        return IsUILoaded<T>() && canvasActives[typeof(T)].gameObject.activeSelf;
    }
    //lay canvas 
    public T GetUI<T>() where T : UICanvas
    {
        if (!IsUILoaded<T>())
        {
            T prefab  = GetUIPrefab<T>();
            T canvas = Instantiate(prefab,parent);
            canvasActives[typeof(T)] = canvas;
        }
        return canvasActives[typeof(T)] as T;
    }
    private T GetUIPrefab<T>() where T :UICanvas
    {
        return canvasPrefabs[typeof(T)] as T ;
    }
    public void CloseAll() 
    {
        foreach (var canvas in canvasActives)
        {
            if(canvas.Value != null && canvas.Value.gameObject.activeSelf)
            {
                canvas.Value.Close(0);
            }
        }
    }

 



}
