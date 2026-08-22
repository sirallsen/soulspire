using BaseLib.Abstracts;
using BaseLib.Extensions;
using soulspire.soulspireCode.Extensions;
using MegaCrit.Sts2.Core.Entities.Cards;

namespace soulspire.soulspireCode.Cards;

public abstract class soulspireCard(int cost, CardType type, CardRarity rarity, TargetType target)
    : CustomCardModel(cost, type, rarity, target)
{
    public override string PortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();
    public override string CustomPortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigCardImagePath();
    public override string BetaPortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();
}
