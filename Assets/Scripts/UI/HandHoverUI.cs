using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


public class HandHoverUI : MonoBehaviour
{
    public Transform handTransform;
    public Canvas handHoverCanvas;
    public RectTransform hoverRegion;

    public float hoverOffset = 1f;
    private Vector3 originalPosition;

    private void Awake()
    {
        if (handTransform == null)
            Debug.LogError("HandHoverUI: handTransform não atribuído!");

        originalPosition = handTransform.localPosition;
        handHoverCanvas = GetComponentInParent<Canvas>();
        hoverRegion = GetComponent<RectTransform>();
    }

    private void Update()
    {
        // Verifica se o mouse está sobre a área do HandHoverUI
        if (IsMouseOverUIRect(hoverRegion, handHoverCanvas))
        {
            // Se estiver, mantém a posição elevada
            handTransform.localPosition = originalPosition + Vector3.up * hoverOffset;
        }
        else
        {
            // Caso contrário, volta à posição original
            handTransform.localPosition = originalPosition;
        }
    }


    public static bool IsMouseOverUIRect(RectTransform rectTransform, Canvas canvas)
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        return RectTransformUtility.RectangleContainsScreenPoint(rectTransform, mousePosition, canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera);
    }

}
