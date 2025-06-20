using UnityEngine;
using UnityEngine.EventSystems;

public class HandHoverUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Transform handTransform;
    public float hoverOffset = 30f;

    private Vector3 originalPosition;

    private void Start()
    {
        if (handTransform == null)
            Debug.LogError("HandHoverUI: handTransform não atribuído!");

        originalPosition = handTransform.localPosition;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        handTransform.localPosition = originalPosition + Vector3.up * hoverOffset;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        handTransform.localPosition = originalPosition;
    }
}
