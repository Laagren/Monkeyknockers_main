using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class removeTileScript : MonoBehaviour
{
    private float removeTimer;
    private bool startTimer;
    private GameObject tile;

    public static bool gameActive;


    void Start()
    {
        tile = transform.root.gameObject;
        gameActive = true;
    }

    void Update()
    {
        if (startTimer && gameActive)
        {
            removeTimer += Time.deltaTime;
            if (removeTimer > 4f)
            {
                Destroy(tile);
            }
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            startTimer = true;
        }
    }
}
