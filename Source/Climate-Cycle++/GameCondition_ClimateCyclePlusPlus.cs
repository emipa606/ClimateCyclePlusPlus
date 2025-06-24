using RimWorld;
using UnityEngine;
using Verse;

namespace ClimateCyclePlusPlus;

public class GameCondition_ClimateCyclePlusPlus : GameCondition
{
    public static CycleSettings Settings;
    private int ticksOffset;

    public override void Init()
    {
        ticksOffset = Rand.Value >= 0.5f ? (int)Settings.CyclePeriods / 2 : 0;
    }

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref ticksOffset, "ticksOffset");
    }

    //one day this'll be convoluted enough for me to need comments.
    public override float TemperatureOffset()
    {
        switch (Settings.CycleType)
        {
            case "Winter is coming":
                return (Mathf.Sin((GenDate.YearsPassedFloat + ticksOffset) / Settings.CyclePeriods * Mathf.PI * 2f) *
                        20f) - (GenDate.YearsPassedFloat * Settings.CycleMultiplier) - 20f;
            case "Waiting for the Sun":
                return (Mathf.Sin((GenDate.YearsPassedFloat + ticksOffset) / Settings.CyclePeriods * Mathf.PI * 2f) *
                        20f) + (GenDate.YearsPassedFloat * Settings.CycleMultiplier);
        }

        if (Settings.CycleType != "Normal Summer, Cold Winter")
        {
            return Mathf.Sin((GenDate.YearsPassedFloat + ticksOffset) / Settings.CyclePeriods * Mathf.PI * 2f) *
                   (20f + (GenDate.YearsPassedFloat * Settings.CycleMultiplier));
        }

        if (GenDate.Season(Find.TickManager.TicksAbs, Find.WorldGrid.LongLatOf(SingleMap.Tile)) == Season.Fall)
        {
            return (Mathf.Sin((GenDate.YearsPassedFloat + ticksOffset) / Settings.CyclePeriods * Mathf.PI *
                              2f) * 20f) - (GenDate.YearsPassedFloat * Settings.CycleMultiplier / 2) - 20f;
        }

        if (GenDate.Season(Find.TickManager.TicksAbs, Find.WorldGrid.LongLatOf(SingleMap.Tile)) ==
            Season.Winter)
        {
            return (Mathf.Sin((GenDate.YearsPassedFloat + ticksOffset) / Settings.CyclePeriods * Mathf.PI *
                              2f) * 20f) - (GenDate.YearsPassedFloat * Settings.CycleMultiplier) -
                   (GenDate.YearsPassedFloat * Settings.CycleMultiplier) - 20f;
        }

        return 0f;
    }
}