using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_CarController : MonoBehaviour
{   
    private float horizontalInput;
    private float verticalInput;
    private float steerAngle;
    private bool isBreaking;
    private int resetLives = 3;
    private int counter;
    enum soundState { Idle, Driving, Horn}
    soundState state=soundState.Idle;

    public float gas = 100f;
    public bool gameActive;

    [SerializeField] private ParticleSystem particle;
    [SerializeField] private C_GameOverScript gameOverScript;
    [SerializeField] private C_AudioManager audioManager;

    [SerializeField] public WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider;
    [SerializeField] private WheelCollider rearRightWheelCollider;
    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform;
    [SerializeField] private Transform rearRightWheelTransform;
    [SerializeField] private Transform carTransform;

    [SerializeField] private float saveCarPos;
    [SerializeField] private float maxSteeringAngle = 50f;
    [SerializeField] private float motorForce = 500f;
    [SerializeField] private float brakeForce = 0f;

    [SerializeField] private Vector3 dustCommingFromFront = new Vector3(0f, 0f, 0f);
    [SerializeField] private Vector3 dustCommingFromBack = new Vector3(180f, 0f, 0f);

    string playername = "Wilmer";

    private void Start()
    {
        particle = GetComponent<ParticleSystem>();
        M_HighScore.highscoreFile = "CarHighscore.txt";
        M_HighScore.highscoreNamesFile = "CarHighscoreNames.txt";
    }

    private void Update()
    {
        if (gameActive)
        {
            if(counter == 0) 
            { 
                ToggleSound();
                counter++;
            }
            GetInput();
            HandleMotor();
            HandleSteering();
            UpdateWheels();
            StartParticle();
            EngineSound();
            ResetCarPos();
            GameOver();

            saveCarPos = transform.position.z - 20; 
        }
    }

    public void ChangeGameStatus()
    {
        gameActive = !gameActive;
    }

    public void GameOver()
    {
        if(C_GasBarScript.gasInstance.currentGas <= 2)
        {
            gameOverScript.Setup(C_PointsDisplay.pointsDisplayInstance.currentPoints);
            C_LevelManager.gameOver = true;
            gameActive = false;
            M_ReadFromFile.SaveHighscoreToFile(C_PointsDisplay.pointsDisplayInstance.currentPoints, playername);
            M_ReadFromFile.SaveNametoFile(playername);
        }    
    }

    private void ResetCarPos() 
    {
        if (resetLives >= 1)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                carTransform.rotation = new Quaternion(0f, 0f, 0f, 0f);
                C_ResetCounter.resetCounterInstance.SubReset();
                resetLives--;
            }
        }
    }

    private void StartParticle()
    {
        var shape = particle.shape;

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {        
            shape.rotation = dustCommingFromBack;  
            particle.Play();
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            particle.Stop();
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            particle.Play();
            shape.rotation = dustCommingFromFront;
        }
        if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            particle.Stop();
        }
    }

    private void EngineSound()
    {
        if (Input.GetKeyDown(KeyCode.W) ||Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            state = soundState.Driving;
            ToggleSound();
        }

        if (Input.GetKey(KeyCode.W) == false && Input.GetKey(KeyCode.UpArrow) == false && Input.GetKey(KeyCode.S) == false && Input.GetKey(KeyCode.DownArrow) == false)
        {
            if (state != soundState.Idle) 
            {
                state = soundState.Idle;
                ToggleSound();
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            state = soundState.Horn;
            ToggleSound();
        }
    }

    private void ToggleSound() 
    {
        switch (state)
        {
            case soundState.Idle:
                FindObjectOfType<C_AudioManager>().Play("CarSoundBreak");
                FindObjectOfType<C_AudioManager>().Stop("CarSound");
                break;
            case soundState.Driving:
                FindObjectOfType<C_AudioManager>().Play("CarSound");
                FindObjectOfType<C_AudioManager>().Stop("CarSoundBreak");
                break;
            case soundState.Horn:
                FindObjectOfType<C_AudioManager>().Play("CarHorn");
                break;
            default:
                break;
        }
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        isBreaking = Input.GetKey(KeyCode.Space);
    }

    private void HandleSteering()
    {
        steerAngle = maxSteeringAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = steerAngle;
        frontRightWheelCollider.steerAngle = steerAngle;
    }

    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;

        brakeForce = isBreaking ? 3000f : 0f;
        frontLeftWheelCollider.brakeTorque = brakeForce;
        frontRightWheelCollider.brakeTorque = brakeForce;
        rearLeftWheelCollider.brakeTorque = brakeForce;
        rearRightWheelCollider.brakeTorque = brakeForce;
    }

    private void UpdateWheels()
    {
        UpdateWheelPos(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateWheelPos(frontRightWheelCollider, frontRightWheelTransform);
        UpdateWheelPos(rearLeftWheelCollider, rearLeftWheelTransform);
        UpdateWheelPos(rearRightWheelCollider, rearRightWheelTransform);
    }

    private void UpdateWheelPos(WheelCollider wheelCollider, Transform trans)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        trans.rotation = rot;
        trans.position = pos;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "invisWall") 
        {
            if(transform.position.z - 3f > other.transform.position.z )
            other.isTrigger = false;
        }
    }
}