﻿using RTLTMPro;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class DebugText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI field;
    [SerializeField] private RTLTextMeshPro rtlField;

    [SerializeField] private TextMeshProUGUI[] fields;

    void Start()
    {
        if (field != null) {
            field.OnPreRenderText += Field_OnPreRenderText;
        }
    }

    private void Field_OnPreRenderText(TMP_TextInfo obj) {
        Debug.Log($"{gameObject.name}: {string.Join(' ', obj.characterInfo.Select(c => $"{c.character} ({c.textElement?.glyphIndex})"))}");
        if (rtlField != null) {
            Debug.Log($"{gameObject.name}: {rtlField.text}");
        }
    }

    [UnityEngine.ContextMenu(nameof(Categorize))]
    public void Categorize() {
        Dictionary<int, List<TextMeshProUGUI>> categories = new Dictionary<int, List<TextMeshProUGUI>>();
        Dictionary<int, string> hashes = new Dictionary<int, string>();
        foreach (var field in fields) {
            var hash = field.text.GetHashCode();
            if (!categories.ContainsKey(hash)) {
                hashes[hash] = field.text;
                categories[hash] = new List<TextMeshProUGUI>();
            }
            categories[hash].Add(field);
        }
        foreach (var category in categories) {
            int hash = category.Key;
            string text = hashes[hash];
            var font = category.Value.First().font;
            Debug.Log($"{DisplayCharInfo(text, font)} ({text}): {string.Join(", ", category.Value.Select(f => f.name))}");
        }
    }

    public string DisplayCharInfo(string text, TMP_FontAsset fontAsset) {
        string result = "";
        foreach (var c in text) {
            result += $"[{c} ({ConvertCharToHex(c)})/{ConvertCharToGlyphID(c, fontAsset)}] ";
        }
        return result;
    }

    public string ConvertCharToHex(char c) {
        return System.Text.Encoding.Unicode.GetBytes(c.ToString()).Select(b => $"{b:X}").Reverse().Aggregate((a, b) => $"{a}{b}");
    }

    public int ConvertCharToRTLTMProGlyphID(char c) {
        return GlyphTable.Convert(c);
    }

    public uint? ConvertCharToGlyphID(char c, TMP_FontAsset fontAsset) {
        fontAsset.characterLookupTable.TryGetValue(c, out var glyphIndex);
        if (glyphIndex != null) {
            return glyphIndex.glyphIndex;
        }
        return null;
    }

    [UnityEngine.ContextMenu(nameof(ColorizeAllFields))]
    public void ColorizeAllFields() {
        foreach (var field in fields) {
            ColorEachCharacterByItsInt(field);
        }
    }

    public void ColorEachCharacterByItsInt(TextMeshProUGUI field) {
        string result = "";
        foreach (var c in field.text) {
            result += $"<color=#{ConvertCharToHex(c)}>{c}</color>";
        }
        UnityEditor.Undo.RecordObject(field, "Color each character by its int");
        field.text = result;
    }
}
