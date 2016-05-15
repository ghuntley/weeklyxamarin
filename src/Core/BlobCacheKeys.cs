using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeeklyXamarin.Core
{
    public static class BlobCacheKeys
    {
        public static string GetKeyForIssues(int issuesPerPage, int page) => $"issues-{issuesPerPage}-{page}";
        public static string GetKeyForIssue(int issueNumber) => $"issue-{issueNumber}";
    }
}