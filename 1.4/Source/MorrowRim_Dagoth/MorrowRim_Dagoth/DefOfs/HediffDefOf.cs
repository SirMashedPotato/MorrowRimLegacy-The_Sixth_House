using System;
using Verse;
using RimWorld;

namespace MorrowRim_Dagoth
{
    [DefOf]
    public static class HediffDefOf
    {
        static HediffDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(HediffDefOf));
        }

        //for MorrowRim
        public static HediffDef MorrowRim_Corprus;
        public static HediffDef MorrowRim_CorprusPermanent;
        public static HediffDef MorrowRim_CorprusSurvived;

        //other
        public static HediffDef Dagoth_BlightBeGoneHigh;
        public static HediffDef Dagoth_HiddenTouched;

        //debuffs
        public static HediffDef Dagoth_DisturbedSleep;
        public static HediffDef Dagoth_Dreamer;
        public static HediffDef Dagoth_SleepParalysis;

        //gifts
        public static HediffDef Dagoth_GiftOfDagothUr_Flesh;
        public static HediffDef Dagoth_GiftOfDagothUr_Strength;
        public static HediffDef Dagoth_GiftOfDagothUr_Speed;
        public static HediffDef Dagoth_GiftOfDagothUr_Sight;
    }
}
