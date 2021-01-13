using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Player : MonoBehaviour
{
    public PlayerHealth healthScript;
    Gun gunScript;
    private Rigidbody rb;
    [SerializeField]
    Vector3 playerStartPosition;
    public float rotationSpeed = 5f;
    public LayerMask layerMask;
    public PowerType currentPoweruptype;
    public bool isdragged;

    #region DataTypes
    [SerializeField]
    private float speed = 20f, jumpForce = 5f;
    private float powerMaxTimer = 3f;
    private float currentPowerTimer = 0f;
    [Range(0f, 1f)]
    float powerUpActivetimer;
    private float maxHealth = 10;
    private int scoreValue = 1;
    private bool gameOver;
    bool isInHealth = false;
    #endregion

    #region Delegates
    public UnityAction TimerStart;
    public UnityAction TimerEnd;
    #endregion

    #region Properties
    public float PowerUpActivetimer
    {
        get { return powerUpActivetimer; }
        set
        {
            powerUpActivetimer = value;
            // powerUpActivetimer = currentPowerTimer / powerMaxTimer;
            PowerUpFill(powerUpActivetimer, currentPowerTimer);
            if (powerUpActivetimer < 0)
            {
                powerUpActivetimer = 0;
            }


        }
    }
    public PowerType CurrentPowerUpType
    {
        get
        {
            return currentPoweruptype;
        }
        set
        {
            currentPoweruptype = value;

            if (currentPoweruptype != PowerType.None)
            {
                ActivatePowerUp();
            }
            else if (currentPoweruptype == PowerType.None)
            {
                currentPowerTimer = 0;
                PowerUpName("None");
                PowerUpFill(0f, 0f);
            }
        }
    }


    public bool GameOver
    {
        get { return gameOver; }
        set
        {
            gameOver = value;
            if(healthScript.Health <= 0)
            {
                
                rb.useGravity = false;
                rb.Sleep();
                gameOver = true;
            }
            if (gameOver)
            {
                GameManager.sharedInstance.GameOver();
                CurrentPowerUpType = PowerType.None;
                healthScript.GameOver();
            }
        }
    }

    #endregion

    #region UnityEvent Functions
    private void Awake()
    {
        gunScript = FindObjectOfType<Gun>();
        healthScript = GetComponent<PlayerHealth>();
    }
    void Start()
    {

        rb = GetComponent<Rigidbody>();
        playerStartPosition = transform.position;
        CurrentPowerUpType = PowerType.None;
    }
    void Update()
    {
        if (GameOver)
        {
            return;
        }
        Move(GetInput());
        Rotation(GetInput());
        Jump();
      if (!GameManager.sharedInstance.uIManagerScript.IsPauseGame)
            ShootFun();
        CheckPlayerisFalling();
    }
    #endregion
    void ShootFun()
    {
        if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.B))
        {
            gunScript.ShootBullet();
        }
    }
    #region PlayerMovements

    Vector3 GetInput()
    {
        float h, v;
        
        if (isdragged)
        {
            h = JoyStick.move.x;
            v = JoyStick.move.z;
        }
        else
        {
            h = Input.GetAxis("Horizontal");
            v = Input.GetAxis("Vertical");
        }

        //_input = new Vector3(joystick.move.x, 0, joystick.move.y);
        return new Vector3(h, 0, v);
    }
    void Move(Vector3 move)
    {
        transform.position += transform.forward * move.magnitude * Time.deltaTime * speed;
  
    }
    void Rotation(Vector3 _rotate)
    {
        if (_rotate.magnitude > 0)
        {
            Quaternion from = transform.rotation;
            Quaternion to = Quaternion.LookRotation(_rotate);
            transform.rotation = Quaternion.Lerp(from, to, rotationSpeed * Time.deltaTime);
        }

    }
    void Jump()
    {

        bool canJump = Physics.Raycast(transform.position, -transform.up, 0.6f, layerMask);
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
           // print("jumped");
        }
    }
    public void GameInputs(bool _drag)
    {
        isdragged = _drag;
    }

    #endregion

    #region TriggerEvents
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Health")
        {
            if (CurrentPowerUpType == PowerType.None)
                healthScript.HealthMode = HealthMode.gaining;
        }

        if (other.CompareTag("Coins"))
        {

            SoundPlay("coin");
            ReduceCoinCount();
            AddScore(scoreValue);
            ParticleEffect();
            other.gameObject.SetActive(false);
            return;
        }

        if (other.tag == "PowerUp")
        {
            if (CurrentPowerUpType == PowerType.None)
            {
                PowerUp powerup = other.gameObject.GetComponent<PowerUp>();
                powerMaxTimer = powerup.activePowerupTime;
                CurrentPowerUpType = powerup.powerupname;
                other.gameObject.SetActive(false);
            }


        }
    }
    private void OnTriggerExit(Collider other)
    {
        healthScript.HealthMode = HealthMode.reducing;
    }
    #endregion

    #region PowerUpINfo
    public void ActivatePowerUp()
    {
        switch (CurrentPowerUpType)
        {

            case PowerType.CoinDouble:
                TimerStart = () =>
                {
                    DoubleCoinsStarted();
                };
                TimerEnd = () => {
                    DoubleCoinsEnd();
                };
                break;
            case PowerType.Immune:
                TimerStart = () =>
                {
                    ImmunePowerStart();
                };
                TimerEnd = () => {
                    ImmunePowerEnd();
                };
                break;
            case PowerType.AntiGravity:
                TimerStart = () =>
                {
                    AntiGravity_Start();
                };
                TimerEnd = () => {
                    AntiGravity_End();
                };
                break;
            default:
                break;
        }
        StartCoroutine(PowerUpStarted());
    }

    public void AntiGravity_Start()
    {
        rb.useGravity = false;
        transform.position += Vector3.up * 2f;
        PowerUpName("AntiGravity");
    }
    void AntiGravity_End()
    {
        rb.useGravity = true;
    }

    IEnumerator PowerUpStarted()
    {
        TimerStart?.Invoke();
        currentPowerTimer = powerMaxTimer;
        while (currentPowerTimer > 0)
        {
            PowerUpActivetimer = currentPowerTimer / powerMaxTimer;
            currentPowerTimer -= Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        TimerEnd?.Invoke();
        PowerUpEnd(); //
    }

    public void PowerUpEnd()
    {
        CurrentPowerUpType = PowerType.None;
        PowerUpName("None");
    }
    void ImmunePowerStart()
    {

        PowerUpName("Immunity");
        healthScript.HealthMode = HealthMode.none;
        PowerUpActivetimer = currentPowerTimer / powerMaxTimer;
    }

    void ImmunePowerEnd()
    {
        healthScript.HealthMode = HealthMode.reducing;

    }
    void DoubleCoinsStarted()
    {
        PowerUpName("TenxCoins");
        scoreValue = 10;
    }
    void DoubleCoinsEnd()
    {
        scoreValue = 1;
    }
    void PowerUpFill(float _powerup, float _powerUpTimerText)
    {
        GameManager.sharedInstance.PowerUpFill(_powerup, _powerUpTimerText);
    }
    void PowerUpName(string _powername)
    {
        GameManager.sharedInstance.PowerUpName(_powername);
    }
    void ParticleEffect()
    {
        Vector3 spawnPosition = transform.position + transform.forward;
        GameManager.sharedInstance.PlayEffect(spawnPosition);
    }
    #endregion

    
    void DisplayHealth(float _health)
    {
        healthScript.DisplayHealth(_health);
    }
    
    


    #region Coins
    void ResetScore()
    {
      //  GameManager.instance.ResetScore();
    }
    void AddScore(int _value)
    {
        GameManager.sharedInstance.AddScore(_value);
    }
    private void ReduceCoinCount()
    {
        GameManager.sharedInstance.ReduceChildCount();

    }
    #endregion

    public void Play()
    {
        //ResetScore();
        GameOver = false;
        healthScript.Play();
        CurrentPowerUpType = PowerType.None;
    }
    public void Restart()
    {
        Play();
        transform.position = playerStartPosition;
        rb.useGravity = true;
        GameOver = false;
    }
    void SoundPlay(string _soundname)
    {
        GameManager.sharedInstance.SoundPlay(_soundname);
    }
    public void GameWin()
    {
        healthScript.GameWin();
    }

    [Header("fallheight prototype")]
    [Space(20)]

    public float airTime;
    public float fallHeight;
    public bool isFalling;
    public bool IsFalling
    {
        get
        {
            return isFalling;
        }
        set
        {
            isFalling = value;

            if (isFalling && rb.velocity.y < 0f)
            {
                airTime += Time.deltaTime;
                fallHeight = -(airTime * airTime * Physics.gravity.y) / 2f;
                if(fallHeight > 3f)
                {
                    FallFromHeight(fallHeight);
                }
                
                if (airTime > 1.5)
                {
                    //FallFromHeight(0);
                    rb.Sleep();
                }
            }
            //if (isFalling && rb.velocity.y < 0f)
            //{
            //    airTime += Time.deltaTime;
            //    if(airTime > 1.5)
            //    {
            //        //FallFromHeight(0);
            //        rb.Sleep();
            //    }
            //}
            else
            {
                airTime = 0;
            }
        }
    }
    
    void CheckPlayerisFalling()
    {
        IsFalling = !Physics.Raycast(transform.position, -transform.up, 0.6f, layerMask);
    }
    void FallFromHeight(float _height)
    {
        healthScript.FallFromHeight(_height);
    }
}
