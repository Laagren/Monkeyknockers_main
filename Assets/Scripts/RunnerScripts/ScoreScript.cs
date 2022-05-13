using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    private float score;

    [SerializeField]
    private Text scoreText;

    private R_MonkeyScript coin;

    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Coin")
        {
            score += 10;
            scoreText.text = score.ToString();
            coin = other.GetComponent<R_MonkeyScript>();        
        }
    }
}
