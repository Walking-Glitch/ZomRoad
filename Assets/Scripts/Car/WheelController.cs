using System.Collections;
using System.Collections.Generic;
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

    public AudioClip[] crashClips;

    private AudioSource audioSource;

    public FixedJoystick joystick;
    private Vector3 moveDirJoystick;

    public int maxHealth;
    public int health;

    public int maxExp;
    public int exp;

    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;
        health = maxHealth;
        exp = 0;
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        // get forward and reverse acceleration
        currentAcceleration = acceleration * Input.GetAxis("Vertical");
        //currentAcceleration = acceleration * joystick.Vertical;

        //
        if (Input.GetKey(KeyCode.Space))
            currentBrakeForce = brakingForce;
        
        else
            currentBrakeForce = 0f;

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

    public void TakeDamage(int damage)
    {
        health -= damage;
        gameManager.uiManager.SetHealth(health);

    }

    public void GainExp(int expReceived)
    {
        exp += expReceived;
        gameManager.uiManager.SetXp(exp);

    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            PlaySfx();
        }
    }


}
