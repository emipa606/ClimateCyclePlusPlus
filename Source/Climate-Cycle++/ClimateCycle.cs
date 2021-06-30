using Mlie;
using UnityEngine;
using Verse;

namespace ClimateCyclePlusPlus
{
    internal class ClimateCycle : Mod
    {
        private static string currentVersion;

        public static readonly string[] cycleTypes =
            {"Regular Cycle", "Winter is coming", "Waiting for the Sun", "Normal Summer, Cold Winter"};

        private readonly CycleSettings settings;

        public ClimateCycle(ModContentPack content) : base(content)
        {
            currentVersion =
                VersionFromManifest.GetVersionFromModMetaData(
                    ModLister.GetActiveModWithIdentifier("Mlie.ClimateCyclePlusPlus"));
            settings = GetSettings<CycleSettings>();
            GameCondition_ClimateCyclePlusPlus.settings = settings;
        }

        public override string SettingsCategory()
        {
            return "Climate Cycle++";
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            var listing_Standard = new Listing_Standard();
            listing_Standard.Begin(inRect);
            //listing_Standard.AddLabeledCheckbox("CCPP_ColdNightHotDays".Translate(), ref settings.showWeather);
            //listing_Standard.AddHorizontalLine(3f);
            listing_Standard.AddLabeledRadioList($"{"CCPP_CycleTypeChoice".Translate()}:", cycleTypes,
                ref settings.cycleType);
            listing_Standard.GapLine(3f);
            listing_Standard.AddLabeledNumericalTextField("CCPP_Multiplier".Translate(), ref settings.cycleMultiplier,
                minValue: 1f, maxValue: 20f);
            listing_Standard.AddLabeledNumericalTextField("CCPP_Periods".Translate(), ref settings.cyclePeriods,
                minValue: 1f, maxValue: 8f);
            if (currentVersion != null)
            {
                listing_Standard.Gap();
                GUI.contentColor = Color.gray;
                listing_Standard.Label("CurrentModVersion_Label".Translate(currentVersion));
                GUI.contentColor = Color.white;
            }

            listing_Standard.End();
            settings.Write();
        }
    }
}