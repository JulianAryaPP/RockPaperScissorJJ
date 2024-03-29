using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionPanel : MonoBehaviour
{
    [SerializeField] AudioManager audioManager;
    [SerializeField] Toggle muteToggle;
    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] TMP_Text bgmVolText;
    [SerializeField] TMP_Text sfxVolText;

    private void Start()
    {
        muteToggle.isOn = audioManager.IsMute;
        bgmSlider.value = audioManager.bgmVolume;
        sfxSlider.value = audioManager.sfxVolume;
        SetBgmVolText(bgmSlider.value);
        SetSfxVolText(sfxSlider.value);
    }
    public void SetBgmVolText(float value)
    {
        bgmVolText.text = Mathf.RoundToInt(value * 100).ToString();
    }
    public void SetSfxVolText(float value)
    {
        sfxVolText.text = Mathf.RoundToInt(value * 100).ToString();
    }
}
