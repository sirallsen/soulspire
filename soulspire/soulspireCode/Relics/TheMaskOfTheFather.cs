using soulspire.soulspireCode.Powers;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.ValueProps;

namespace soulspire.soulspireCode.Relics;

[Pool(typeof(EventRelicPool))]
public class TheMaskOfTheFather : soulspireRelic
{
    public override RelicRarity Rarity => RelicRarity.Ancient;

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<MaskOfTheFatherPower>()];

    private decimal _pendingStrength;

    public override Task BeforeCombatStart()
    {
        _pendingStrength = 0;
        return Task.CompletedTask;
    }

    public override Task AfterDamageReceived(PlayerChoiceContext choiceContext, Creature target, DamageResult result, ValueProp props, Creature dealer, CardModel cardSource)
    {
        if (target != Owner.Creature || result.WasFullyBlocked || dealer == null || !dealer.IsEnemy) return Task.CompletedTask;
        _pendingStrength += result.UnblockedDamage;
        return Task.CompletedTask;
    }

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        if (player != Owner || _pendingStrength <= 0) return;
        var amount = Math.Ceiling(_pendingStrength / 2);
        _pendingStrength = 0;
        await PowerCmd.Apply<MaskOfTheFatherPower>(choiceContext, Owner.Creature, amount, Owner.Creature, null);
    }
}
