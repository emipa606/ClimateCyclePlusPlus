using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;

namespace ClimateCyclePlusPlus
{
    //"Adapted" from RimWorld.Alert_Hypothermia
    public class Alert_Heatstroke : Alert_Critical
    {
        public Alert_Heatstroke()
        {
            defaultLabel = "AlertHeatstroke".Translate();
        }

        private IEnumerable<Pawn> HeatstrokeDangerColonists
        {
            get
            {
                foreach (var p in PawnsFinder.AllMaps_FreeColonistsSpawned)
                {
                    if (p.SafeTemperatureRange().Includes(p.AmbientTemperature))
                    {
                        continue;
                    }

                    var heatstroke = p.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.Heatstroke);
                    if (heatstroke != null && heatstroke.CurStageIndex >= 3)
                    {
                        yield return p;
                    }
                }
            }
        }

        public override TaggedString GetExplanation()
        {
            var stringBuilder = new StringBuilder();
            foreach (var current in HeatstrokeDangerColonists)
            {
                stringBuilder.AppendLine("    " + current.NameShortColored);
            }

            return "AlertHeatstrokeDesc".Translate(stringBuilder.ToString());
        }

        public override AlertReport GetReport()
        {
            return AlertReport.CulpritsAre(HeatstrokeDangerColonists.ToList());
        }
    }
}