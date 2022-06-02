using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_IntroScript : MonoBehaviour
{
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 2.5f)
        {
            gameObject.SetActive(false);
        }
    }
}
