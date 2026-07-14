using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace TheWitch.TheWitchCode.Powers;

public class EssencePower() : TheWitchPower()
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (this.Amount > 0) 
        {
            await PowerCmd.Apply<EssencePower>(choiceContext, Owner, -1M, Owner, null, true);
            await PowerCmd.Apply<StrengthPower>(choiceContext, Owner, -1M, Owner, null, true);
            await PowerCmd.Apply<DexterityPower>(choiceContext, Owner, -1M, Owner, null, true);
        }
    }

    public override async Task BeforeApplied(
        Creature target,
        Decimal amount,
        Creature? applier,
        CardModel? cardSource)
    {
        await PowerCmd.Apply<StrengthPower>((PlayerChoiceContext) new ThrowingPlayerChoiceContext(), target, (Decimal) amount, applier, cardSource, true);
        await PowerCmd.Apply<DexterityPower>((PlayerChoiceContext) new ThrowingPlayerChoiceContext(), target, (Decimal) amount, applier, cardSource, true);
    }
    
    public override async Task AfterPowerAmountChanged(
        PlayerChoiceContext choiceContext,
        PowerModel power,
        Decimal amount,
        Creature? applier,
        CardModel? cardSource)
    {
        if (amount == (Decimal) this.Amount || power != this)
            return;
        await PowerCmd.Apply<StrengthPower>(choiceContext, Owner, (Decimal) amount, applier, cardSource, true);
        await PowerCmd.Apply<DexterityPower>(choiceContext, Owner, (Decimal) amount, applier, cardSource, true);
    }
    
    
    public override async Task AfterSideTurnEnd(
        PlayerChoiceContext choiceContext,
        CombatSide side,
        IEnumerable<Creature> participants)
    {
        if (!participants.Contains<Creature>(Owner))
            return;
        Flash();
        await PowerCmd.Remove((PowerModel) this);
        await PowerCmd.Apply<StrengthPower>(choiceContext, Owner, (Decimal) (Amount * -1), Owner, null);
        await PowerCmd.Apply<DexterityPower>(choiceContext, Owner, (Decimal) (Amount * -1), Owner, null);
    }
}