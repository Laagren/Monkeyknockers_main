using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class M_HighScore : MonoBehaviour
{
    private List<int> highscoreList;
    private List<string> nameList;

    public static string highscoreFile;
    public static string highscoreNamesFile;

    [SerializeField] private TextMeshProUGUI[] pointsText;
    [SerializeField] private bool status;

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
    }

    public void ToggleStatus()
    {
        status = !status;
        gameObject.SetActive(status);
    }

    /// <summary>
    /// Läser in från highscore fil och sparar ner dessa i en lista (List<string>) som sedan kan skrivas ut.
    /// </summary>
    public void ReadHighscore()
    {
        List<string> lines = new List<string>();
        StreamReader sr = new StreamReader(highscoreFile);
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
