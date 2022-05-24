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
    private float idleTimer, controllerSaveHeight, controllerSlideHeight, controllerSaveCenterY, controllerSlideCenterY, distanceToFloor, runTimer, savePlayerYpos;
    private bool startRunning, canTurn, sliding, stopSideRun, spawnTile, gameActive;
    public bool onFloor;
    public static int lives;

    string playername = "adam";

    [Header("Level settings")]
    [SerializeField] private LayerMask floor;

    [SerializeField] private Text livesText;

    [SerializeField] private R_GameOverScript gameOverScript;

    [SerializeField] private M_HighScore highscore;

    [Header("Player settings")]
    [SerializeField] private float gravity;

    [SerializeField] private float sideSpeed;

    [SerializeField] private float runningSpeed;

    public int Lives { get { return lives; } }
    public bool SpawnTile { get { return spawnTile; } set { spawnTile = value; } }

    private enum soundState { idle, running, jumping, sliding }
    private soundState currentSoundState = soundState.idle;

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
        savePlayerYpos = transform.position.y;

        M_HighScore.highscoreFile = "RunnerHighscore.txt";
        M_HighScore.highscoreNamesFile = "RunnerHighscoreNames.txt";
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
                //HandleSound();
                HandleMovement();
            } 
        }
    }

    public void ChangeGameActiveStatus()
    {
        gameActive = !gameActive;
    }

    private void StartRunning()
    {
        if (gameActive)
        {
            if (idleTimer < 2)
            {
                idleTimer += Time.deltaTime;
            }
            else
            {
                startRunning = true;
                runTimer += Time.deltaTime;
                if (currentSoundState == soundState.idle)
                {
                    currentSoundState = soundState.running;
                    FindObjectOfType<C_AudioManager>().Play("RunningSound");
                }
            }

            if (runTimer > 15f && runningSpeed <= 30f)
            {
                runningSpeed += 1f;
                runTimer = 0;
            } 
        }
    }
    private void HandleInput()
    {
        //onFloor = Physics.CheckSphere(transform.position, distanceToFloor, floor);
        animator.SetBool("running", true);

        if (transform.position.y <= savePlayerYpos + 0.1f)
        {
            Debug.Log("onfloor");
            onFloor = true;
        }
        else
        {
            onFloor = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && onFloor && animator.GetBool("jumping") != true)
        {
            currentSoundState = soundState.jumping;
            animator.SetBool("jumping", true);
            jumpForce.y = Mathf.Sqrt(1.5f * -2f * gravity);           
            HandleSound();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            animator.SetBool("sliding", true);
            sliding = true;
            currentSoundState = soundState.sliding;
            HandleSound();
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

    private void HandleSound()
    {
        switch (currentSoundState)
        {
            case soundState.idle:
                FindObjectOfType<C_AudioManager>().Stop("JumpingSound");
                FindObjectOfType<C_AudioManager>().Stop("RunningSound");
                FindObjectOfType<C_AudioManager>().Stop("SlidingSound");
                break;
            case soundState.running:
                FindObjectOfType<C_AudioManager>().Stop("JumpingSound");
                FindObjectOfType<C_AudioManager>().Stop("SlidingSound");
                FindObjectOfType<C_AudioManager>().Play("RunningSound");
                break;
            case soundState.jumping:
                FindObjectOfType<C_AudioManager>().Play("JumpingSound");
                FindObjectOfType<C_AudioManager>().Stop("SlidingSound");
                FindObjectOfType<C_AudioManager>().Stop("RunningSound");              
                break;
            case soundState.sliding:
                FindObjectOfType<C_AudioManager>().Stop("JumpingSound");
                FindObjectOfType<C_AudioManager>().Stop("RunningSound");
                FindObjectOfType<C_AudioManager>().Play("SlidingSound");
                break;
            default:
                break;
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

    private void HandleDeath()
    {
        livesText.text = lives.ToString();
        animator.SetBool("dead", true);
        R_RemoveTileScript.gameActive = false;
        currentSoundState = soundState.idle;
        HandleSound();
        FindObjectOfType<C_AudioManager>().Stop("BackgroundMusic");
        gameOverScript.Setup(C_PointsDisplay.pointsDisplayInstance.currentPoints);
        //save name and score to file
        M_ReadFromFile.SaveHighscoreToFile(C_PointsDisplay.pointsDisplayInstance.currentPoints, playername);
        M_ReadFromFile.SaveNametoFile(playername);

        //highscore.Setup();
    }
    private void OnTriggerEnter(Collider other)
    {
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
        if (other.gameObject.tag == "WallTurnRight"/* && canTurn*/ && onFloor)
        {
            canTurn = false;
            lives--;

            if (lives >= 1 && onFloor)
            {
                transform.Rotate(0, 90, 0);
            }
            else if (lives <= 0 && onFloor)
            {
                HandleDeath();
            }

            livesText.text = lives.ToString();
        }
        //else if (other.gameObject.tag == "WallTurnRight" && onFloor == false)
        //{
        //    other.gameObject.SetActive(false);
        //    runningSpeed = 12f;
        //    animator.SetBool("JumpCrash", true);
        //}

        if (other.gameObject.tag == "WallTurnLeft"/* && canTurn*/ && onFloor)
        {
            canTurn = false;
            lives--;

            if (lives >= 1 && onFloor)
            {
                transform.Rotate(0, -90, 0);
            }
            else if (lives <= 0 && onFloor)
            {
                HandleDeath();
            }

            livesText.text = lives.ToString();
        }
        //else if (other.gameObject.tag == "WallTurnLeft" && onFloor == false)
        //{
        //    other.gameObject.SetActive(false);
        //    runningSpeed = 12f;
        //    animator.SetBool("JumpCrash", true);
        //}
    }

    private void OnControllerColliderHit(ControllerColliderHit other)
    {
        if (other.gameObject.tag == "JumpCrashTrigger" && onFloor == false)
        {
            other.gameObject.SetActive(false);
            runningSpeed = 12f;
            animator.SetBool("JumpCrash", true);

            //lives = 0;
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
            currentSoundState = soundState.running;
            HandleSound();
        }
    }

    public void SlideUp(string message)
    {
        if (message.Equals("SlideUp"))
        {
            sliding = false;
            currentSoundState = soundState.running;
            HandleSound();
        }
    }

    public void JumpCrashEnd(string message)
    {
        if (message.Equals("JumpCrashEnd"))
        {
            //animator.GetComponent<Animator>.enabled = false;
            gameObject.GetComponent<Animator>().enabled = false; 
            lives = 0;
            HandleDeath();
        }
    }

    //private void ( hit)
    //{
        
    ////}
    //private void OnControllerColliderHit(ControllerColliderHit other)
    //{
    //    //if (other.gameObject.tag == "CrashObject")
    //    //{
    //    //    lives = 0;         
    //    //    HandleDeath();
    //    //}
    //    //if (other.gameObject.tag == "JumpCrashTrigger")
    //    //{
    //    //    runningSpeed = 12f;
    //    //    animator.SetBool("JumpCrash", true);

    //    //    //lives = 0;
    //    //}

    //    if (other.gameObject.tag == "WallTurnRight"/* && canTurn*/ && transform.position.y <= savePlayerYpos)
    //    {
    //        lives--;

    //        if (lives >= 1 && onFloor)
    //        {
    //            transform.Rotate(0, 90, 0);
    //        }
    //        else if(lives <= 0 && onFloor)
    //        {
    //            HandleDeath();
    //        }

    //        livesText.text = lives.ToString();
    //    }
    //    else if(other.gameObject.tag == "WallTurnRight" && transform.position.y >= savePlayerYpos && onFloor != true)
    //    {
    //        other.gameObject.SetActive(false);
    //        runningSpeed = 12f;
    //        animator.SetBool("JumpCrash", true);
    //    }

    //    if (other.gameObject.tag == "WallTurnLeft"/* && canTurn*/ && transform.position.y <= savePlayerYpos)
    //    {
    //        lives--;

    //        if (lives >= 1 && onFloor)
    //        {
    //            transform.Rotate(0, -90, 0);
    //        }
    //        else if (lives <= 0 && onFloor)
    //        {
    //            HandleDeath();
    //        }

    //        livesText.text = lives.ToString();
    //    }
    //    else if (other.gameObject.tag == "WallTurnLeft" && transform.position.y >= savePlayerYpos && onFloor != true)
    //    {
    //        other.gameObject.SetActive(false);
    //        runningSpeed = 12f;
    //        animator.SetBool("JumpCrash", true);
    //    }
    //}
}
