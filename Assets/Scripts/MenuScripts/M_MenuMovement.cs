using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class M_MenuMovement : MonoBehaviour
{
    private Vector3 velocity;
    private bool isGrounded;
    private float turnSmoothVelocity;
    private float standStill, walking, running, currentAnimationValue, distToGround;
    private float animationConst;

    public static string playerName;

    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform cam;
    [SerializeField] private Animator animator;

    [Header("Player settings")]
    
    [SerializeField] private float speed;
    [SerializeField] private float gravity;
    [SerializeField] private float jumpHeight;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance;
    [SerializeField] private LayerMask ground;
    [SerializeField] private float turnSmoothTime = 0.1f;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        currentAnimationValue = 0f;
        standStill = 0;

        walking = 0.33333f;
        running = 0.66666f;

        distToGround = 0.35f;
        Debug.Log(playerName);
    }

    private bool IsGrounded()
    {

        return Physics.Raycast(transform.position, Vector3.down, distToGround, 1 << LayerMask.NameToLayer("Ground"));
    }

    // Update is called once per frame
    void Update()
    {
            isGrounded = IsGrounded();
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = 12.0f;
                currentAnimationValue = Mathf.Lerp(currentAnimationValue, running, Time.deltaTime * animationConst);
                animator.SetFloat("Blend", currentAnimationValue);
            }

            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);

            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
            {
                if (!Input.GetKey(KeyCode.LeftShift) && isGrounded)
                {
                    animator.SetBool("Still", false);
                    speed = 6f;
                    currentAnimationValue = Mathf.Lerp(currentAnimationValue, walking, Time.deltaTime * animationConst);
                    animator.SetFloat("Blend", currentAnimationValue);
                }
            }

            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDir.normalized * speed * Time.deltaTime);
            }
            else
            {
                speed = 0f;
                if (currentAnimationValue <= 0.01f)
                {

                }
                currentAnimationValue = Mathf.Lerp(currentAnimationValue, standStill, Time.deltaTime * animationConst);

                animator.SetFloat("Blend", currentAnimationValue);
            }       
    }

    public void JumpFinished(string message)
    {
        if (message.Equals("JumpFinished"))
        {
            animator.SetBool("Still", false);
        }
    }

    public void StartJump(string message)
    {
        if (message.Equals("StartJump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "StartRunnerTrigger")
        {
            
            SceneManager.LoadScene("RunnerGame");
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        if (other.gameObject.tag == "StartDriveTrigger")
        {
            SceneManager.LoadScene("cargame2022_04_07");
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

        }
    }   
}