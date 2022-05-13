using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_GasBarScript : MonoBehaviour
{
    private Image gasBar; 
    private C_CarController carController;
    private C_GasBarScript gasBarScript;
    private float timer;
    private float speed;
    private float maxGas = 100f;

    public float currentGas;
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
        speed = carController.frontLeftWheelCollider.motorTorque;
        if (speed > 0)
        {
            timer += 1.7f * Time.deltaTime;
        }
        else
        {
            timer += 10 * Time.deltaTime;
        }
        currentGas = carController.gas - timer;
        gasBar.fillAmount = currentGas / maxGas;
    }

    public void FillUpGasMeter() 
    {
        carController.gas += 10f;
    }   
}
