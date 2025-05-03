using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float sprintMultiplier = 1.5f; // Facteur de sprint
    public float jumpForce = 5f;
    public Transform orientation;
    public Transform objectToRotate;

    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();

        // Vérifier si la touche espace est pressée pour sauter
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal"); // Q/D
        float z = Input.GetAxisRaw("Vertical");   // Z/S

        // Augmenter la vitesse si la touche Shift est enfoncée
        float currentMoveSpeed = moveSpeed;
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            currentMoveSpeed *= sprintMultiplier; // Appliquer le multiplicateur de sprint
        }

        // Calcul de la direction de mouvement
        Vector3 moveDir = orientation.forward * z + orientation.right * x;
        moveDir.Normalize();

        // Appliquer la vélocité de mouvement
        rb.linearVelocity = new Vector3(moveDir.x * currentMoveSpeed, rb.linearVelocity.y, moveDir.z * currentMoveSpeed);

        // Appliquer la rotation seulement si le joueur se déplace
        if (moveDir.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            objectToRotate.rotation = targetRotation;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}
