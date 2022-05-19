using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_GasObstacleScript : MonoBehaviour
{
    void Update()
    {
        //Så att gastanken roteras.
        transform.Rotate(0, 90 * Time.deltaTime, 0);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.name == "Car")
        {
            if(C_GasBarScript.gasInstance.currentGas < 100) 
            {
                C_GasBarScript.gasInstance.FillUpGasMeter();
            }
            Destroy(gameObject);
            FindObjectOfType<C_AudioManager>().Play("GasPickUpSound");
        }
    }
}
