using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deactivate : MonoBehaviour
{
    public GameHandler gh;


    private void Update()
    {
        if (gh.currentLives <= 0)
        {
            gameObject.GetComponent<Button>().interactable = false;
        }
    }
}
