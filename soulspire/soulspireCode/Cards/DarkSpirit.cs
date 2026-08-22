using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Powers;

namespace soulspire.soulspireCode.Cards;

[Pool(typeof(EventCardPool))]
public class DarkSpirit() : soulspireCard(1, CardType.Curse, CardRarity.Curse, TargetType.Self)
{
    public override int MaxUpgradeLevel => 0;
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<FrailPower>(1), new PowerVar<WeakPower>(1)];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Ethereal, CardKeyword.Exhaust];
    public override bool HasTurnEndInHandEffect => true;

    private bool _pendingDebuffs;

    protected override Task OnTurnEndInHand(PlayerChoiceContext choiceContext)
    {
        _pendingDebuffs = true;
        return Task.CompletedTask;
    }

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        if (player != Owner || !_pendingDebuffs) return;
        _pendingDebuffs = false;
        await PowerCmd.Apply<FrailPower>(choiceContext, Owner.Creature, DynamicVars.Power<FrailPower>().BaseValue, Owner.Creature, this);
        await PowerCmd.Apply<WeakPower>(choiceContext, Owner.Creature, DynamicVars.Power<WeakPower>().BaseValue, Owner.Creature, this);
    }
}
