using System;
using Verse;
using RimWorld;

namespace MorrowRim_Dagoth
{
    [DefOf]
    public static class ThingDefOf
    {
        static ThingDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(ThingDefOf));
        }
        public static ThingDef Gas_Corprus;
    }
}
