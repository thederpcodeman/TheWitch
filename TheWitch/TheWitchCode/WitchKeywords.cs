using BaseLib.Patches.Content;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Models;

namespace TheWitch.Code.Keywords;

public static class WatcherKeywords
{
    [CustomEnum(null)]
    [KeywordProperties(AutoKeywordPosition.After)]
    public static CardKeyword Scry;

    public static bool IsScry(this CardModel card) => card.Keywords.Contains(WatcherKeywords.Scry);
}