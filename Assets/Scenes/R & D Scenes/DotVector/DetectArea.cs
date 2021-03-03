using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectArea : MonoBehaviour
{
    public float cutOffAngle = 45f;
    public  bool Check(Vector3 another)
    {
        Color g;
       
        Vector3 direction = (another - this.transform.position).normalized;
        float radian = Vector3.Dot(direction, this.transform.forward); // Cos(@)..
        float angle = Mathf.Acos(radian) * Mathf.Rad2Deg; // @ = cos(pow -1)
       
        print(radian+"Radian=====Degree"+angle);
        g = angle < cutOffAngle ? Color.green : Color.red;
        Debug.DrawRay(this.transform.position, direction * 25f,g);
        Debug.DrawLine(this.transform.position, Vector3.forward * 25f);
        return angle < cutOffAngle;
    }
}
