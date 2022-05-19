using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class C_GameOverScript : MonoBehaviour
{
    private bool status;

    [SerializeField] private TextMeshProUGUI pointsText;
    
    private void Start()
    {
        FindObjectOfType<C_AudioManager>().Play("GameOverSound");
    }

    //Denna öppnar upp GameOver skärmen.
    public void Setup(int score)
    {
        ToggleStatus();
        pointsText.text = "Score: " + score.ToString();
        StopJungleDriveSound();
    }

    //Switchar mellan att vissa och gömma objektet.
    public void ToggleStatus()
    {
        status = !status;
        gameObject.SetActive(status);
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("cargame2022_04_07");
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("Menu");
    }

    private void StopJungleDriveSound() 
    {
        FindObjectOfType<C_AudioManager>().Stop("CarSoundBreak");
        FindObjectOfType<C_AudioManager>().Stop("BackgroundMusic");
        FindObjectOfType<C_AudioManager>().Stop("CarHorn");
        FindObjectOfType<C_AudioManager>().Stop("CarSound");
    }
}
