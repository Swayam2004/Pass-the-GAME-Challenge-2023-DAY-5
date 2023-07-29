using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour {
    [SerializeField] private TMP_Text levelNameText;

    void Start() {
        UpdateLevelName();
    }

    private void OnDrawGizmos() {
        UpdateLevelName();
    }

    private void UpdateLevelName() {
        Scene currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        if(currentScene == null) {
            levelNameText.text = "Unknown Level";
        } else {
            levelNameText.text = currentScene.name;
        }
    }
}
