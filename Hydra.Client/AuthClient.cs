using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Hydra.Client.Config;
using Hydra.Client.Http;
using Hydra.Client.Models.Auth;
using Newtonsoft.Json;

namespace Hydra.Client
{
    internal class AuthClient
    {
        private readonly RateLimiter _rateLimiter;
        private readonly HttpClient _httpClient;
        private readonly AuthConfig _config;

        public AuthClient(AuthConfig config, RateLimiter rateLimiter)
        {
            _rateLimiter = rateLimiter;
            _httpClient = CreateHttpClient();
            _config = config;
        }
        
        public async Task<AuthResponse> Authenticate(AuthRequest authRequest)
        {
            var requestContent = HttpContentUtil.CreateJsonContent(authRequest);
            var requestMessage = new HttpRequestMessage(
                HttpMethod.Post,
                CreateUri("cdp-user/auth"))
            {
                Content = requestContent
            };

            requestMessage.Headers.Add("Accept", "application/json");
            requestMessage.Headers.Add("Accept-Encoding", "identity");

            var responseMessage = await _httpClient.SendAsync(requestMessage);

            responseMessage.EnsureSuccessStatusCode();
            var responseContent = await responseMessage.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<AuthResponse>(responseContent);
            
            return response;
        }

        public async Task<VerifyResponse> VerifySession(VerifyRequest verifyRequest)
        {
            var requestContent = HttpContentUtil.CreateJsonContent(verifyRequest);
            var requestMessage = new HttpRequestMessage(
                HttpMethod.Post,
                CreateUri("/cdp-user/verify/.json"))
            {
                Content = requestContent
            };

            requestMessage.Headers.Add("Accept", "application/json");
            requestMessage.Headers.Add("Accept-Encoding", "identity");

            var responseMessage = await _httpClient.SendAsync(requestMessage);

            responseMessage.EnsureSuccessStatusCode();
            var responseContent = await responseMessage.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<VerifyResponse>(responseContent);

            return response;
        }

        public async Task<GamecodeResponse> GetGamecode(int project)
        {
            var requestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                CreateUri($"cdp-user/projects/{project}/gamecode/.json"));
            requestMessage.Headers.Add("Accept", "*/*");
            requestMessage.Headers.Add("Accept-Encoding", "identity");

            await _rateLimiter.Await();
            var responseMessage = await _httpClient.SendAsync(requestMessage);

            responseMessage.EnsureSuccessStatusCode();
            var responseContent = await responseMessage.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<GamecodeResponse>(responseContent);
            return response;
        }

        public void UpdateAuthorizationToken(string authResponseToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse($"Token {authResponseToken}");
        }

        private Uri CreateUri(string path)
        {
            return new Uri(_config.ServiceBaseUrl, path);
        }

        private HttpClient CreateHttpClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.ConnectionClose = false;
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.116 Safari/537.36");
            client.DefaultRequestHeaders.Add("x-cdp-app", "Bethesda Launcher");
            client.DefaultRequestHeaders.Add("x-cdp-app-ver", "1.24.3");
            client.DefaultRequestHeaders.Add("x-cdp-lib-ver", "1.24.3");
            client.DefaultRequestHeaders.Add("x-cdp-platform", "Win/32");
            client.DefaultRequestHeaders.Add("x-src-fp", GenFpHeader());
            return client;
        }
        
        private static string GenFpHeader()
        {
            var bytes = new byte[20];
            using (var rngProvider = new RNGCryptoServiceProvider())
            {
                rngProvider.GetBytes(bytes);
            }
            
            string fp = BitConverter.ToString(bytes).Replace("-", "").ToUpper();
            return fp;
        }

    }
}