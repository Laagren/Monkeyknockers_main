using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

static class M_ReadFromFile
{
    public static void SaveNametoFile(string name)
    {
        FileStream stream = File.Open(@"C:\Users\Simon\Desktop\Code\MonkeyKnockers-main\Monkeyknockers_main\Assets\RunnerAssets\JungleHighscoreNames.txt", FileMode.Append);
        StreamWriter writer = new StreamWriter(stream);
        writer.WriteLine(name);
        writer.Close();
    }

    public static void SaveHighscoreToFile(int score, string name)
    {
        FileStream stream = File.Open(@"C:\Users\Simon\Desktop\Code\MonkeyKnockers-main\Monkeyknockers_main\Assets\RunnerAssets\JungleHighscore.txt", FileMode.Append);
        StreamWriter writer = new StreamWriter(stream);
        string scoreString = name + "-" + score.ToString();
        writer.WriteLine(scoreString);
        writer.Close();
    }

    public static int Convert_int(string str)
    {
        int x;
        if (!int.TryParse(str, out x))
        {
           //("Could not convert" + str);
        }
        return x;
    }
}
