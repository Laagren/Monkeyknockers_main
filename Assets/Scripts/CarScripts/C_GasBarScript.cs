using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_GasBarScript : MonoBehaviour
{
    private Image gasBar; 
    private C_CarController carController;
    private C_GasBarScript gasBarScript;
    private float gasAccelerate = 0.013f;
    private float gasIdle = 0.009f;
    private float maxGas = 100f;
    public float currentGas = 100f;
    public float fill;
    private float speed;
    public static C_GasBarScript gasInstance;

    private void Awake()
    {
        gasInstance = this;
    }

    void Start()
    {
        gasBar = GetComponent<Image>();
        carController = FindObjectOfType<C_CarController>();
        gasBarScript = FindObjectOfType<C_GasBarScript>();
        
    }
  
    void Update()
    {
        if (carController.gameActive)
        {
            speed = carController.frontLeftWheelCollider.motorTorque;

            if (speed != 0)
            {
                currentGas -= gasAccelerate;
            }
            else
            {
                currentGas -= gasIdle;
            }
            gasBar.fillAmount = currentGas / maxGas;
            fill = gasBar.fillAmount;   
        }
    }

    public void FillUpGasMeter() 
    {
        currentGas += 10;
    }   
}
