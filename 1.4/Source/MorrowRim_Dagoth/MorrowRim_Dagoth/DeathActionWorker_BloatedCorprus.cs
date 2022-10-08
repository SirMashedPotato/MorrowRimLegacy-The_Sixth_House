using Verse;
using RimWorld;

namespace MorrowRim_Dagoth
{
    class DeathActionWorker_BloatedCorprus : DeathActionWorker
    {

        public override RulePackDef DeathRules
        {
            get
            {
                return RimWorld.RulePackDefOf.Transition_DiedExplosive;
            }
        }

        public override bool DangerousInMelee
        {
            get
            {
                return true;
            }
        }

        public override void PawnDied(Corpse corpse)
        {
            GenExplosion.DoExplosion(corpse.Position, corpse.Map, 1.9f, DamageDefOf.Burn, corpse.InnerPawn, -1, -1f, null, null, null, null, ThingDefOf.Gas_Corprus, 1f, 1, null, false, null, 0f, 1, 0f, false, null, null, null, true, 1f, 0f, true, null, 1f);
            FilthMaker.TryMakeFilth(corpse.Position, corpse.Map, RimWorld.ThingDefOf.Filth_CorpseBile, 3);
            if (!corpse.Destroyed)
            {
                corpse.Destroy();
            }
        }

    }
}
