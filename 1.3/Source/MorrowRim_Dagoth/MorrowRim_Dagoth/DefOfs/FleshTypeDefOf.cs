using RimWorld;

namespace MorrowRim_Dagoth
{
    [DefOf]
    public static class FleshTypeDefOf
    {
        static FleshTypeDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(FleshTypeDefOf));
        }

        public static FleshTypeDef MorrowRim_AshBeast;

    }
}