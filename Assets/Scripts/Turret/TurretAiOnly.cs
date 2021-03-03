using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TurretAiOnly : MonoBehaviour
{

    [SerializeField] Transform wayPointParent;
    Transform playerTransform;
    public NavMeshAgent agent;
    Vector3 currentDestination;
    public Vector3[] wayPointsArray;
    float distance;
    public bool isPlayerInArea,isPlayerInRange,stopAI;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (wayPointParent == null)
        { this.enabled = false; return; }

        if (wayPointParent.childCount < 2)//turret must not patrol if waypoints are less than 2
        { this.enabled = false; return; }

        GetWayPoints();
        playerTransform = GameManager.sharedInstance.playerScript.transform;
    }
    void GetWayPoints()
    {
        wayPointsArray = new Vector3[wayPointParent.childCount];
        int getChildCount = wayPointParent.childCount;
        for (int i = 0; i < getChildCount; i++)
        {
            wayPointsArray[i] = wayPointParent.GetChild(i).position;
        }
    }
    private void Update()
    {
        if (stopAI)
        {
            return;
        }
        if (isPlayerInArea/* && !isPlayerInRange*/)
        {
            if (isPlayerInRange)
            {
                ChasePlayer();
                return;
            }
            CheckNextDestination();
        }
       
    }
    void ChasePlayer()
    {
        currentDestination = playerTransform.position;
        agent.stoppingDistance = 1f;
        agent.SetDestination(currentDestination);
    }
    void Patrol()
    {
        currentDestination = wayPointsArray[Random.Range(0, wayPointsArray.Length)];
        agent.SetDestination(currentDestination);
    }
    void CheckNextDestination()
    {
        agent.stoppingDistance = 0.5f;
        distance = Vector3.Distance(transform.position, currentDestination);
        if (agent.stoppingDistance > distance)
        {
            Patrol();
        }
    }
    public void OnPlayerInArea(bool val)
    {
        isPlayerInArea = val;
        if (isPlayerInArea)
        {
            agent.isStopped = false;
            Patrol();
        }
        else
        {
            agent.isStopped = true;
        }
    }
    public void OnPlayerInRange(bool val)
    {
        isPlayerInRange = val;
        if (isPlayerInRange)
        {
            agent.isStopped = true;
        }
        else { agent.isStopped = false; }
    }
    public void SetStopCondition(bool val)
    {
        stopAI = val;
        agent.isStopped = stopAI;
    }

}
