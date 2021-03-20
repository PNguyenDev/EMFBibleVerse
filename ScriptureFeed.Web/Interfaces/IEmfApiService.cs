using ScriptureFeed.Web.Models;
using System;
using System.Threading.Tasks;

namespace ScriptureFeed.Web.Interfaces
{
    public interface IEmfApiService
    {
        Task<GetVerseResponse> GetBibleVerse(DateTime startDate, int pageSize);
    }
}
