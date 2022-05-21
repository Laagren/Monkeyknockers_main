using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_MonkeyScript : MonoBehaviour
{
    private bool hit;

    [SerializeField] private float speedX;

    [SerializeField] private float speedY;

    [SerializeField] private float speedZ;

    [SerializeField] private float rotationSpeed;

    void Update()
    {
        //När aporna blir träffade ska dem flygga iväg från banan.
        if (hit)
        {
            transform.position = new Vector3(transform.position.x + speedX, transform.position.y + speedY, transform.position.z + speedZ);
            transform.Rotate(0, 90 * Time.deltaTime * rotationSpeed,  0);
        }
        if (C_GasBarScript.gasInstance.currentGas <= 2)
        {
            Destroy(GetComponent<AudioSource>());
        }
        
    }

    public void OnTriggerEnter(Collider other)
    {      
        if (other.name == "Car")
        {
            hit = true;
            C_PointsDisplay.pointsDisplayInstance.AddPoint();
            FindObjectOfType<C_AudioManager>().Play("MonkeyDeath");
        }
    }
}
