using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    
    public float playerDamageRate = 3f;
    private float health = 10f;
    [SerializeField]
    private float healthDecreaseRate = 1;
    [SerializeField]
    private float healthIncreaseRate = 1;

    [SerializeField]
    private float maxHealth = 10;

    private bool gameOver;
    bool isInHealth = false;
    string funcNameIReductionHealth = "IReductionHealth";
    string funcNameIChargingHealth = "IChargingHealth";
    [SerializeField] private HealthMode healthMode;
    public HealthMode HealthMode
    {
        get { return healthMode; }
        set
        {
            healthMode = value;
            switch (healthMode)
            {
                case HealthMode.none:
                    ReductionHealth(false);
                    ChargingHealth(false);
                    break;
                case HealthMode.gaining:
                    ReductionHealth(false);
                    ChargingHealth(true);
                    break;
                case HealthMode.reducing:
                    ReductionHealth(true);
                    ChargingHealth(false);
                    break;
                default:
                    break;
            }
        }
    }
    public float Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
            if (health > maxHealth)
            {
                health = maxHealth;
                HealthMode = HealthMode.none;
            }
            else if (health <= 0f)
            {
                health = 0;
                GameManager.sharedInstance.GameOver();
            }
            DisplayHealth(health / maxHealth);
        }
    }
    void Start()
    {
        Health = maxHealth;

    }


    public void PlayerDiedDueToHeight()
    {
        Health = 0;
    }

    void ParticleEffect()
    {
        Vector3 spawnPosition = transform.position + transform.forward;
        GameManager.sharedInstance.PlayEffect(spawnPosition);
    }
    public void DisplayHealth(float _health)
    {
        GameManager.sharedInstance.DisplayHealth(_health);
    }
    void AddScore(int _value)
    {
        GameManager.sharedInstance.AddScore(_value);
    }
    public void Play()
    {

        Health = maxHealth;
        HealthMode = HealthMode.reducing;

    }
    public void GameWin()
    {
        HealthMode = HealthMode.none;
    }
    public void GameOver()
    {
        healthMode = HealthMode.none;
        GameManager.sharedInstance.GameOver();

    }
    public void TakeDamage(float damage)
    {
        Health -= damage;
    }
    public void ReductionHealth(bool status)
    {
        StopCoroutine(funcNameIReductionHealth);
        if (status)
        {
            StartCoroutine(funcNameIReductionHealth);
        }
    }
    public void ChargingHealth(bool status)
    {
        StopCoroutine(funcNameIChargingHealth);
        if (status)
        {
            StartCoroutine(funcNameIChargingHealth);
        }
    }
    IEnumerator IReductionHealth()
    {
        while (Health > 0)
        {
            //print("red");
            Health -= Time.deltaTime * healthDecreaseRate;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
    IEnumerator IChargingHealth()
    {
        while (Health < 20f)
        {
            Health += Time.deltaTime * healthIncreaseRate;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

   

    [Range(0f, 40f)]
    public float slider;

    public AnimationCurve ac;

    public float fallHeightForAc, calculateDamage;
    private void Update()
    {
        if (fallHeightForAc != slider)
        {
            fallHeightForAc = slider;
            FallFromHeight(slider);
        }
    }
   public void FallFromHeight(float _fallHeight)
    {
        
        fallHeightForAc = Mathf.InverseLerp(3f, 25f, _fallHeight);
        calculateDamage = (maxHealth / 100) * ac.Evaluate(fallHeightForAc) * 100;
        Health -= calculateDamage;
    }
}
public enum HealthMode
{
    none,
    gaining,
    reducing
}