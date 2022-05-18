using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class M_NPC : MonoBehaviour
{
    public Transform ChatBackGround;
    public Transform NPCCharacter;

    private M_DialogueScript dialogueSystem;

    public string Name;

    [TextArea(5, 10)]
    public string[] sentences;

    void Start()
    {
        dialogueSystem = FindObjectOfType<M_DialogueScript>();
    }

    void Update()
    {
        Vector3 Pos = Camera.main.WorldToScreenPoint(NPCCharacter.position);
        Pos.y += 175;
        ChatBackGround.position = Pos;
    }

    public void OnTriggerStay(Collider other)
    {
        this.gameObject.GetComponent<M_NPC>().enabled = true;
        FindObjectOfType<M_DialogueScript>().EnterRangeOfNPC();
        if ((other.gameObject.tag == "Player") && Input.GetKeyDown(KeyCode.F))
        {
            this.gameObject.GetComponent<M_NPC>().enabled = true;
            dialogueSystem.Names = Name;
            dialogueSystem.dialogueLines = sentences;
            FindObjectOfType<M_DialogueScript>().NPCName();
        }
    }

    public void OnTriggerExit()
    {
        FindObjectOfType<M_DialogueScript>().OutOfRange();
        this.gameObject.GetComponent<M_NPC>().enabled = false;
    }
}
