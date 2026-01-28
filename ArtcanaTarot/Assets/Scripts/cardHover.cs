using UnityEngine;
using UnityEngine.EventSystems;

public class CardHoverTilt : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler
{
    [Header("Tilt Settings")]
    public float maxTiltAngle = 12f;
    public float smoothSpeed = 10f;

    [Header("Axis Correction")]
    public bool invertX = false;
    public bool invertY = false;

    [Tooltip("Assign the Button RectTransform if this is on a child")]
    public RectTransform hoverArea;

    bool isHovered;
    RectTransform rect;
    Quaternion startRotation;

    void Awake()
    {
        rect = hoverArea != null
            ? hoverArea
            : GetComponent<RectTransform>();

        startRotation = transform.localRotation;
    }

    void Update()
    {
        Quaternion targetRotation = startRotation;

        if (isHovered)
        {
            Vector2 localMousePos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rect,
                Input.mousePosition,
                null,
                out localMousePos
            );

            // Normalize from -1 to 1
            Vector2 normalized = new Vector2(
                localMousePos.x / (rect.rect.width * 0.5f),
                localMousePos.y / (rect.rect.height * 0.5f)
            );

            normalized = Vector2.ClampMagnitude(normalized, 1f);

            float xDir = invertX ? -1f : 1f;
            float yDir = invertY ? -1f : 1f;

            float tiltX = -normalized.y * maxTiltAngle * yDir;
            float tiltY = normalized.x * maxTiltAngle * xDir;

            targetRotation = startRotation * Quaternion.Euler(tiltX, tiltY, 0f);
        }

        transform.localRotation = Quaternion.Slerp(
            transform.localRotation,
            targetRotation,
            Time.deltaTime * smoothSpeed
        );
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
    }
}
