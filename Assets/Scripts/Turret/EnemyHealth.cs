using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float enemyDamageRate = 5f;
    //TurretE turretE;
    [SerializeField] Slider healthBar;
    Image fillImage;
    [SerializeField ] GameObject parentObj;
public float maxHealth = 100f;
public float currentHealth;
    public float CurrentHealth {
        get { return currentHealth; }
    set
        {
            currentHealth = value;
            if (currentHealth >= maxHealth)
            {
                currentHealth = maxHealth;
            }
            else if(currentHealth<=0)
            {
                currentHealth = 0f;
                parentObj.SetActive(false);
            }
            //healthBar.value = currentHealth / maxHealth;       
        }
    }

    private void Awake()
    {
        //turretE = GetComponent<TurretE>();
        parentObj = GetComponent<TurretE>().transform.gameObject;
        fillImage = healthBar.fillRect.GetComponent<Image>();
    }
   
    private void Start()
    {
        CurrentHealth = maxHealth;
    }
    public void EnemyTakeDamage(float amount)
    {
        if (CurrentHealth > 0)
        {
            CurrentHealth -= amount;
            healthBar.value = CurrentHealth / maxHealth;
            fillImage.color = GameManager.sharedInstance.GetHealthGradient(healthBar.value);

            //print(healthBar.value);
        } 
    }
    public void Replay()
    {
        CurrentHealth = maxHealth;
        if(parentObj)
        parentObj.SetActive(true);
    }
}
