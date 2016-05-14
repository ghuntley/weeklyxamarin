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
    public class AuthenticatedHttpClientHandler : HttpClientHandler
    {
        private readonly string _publicationKey;
        private readonly string _apiKey;

        public AuthenticatedHttpClientHandler(string publicationKey, string apiKey)
        {
            _publicationKey = publicationKey;
            _apiKey = apiKey;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var authorization = request.Headers.Authorization;
            if (authorization != null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue(authorization.Scheme, string.Format("Token token=\"{0}\"", _apiKey));
            }

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}