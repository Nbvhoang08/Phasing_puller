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
    public GameObject currbox;
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

    }




}
