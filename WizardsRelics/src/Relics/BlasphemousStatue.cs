using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Models.Cards;

namespace WizardsRelics.Relics;

public class BlasphemousStatue : RelicModel
{
    public override RelicRarity Rarity => RelicRarity.Common;
    public override bool IsAllowed(IRunState runState) => true;
    public override bool ShouldReceiveCombatHooks => true;

    public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
    {
        var relic = this;
        if (side != relic.Owner.Creature.Side)
        {
            // プレイヤーターンではない
            return;
        }
        var creature = this.Owner.Creature;
        if (combatState.RoundNumber <= 1)
        {
            // 戦闘開始時
            // TODO: Dazed、Mind Rot、Beckonをデッキに追加
            CardModel dazed = combatState.CreateCard(ModelDb.Card<Dazed>(), base.Owner);
            CardModel mindRot = combatState.CreateCard(ModelDb.Card<MindRot>(), base.Owner);
            CardModel beckon = combatState.CreateCard(ModelDb.Card<Beckon>(), base.Owner);
            CardCmd.PreviewCardPileAdd(await CardPileCmd.AddGeneratedCardToCombat(dazed, PileType.Draw, addedByPlayer: true, CardPilePosition.Random));
            CardCmd.PreviewCardPileAdd(await CardPileCmd.AddGeneratedCardToCombat(mindRot, PileType.Draw, addedByPlayer: true, CardPilePosition.Random));
            CardCmd.PreviewCardPileAdd(await CardPileCmd.AddGeneratedCardToCombat(beckon, PileType.Draw, addedByPlayer: true, CardPilePosition.Random));
            creature.Player!.PlayerCombatState!.GainEnergy(2);
            relic.Flash();
            return;
        }
        if (combatState.RoundNumber > 1)
        {
            // 戦闘開始時ではない / ターン開始時
            // TODO: Dazedをデッキに追加
            CardModel dazed = combatState.CreateCard(ModelDb.Card<Dazed>(), base.Owner);
            creature.Player!.PlayerCombatState!.GainEnergy(2);
            CardCmd.PreviewCardPileAdd(await CardPileCmd.AddGeneratedCardToCombat(dazed, PileType.Draw, addedByPlayer: true, CardPilePosition.Random));
            relic.Flash();
            return;
        }

    }
}