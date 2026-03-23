using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.Models.Powers;

namespace WizardsRelics.Relics;

public class MagicRingRelic : RelicModel
{
    public override RelicRarity Rarity => RelicRarity.Common;
    public override bool IsAllowed(IRunState runState) => true;
    public override bool ShouldReceiveCombatHooks => true;

    public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
    {
        var magicRing = this;
        if (side != magicRing.Owner.Creature.Side)
        {
            // プレイヤーターンではない
            return;
        }
        var creature = this.Owner.Creature;
        if (combatState.RoundNumber > 1)
        {
            // 戦闘開始時ではない
            creature.Player!.PlayerCombatState!.GainEnergy(1);
            magicRing.Flash();
            return;
        }
        await PowerCmd.Apply<StrengthPower>(Owner.Creature, -2, Owner.Creature, null);
        magicRing.Flash();
    }
}