using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonMenuSelect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    bool Select;
    public string String_;
    private void Start()
    {
        String_ = GetComponent<TextMeshProUGUI>().text;
    }
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        Select = false;
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
            GetComponent<TextMeshProUGUI>().text = "> "+ String_;
        }
        else
        {
            GetComponent<TextMeshProUGUI>().text = "  " + String_;
        }
    }
}
