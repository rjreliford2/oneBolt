using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SavedInfo : MonoBehaviour
{
    public float volume;
    public Slider volumeSlider;
    public bool particles;
    public Toggle particleCBox;
    public AudioMixer audioMixer;

    private void Start()
    {
        
        volume = PlayerPrefs.GetFloat("volume", 10f);
        particles = PlayerPrefs.GetInt("particles", 0) == 1 ? true : false;
        SetVolume(volume);
        SetParticles(particles);
    }

    public void SetVolume(float vol)
    {
        if (vol < -25f)
        {
            vol = -80f;
        }
        audioMixer.SetFloat("volume", vol);
        volume = vol;
        volumeSlider.value = vol;
        // Save the volume to PlayerPrefs
        PlayerPrefs.SetFloat("volume", vol);
        PlayerPrefs.Save();
    }

    public void SetParticles(bool yes)
    {
        if (yes)
        {
            PlayerPrefs.SetInt("particles", 1);
            
            particleCBox.isOn = true;
        }
        else
        {
            PlayerPrefs.SetInt("particles", 0);
            particleCBox.isOn = false;
        }
        PlayerPrefs.Save();
    }

    public void setUp()
    {

    }
}