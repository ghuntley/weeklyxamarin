using System;
using System.Net.Http;
using AppSettingsMapper;
using Nancy;
using Nancy.Responses;
using WeeklyXamarin.Api.Services;

namespace WeeklyXamarin.Api.Modules
{
    public class IndexModule : NancyModule
    {
        private readonly CuratedApiService _curatedApiService;

        public IndexModule()
        {
            var settings = AppSettings.MapTo<Settings>();

            var curatedApiAuthenticationHandler = new CuratedApiAuthenticationHandler(settings.CuratedPublicationKey, settings.CuratedApiKey);
            _curatedApiService = new CuratedApiService(curatedApiAuthenticationHandler, apiBaseAddress: null, enableDiagnostics: false);

            Get["/"] = parameters =>
            {
                return Response.AsRedirect("http://weeklyxamarin.com", RedirectResponse.RedirectType.Permanent);
            };

            Get["/issues", runAsync: true] = async (parameters, cancellationToken) =>
            {
                int issuesPerPage;
                int.TryParse(Request.Query["per_page"].ToString(), out issuesPerPage);
                if (issuesPerPage == 0) issuesPerPage = 250;

                int page;
                int.TryParse(Request.Query["page"].ToString(), out page);
                if (page == 0) page = 1;

                var response = await _curatedApiService.CuratedApi.Issues(issuesPerPage, page);

                return Response.AsText(response, "application/json");
            };

            Get["/issues/{issueNumber:int}", runAsync: true] = async (parameters, cancellationToken) =>
            {
                int issueNumber = parameters.issueNumber;

                var response = await _curatedApiService.CuratedApi.Issue(issueNumber);

                return Response.AsText(response, "application/json");
            };

        }
    }
}