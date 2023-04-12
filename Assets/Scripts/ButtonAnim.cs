using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAnim : MonoBehaviour
{
    public Animator anim;
    public SoundManager sm;

    public void AnimateMenu()
    {
        sm.playSound(0);
        if (anim.gameObject.transform.position.y < 0)
        {
            anim.SetTrigger("MenuIn");
        }
        else
        {
            anim.SetTrigger("MenuOut");
        }

    }

    public void AnimateInfoMenu()
    {
        sm.playSound(0);
        anim.SetTrigger("Animate");
    }


}
