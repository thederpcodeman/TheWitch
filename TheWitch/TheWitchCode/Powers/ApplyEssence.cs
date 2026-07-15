using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace TheWitch.TheWitchCode.Powers;

public class ApplyEssence() : TheWitchPower()
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    
    public override async Task AfterCardPlayedLate(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (this.Amount > 0) 
        {
            await PowerCmd.Apply<EssencePower>(choiceContext, Owner, this.Amount, Owner, null, true);
            await PowerCmd.Remove(this);
        }
    }
}