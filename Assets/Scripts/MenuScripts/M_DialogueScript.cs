using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M_DialogueScript : MonoBehaviour
{
    [SerializeField] Text nameText;
    [SerializeField] Text dialogueText;

    [SerializeField] GameObject dialogueGUI;
    [SerializeField] Transform dialogueBoxGUI;

    public float letterDelay = 0.1f;
    public float letterMultiplier = 0.5f;

    public KeyCode dialogueInput = KeyCode.F;

    public string Name;

    public string[] dialogLines;

    public bool letterIsMultiplied = false;
    public bool dialogueIsActive = false;
    public bool dialogueEnded = false;
    public bool outOfRange = true;




    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
