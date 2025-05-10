using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;
using System.Collections.Generic;

public class SettingsManager : MonoBehaviour
{
    public AudioMixer audioMixer;  // Audio Mixer pour contr�ler le volume
    public Slider volumeSlider;    // Slider pour le volume
    public TMP_Dropdown qualityDropdown;  // Dropdown pour la qualit� graphique
    public TMP_Dropdown resolutionDropdown;  // Dropdown pour la r�solution
    public Toggle fullscreenToggle;  // Toggle pour activer/d�sactiver le plein �cran

    private Resolution[] resolutions;

    void Start()
    {
        // Initialisation des r�solutions disponibles
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            // V�rifier si la r�solution correspond � la r�solution actuelle
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        // Ajouter les options au dropdown et s�lectionner l'option actuelle
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        // Initialisation de la qualit� graphique
        qualityDropdown.value = QualitySettings.GetQualityLevel();
        qualityDropdown.RefreshShownValue();

        // Initialiser le mode plein �cran
        fullscreenToggle.isOn = Screen.fullScreen;

        // Initialiser le volume � partir du AudioMixer
        float volume;
        audioMixer.GetFloat("MasterVolume", out volume);
        volumeSlider.value = Mathf.Pow(10f, volume / 20f);  // Convertir dB en [0,1]
    }

    // Modifier le volume
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
    }

    // Modifier la qualit� graphique
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    // Modifier la r�solution
    public void SetResolution(int resolutionIndex)
    {
        Resolution res = resolutions[resolutionIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }

    // Activer/d�sactiver le plein �cran
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    // Cette m�thode sera appel�e pour appliquer les changements dans le menu param�tres
    public void ApplySettings()
    {
        // Appliquer la r�solution
        SetResolution(resolutionDropdown.value);

        // Appliquer la qualit� graphique
        SetQuality(qualityDropdown.value);

        // Appliquer le mode plein �cran
        SetFullscreen(fullscreenToggle.isOn);

        // Appliquer le volume
        SetVolume(volumeSlider.value);
    }
}
