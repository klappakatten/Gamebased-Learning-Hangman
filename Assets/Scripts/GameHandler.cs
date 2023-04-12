using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameHandler : MonoBehaviour
{
    private SpriteHandler sh;
    public SpawnManager sm;
    public SoundManager soundManager;

    public int wrongAnswers = 0;
    public int currentQuestion = 0;
    public int currentHint = 0;
    public int maxLives = 9;
    public int currentLives = 9;
    private int highScore;
    public float score = 0;
    public int questionAnsweredAmount = 0;
    private int totalQuestionAmount;

    public float multiplier = 0;

    public bool isGameActive = true;
    public bool isQuestionComplete = false;
    public bool isFirstQuestion = true;
    public bool noPoints = false;
    public bool doublePoints = false;

    private string correctAnswer;

    private char currentChar;
    private char[] charArray;

    public Text questionText;
    public Text hintTextOne;
    public Text hintTextTwo;
    public Text hintTextThree;
    public Text livesText;
    public Text scoreText;
    public Text highScoreText;
    public Text gameOverTextQAnswered;
    public Text gameOverHighScore;
    public Text gameOverScore;
    public TextMeshProUGUI answerText;

    public List<Questions> questions;
    public List<Button> buttonList;
    public List<Button> miscbuttonList;
    public List<char> realList;
    public List<char> unknownList;
    public List<int> randomNumberList;

    public Animator gameOverAnimator;

    private void Start()
    {
        sh = FindObjectOfType<SpriteHandler>();
        totalQuestionAmount = questions.Count;
        NextQuestion();
    }

    private void Update()
    {

        livesText.text = currentLives + "/9";
        if (currentLives <= 0)
        {
            GameOver();
        }


    }
    public void GameOver()
    {

        if (isGameActive) { soundManager.playSound(2); }
        isGameActive = false;
        DisableButtons();
        gameOverAnimator.SetTrigger("animate");
        gameOverTextQAnswered.text = "Questions Answered:" + questionAnsweredAmount + "/" + totalQuestionAmount;
        gameOverHighScore.text = "Highscore: " + PlayerPrefs.GetInt("highScore");
        gameOverScore.text = "Score: " + (int)score;

    }

    //kollar om rätt knapp har klickats på och hanterar liv vid rätt/fel svar
    public void setChar(char c)
    {
        if (isGameActive)
        {
            currentChar = char.ToUpper(c);
            if (correctAnswer.Contains(currentChar))
            {
                CompareString();
                soundManager.playSound(4);
            }
            else
            {
                soundManager.playSound(5);
                sh.ChangePic();
                currentLives--;
                sm.SpawnText(currentChar, false);
            }

        }

    }

    //Kollar och hanterar om rätt knapp har tryckts på, om listan inte innehåller _ så är den besvarad och nästa fråga laddas in
    public void CompareString()
    {
        string textDisplay = "";
        for (int i = 0; i < realList.Count; i++)
        {
            if (currentChar.Equals(char.ToUpper(realList[i])))
            {
                unknownList[i] = currentChar;
            }
            if (unknownList[i].Equals('9'))
            {
                textDisplay += "\n" + "";
            }
            else { textDisplay += unknownList[i]; }
        }
        answerText.text = textDisplay;

        if (!unknownList.Contains('_'))
        {
            questions.Remove(questions[currentQuestion]);
            NextQuestion();
        }
    }

    //har ordning på poäng och highscore
    public void CountScore()
    {
        /*float calcScore = 100;
        calcScore = 10 * multiplier;
        score += calcScore;
        multiplier += 1;*/
        float addscore = 10 * multiplier;
        if (doublePoints)
        {
            addscore *= 2;
            doublePoints = false;
        }
        score += addscore;
        scoreText.text = "Score: " + (int)score;
        if (!isFirstQuestion)
        {
            sm.spawnText((int)addscore);
            sm.SpawnText("Question Complete");
        }
        if (highScore < score)
        {
            PlayerPrefs.SetInt("highScore", (int)score);
        }
        highScoreText.text = "Highscore: " + highScore;
    }

    //visar ledtråd om spelaren har klickat på lös upp ledtrådknappen
    public void unlockHint()
    {
        currentHint++;
        sm.spawnText(-2);
        sm.SpawnText("Hint Unlocked");
        soundManager.playSound(6);

        if (currentHint == 1)
        {
            hintTextOne.text = questions[currentQuestion].clue1;
            hintTextOne.GetComponentInChildren<Button>().interactable = false;
            hintTextTwo.GetComponentInChildren<Button>().interactable = true;
            hintTextThree.GetComponentInChildren<Button>().interactable = false;
        }
        else if (currentHint == 2)
        {
            hintTextTwo.text = questions[currentQuestion].clue2;
            hintTextOne.GetComponentInChildren<Button>().interactable = false;
            hintTextTwo.GetComponentInChildren<Button>().interactable = false;
            hintTextThree.GetComponentInChildren<Button>().interactable = true;
        }
        else if (currentHint == 3)
        {
            hintTextThree.text = questions[currentQuestion].clue3;
            hintTextOne.GetComponentInChildren<Button>().interactable = false;
            hintTextTwo.GetComponentInChildren<Button>().interactable = false;
            hintTextThree.GetComponentInChildren<Button>().interactable = false;
        }
        multiplier -= 0.2f;
    }

    //kallar på alla nödvändiga funktioner för att ta fram nästa fråga
    public void NextQuestion()
    {
        highScore = PlayerPrefs.GetInt("highScore");
        if (!noPoints) { CountScore(); }
        if (!isFirstQuestion) { soundManager.playSound(3); }
        if (noPoints) { questions.Remove(questions[currentQuestion]); }
        isFirstQuestion = false;
        if (questions.Count != 0)
        {
            unknownList.Clear();
            realList.Clear();
            answerText.text = "";
            RandomizeQuestion();
            ResetButtons();
            questionAnsweredAmount++;
            questionText.text = "Question " + questionAnsweredAmount + "/" + totalQuestionAmount + "\n" + questions[currentQuestion].question;
            correctAnswer = questions[currentQuestion].correctAnswer.ToUpper();
            charArray = questions[currentQuestion].correctAnswer.ToCharArray(0, questions[currentQuestion].correctAnswer.Length);
            translateChar();

        }
        else
        {
            GameOver();
            answerText.text = "";
        }
    }

    //Återställer knapparna
    public void ResetButtons()
    {
        if (buttonList != null)
        {
            for (int i = 0; i < buttonList.Count; i++)
            {
                buttonList[i].interactable = true;
            }
        }
        hintTextOne.GetComponentInChildren<Button>().interactable = true;
        hintTextTwo.GetComponentInChildren<Button>().interactable = false;
        hintTextThree.GetComponentInChildren<Button>().interactable = false;
        hintTextOne.text = "Hint 1";
        hintTextTwo.text = "Hint 2";
        hintTextThree.text = "Hint 3";
        currentHint = 0;
        multiplier = 1;
    }

    //slumpar vilken fråga som ska ställas
    public void RandomizeQuestion()
    {
        if (questions.Count != 0)
        {
            currentQuestion = Random.Range(0, questions.Count - 1);
        }
        else
        {
            DisableButtons();
            isGameActive = false;
        }
    }

    //inaktiverar knapparna
    public void DisableButtons()
    {
        if (buttonList != null)
        {
            for (int i = 0; i < buttonList.Count; i++)
            {
                buttonList[i].interactable = false;
            }
        }
    }


    //översätter tecken till sträng av _ om 9 = radbrytning
    private void translateChar()
    {

        CreateLists();
        string textDisplay = "";
        for (int i = 0; i < unknownList.Count; i++)
        {
            if (unknownList[i].Equals('9'))
            {
                textDisplay += "\n" + "";
            }
            else { textDisplay += unknownList[i]; }

        }
        answerText.text = textDisplay;
    }

    //skapar listor för orden som delats in i array av stringen question, 9 representerar mellanslag
    public void CreateLists()
    {
        for (int i = 0; i < charArray.Length; i++)
        {
            if (!charArray[i].Equals(' ')
                )
            {
                realList.Add(charArray[i]);
                unknownList.Add('_');
            }
            else
            {
                realList.Add('9');
                unknownList.Add('9');
            }

        }
    }
    //visar x antal random tecken
    public void ShowCharPower()
    {
        int number;
        if (realList.Count < 4)
        {
            number = 1;
        }
        else if (realList.Count < 8)
        {
            number = 2;
        }
        else
        {
            number = 3;
        }
        for (int i = 0; i < number; i++)
        {
            int randomNumber = Random.Range(0, unknownList.Count);
            while (randomNumberList.Contains(randomNumber))
            {
                int counter = 0;
                counter++;
                randomNumber = Random.Range(0, unknownList.Count);
                if (counter > 10) { return; }
            }
            randomNumberList.Add(randomNumber);
            setChar(realList[randomNumber]);
        }
        randomNumberList.Clear();
    }
}
