using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class M_StartScreenScript : MonoBehaviour
{
    private bool status = true;
    [SerializeField] public TMP_InputField playerNameTxt;

    private void Start()
    {
        FindObjectOfType<C_AudioManager>().Play("GameOverSound");
    }

    public void ToggleStartScreen()
    {
        status = !status;
        gameObject.SetActive(status);
    }

    public void TurnOffStartScreen()
    {
        status = !status;
        gameObject.SetActive(status);
        FindObjectOfType<C_AudioManager>().Stop("GameOverSound");
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("Menu");
    }

    public void StartGame()
    {
        R_PlayerScript.playerName = playerNameTxt.text;
        C_CarController.playerName = playerNameTxt.text;
        M_MenuMovement.playerName = playerNameTxt.text;
        SceneManager.LoadScene("Menu");
    }
}
