using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class R_MonkeyScript : MonoBehaviour
{
    private float maxY, minY, removeTimer, speed;
    private bool coinHit;
    private Transform savePos;
    private AudioSource monkeySound;

    private C_AudioManager sound;

    [SerializeField] private GameObject monkey;

    [SerializeField] private Text scoreText;

    [SerializeField] private float speedX;

    [SerializeField] private float speedY;

    [SerializeField] private float speedZ;

    [SerializeField] private float rotationYSpeed;

    [SerializeField] private float rotationXspeed;

    [SerializeField] private float rotationZspeed;

    void Start()
    {
        maxY = transform.position.y + 0.3f;
        minY = transform.position.y;
        speed = -0.0004f;
        sound = GetComponent<C_AudioManager>();
        monkeySound = GetComponent<AudioSource>();
    }

    void Update()
    {
        savePos = GameObject.FindWithTag("Player").transform;

        if (transform.position.y <= minY)
        {
            speed *= -1;
        }
        else if (transform.position.y >= maxY)
        {
            speed *= -1;
        }
  
        if (coinHit)
        {
            transform.Rotate(rotationXspeed, rotationYSpeed, rotationZspeed);
            transform.position = new Vector3(transform.position.x, transform.position.y - speed, transform.position.z);
            transform.Translate(Vector3.left * speedX * Time.deltaTime, savePos);
            transform.Translate(Vector3.forward * speedZ * Time.deltaTime, savePos);
            transform.Translate(Vector3.up * speedY * Time.deltaTime, savePos); 
            removeTimer += Time.deltaTime;          
        }
        if (R_PlayerScript.lives == 0)
        {
            monkeySound.Stop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            C_PointsDisplay.pointsDisplayInstance.AddPoint();
            coinHit = true;
            FindObjectOfType<C_AudioManager>().Play("MonkeyDeath");
        }
    }
}
