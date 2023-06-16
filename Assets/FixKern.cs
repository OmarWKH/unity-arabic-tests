using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;

public class FixKern : MonoBehaviour
{
    [SerializeField] private TMP_FontAsset fontAsset;

    public TMP_FontFeatureTable fontFeatureTable = new TMP_FontFeatureTable()
    {
        multipleSubstitutionRecords = new List<MultipleSubstitutionRecord>(),
        ligatureRecords = new List<LigatureSubstitutionRecord>(),
        glyphPairAdjustmentRecords = new List<GlyphPairAdjustmentRecord>(),
        MarkToBaseAdjustmentRecords = new List<MarkToBaseAdjustmentRecord>(),
        MarkToMarkAdjustmentRecords = new List<MarkToMarkAdjustmentRecord>(),
    };

    void Start()
    {
        if (fontAsset != null)
        {
            fontFeatureTable = fontAsset.fontFeatureTable;
        }

    }

    [UnityEngine.ContextMenu(nameof(ModifyGlyphAdjustmentTable))]
    public void ModifyGlyphAdjustmentTable()
    {
        var records = fontAsset.fontFeatureTable.glyphPairAdjustmentRecords;
        var newRecords = new List<GlyphPairAdjustmentRecord>();
        foreach (var record in records)
        {
            var firstAdjustmentRecord = record.firstAdjustmentRecord;
            var firstValue = firstAdjustmentRecord.glyphValueRecord;
            firstValue.xPlacement = 0;
            firstAdjustmentRecord.glyphValueRecord = firstValue;
            newRecords.Add(new GlyphPairAdjustmentRecord()
            {
                firstAdjustmentRecord = firstAdjustmentRecord,
                secondAdjustmentRecord = record.secondAdjustmentRecord,
            });
        }
        fontAsset.fontFeatureTable.glyphPairAdjustmentRecords = newRecords;
    }
}
