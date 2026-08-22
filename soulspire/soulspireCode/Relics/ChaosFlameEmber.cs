using soulspire.soulspireCode.Enchantments;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.RelicPools;

namespace soulspire.soulspireCode.Relics;

[Pool(typeof(EventRelicPool))]
public class ChaosFlameEmber : soulspireRelic
{
    public override RelicRarity Rarity => RelicRarity.Ancient;

    public override bool HasUponPickupEffect => true;

    protected override IEnumerable<IHoverTip> ExtraHoverTips => HoverTipFactory.FromEnchantment<Enflame>();

    public override async Task AfterObtained()
    {
        var enchantment = ModelDb.Enchantment<Enflame>();
        CardSelectorPrefs prefs = new CardSelectorPrefs(CardSelectorPrefs.EnchantSelectionPrompt, 0, 3)
        {
            Cancelable = false,
            RequireManualConfirmation = true
        };
        foreach (var card in await CardSelectCmd.FromDeckForEnchantment(Owner, enchantment, 1, prefs))
        {
            CardCmd.Enchant(enchantment.ToMutable(), card, 1);
            CardCmd.Preview(card);
        }
    }
}
