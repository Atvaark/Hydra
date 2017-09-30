using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Hydra.Client.Config;
using Hydra.Client.Exceptions;
using Hydra.Client.Http;
using Hydra.Client.Models;
using Hydra.Client.Models.Unknown;

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
                CreateUri(HydraServices.UserService4, HydraMethods.GetUserInventory),
                compress: true);
        }

        public async Task<GetGameConfigLayersResponse> GetGameConfigLayers(
            GetGameConfigLayersRequest request)
        {
            return await PostAsync<GetGameConfigLayersRequest, GetGameConfigLayersResponse>(
                request,
                CreateUri(HydraServices.GameconfigService1, HydraMethods.GetGameConfigLayers),
                compress: true);
        }

        public async Task<GetGameConfigResponse> GetGameConfig(GetGameConfigRequest request)
        {
            return await PostAsync<GetGameConfigRequest, GetGameConfigResponse>(
                request,
                CreateUri(HydraServices.GameconfigService1, HydraMethods.GetGameConfig));
        }

        public async Task<GetUsersResponse> GetUsers(GetUsersRequest request)
        {
            return await PostAsync<GetUsersRequest, GetUsersResponse>(
                request,
                CreateUri(HydraServices.UserService4, HydraMethods.GetUsers));
        }

        public async Task<RefreshTokensResponse> RefreshTokens(RefreshTokensRequest request)
        {
            return await PostAsync<RefreshTokensRequest, RefreshTokensResponse>(
                request,
                CreateUri(HydraServices.AuthService1, HydraMethods.RefreshTokens));
        }

        public async Task<GetEnvironmentsResponse> GetEnvironments(GetEnvironmentsRequest request)
        {
            return await PostAsync<GetEnvironmentsRequest, GetEnvironmentsResponse>(
                request,
                CreateUri(HydraServices.SharedService, HydraMethods.GetEnvironments),
                expectContinue: true);
        }

        public async Task<GetTokensResponse> GetTokens(GetTokensRequest request)
        {
            return await PostAsync<GetTokensRequest, GetTokensResponse>(
                request,
                CreateUri(HydraServices.AuthService2, HydraMethods.GetTokens),
                expectContinue: true);
        }

        public async Task<LoginResponse> CheckLogin(CheckLoginRequest request)
        {
            TimeSpan rateLimit = TimeSpan.FromMilliseconds(500);
            return await PostAsync<CheckLoginRequest, LoginResponse>(
                request,
                CreateUri(HydraServices.AuthService2, HydraMethods.CheckLogin),
                expectContinue: true,
                rateLimit: rateLimit);
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            return await PostAsync<LoginRequest, LoginResponse>(
                request,
                CreateUri(HydraServices.AuthService2, HydraMethods.Login),
                expectContinue: true);
        }

        public async Task<GetChallengesResponse> GetChallenges(GetChallengesRequest request)
        {
            return await PostAsync<GetChallengesRequest, GetChallengesResponse>(
                request,
                CreateUri(HydraServices.UserService5, HydraMethods.GetChallenges),
                compress: true);
        }

        public async Task<GetDataCenterOccupationResponse> GetDataCenterOccupation(GetDataCenterOccupationRequest request)
        {
            return await PostAsync<GetDataCenterOccupationRequest, GetDataCenterOccupationResponse>(
                request,
                CreateUri(HydraServices.UhService, HydraMethods.GetDataCenterOccupation));
        }
        

        public async Task<LoginMessagingRequestResponse> LoginMessaging(LoginMessagingRequest request)
        {
            return await PostAsync<LoginMessagingRequest, LoginMessagingRequestResponse>(
                request,
                CreateUri(HydraServices.MessageService, HydraMethods.LoginMessaging));
        }
        
        public async Task<SendExternalSessionTokenResponse> SendExternalSessionToken(SendExternalSessionTokenRequest request)
        {
            return await PostAsync<SendExternalSessionTokenRequest, SendExternalSessionTokenResponse>(
                request,
                CreateUri(HydraServices.MessageService, HydraMethods.SendExternalSessionToken));
        }
        
        public async Task<Unknowna187495943b54cdca28ab8355bbe5898Response> Unknowna187495943b54cdca28ab8355bbe5898(Unknowna187495943b54cdca28ab8355bbe5898Request request)
        {
            return await PostAsync<Unknowna187495943b54cdca28ab8355bbe5898Request, Unknowna187495943b54cdca28ab8355bbe5898Response>(
                request,
                CreateUri(HydraServices.SharedService, HydraMethods.Methoda187495943b54cdca28ab8355bbe5898));
        }

        public async Task<Unknown3562511020674c16bed3979a9b2a9ef9Response> Unknown3562511020674c16bed3979a9b2a9ef9(Unknown3562511020674c16bed3979a9b2a9ef9Request request)
        {
            return await PostAsync<Unknown3562511020674c16bed3979a9b2a9ef9Request, Unknown3562511020674c16bed3979a9b2a9ef9Response>(
                request,
                CreateUri(HydraServices.AbstractService, HydraMethods.Method3562511020674c16bed3979a9b2a9ef9));
        }
        
        public async Task<Unknown4222aaa7e1254755af143c349e9a18efResponse> Unknown4222aaa7e1254755af143c349e9a18ef(Unknown4222aaa7e1254755af143c349e9a18efRequest request)
        {
            return await PostAsync<Unknown4222aaa7e1254755af143c349e9a18efRequest, Unknown4222aaa7e1254755af143c349e9a18efResponse>(
                request,
                CreateUri(HydraServices.AbstractService, HydraMethods.Method4222aaa7e1254755af143c349e9a18ef));
        }

        public async Task<Unknown43525158cd024b78be1522899e2c8e14Response> Unknown43525158cd024b78be1522899e2c8e14(Unknown43525158cd024b78be1522899e2c8e14Request request)
        {
            return await PostAsync<Unknown43525158cd024b78be1522899e2c8e14Request, Unknown43525158cd024b78be1522899e2c8e14Response>(
                request,
                CreateUri(HydraServices.AbstractService, HydraMethods.Method43525158cd024b78be1522899e2c8e14));
        }

        public async Task<Unknown8459aa2a4bc24ba990c170cc2ffac9b4Response> Unknown8459aa2a4bc24ba990c170cc2ffac9b4(Unknown8459aa2a4bc24ba990c170cc2ffac9b4Request request)
        {
            return await PostAsync<Unknown8459aa2a4bc24ba990c170cc2ffac9b4Request, Unknown8459aa2a4bc24ba990c170cc2ffac9b4Response>(
                request,
                CreateUri(HydraServices.AbstractService, HydraMethods.Method8459aa2a4bc24ba990c170cc2ffac9b4));
        }
        
        public async Task<UnknownAuthResponse> UnknownAuth(UnknownAuthRequest request)
        {
            return await PostAsync<UnknownAuthRequest, UnknownAuthResponse>(
                request,
                CreateUri(HydraServices.AuthService1, HydraMethods.UnknownAuth));
        }
        
        public async Task<UnknownTokenResponse> UnknownToken(UnknownTokenRequest request)
        {
            return await PostAsync<UnknownTokenRequest, UnknownTokenResponse>(
                request,
                CreateUri(HydraServices.AuthService1, HydraMethods.UnknownToken));
        }
        
        public async Task<Unknown3e61b8fd7afe45df9bc37786a3bb621eResponse> Unknown3e61b8fd7afe45df9bc37786a3bb621e(Unknown3e61b8fd7afe45df9bc37786a3bb621eRequest request)
        {
            return await PostAsync<Unknown3e61b8fd7afe45df9bc37786a3bb621eRequest, Unknown3e61b8fd7afe45df9bc37786a3bb621eResponse>(
                request,
                CreateUri(HydraServices.AuthService2, HydraMethods.Method3e61b8fd7afe45df9bc37786a3bb621e));
        }
        
        public async Task<Unknown79b6105a955c4235b1344048b1f278cfResponse> Unknown79b6105a955c4235b1344048b1f278cf(Unknown79b6105a955c4235b1344048b1f278cfRequest request)
        {
            return await PostAsync<Unknown79b6105a955c4235b1344048b1f278cfRequest, Unknown79b6105a955c4235b1344048b1f278cfResponse>(
                request,
                CreateUri(HydraServices.AuthService2, HydraMethods.Method79b6105a955c4235b1344048b1f278cf));
        }
        
        public async Task<Unknowna9dd19e747ea46a98aed7dc6058aebdcResponse> Unknowna9dd19e747ea46a98aed7dc6058aebdc(Unknowna9dd19e747ea46a98aed7dc6058aebdcRequest request)
        {
            return await PostAsync<Unknowna9dd19e747ea46a98aed7dc6058aebdcRequest, Unknowna9dd19e747ea46a98aed7dc6058aebdcResponse>(
                request,
                CreateUri(HydraServices.AuthService2, HydraMethods.Methoda9dd19e747ea46a98aed7dc6058aebdc));
        }

        public async Task<Unknown17a2ffa34c43424bb257658746e55c2dResponse> Unknown17a2ffa34c43424bb257658746e55c2d(Unknown17a2ffa34c43424bb257658746e55c2dRequest request)
        {
            return await PostAsync<Unknown17a2ffa34c43424bb257658746e55c2dRequest, Unknown17a2ffa34c43424bb257658746e55c2dResponse>(
                request,
                CreateUri(HydraServices.DiagnosticService, HydraMethods.Method17a2ffa34c43424bb257658746e55c2d));
        }
        
        public async Task<Unknown75f49a6bb7374463aecc191755b88ef8Response> Unknown75f49a6bb7374463aecc191755b88ef8(Unknown75f49a6bb7374463aecc191755b88ef8Request request)
        {
            return await PostAsync<Unknown75f49a6bb7374463aecc191755b88ef8Request, Unknown75f49a6bb7374463aecc191755b88ef8Response>(
                request,
                CreateUri(HydraServices.DiagnosticService, HydraMethods.Method75f49a6bb7374463aecc191755b88ef8));
        }
        
        public async Task<Unknown8d3bb2f3cfa54bb0a7e3d3eabb83329aResponse> Unknown8d3bb2f3cfa54bb0a7e3d3eabb83329a(Unknown8d3bb2f3cfa54bb0a7e3d3eabb83329aRequest request)
        {
            return await PostAsync<Unknown8d3bb2f3cfa54bb0a7e3d3eabb83329aRequest, Unknown8d3bb2f3cfa54bb0a7e3d3eabb83329aResponse>(
                request,
                CreateUri(HydraServices.DiagnosticService, HydraMethods.Method8d3bb2f3cfa54bb0a7e3d3eabb83329a));
        }
        
        public async Task<Unknown8be9ba10a3004bab886a5ce8239a8d5cResponse> Unknown8be9ba10a3004bab886a5ce8239a8d5c(Unknown8be9ba10a3004bab886a5ce8239a8d5cRequest request)
        {
            return await PostAsync<Unknown8be9ba10a3004bab886a5ce8239a8d5cRequest, Unknown8be9ba10a3004bab886a5ce8239a8d5cResponse>(
                request,
                CreateUri(HydraServices.GameconfigService1, HydraMethods.Method8be9ba10a3004bab886a5ce8239a8d5c));
        }
        
        public async Task<Unknown4e0590bd5c1749d1bb60ac1dd9b524ceResponse> Unknown4e0590bd5c1749d1bb60ac1dd9b524ce(Unknown4e0590bd5c1749d1bb60ac1dd9b524ceRequest request)
        {
            return await PostAsync<Unknown4e0590bd5c1749d1bb60ac1dd9b524ceRequest, Unknown4e0590bd5c1749d1bb60ac1dd9b524ceResponse>(
                request,
                CreateUri(HydraServices.GameconfigService2, HydraMethods.Method4e0590bd5c1749d1bb60ac1dd9b524ce));
        }
        
        public async Task<Unknownb40a2687e1f84e808b65e95945751cd7Response> Unknownb40a2687e1f84e808b65e95945751cd7(Unknownb40a2687e1f84e808b65e95945751cd7Request request)
        {
            return await PostAsync<Unknownb40a2687e1f84e808b65e95945751cd7Request, Unknownb40a2687e1f84e808b65e95945751cd7Response>(
                request,
                CreateUri(HydraServices.GameconfigService2, HydraMethods.Methodb40a2687e1f84e808b65e95945751cd7));
        }
        
        public async Task<Unknownc05cdc1fd6f44c419591d39408d27a51Response> Unknownc05cdc1fd6f44c419591d39408d27a51(Unknownc05cdc1fd6f44c419591d39408d27a51Request request)
        {
            return await PostAsync<Unknownc05cdc1fd6f44c419591d39408d27a51Request, Unknownc05cdc1fd6f44c419591d39408d27a51Response>(
                request,
                CreateUri(HydraServices.GameconfigService2, HydraMethods.Methodc05cdc1fd6f44c419591d39408d27a51));
        }
        
        public async Task<Unknown818178d3eec145d6adc2f35a6d241ccdResponse> Unknown818178d3eec145d6adc2f35a6d241ccd(Unknown818178d3eec145d6adc2f35a6d241ccdRequest request)
        {
            return await PostAsync<Unknown818178d3eec145d6adc2f35a6d241ccdRequest, Unknown818178d3eec145d6adc2f35a6d241ccdResponse>(
                request,
                CreateUri(HydraServices.GameconfigService3, HydraMethods.Method818178d3eec145d6adc2f35a6d241ccd));
        }
        
        public async Task<Unknown0e7ebdaeae61462ab6eba53337b9f509Response> Unknown0e7ebdaeae61462ab6eba53337b9f509(Unknown0e7ebdaeae61462ab6eba53337b9f509Request request)
        {
            return await PostAsync<Unknown0e7ebdaeae61462ab6eba53337b9f509Request, Unknown0e7ebdaeae61462ab6eba53337b9f509Response>(
                request,
                CreateUri(HydraServices.MessageService, HydraMethods.Method0e7ebdaeae61462ab6eba53337b9f509));
        }

        public async Task<Unknown2cf0a2cc6a3b4365a6e1596fec1ee658Response> Unknown2cf0a2cc6a3b4365a6e1596fec1ee658(Unknown2cf0a2cc6a3b4365a6e1596fec1ee658Request request)
        {
            return await PostAsync<Unknown2cf0a2cc6a3b4365a6e1596fec1ee658Request, Unknown2cf0a2cc6a3b4365a6e1596fec1ee658Response>(
                request,
                CreateUri(HydraServices.MessageService, HydraMethods.Method2cf0a2cc6a3b4365a6e1596fec1ee658));
        }

        public async Task<Unknown2ff9bd7fc6f2475a9b9e042529de70f7Response> Unknown2ff9bd7fc6f2475a9b9e042529de70f7(Unknown2ff9bd7fc6f2475a9b9e042529de70f7Request request)
        {
            return await PostAsync<Unknown2ff9bd7fc6f2475a9b9e042529de70f7Request, Unknown2ff9bd7fc6f2475a9b9e042529de70f7Response>(
                request,
                CreateUri(HydraServices.MessageService, HydraMethods.Method2ff9bd7fc6f2475a9b9e042529de70f7));
        }
        
        public async Task<Unknown362afabd85654a57866e30b9121b3e39Response> Unknown362afabd85654a57866e30b9121b3e39(Unknown362afabd85654a57866e30b9121b3e39Request request)
        {
            return await PostAsync<Unknown362afabd85654a57866e30b9121b3e39Request, Unknown362afabd85654a57866e30b9121b3e39Response>(
                request,
                CreateUri(HydraServices.MessageService, HydraMethods.Method362afabd85654a57866e30b9121b3e39));
        }
        
        public async Task<Unknown7873eebd874343ca98edbb6ca48a18c1Response> Unknown7873eebd874343ca98edbb6ca48a18c1(Unknown7873eebd874343ca98edbb6ca48a18c1Request request)
        {
            return await PostAsync<Unknown7873eebd874343ca98edbb6ca48a18c1Request, Unknown7873eebd874343ca98edbb6ca48a18c1Response>(
                request,
                CreateUri(HydraServices.MessageService, HydraMethods.Method7873eebd874343ca98edbb6ca48a18c1));
        }

        public async Task<Unknowna692be4389e04e35b9023864e5146e97Response> Unknowna692be4389e04e35b9023864e5146e97(Unknowna692be4389e04e35b9023864e5146e97Request request)
        {
            return await PostAsync<Unknowna692be4389e04e35b9023864e5146e97Request, Unknowna692be4389e04e35b9023864e5146e97Response>(
                request,
                CreateUri(HydraServices.MessageService, HydraMethods.Methoda692be4389e04e35b9023864e5146e97));
        }
        
        public async Task<Unknown06e4cf76e6d148769543e0774aad4b73Response> Unknown06e4cf76e6d148769543e0774aad4b73(Unknown06e4cf76e6d148769543e0774aad4b73Request request)
        {
            return await PostAsync<Unknown06e4cf76e6d148769543e0774aad4b73Request, Unknown06e4cf76e6d148769543e0774aad4b73Response>(
                request,
                CreateUri(HydraServices.PresenceService1, HydraMethods.Method06e4cf76e6d148769543e0774aad4b73));
        }
        
        public async Task<UnknownPresence1Response> UnknownPresence1(UnknownPresence1Request request)
        {
            return await PostAsync<UnknownPresence1Request, UnknownPresence1Response>(
                request,
                CreateUri(HydraServices.PresenceService1, HydraMethods.UnknownPresence1));
        }
        
        public async Task<Unknown1223411d79164d0497e74abf8edc922aResponse> Unknown1223411d79164d0497e74abf8edc922a(Unknown1223411d79164d0497e74abf8edc922aRequest request)
        {
            return await PostAsync<Unknown1223411d79164d0497e74abf8edc922aRequest, Unknown1223411d79164d0497e74abf8edc922aResponse>(
                request,
                CreateUri(HydraServices.PresenceService1, HydraMethods.Method1223411d79164d0497e74abf8edc922a));
        }
        
        public async Task<Unknown2c5326857083430eb3b2e27d8a82d5c5Response> Unknown2c5326857083430eb3b2e27d8a82d5c5(Unknown2c5326857083430eb3b2e27d8a82d5c5Request request)
        {
            return await PostAsync<Unknown2c5326857083430eb3b2e27d8a82d5c5Request, Unknown2c5326857083430eb3b2e27d8a82d5c5Response>(
                request,
                CreateUri(HydraServices.PresenceService1, HydraMethods.Method2c5326857083430eb3b2e27d8a82d5c5));
        }
        
        public async Task<Unknown31710983bb924d55adea3c074920e52bResponse> Unknown31710983bb924d55adea3c074920e52b(Unknown31710983bb924d55adea3c074920e52bRequest request)
        {
            return await PostAsync<Unknown31710983bb924d55adea3c074920e52bRequest, Unknown31710983bb924d55adea3c074920e52bResponse>(
                request,
                CreateUri(HydraServices.PresenceService1, HydraMethods.Method31710983bb924d55adea3c074920e52b));
        }
        
        public async Task<Unknown357a419ab4ce4d89ad01275c2b513576Response> Unknown357a419ab4ce4d89ad01275c2b513576(Unknown357a419ab4ce4d89ad01275c2b513576Request request)
        {
            return await PostAsync<Unknown357a419ab4ce4d89ad01275c2b513576Request, Unknown357a419ab4ce4d89ad01275c2b513576Response>(
                request,
                CreateUri(HydraServices.PresenceService1, HydraMethods.Method357a419ab4ce4d89ad01275c2b513576));
        }
        
        public async Task<Unknown3a60e3c677124e76bb861d9655acf9d6Response> Unknown3a60e3c677124e76bb861d9655acf9d6(Unknown3a60e3c677124e76bb861d9655acf9d6Request request)
        {
            return await PostAsync<Unknown3a60e3c677124e76bb861d9655acf9d6Request, Unknown3a60e3c677124e76bb861d9655acf9d6Response>(
                request,
                CreateUri(HydraServices.PresenceService1, HydraMethods.Method3a60e3c677124e76bb861d9655acf9d6));
        }
        
        public async Task<UnknownPresenceResponse> UnknownPresenceTournamentMatch(UnknownPresenceTournamentMatchRequest request)
        {
            return await PostAsync<UnknownPresenceTournamentMatchRequest, UnknownPresenceResponse>(
                request,
                CreateUri(HydraServices.PresenceService1, HydraMethods.UnknownPresenceTournamentMatch));
        }
        
        public async Task<Unknown4342d3041be74aebad69d24c06b8cdceResponse> Unknown4342d3041be74aebad69d24c06b8cdce(Unknown4342d3041be74aebad69d24c06b8cdceRequest request)
        {
            return await PostAsync<Unknown4342d3041be74aebad69d24c06b8cdceRequest, Unknown4342d3041be74aebad69d24c06b8cdceResponse>(
                request,
                CreateUri(HydraServices.PresenceService1, HydraMethods.Method4342d3041be74aebad69d24c06b8cdce));
        }

        public async Task<Unknown481d6938535d4379b61cd726a9b54c08Response> Unknown481d6938535d4379b61cd726a9b54c08(Unknown481d6938535d4379b61cd726a9b54c08Request request)
        {
            return await PostAsync<Unknown481d6938535d4379b61cd726a9b54c08Request, Unknown481d6938535d4379b61cd726a9b54c08Response>(
                request,
                CreateUri(HydraServices.PresenceService1, HydraMethods.Method481d6938535d4379b61cd726a9b54c08));
        }
        
        public async Task<Unknown495afb4580ef48d3b7d39fb625165aaaResponse> Unknown495afb4580ef48d3b7d39fb625165aaa(Unknown495afb4580ef48d3b7d39fb625165aaaRequest request)
        {
            return await PostAsync<Unknown495afb4580ef48d3b7d39fb625165aaaRequest, Unknown495afb4580ef48d3b7d39fb625165aaaResponse>(
                request,
                CreateUri(HydraServices.PresenceService1, HydraMethods.Method495afb4580ef48d3b7d39fb625165aaa));
        }
        
        public async Task<Unknown77f0b9cf113141baa2d40e0ac53a6eb8Response> Unknown77f0b9cf113141baa2d40e0ac53a6eb8(Unknown77f0b9cf113141baa2d40e0ac53a6eb8Request request)
        {
            return await PostAsync<Unknown77f0b9cf113141baa2d40e0ac53a6eb8Request, Unknown77f0b9cf113141baa2d40e0ac53a6eb8Response>(
                request,
                CreateUri(HydraServices.PresenceService1, HydraMethods.Method77f0b9cf113141baa2d40e0ac53a6eb8));
        }

        public async Task<Unknown7903a8b8d4ce455da96449ac0d862121Response> Unknown7903a8b8d4ce455da96449ac0d862121(Unknown7903a8b8d4ce455da96449ac0d862121Request request)
        {
            return await PostAsync<Unknown7903a8b8d4ce455da96449ac0d862121Request, Unknown7903a8b8d4ce455da96449ac0d862121Response>(
                request,
                CreateUri(HydraServices.PresenceService1, HydraMethods.Method7903a8b8d4ce455da96449ac0d862121));
        }

        public async Task<UnknownPresence2Response> UnknownPresence2(UnknownPresence2Request request)
        {
            return await PostAsync<UnknownPresence2Request, UnknownPresence2Response>(
                request,
                CreateUri(HydraServices.PresenceService1, HydraMethods.UnknownPresence2));
        }

        public async Task<UnknownPresenceResponse> UnknownPresenceMatchmake1(UnknownPresenceMatchmake1Request request)
        {
            return await PostAsync<UnknownPresenceMatchmake1Request, UnknownPresenceResponse>(
                request,
                CreateUri(HydraServices.PresenceService1, HydraMethods.UnknownPresenceMatchmake1));
        }

        public async Task<UnknownPresenceResponse> UnknownPresenceMatchmake2(UnknownPresenceMatchmake2Request request)
        {
            return await PostAsync<UnknownPresenceMatchmake2Request, UnknownPresenceResponse>(
                request,
                CreateUri(HydraServices.PresenceService1, HydraMethods.UnknownPresenceMatchmake2));
        }

        public async Task<Unknown914980e66e5c4b6d973a291ffb094677Response> Unknown914980e66e5c4b6d973a291ffb094677(Unknown914980e66e5c4b6d973a291ffb094677Request request)
        {
            return await PostAsync<Unknown914980e66e5c4b6d973a291ffb094677Request, Unknown914980e66e5c4b6d973a291ffb094677Response>(
                request,
                CreateUri(HydraServices.PresenceService1, HydraMethods.Method914980e66e5c4b6d973a291ffb094677));
        }

        public async Task<Unknown9ab159368de34865b9c5f4b0b5bdb9e3Response> Unknown9ab159368de34865b9c5f4b0b5bdb9e3(Unknown9ab159368de34865b9c5f4b0b5bdb9e3Request request)
        {
            return await PostAsync<Unknown9ab159368de34865b9c5f4b0b5bdb9e3Request, Unknown9ab159368de34865b9c5f4b0b5bdb9e3Response>(
                request,
                CreateUri(HydraServices.PresenceService1, HydraMethods.Method9ab159368de34865b9c5f4b0b5bdb9e3));
        }

        public async Task<UnknownPresenceResponse> UnknownPresenceSquad1(UnknownPresenceSquad1Request request)
        {
            return await PostAsync<UnknownPresenceSquad1Request, UnknownPresenceResponse>(
                request,
                CreateUri(HydraServices.PresenceService1, HydraMethods.UnknownPresenceSquad1));
        }
        
        public async Task<UnknownPresenceResponse> UnknownPresenceSquad2(UnknownPresenceSquad2Request request)
        {
            return await PostAsync<UnknownPresenceSquad2Request, UnknownPresenceResponse>(
                request,
                CreateUri(HydraServices.PresenceService1, HydraMethods.UnknownPresenceSquad2));
        }
        
        public async Task<UnknownPresenceResponse> UnknownPresenceSquad3(UnknownPresenceSquad3Request request)
        {
            return await PostAsync<UnknownPresenceSquad3Request, UnknownPresenceResponse>(
                request,
                CreateUri(HydraServices.PresenceService1, HydraMethods.UnknownPresenceSquad3));
        }
        
        public async Task<UnknownPresenceResponse> UnknownPresenceMatchmake3(UnknownPresenceMatchmake3Request request)
        {
            return await PostAsync<UnknownPresenceMatchmake3Request, UnknownPresenceResponse>(
                request,
                CreateUri(HydraServices.PresenceService1, HydraMethods.UnknownPresenceMatchmake3));
        }
        
        public async Task<Unknownb5e4f488c68d4e95959c0144330908f6Response> Unknownb5e4f488c68d4e95959c0144330908f6(Unknownb5e4f488c68d4e95959c0144330908f6Request request)
        {
            return await PostAsync<Unknownb5e4f488c68d4e95959c0144330908f6Request, Unknownb5e4f488c68d4e95959c0144330908f6Response>(
                request,
                CreateUri(HydraServices.PresenceService1, HydraMethods.Methodb5e4f488c68d4e95959c0144330908f6));
        }
        
        public async Task<Unknownc2b4cc19b0a74acb8a59186f1f7119b0Response> Unknownc2b4cc19b0a74acb8a59186f1f7119b0(Unknownc2b4cc19b0a74acb8a59186f1f7119b0Request request)
        {
            return await PostAsync<Unknownc2b4cc19b0a74acb8a59186f1f7119b0Request, Unknownc2b4cc19b0a74acb8a59186f1f7119b0Response>(
                request,
                CreateUri(HydraServices.PresenceService1, HydraMethods.Methodc2b4cc19b0a74acb8a59186f1f7119b0));
        }
        
        public async Task<Unknownd331ec8194e94c179f80ef22271475fdResponse> Unknownd331ec8194e94c179f80ef22271475fd(Unknownd331ec8194e94c179f80ef22271475fdRequest request)
        {
            return await PostAsync<Unknownd331ec8194e94c179f80ef22271475fdRequest, Unknownd331ec8194e94c179f80ef22271475fdResponse>(
                request,
                CreateUri(HydraServices.PresenceService1, HydraMethods.Methodd331ec8194e94c179f80ef22271475fd));
        }
        
        public async Task<Unknownd3b5c5894eaf4b5984a3da64cf492359Response> Unknownd3b5c5894eaf4b5984a3da64cf492359(Unknownd3b5c5894eaf4b5984a3da64cf492359Request request)
        {
            return await PostAsync<Unknownd3b5c5894eaf4b5984a3da64cf492359Request, Unknownd3b5c5894eaf4b5984a3da64cf492359Response>(
                request,
                CreateUri(HydraServices.PresenceService1, HydraMethods.Methodd3b5c5894eaf4b5984a3da64cf492359));
        }

        public async Task<Unknownda785897995d47fc83b96b40f41bcb59Response> Unknownda785897995d47fc83b96b40f41bcb59(Unknownda785897995d47fc83b96b40f41bcb59Request request)
        {
            return await PostAsync<Unknownda785897995d47fc83b96b40f41bcb59Request, Unknownda785897995d47fc83b96b40f41bcb59Response>(
                request,
                CreateUri(HydraServices.PresenceService1, HydraMethods.Methodda785897995d47fc83b96b40f41bcb59));
        }

        public async Task<Unknowndac0fa31a1a241f8b5a8e033e8e497e6Response> Unknowndac0fa31a1a241f8b5a8e033e8e497e6(Unknowndac0fa31a1a241f8b5a8e033e8e497e6Request request)
        {
            return await PostAsync<Unknowndac0fa31a1a241f8b5a8e033e8e497e6Request, Unknowndac0fa31a1a241f8b5a8e033e8e497e6Response>(
                request,
                CreateUri(HydraServices.PresenceService1, HydraMethods.Methoddac0fa31a1a241f8b5a8e033e8e497e6));
        }

        public async Task<Unknowne4c06258140645bbb39c63a8a0cd230eResponse> Unknowne4c06258140645bbb39c63a8a0cd230e(Unknowne4c06258140645bbb39c63a8a0cd230eRequest request)
        {
            return await PostAsync<Unknowne4c06258140645bbb39c63a8a0cd230eRequest, Unknowne4c06258140645bbb39c63a8a0cd230eResponse>(
                request,
                CreateUri(HydraServices.PresenceService1, HydraMethods.Methode4c06258140645bbb39c63a8a0cd230e));
        }
        
        public async Task<UnknownPresenceResponse> UnknownPresenceSquad4(UnknownPresenceSquad4Request request)
        {
            return await PostAsync<UnknownPresenceSquad4Request, UnknownPresenceResponse>(
                request,
                CreateUri(HydraServices.PresenceService1, HydraMethods.MethodUnknownPresenceSquad4));
        }
        
        public async Task<UnknownPresenceResponse> UnknownPresenceMatchmake4(UnknownPresenceMatchmake4Request request)
        {
            return await PostAsync<UnknownPresenceMatchmake4Request, UnknownPresenceResponse>(
                request,
                CreateUri(HydraServices.PresenceService1, HydraMethods.MethodUnknownPresenceMatchmake4));
        }

        public async Task<UnknownPresenceResponse> UnknownPresenceTournament(UnknownPresenceTournamentRequest request)
        {
            return await PostAsync<UnknownPresenceTournamentRequest, UnknownPresenceResponse>(
                request,
                CreateUri(HydraServices.PresenceService1, HydraMethods.MethodUnknownPresenceTournament));
        }

        public async Task<Unknown2edd52375d674e52821b941778906a8bResponse> Unknown2edd52375d674e52821b941778906a8b(Unknown2edd52375d674e52821b941778906a8bRequest request)
        {
            return await PostAsync<Unknown2edd52375d674e52821b941778906a8bRequest, Unknown2edd52375d674e52821b941778906a8bResponse>(
                request,
                CreateUri(HydraServices.PresenceService2, HydraMethods.Method2edd52375d674e52821b941778906a8b));
        }

        public async Task<Unknown3bbc298631fc402ea804318bd81c9e61Response> Unknown3bbc298631fc402ea804318bd81c9e61(Unknown3bbc298631fc402ea804318bd81c9e61Request request)
        {
            return await PostAsync<Unknown3bbc298631fc402ea804318bd81c9e61Request, Unknown3bbc298631fc402ea804318bd81c9e61Response>(
                request,
                CreateUri(HydraServices.PresenceService2, HydraMethods.Method3bbc298631fc402ea804318bd81c9e61));
        }

        public async Task<Unknown10ef138f7e3a4d89be76954001d2c8e9Response> Unknown10ef138f7e3a4d89be76954001d2c8e9(Unknown10ef138f7e3a4d89be76954001d2c8e9Request request)
        {
            return await PostAsync<Unknown10ef138f7e3a4d89be76954001d2c8e9Request, Unknown10ef138f7e3a4d89be76954001d2c8e9Response>(
                request,
                CreateUri(HydraServices.UhService, HydraMethods.Method10ef138f7e3a4d89be76954001d2c8e9));
        }
        
        public async Task<Unknown20ab2a71bc824d28b0336677fb4e7702Response> Unknown20ab2a71bc824d28b0336677fb4e7702(Unknown20ab2a71bc824d28b0336677fb4e7702Request request)
        {
            return await PostAsync<Unknown20ab2a71bc824d28b0336677fb4e7702Request, Unknown20ab2a71bc824d28b0336677fb4e7702Response>(
                request,
                CreateUri(HydraServices.UhService, HydraMethods.Method20ab2a71bc824d28b0336677fb4e7702));
        }
        

        public async Task<Unknown255c934598874d688ac024552a4c8184Response> Unknown255c934598874d688ac024552a4c8184(Unknown255c934598874d688ac024552a4c8184Request request)
        {
            return await PostAsync<Unknown255c934598874d688ac024552a4c8184Request, Unknown255c934598874d688ac024552a4c8184Response>(
                request,
                CreateUri(HydraServices.UserService4, HydraMethods.Method255c934598874d688ac024552a4c8184));
        }
        
        public async Task<Unknown605000618bbf46078491664420dd524aResponse> Unknown605000618bbf46078491664420dd524a(Unknown605000618bbf46078491664420dd524aRequest request)
        {
            return await PostAsync<Unknown605000618bbf46078491664420dd524aRequest, Unknown605000618bbf46078491664420dd524aResponse>(
                request,
                CreateUri(HydraServices.UserService4, HydraMethods.Method605000618bbf46078491664420dd524a));
        }

        public async Task<Unknown95090912d7d14fb4be6450c138ca9371Response> Unknown95090912d7d14fb4be6450c138ca9371(Unknown95090912d7d14fb4be6450c138ca9371Request request)
        {
            return await PostAsync<Unknown95090912d7d14fb4be6450c138ca9371Request, Unknown95090912d7d14fb4be6450c138ca9371Response>(
                request,
                CreateUri(HydraServices.UserService4, HydraMethods.Method95090912d7d14fb4be6450c138ca9371));
        }
        
        public async Task<Unknown1df66e3e7245462e9cbd24c6ccb78219Response> Unknown1df66e3e7245462e9cbd24c6ccb78219(Unknown1df66e3e7245462e9cbd24c6ccb78219Request request)
        {
            return await PostAsync<Unknown1df66e3e7245462e9cbd24c6ccb78219Request, Unknown1df66e3e7245462e9cbd24c6ccb78219Response>(
                request,
                CreateUri(HydraServices.UserService3, HydraMethods.Method1df66e3e7245462e9cbd24c6ccb78219));
        }
        
        public async Task<Unknown29db08d7f205403b96d2c661c5a21fb5Response> Unknown29db08d7f205403b96d2c661c5a21fb5(Unknown29db08d7f205403b96d2c661c5a21fb5Request request)
        {
            return await PostAsync<Unknown29db08d7f205403b96d2c661c5a21fb5Request, Unknown29db08d7f205403b96d2c661c5a21fb5Response>(
                request,
                CreateUri(HydraServices.UserService3, HydraMethods.Method29db08d7f205403b96d2c661c5a21fb5));
        }
        
        public async Task<Unknown48c8e3ccec72406da06c5ceb3b61307eResponse> Unknown48c8e3ccec72406da06c5ceb3b61307e(Unknown48c8e3ccec72406da06c5ceb3b61307eRequest request)
        {
            return await PostAsync<Unknown48c8e3ccec72406da06c5ceb3b61307eRequest, Unknown48c8e3ccec72406da06c5ceb3b61307eResponse>(
                request,
                CreateUri(HydraServices.UserService3, HydraMethods.Method48c8e3ccec72406da06c5ceb3b61307e));
        }
        
        public async Task<Unknown672167912f854154b40175aed5a203deResponse> Unknown672167912f854154b40175aed5a203de(Unknown672167912f854154b40175aed5a203deRequest request)
        {
            return await PostAsync<Unknown672167912f854154b40175aed5a203deRequest, Unknown672167912f854154b40175aed5a203deResponse>(
                request,
                CreateUri(HydraServices.UserService3, HydraMethods.Method672167912f854154b40175aed5a203de));
        }
        
        public async Task<Unknowne34e2a4286704499bb5d195ea4b06e62Response> Unknowne34e2a4286704499bb5d195ea4b06e62(Unknowne34e2a4286704499bb5d195ea4b06e62Request request)
        {
            return await PostAsync<Unknowne34e2a4286704499bb5d195ea4b06e62Request, Unknowne34e2a4286704499bb5d195ea4b06e62Response>(
                request,
                CreateUri(HydraServices.UserService3, HydraMethods.Methode34e2a4286704499bb5d195ea4b06e62));
        }
        
        public async Task<Unknown25f40aab5feb4370b3d60d5960cb5fd0Response> Unknown25f40aab5feb4370b3d60d5960cb5fd0(Unknown25f40aab5feb4370b3d60d5960cb5fd0Request request)
        {
            return await PostAsync<Unknown25f40aab5feb4370b3d60d5960cb5fd0Request, Unknown25f40aab5feb4370b3d60d5960cb5fd0Response>(
                request,
                CreateUri(HydraServices.UserService1, HydraMethods.Method25f40aab5feb4370b3d60d5960cb5fd0));
        }
        
        public async Task<Unknown76bca0c11f104b59a6c23d8a566771efResponse> Unknown76bca0c11f104b59a6c23d8a566771ef(Unknown76bca0c11f104b59a6c23d8a566771efRequest request)
        {
            return await PostAsync<Unknown76bca0c11f104b59a6c23d8a566771efRequest, Unknown76bca0c11f104b59a6c23d8a566771efResponse>(
                request,
                CreateUri(HydraServices.UserService1, HydraMethods.Method76bca0c11f104b59a6c23d8a566771ef));
        }
        
        public async Task<Unknown9ba54a165e594fb59a720d051d55f40cResponse> Unknown9ba54a165e594fb59a720d051d55f40c(Unknown9ba54a165e594fb59a720d051d55f40cRequest request)
        {
            return await PostAsync<Unknown9ba54a165e594fb59a720d051d55f40cRequest, Unknown9ba54a165e594fb59a720d051d55f40cResponse>(
                request,
                CreateUri(HydraServices.UserService1, HydraMethods.Method9ba54a165e594fb59a720d051d55f40c));
        }
        
        public async Task<Unknownb5d139571f3b48f6a6b17743b490dfbaResponse> Unknownb5d139571f3b48f6a6b17743b490dfba(Unknownb5d139571f3b48f6a6b17743b490dfbaRequest request)
        {
            return await PostAsync<Unknownb5d139571f3b48f6a6b17743b490dfbaRequest, Unknownb5d139571f3b48f6a6b17743b490dfbaResponse>(
                request,
                CreateUri(HydraServices.UserService1, HydraMethods.Methodb5d139571f3b48f6a6b17743b490dfba));
        }
        
        public async Task<Unknownd094522958ee4e47ac73e9e5a9c9b1e4Response> Unknownd094522958ee4e47ac73e9e5a9c9b1e4(Unknownd094522958ee4e47ac73e9e5a9c9b1e4Request request)
        {
            return await PostAsync<Unknownd094522958ee4e47ac73e9e5a9c9b1e4Request, Unknownd094522958ee4e47ac73e9e5a9c9b1e4Response>(
                request,
                CreateUri(HydraServices.UserService1, HydraMethods.Methodd094522958ee4e47ac73e9e5a9c9b1e4));
        }
        
        public async Task<Unknownef350d399b3648928dbe92660df902a3Response> Unknownef350d399b3648928dbe92660df902a3(Unknownef350d399b3648928dbe92660df902a3Request request)
        {
            return await PostAsync<Unknownef350d399b3648928dbe92660df902a3Request, Unknownef350d399b3648928dbe92660df902a3Response>(
                request,
                CreateUri(HydraServices.UserService1, HydraMethods.Methodef350d399b3648928dbe92660df902a3));
        }
        
        public async Task<Unknown6f7a1ca925674cb6b0db585cab8edb53Response> Unknown6f7a1ca925674cb6b0db585cab8edb53(Unknown6f7a1ca925674cb6b0db585cab8edb53Request request)
        {
            return await PostAsync<Unknown6f7a1ca925674cb6b0db585cab8edb53Request, Unknown6f7a1ca925674cb6b0db585cab8edb53Response>(
                request,
                CreateUri(HydraServices.UserService5, HydraMethods.Method6f7a1ca925674cb6b0db585cab8edb53));
        }
        
        public async Task<Unknownbc099ffb41aa45c38525b0d40d7309a0Response> Unknownbc099ffb41aa45c38525b0d40d7309a0(Unknownbc099ffb41aa45c38525b0d40d7309a0Request request)
        {
            return await PostAsync<Unknownbc099ffb41aa45c38525b0d40d7309a0Request, Unknownbc099ffb41aa45c38525b0d40d7309a0Response>(
                request,
                CreateUri(HydraServices.UserService5, HydraMethods.Methodbc099ffb41aa45c38525b0d40d7309a0));
        }
        
        public async Task<Unknown8117a48a84f942dea6e057c53f06fbe5Response> Unknown8117a48a84f942dea6e057c53f06fbe5(Unknown8117a48a84f942dea6e057c53f06fbe5Request request)
        {
            return await PostAsync<Unknown8117a48a84f942dea6e057c53f06fbe5Request, Unknown8117a48a84f942dea6e057c53f06fbe5Response>(
                request,
                CreateUri(HydraServices.UserService2, HydraMethods.Method8117a48a84f942dea6e057c53f06fbe5));
        }
        
        public void UpdateAuthorizationToken(string authorizationToken)
        {
            _httpClient.DefaultRequestHeaders.Remove("USER_CONTEXT");
            _httpClient.DefaultRequestHeaders.Add("USER_CONTEXT", authorizationToken);
        }

        public void UpdateAccessRoleToken(string accessRoleToken)
        {
            _httpClient.DefaultRequestHeaders.Remove("ACL-Token");
            _httpClient.DefaultRequestHeaders.Add("ACL-Token", accessRoleToken);
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
            TimeSpan? rateLimit = null) where TResponse : ServiceResult
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