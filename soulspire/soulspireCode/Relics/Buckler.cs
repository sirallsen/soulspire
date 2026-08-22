using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.ValueProps;

namespace soulspire.soulspireCode.Relics;

[Pool(typeof(EventRelicPool))]
public class Buckler : soulspireRelic
{
    public override RelicRarity Rarity => RelicRarity.Ancient;

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<VulnerablePower>()];

    private bool _anyAttackThisTurn;
    private bool _allFullyBlocked;

    public override Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
    {
        if (side == CombatSide.Enemy)
        {
            _anyAttackThisTurn = false;
            _allFullyBlocked = true;
        }
        return Task.CompletedTask;
    }

    public override Task AfterDamageReceived(PlayerChoiceContext choiceContext, Creature target, DamageResult result, ValueProp props, Creature dealer, CardModel cardSource)
    {
        if (target != Owner.Creature) return Task.CompletedTask;
        _anyAttackThisTurn = true;
        if (!result.WasFullyBlocked) _allFullyBlocked = false;
        return Task.CompletedTask;
    }

    public override async Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
    {
        if (side != CombatSide.Enemy || !_anyAttackThisTurn || !_allFullyBlocked) return;

        var targets = Owner.Creature.CombatState.GetOpponentsOf(Owner.Creature);
        await CreatureCmd.Damage(choiceContext, targets, 10, ValueProp.Unpowered, Owner.Creature);
        await PowerCmd.Apply<VulnerablePower>(choiceContext, targets, 1, Owner.Creature, null);
    }
}
