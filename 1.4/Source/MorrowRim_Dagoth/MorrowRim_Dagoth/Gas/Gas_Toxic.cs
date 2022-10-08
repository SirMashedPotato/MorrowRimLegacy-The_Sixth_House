using System;
using Verse;
using RimWorld;
using System.Collections.Generic;
using Verse.AI;
using UnityEngine;


namespace MorrowRim_Dagoth
{
    public class Gas_Toxic : Gas
    {
        private int tickerInterval = 0;
        private int tickerMax = 120;

        public override void Tick()
        {
            base.Tick();
            try
            {
                if (tickerInterval >= tickerMax)
                {
                    HashSet<Thing> hashSet = new HashSet<Thing>(this.Position.GetThingList(this.Map));
                    if (hashSet != null)
                    {
                        foreach (Thing thing in hashSet)
                        {
                            if (thing is Pawn)
                            {
                                Pawn p = thing as Pawn;
                                if (!p.RaceProps.IsFlesh)
                                {
                                    return;
                                }
                                float num = 0.028758334f;
                                num *= Mathf.Max(1f - p.GetStatValue(StatDefOf.ToxicResistance, true, -1), 0f);
                                if (num != 0f && !this.Destroyed)
                                {
                                    float num2 = Mathf.Lerp(0.85f, 1.15f, Rand.ValueSeeded(p.thingIDNumber ^ 74374237));
                                    num *= num2;
                                    HealthUtility.AdjustSeverity(p, RimWorld.HediffDefOf.ToxicBuildup, num * 2);
                                }
                            }
                        }
                    }
                    tickerInterval = 0;
                }
                tickerInterval++;
            }
            catch (NullReferenceException e)
            {

            }
        }
    }

}
