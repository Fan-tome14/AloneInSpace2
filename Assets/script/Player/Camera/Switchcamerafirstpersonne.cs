using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FpsPlayerController : MonoBehaviour
{
    [Header("Références")]
    public Transform cameraTransform; // La caméra FPS (souvent Main Camera)
    public Transform cameraTarget;    // Point de la tête

    [Header("Paramètres Joueur")]
    public float mouseSensitivity = 100f;
    public float moveSpeed = 5f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.5f;

    private float xRotation = 0f;
    private Vector3 velocity;
    private bool isGrounded;

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (cameraTransform == null)
            Debug.LogError("CameraTransform n'est pas assigné !");
        if (cameraTarget == null)
            Debug.LogError("CameraTarget (tête) n'est pas assigné !");

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMouseLook();
        HandleMovement();
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Rotation du corps (yaw)
        transform.Rotate(Vector3.up * mouseX);

        // Appliquer la rotation combinée à la caméra
        cameraTransform.position = cameraTarget.position;
        cameraTransform.rotation = Quaternion.Euler(xRotation, transform.eulerAngles.y, 0f);
    }

    void HandleMovement()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        float x = Input.GetAxis("Horizontal"); // Q / D
        float z = Input.GetAxis("Vertical");   // Z / S

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // Saut
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Gravité
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}


