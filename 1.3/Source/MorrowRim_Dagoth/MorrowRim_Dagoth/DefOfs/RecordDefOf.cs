using System;
using Verse;
using RimWorld;

namespace MorrowRim_Dagoth
{
    [DefOf]
    public static class RecordDefOf
    {
        static RecordDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(RecordDefOf));
        }
        public static RecordDef MorrowRim_DagothDreamsTargeted;
    }
}