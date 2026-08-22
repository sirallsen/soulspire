using BaseLib.Abstracts;
using BaseLib.Extensions;
using soulspire.soulspireCode.Extensions;
using BaseLib.Cards.Variables;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;

namespace soulspire.soulspireCode.Cards;

[Pool(typeof(EventCardPool))]
public class EstusFlask() : soulspireCard(0, CardType.Skill, CardRarity.Ancient, TargetType.Self)
{
    public override int MaxUpgradeLevel => 1;
    protected override IEnumerable<DynamicVar> CanonicalVars => [new HealVar(4M), new CardsVar(1)];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.TriggerAnim(this.Owner.Creature, "Cast", this.Owner.Character.CastAnimDelay);
        await CardPileCmd.Draw(choiceContext, this.DynamicVars.Cards.BaseValue, this.Owner);
        await CreatureCmd.Heal(this.Owner.Creature, this.DynamicVars.Heal.BaseValue);
    }
    
    protected override void OnUpgrade() => this.DynamicVars.Heal.UpgradeValueBy(2M);
}