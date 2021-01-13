using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch : MonoBehaviour
{
    private RaycastHit hit;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                //transform.position = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                Vector3 direction = hit.point - transform.position;
                transform.rotation = Quaternion.LookRotation(direction);
                //transform.Rotate(new Vector3(transform.position.x,direction.y,transform.position.z));
            }
        }
    }
}
