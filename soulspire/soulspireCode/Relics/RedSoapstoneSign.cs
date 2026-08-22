using soulspire.soulspireCode.Cards;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Runs;

namespace soulspire.soulspireCode.Relics;

[Pool(typeof(EventRelicPool))]
public class RedSoapstoneSign : soulspireRelic
{
    public override RelicRarity Rarity => RelicRarity.Ancient;

    public override bool HasUponPickupEffect => true;

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [..HoverTipFactory.FromCardWithCardHoverTips<Invasion>(), ..HoverTipFactory.FromCardWithCardHoverTips<DarkSpirit>()];

    public override bool IsAllowed(IRunState runState) => !RunManager.Instance.IsSingleplayerOrFakeMultiplayer;

    public override async Task AfterObtained()
    {
        CardCmd.PreviewCardPileAdd([await CardPileCmd.Add(Owner.RunState.CreateCard<Invasion>(Owner), PileType.Deck)], 2F);
    }
}
