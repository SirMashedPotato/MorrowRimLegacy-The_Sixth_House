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
    class Comp_GiftedSpawnInit : ThingComp
    {
        private bool doneAlready = false;

        protected CompProperties_GiftedSpawnInit Props
        {
            get
            {
                return (CompProperties_GiftedSpawnInit)this.props;
            }
        }

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
                if (p.Name == null && Rand.Chance(this.Props.chance))
                {
                    string name = NameGenerator.GenerateName(RulePackDefOf.MorrowRim_NamerSixthHouseMember);
                    NameSingle nameSingle = new NameSingle(name);
                    p.Name = nameSingle;
                    p.health.Reset();
                    int i = Rand.RangeInclusive(1, 4);
                    switch (i)
                    {
                        case 1:
                            p.health.AddHediff(HediffDefOf.Dagoth_GiftOfDagothUr_Flesh);
                            break;
                        case 2:
                            p.health.AddHediff(HediffDefOf.Dagoth_GiftOfDagothUr_Sight);
                            break;
                        case 3:
                            p.health.AddHediff(HediffDefOf.Dagoth_GiftOfDagothUr_Speed);
                            break;
                        case 4:
                            p.health.AddHediff(HediffDefOf.Dagoth_GiftOfDagothUr_Strength);
                            break;
                        default:
                            p.health.AddHediff(HediffDefOf.Dagoth_GiftOfDagothUr_Flesh);
                            break;
                    }
                }
                doneAlready = true;
            }
        }
    }
}
