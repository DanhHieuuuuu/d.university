using Microsoft.Extensions.Configuration;
using Serilog.Formatting;
using Serilog.Sinks.Http;
using System.Net.Http.Headers;

namespace D.Core.API.AxiomLog
{
    public class AxiomHttpClient : IHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _token;

        public AxiomHttpClient(string token)
        {
            _httpClient = new HttpClient();
            _token = token;
        }

        public void Configure(IConfiguration configuration)
        {
        }

        public async Task<HttpResponseMessage> PostAsync(
            string requestUri,
            Stream contentStream,
            CancellationToken cancellationToken)
        {
            using var content = new StreamContent(contentStream);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-ndjson");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            return await _httpClient.PostAsync(requestUri, content, cancellationToken);
        }

        public void Dispose() => _httpClient.Dispose();
    }

    public class AxiomBatchFormatter : IBatchFormatter
    {
        public void Format(IEnumerable<string> logEvents, TextWriter output)
        {
            foreach (var logEvent in logEvents)
            {
                output.Write(logEvent);
                if (!logEvent.EndsWith("\n")) output.Write("\n");
            }
        }
    }
}
