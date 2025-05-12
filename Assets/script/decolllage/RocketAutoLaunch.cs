using UnityEngine;

public class RocketAutoLaunch : MonoBehaviour
{
    public Rigidbody rocketRigidbody;     
    public float accelerationRate = 50f; // Taux d'accélération (force par seconde)
    public float launchDuration = 5f;    // Durée pendant laquelle la force est appliquée
    public float delayBeforeLaunch = 2f;  // Délai avant le décollage
    public AudioSource launchAudio;      
    public ParticleSystem launchEffect;  

    private float launchStartTime;
    private bool isLaunching = false;

    void Start()
    {
        // Vérifier si le Rigidbody est bien assigné
        if (rocketRigidbody == null)
        {
            rocketRigidbody = GetComponent<Rigidbody>();
        }

        // Vérifier si l'AudioSource est bien assigné
        if (launchAudio == null)
        {
            launchAudio = GetComponent<AudioSource>();
        }

        // Vérifier si le ParticleSystem est bien assigné
        if (launchEffect == null)
        {
            launchEffect = GetComponentInChildren<ParticleSystem>(); // Récupère le ParticleSystem enfant
        }

        // Geler la fusée au départ
        rocketRigidbody.useGravity = false; // Désactiver la gravité
        rocketRigidbody.isKinematic = true;
        rocketRigidbody.linearVelocity = Vector3.zero;
        rocketRigidbody.angularVelocity = Vector3.zero;

        // Planifier le démarrage du lancement
        Invoke("StartLaunchSequence", delayBeforeLaunch);
    }

    void StartLaunchSequence()
    {
        Debug.Log("Préparation au décollage... !");

        // Jouer le son de lancement
        if (launchAudio != null)
        {
            launchAudio.Play();
        }
        else
        {
            Debug.LogWarning("Aucun AudioSource trouvé sur l'objet de la fusée !");
        }

        // Activer l'effet de particules
        if (launchEffect != null)
        {
            launchEffect.Play();
        }
        else
        {
            Debug.LogWarning("Aucun ParticleSystem trouvé sur l'objet de la fusée !");
        }

        // Assurer que la fusée n'est plus en mode cinématique
        rocketRigidbody.useGravity = true; // Réactiver la gravité
        rocketRigidbody.isKinematic = false;
        isLaunching = true;
        launchStartTime = Time.time;
    }

    void FixedUpdate()
    {
        if (isLaunching && Time.time < launchStartTime + launchDuration)
        {
            // Appliquer une force continue vers l'avant
            Vector3 thrustDirection = transform.forward;
            rocketRigidbody.AddForce(thrustDirection * accelerationRate, ForceMode.Force);
        }
        else if (isLaunching)
        {
            isLaunching = false;

        }
    }
}