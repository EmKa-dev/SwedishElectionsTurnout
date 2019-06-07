using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StatistikDataBasen.Api.Core
{
    public class ApiRequest : IDisposable
    {
        HttpClient _client = new HttpClient();

        HttpMethod _method;
        Uri _url;

        public ApiRequest(HttpMethod method, Uri url)
        {
            _method = method;
            _url = url;
        }

        public async Task<string> ExecuteQuery(string jsonstring = null)
        {
            HttpRequestMessage req = new HttpRequestMessage(_method, _url);

            if (_method == HttpMethod.Post)
            {
                if (string.IsNullOrEmpty(jsonstring))
                {
                    throw new ArgumentException("JSON¨-string must be provided with POST queries", nameof(jsonstring));
                }

                req.Content = new StringContent(jsonstring, Encoding.UTF8, "application/json");
            }

            var response = await _client.SendAsync(req);

            return await response.Content.ReadAsStringAsync();
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
