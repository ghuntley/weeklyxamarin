using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace WeeklyXamarin.Services.Api
{
    public class IssueResult
    {
        public int Number { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Url { get; set; }

        [JsonProperty("published_at")]
        public DateTime PublishedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }

        public List<IssueCategory> IssueCategories { get; set; }
    }

    public class IssueCategory
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public List<IssueCategoryItem> IssueCategoryItems { get; set; }
    }

    public class IssueCategoryItem
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Footer { get; set; }
        public List<IssueCategoryItemEmbeddedLinks> IssueCategoryItemEmbeddedLinks { get; set; }
        public string Identifier { get; set; }

        [JsonProperty("url_domain")]
        public string UrlDomain { get; set; }

        public string Url { get; set; }
    }

    public class IssueCategoryItemEmbeddedLinks
    {
        public string Identifier { get; set; }
        public string Title { get; set; }

        [JsonProperty("url_domain")]
        public string UrlDomain { get; set; }

        public string Url { get; set; }
    }

    public class IssuesResult
    {
        public List<Issues> Issues { get; set; }

        public int Page { get; set; }

        [JsonProperty("total_pages")]
        public int TotalPages { get; set; }

        [JsonProperty("total_results")]
        public int TotalResults { get; set; }
    }

    public class Issues
    {
        public int Number { get; set; }

        [JsonProperty("published_at")]
        public DateTimeOffset PublishedAt { get; set; }

        public string Summary { get; set; }
        public string Title { get; set; }

        [JsonProperty("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }

        public string Url { get; set; }
    }
}