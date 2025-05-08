using UnityEngine;

public class CameraViewSwitcher : MonoBehaviour
{
    [Header("Références")]
    public Transform fpsTarget;         // Tête du joueur (FPS)
    public Transform tpsFollowTarget;   // Corps du joueur (TPS)
    public PlayerMovement playerMovement;

    [Header("Sensibilité")]
    public float mouseSensitivity = 3f;
    public float scrollSensitivity = 2f;
    public float minY = -30f;
    public float maxY = 60f;

    [Header("Distance de la caméra")]
    public float distanceFromTarget = 4f;
    public float minDistance = 2f;
    public float maxDistance = 6f;

    [Header("Collision")]
    public LayerMask collisionLayers; // Coche "Default", "Environment", etc.

    private bool isFirstPerson = false;
    private float xRotation = 0f;
    private float yaw = 0f;
    private float pitch = 20f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        isFirstPerson = false;
        SwitchToTPS();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            isFirstPerson = !isFirstPerson;

            if (isFirstPerson)
                SwitchToFPS();
            else
                SwitchToTPS();
        }

        if (isFirstPerson)
            HandleFPSCamera();
        else
            HandleOrbitalTPSCamera();

        if (playerMovement != null)
            playerMovement.isInFirstPerson = isFirstPerson;
    }

    void HandleFPSCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * 30f * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * 30f * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        tpsFollowTarget.Rotate(Vector3.up * mouseX);

        if (fpsTarget != null)
        {
            transform.position = fpsTarget.position;
            transform.rotation = Quaternion.Euler(xRotation, tpsFollowTarget.eulerAngles.y, 0f);
        }
    }

    void HandleOrbitalTPSCamera()
    {
        // Rotation orbitale avec la souris
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, minY, maxY);

        // Zoom avec la molette
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        distanceFromTarget -= scroll * scrollSensitivity;
        distanceFromTarget = Mathf.Clamp(distanceFromTarget, minDistance, maxDistance);

        // Direction orbitale
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 desiredCameraOffset = rotation * Vector3.back * distanceFromTarget;

        // Collision : raycast depuis le joueur vers la position désirée
        Vector3 targetPosition = tpsFollowTarget.position + Vector3.up * 1.5f;
        Vector3 desiredPosition = targetPosition + desiredCameraOffset;

        if (Physics.Raycast(targetPosition, desiredCameraOffset.normalized, out RaycastHit hit, distanceFromTarget, collisionLayers))
        {
            // Position ajustée à l'impact
            desiredPosition = hit.point - desiredCameraOffset.normalized * 0.2f;
        }

        transform.position = desiredPosition;
        transform.LookAt(targetPosition);
    }

    void SwitchToFPS()
    {
        xRotation = 0f;
    }

    void SwitchToTPS()
    {
        yaw = tpsFollowTarget.eulerAngles.y;
        pitch = 20f;
    }
}
