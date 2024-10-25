using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.WSA;
using static TreeEditor.TextureAtlas;

public class Trail : MonoBehaviour
{
    private LineRenderer _currentLine;
    private float _minimumDistance = 0.1f;
    private float _maxPoints = 1000;
    private int _currentPoints = 0;
    public Material trailMaterial;
    public Color trailColor = Color.white;
    public float lineWidth = 0.1f;
    private GameObject _linesParent;

    [Header("Trail Offset")]
    public float offsetDistance = 0.5f;
    private Vector2 _center = Vector2.zero;

    [Header("Progress Tracking")]
    public float _fillPercentage;
    private Vector2 _startPosition;
    private float _startAngle;
    private float _currentAngle;
    private float _totalAngleRotated;
    public bool isCompleted = false;

    private bool _wasDrawing = false;
    Move move;

    // Danh sách để lưu trữ các đoạn cung tròn đã đi qua
    private List<ArcSegment> _visitedArcs = new List<ArcSegment>();

    void Start()
    {
        _linesParent = new GameObject("PaintLines");
        CreateNewLine();

        // Lưu vị trí bắt đầu
        _startPosition = GetOffsetPosition();
        _startAngle = GetAngleFromPosition(_startPosition);
        _currentAngle = _startAngle;
        _totalAngleRotated = 0;

        move = this.GetComponent<Move>();
    }

    float GetAngleFromPosition(Vector2 position)
    {
        // Tính góc từ điểm hiện tại đến tâm (theo radian)
        return Mathf.Atan2(position.y - _center.y, position.x - _center.x);
    }

    void Update()
    {
        if (!isCompleted)
        {
            Vector2 currentOffsetPos = GetOffsetPosition();

            if (move.CanDraw && !_wasDrawing)
            {
                // Reset tracking khi bắt đầu vẽ mới
                _startPosition = currentOffsetPos;
                _startAngle = GetAngleFromPosition(_startPosition);
                _currentAngle = _startAngle;
                CreateNewLine();
            }
            _wasDrawing = move.CanDraw;

            if (_currentLine != null && move.CanDraw)
            {
                if (_currentPoints == 0 || Vector2.Distance(GetLastPoint(), currentOffsetPos) > _minimumDistance)
                {
                    if (_currentPoints >= _maxPoints)
                    {
                        CreateNewLine();
                    }

                    // Tính toán góc mới và cập nhật tiến độ
                    float newAngle = GetAngleFromPosition(currentOffsetPos);
                    float deltaAngle = Mathf.DeltaAngle(_currentAngle * Mathf.Rad2Deg, newAngle * Mathf.Rad2Deg) * Mathf.Deg2Rad;

                    // Tạo đoạn cung tròn mới
                    ArcSegment newArc = new ArcSegment(_currentAngle, newAngle);

                    // Kiểm tra xem đoạn cung tròn mới có giao với bất kỳ đoạn cung tròn nào đã đi qua không
                    bool overlaps = false;
                    foreach (var arc in _visitedArcs)
                    {
                        if (newArc.Overlaps(arc))
                        {
                            overlaps = true;
                            break;
                        }
                    }

                    // Nếu không giao, thêm đoạn cung tròn mới vào danh sách và cập nhật _totalAngleRotated
                    if (!overlaps)
                    {
                        _totalAngleRotated += deltaAngle;
                        _visitedArcs.Add(newArc);
                    }

                    _currentAngle = newAngle;

                    // Tính phần trăm hoàn thành dựa trên góc đã quét
                    _fillPercentage = (_totalAngleRotated / (2 * Mathf.PI)) * 100f;
                    _fillPercentage = Mathf.Abs(_fillPercentage);

                    // Kiểm tra hoàn thành
                    if (_fillPercentage >= 100f)
                    {
                        isCompleted = true;
                        _fillPercentage = 100f;
                    }

                    // Vẽ line
                    _currentPoints++;
                    _currentLine.positionCount = _currentPoints;
                    _currentLine.SetPosition(_currentPoints - 1, currentOffsetPos);
                }
            }
        }

     
    }

    Vector2 GetOffsetPosition()
    {
        Vector2 directionToCenter = (_center - (Vector2)transform.position).normalized;
        return (Vector2)transform.position - directionToCenter * offsetDistance;
    }

    void CreateNewLine()
    {
        GameObject lineGO = new GameObject("PaintLine");
        lineGO.transform.SetParent(_linesParent.transform);

        _currentLine = lineGO.AddComponent<LineRenderer>();

        _currentLine.material = trailMaterial != null ? trailMaterial : new Material(Shader.Find("Sprites/Default"));
        _currentLine.startColor = trailColor;
        _currentLine.endColor = trailColor;
        _currentLine.startWidth = lineWidth;
        _currentLine.endWidth = lineWidth;
        _currentLine.positionCount = 0;
        _currentLine.useWorldSpace = true;
        _currentLine.textureMode = LineTextureMode.Tile; ;

        _currentPoints = 0;
    }

    Vector3 GetLastPoint()
    {
        if (_currentPoints > 0)
        {
            return _currentLine.GetPosition(_currentPoints - 1);
        }
        return Vector3.zero;
    }

    public void ClearLines()
    {
        if (_linesParent != null)
        {
            Destroy(_linesParent);
            _linesParent = new GameObject("PaintLines");
            CreateNewLine();

            _startPosition = GetOffsetPosition();
            _startAngle = GetAngleFromPosition(_startPosition);
            _currentAngle = _startAngle;
            isCompleted = false;
            _wasDrawing = false;
            _visitedArcs.Clear(); // Xóa danh sách các đoạn cung tròn đã đi qua
        }
    }

    public float GetFillPercentage()
    {
        return _fillPercentage;
    }

    public bool IsCompleted()
    {
        return isCompleted;
    }
}

// Lớp để lưu trữ các đoạn cung tròn
public class ArcSegment
{
    public float StartAngle { get; set; }
    public float EndAngle { get; set; }

    public ArcSegment(float startAngle, float endAngle)
    {
        StartAngle = startAngle;
        EndAngle = endAngle;
    }

    public bool Overlaps(ArcSegment other)
    {
        return !(EndAngle <= other.StartAngle || StartAngle >= other.EndAngle);
    }
}
