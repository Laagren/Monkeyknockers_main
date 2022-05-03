using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetCounter : MonoBehaviour
{
    public int currentResets = 3;
    public Text resetText;

    // Start is called before the first frame update
    public static ResetCounter resetCounterInstance;

    
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
