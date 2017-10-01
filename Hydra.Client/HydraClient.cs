using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Hydra.Client.Config;
using Hydra.Client.Exceptions;
using Hydra.Client.Http;
using Hydra.Client.Models;
using Hydra.Client.Models.Hydra;
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
            // TODO: Serialize/Deserialize: application/x-hydra-binary + application/json + compress
            return await PostAsync<Unknown3562511020674c16bed3979a9b2a9ef9Request, Unknown3562511020674c16bed3979a9b2a9ef9Response>(
                request,
                CreateUri(HydraServices.AbstractService, HydraMethods.Method3562511020674c16bed3979a9b2a9ef9));
        }
        
        public async Task<GetContainerByNameResponse> GetContainerByName(GetContainerByNameRequest request)
        {
            // TODO: Serialize/Deserialize: application/x-hydra-binary + application/json + compress
            return await PostAsync<GetContainerByNameRequest, GetContainerByNameResponse>(
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

        public async Task<GetMessageChannelsByNameGsaResponse> GetMessageChannelsByNameGsa(GetMessageChannelsByNameGsaRequest request)
        {
            return await PostAsync<GetMessageChannelsByNameGsaRequest, GetMessageChannelsByNameGsaResponse>(
                request,
                CreateUri(HydraServices.MessageService, HydraMethods.GetMessageChannelsByNameGsa));
        }

        public async Task<UnknownMessageServiceResponse> UnknownMessageService(UnknownMessageServiceRequest request)
        {
            return await PostAsync<UnknownMessageServiceRequest, UnknownMessageServiceResponse>(
                request,
                CreateUri(HydraServices.MessageService, HydraMethods.UnknownMessageService));
        }
        
        public async Task<GetMessageChannelsResponse> GetMessageChannels(GetMessageChannelsRequest request)
        {
            return await PostAsync<GetMessageChannelsRequest, GetMessageChannelsResponse>(
                request,
                CreateUri(HydraServices.MessageService, HydraMethods.GetMessageChannels));
        }
        
        public async Task<GetMessageChannelsByNameResponse> GetMessageChannelsByName(GetMessageChannelsByNameRequest request)
        {
            return await PostAsync<GetMessageChannelsByNameRequest, GetMessageChannelsByNameResponse>(
                request,
                CreateUri(HydraServices.MessageService, HydraMethods.GetMessageChannelsByName));
        }

        public async Task<SendChannelMessageResponse> SendChannelMessage(SendChannelMessageRequest request)
        {
            return await PostAsync<SendChannelMessageRequest, SendChannelMessageResponse>(
                request,
                CreateUri(HydraServices.MessageService, HydraMethods.SendChannelMessage));
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
        
        public async Task<UpdatePresenceStateResponse> UpdatePresenceState(UpdatePresenceStateRequest request)
        {
            return await PostAsync<UpdatePresenceStateRequest, UpdatePresenceStateResponse>(
                request,
                CreateUri(HydraServices.PresenceService1, HydraMethods.UpdatePresenceState));
        }
        
        public async Task<SquadInviteResponse> SquadInvite(SquadInviteRequest request)
        {
            return await PostAsync<SquadInviteRequest, SquadInviteResponse>(
                request,
                CreateUri(HydraServices.PresenceService1, HydraMethods.SquadInvite));
        }
        
        public async Task<UnknownPresenceUser2Response> UnknownPresenceUser2(UnknownPresenceUser2Request request)
        {
            return await PostAsync<UnknownPresenceUser2Request, UnknownPresenceUser2Response>(
                request,
                CreateUri(HydraServices.PresenceService1, HydraMethods.UnknownPresenceUser2));
        }
        
        public async Task<GetUsersClientStateResponse> GetUsersClientState(GetUsersClientStateRequest request)
        {
            return await PostAsync<GetUsersClientStateRequest, GetUsersClientStateResponse>(
                request,
                CreateUri(HydraServices.PresenceService1, HydraMethods.GetUsersClientState));
        }
        
        public async Task<UnknownPresenceResponse> UnknownPresenceTournamentMatch(UnknownPresenceTournamentMatchRequest request)
        {
            return await PostAsync<UnknownPresenceTournamentMatchRequest, UnknownPresenceResponse>(
                request,
                CreateUri(HydraServices.PresenceService1, HydraMethods.UnknownPresenceTournamentMatch));
        }
        
        public async Task<UnknownPresenceResponse> UnknownPresenceUser1(UnknownPresenceUser1Request request)
        {
            return await PostAsync<UnknownPresenceUser1Request, UnknownPresenceResponse>(
                request,
                CreateUri(HydraServices.PresenceService1, HydraMethods.UnknownPresenceUser1));
        }

        public async Task<UnknownPresenceResponse> UnknownPresenceMatchmake5(UnknownPresenceMatchmake5Request request)
        {
            return await PostAsync<UnknownPresenceMatchmake5Request, UnknownPresenceResponse>(
                request,
                CreateUri(HydraServices.PresenceService1, HydraMethods.UnknownPresenceMatchmake5));
        }
        
        public async Task<GetPlaylistsStatsResponse> GetPlaylistsStats(GetPlaylistsStatsRequest request)
        {
            return await PostAsync<GetPlaylistsStatsRequest, GetPlaylistsStatsResponse>(
                request,
                CreateUri(HydraServices.PresenceService1, HydraMethods.GetPlaylistsStats));
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
        
        public async Task<MatchmakeByPlaylistResponse> MatchmakeByPlaylist(MatchmakeByPlaylistRequest request)
        {
            return await PostAsync<MatchmakeByPlaylistRequest, MatchmakeByPlaylistResponse>(
                request,
                CreateUri(HydraServices.PresenceService1, HydraMethods.MatchmakeByPlaylist));
        }
        
        public async Task<Unknownc2b4cc19b0a74acb8a59186f1f7119b0Response> Unknownc2b4cc19b0a74acb8a59186f1f7119b0(Unknownc2b4cc19b0a74acb8a59186f1f7119b0Request request)
        {
            return await PostAsync<Unknownc2b4cc19b0a74acb8a59186f1f7119b0Request, Unknownc2b4cc19b0a74acb8a59186f1f7119b0Response>(
                request,
                CreateUri(HydraServices.PresenceService1, HydraMethods.Methodc2b4cc19b0a74acb8a59186f1f7119b0));
        }
        
        public async Task<SendGameClientVersionResponse> SendGameClientVersion(SendGameClientVersionRequest request)
        {
            return await PostAsync<SendGameClientVersionRequest, SendGameClientVersionResponse>(
                request,
                CreateUri(HydraServices.PresenceService1, HydraMethods.SendGameClientVersion));
        }
        
        public async Task<UnknownPresenceResponse> UnknownPresenceSquad5(UnknownPresenceSquad5Request request)
        {
            return await PostAsync<UnknownPresenceSquad5Request, UnknownPresenceResponse>(
                request,
                CreateUri(HydraServices.PresenceService1, HydraMethods.UnknownPresenceSquad5));
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
                CreateUri(HydraServices.PresenceService1, HydraMethods.UnknownPresenceSquad4));
        }
        
        public async Task<UnknownPresenceResponse> UnknownPresenceMatchmake4(UnknownPresenceMatchmake4Request request)
        {
            return await PostAsync<UnknownPresenceMatchmake4Request, UnknownPresenceResponse>(
                request,
                CreateUri(HydraServices.PresenceService1, HydraMethods.UnknownPresenceMatchmake4));
        }

        public async Task<UnknownPresenceResponse> UnknownPresenceTournament(UnknownPresenceTournamentRequest request)
        {
            return await PostAsync<UnknownPresenceTournamentRequest, UnknownPresenceResponse>(
                request,
                CreateUri(HydraServices.PresenceService1, HydraMethods.UnknownPresenceTournament));
        }

        public async Task<UpdateClientContextResponse> UpdateClientContext(UpdateClientContextRequest request)
        {
            return await PostAsync<UpdateClientContextRequest, UpdateClientContextResponse>(
                request,
                CreateUri(HydraServices.PresenceService2, HydraMethods.UpdateClientContext));
        }

        public async Task<Unknown3bbc298631fc402ea804318bd81c9e61Response> Unknown3bbc298631fc402ea804318bd81c9e61(Unknown3bbc298631fc402ea804318bd81c9e61Request request)
        {
            return await PostAsync<Unknown3bbc298631fc402ea804318bd81c9e61Request, Unknown3bbc298631fc402ea804318bd81c9e61Response>(
                request,
                CreateUri(HydraServices.PresenceService2, HydraMethods.Method3bbc298631fc402ea804318bd81c9e61));
        }

        public async Task<GetDataCenterOccupationVersionedResponse> GetDataCenterOccupationVersioned(GetDataCenterOccupationVersionedRequest request)
        {
            return await PostAsync<GetDataCenterOccupationVersionedRequest, GetDataCenterOccupationVersionedResponse>(
                request,
                CreateUri(HydraServices.UhService, HydraMethods.GetDataCenterOccupationVersioned));
        }
        
        public async Task<UnknownUhGenerationResponse> UnknownUhGeneration(UnknownUhGenerationRequest request)
        {
            return await PostAsync<UnknownUhGenerationRequest, UnknownUhGenerationResponse>(
                request,
                CreateUri(HydraServices.UhService, HydraMethods.UnknownUhGeneration));
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

        public async Task<SearchUserResponse> SearchUserNicknamePrefix(SearchUserNicknamePrefixRequest request)
        {
            return await PostAsync<SearchUserNicknamePrefixRequest, SearchUserResponse>(
                request,
                CreateUri(HydraServices.UserService4, HydraMethods.SearchUserNicknamePrefix));
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
        
        public async Task<OfferTransactionResponse> OfferTransaction(OfferTransactionRequest request)
        {
            return await PostAsync<OfferTransactionRequest, OfferTransactionResponse>(
                request,
                CreateUri(HydraServices.UserService3, HydraMethods.OfferTransaction));
        }
        
        public async Task<Unknown672167912f854154b40175aed5a203deResponse> Unknown672167912f854154b40175aed5a203de(Unknown672167912f854154b40175aed5a203deRequest request)
        {
            return await PostAsync<Unknown672167912f854154b40175aed5a203deRequest, Unknown672167912f854154b40175aed5a203deResponse>(
                request,
                CreateUri(HydraServices.UserService3, HydraMethods.Method672167912f854154b40175aed5a203de));
        }
        
        public async Task<GetTransactionsResponse> GetTransactions(GetTransactionsRequest request)
        {
            return await PostAsync<GetTransactionsRequest, GetTransactionsResponse>(
                request,
                CreateUri(HydraServices.UserService3, HydraMethods.GetTransactions));
        }
        
        public async Task<GetFollowersResponse> GetFollowers(GetFollowersRequest request)
        {
            return await PostAsync<GetFollowersRequest, GetFollowersResponse>(
                request,
                CreateUri(HydraServices.UserService1, HydraMethods.GetFollowers),
                compress: true);
        }
        
        public async Task<Unknown76bca0c11f104b59a6c23d8a566771efResponse> Unknown76bca0c11f104b59a6c23d8a566771ef(Unknown76bca0c11f104b59a6c23d8a566771efRequest request)
        {
            return await PostAsync<Unknown76bca0c11f104b59a6c23d8a566771efRequest, Unknown76bca0c11f104b59a6c23d8a566771efResponse>(
                request,
                CreateUri(HydraServices.UserService1, HydraMethods.Method76bca0c11f104b59a6c23d8a566771ef));
        }
        
        public async Task<GetSubscriptionsResponse> GetSubscriptions(GetSubscriptionsRequest request)
        {
            return await PostAsync<GetSubscriptionsRequest, GetSubscriptionsResponse>(
                request,
                CreateUri(HydraServices.UserService1, HydraMethods.GetSubscriptions),
                compress: true);
        }
        
        public async Task<Unknownb5d139571f3b48f6a6b17743b490dfbaResponse> Unknownb5d139571f3b48f6a6b17743b490dfba(Unknownb5d139571f3b48f6a6b17743b490dfbaRequest request)
        {
            return await PostAsync<Unknownb5d139571f3b48f6a6b17743b490dfbaRequest, Unknownb5d139571f3b48f6a6b17743b490dfbaResponse>(
                request,
                CreateUri(HydraServices.UserService1, HydraMethods.Methodb5d139571f3b48f6a6b17743b490dfba));
        }
        
        public async Task<UnknownUserService1Response> UnknownUserService1(UnknownUserService1Request request)
        {
            return await PostAsync<UnknownUserService1Request, UnknownUserService1Response>(
                request,
                CreateUri(HydraServices.UserService1, HydraMethods.UnknownUserService1),
                compress: true);
        }
        
        public async Task<SubscribeUserResponse> SubscribeUser(SubscribeUserRequest request)
        {
            return await PostAsync<SubscribeUserRequest, SubscribeUserResponse>(
                request,
                CreateUri(HydraServices.UserService1, HydraMethods.SubscribeUser));
        }
        
        public async Task<GetChallenges2Response> GetChallenges2(GetChallenges2Request request)
        {
            return await PostAsync<GetChallenges2Request, GetChallenges2Response>(
                request,
                CreateUri(HydraServices.UserService5, HydraMethods.GetChallenges2));
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