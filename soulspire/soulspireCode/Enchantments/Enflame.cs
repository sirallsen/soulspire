using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Entities.Cards;

namespace soulspire.soulspireCode.Enchantments;

public class Enflame : CustomEnchantmentModel
{
    public override bool CanEnchantCardType(CardType cardType) => cardType == CardType.Attack;

    protected override void OnEnchant()
    {
        Card.EnergyCost.UpgradeBy(-Card.EnergyCost.GetWithModifiers(CostModifiers.None));
        Card.AddKeyword(CardKeyword.Exhaust);
    }
}
