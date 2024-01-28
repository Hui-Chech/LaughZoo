using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

public class ShaderButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] Material hoverMaterial;

    

    Image renderer;

    Material defaultMaterial;

    private void Awake()
    {
        renderer = GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        renderer.material = hoverMaterial;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        renderer.material = null;
    }
}
