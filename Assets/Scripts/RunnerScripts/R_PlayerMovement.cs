using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class R_PlayerMovement : MonoBehaviour
{
    private Animator animator;
    private CharacterController characterController;
    private System.Random random = new System.Random();
    private Vector3 direction, jumpForce;
    private float idleTimer, controllerSaveHeight, controllerSlideHeight, controllerSaveCenterY, controllerSlideCenterY, distanceToFloor, runTimer;
    private bool startRunning, onFloor, canTurn, sliding, stopSideRun, spawnTile;
    public int lives;

    [Header("Level settings")]
    [SerializeField] private LayerMask floor;

    [SerializeField] private Text livesText;


    [Header("Player settings")]
    [SerializeField] private float gravity;

    [SerializeField] private float sideSpeed;

    [SerializeField] private float runningSpeed;

    public int Lives { get { return lives; } }
    public bool SpawnTile { get { return spawnTile; } set { spawnTile = value; } }

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        characterController = GetComponent<CharacterController>();

        controllerSaveHeight = characterController.height;
        controllerSlideHeight = 0.7f;
        controllerSaveCenterY = 0.85f;
        controllerSlideCenterY = 0.4f;
        distanceToFloor = 0.2f;
        lives = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (lives > 0)
        {
            StartRunning();

            if (startRunning)
            {
                HandleInput();
                HandleMovement();
            } 
        }
    }

    private void StartRunning()
    {
        if (idleTimer < 2)
        {
            idleTimer += Time.deltaTime;        
        }
        else
        {
            startRunning = true;
            runTimer += Time.deltaTime;
        }

        if (runTimer > 15f && runningSpeed <= 30f)
        {
            runningSpeed += 1f;
            runTimer = 0;
        }
    }
    private void HandleInput()
    {
        onFloor = Physics.CheckSphere(transform.position, distanceToFloor, floor);
        animator.SetBool("running", true);

        if (Input.GetKeyDown(KeyCode.Space) && onFloor && animator.GetBool("jumping") != true)
        {
            animator.SetBool("jumping", true);
            jumpForce.y = Mathf.Sqrt(1.5f * -2f * gravity);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            animator.SetBool("sliding", true);
            sliding = true;
        }

        if (canTurn)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                canTurn = false;
                transform.Rotate(0, 90, 0);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                canTurn = false;
                transform.Rotate(0, -90, 0);
            }
        }
    }

    private void HandleMovement()
    {
        if (sliding)
        {
            characterController.height = controllerSlideHeight;
            characterController.center = new Vector3(0, controllerSlideCenterY, 0);
        }
        else
        {
            characterController.height = controllerSaveHeight;
            characterController.center = new Vector3(0, controllerSaveCenterY, 0);
        }

        if (onFloor && jumpForce.y < 0)
        {
            jumpForce.y = -2f;
        }

        jumpForce.y += gravity * Time.deltaTime;
        if (!stopSideRun)
        {
            direction = new Vector3(Input.GetAxis("Horizontal") * sideSpeed, 0, runningSpeed);
        }
        direction = transform.TransformDirection(direction);
        characterController.Move(direction * Time.deltaTime);
        characterController.Move(jumpForce * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        // Kollar om spelaren sprungit in i en vägg (T-korsning) och svänger automatiskt.
        if (other.isTrigger && other.gameObject.tag == "WallBoth" && canTurn)
        {
            int i = random.Next(0, 2);
            if (i == 0) // Sväng höger
            {
                transform.Rotate(0, 90, 0);
            }
            else
            {
                transform.Rotate(0, -90, 0);
            }
            other.isTrigger = false;
        }

        // Kollar om spelaren klivit på en trigger som gör att den kan vända 90 grader.
        if (other.isTrigger && other.gameObject.tag == "turnPlatform")
        {
            canTurn = true;
        }

        if (other.gameObject.tag == "SpawnNewTile")
        {
            spawnTile = true;
        }
        if (other.gameObject.tag == "wall")
        {
            stopSideRun = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.isTrigger && other.gameObject.tag == "turnPlatform")
        {
            canTurn = false;
        }
        if (other.gameObject.tag == "wall")
        {
            stopSideRun = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {

    }
    public void SlideFinished(string message)
    {
        if (message.Equals("SlideFinished"))
        {
            animator.SetBool("sliding", false);
        }
    }

    public void JumpFinished(string message)
    {
        if (message.Equals("JumpFinished"))
        {
            animator.SetBool("jumping", false);
            animator.SetBool("sliding", false);
            
        }
    }

    public void SlideUp(string message)
    {
        if (message.Equals("SlideUp"))
        {
            sliding = false;
        }
    }
    private void OnControllerColliderHit(ControllerColliderHit other)
    {
        if (other.gameObject.tag == "CrashObject")
        {
            lives = 0;
            livesText.text = lives.ToString();
            animator.SetBool("dead", true);
            R_RemoveTileScript.gameActive = false;
        }

        if (other.gameObject.tag == "WallTurnRight" && canTurn)
        {
            lives--;

            if (lives >= 1)
            {
                transform.Rotate(0, 90, 0);
            }
            else
            {
                animator.SetBool("dead", true);
                R_RemoveTileScript.gameActive = false;
            }
                    
            livesText.text = lives.ToString();
        }

        if (other.gameObject.tag == "WallTurnLeft" && canTurn)
        {
            lives--;

            if (lives >= 1)
            {
                transform.Rotate(0, -90, 0);
            }
            else
            {
                animator.SetBool("dead", true);
                R_RemoveTileScript.gameActive = false;
            }

            livesText.text = lives.ToString();
        }
    }
}
