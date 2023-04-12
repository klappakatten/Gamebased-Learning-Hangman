using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SpriteHandler : MonoBehaviour
{
    public GameHandler gameHandler;
    private SpriteRenderer sr;
    public List<Sprite> imageList;
    public int currentPic = 0;

    // Start is called before the first frame update
    public void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void ChangePic()
    {
        if (gameHandler.isGameActive)
        {
            currentPic++;
            sr.sprite = imageList[currentPic];
        }

    }


}


