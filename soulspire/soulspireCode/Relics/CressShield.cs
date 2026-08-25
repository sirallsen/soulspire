using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Models.RelicPools;

namespace soulspire.soulspireCode.Relics;

[Pool(typeof(EventRelicPool))]
public class GrassCrestShield : soulspireRelic
{
    public override RelicRarity Rarity => RelicRarity.Ancient;

    public override bool ShowCounter => true;
    public override int DisplayAmount => _remaining;

    private int _remaining;

    protected override IEnumerable<DynamicVar> CanonicalVars => [new EnergyVar(1)];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.ForEnergy(this)];

    public override async Task BeforeCombatStart()
    {
        _remaining = 3;
        InvokeDisplayAmountChanged();
        await PowerCmd.Apply<DexterityPower>(new ThrowingPlayerChoiceContext(), Owner.Creature, 3, Owner.Creature, null);
    }

    public override async Task AfterSideTurnStart(CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
    {
        if (!participants.Contains(Owner.Creature) || _remaining <= 0) return;
        await PlayerCmd.GainEnergy(DynamicVars.Energy.BaseValue, Owner);
    }

    public override async Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
    {
        if (side != CombatSide.Player || _remaining <= 0) return;
        _remaining--;
        InvokeDisplayAmountChanged();
        if (_remaining == 0)
            await PowerCmd.Apply<DexterityPower>(choiceContext, Owner.Creature, -3, Owner.Creature, null);
    }
}
