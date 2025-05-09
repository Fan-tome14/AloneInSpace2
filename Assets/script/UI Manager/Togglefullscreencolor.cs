using UnityEngine;
using UnityEngine.UI;

public class FullscreenToggleColor : MonoBehaviour
{
    public Toggle fullscreenToggle;  // Le Toggle de plein écran
    public Text toggleText;          // Le texte du Toggle
    public Color fullscreenColor = Color.green;  // Couleur verte pour plein écran
    public Color windowedColor = Color.red;      // Couleur rouge pour mode fenêtré
    public Color hoverColor = Color.blue;        // Couleur bleue pour survol souris

    void Start()
    {
        // Si le texte n'est pas assigné, on le récupère automatiquement
        if (toggleText == null)
        {
            toggleText = fullscreenToggle.GetComponentInChildren<Text>();
        }

        // Assurez-vous que la couleur du texte soit visible dès le départ
        toggleText.color = windowedColor;  // Par défaut, force la couleur du texte à être rouge

        // Initialiser la couleur en fonction de l'état actuel du plein écran
        UpdateToggleColor();
    }

    void Update()
    {
        // Vérifier l'état du Toggle et ajuster la couleur du texte
        if (fullscreenToggle.isOn)
        {
            toggleText.color = fullscreenColor;  // Vert si plein écran
        }
        else
        {
            toggleText.color = windowedColor;    // Rouge si mode fenêtre
        }
    }

    // Lors du survol de la souris, changer la couleur du texte en bleu
    public void OnMouseEnter()
    {
        toggleText.color = hoverColor;  // Changer la couleur en bleu lors du survol
    }

    // Lors du retrait de la souris, revenir à la couleur normale
    public void OnMouseExit()
    {
        UpdateToggleColor();  // Remet la couleur en fonction de l'état du Toggle
    }

    // Mettre à jour la couleur en fonction de l'état du plein écran
    private void UpdateToggleColor()
    {
        if (fullscreenToggle.isOn)
        {
            toggleText.color = fullscreenColor;  // Vert si plein écran
        }
        else
        {
            toggleText.color = windowedColor;    // Rouge si mode fenêtre
        }
    }
}
