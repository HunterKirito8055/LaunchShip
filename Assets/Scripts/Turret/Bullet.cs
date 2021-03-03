using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float playerDamageRate;
    [SerializeField] float enemyDamageRate;
    TurretE turretE;
    Rigidbody rb;
    private void Awake()
    {
        if (turretE)
        {
            enemyDamageRate = FindObjectOfType<EnemyHealth>().enemyDamageRate;
        }
        playerDamageRate = FindObjectOfType<PlayerHealth>().playerDamageRate;
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" )
        {
            other.GetComponentInParent<PlayerHealth>().TakeDamage(playerDamageRate); 
            CancelInvoke("Deactivate");
            Deactivate();
        }
        if( other.tag == "Enemy") 
        {
            other.GetComponentInParent<EnemyHealth>().EnemyTakeDamage(enemyDamageRate);
            CancelInvoke("Deactivate");
            Deactivate();
        }
      
    }
    private void OnEnable()
    {
        Invoke("Deactivate", 5f);
    }
    void Deactivate()
    {
        if (rb)
        {
            rb.Sleep();
        }
        gameObject.SetActive(false);
    }
}


