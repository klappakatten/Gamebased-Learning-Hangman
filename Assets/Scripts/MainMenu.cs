using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    public Text highscoreText;

    private void Start()
    {
        highscoreText.text ="Highscore: " +  PlayerPrefs.GetInt("highScore");
    }


}
