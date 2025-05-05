using UnityEngine;
using UnityEngine.SceneManagement;

public class portalsceneChanger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Vérifie si la main touche le bouton
        {
            LoadMainScene();
        }
    }

    public void LoadMainScene()
    {
        Debug.Log("Chargement de la sc�ne main...");
        SceneManager.LoadScene("decollage"); // Charge la scéne "decolage"
    }
}