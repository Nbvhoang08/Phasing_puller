using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCau : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform luoiCau;
    [SerializeField] Transform player;
    private LineRenderer lineRenderer;
    private EdgeCollider2D edgeCollider;
    void Start()
    {
        transform.position = player.position;
        lineRenderer = GetComponent<LineRenderer>();
        edgeCollider = GetComponent<EdgeCollider2D>();

        UpdateCollider();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<LineRenderer>().SetPosition(0, new Vector3(player.position.x,player.position.y,-1));
        gameObject.GetComponent<LineRenderer>().SetPosition(1, new Vector3(luoiCau.position.x, luoiCau.position.y, -1));
    }

    void UpdateCollider()
    {
        Vector3[] positions = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(positions);

        Vector2[] colliderPoints = new Vector2[positions.Length];
        for (int i = 0; i < positions.Length; i++)
        {
            colliderPoints[i] = new Vector2(positions[i].x, positions[i].y);
        }

        edgeCollider.points = colliderPoints;
    }

}
