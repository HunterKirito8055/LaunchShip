using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoTest : MonoBehaviour
{
    DetectArea detectArea ;
    public int HowMany;
    public float radius = 100f;
    private void Start()
    {
        if(detectArea ==null)
        detectArea = GameObject.FindObjectOfType<DetectArea>();
        for(int i=0;i<HowMany;i++)
        {
            GameObject go = new GameObject();
            go.transform.position = Random.insideUnitSphere * radius;
            go.AddComponent(typeof(GizmoTest));
        }
    }
    private void OnDrawGizmos()
    {
        detectArea = GameObject.FindObjectOfType<DetectArea>();
        Gizmos.color = detectArea.Check(this.transform.position) ? Color.green : Color.red;
        Gizmos.DrawSphere(this.transform.position,0.5f);
        Debug.DrawRay(transform.position,transform.up.normalized * 5f);
        Debug.DrawRay(transform.position, -transform.up * 5f);
        
    }
}
