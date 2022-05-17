using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class M_StartScreenScript : MonoBehaviour
{
    private bool status = true;

    private void Start()
    {
        FindObjectOfType<C_AudioManager>().Play("GameOverSound");
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
}
