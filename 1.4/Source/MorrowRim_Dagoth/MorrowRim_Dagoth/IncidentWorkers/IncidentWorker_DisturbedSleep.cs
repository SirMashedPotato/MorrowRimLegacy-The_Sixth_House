using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld.Planet;
using UnityEngine;
using Verse;
using RimWorld;

namespace MorrowRim_Dagoth
{
	class IncidentWorker_DisturbedSleep : IncidentWorker
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
				return Utility.GetColonyPawnSleeping(map);
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
			if (!text2.NullOrEmpty())
			{
				//adds pawn names for trait adding
				List<Pawn> list2 = ApplyToPawnsAdditional(list);
				if (list2.Any<Pawn>())
				{
					StringBuilder stringBuilder2 = new StringBuilder();
					stringBuilder2.Append("DagothDream_Touched".Translate(list2.Count));
					stringBuilder2.AppendLine();
					for (int i = 0; i < list2.Count; i++)
					{
						if (stringBuilder2.Length != 0)
						{
							stringBuilder2.AppendLine();
						}
						stringBuilder2.Append("  - " + list2[i].LabelNoCountColored.Resolve());
					}
					text2 += stringBuilder2;
				}
			}
			base.SendStandardLetter(this.def.label, text2, this.def.letterDef, parms, list);
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
			foreach (Pawn pawn in pawns)
			{
				if (!pawn.Awake())
				{
					if (pawn.story.traits.HasTrait(TraitDefOf.MorrowRim_TouchedByDagothUr))
					{
						pawn.health.AddHediff(HediffDefOf.Dagoth_Dreamer).Severity = 1f;
					} 
					else
					{
						pawn.health.AddHediff(HediffDefOf.Dagoth_DisturbedSleep).Severity = 1f;
					}
					/*TaleRecorder.RecordTale(TaleDefOf.IllnessRevealed, new object[]
					{
						pawn,
						this.def.diseaseIncident
					});*/
					list.Add(pawn);
				}
			}
			return list;
		}

		public List<Pawn> ApplyToPawnsAdditional(IEnumerable<Pawn> pawns)
		{
			List<Pawn> list = new List<Pawn>();
			foreach (Pawn pawn in pawns)
			{
				if (!pawn.story.traits.HasTrait(TraitDefOf.MorrowRim_TouchedByDagothUr))
				{
					if (Rand.Chance(pawn.GetStatValue(StatDefOf.PsychicSensitivity) / 10))
					{
						pawn.story.traits.GainTrait(new Trait(TraitDefOf.MorrowRim_TouchedByDagothUr));
						pawn.health.AddHediff(HediffDefOf.Dagoth_HiddenTouched);
						list.Add(pawn);
					}
				}
				/*TaleRecorder.RecordTale(TaleDefOf.IllnessRevealed, new object[]
				{
					pawn,
					this.def.diseaseIncident
				});*/
			}
			return list;
		}

		public string GetFlavourText()
		{
			int num = Rand.RangeInclusive(1, 5);
			string letter = String.Concat("DagothDream_" + num).Translate();
			return (letter);
		}
	}
}
