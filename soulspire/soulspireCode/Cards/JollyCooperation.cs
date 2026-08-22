using BaseLib.Utils;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.CardPools;

namespace soulspire.soulspireCode.Cards;

[Pool(typeof(EventCardPool))]
public class JollyCooperation() : soulspireCard(1, CardType.Skill, CardRarity.Event, TargetType.Self)
{
    public override int MaxUpgradeLevel => 1;
    protected override IEnumerable<DynamicVar> CanonicalVars => [new EnergyVar(1)];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    public override CardMultiplayerConstraint MultiplayerConstraint => CardMultiplayerConstraint.MultiplayerOnly;

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var hand = PileType.Hand.GetPile(Owner);
        var prefs = new CardSelectorPrefs(CardSelectorPrefs.ExhaustSelectionPrompt, 0, hand.Cards.Count) { Cancelable = true };
        var selected = (await CardSelectCmd.FromCombatPile(choiceContext, hand, Owner, prefs)).ToList();

        foreach (var card in selected)
            await CardCmd.Exhaust(choiceContext, card);

        var energy = selected.Count * DynamicVars.Energy.BaseValue;
        foreach (var player in CombatState.Players)
            await PlayerCmd.GainEnergy(energy, player);
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}
