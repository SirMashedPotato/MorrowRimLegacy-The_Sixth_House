using System;
using Verse;
using RimWorld;

namespace MorrowRim_Dagoth
{
    [DefOf]
    public static class RulePackDefOf
    {
        static RulePackDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(RulePackDefOf));
        }

        public static RulePackDef MorrowRim_NamerSixthHouseMember;
    }
}
