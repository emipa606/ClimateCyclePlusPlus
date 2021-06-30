using Verse;

namespace ClimateCyclePlusPlus
{
    public class CycleSettings : ModSettings
    {
        private const string defaultCycleType = "Regular Cycle";
        public float cycleMultiplier = 5f;

        public float cyclePeriods = 4f;
        public string cycleType = defaultCycleType;

        //public bool fuckMyShitUp = false;
        //public bool reallyFuckItUp = false;
        //not today.

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref cyclePeriods, "cyclePeriods", 4f);
            Scribe_Values.Look(ref cycleMultiplier, "cycleMultiplier", 5f);
            Scribe_Values.Look(ref cycleType, "cycleType", "Regular Cycle");
        }
    }
}