using Verse;

namespace MorrowRim_Dagoth
{
    class DamageWorker_DagothInjury : DamageWorker_AddInjury
    {
        protected override BodyPartRecord ChooseHitPart(DamageInfo dinfo, Pawn pawn)
        {
            if (DamageWorkerUtility_Corprus.CanInfectPawn(pawn))
            {
                DamageWorkerUtility_Corprus.InfectPawn(pawn);
            }
            return base.ChooseHitPart(dinfo, pawn);
        }
    }
}
