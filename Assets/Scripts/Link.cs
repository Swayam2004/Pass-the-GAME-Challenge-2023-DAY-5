using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Link : MonoBehaviour
{
    private const float AREA = 1.5f;

    private Linkable linkable1;
    private Linkable linkable2;
    private LineRenderer lineRenderer;

    private bool connecting = true;

    public void Start()
    {
        lineRenderer = GetComponentInChildren<LineRenderer>();
        lineRenderer.enabled = false;
    }

    public void Update()
    {
        if (!lineRenderer.enabled && linkable1 != null)
        {
            lineRenderer.enabled = true;
        }
        if (linkable1 != null)
        {
            Vector3 fromPos = GetFromPosition();
            Vector3 toPos = GetToPosition();
            float dist = Vector3.Distance(fromPos, toPos);
            float width = Mathf.Clamp(AREA / dist, 0, 1);
            lineRenderer.SetPosition(0, fromPos);
            lineRenderer.SetPosition(1, toPos);
            lineRenderer.startWidth = width / m;
            lineRenderer.endWidth = width / n;
        }
    }
    float m = 1.3f, n = 2;

    private Vector3 GetFromPosition()
    {
        return linkable1.transform.position;
    }

    private Vector3 GetToPosition()
    {
        if (linkable2 == null)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            return new Vector3(mousePos.x, mousePos.y, 0);
        }
        else
        {
            return linkable2.transform.position;
        }
    }

    public void ConnectLinkable1(Linkable linkable1)
    {
        if (!connecting) return;
        this.linkable1 = linkable1;
    }

    public void ConnectLinkable2(Linkable linkable2)
    {
        if (!connecting) return;
        this.linkable2 = linkable2;
    }

    public void DisconnectLinkable2()
    {
        if (!connecting) return;
        linkable2 = null;
    }


    public void ActivateConnection()
    {
        if (!connecting) return;
        if (linkable2 == null)
        {
            Debug.LogError("Activating link without linkable2");
            return;
        }
        SpringJoint2D joint = linkable1.CreateJoin(linkable2);
        // TODO: Use scriptable object to store these values
        joint.distance = 3.5f;
        joint.frequency = 3f;
        joint.dampingRatio = .1f;
        joint.enableCollision = true;
        connecting = false;
        n = 1;
        m = 1;
    }

    public void DestroySelf()
    {
        if (!connecting)
        {
            Debug.LogError("Not implemented: destroying activated link");
            return;
        }
        GameObject.Destroy(gameObject);
    }

    public bool AlreadyConnects(Linkable linkable1, Linkable linkable2)
    {
        return (linkable1 == this.linkable1 && linkable2 == this.linkable2) || (linkable1 == this.linkable2 && linkable2 == this.linkable1);
    }
}
