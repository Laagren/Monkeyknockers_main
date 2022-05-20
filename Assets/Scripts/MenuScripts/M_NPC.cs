using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class M_NPC : MonoBehaviour
{
    public Transform ChatBackGround;
    public Transform NPCCharacter;

    [SerializeField] private M_DialogueScript dialogueSystem;

    public string Name;

    [TextArea(5, 10)]
    public string[] sentences;

    void Start()
    {
        //dialogueSystem = FindObjectOfType<M_DialogueScript>();
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
        //FindObjectOfType<M_DialogueScript>().EnterRangeOfNPC();
        dialogueSystem.gameObject.SetActive(true);
        dialogueSystem.EnterRangeOfNPC();
        //FindObjectOfType<C_AudioManager>().Play("RunningSound");
        if ((other.gameObject.tag == "Player") && Input.GetKeyDown(KeyCode.F))
        {
            this.gameObject.GetComponent<M_NPC>().enabled = true;
            dialogueSystem.Names = Name;
            dialogueSystem.dialogueLines = sentences;
            dialogueSystem.NPCName();
        }
    }

    public void OnTriggerExit()
    {
        dialogueSystem.OutOfRange();
        dialogueSystem.gameObject.SetActive(false);
        this.gameObject.GetComponent<M_NPC>().enabled = false;
    }
}
