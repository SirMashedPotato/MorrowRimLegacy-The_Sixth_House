using RimWorld;

namespace MorrowRim_Dagoth
{
    [DefOf]
    public static class FactionDefOf
    {
        static FactionDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(FactionDefOf));
        }

        public static FactionDef MorrowRim_Corprus;
        public static FactionDef MorrowRim_SixthHouse;

    }
}