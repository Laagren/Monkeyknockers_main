using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    private float score;

    [SerializeField]
    private Text scoreText;

    private Coin_Script coin;

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
            //coin = other.GetComponent<scri>();
            coin = other.GetComponent<Coin_Script>();
            //coin.coinHit = true;
            //coin.Coin
            //other.GetComponents<CoinScript>();
            //Destroy(other.gameObject);
            //Debug.Log("scoreee");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

    }
}
