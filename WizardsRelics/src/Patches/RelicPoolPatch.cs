using System;
using System.Linq;

using WizardsRelics.Relics;

using HarmonyLib;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Unlocks;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Models.RelicPools;
using System.Collections.Generic;

namespace WizardsRelics.Patches;

[HarmonyPatch(typeof(SharedRelicPool), "GenerateAllRelics")]
public class RelicPoolPatch
{
    static void Postfix(ref IEnumerable<RelicModel> __result)
    {
        var list = __result.ToList();

        var customRelic = ModelDb.Relic<MagicRingRelic>();
        list.Add(customRelic);

        __result = list;
    }
}