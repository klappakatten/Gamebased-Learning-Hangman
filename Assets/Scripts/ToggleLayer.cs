using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleLayer : MonoBehaviour
{
    public GameObject one;
    public GameObject two;
    public SoundManager sm;

    public void ToggleObject()
    {
        if (one.activeInHierarchy == false)
        {
            one.SetActive(true);
            two.SetActive(false);
        }
        else
        {
            one.SetActive(false);
            two.SetActive(true);
        }
        sm.playSound(0);

    }


}
