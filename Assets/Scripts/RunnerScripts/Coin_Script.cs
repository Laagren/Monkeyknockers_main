using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin_Script : MonoBehaviour
{
    private float maxY, minY, removeTimer, speed;
    public float coinBackSpeed = 6f;
    public float coinLeftSpeed = 3f;
    private bool coinHit;
    private float score;
    private Transform savePos;
    public GameObject monkey;

    [SerializeField]
    private Text scoreText;

    [Header("Monkey settings")]
    [SerializeField]
    private float speedX;

    [SerializeField]
    private float speedY;

    [SerializeField]
    private float speedZ;

    [SerializeField]
    private float rotationYSpeed;

    [SerializeField]
    private float rotationXspeed;

    [SerializeField]
    private float rotationZspeed;

    // Start is called before the first frame update
    void Start()
    {
        maxY = transform.position.y + 0.3f;
        minY = transform.position.y;
        speed = -0.0004f;
        //obj = GetComponent<GameObject>();
        //player = GameObject.FindGameObjectWithTag("player");
        //savePos = player.GetComponent<Transform>();
    }

    // Update is called once per frame
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
        //if (coinHit)
        //{
        //    transform.position = new Vector3(transform.position.x + speedX, transform.position.y + speedY, transform.position.z + speedZ);
        //    transform.Rotate(0, 90 * Time.deltaTime * rotationSpeed, 0);
        //}
        //if (removeTimer > 0.8f)
        //{
        //    Destroy(monkey);
        //}
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            score += 10;
            //scoreText.text = score.ToString();
            coinHit = true;
        }
    }
}
