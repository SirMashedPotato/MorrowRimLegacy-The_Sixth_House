using Verse;

namespace MorrowRim_Dagoth
{
	public class DamageWorker_CorprusScratch : DamageWorker_Scratch
	{
		protected override BodyPartRecord ChooseHitPart(DamageInfo dinfo, Pawn pawn)
		{
			if (DamageWorkerUtility_Corprus.CanInfectPawn(pawn))
			{
				DamageWorkerUtility_Corprus.InfectPawn(pawn);
			}
			return pawn.health.hediffSet.GetRandomNotMissingPart(dinfo.Def, dinfo.Height, BodyPartDepth.Outside, null);
		}
	}
}
