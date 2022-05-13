using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_GasObstacleScript : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(0, 90 * Time.deltaTime, 0);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.name == "Car")
        {
            C_GasBarScript.gasInstance.FillUpGasMeter();
            Destroy(gameObject);
        }
    }
}
