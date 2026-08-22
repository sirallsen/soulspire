using soulspire.soulspireCode.Cards;
using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.RestSite;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models;

namespace soulspire.soulspireCode.RestSiteOptions;

public class PendantTradeOption(Player owner) : RestSiteOption(owner), ICustomModel
{
    public override string OptionId => "SOULSPIRE-PENDANT_TRADE";

    public override LocString Description => new LocString("rest_site_ui", $"OPTION_{OptionId}.description");

    public override async Task<bool> OnSelect()
    {
        CardModel? pendant = PileType.Deck.GetPile(Owner).Cards.FirstOrDefault(c => c is Pendant);
        if (pendant != null)
            await CardPileCmd.RemoveFromDeck(pendant);

        for (int i = 0; i < 3; i++)
        {
            var relic = RelicFactory.PullNextRelicFromFront(Owner).ToMutable();
            await RelicCmd.Obtain(relic, Owner);
        }

        return true;
    }
}
