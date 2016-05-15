using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace WeeklyXamarin.Api.Services
{
    public class CuratedApiAuthenticationHandler : HttpClientHandler
    {
        private readonly string _publicationKey;
        private readonly string _apiKey;

        public CuratedApiAuthenticationHandler(string publicationKey, string apiKey)
        {
            _publicationKey = publicationKey;
            _apiKey = apiKey;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Token", $"token=\"{_apiKey}\"");

            var requestUrl = $"{request.RequestUri.Scheme}://{request.RequestUri.Host}:{request.RequestUri.Port}/{_publicationKey}{request.RequestUri.PathAndQuery}";
            request.RequestUri = new Uri(requestUrl);

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}