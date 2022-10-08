﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld.Planet;
using UnityEngine;
using Verse;
using RimWorld;
using Verse.AI.Group;

namespace MorrowRim_Dagoth
{
	class IncidentWorker_InterruptedSleep : IncidentWorker
	{

		protected override bool CanFireNowSub(IncidentParms parms)
		{
			return base.CanFireNowSub(parms) && this.PotentialVictims(parms.target).Any<Pawn>() && Utility.DagothIncidentCheck();
		}

		protected IEnumerable<Pawn> PotentialVictims(IIncidentTarget target)
		{
			Map map = target as Map;
			if (map != null)
			{
				return Utility.GetColonyPawnSleepingNotTouched(map);
			}
			return from x in ((Caravan)target).PawnsListForReading
				   where x.IsFreeColonist || x.IsPrisonerOfColony
				   select x;
		}

		protected override bool TryExecuteWorker(IncidentParms parms)
		{
			List<Pawn> list = ApplyToPawns(this.ActualVictims(parms).ToList<Pawn>());
			if (!list.Any<Pawn>())
			{
				return false;
			}
			Utility.IncrementRecordMulti(list, RecordDefOf.MorrowRim_DagothDreamsTargeted);
			//adds pawn names for 'infection'
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < list.Count; i++)
			{
				if (stringBuilder.Length != 0)
				{
					stringBuilder.AppendLine();
				}
				stringBuilder.Append("  - " + list[i].LabelNoCountColored.Resolve());
			}
			string text2;
			//adds variables into message
			if (list.Any<Pawn>())
			{
				text2 = string.Format(this.def.letterText, new object[]
				{
					list.Count.ToString(),
					Faction.OfPlayer.def.pawnsPlural,
					GetFlavourText(),
					stringBuilder.ToString()
				});
			}
			else
			{
				text2 = "";
			}
			base.SendStandardLetter(this.def.label, text2, this.def.letterDef, parms, list/*, Array.Empty<NamedArgument>()*/);
			return true;
		}

		protected IEnumerable<Pawn> ActualVictims(IncidentParms parms)
		{
			int num = this.PotentialVictims(parms.target).Count<Pawn>();
			IntRange intRange = new IntRange(Mathf.RoundToInt((float)num * this.def.diseaseVictimFractionRange.min), Mathf.RoundToInt((float)num * this.def.diseaseVictimFractionRange.max));
			int num2 = intRange.RandomInRange;
			num2 = Mathf.Clamp(num2, 1, this.def.diseaseMaxVictims);
			return PotentialVictims(parms.target).InRandomOrder(null).Take(num2);
		}

		public List<Pawn> ApplyToPawns(IEnumerable<Pawn> pawns)
		{
			List<Pawn> list = new List<Pawn>();
			List<Pawn> list2 = new List<Pawn>();
			Faction faction = FactionUtility.DefaultFactionFrom(FactionDefOf.MorrowRim_SixthHouse);
			foreach (Pawn pawn in pawns)
			{
				if (!pawn.Awake())
				{
					int temp = Rand.RangeInclusive(1, 10);
					PawnKindDef def;
					switch (temp)
					{
						case 10:
							def = PawnKindDefOf.Dagoth_Ghoul;
							break;
						case 9:
							def = PawnKindDefOf.Dagoth_Slave;
							break;
						case 8:
							def = PawnKindDefOf.Dagoth_Slave;
							break;
						default:
							def = PawnKindDefOf.Dagoth_Zombie;
							break;
					}
					Pawn newPawn = PawnGenerator.GeneratePawn(def, faction);
					PawnUtility.TrySpawnHatchedOrBornPawn(newPawn, pawn);
					list.Add(pawn);
					list2.Add(newPawn);
				}
			}
			if (list2.Any<Pawn>())
			{
				LordMaker.MakeNewLord(faction, new LordJob_AssaultColony(faction, true, true, false, true, true), list2.ElementAt(0).Map, list2);
			}
			return list;
		}

		public string GetFlavourText()
		{
			return ("DagothDream_InterruptedSleep").Translate();
		}
	}
}
