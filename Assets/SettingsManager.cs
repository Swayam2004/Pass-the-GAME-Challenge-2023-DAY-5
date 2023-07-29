using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public TextMeshProUGUI BloomText;
    bool postProcessing;
    public void ToggleBloom()
    {
        postProcessing = !postProcessing;
        SettingsHolder.postProc = postProcessing;
        BloomText.text = "Post Processing: " + postProcessing;
    }
}
