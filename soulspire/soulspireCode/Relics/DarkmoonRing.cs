using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.RelicPools;

namespace soulspire.soulspireCode.Relics;

[Pool(typeof(EventRelicPool))]
public class DarkmoonRing : soulspireRelic
{
    public override RelicRarity Rarity => RelicRarity.Ancient;

    private bool _usedThisCombat;

    public override Task BeforeCombatStart()
    {
        _usedThisCombat = false;
        return Task.CompletedTask;
    }

    public override int ModifyCardPlayCount(CardModel card, Creature target, int playCount)
    {
        if (_usedThisCombat || card.Owner != Owner || card.Type != CardType.Power) return playCount;
        _usedThisCombat = true;
        return playCount * 2;
    }
}
