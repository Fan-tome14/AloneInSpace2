using UnityEngine;

public class CameraModeSwitcher : MonoBehaviour
{
    public Transform firstPersonTarget; // généralement la tête ou le haut du personnage
    public float mouseSensitivity = 100f;

    public Transform playerBody; // corps ou racine du personnage

    private float xRotation = 0f;
    private bool isFirstPerson = false;

    private CameraController thirdPersonScript;

    void Start()
    {
        thirdPersonScript = GetComponent<CameraController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Switch de caméra avec F5
        if (Input.GetKeyDown(KeyCode.F5))
        {
            isFirstPerson = !isFirstPerson;

            if (thirdPersonScript != null)
                thirdPersonScript.enabled = !isFirstPerson;
        }

        if (isFirstPerson)
        {
            HandleFirstPersonView();
        }
    }

    void HandleFirstPersonView()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Applique la rotation verticale à la caméra (pitch)
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Applique la rotation horizontale au corps du joueur (yaw)
        if (playerBody != null)
        {
            playerBody.Rotate(Vector3.up * mouseX);
            transform.position = firstPersonTarget.position; // la caméra suit la tête
        }
    }
}

