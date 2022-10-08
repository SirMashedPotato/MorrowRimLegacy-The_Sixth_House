using RimWorld;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Verse;
using Verse.AI;
using System.Linq;
using RimWorld.Planet;

namespace MorrowRim_Dagoth
{
    class Comp_CorprusSpawnInit : ThingComp
    {
        private bool doneAlready = false;

        public void ExposeData()
        {
            Scribe_Values.Look<bool>(ref this.doneAlready, "summonOnce", true, false);
        }

        public override void CompTick()
        {
            base.CompTick();
            if (!doneAlready && parent.Spawned)
            {
                Pawn p = parent as Pawn;
                p.health.Reset();
                p.health.AddHediff(HediffDefOf.MorrowRim_CorprusPermanent);

                //check for dagoth faction
                IEnumerable<Pawn> pawns = p?.Map?.mapPawns?.AllPawnsSpawned?.Where(x => x?.Faction == FactionUtility.DefaultFactionFrom(FactionDefOf.MorrowRim_SixthHouse));
                if (pawns.Any()) p.SetFaction(FactionUtility.DefaultFactionFrom(FactionDefOf.MorrowRim_SixthHouse));

                if (p.Name == null)
                {
                    p.Name = PawnBioAndNameGenerator.GeneratePawnName(p, NameStyle.Full);
                } else if (p.Faction == FactionUtility.DefaultFactionFrom(FactionDefOf.MorrowRim_Corprus))
                {
                    p.mindState.mentalStateHandler.TryStartMentalState(MentalStateDefOf.ManhunterPermanent);
                }
                doneAlready = true;
            }
        }
    }
}
