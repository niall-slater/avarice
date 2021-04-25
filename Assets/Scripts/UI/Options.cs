using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public Slider CreatureCapSlider;
    public Slider MusicVol;
    public Slider MasterVol;

    public TextMeshProUGUI CreatureCapReadout;
    public TextMeshProUGUI MusicVolReadout;
    public TextMeshProUGUI MasterVolReadout;

    public MusicController music;

    public void UpdateCreatureCap()
    {
        var value = CreatureCapSlider.value;
        CreatureCapReadout.text = $"{value}";
    }

    public void UpdateMusicVol()
    {
        var value = MusicVol.value;
        music.SetMusicVolume(value);
        MusicVolReadout.text = $"{Mathf.RoundToInt(value * 100f)}%";
    }

    public void UpdateMasterVol()
    {
        var value = MasterVol.value;
        AudioListener.volume = value;
        MasterVolReadout.text = $"{Mathf.RoundToInt(value * 100f)}%";
    }
}
