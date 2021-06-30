using RimWorld;
using UnityEngine;
using Verse;

namespace ClimateCyclePlusPlus
{
    public class GameCondition_ClimateCyclePlusPlus : GameCondition
    {
        public static CycleSettings settings;
        private int ticksOffset;

        public override void Init()
        {
            ticksOffset = Rand.Value >= 0.5f ? (int) settings.cyclePeriods / 2 : 0;
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref ticksOffset, "ticksOffset");
        }

        //one day this'll be convoluted enough for me to need comments.
        public override float TemperatureOffset()
        {
            if (settings.cycleType == "Winter is coming")
            {
                return (Mathf.Sin((GenDate.YearsPassedFloat + ticksOffset) / settings.cyclePeriods * Mathf.PI * 2f) *
                        20f) - (GenDate.YearsPassedFloat * settings.cycleMultiplier) - 20f;
            }

            if (settings.cycleType == "Waiting for the Sun")
            {
                return (Mathf.Sin((GenDate.YearsPassedFloat + ticksOffset) / settings.cyclePeriods * Mathf.PI * 2f) *
                        20f) + (GenDate.YearsPassedFloat * settings.cycleMultiplier);
            }

            if (settings.cycleType != "Normal Summer, Cold Winter")
            {
                return Mathf.Sin((GenDate.YearsPassedFloat + ticksOffset) / settings.cyclePeriods * Mathf.PI * 2f) *
                       (20f + (GenDate.YearsPassedFloat * settings.cycleMultiplier));
            }

            if (GenDate.Season(Find.TickManager.TicksAbs, Find.WorldGrid.LongLatOf(SingleMap.Tile)) == Season.Fall)
            {
                return (Mathf.Sin((GenDate.YearsPassedFloat + ticksOffset) / settings.cyclePeriods * Mathf.PI *
                                  2f) * 20f) - (GenDate.YearsPassedFloat * settings.cycleMultiplier / 2) - 20f;
            }

            if (GenDate.Season(Find.TickManager.TicksAbs, Find.WorldGrid.LongLatOf(SingleMap.Tile)) ==
                Season.Winter)
            {
                return (Mathf.Sin((GenDate.YearsPassedFloat + ticksOffset) / settings.cyclePeriods * Mathf.PI *
                                  2f) * 20f) - (GenDate.YearsPassedFloat * settings.cycleMultiplier) -
                       (GenDate.YearsPassedFloat * settings.cycleMultiplier) - 20f;
            }

            return 0f;
        }
    }
}