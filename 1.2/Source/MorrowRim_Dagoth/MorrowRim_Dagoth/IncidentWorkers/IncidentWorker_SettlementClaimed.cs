using System.Collections.Generic;
using System.Linq;
using RimWorld.Planet;
using Verse;
using RimWorld;

namespace MorrowRim_Dagoth
{
    class IncidentWorker_SettlementClaimed : IncidentWorker
    {
        protected override bool CanFireNowSub(IncidentParms parms)
        {
			return base.CanFireNowSub(parms) && this.PotentialVictims(parms.target).Any<Settlement>() && Utility.DagothIncidentCheck();
		}

		protected IEnumerable<Settlement> PotentialVictims(IIncidentTarget target)
		{
			World world = target as World;
			if (world != null)
			{
				return world.worldObjects.Settlements.FindAll(x => x.Faction != Faction.OfPlayer && x.Faction.def != FactionDefOf.MorrowRim_SixthHouse 
				&& x.Faction.def != Faction.Empire.def && x.Faction.def.techLevel != TechLevel.Animal && x.Faction.def.defName != "GreatHouseTelvanni");
			}
			return null;
		}

		protected override bool TryExecuteWorker(IncidentParms parms)
		{
			//take settlement
			Settlement settlement = this.ActualVictims(parms).ToList<Settlement>().First();
			Faction faction = FactionUtility.DefaultFactionFrom(FactionDefOf.MorrowRim_SixthHouse);
			string oldFaction = settlement.Faction.ToString();
			settlement.SetFaction(faction);

			string text2 = null;
			//adds variables into message
			text2 = string.Format(this.def.letterText, new object[]
			{
				FactionUtility.DefaultFactionFrom(FactionDefOf.MorrowRim_SixthHouse).Name,
				settlement.Name,
				oldFaction
			});
			
			base.SendStandardLetter(this.def.label, text2, this.def.letterDef, parms, settlement/*, Array.Empty<NamedArgument>()*/);
			return true;
		}

		protected IEnumerable<Settlement> ActualVictims(IncidentParms parms)
		{
			return PotentialVictims(parms.target).InRandomOrder(null);
		}

		public string GetFlavourText()
		{
			return ("DagothDream_SettlementClaimed").Translate();
		}
	}
}
