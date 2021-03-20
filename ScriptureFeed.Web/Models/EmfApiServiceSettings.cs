namespace ScriptureFeed.Web.Models
{
    public class EmfApiServiceSettings
    {
        public const string SectionName = "EmfApiService";
        public string BaseUrl { get; set; }
        public string VerseByDateString { get; set; }
        public string ServiceKeyName { get; set; }
        public string ServiceKeyValue { get; set; }
        public string SiteIdKey { get; set; }
        public string SiteIdValue { get; set; }
    }
}
