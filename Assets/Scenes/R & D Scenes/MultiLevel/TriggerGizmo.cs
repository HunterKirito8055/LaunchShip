using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGizmo : MonoBehaviour
{
    public Transform nextTP;
    public Transform prevTP;
    public int prevScene;
    public int nextScene;
   public bool isPlayerEntered;
    void Start()
    {
        prevScene = nextScene - 1;
        if(transform.childCount > 0)
        {
            nextTP = transform.GetChild(0).transform;
            prevTP = transform.GetChild(1).transform;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (isPlayerEntered)
        {
            isPlayerEntered = false;
            AsyncLevelLoading.sharedInstance.UnloadCurrentLevel(nextScene, prevScene, prevTP.position);
            return;
        }
        if (other.CompareTag("Player"))
        {
            isPlayerEntered = true;
            AsyncLevelLoading.sharedInstance.LoadNextLevel(nextScene, prevScene, nextTP.position);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
    }
}
