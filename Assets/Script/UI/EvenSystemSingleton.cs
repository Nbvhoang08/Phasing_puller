using UnityEngine;
using UnityEngine.EventSystems;

public class EventSystemSingleton : MonoBehaviour
{
    private static EventSystemSingleton instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // Đảm bảo chỉ có một instance tồn tại
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // Đừng hủy khi chuyển scene
    }

    // Getter để truy cập instance từ bất kỳ đâu trong game
    public static EventSystemSingleton Instance
    {
        get { return instance; }
    }
}

