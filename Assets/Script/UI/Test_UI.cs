using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class Test_UI : MonoBehaviour
{
    private static Test_UI instance;

    public static Test_UI Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Test_UI>();
                if (instance == null)
                {
                    GameObject go = new GameObject("Test Ui");
                    instance = go.AddComponent<Test_UI>();
                }
            }
            return instance;
        }
    }
    private void Awake()
    {
        // Đảm bảo rằng chỉ có một instance duy nhất của CameraManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Giữ nguyên đối tượng này khi chuyển scene
        }
        else
        {
            Destroy(gameObject); // Đã có instance khác tồn tại, hủy đối tượng này đi
            return;
        }
    }
    private void Start()
    {
        UIManager.Instance.OpenUI<CanvasMainMenu>();
    }
    void OnEnable()
    {
        // Đăng ký sự kiện SceneManager.sceneLoaded
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // Hủy đăng ký sự kiện SceneManager.sceneLoaded khi không cần thiết nữa
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Lấy đối tượng Camera của scene mới
        Camera newSceneCamera = Camera.main; // Hoặc bạn có thể lấy camera theo cách khác tùy theo thiết lập của bạn

        // Cập nhật lại eventCamera của canvas hoặc bất kỳ đối tượng nào cần truy cập camera
        Canvas canvas = GetComponent<Canvas>();
        if (canvas != null)
        {
            canvas.worldCamera = newSceneCamera;
        }
    }
}
