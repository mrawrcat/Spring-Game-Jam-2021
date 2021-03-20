using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioHelper : MonoBehaviour
{

    [SerializeField]
    private Slider volumeSlider;
    [SerializeField]
    private Toggle tips;
    // Start is called before the first frame update
    void Start()
    {
        volumeSlider.minValue = 0;
        volumeSlider.maxValue = 1;
        volumeSlider.value = AudioManager.audioManager.sounds[0].volume;
        tips.isOn = GameManager.manager.tutorialOn;
    }

    // Update is called once per frame
    void Update()
    {
        AudioManager.audioManager.sounds[0].source.volume = volumeSlider.value;
        AudioManager.audioManager.sounds[0].volume = volumeSlider.value;
        GameManager.manager.tutorialOn = tips.isOn;
    }
}
