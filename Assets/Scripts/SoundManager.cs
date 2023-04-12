using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public List<AudioClip> soundList;
    public AudioSource src;
    public AudioSource bgsrc;
    [SerializeField] private Slider volSlider = null;
    [SerializeField] private Slider bgvolSlider = null;
    public float volume;
    public float bgvolume;

    private void Start()
    {
        if (PlayerPrefs.GetInt("firstTime") == 1)
        {
            bgsrc.volume = PlayerPrefs.GetFloat("bgvolume");
            src.volume = PlayerPrefs.GetFloat("volume");
            if (volSlider && bgvolSlider != null)
            {
                volSlider.value = PlayerPrefs.GetFloat("volume");
                bgvolSlider.value = PlayerPrefs.GetFloat("bgvolume");
            }
        }



    }


    public void VolumeSlider()
    {
        volume = volSlider.value;
        PlayerPrefs.SetFloat("volume", volume);
        src.volume = volume;
        PlayerPrefs.SetInt("firstTime", 1);
    }

    public void MusicSlider()
    {
        bgvolume = bgvolSlider.value;
        PlayerPrefs.SetFloat("bgvolume", bgvolume);
        bgsrc.volume = bgvolume;
        PlayerPrefs.SetInt("firstTime", 1);
    }

    public void playSound(int index)
    {
        src.pitch = Random.Range(0.8f, 1);
        src.PlayOneShot(soundList[index]);
    }



}
