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
    public bool checkObs;
    void Start()
    {
        transform.position = player.position;
        lineRenderer = GetComponent<LineRenderer>();
       /* edgeCollider = GetComponent<EdgeCollider2D>();*/

        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<LineRenderer>().SetPosition(0, new Vector3(player.position.x,player.position.y,-1));
        gameObject.GetComponent<LineRenderer>().SetPosition(1, new Vector3(luoiCau.position.x, luoiCau.position.y, -1));
        DetectInteractionWithObjects() ;
       /* UpdateCollider();*/



    }

   /* void UpdateCollider()
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
*/


    void DetectInteractionWithObjects()
    {
        // Duyệt qua từng segment của LineRenderer
        for (int i = 0; i < lineRenderer.positionCount - 1; i++)
        {
            Vector2 startPoint = lineRenderer.GetPosition(i);
            Vector2 endPoint = lineRenderer.GetPosition(i + 1);

            // Kiểm tra tương tác với các đối tượng có EdgeCollider2D
            if (CheckInteractionWithEdgeColliders(startPoint, endPoint))
            {
                Debug.Log("Interaction with EdgeCollider2D detected!");
                // Thực hiện hành động khi phát hiện tương tác
                checkObs= true;
            }
            else
            {
                checkObs=  false;
            }
        }
    }

    public bool CheckInteractionWithEdgeColliders(Vector2 start, Vector2 end)
    {
        // Lấy tất cả các Collider2D trong khoảng từ start đến end
        RaycastHit2D[] hits = Physics2D.LinecastAll(start, end);

        foreach (RaycastHit2D hit in hits)
        {
            EdgeCollider2D edgeCollider = hit.collider.GetComponent<EdgeCollider2D>();
            if (edgeCollider != null && (edgeCollider.CompareTag("box") || edgeCollider.gameObject.layer == 9))
            {
                return true;
            }
        }

        return false;
    }

}
