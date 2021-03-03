using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiTriggerChallenge : MonoBehaviour
{

    public OnBoolMod OnPlayerEnterTrigger;
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
        OnPlayerEnterTrigger?.Invoke(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            OnPlayerEnterTrigger?.Invoke(false);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
    }
}
