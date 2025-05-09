using UnityEngine;
using UnityEngine.UI;
using TMPro;  // Assurez-vous que ce namespace est inclus pour TMP
using UnityEngine.Audio;
using System.Collections.Generic;

public class SettingsManager : MonoBehaviour
{
    public AudioMixer audioMixer;  // Audio Mixer que tu vas utiliser pour contrôler le volume
    public Slider volumeSlider;    // Slider pour le volume
    public TMP_Dropdown qualityDropdown;  // Dropdown pour la qualité graphique
    public TMP_Dropdown resolutionDropdown;  // Dropdown pour la résolution
    public Toggle fullscreenToggle;  // Toggle pour activer/désactiver le plein écran

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

            // Vérifier si la résolution correspond à la résolution actuelle
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        // Ajouter les options au dropdown et sélectionner l'option actuelle
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        // Initialisation de la qualité graphique
        qualityDropdown.value = QualitySettings.GetQualityLevel();
        qualityDropdown.RefreshShownValue();

        // Initialiser le mode plein écran
        fullscreenToggle.isOn = Screen.fullScreen;

        // Initialiser le volume à partir du AudioMixer
        float volume;
        audioMixer.GetFloat("MasterVolume", out volume);
        volumeSlider.value = Mathf.Pow(10f, volume / 20f);  // Convertir dB en [0,1]
    }

    // Modifier le volume
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
    }

    // Modifier la qualité graphique
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    // Modifier la résolution
    public void SetResolution(int resolutionIndex)
    {
        Resolution res = resolutions[resolutionIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }

    // Activer/désactiver le plein écran
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}

