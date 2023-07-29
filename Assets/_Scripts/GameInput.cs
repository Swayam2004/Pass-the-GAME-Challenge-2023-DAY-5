using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour {
    public static GameInput Instance { get; private set;}
    private Linkable closestLinkable;
    private float closestLinkableDist = float.MaxValue;

    void Awake() {
        if(Instance != null) {
            Debug.LogError("More than one GameInput in scene!");
        }
        Instance = this;
    }

    void Update() {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = new Vector3(mousePos.x, mousePos.y, 0);
        closestLinkable = null;
        closestLinkableDist = float.MaxValue;
        foreach(Linkable linkable in FindObjectsOfType<Linkable>()) {
            float dist = Vector3.Distance(mousePos, linkable.transform.position);
            if(dist < closestLinkableDist) {
                closestLinkable = linkable;
                closestLinkableDist = dist;
            }
        }
    }

    public Linkable GetClosestLinkable() {
        return closestLinkable;
    }
    public float GetClosestLinkableDist() {
        return closestLinkableDist;
    }
}
