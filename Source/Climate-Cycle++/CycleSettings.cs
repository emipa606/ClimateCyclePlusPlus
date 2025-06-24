using Verse;

namespace ClimateCyclePlusPlus;

public class CycleSettings : ModSettings
{
    private const string DefaultCycleType = "Regular Cycle";
    public float CycleMultiplier = 5f;

    public float CyclePeriods = 4f;
    public string CycleType = DefaultCycleType;

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref CyclePeriods, "cyclePeriods", 4f);
        Scribe_Values.Look(ref CycleMultiplier, "cycleMultiplier", 5f);
        Scribe_Values.Look(ref CycleType, "cycleType", "Regular Cycle");
    }
}