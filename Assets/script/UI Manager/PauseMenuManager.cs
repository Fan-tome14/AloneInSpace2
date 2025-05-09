using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject settingsMenuUI;

    public MonoBehaviour[] scriptsToDisable; // Liste des scripts à désactiver pendant la pause

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    // Fonction pour reprendre le jeu
    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        settingsMenuUI?.SetActive(false);
        Time.timeScale = 1f;  // Rétablir la vitesse normale du jeu
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Réactiver les scripts désactivés pendant la pause
        foreach (var script in scriptsToDisable)
            script.enabled = true;

        isPaused = false;
    }

    // Fonction pour mettre le jeu en pause
    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        settingsMenuUI?.SetActive(false);
        Time.timeScale = 0f;  // Mettre le temps en pause
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Désactiver les scripts pendant la pause
        foreach (var script in scriptsToDisable)
            script.enabled = false;

        isPaused = true;
    }

    // Fonction pour ouvrir le menu des paramètres
    public void OpenSettings()
    {
        pauseMenuUI.SetActive(false);
        settingsMenuUI?.SetActive(true);
    }

    // Fonction pour quitter le jeu
    public void QuitGame()
    {
        Time.timeScale = 1f;  // Rétablir la vitesse normale du jeu

#if UNITY_EDITOR
        // Arrêter le jeu dans l'éditeur Unity
        EditorApplication.isPlaying = false;
#else
        // Fermer l'application dans une version construite
        Application.Quit();
#endif
    }
}
