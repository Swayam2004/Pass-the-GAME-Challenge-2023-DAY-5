using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MouseCunvas : MonoBehaviour
{
    public Image image;
    public Sprite Press;
    public Sprite Normal;

    GameObject RotationHelper;
    GameObject PositionHelper;
    private void Start()
    {
        RotationHelper = GameObject.CreatePrimitive(PrimitiveType.Quad);
        RotationHelper.SetActive(false);
        RotationHelper.name = "RotationHelper : Dont Delete its help the cursor";
        PositionHelper = GameObject.CreatePrimitive(PrimitiveType.Quad);
        PositionHelper.name = "PositionHelper : Dont delete its help the cursor";
        PositionHelper.SetActive(false);
        Cursor.visible = false;
    }

    public void Rotation()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        PositionHelper.transform.position = Camera.main.ScreenToWorldPoint(mousePos);
        RotationHelper.transform.position = Vector3.Lerp(RotationHelper.transform.position, PositionHelper.transform.position, Time.deltaTime * 3);
        Vector3 mousePosition = Input.mousePosition;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = mouseWorldPosition - RotationHelper.transform.position;
        float rotationAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0f, 0f, rotationAngle);
        RotationHelper.transform.rotation = rotation;
        transform.rotation = RotationHelper.transform.rotation;
    }

    void Update()
    {
        Rotation();
        if (Input.GetMouseButtonDown(0))
        {
            image.sprite = Press;
        }
        if (Input.GetMouseButtonUp(0))
        {
            image.sprite = Normal;
        }
        RectTransform Rect = GetComponent<RectTransform>();
        RectTransform CunvasRect = transform.parent.GetComponent<RectTransform>();
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(CunvasRect, Input.mousePosition, null, out localPoint);
        Vector2 normalizedPoint = new Vector2((localPoint.x / CunvasRect.sizeDelta.x) + 0.5f, (localPoint.y / CunvasRect.sizeDelta.y) + 0.5f);

        Rect.anchorMin = normalizedPoint;
        Rect.anchorMax = normalizedPoint;
    }
}
