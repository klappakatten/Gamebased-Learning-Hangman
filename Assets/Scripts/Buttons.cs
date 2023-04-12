using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Buttons : MonoBehaviour
{

    private Button btn;
    private GameHandler gh;
    private SpriteHandler sh;
    private TransitionHandler th;
    private SpawnManager sm;
    private SoundManager soundManager;
    public char charC;
    public bool addToList = true;



    private void Start()
    {
        gh = FindObjectOfType<GameHandler>();
        sh = FindObjectOfType<SpriteHandler>();
        th = FindObjectOfType<TransitionHandler>();
        btn = gameObject.GetComponent<Button>();
        charC = gameObject.GetComponentInChildren<Text>().text[0];
        sm = FindObjectOfType<SpawnManager>();
        soundManager = FindObjectOfType<SoundManager>();
        if (addToList)
        {
            gh.buttonList.Add(btn);
        }
    }

    //klickad bokstav
    public void charClick()
    {

        btn.interactable = false;
        gh.setChar(charC);
    }

    //klickad ledtråd
    public void unlockClue()
    {
        btn.interactable = false;
        gh.unlockHint();
    }

    //klicka play
    public void startGame()
    {
        th.LoadNextLevel();
    }

    public void ResetGame()
    {
        th.RestartLevel();
    }

    //dubbel poäng powerup knapptryck
    public void DoublePointsPowerup()
    {
        soundManager.playSound(1);
        sm.SpawnText("Double Points");
        btn.interactable = false;
        gh.doublePoints = true;
    }
    //skippa fråga powerup knapptryck
    public void SkipQuestionPowerup()
    {
        soundManager.playSound(1);
        sm.SpawnText("Skip Question");
        btn.interactable = false;
        gh.noPoints = true;
        gh.isFirstQuestion = true;
        gh.NextQuestion();
        gh.noPoints = false;
    }
    //visa random tecken powerup knapptryck
    public void ShowCharPowerup()
    {
        soundManager.playSound(1);
        sm.SpawnText("Show Random");
        btn.interactable = false;
        gh.ShowCharPower();
    }

    public void ResetPlayerprefs()
    {
        PlayerPrefs.SetInt("highScrore", 0);
    }

}
