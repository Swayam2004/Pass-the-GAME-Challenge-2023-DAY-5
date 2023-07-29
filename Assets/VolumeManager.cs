using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeManager : MonoBehaviour
{
    [SerializeField] GameObject volumeObject;
    private void Update() {
        volumeObject.SetActive(SettingsHolder.postProc);
    }
}
