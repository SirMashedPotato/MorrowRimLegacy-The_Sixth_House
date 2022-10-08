using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using System.Reflection;
using Verse;
using System;
using System.Linq;
using System.Collections.Generic;
using Verse.AI;
using Verse.AI.Group;
using System.Text;

namespace MorrowRim_Dagoth
{

    //Setting the Harmony instance
    [StaticConstructorOnStartup]
    public class Main
    {
        static Main()
        {
            var harmony = new Harmony("com.MorrowRim_Dagoth");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }

    //patch to prevent sixth house settlements from being marked as defeated if no dreamers are present
    [HarmonyPatch(typeof(SettlementDefeatUtility))]
    [HarmonyPatch("IsDefeated")]
    public static class SettlementDefeatUtility_IsDefeated_Patch
    {
        [HarmonyPostfix]
        public static void IsDefeated_SixthHouse(ref bool __result, Map map, Faction faction)
        {
            if(faction == null || faction.def != FactionDefOf.MorrowRim_SixthHouse)
            {
                return;
            }
            List<Pawn> pawns = map.mapPawns.SpawnedPawnsInFaction(faction);
            if (pawns.Any())
            {
                __result = false;
            }
            return;
        }
    }

    [HarmonyPatch(typeof(Faction))]
    class GetRidOfLeaderError
    {
        [HarmonyPostfix]
        [HarmonyPatch("ShouldHaveLeader", MethodType.Getter)]
        static void GetRidOfLeaderError_Patch(ref Faction __instance, ref bool __result)
        {
            if (__instance.def.defName == "MorrowRim_SixthHouse") __result = false;
        }
    }
}