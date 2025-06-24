using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace ClimateCyclePlusPlus;

//thanks to AlexTD and Why_is_that for the below
internal static class SettingsHelper
{
    private static Rect getRect(this Listing_Standard listingStandard, float? height = null)
    {
        return listingStandard.GetRect(height ?? Text.LineHeight);
    }

    public static void AddLabeledRadioList(this Listing_Standard listingStandard, string header, string[] labels,
        ref string val, float? headerHeight = null)
    {
        if (header != string.Empty)
        {
            Widgets.Label(listingStandard.getRect(headerHeight), header);
        }

        listingStandard.addRadioList(generateLabeledRadioValues(labels), ref val);
    }

    private static void addRadioList<T>(this Listing_Standard listing_Standard, List<LabeledRadioValue<T>> items,
        ref T val, float? height = null)
    {
        foreach (var item in items)
        {
            var lineRect = listing_Standard.getRect(height);
            if (Widgets.RadioButtonLabeled(lineRect, item.Label,
                    EqualityComparer<T>.Default.Equals(item.Value, val)))
            {
                val = item.Value;
            }
        }
    }

    private static List<LabeledRadioValue<string>> generateLabeledRadioValues(string[] labels)
    {
        var list = new List<LabeledRadioValue<string>>();
        foreach (var label in labels)
        {
            list.Add(new LabeledRadioValue<string>(label, label));
        }

        return list;
    }

    public static void AddLabeledNumericalTextField<T>(this Listing_Standard listingStandard, string label,
        ref T settingsValue, float leftPartPct = 0.5f, float minValue = 1f, float maxValue = 100000f)
        where T : struct
    {
        listingStandard.Gap();
        listingStandard.lineRectSplitter(out var leftHalf, out var rightHalf, leftPartPct);

        Widgets.Label(leftHalf, label);

        var buffer = settingsValue.ToString();
        Widgets.TextFieldNumeric(rightHalf, ref settingsValue, ref buffer, minValue, maxValue);
    }

    private static void lineRectSplitter(this Listing_Standard listingStandard, out Rect leftHalf,
        out Rect rightHalf, float leftPartPct = 0.5f, float? height = null)
    {
        var lineRect = listingStandard.lineRectSplitter(out leftHalf, leftPartPct, height);
        rightHalf = lineRect.RightPart(1f - leftPartPct).Rounded();
    }

    private static Rect lineRectSplitter(this Listing_Standard listingStandard, out Rect leftHalf,
        float leftPartPct = 0.5f, float? height = null)
    {
        var lineRect = listingStandard.getRect(height);
        leftHalf = lineRect.LeftPart(leftPartPct).Rounded();
        return lineRect;
    }

    private class LabeledRadioValue<T>(string label, T val)
    {
        public string Label { get; } = label;

        public T Value { get; } = val;
    }
}