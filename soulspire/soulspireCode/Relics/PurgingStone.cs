using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Models.RelicPools;

namespace soulspire.soulspireCode.Relics;

[Pool(typeof(EventRelicPool))]
public class PurgingStone : soulspireRelic
{
    public override RelicRarity Rarity => RelicRarity.Ancient;

    public override bool HasUponPickupEffect => true;

    public override async Task AfterObtained()
    {
        var curses = Owner.Deck.Cards.Where(c => c.Type == CardType.Curse).ToList();
        if (curses.Count > 0)
            await CardPileCmd.RemoveFromDeck(curses, true);
    }
}
