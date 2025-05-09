using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;
    public Slider volumeSlider;

    [Header("Other")]
    public AudioMixer audioMixer; // Assigné dans l'inspecteur
    public GameObject pauseMenuUI; // Le Menu Pause à réactiver
    public GameObject settingsMenuUI; // Le Menu Paramètres à désactiver

    Resolution[] resolutions;
    int currentResolutionIndex;

    void Start()
    {
        // Remplir la liste des résolutions
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        int i = 0;
        currentResolutionIndex = 0;
        var options = new System.Collections.Generic.List<string>();

        foreach (Resolution res in resolutions)
        {
            string option = res.width + " x " + res.height;
            options.Add(option);

            if (res.width == Screen.currentResolution.width && res.height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }

            i++;
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        fullscreenToggle.isOn = Screen.fullScreen;

        float currentVolume;
        audioMixer.GetFloat("MasterVolume", out currentVolume);
        volumeSlider.value = Mathf.Pow(10, currentVolume / 20f); // Converti dB → [0-1]
    }

    // Appelé par le bouton "Appliquer"
    public void ApplySettings()
    {
        // Volume
        float volume = volumeSlider.value;
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);

        // Résolution
        Resolution selectedRes = resolutions[resolutionDropdown.value];
        Screen.SetResolution(selectedRes.width, selectedRes.height, fullscreenToggle.isOn);

        // Plein écran
        Screen.fullScreen = fullscreenToggle.isOn;
    }

    // Appelé par le bouton "Quitter"
    public void BackToPauseMenu()
    {
        settingsMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }
}
