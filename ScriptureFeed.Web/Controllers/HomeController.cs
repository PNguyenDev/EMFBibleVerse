using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ScriptureFeed.Web.Interfaces;
using ScriptureFeed.Web.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ScriptureFeed.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> Logger;
        private readonly IEmfApiService ApiService;
        private readonly IScriptureFeedDataContext Db;

        public HomeController(ILogger<HomeController> logger, IEmfApiService service, IScriptureFeedDataContext db)
        {
            Logger = logger;
            ApiService = service;
            Db = db;
        }

        [HttpGet("GetFavoriteVerses")]
        public JsonResult GetFavoriteVerses()
        {
            var result = Db.GetFavoriteVerses();
            return new JsonResult(result);
        }

        [HttpPost("GetVerse")]
        public async Task<JsonResult> GetVerse([FromBody] GetVerseRequest request)
        {
            var result = await ApiService.GetBibleVerse(request.StartDate, request.PageSize);
            var favorites = Db.GetFavoriteVerses();
            var resultFavorites = result.Verses.Where(v => favorites.Verses.Any(f => v.Id == f.Id));
            foreach(var verse in resultFavorites)
            {
                verse.IsFavorite = true;
            }
            return new JsonResult(result);
        }

        [HttpPost("SaveFavoriteVerse")]
        public bool SaveFavoriteVerse([FromBody] SaveFavoriteVerseRequest request)
        {
            var result = Db.SaveFavoriteVerse(request.Verse, request.MakeFavorite);
            return result;
        }

        [Serializable]
        public class GetVerseRequest
        {
            public DateTime StartDate { get; set; }
            public int PageSize { get; set; }
        }

        [Serializable]
        public class SaveFavoriteVerseRequest
        {
            public Verse Verse { get; set; }
            public bool MakeFavorite { get; set; }
        }
    }
}
