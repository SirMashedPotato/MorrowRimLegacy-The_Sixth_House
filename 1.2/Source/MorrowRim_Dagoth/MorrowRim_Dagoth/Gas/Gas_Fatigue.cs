﻿using System;
using Verse;
using RimWorld;
using System.Collections.Generic;
using Verse.AI;
using UnityEngine;


namespace MorrowRim_Dagoth
{
    public class Gas_Fatigue : Gas
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
                    //make pawns vomit
                    HashSet<Thing> hashSet = new HashSet<Thing>(this.Position.GetThingList(this.Map));
                    if (hashSet != null)
                    {
                        foreach (Thing thing in hashSet)
                        {
                            //check if is pawn
                            if (thing is Pawn)
                            {
                                Pawn p = thing as Pawn;
                                if (!p.RaceProps.IsFlesh || p.RaceProps.needsRest == false)
                                {
                                    return;
                                }
                                float num = 0.028758334f;
                                if (num != 0f && !this.Destroyed)
                                {
                                    float num2 = Mathf.Lerp(0.85f, 1.15f, Rand.ValueSeeded(p.thingIDNumber ^ 74374237));
                                    num *= num2;
                                    p.needs.rest.CurLevel -= num * 5;
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
