using UnityEngine;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject settingsMenuUI;
    public GameObject backgroundParametre;
    public SettingsManager settingsManager; // Référence au SettingsManager pour appliquer les changements
    public MonoBehaviour[] scriptsToDisable; // Les scripts à désactiver pendant pause

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingsMenuUI.activeSelf)
                BackToPauseMenu();
            else if (pauseMenuUI.activeSelf)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        settingsMenuUI.SetActive(false);
        backgroundParametre.SetActive(false);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        ToggleScripts(false);
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(false);
        backgroundParametre.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        EventSystem.current.SetSelectedGameObject(null);
        ToggleScripts(true);
    }

    public void OpenSettings()
    {
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(true);
        backgroundParametre.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void BackToPauseMenu()
    {
        settingsMenuUI.SetActive(false);
        backgroundParametre.SetActive(false);
        pauseMenuUI.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void ApplySettings()
    {
        settingsManager.ApplySettings();  // Applique tous les paramètres depuis SettingsManager
    }

    private void ToggleScripts(bool state)
    {
        foreach (var script in scriptsToDisable)
            script.enabled = state;
    }
    public void QuitGame()
    {
        Time.timeScale = 1f; // Réactive le temps (optionnel mais conseillé)
        EventSystem.current.SetSelectedGameObject(null);

#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
