using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class SetFonts : MonoBehaviour
{
    [SerializeField] private TMP_FontAsset dynamicFont;
    [SerializeField] private TMP_FontAsset staticFont;
    [SerializeField] private TMP_FontAsset dynamicFontNoFeatures;
    [SerializeField] private TMP_FontAsset staticFontNoFeatures;
    [SerializeField] private TMP_FontAsset staticFontFixKerning;

    [SerializeField] private GameObject[] dynamicParents;
    [SerializeField] private GameObject[] staticParents;
    [SerializeField] private GameObject[] dynamicParentsNoFeatures;
    [SerializeField] private GameObject[] staticParentsNoFeatures;
    [SerializeField] private GameObject[] staticParentsFixKerning;

    [SerializeField] private GameObject[] centerParents;

    [UnityEngine.ContextMenu(nameof(SetFontGroups))]
    public void SetFontGroups() {
        SetFontGroup(dynamicFont, dynamicParents);
        SetFontGroup(staticFont, staticParents);
        SetFontGroup(dynamicFontNoFeatures, dynamicParentsNoFeatures);
        SetFontGroup(staticFontNoFeatures, staticParentsNoFeatures);
        SetFontGroup(staticFontFixKerning, staticParentsFixKerning);
    }

    private void SetFontGroup(TMP_FontAsset font, GameObject[] parents)
    {
        foreach (var parent in parents) {
            foreach (var text in parent.GetComponentsInChildren<TextMeshProUGUI>()) {
                text.font = font;
            }
        }
    }

    [UnityEngine.ContextMenu(nameof(Center))]
    public void Center() {
        var centerFields = centerParents.SelectMany(p => p.GetComponentsInChildren<TextMeshProUGUI>()).ToArray();
        foreach (var field in centerFields) {
            field.alignment = TextAlignmentOptions.Center;
        }
    }
}
