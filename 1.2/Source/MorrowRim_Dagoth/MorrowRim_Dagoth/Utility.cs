using RimWorld;
using Verse;
using System.Linq;
using System.Collections.Generic;

namespace MorrowRim_Dagoth
{
    public class Utility
    {
        //faction checks
        public static bool DagothIncidentCheck() //Compound check
        {
            //return DagothFactionValidEnemy() && NightCheck(m);
            return DagothFactionStatusCheck() && DagothFactionHostileCheck();
        }

        public static bool DagothFactionStatusCheck()
        {
            return !FactionUtility.DefaultFactionFrom(FactionDefOf.MorrowRim_SixthHouse).defeated;
        }

        public static bool DagothFactionHostileCheck()
        {
            return FactionUtility.DefaultFactionFrom(FactionDefOf.MorrowRim_SixthHouse).HostileTo(Faction.OfPlayer);
        }

        //time checks
        /*public static bool NightCheck(Map m)
        {
            return GenLocalDate.HourOfDay(m) >= 22 || GenLocalDate.HourOfDay(m) <= 2;
        }*/

        //pawn checks
        public static bool ColonyPawnSleepingTrueCheck(Map map)
        {
            return GetColonyPawnSleeping(map).Any<Pawn>();
        }

        //pawn gets
        public static IEnumerable<Pawn> GetColonyPawnSleepingTouched(Map map) //Compound check
        {
            IEnumerable<Pawn> temp = GetColonyPawnSleeping(map);
            return GetColonyPawnTouched(temp);
        }

        public static IEnumerable<Pawn> GetColonyPawnSleepingNotTouched(Map map) //Compound check
        {
            IEnumerable<Pawn> temp = GetColonyPawnSleeping(map);
            return GetColonyPawnNotTouched(temp);
        }

        public static IEnumerable<Pawn> GetColonyPawnSleeping(Map map)
        {
            return map.mapPawns.FreeColonistsAndPrisoners.Where(x => !x.Awake());
        }

        public static IEnumerable<Pawn> GetColonyPawnTouched(Map map)
        {
            return map.mapPawns.FreeColonistsAndPrisoners.Where(x => x.story.traits.HasTrait(TraitDefOf.MorrowRim_TouchedByDagothUr));
        }

        public static IEnumerable<Pawn> GetColonyPawnTouched(IEnumerable<Pawn> initial)
        {
            return initial.Where(x => x.story.traits.HasTrait(TraitDefOf.MorrowRim_TouchedByDagothUr));
        }

        public static IEnumerable<Pawn> GetColonyPawnNotTouched(Map map)
        {
            return map.mapPawns.FreeColonistsAndPrisoners.Where(x => !x.story.traits.HasTrait(TraitDefOf.MorrowRim_TouchedByDagothUr));
        }

        public static IEnumerable<Pawn> GetColonyPawnNotTouched(IEnumerable<Pawn> initial)
        {
            return initial.Where(x => !x.story.traits.HasTrait(TraitDefOf.MorrowRim_TouchedByDagothUr));
        }

        /* Misc */

        public static void IncrementRecordMulti(List<Pawn> pawns, RecordDef record)
        {
            foreach(Pawn pawn in pawns)
            pawn.records.Increment(record);
        }
    }
}
