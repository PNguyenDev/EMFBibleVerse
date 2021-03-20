using ScriptureFeed.Web.Models;
using System.Collections.Generic;

namespace ScriptureFeed.Web.Interfaces
{
    public interface IScriptureFeedDataContext
    {
        GetVerseResponse GetFavoriteVerses();
        bool SaveFavoriteVerse(Verse verse, bool makeFavorite);
    }
}
