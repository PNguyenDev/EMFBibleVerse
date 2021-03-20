using Microsoft.EntityFrameworkCore;
using ScriptureFeed.Web.Interfaces;
using ScriptureFeed.Web.Models;
using System.Collections.Generic;
using System.Linq;

namespace ScriptureFeed.Web.Data
{
    public class ScriptureFeedDataContext : DbContext, IScriptureFeedDataContext
    {
        public ScriptureFeedDataContext(DbContextOptions<ScriptureFeedDataContext> options)
            : base(options)
        {
        }

        public DbSet<Verse> FavoriteVerses { get; set; }

        public GetVerseResponse GetFavoriteVerses()
        {
            GetVerseResponse response = new()
            {
                Verses = FavoriteVerses.Where(v => v.IsFavorite).ToList()
            };
            return response;
        }

        public bool SaveFavoriteVerse(Verse verse, bool makeFavorite)
        {
            bool operationSuccessful = false;
            Verse exists = FavoriteVerses.FirstOrDefault(v => v.Id == verse.Id);
            if (makeFavorite && exists == null)
            {
                verse.IsFavorite = true;
                FavoriteVerses.Add(verse);
                operationSuccessful = true;
            }
            else if (!(makeFavorite || exists == null))
            {
                FavoriteVerses.Remove(exists);
                operationSuccessful = true;
            }
            if (operationSuccessful)
                SaveChanges();
            return operationSuccessful;
        }
    }
}
