using System;
using Verse;
using RimWorld;

namespace MorrowRim_Dagoth
{
    [DefOf]
    public static class PawnKindDefOf
    {
        static PawnKindDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(PawnKindDefOf));
        }
        //Corprus
        public static PawnKindDef MorrowRim_CorprusStalker;
        public static PawnKindDef MorrowRim_CorprusLame;
        public static PawnKindDef MorrowRim_CorprusBloated;

        //Ash creatures
        public static PawnKindDef Dagoth_Slave;
        public static PawnKindDef Dagoth_Zombie;
        public static PawnKindDef Dagoth_Ghoul;
        public static PawnKindDef Dagoth_Vampire;
        public static PawnKindDef Dagoth_Ascended;

    }
}
