using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using TheWitch.TheWitchCode.Powers;

namespace TheWitch.TheWitchCode.Cards;

public sealed class ManifestSubstance() : TheWitchCard(2, CardType.Skill, CardRarity.Basic, TargetType.Self)
{

    public override bool GainsBlock => true;

    protected override IEnumerable<DynamicVar> CanonicalVars => [new BlockVar(8M, ValueProp.Move), new PowerVar<EssenceNextTurn>(0)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, cardPlay);
        await PowerCmd.Apply<ApplyEssence>(choiceContext, Owner.Creature, 5m, Owner.Creature, null);
        await PowerCmd.Apply<EssenceNextTurn>(choiceContext, Owner.Creature, DynamicVars[nameof(EssenceNextTurn)].BaseValue, Owner.Creature, null);
        
    }

    protected override void OnUpgrade()
    {
        this.DynamicVars.Block.UpgradeValueBy(3M);
        this.DynamicVars.Power<EssenceNextTurn>().UpgradeValueBy(3M);
    } 
}