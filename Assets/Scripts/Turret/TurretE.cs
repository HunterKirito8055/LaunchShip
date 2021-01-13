using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TurretE : MonoBehaviour
{
    public Transform currentDestination;
    public Transform[] wayPoints;
    public int i;
    NavMeshAgent agent;

    public float range = 1f;
   public float shootTimer = 1f;
    public float startTimer ;
    Rigidbody rb;
    Transform Player;
    Gun gun;
    bool gameOver;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gun = GetComponent<Gun>();
        agent = GetComponent<NavMeshAgent>();
        CheckNextDestination();
    }

    // Update is called once per frame
   
    void Update()
    {

        if (gameOver || GameManager.sharedInstance.uIManagerScript.IsPauseGame)
        {
            return;
        }

            if (Player )
            {
            agent.isStopped = true;
                Vector3 faceDir = new Vector3(Player.position.x, transform.position.y, Player.position.z);

                Vector3 dir = faceDir - transform.position;
                dir.Normalize();

                float distance = Vector3.Distance(transform.position, Player.position);
                float dot = Vector3.Dot(transform.forward, dir);
                Quaternion from = transform.rotation;
                Quaternion to = Quaternion.LookRotation(dir);

                transform.rotation = Quaternion.Lerp(from, to, Time.deltaTime * 10f);
                startTimer += Time.deltaTime;
                if (startTimer > shootTimer)
                {
                    startTimer = 0;
                    gun.ShootBullet();
                }

            }
        else
        {
            agent.isStopped = false;
            if(agent.stoppingDistance >=Vector3.Distance(currentDestination.position,gameObject.transform.position))
                CheckNextDestination();
        }
        
    }
    void CheckNextDestination()
    {
        i = Random.Range(0, wayPoints.Length);
        currentDestination = wayPoints[i];
        Patrol();
    }
    void Patrol()
    {
        if(!Player)
        agent.SetDestination(currentDestination.position);
    }
    private void OnDrawGizmos()
    {
        GetComponent<SphereCollider>().radius = range;
        Gizmos.DrawWireSphere(transform.position, range);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player = other.gameObject.transform;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            Player = null;
        }
    }

    public void GameOverCheck()
    {
        gameOver = true;
        Player = null;
    }
    public void Replay()
    {
        gameOver = false;
    }
}
