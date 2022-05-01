using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasObstacleScript : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        transform.Rotate(0, 90 * Time.deltaTime, 0);
    }

    public void OnTriggerEnter(Collider other)
    {

        if (other.name == "Car")
        {
            GasBarScript.gasInstance.FillUpGasMeter();
            Destroy(gameObject);
        }
    }
}
