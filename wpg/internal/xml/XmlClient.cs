using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using wpg.connection;
using wpg.connection.auth;
using wpg.exception;
using wpg.connection.http;

namespace wpg.@internal.xml
{
    public class XmlClient
    {
        private static Dictionary<KeyValuePair<XmlEndpoint, GatewayEnvironment>, HttpClient> clients = new Dictionary<KeyValuePair<XmlEndpoint, GatewayEnvironment>, HttpClient>();

        public static HttpClient Get(XmlEndpoint endpoint, GatewayEnvironment environment)
        {
            HttpClient client;
            lock (clients)
            {
                KeyValuePair<XmlEndpoint, GatewayEnvironment> key = new KeyValuePair<XmlEndpoint, GatewayEnvironment>(endpoint, environment);
                if (clients.ContainsKey(key))
                {
                    client = clients[key];
                }
                else
                {
                    client = new HttpClient();
                    Uri uri = endpoint.GetUri(environment);
                    client.BaseAddress = uri;
                    clients[key] = client;
                }
            }
            return client;
        }

        public async Task<XmlResponse> send(XmlBuildParams buildParams)
        {
            HttpRequestMessage requestMessage = build(buildParams);
            HttpClient client = Get(buildParams.Builder.Endpoint, buildParams.GatewayContext.Environment);
            HttpResponseMessage rawResponse = await client.SendAsync(requestMessage).ConfigureAwait(false);
            HttpResponse response = await transform(buildParams.SessionContext, rawResponse);

            // Check respone for gateway errors
            MatchCollection matches = Regex.Matches(response.Body, "(?:<p>)([^<]+)", RegexOptions.Multiline);
            if (matches.Count > 0)
            {
                String pageError = matches[0].Value.Trim();
                throw new WpgRequestException("Failed to make request - message from gateway: " + pageError);
            }

            // Convert to xml response
            XmlBuilder responseBuilder = XmlBuilder.parse(buildParams.Builder.Endpoint, response.Body);
            XmlResponse xmlResponse = new XmlResponse(buildParams.SessionContext, response, responseBuilder);
            return xmlResponse;
        }

        private HttpRequestMessage build(XmlBuildParams buildParams)
        {
            GatewayContext gatewayContext = buildParams.GatewayContext;
            SessionContext sessionContext = buildParams.SessionContext;
            XmlBuilder builder = buildParams.Builder;

            String xml = builder.ToString();
            String authHeader = buildAuthHeader(gatewayContext);

            HttpRequestMessage request = new HttpRequestMessage();

            StringContent content = new StringContent(xml, System.Text.Encoding.UTF8);
            MediaTypeHeaderValue contentType = new MediaTypeHeaderValue("text/xml");
            contentType.CharSet = "utf-8";
            content.Headers.ContentType = contentType;
            request.Content = content;

            request.Method = HttpMethod.Post;
            request.Headers.Add("Authorization", authHeader);
            request.Headers.Add(Constants.STATS_HEADER_KEY, Constants.STATS_HEADER_VALUE);

            return request;
        }

        private string buildAuthHeader(GatewayContext gatewayContext)
        {
            UserPassAuth auth = (UserPassAuth) gatewayContext.Auth;
            string authHeader = auth.User + ":" + auth.Pass;
            byte[] raw = System.Text.Encoding.UTF8.GetBytes(authHeader);
            string encoded = "Basic " + System.Convert.ToBase64String(raw);
            return encoded;
        }

        private async Task<HttpResponse> transform(SessionContext sessionContext, HttpResponseMessage rawResponse)
        {
            String body = await rawResponse.Content.ReadAsStringAsync();
            Dictionary<String, String> headers = transformHeaders(rawResponse);
            HttpResponse response = new HttpResponse(headers, body);
            storeCookies(sessionContext, rawResponse);
            return response;
        }

        private void storeCookies(SessionContext sessionContext, HttpResponseMessage rawResponse)
        {
            // Clear existing cookie headers
            sessionContext.Headers.Remove("Cookie");

            // Transfer cookies
            IEnumerable<string> cookieHeaders = rawResponse.Headers.GetValues("Set-Cookie");
            foreach (string header in cookieHeaders)
            {
                sessionContext.Headers.Add("Cookie", header);
            }
        }

        private Dictionary<String, String> transformHeaders(HttpResponseMessage rawResponse)
        {
            Dictionary<String, String> result = new Dictionary<string, string>();
            foreach (var headers in rawResponse.Headers)
            {
                foreach (var header in headers.Value)
                {
                    result.Add(headers.Key, header);
                }
            }
            return result;
        }

    }
}
