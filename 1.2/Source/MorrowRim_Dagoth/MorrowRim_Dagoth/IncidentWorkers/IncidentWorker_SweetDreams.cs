using Verse;
using RimWorld;

namespace MorrowRim_Dagoth
{
    class IncidentWorker_SweetDreams : IncidentWorker
    {
        protected override bool CanFireNowSub(IncidentParms parms)
        {
			return base.CanFireNowSub(parms) && Utility.DagothIncidentCheck() && Utility.ColonyPawnSleepingTrueCheck(parms.target as Map);
        }

		protected override bool TryExecuteWorker(IncidentParms parms)
		{
			string text2;
			//adds variables into message
			text2 = string.Format(this.def.letterText, new object[]
			{
				Faction.OfPlayer.def.pawnsPlural
			});
			base.SendStandardLetter(this.def.label, text2, this.def.letterDef, parms, null);
			return true;
		}
	}
}
