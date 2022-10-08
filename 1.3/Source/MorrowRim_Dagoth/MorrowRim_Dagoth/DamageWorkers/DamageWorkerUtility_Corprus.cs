using RimWorld;
using Verse;
using System;
using System.Linq;

namespace MorrowRim_Dagoth
{
    public class DamageWorkerUtility_Corprus
    {

        private static readonly String[] immuneDefs = { PawnKindDefOf.MorrowRim_CorprusLame.defName, PawnKindDefOf.MorrowRim_CorprusStalker.defName, PawnKindDefOf.MorrowRim_CorprusBloated.defName,
        "Dagoth_Slave", "Dagoth_Zombie", "Dagoth_Ghoul", "Dagoth_Vampire", "Dagoth_Ascended"};

        public static bool CanInfectPawn(Pawn p)
        {
            return p.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.MorrowRim_Corprus, false) == null
                && p.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.MorrowRim_CorprusSurvived, false) == null
                && p.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.MorrowRim_CorprusPermanent, false) == null
                && p.RaceProps.IsFlesh && !immuneDefs.Contains(p.def.defName);
        }

        public static void InfectPawn(Pawn p)
        {
            p.health.AddHediff(HediffDefOf.MorrowRim_Corprus).Severity = 0.01f;
            Messages.Message("MorrowRim_Notification_CorprusInfected".Translate(p.Name), p, MessageTypeDefOf.NegativeHealthEvent, true);
        }
    }
}
