using System;
using RimWorld;

namespace MorrowRim_Dagoth
{
    [DefOf]
    public static class TraitDefOf
    {
        static TraitDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(TraitDefOf));
        }

        public static TraitDef MorrowRim_TouchedByDagothUr;
    }
}
