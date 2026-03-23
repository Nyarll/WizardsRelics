using System;

using WizardsRelics.Relics;

using HarmonyLib;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Unlocks;

namespace WizardsRelics.Patches;

[HarmonyPatch(typeof(Player), nameof(Player.CreateForNewRun), new Type[] { typeof(CharacterModel), typeof(UnlockState), typeof(ulong) })]
public class TestPatch
{
    public static MegaCrit.Sts2.Core.Logging.Logger Logger { get; } = new(ModEntry.ModId, MegaCrit.Sts2.Core.Logging.LogType.Generic);

    static void Postfix(Player __result)
    {
        __result.Gold = 999;

        var customRelic = ModelDb.Relic<MagicRingRelic>().ToMutable();
        __result.AddRelicInternal(customRelic);
        Logger.Info("Relics add patch success.");
    }
}