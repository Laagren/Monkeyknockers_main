using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class R_GameOverScript : MonoBehaviour
{
    private bool status;

    [SerializeField] private TextMeshProUGUI pointsText;
    
    private void Start()
    {
        FindObjectOfType<C_AudioManager>().Play("GameOverSound");
    }

    public void Setup(int score)
    {
        ToggleGameoverScreen();
        pointsText.text = "Score: " + score.ToString();
        //StopJungleRunnerSound();
    }

    public void ToggleGameoverScreen()
    {
        status = !status;
        gameObject.SetActive(status);
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("RunnerGame");
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("Menu");
    }

    //private void StopJungleRunnerSound()
    //{
    //    FindObjectOfType<C_AudioManager>().Stop("RunningSound");
    //    FindObjectOfType<C_AudioManager>().Stop("BackgroundMusic");
    //    FindObjectOfType<C_AudioManager>().Stop("JumpingSound");
    //    FindObjectOfType<C_AudioManager>().Stop("SlidingSound");
    //}
}
