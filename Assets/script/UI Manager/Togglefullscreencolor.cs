using UnityEngine;
using UnityEngine.UI;

public class FullscreenToggleColor : MonoBehaviour
{
    public Toggle fullscreenToggle;  // Le Toggle de plein �cran
    public Text toggleText;          // Le texte du Toggle
    public Color fullscreenColor = Color.green;  // Couleur verte pour plein �cran
    public Color windowedColor = Color.red;      // Couleur rouge pour mode fen�tr�
    public Color hoverColor = Color.blue;        // Couleur bleue pour survol souris

    void Start()
    {
        // Si le texte n'est pas assign�, on le r�cup�re automatiquement
        if (toggleText == null)
        {
            toggleText = fullscreenToggle.GetComponentInChildren<Text>();
        }

        // Assurez-vous que la couleur du texte soit visible d�s le d�part
        toggleText.color = windowedColor;  // Par d�faut, force la couleur du texte � �tre rouge

        // Initialiser la couleur en fonction de l'�tat actuel du plein �cran
        UpdateToggleColor();
    }

    void Update()
    {
        // V�rifier l'�tat du Toggle et ajuster la couleur du texte
        if (fullscreenToggle.isOn)
        {
            toggleText.color = fullscreenColor;  // Vert si plein �cran
        }
        else
        {
            toggleText.color = windowedColor;    // Rouge si mode fen�tre
        }
    }

    // Lors du survol de la souris, changer la couleur du texte en bleu
    public void OnMouseEnter()
    {
        toggleText.color = hoverColor;  // Changer la couleur en bleu lors du survol
    }

    // Lors du retrait de la souris, revenir � la couleur normale
    public void OnMouseExit()
    {
        UpdateToggleColor();  // Remet la couleur en fonction de l'�tat du Toggle
    }

    // Mettre � jour la couleur en fonction de l'�tat du plein �cran
    private void UpdateToggleColor()
    {
        if (fullscreenToggle.isOn)
        {
            toggleText.color = fullscreenColor;  // Vert si plein �cran
        }
        else
        {
            toggleText.color = windowedColor;    // Rouge si mode fen�tre
        }
    }
}
