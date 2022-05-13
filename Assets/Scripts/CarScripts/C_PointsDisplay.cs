using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_PointsDisplay : MonoBehaviour
{
    public int currentPoints = 0;

    public static C_PointsDisplay pointsDisplayInstance;

    [SerializeField] private Text pointText;

    private void Awake()
    {
        pointsDisplayInstance = this;
    }

    public void AddPoint()
    {
        currentPoints += 100;
        pointText.text = "Points: " + currentPoints.ToString();
    }
}
