using UnityEngine;

public class CardDragRotate : MonoBehaviour
{
    [Header("Rotation Limits (Degrees)")]
    public Vector2 xClamp = new Vector2(-45f, 45f);
    public Vector2 yClamp = new Vector2(-45f, 45f);
    public Vector2 zClamp = new Vector2(-10f, 10f);

    [Header("Rotation Settings")]
    public float sensitivity = 0.2f;
    public float smoothSpeed = 10f;
    public int mouseButton = 0; // 0 = Left Mouse Button

    [Header("Invert Drag Direction")]
    public bool invertX = false;
    public bool invertY = false;

    [Header("Return To Center")]
    public bool returnOnRelease = true;
    public float returnSpeed = 6f;

    Quaternion startRotation;
    Vector2 currentInput;
    bool dragging;

    void Start()
    {
        startRotation = transform.localRotation;
    }

    void Update()
    {
        dragging = Input.GetMouseButton(mouseButton);

        if (dragging)
        {
            float mx = Input.GetAxis("Mouse X") * sensitivity;
            float my = Input.GetAxis("Mouse Y") * sensitivity;

            if (invertX) mx *= -1f;
            if (invertY) my *= -1f;

            currentInput.x += mx;
            currentInput.y -= my;
        }
        else if (returnOnRelease)
        {
            currentInput = Vector2.Lerp(
                currentInput,
                Vector2.zero,
                Time.deltaTime * returnSpeed
            );
        }

        // Clamp rotation
        currentInput.x = Mathf.Clamp(currentInput.x, yClamp.x, yClamp.y);
        currentInput.y = Mathf.Clamp(currentInput.y, xClamp.x, xClamp.y);

        float z = Mathf.Clamp(-currentInput.x * 0.2f, zClamp.x, zClamp.y);

        Quaternion targetRotation = Quaternion.Euler(
            currentInput.y,
            currentInput.x,
            z
        );

        transform.localRotation = Quaternion.Slerp(
            transform.localRotation,
            startRotation * targetRotation,
            Time.deltaTime * smoothSpeed
        );
    }
}
