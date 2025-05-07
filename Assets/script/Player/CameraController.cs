using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // Le joueur
    public float distance = 5f;
    public float height = 2f;
    public float sensitivity = 5f;
    public float minY = -15f;
    public float maxY = 80f;

    private float rotationX = 0f;
    private float rotationY = 10f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        rotationX += Input.GetAxis("Mouse X") * sensitivity;
        rotationY -= Input.GetAxis("Mouse Y") * sensitivity;
        rotationY = Mathf.Clamp(rotationY, minY, maxY);

        Quaternion rotation = Quaternion.Euler(rotationY, rotationX, 0);
        Vector3 position = target.position + rotation * new Vector3(0, 0, -distance);
        position.y += height;

        transform.position = position;
        transform.LookAt(target.position + Vector3.up * height);
    }
}

