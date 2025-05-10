using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FullscreenToggleColor : MonoBehaviour
{
    public Toggle togglePleinEcran;                // Le Toggle UI
    public Image checkmarkImage;                   // L'image du Checkmark
    public Color couleurPleinEcran = Color.green;  // Couleur si plein �cran
    public Color couleurFenetre = Color.red;       // Couleur si fen�tr�

    void Start()
    {
        // V�rifier si le Checkmark a �t� assign�
        if (checkmarkImage == null && togglePleinEcran != null)
        {
            Transform background = togglePleinEcran.transform.Find("Background");
            if (background != null)
            {
                Transform checkmark = background.Find("Checkmark");
                if (checkmark != null)
                {
                    checkmarkImage = checkmark.GetComponent<Image>();
                }
            }
        }

        // Afficher l'�tat initial du toggle
        if (togglePleinEcran != null)
        {
            Debug.Log("Initial toggle state: " + togglePleinEcran.isOn);
        }

        // Appliquer l'�tat initial de la couleur du checkmark en fonction du toggle
        MettreAJourEtat();

        // Ajouter un listener pour d�tecter les changements du Toggle
        if (togglePleinEcran != null)
        {
            togglePleinEcran.onValueChanged.AddListener(delegate { MettreAJourEtat(); });
        }
    }

    private void MettreAJourEtat()
    {
        if (togglePleinEcran == null || checkmarkImage == null) return;

        bool estPleinEcran = togglePleinEcran.isOn;

        // Afficher un log pour suivre l'�tat du toggle et du checkmark
        Debug.Log("Toggle State Changed: " + estPleinEcran);

        // Appliquer la couleur du Checkmark en fonction de l'�tat du Toggle
        checkmarkImage.color = estPleinEcran ? couleurPleinEcran : couleurFenetre;

        // V�rifier que la couleur du Checkmark est bien appliqu�e
        Debug.Log("Checkmark color: " + checkmarkImage.color);
    }
}
