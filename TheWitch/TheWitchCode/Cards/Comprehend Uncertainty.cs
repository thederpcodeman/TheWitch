using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using TheWitch.TheWitchCode.Powers;

namespace TheWitch.TheWitchCode.Cards;

public sealed class ComprehendUncertainty() : TheWitchCard(0, CardType.Skill, CardRarity.Basic, TargetType.Self)
{

    protected override IEnumerable<DynamicVar> CanonicalVars => [new IntVar("Scry", 1)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        this.DynamicVars.TryGetValue("Scry", out var scry);
        if (scry != null)
        {
            await ScryCmd.Execute(choiceContext, cardPlay.Card.Owner, scry.IntValue);
        }
        //TODO ANTI INFINITE
        await CardPileCmd.Draw(choiceContext, 1, cardPlay.Card.Owner);
    }

    protected override void OnUpgrade()
    {
        this.DynamicVars.TryGetValue("Scry", out var scry);
        if (scry != null)
        {
            scry.UpgradeValueBy(1m);
        }
    } 
}