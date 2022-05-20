using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class M_MonkeyDialogue : MonoBehaviour
{
    private bool chatTextActive;
    private bool fTextActive;
    private float writeTimer, dotActiveTimer, dotRemoveTimer, removeMaxValue, dotTimerMaxValue, writeTimeMaxValue;
    private int writeCounter, dotCounter;

    [SerializeField] private GameObject fText;
    [SerializeField] private GameObject chatText;
    [SerializeField] private TextMeshPro textboxContent;
    [SerializeField] private string monkeyText;
    [SerializeField] private GameObject[] dots;

    void Start()
    {
        removeMaxValue = 1.3f;
        dotTimerMaxValue = 0.3f;
        writeTimeMaxValue = 0.04f;
    }

    // Update is called once per frame
    void Update()
    {
        if (fTextActive && Input.GetKeyDown(KeyCode.F))
        {
            chatTextActive = !chatTextActive;
            fText.SetActive(false);
            chatText.SetActive(chatTextActive);
            textboxContent.text = "";
            writeCounter = 0;
        }

        if(chatText.activeInHierarchy)
        {
            dotActiveTimer += Time.deltaTime;
            dotRemoveTimer += Time.deltaTime;

            if (textboxContent.text.Length != monkeyText.Length)
            {
                writeTimer += Time.deltaTime;
                if (writeTimer >= writeTimeMaxValue)
                {
                    textboxContent.text += monkeyText[writeCounter];
                    writeTimer = 0;
                    writeCounter++;
                }
            }
            if (dotActiveTimer >= dotTimerMaxValue)
            {
                if (dotCounter <= 2)
                {
                    dots[dotCounter].SetActive(true);
                    dotCounter++;
                }
                else if (dotCounter >= 3 && dotRemoveTimer >= removeMaxValue)
                {
                    foreach (GameObject dot in dots)
                    {
                        dot.SetActive(false);
                    }
                    dotCounter = 0;
                    dotRemoveTimer = 0;
                }
             
                dotActiveTimer = 0;             
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            fText.SetActive(true);
            chatText.SetActive(false);
            fTextActive = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            textboxContent.text = "";
            dotActiveTimer = 0;
            dotRemoveTimer = 0;
            dotCounter = 0;
            fText.SetActive(false);
            chatText.SetActive(false);
            fTextActive = false;
            chatTextActive = false;
        }
    }
}
