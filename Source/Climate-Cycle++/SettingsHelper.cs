using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace ClimateCyclePlusPlus;

//thanks to AlexTD and Why_is_that for the below
internal static class SettingsHelper
{
    public static Rect GetRect(this Listing_Standard listing_Standard, float? height = null)
    {
        return listing_Standard.GetRect(height ?? Text.LineHeight);
    }

    public static void AddLabeledRadioList(this Listing_Standard listing_Standard, string header, string[] labels,
        ref string val, float? headerHeight = null)
    {
        //listing_Standard.Gap();
        if (header != string.Empty)
        {
            Widgets.Label(listing_Standard.GetRect(headerHeight), header);
        }

        listing_Standard.AddRadioList(GenerateLabeledRadioValues(labels), ref val);
    }

    private static void AddRadioList<T>(this Listing_Standard listing_Standard, List<LabeledRadioValue<T>> items,
        ref T val, float? height = null)
    {
        foreach (var item in items)
        {
            //listing_Standard.Gap();
            var lineRect = listing_Standard.GetRect(height);
            if (Widgets.RadioButtonLabeled(lineRect, item.Label,
                    EqualityComparer<T>.Default.Equals(item.Value, val)))
            {
                val = item.Value;
            }
        }
    }

    private static List<LabeledRadioValue<string>> GenerateLabeledRadioValues(string[] labels)
    {
        var list = new List<LabeledRadioValue<string>>();
        foreach (var label in labels)
        {
            list.Add(new LabeledRadioValue<string>(label, label));
        }

        return list;
    }

    public static void AddLabeledNumericalTextField<T>(this Listing_Standard listing_Standard, string label,
        ref T settingsValue, float leftPartPct = 0.5f, float minValue = 1f, float maxValue = 100000f)
        where T : struct
    {
        listing_Standard.Gap();
        listing_Standard.LineRectSpilter(out var leftHalf, out var rightHalf, leftPartPct);

        Widgets.Label(leftHalf, label);

        var buffer = settingsValue.ToString();
        Widgets.TextFieldNumeric(rightHalf, ref settingsValue, ref buffer, minValue, maxValue);
    }

    public static void LineRectSpilter(this Listing_Standard listing_Standard, out Rect leftHalf,
        out Rect rightHalf, float leftPartPct = 0.5f, float? height = null)
    {
        var lineRect = listing_Standard.LineRectSpilter(out leftHalf, leftPartPct, height);
        rightHalf = lineRect.RightPart(1f - leftPartPct).Rounded();
    }

    public static Rect LineRectSpilter(this Listing_Standard listing_Standard, out Rect leftHalf,
        float leftPartPct = 0.5f, float? height = null)
    {
        var lineRect = listing_Standard.GetRect(height);
        leftHalf = lineRect.LeftPart(leftPartPct).Rounded();
        return lineRect;
    }

    public class LabeledRadioValue<T>(string label, T val)
    {
        public string Label { get; } = label;

        public T Value { get; } = val;
    }
}