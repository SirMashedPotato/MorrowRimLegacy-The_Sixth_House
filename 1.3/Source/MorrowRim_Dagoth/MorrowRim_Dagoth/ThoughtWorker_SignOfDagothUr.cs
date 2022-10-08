using System;
using Verse;
using RimWorld;

namespace MorrowRim_Dagoth.ThoughtWorkers
{
    public class ThoughtWorker_SignOfDagothUr : ThoughtWorker
    {
        protected override ThoughtState CurrentStateInternal(Pawn p)
        {
            return p.Awake() && CheckForSign(p);
        }

        public bool CheckForSign(Pawn p)
        {
            return (p.Map.weatherManager.curWeather.defName == "MorrowRim_BlightStorm" && !p.Position.Roofed(p.Map));
        }
    }
}
