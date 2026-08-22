using soulspire.soulspireCode.Relics;
using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.Models;

namespace soulspire.soulspireCode.Ancients;

public class Artorias : CustomAncientModel
{
    protected override OptionPools MakeOptionPools =>
        new(
            MakePool(AncientOption<RiteOfKindling>(), AncientOption<DarkSoul>(), AncientOption<TheMaskOfTheFather>(), AncientOption<RedSoapstoneSign>()),
            MakePool(AncientOption<PurgingStone>(), AncientOption<SunlightMedal>(), AncientOption<ChaosFlameEmber>(), AncientOption<ThePendant>()),
            MakePool(AncientOption<Buckler>(), AncientOption<GrassCrestShield>(), AncientOption<RingOfFavorAndProtection>(), AncientOption<DarkmoonRing>()));

    public override Color ButtonColor => new(0.42f, 0.32f, 0.46f, 0.80f);
    public override Color DialogueColor => new(0.42f, 0.32f, 0.46f, 0.80f);

    public override string CustomMapIconPath => "res://images/packed/map/ancients/ancient_node_artorias.png";
    public override string CustomMapIconOutlinePath => "res://images/packed/map/ancients/ancient_node_artorias_outline.png";

    public override bool IsValidForAct(ActModel act) => act.ActNumber() == 2;
}
