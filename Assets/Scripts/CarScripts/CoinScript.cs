using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    private bool hit;
    public float speedZ, speedX, speedY, rotationSpeed;
    PointsDisplay pointsDisplay;
    void Start()
    {
    }

    void Update()
    {
        //M�ste f� s� aporna destroyas efter en viss tid n�r dem jittar iv�g
        if (hit)
        {
            transform.position = new Vector3(transform.position.x + speedX, transform.position.y + speedY, transform.position.z + speedZ);
            transform.Rotate(0, 90 * Time.deltaTime * rotationSpeed,  0);
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        
        if (other.name == "Car")
        {
            //Destroy(gameObject);
            hit = true;
            PointsDisplay.pointsDisplayInstance.AddPoint();
            FindObjectOfType<AudioManager>().Play("MonkeyDeath");
        }
    }
}
