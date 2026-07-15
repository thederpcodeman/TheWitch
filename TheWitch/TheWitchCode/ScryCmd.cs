using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.Models;

namespace TheWitch.TheWitchCode;

public static class ScryCmd
{

    public static async Task Execute(
        PlayerChoiceContext choiceContext,
        Player player,
        int amount)
    {
        if (amount <= 0)
            return;
        CardPile drawPile = PileType.Draw.GetPile(player);
        CardPile discardPile = PileType.Discard.GetPile(player);
        var combatState = player.Creature.CombatState;
        if (combatState == null)
            return;
        List<CardModel> list = drawPile.Cards.Take<CardModel>(amount).ToList<CardModel>();
        if (list.Count == 0)
            return;
        CardSelectorPrefs prefs = new CardSelectorPrefs(CardSelectorPrefs.DiscardSelectionPrompt, 0, list.Count);
        List<CardModel> cardsToDiscard = (await CardSelectCmd.FromSimpleGrid(choiceContext, (IReadOnlyList<CardModel>) list, player, prefs)).ToList<CardModel>();
        await CardCmd.Discard(choiceContext, cardsToDiscard);
        discardPile.InvokeContentsChanged();
        return;
    }
}