using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Models.CardPools;

namespace soulspire.soulspireCode.Cards;

[Pool(typeof(EventCardPool))]
public class Pendant() : soulspireCard(-1, CardType.Quest, CardRarity.Quest, TargetType.None)
{
    public override int MaxUpgradeLevel => 0;
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Unplayable];
}
