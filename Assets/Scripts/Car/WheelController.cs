using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    [SerializeField] private WheelCollider frontRight;
    [SerializeField] private WheelCollider frontLeft;
    [SerializeField] private WheelCollider backRight;
    [SerializeField] private WheelCollider backLeft;

    [SerializeField] private Transform frontRightTransform;
    [SerializeField] private Transform frontLeftTransform;
    [SerializeField] private Transform backRightTransform;
    [SerializeField] private Transform backLeftTransform;

    public float acceleration = 500f;
    public float brakingForce = 300f;
    public float maxTurnAngle = 15f;

    private float currentAcceleration = 0f;
    private float currentBrakeForce = 0f;
    private float currentTurnAngle = 0f;

    private bool isBraking;

    public AudioClip[] crashClips;
    public AudioClip[] engineClips;

    private AudioSource audioSource;
    public AudioSource engineAudioSource;

    public FixedJoystick joystick;
    private Vector3 moveDirJoystick;

    public int maxHealth;
    public int health;

    public int maxExp;
    public int exp;

    public bool IsGrounded { get; set; }
    public bool IsInvincible { get; set; }

    public MeshCollider CarMeshCollider;

    public Rigidbody CarRigidbody;

    public int slugAmmo = 0;
    public int energyAmmo = 0;
    public int bulletAmmo = 0;

    public bool isRecDis;
    public float distance = 0;
    private Vector3 previousPos;
    

    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;
        health = maxHealth;
        exp = 0;
       // distance = gameObject.transform.position.magnitude;
        audioSource = GetComponent<AudioSource>();
        CarMeshCollider = GetComponent<MeshCollider>();
        CarRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // get forward and reverse acceleration
        currentAcceleration = acceleration * Input.GetAxis("Vertical");
        //currentAcceleration = acceleration * joystick.Vertical;

        //
        if (Input.GetKey(KeyCode.Space))
        {
            currentBrakeForce = brakingForce;
            isBraking = true;

        }

        else
        {
            currentBrakeForce = 0f;
            isBraking = false;
        }
         

        //add acceleration to front wheels
        frontRight.motorTorque = currentAcceleration;
        frontLeft.motorTorque = currentAcceleration;
        backRight.motorTorque = currentAcceleration;
        backLeft.motorTorque = currentAcceleration;

        // brake all wheels
        frontRight.brakeTorque = currentBrakeForce;
        frontLeft.brakeTorque = currentBrakeForce;
        backRight.brakeTorque = currentBrakeForce;
        backLeft.brakeTorque = currentBrakeForce;

        // steering
        currentTurnAngle = maxTurnAngle * Input.GetAxis("Horizontal");
        //currentTurnAngle = maxTurnAngle * joystick.Horizontal;
        frontRight.steerAngle = currentTurnAngle;
        frontLeft.steerAngle = currentTurnAngle;

        UpdateWheel(frontRight, frontRightTransform);
        UpdateWheel(frontLeft, frontLeftTransform);
        UpdateWheel(backLeft, backLeftTransform);
        UpdateWheel(backRight, backRightTransform);

        if (isRecDis)
        {
            CalculateDistance();
        }

        PlayEngineSfx(CarRigidbody.velocity.magnitude, isBraking);

        //Debug.Log(CarRigidbody.velocity.magnitude);
        //Debug.Log(engineAudioSource.isPlaying);
    }

    void UpdateWheel(WheelCollider col, Transform transform)
    {
        Vector3 position;
        Quaternion rotation;

        col.GetWorldPose(out position, out rotation);

        transform.position = position;
        transform.rotation = rotation;
    }

    public void PlaySfx()
    {
        audioSource.clip = crashClips[Random.Range(0, crashClips.Length)];
        audioSource.Play();
    }

    public void PlayEngineSfx(float currAcceleration, bool isBraking)
    {

     
        if (isBraking && currAcceleration > 1 && IsGrounded)
        {
            engineAudioSource.clip = engineClips[3];
        }

        else if (currAcceleration < 3)
        {
            engineAudioSource.clip = engineClips[1];
        }
        else if (currAcceleration > 8)
        {
            engineAudioSource.clip = engineClips[2];
        }

        if (!engineAudioSource.isPlaying) engineAudioSource.Play();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        health = Mathf.Clamp(health, 0, maxHealth);
        gameManager.uiManager.SetHealth(health);
    }

    public void Heal(int heal)
    {
        health += heal;
        health = Mathf.Clamp(health, 0, maxHealth);
        gameManager.uiManager.SetHealth(health);
    }

    public void GainExp(int expReceived)
    {
        exp += expReceived;
        gameManager.uiManager.SetXp(exp);

        if (exp >= maxExp)
        {
            IncreaseDifficulty();
        }
    }

    public void SpendSlugAmmo(int amount)
    {
        slugAmmo -= amount;
        slugAmmo = Mathf.Clamp(slugAmmo, 0, 500);
        gameManager.uiManager.SetSlugAmmoText();
    }

    public void SpendBulletAmmo(int amount)
    {
        bulletAmmo -= amount;
        bulletAmmo = Mathf.Clamp(bulletAmmo, 0, 500);
        gameManager.uiManager.SetBulletAmmoText();
    }
    public void SpendEnergyAmmo(int amount)
    {
        energyAmmo -= amount;
        energyAmmo = Mathf.Clamp(energyAmmo, 0, 500);
        gameManager.uiManager.SetEnergyAmmoText();
    }

    public void CollectSlugAmmo(int amount)
    {
        slugAmmo += amount;
        gameManager.uiManager.SetSlugAmmoText();
    }

    public void CollectBulletAmmo(int amount)
    {
        bulletAmmo += amount;
        gameManager.uiManager.SetBulletAmmoText();
    }

    public void CollectEnergyAmmo(int amount)
    {
        energyAmmo += amount;
        gameManager.uiManager.SetEnergyAmmoText();
    }

    public void SetIsGrounded(bool isGrounded)
    {
        IsGrounded = isGrounded;
    }

    public void SetIsInvincible(bool isInvincible)
    {
        IsInvincible = isInvincible;
    }
    private void IncreaseDifficulty()
    {
        maxExp = (int) (maxExp * 1.5);
        exp = 0;
        gameManager.uiManager.SetXp(exp);
        gameManager.uiManager.SetMaxXp(maxExp);
        gameManager.enemyManager.maxEnemy += 1;
    }

    public void CalculateDistance()
    {
        distance += Vector2.Distance(transform.position, previousPos);
        previousPos = transform.position;

        //Debug.Log(distance);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            PlaySfx();
            if (IsInvincible)
            { 
                collision.gameObject.GetComponent<UndeadBase>().TakeDamage(500, new Vector3(25, 5, -50), false, -500f);
            }
            
        }
    }
 



}
