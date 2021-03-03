using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TurretE : MonoBehaviour
{
    public OnBoolMod onPlayerInRange;
    public float range = 1f;
    public float shootTimer = 1f;
    public float startTimer;
    Rigidbody rb;
    Transform Player;
    Gun gun;
    bool gameOver;
    Vector3 startingPosition;
    [SerializeField] Transform shootPoint;
    TurretAiOnly tAI;
    void Start()
    {
        tAI = FindObjectOfType<TurretAiOnly>();
        rb = GetComponent<Rigidbody>();
        startingPosition = transform.position;
        gun = GetComponent<Gun>();
    }

    // Update is called once per frame

    void Update()
    {

        if (gameOver || GameManager.sharedInstance.uIManagerScript.IsPauseGame)
        {
            return;
        }

        if (Player)
        {
            chasePlayer = ConeDetection(Player.position);
            if (chasePlayer)
            {
                TurnAndShootPlayer();
            }
        }
    }

    void TurnAndShootPlayer()
    {
        Vector3 faceDir = new Vector3(Player.position.x, transform.position.y, Player.position.z);

        Vector3 dir = faceDir - transform.position;
        dir.Normalize();

        //float distance = Vector3.Distance(transform.position, Player.position);
        //float dot = Vector3.Dot(transform.forward, dir);
        Quaternion from = transform.rotation;
        Quaternion to = Quaternion.LookRotation(dir);

        transform.rotation = Quaternion.Lerp(from, to, Time.deltaTime * 10f);
        startTimer += Time.deltaTime;
        if (startTimer > shootTimer)
        {
            startTimer = 0;
            gun.ShootBullet(shootPoint.position, shootPoint.rotation);
        }
    }

    private void OnDrawGizmos()
    {
        GetComponent<SphereCollider>().radius = range;
        Gizmos.DrawWireSphere(transform.position, range);
    }
    public  Vector3 dirTowardsPlayer;
    public float angleOfPlayer;
    public float maxFov = 45f;
    public  bool chasePlayer;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player = other.gameObject.transform;
            //onPlayerInRange.Invoke(true);

        }
    }
    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        Player = other.gameObject.transform;
    //        chasePlayer = ConeDetection(Player.position);
    //    }
    //}
    bool ConeDetection(Vector3 target)
    {
        dirTowardsPlayer = (target - transform.position);

        angleOfPlayer = Mathf.Acos(Vector3.Dot(dirTowardsPlayer.normalized, transform.forward)) * Mathf.Rad2Deg;
        if(angleOfPlayer<maxFov)
        {
            onPlayerInRange.Invoke(true);
        }
        return angleOfPlayer < maxFov;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player = null;
            onPlayerInRange.Invoke(false);
        }
    }

    public void CheckGameOver()
    {
        gameOver = true;
        Player = null;
        transform.position = startingPosition;
        tAI.SetStopCondition(true);
    }
    public void Replay()
    {
        gameOver = false;
        transform.position = startingPosition;
        tAI.SetStopCondition(false);
    }
}
