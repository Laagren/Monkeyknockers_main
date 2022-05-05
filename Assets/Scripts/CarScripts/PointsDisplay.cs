using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsDisplay : MonoBehaviour
{
    public static PointsDisplay pointsDisplayInstance;

    public int currentPoints = 0;
    public Text pointText;
    private void Awake()
    {
        pointsDisplayInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    public void AddPoint()
    {
        currentPoints += 100;
        pointText.text = "Points: " + currentPoints.ToString();
    }

}
