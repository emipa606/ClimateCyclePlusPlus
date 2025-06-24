using Mlie;
using UnityEngine;
using Verse;

namespace ClimateCyclePlusPlus;

internal class ClimateCycle : Mod
{
    private static string currentVersion;

    private static readonly string[] cycleTypes =
        ["Regular Cycle", "Winter is coming", "Waiting for the Sun", "Normal Summer, Cold Winter"];

    private readonly CycleSettings settings;

    public ClimateCycle(ModContentPack content) : base(content)
    {
        currentVersion =
            VersionFromManifest.GetVersionFromModMetaData(content.ModMetaData);
        settings = GetSettings<CycleSettings>();
        GameCondition_ClimateCyclePlusPlus.Settings = settings;
    }

    public override string SettingsCategory()
    {
        return "Climate Cycle++";
    }

    public override void DoSettingsWindowContents(Rect inRect)
    {
        var listingStandard = new Listing_Standard();
        listingStandard.Begin(inRect);
        listingStandard.AddLabeledRadioList($"{"CCPP_CycleTypeChoice".Translate()}:", cycleTypes,
            ref settings.CycleType);
        listingStandard.GapLine(3f);
        listingStandard.AddLabeledNumericalTextField("CCPP_Multiplier".Translate(), ref settings.CycleMultiplier,
            minValue: 1f, maxValue: 20f);
        listingStandard.AddLabeledNumericalTextField("CCPP_Periods".Translate(), ref settings.CyclePeriods,
            minValue: 1f, maxValue: 8f);
        if (currentVersion != null)
        {
            listingStandard.Gap();
            GUI.contentColor = Color.gray;
            listingStandard.Label("CurrentModVersion_Label".Translate(currentVersion));
            GUI.contentColor = Color.white;
        }

        listingStandard.End();
        settings.Write();
    }
}