using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public ParticleSystem particle;
    public GameOverScript gameOverScript;
    
    private float horizontalInput;
    private float verticalInput;
    private float steerAngle;
    private bool isBreaking;
    //private bool isPassedWall;
    public float gas = 100f;
    private int resetLives = 3;

    public WheelCollider frontLeftWheelCollider;
    public WheelCollider frontRightWheelCollider;
    public WheelCollider rearLeftWheelCollider;
    public WheelCollider rearRightWheelCollider;
    public Transform frontLeftWheelTransform;
    public Transform frontRightWheelTransform;
    public Transform rearLeftWheelTransform;
    public Transform rearRightWheelTransform;
    public Transform carTransform;

    public float saveCarPos;
    public float maxSteeringAngle = 50f;
    public float motorForce = 500f;
    public float brakeForce = 0f;

    public Vector3 dustCommingFromFront = new Vector3(0f, 0f, 0f);
    public Vector3 dustCommingFromBack = new Vector3(180f, 0f, 0f);

    private void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
        StartParticle();
        EngineSound();
        ResetCarPos();
        GameOver();

        saveCarPos = transform.position.z-20;
    }

    public void GameOver()
    {
        if(GasBarScript.gasInstance.currentGas <= 2)
        {
            gameOverScript.Setup(PointsDisplay.pointsDisplayInstance.currentPoints);
        }
    }

    private void ResetCarPos() 
    {
        if (resetLives >= 1)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                carTransform.rotation = new Quaternion(0f, 0f, 0f, 0f);
                ResetCounter.resetCounterInstance.SubReset();
                resetLives--;
            }
        }
    }


    private void EngineSound() 
    {
        if (Input.GetKeyDown(KeyCode.W)) 
        {
            FindObjectOfType<AudioManager>().Play("CarSound");
        }
    
        else if (Input.GetKeyDown(KeyCode.Space)) 
        {
            FindObjectOfType<AudioManager>().Play("CarSoundBreak");
        }

        else if (Input.GetKeyDown(KeyCode.Q))
        {
            FindObjectOfType<AudioManager>().Play("CarHorn");
        }

    }

    
    private void StartParticle()
    {
        var shape = particle.shape;

        //KÖRA BILEN FRAMÅT
        if (Input.GetKeyDown(KeyCode.W))
        {
            
            //Så partikelsystemet 
            shape.rotation = dustCommingFromBack;  
            //Startar partikelsystemet när man trycker ner tangenten W
            particle.Play();
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            //Stoppar partikelsystemet när man släpper tangenten W
            //Dem partiklarna som finns kvar lever vidare i några få sekunder så allt inte försvinner när man släpper tangenten
            particle.Stop();
        }
        //KÖR BILEN BAKÅT
        if (Input.GetKeyDown(KeyCode.S))
        {
            particle.Play();
            shape.rotation = dustCommingFromFront;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            particle.Stop();
            //shape.rotation = changeRoatation;
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
            other.isTrigger = false;
        }
    }

}