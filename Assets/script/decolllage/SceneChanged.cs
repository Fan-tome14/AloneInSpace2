using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneChanged : MonoBehaviour
{
    public Image fadeImage; // Image noire pour l'effet de fondu
    public float fadeDuration = 2f; // Durée du fondu
    public float delayBeforeFadeOut = 15f; // Temps avant le début du fondu
    public float minAlphaAfterFade = 0f; // Valeur d'alpha minimale après le clignotement
    public float blinkSpeed = 0.1f; // Vitesse du clignotement
    public float minBlinkInterval = 2f; // Intervalle minimal entre les clignotements
    public float maxBlinkInterval = 5f; // Intervalle maximal entre les clignotements
    public AudioSource audioSource1;
    public float delayBeforeAudio1 = 1f; // Délai avant de jouer le son 1 à la fin

    private bool canBlink = true;

    void Start()
    {
        // Démarrer avec un fondu d'ouverture
        StartCoroutine(FadeIn());

        // Lancer les clignotements aléatoires
        StartCoroutine(RandomBlinking());

        // Planifier le fondu de sortie
        Invoke("StartFadeOutSequence", delayBeforeFadeOut);
    }

    IEnumerator FadeIn()
    {
        float alpha = 1f;
        while (alpha > 0)
        {
            alpha -= Time.deltaTime / fadeDuration;
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
    }

    void StartFadeOutSequence()
    {
        canBlink = false; // Empêcher de nouveaux clignotements pendant le fondu de sortie
        StartCoroutine(FadeOutAndPlayAudio());
    }

    IEnumerator FadeOutAndPlayAudio()
    {
        float alpha = 0f;
        while (alpha < 1)
        {
            alpha += Time.deltaTime / fadeDuration;
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        // Jouer l'audio 1 après un délai pendant le fondu final
        if (audioSource1 != null)
        {
            audioSource1.Play();
        }
        else
        {
            Debug.LogWarning("AudioSource 1 n'est pas assigné !");
        }

        // Attendre la fin de l'audio 1 avant de changer de scène (optionnel)
        if (audioSource1 != null && audioSource1.clip != null)
        {
            yield return new WaitForSeconds(audioSource1.clip.length);
        }

        // Changer de scène après le fondu et (optionnellement) l'audio
        SceneManager.LoadScene("jour1");
    }

    IEnumerator RandomBlinking()
    {
        while (canBlink)
        {
            float randomDelay = Random.Range(minBlinkInterval, maxBlinkInterval);
            yield return new WaitForSeconds(randomDelay);
            StartCoroutine(BlinkOnce());
        }
    }

    IEnumerator BlinkOnce()
    {
        float alpha = minAlphaAfterFade;

        while (alpha < 1f)
        {
            alpha += Time.deltaTime / blinkSpeed;
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        yield return new WaitForSeconds(0.05f);

        while (alpha > minAlphaAfterFade)
        {
            alpha -= Time.deltaTime / blinkSpeed;
            alpha = Mathf.Max(alpha, minAlphaAfterFade);
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
    }
}