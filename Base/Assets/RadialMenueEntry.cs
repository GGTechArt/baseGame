using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Analytics;
using DG.Tweening;

public class RadialMenueEntry : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public delegate void RadialMenuEntryDelegate(RadialMenueEntry pEntry);
    
    [SerializeField]
    TextMeshProUGUI Label;

    [SerializeField]
    RawImage icon;

    RectTransform rect;
    RadialMenuEntryDelegate Callback;


    private void Start()
    {
        rect = icon.GetComponent<RectTransform>();
    }

    public void SetLabel(string pText)
    {
        Label.text = pText;
    }

    public void SetIcon(Texture pIcon)
    {
        icon.texture = pIcon;
    }
    
    public Texture GetIcon()
    { 
        return (icon.texture);
    }

    public void SetCallback(RadialMenuEntryDelegate pCallback)
    {
        Callback = pCallback;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Callback?.Invoke(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        rect.DOComplete();
        rect.DOScale(Vector3.one * 1.5f, .3f).SetEase(Ease.OutQuad);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        rect.DOComplete();
        rect.DOScale(Vector3.one, .3f).SetEase(Ease.OutQuad);
    }
}
