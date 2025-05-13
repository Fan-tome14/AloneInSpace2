using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;
using System.Collections.Generic;

public class SettingsManager : MonoBehaviour
{
    public AudioMixer audioMixer;                  // Audio Mixer pour contrôler le volume
    public Slider volumeSlider;                    // Slider pour le volume général
    public Slider musicVolumeSlider;               // Slider pour le volume musique
    public TMP_Dropdown resolutionDropdown;        // Dropdown pour la résolution
    public Toggle fullscreenToggle;                // Toggle pour le plein écran

    private Resolution[] resolutions;

    void Start()
    {
        // Initialisation des résolutions disponibles
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        fullscreenToggle.isOn = Screen.fullScreen;

        // Initialiser les volumes à partir de l'AudioMixer
        if (audioMixer.GetFloat("MasterVolume", out float masterVolume))
        {
            volumeSlider.value = Mathf.Pow(10f, masterVolume / 20f);  // dB → [0,1]
        }

        if (audioMixer.GetFloat("MusicVolume", out float musicVolume))
        {
            musicVolumeSlider.value = Mathf.Pow(10f, musicVolume / 20f);  // dB → [0,1]
        }
    }

    public void SetVolume(float value)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(Mathf.Clamp(value, 0.001f, 1f)) * 20f);
    }

    public void SetMusicVolume(float value)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(Mathf.Clamp(value, 0.001f, 1f)) * 20f);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution res = resolutions[resolutionIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void ApplySettings()
    {
        SetResolution(resolutionDropdown.value);
        SetFullscreen(fullscreenToggle.isOn);
        SetVolume(volumeSlider.value);
        SetMusicVolume(musicVolumeSlider.value);
    }
}
