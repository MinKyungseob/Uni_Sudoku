using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonSetting : MonoBehaviour
{
    public void ButtonClick(string setting)
    {
        if (setting.ToLower() == "easy")
        {
            Settings.difficulty = Settings.Difficulties.EASY;
        }
        if (setting.ToLower() == "medium")
        {
            Settings.difficulty = Settings.Difficulties.MEDIUM;
        }
        if (setting.ToLower() == "hard")
        {
            Settings.difficulty = Settings.Difficulties.HARD;
        }
        if (setting.ToLower() == "insane")
        {
            Settings.difficulty = Settings.Difficulties.INSANE;
        }

        SceneManager.LoadScene("GameScene");
    }
}
