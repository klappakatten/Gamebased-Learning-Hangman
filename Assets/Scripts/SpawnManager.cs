using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    public GameObject plusText;
    public GameObject minusText;
    public GameObject powerUpText;
    public GameObject charText;
    private GameObject textT;
    public SoundManager sm;

    public void spawnText(int score)
    {
        if (score > 0)
        {
            plusText.GetComponentInChildren<Text>().text = "+" + score;
            textT = Instantiate(plusText, gameObject.transform.position, gameObject.transform.rotation);
        }
        else
        {
            textT = Instantiate(minusText, gameObject.transform.position, gameObject.transform.rotation);
        }
        textT.transform.SetParent(gameObject.transform);

    }

    public void SpawnText(char c, bool isCorrect)
    {
        StartCoroutine(Wait(c, isCorrect));
    }

    public void SpawnText(string txt)
    {
        StartCoroutine(Wait(txt));
    }

    IEnumerator Wait(string txt)
    {
        powerUpText.GetComponentInChildren<Text>().color = Color.yellow;
        powerUpText.GetComponentInChildren<Text>().text = txt;
        yield return new WaitForSeconds(0.2f);
        textT = Instantiate(powerUpText, gameObject.transform.position, gameObject.transform.rotation);
        textT.transform.SetParent(gameObject.transform);
    }

    IEnumerator Wait(char c, bool isCorrect)
    {
        Text txt = charText.GetComponentInChildren<Text>();

        if (isCorrect)
        {
            txt.color = Color.green;
        }
        else
        {
            txt.color = Color.red;
        }

        charText.GetComponentInChildren<Text>().text = c + "";
        yield return new WaitForSeconds(0.1f);
        textT = Instantiate(charText, gameObject.transform.position, gameObject.transform.rotation);
        textT.transform.SetParent(gameObject.transform);

    }
}
