using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GasBarScript : MonoBehaviour
{
    public static GasBarScript gasInstance;

    private Image gasBar;
    public float currentGas;
    private float maxGas = 100f;
    CarController carController;
    GasBarScript gasBarScript;
    private float timer;
    private float speed;


    private void Awake()
    {
        gasInstance = this;
    }



    void Start()
    {
        gasBar = GetComponent<Image>();
        carController = FindObjectOfType<CarController>();
        gasBarScript = FindObjectOfType<GasBarScript>();
    }

    
    void Update()
    {
        speed = carController.frontLeftWheelCollider.motorTorque;
        if (speed > 0)
        {
            timer += 1.7f * Time.deltaTime;
        }
        else
        {
            timer += 1 * Time.deltaTime;
        }
        currentGas = carController.gas - timer;
        gasBar.fillAmount = currentGas / maxGas;
    }

    public void FillUpGasMeter() 
    {
        carController.gas += 10f;
    }

    
}
