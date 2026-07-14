using BaseLib.Abstracts;
using BaseLib.Utils;
using TheWitch.TheWitchCode.Character;

namespace TheWitch.TheWitchCode.Potions;

[Pool(typeof(TheWitchPotionPool))]
public abstract class TheWitchPotion : CustomPotionModel;