using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Hydra.Client.Config;
using Hydra.Client.Exceptions;
using Hydra.Client.Http;
using Hydra.Client.Models;
using Newtonsoft.Json.Linq;

namespace Hydra.Client
{
    internal class HydraClient
    {
        private readonly RateLimiter _rateLimiter;
        private readonly HttpClient _httpClient;
        private readonly HydraConfig _config;

        public HydraClient(HydraConfig config, RateLimiter rateLimiter)
        {
            _rateLimiter = rateLimiter;
            _httpClient = CreateHttpClient();
            _config = config;
        }

        public async Task<GetUserInventoryResponse> GetUserInventory(
            GetUserInventoryRequest request)
        {
            return await PostAsync<GetUserInventoryRequest, GetUserInventoryResponse>(
                request,
                CreateUri(HydraConstants.UserService4, "d240545c28b84eb0b794322cacacabba"),
                compress: true);
        }

        public async Task<GetGameConfigLayersResponse> GetGameConfigLayers(
            GetGameConfigLayersRequest request)
        {
            return await PostAsync<GetGameConfigLayersRequest, GetGameConfigLayersResponse>(
                request,
                CreateUri(HydraConstants.GameconfigService1, "26de4049e4d44091ab9515a9935b6d3d"),
                compress: true);
        }

        public async Task<GetGameConfigResponse> GetGameConfig(GetGameConfigRequest request)
        {
            return await PostAsync<GetGameConfigRequest, GetGameConfigResponse>(
                request,
                CreateUri(HydraConstants.GameconfigService1, "dd989feee355446580b1a2e95884443b"));
        }

        public async Task<GetUsersResponse> GetUsers(GetUsersRequest request)
        {
            return await PostAsync<GetUsersRequest, GetUsersResponse>(
                request,
                CreateUri(HydraConstants.UserService4, "38b391c62c2a47409c1b03a2e43750ed"));
        }

        public async Task<RefreshTokensResponse> RefreshTokens(RefreshTokensRequest request)
        {
            return await PostAsync<RefreshTokensRequest, RefreshTokensResponse>(
                request,
                CreateUri(HydraConstants.AuthService1, "a688adf7d0e44bb49b6cc0c89b3b9cbe"));
        }

        public async Task<GetEnvironmentsResponse> GetEnvironments(GetEnvironmentsRequest request)
        {
            return await PostAsync<GetEnvironmentsRequest, GetEnvironmentsResponse>(
                request,
                CreateUri(HydraConstants.SharedService, "dc967910e5f347b0a9ca67822ae58469"),
                expectContinue: true);
        }

        public async Task<GetTokensResponse> GetTokens(GetTokensRequest request)
        {
            var login2Response = await PostAsync<GetTokensRequest, GetTokensResponse>(
                request,
                CreateUri(HydraConstants.AuthService2, "645f1997ddae4939b88364ed93777cff"),
                expectContinue: true);

            _httpClient.DefaultRequestHeaders.Add("USER_CONTEXT", login2Response.data.AuthorizationToken);
            _httpClient.DefaultRequestHeaders.Add("ACL-Token", login2Response.data.AccessRoleToken);

            return login2Response;
        }

        public async Task<LoginResponse> CheckLogin(CheckLoginRequest request)
        {
            TimeSpan rateLimit = TimeSpan.FromMilliseconds(500);
            return await PostAsync<CheckLoginRequest, LoginResponse>(
                request,
                CreateUri(HydraConstants.AuthService2, "e8344ad7920649e4b3595a6c5890548b"),
                expectContinue: true,
                rateLimit: rateLimit);
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            return await PostAsync<LoginRequest, LoginResponse>(
                request,
                CreateUri(HydraConstants.AuthService2, "a15cc4fe55f94320bc42089c5cb7232a"),
                expectContinue: true);
        }

        public async Task<GetChallengesResponse> GetChallenges(GetChallengesRequest request)
        {
            return await PostAsync<GetChallengesRequest, GetChallengesResponse>(
                request,
                CreateUri(HydraConstants.UserService5, "48c0fc9485284f828ec17b1ed14b6890"),
                compress: true);
        }

        public async Task<GetDataCenterOccupationResponse> GetDataCenterOccupation(GetDataCenterOccupationRequest request)
        {
            return await PostAsync<GetDataCenterOccupationRequest, GetDataCenterOccupationResponse>(
                request,
                CreateUri(HydraConstants.UhService, "afba8b534d4e40acbb22f0580a6489cb"));
        }


        public async Task<LoginMessagingRequestResponse> LoginMessaging(LoginMessagingRequest request)
        {
            return await PostAsync<LoginMessagingRequest, LoginMessagingRequestResponse>(
                request,
                CreateUri(HydraConstants.MessageService, "c1a4fef57e2b4438ac279c627241c833"));
        }


        public async Task<SendExternalSessionTokenResponse> SendExternalSessionToken(SendExternalSessionTokenRequest request)
        {
            return await PostAsync<SendExternalSessionTokenRequest, SendExternalSessionTokenResponse>(
                request,
                CreateUri(HydraConstants.MessageService, "c1a4fef57e2b4438ac279c627241c833"));
        }
        
        public void UpdateAuthorizationToken(string dataAuthorizationToken)
        {
            _httpClient.DefaultRequestHeaders.Remove("USER_CONTEXT");
            _httpClient.DefaultRequestHeaders.Add("USER_CONTEXT", dataAuthorizationToken);
        }

        private HttpClient CreateHttpClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.ConnectionClose = false;
            return client;
        }

        private Uri CreateUri(string name, string method)
        {
            Uri baseUri = _config.GetServiceUri(name);
            var methodUrl = new Uri(baseUri, method);
            return methodUrl;
        }

        private async Task<TResponse> PostAsync<TRequest, TResponse>(
            TRequest request,
            Uri url,
            bool compress = false,
            bool expectContinue = false,
            TimeSpan? rateLimit = null) where TResponse : BaseResponse
        {
            HttpContent content = compress
                ? HttpContentUtil.CreateCompressedJsonContent(request)
                : HttpContentUtil.CreateJsonContent(request);

            var requestMessage = new HttpRequestMessage(
                HttpMethod.Post,
                url)
            {
                Content = content
            };

            requestMessage.Headers.Add("Accept", "*/*");

            if (compress)
            {
                requestMessage.Headers.Add("Compression-Enabled", "true");
            }

            if (expectContinue)
            {
                requestMessage.Headers.ExpectContinue = true;
            }

            await _rateLimiter.Await(rateLimit);
            var responseMessage = await _httpClient.SendAsync(requestMessage);

            responseMessage.EnsureSuccessStatusCode();

            TResponse response;
            IEnumerable<string> compressHeaders;
            if (responseMessage.Headers.TryGetValues("Compression-Enabled", out compressHeaders) && compressHeaders.Contains("true"))
            {
                response = await HttpContentUtil.ReadCompressedJsonContent<TResponse>(responseMessage.Content);
            }
            else
            {
                response = await HttpContentUtil.ReadJsonContent<TResponse>(responseMessage.Content);
            }

            if (response.retCode != 0)
            {
                throw ServiceFaultException.Create(response.retCode);
            }

            return response;
        }

    }
}