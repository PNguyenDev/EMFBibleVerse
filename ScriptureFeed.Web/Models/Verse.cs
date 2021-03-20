using System;
using System.Collections.Generic;

namespace ScriptureFeed.Web.Models
{
    [Serializable]
    public class Verse
    {
        public string Id { get; set; }
        public string VerseText { get; set; }
        public string ImageLink { get; set; }
        public DateTime VerseDate { get; set; }
        public string VideoLink { get; set; }
        public string ReferenceLink { get; set; }
        public string VerseNumbers { get; set; }
        public string Chapter { get; set; }
        public string Book { get; set; }
        public string ReferenceText { get; set; }
        public string BibleReferenceLink { get; set; }
        public string FacebookShareUrl { get; set; }
        public string TwitterShareUrl { get; set; }
        public string PinterestShareUrl { get; set; }
        public string Url { get; set; }
        public bool IsFavorite { get; set; } = false;
    }

    [Serializable]
    public class GetVerseResponse
    {
        public List<Verse> Verses { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public bool HasMorePages { get; set; }
    }
}
