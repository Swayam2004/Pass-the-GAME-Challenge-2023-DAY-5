using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ExtraButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    bool Select;
    Vector3 normalscale;
    public float Scale = 1.2f;
    public float Speed = 3;
    void Start()
    {
        normalscale = transform.localScale;
    }
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        Select = true;
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        Select = false;
    }
    void Update()
    {

        if (Select)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, normalscale * Scale, Speed * Time.deltaTime);
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, normalscale, Speed * Time.deltaTime);
        }
    }
}