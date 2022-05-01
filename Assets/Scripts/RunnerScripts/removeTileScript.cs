using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class removeTileScript : MonoBehaviour
{
    private float removeTimer;
    private bool startTimer;
    private GameObject tile;
    void Start()
    {
        tile = transform.root.gameObject;
    }

    void Update()
    {
        if (startTimer)
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
