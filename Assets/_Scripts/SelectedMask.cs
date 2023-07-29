using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedMask : MonoBehaviour {
    [SerializeField] private Linkable linkable;
    [SerializeField] private GameObject visual;

    private void Start() {
        linkable.OnSelected += Linkable_OnSelected;
        linkable.OnDeselected += Linkable_OnDeselected;
    }

    private void Linkable_OnSelected(object sender, System.EventArgs e) {
        visual.SetActive(true);
    }
    private void Linkable_OnDeselected(object sender, System.EventArgs e) {
        visual.SetActive(false);
    }
}
