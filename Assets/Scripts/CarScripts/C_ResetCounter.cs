using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_ResetCounter : MonoBehaviour
{
    public static C_ResetCounter resetCounterInstance;
    private int currentResets = 3;

    [SerializeField] private Text resetText;

    
    private void Awake()
    {
        resetCounterInstance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SubReset()
    {
        currentResets -= 1;
        resetText.text = "Resets: " + currentResets.ToString();
    }
}
