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
        
        public async Task<GetEnvironmentsResponse> GetDataCenter(GetDataCenterRequest request)
        {
            // Untested
            return await PostAsync<GetDataCenterRequest, GetEnvironmentsResponse>(
                request,
                CreateUri(HydraServices.SharedService, HydraMethods.GetDataCenter));
        }

        public async Task<UpdateContainerResponse> UpdateContainer<T>(UpdateContainerRequest request, SslContainer<T> requestData) where T : SslType
        {
            var response = await PostHydraAsync<
                UpdateContainerRequest,
                SslContainer<T>,
                UpdateContainerResponse,
                NullHydraServiceData>(
                request,
                requestData,
                CreateUri(HydraServices.AbstractService, HydraMethods.UpdateContainer),
                compress: true);
            return response.Item1;
        }
        
        public async Task<(GetContainerResponse response, ICollection<SslContainer<T>> data)> GetContainerByName<T>(GetContainerByNameRequest request) where T: SslType
        {
            return await PostHydraAsync<
                GetContainerByNameRequest,
                NullHydraServiceData,
                GetContainerResponse,
                SslContainer<T>>(
                    request,
                    NullHydraServiceData.Null,
                    CreateUri(HydraServices.AbstractService, HydraMethods.GetContainerByName),
                    compress: true
            );
        }

        public async Task<ServiceResult> UnknownContainer(UnknownContainerRequest request)
        {
            // Untested. Hydra?
            return await PostAsync<UnknownContainerRequest, ServiceResult>(
                request,
                CreateUri(HydraServices.AbstractService, HydraMethods.UnknownContainer));
        }

        public async Task<(GetContainerResponse response, ICollection<SslContainer<T>> data)> GetContainerByUserId<T>(GetContainerByUserIdRequest request)
            where T : SslType
        {
            return await PostHydraAsync<
                GetContainerByUserIdRequest,
                NullHydraServiceData,
                GetContainerResponse,
                SslContainer<T>>(
                request,
                NullHydraServiceData.Null,
                CreateUri(HydraServices.AbstractService, HydraMethods.GetContainerByUserId),
                compress: true);
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
        
        public async Task<GetTokensResponse> UnknownAuthToken(UnknownAuthTokenRequest request)
        {
            // Untested
            return await PostAsync<UnknownAuthTokenRequest, GetTokensResponse>(
                request,
                CreateUri(HydraServices.AuthService2, HydraMethods.UnknownAuthToken));
        }
        
        public async Task<CheckTokenBanResponse> CheckTokenBan(CheckTokenBanRequest request)
        {
            // Untested
            return await PostAsync<CheckTokenBanRequest, CheckTokenBanResponse>(
                request,
                CreateUri(HydraServices.AuthService2, HydraMethods.CheckTokenBan));
        }
        
        public async Task<LoginResponse> UnknownLogin(UnknownLoginRequest request)
        {
            // Untested
            return await PostAsync<UnknownLoginRequest, LoginResponse>(
                request,
                CreateUri(HydraServices.AuthService2, HydraMethods.UnknownLogin));
        }

        public async Task<ServiceResult> UnknownProperties1(UnknownPropertiesRequest1 request)
        {
            // Untested
            return await PostAsync<UnknownPropertiesRequest1, ServiceResult>(
                request,
                CreateUri(HydraServices.DiagnosticService, HydraMethods.UnknownProperties1));
        }
        
        public async Task<ServiceResult> UnknownProperties2(UnknownPropertiesRequest2 request)
        {
            // Untested
            return await PostAsync<UnknownPropertiesRequest2, ServiceResult>(
                request,
                CreateUri(HydraServices.DiagnosticService, HydraMethods.UnknownProperties2));
        }
        
        public async Task<ServiceResult> SendLog(SendLogRequest request)
        {
            // Untested
            return await PostAsync<SendLogRequest, ServiceResult>(
                request,
                CreateUri(HydraServices.DiagnosticService, HydraMethods.SendLog));
        }
        
        public async Task<GetLayersNewTokenResponse> GetLayersNewToken(GetLayersNewTokenRequest request)
        {
            // Untested
            return await PostAsync<GetLayersNewTokenRequest, GetLayersNewTokenResponse>(
                request,
                CreateUri(HydraServices.GameconfigService1, HydraMethods.GetLayersNewToken));
        }
        
        public async Task<ServiceResult> SteamInitTransaction(SteamInitTransactionRequest request)
        {
            // Untested
            return await PostAsync<SteamInitTransactionRequest, ServiceResult>(
                request,
                CreateUri(HydraServices.GameconfigService2, HydraMethods.SteamInitTransaction));
        }
        
        public async Task<ServiceResult> SteamFinalizeTransaction(SteamFinalizeTransactionRequest request)
        {
            // Untested
            return await PostAsync<SteamFinalizeTransactionRequest, ServiceResult>(
                request,
                CreateUri(HydraServices.GameconfigService2, HydraMethods.SteamFinalizeTransaction));
        }
        
        public async Task<SteamGetCurrencyCodeResponse> SteamGetCurrencyCode(SteamGetCurrencyCodeRequest request)
        {
            // Untested
            return await PostAsync<SteamGetCurrencyCodeRequest, SteamGetCurrencyCodeResponse>(
                request,
                CreateUri(HydraServices.GameconfigService2, HydraMethods.SteamGetCurrencyCode));
        }
        
        public async Task<GetGameconfig3Response> GetGameconfig3(GetGameconfig3Request request)
        {
            // Untested
            return await PostAsync<GetGameconfig3Request, GetGameconfig3Response>(
                request,
                CreateUri(HydraServices.GameconfigService3, HydraMethods.GetGameconfig3));
        }
        
        public async Task<ServiceResult> SendMessageForUser(SendMessageForUserRequest request)
        {
            // Untested
            return await PostAsync<SendMessageForUserRequest, ServiceResult>(
                request,
                CreateUri(HydraServices.MessageService, HydraMethods.SendMessageForUser));
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
        
        public async Task<UnknownPresenceResponse> UnknownPresence3(UnknownPresence3Request request)
        {
            // Untested
            return await PostAsync<UnknownPresence3Request, UnknownPresenceResponse>(
                request,
                CreateUri(HydraServices.PresenceService1, HydraMethods.UnknownPresence3));
        }
        
        public async Task<UnknownPresence1Response> UnknownPresence1(UnknownPresence1Request request)
        {
            return await PostAsync<UnknownPresence1Request, UnknownPresence1Response>(
                request,
                CreateUri(HydraServices.PresenceService1, HydraMethods.UnknownPresence1));
        }
        
        public async Task<UnknownPresenceResponse> UnknownPresenceSquadInvite1(SquadInvitePresenceRequest request)
        {
            // Untested
            return await PostAsync<SquadInvitePresenceRequest, UnknownPresenceResponse>(
                request,
                CreateUri(HydraServices.PresenceService1, HydraMethods.UnknownPresenceSquadInvite1));
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
        
        public async Task<ServiceResult> SendEloRatingPresence(SendEloRatingPresenceRequest request)
        {
            // Untested
            return await PostAsync<SendEloRatingPresenceRequest, ServiceResult>(
                request,
                CreateUri(HydraServices.PresenceService1, HydraMethods.SendEloRatingPresence));
        }

        public async Task<UnknownPresenceResponse> SendTournamentMatchPresence(SendTournamentMatchPresenceRequest request)
        {
            // Untested
            return await PostAsync<SendTournamentMatchPresenceRequest, UnknownPresenceResponse>(
                request,
                CreateUri(HydraServices.PresenceService1, HydraMethods.SendTournamentMatchPresenceResponse));
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

        public async Task<UnknownPresenceResponse> UnknownPresenceSquadInvite2(SquadInvitePresenceRequest request)
        {
            // Untested
            return await PostAsync<SquadInvitePresenceRequest, UnknownPresenceResponse>(
                request,
                CreateUri(HydraServices.PresenceService1, HydraMethods.UnknownPresenceSquadInvite2));
        }

        public async Task<UnknownPresenceResponse> UnknownPresenceSquadInvite3(SquadInvitePresenceRequest request)
        {
            // Untested
            return await PostAsync<SquadInvitePresenceRequest, UnknownPresenceResponse>(
                request,
                CreateUri(HydraServices.PresenceService1, HydraMethods.UnknownPresenceSquadInvite3));
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
        
        public async Task<UnknownPresenceResponse> UnknownPresence4(UnknownPresence4Request request)
        {
            // Untested
            return await PostAsync<UnknownPresence4Request, UnknownPresenceResponse>(
                request,
                CreateUri(HydraServices.PresenceService1, HydraMethods.UnknownPresence4));
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

        public async Task<UnknownPresenceResponse> UnknownPresence5(UnknownPresence5Request request)
        {
            // Untested
            return await PostAsync<UnknownPresence5Request, UnknownPresenceResponse>(
                request,
                CreateUri(HydraServices.PresenceService1, HydraMethods.UnknownPresence5));
        }

        public async Task<UnknownPresenceResponse> UnknownPresence6(UnknownPresence6Request request)
        {
            // Untested
            return await PostAsync<UnknownPresence6Request, UnknownPresenceResponse>(
                request,
                CreateUri(HydraServices.PresenceService1, HydraMethods.UnknownPresence6));
        }

        public async Task<UnknownPresenceResponse> MatchPresence(MatchPresenceRequest request)
        {
            // Untested
            return await PostAsync<MatchPresenceRequest, UnknownPresenceResponse>(
                request,
                CreateUri(HydraServices.PresenceService1, HydraMethods.MatchPresence));
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

        public async Task<GetSessionsPagedResponse> GetSessionsPaged(GetSessionsPagedRequest request)
        {
            return await PostAsync<GetSessionsPagedRequest, GetSessionsPagedResponse>(
                request,
                CreateUri(HydraServices.PresenceService2, HydraMethods.GetSessionsPaged));
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
        

        public async Task<ServiceResult> ChangeNickname(ChangeNicknameRequest request)
        {
            return await PostAsync<ChangeNicknameRequest, ServiceResult>(
                request,
                CreateUri(HydraServices.UserService4, HydraMethods.ChangeNickname));
        }
        
        public async Task<GetProviderUsersResponse> GetProviderUsers(GetProviderUsersRequest request)
        {
            return await PostAsync<GetProviderUsersRequest, GetProviderUsersResponse>(
                request,
                CreateUri(HydraServices.UserService4, HydraMethods.GetProviderUsers));
        }

        public async Task<SearchUserResponse> SearchUsersByNicknamePrefix(SearchUsersByNicknamePrefixRequest request)
        {
            return await PostAsync<SearchUsersByNicknamePrefixRequest, SearchUserResponse>(
                request,
                CreateUri(HydraServices.UserService4, HydraMethods.SearchUserNicknamePrefix));
        }
        
        public async Task<ServiceResult> GiftSend(GiftSendRequest request)
        {
            return await PostAsync<GiftSendRequest, ServiceResult>(
                request,
                CreateUri(HydraServices.UserService3, HydraMethods.GiftSend));
        }
        
        public async Task<ServiceResult> GiftAccept(GiftAcceptRequest request)
        {
            return await PostAsync<GiftAcceptRequest, ServiceResult>(
                request,
                CreateUri(HydraServices.UserService3, HydraMethods.GiftAccept));
        }
        
        public async Task<OfferTransactionResponse> OfferTransaction(OfferTransactionRequest request)
        {
            return await PostAsync<OfferTransactionRequest, OfferTransactionResponse>(
                request,
                CreateUri(HydraServices.UserService3, HydraMethods.OfferTransaction));
        }
        
        public async Task<ServiceResult> GiftReject(GiftRejectRequest request)
        {
            return await PostAsync<GiftRejectRequest, ServiceResult>(
                request,
                CreateUri(HydraServices.UserService3, HydraMethods.GiftReject));
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
        
        public async Task<ServiceResult> SendUserId(SendUserIdRequest request)
        {
            return await PostAsync<SendUserIdRequest, ServiceResult>(
                request,
                CreateUri(HydraServices.UserService1, HydraMethods.SendUserId));
        }
        
        public async Task<GetSubscriptionsResponse> GetSubscriptions(GetSubscriptionsRequest request)
        {
            return await PostAsync<GetSubscriptionsRequest, GetSubscriptionsResponse>(
                request,
                CreateUri(HydraServices.UserService1, HydraMethods.GetSubscriptions),
                compress: true);
        }
        
        public async Task<ChangeUserIgnoreListResponse> ChangeUserIgnoreList(ChangeUserIgnoreListRequest request)
        {
            return await PostAsync<ChangeUserIgnoreListRequest, ChangeUserIgnoreListResponse>(
                request,
                CreateUri(HydraServices.UserService1, HydraMethods.ChangeUserIgnoreList));
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
        
        public async Task<GetChallengesResponse> GetChallenges2(GetChallenges2Request request)
        {
            return await PostAsync<GetChallenges2Request, GetChallengesResponse>(
                request,
                CreateUri(HydraServices.UserService5, HydraMethods.GetChallenges2));
        }
        
        public async Task<GetChallengeResponse> GetChallenge(GetChallengeRequest request)
        {
            return await PostAsync<GetChallengeRequest, GetChallengeResponse>(
                request,
                CreateUri(HydraServices.UserService5, HydraMethods.GetChallenge));
        }
        
        public async Task<ServiceResult> UpgradeRune(UpgradeRuneRequest request)
        {
            return await PostAsync<UpgradeRuneRequest, ServiceResult>(
                request,
                CreateUri(HydraServices.UserService2, HydraMethods.UpgradeRune));
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
        
        private async Task<(TResponseHead, ICollection<TResponseData>)> PostHydraAsync<TRequestHead, TRequestData, TResponseHead, TResponseData>(
            TRequestHead requestHead,
            TRequestData requestData,
            Uri url,
            bool compress = false,
            bool expectContinue = false,
            TimeSpan? rateLimit = null)
            where TResponseHead : ServiceResult
        {
            HttpContent content = compress
                ? HttpContentUtil.CreateCompressedHydraContent(requestHead, requestData)
                : HttpContentUtil.CreateHydraContent(requestHead, requestData);
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, url)
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

            bool compressedResponse = responseMessage.Headers.TryGetValues("Compression-Enabled", out var compressHeaders) && compressHeaders.Contains("true");

            (TResponseHead, ICollection<TResponseData>) response;
            if (responseMessage.Content.Headers.ContentType.MediaType == "application/x-hydra-binary")
            {
                if (compressedResponse)
                {
                    response = await HttpContentUtil.ReadCompressedHydraContent<TResponseHead, TResponseData>(responseMessage.Content);
                }
                else
                {
                    response = await HttpContentUtil.ReadHydraContent<TResponseHead, TResponseData>(responseMessage.Content);
                }
            }
            else
            {
                throw new ApplicationException("Missing service response");
            }
            
            if (response.Item1.retCode != 0)
            {
                throw ServiceFaultException.Create(response.Item1.retCode);
            }

            return response;
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

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, url)
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

            var compressedResponse = responseMessage.Headers.TryGetValues("Compression-Enabled", out var compressHeaders) && compressHeaders.Contains("true");

            TResponse response;
            if (responseMessage.Content.Headers.ContentType.MediaType == "application/json")
            {
                if (compressedResponse)
                {
                    response = await HttpContentUtil.ReadCompressedJsonContent<TResponse>(responseMessage.Content);
                }
                else
                {
                    response = await HttpContentUtil.ReadJsonContent<TResponse>(responseMessage.Content);
                }
            }
            else 
            {
                response = null;
            }

            if (response == null)
            {
                throw new ApplicationException("Missing service response");
            }

            if (response.retCode != 0)
            {
                throw ServiceFaultException.Create(response.retCode);
            }

            return response;
        }

    }
}