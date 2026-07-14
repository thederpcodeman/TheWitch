using BaseLib.Abstracts;
using TheWitch.TheWitchCode.Extensions;
using Godot;

namespace TheWitch.TheWitchCode.Character;

public class TheWitchRelicPool : CustomRelicPoolModel
{
    public override Color LabOutlineColor => TheWitch.Color;

    public override string BigEnergyIconPath => "charui/big_energy.png".ImagePath();
    public override string TextEnergyIconPath => "charui/text_energy.png".ImagePath();
}