using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowPlayer : MonoBehaviour
{
    public GameObject Player;
    [Range(1f,10f)]
    public float Smoothness;
    public Vector3 offset;
    private void Start()
    {
        offset = transform.position - Player.transform.position;
    }
    private void Update()
    {
        Vector3 nextPos = offset + Player.transform.position;
        transform.position = Vector3.Lerp(transform.position, nextPos, Smoothness);
    }
}
