using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class UIButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform rectTransform;
    private Vector3 originalScale;
    public float hoverScale = 1.2f;
    public float animationDuration = 0.2f;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalScale = rectTransform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        rectTransform.DOScale(originalScale * hoverScale, animationDuration).SetEase(Ease.OutBounce);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        rectTransform.DOScale(originalScale, animationDuration).SetEase(Ease.InBounce);
    }
}

