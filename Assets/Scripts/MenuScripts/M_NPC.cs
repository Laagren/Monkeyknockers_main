using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class M_NPC : MonoBehaviour
{


    public Transform chatBackground;
    public Transform npcCharacter;

    private M_DialogueScript dialogueSystem;

    public string Name;

    [TextArea(5, 10)]
    public string[] sentences;

    // Start is called before the first frame update
    void Start()
    {
        dialogueSystem = FindObjectOfType<M_DialogueScript>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(npcCharacter.position);
        pos.y += 175;
        chatBackground.position = pos;
    }

    public void OnTriggerStay(Collider other)
    {
        this.gameObject.GetComponent<M_NPC>().enabled = true;
        FindObjectOfType<M_DialogueScript>().EnterRangeOfNPC();
        if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.F))
        {
            this.gameObject.GetComponent<M_NPC>().enabled = true;
            dialogueSystem.name = Name;
            dialogueSystem.dialogueLines = sentences;
            FindObjectOfType<M_DialogueScript>().npcName;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        FindObjectOfType<M_DialogueScript>().OutOfRange();
        this.gameObject.GetComponent<M_NPC>().enabled = false;
    }
}
