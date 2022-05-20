using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_MonkeyDialogue : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject fText;
    [SerializeField] private GameObject chatText;

    public bool chatTextActive;
    public bool fTextActive;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fTextActive && Input.GetKeyDown(KeyCode.F))
        {
            chatTextActive = !chatTextActive;
            fText.SetActive(false);
            chatText.SetActive(chatTextActive);
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
            fText.SetActive(false);
            chatText.SetActive(false);
            fTextActive = false;
            chatTextActive = false;
        }
    }
}
