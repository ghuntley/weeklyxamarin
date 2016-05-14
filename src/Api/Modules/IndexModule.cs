using System.Net.Http;
using Nancy;
using Nancy.Responses;

namespace WeeklyXamarin.Api.Modules
{
    public class IndexModule : NancyModule
    {
        public IndexModule()
        {
            Get["/"] = parameters =>
            {
                return Response.AsRedirect("http://weeklyxamarin.com", RedirectResponse.RedirectType.Permanent);
            };

            Get["/issues"] = parameters =>
            {
            };
        }
    }
}