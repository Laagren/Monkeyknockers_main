using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class M_HighScore : MonoBehaviour
{
    private List<int> highscoreList;
    private List<string> nameList;
    private bool status;

    public static string highscoreFile;
    public static string highscoreNamesFile;

    [SerializeField] private TextMeshProUGUI[] pointsText;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup()
    {
        ReadHighscore();
        ToggleStatus();
        for (int i = 0; i < nameList.Count; i++)
        {
            if (i <= 4)
            {
                pointsText[i].text = nameList[i] + " - " + highscoreList[i].ToString();
            }
        }
        //pointsText.text = nameList[0] + " - " + highscoreList[0].ToString();
        //StopJungleRunnerSound();
    }

    public void ToggleStatus()
    {
        status = !status;
        gameObject.SetActive(status);
    }

    public void ReadHighscore()
    {
        List<string> lines = new List<string>();
        StreamReader sr = new StreamReader(highscoreFile);
        // @"C:\Users\Simon\Desktop\Code\MonkeyKnockers-main\Monkeyknockers_main\Assets\RunnerAssets\JungleHighscore.txt"
        while (!sr.EndOfStream)
        {
            lines.Add(sr.ReadLine());
        }
        sr.Close();

        nameList = new List<string>();
        highscoreList = new List<int>();
        for (int i = 0; i < lines.Count; i++)
        {
            string[] scoreArray = lines[i].Split('-');
            highscoreList.Add(M_ReadFromFile.Convert_int(scoreArray[1]));
        }

        highscoreList.Sort();
        highscoreList.Reverse();
        for (int i = 0; i < highscoreList.Count; i++)
        {
            for (int j = 0; j < lines.Count; j++)
            {
                if (lines[j].Contains(highscoreList[i].ToString()))
                {
                    string[] nameArray = lines[j].Split('-');
                    nameList.Add(nameArray[0]);
                }
            }
        }

    }
}
