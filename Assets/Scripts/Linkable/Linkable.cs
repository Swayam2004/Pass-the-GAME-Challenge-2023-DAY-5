using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Linkable : MonoBehaviour {
    public event EventHandler OnSelected;
    public event EventHandler OnDeselected;
    public event EventHandler OnActiveLinkAdded;

    [SerializeField] private Link linkPrefab;
    [SerializeField] private float selectRadius = 1f;
    [SerializeField] private GameObject burst;
    [SerializeField] private AudioSource bonkSound;

    private bool selected = false;
    private Link createdLink;
    protected List<Link> links;

    public bool isPlanet;

    private void Awake() {
        links = new List<Link>();
        bonkSound = GameObject.Find("Bonk").GetComponent<AudioSource>();
    }

    private void Update() {
        Linkable closestLinkableToMouse = GameInput.Instance.GetClosestLinkable();
        float closestLinkableToMouseDist = GameInput.Instance.GetClosestLinkableDist();
        bool canSelectAnyLinkable = closestLinkableToMouseDist < selectRadius;

        // Handle selecting this linkable
        if(canSelectAnyLinkable && closestLinkableToMouse == this) {
            SetSelected(true);
        } else {
            SetSelected(false);
        }

        // Handle snapping the current link to the linkable closest to the mouse
        if(createdLink != null) {
            if(!canSelectAnyLinkable) {
                createdLink.DisconnectLinkable2();
            } else {
                if(CanLinkTo(closestLinkableToMouse)) {
                    createdLink.ConnectLinkable2(closestLinkableToMouse);
                } else {
                    createdLink.DisconnectLinkable2();
                }
            }
        }

        uniqueUpdate();
    }

    public virtual void uniqueUpdate()
    {

    }

    private void SetSelected(bool selected) {
        if(this.selected == selected) return;
        this.selected = selected;
        if(selected) {
            OnSelected?.Invoke(this, EventArgs.Empty);
        } else {
            OnDeselected?.Invoke(this, EventArgs.Empty);
        }
    }

    private void OnMouseUp() {
        if(createdLink != null) {
            Linkable closestLinkableToMouse = GameInput.Instance.GetClosestLinkable();
            if(closestLinkableToMouse.selected && CanLinkTo(closestLinkableToMouse)) {
                LinkTo(closestLinkableToMouse);
            } else {
                createdLink.DestroySelf();
            }
        }
    }

    private void OnMouseDown() {
        GameObject linkParent = GameObject.FindGameObjectWithTag("LinkParent");
        createdLink = Instantiate(linkPrefab, linkParent.transform);
        createdLink.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - .5f);
        createdLink.ConnectLinkable1(this);
    }

    public SpringJoint2D CreateJoin(Linkable other) {
        SpringJoint2D joint = gameObject.AddComponent<SpringJoint2D>();
        joint.connectedBody = other.GetComponent<Rigidbody2D>();
        joint.anchor = Vector2.zero;
        joint.connectedAnchor = Vector2.zero;
        joint.autoConfigureDistance = joint.autoConfigureConnectedAnchor = false;
        return joint;
    }

    public bool CanLinkTo(Linkable other) {
        if(other == null || other == this) return false;
        foreach(Link link in links) {
            if(link.AlreadyConnects(this, other)) {
                return false;
            }
        }
        return true;
    }

    private void LinkTo(Linkable other) {
        if(createdLink == null) return;
        if(!CanLinkTo(other)) return;
        createdLink.ConnectLinkable2(other);
        createdLink.ActivateConnection();
        links.Add(createdLink);
        other.links.Add(createdLink);
        OnActiveLinkAdded?.Invoke(this, EventArgs.Empty);
        other.OnActiveLinkAdded?.Invoke(other, EventArgs.Empty);
        createdLink = null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(burst, transform.position, transform.rotation);
        bonkSound.pitch = UnityEngine.Random.Range(0.6f, 1.6f);
        bonkSound.Play();
    }
}
