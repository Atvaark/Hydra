using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hydra.Client.Config;
using Hydra.Client.Http;
using Hydra.Client.Models;
using Hydra.Client.Models.Auth;

namespace Hydra.Client
{
    public class Program
    {
        public static int Main(string[] args)
        {
            return Start(args).Result;
        }

        private static async Task<int> Start(string[] args)
        {
            try
            {
                ProgramArgs pa = ParseArgs(args);
                await Run(pa);
                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return 1;
            }
        }

        private static ProgramArgs ParseArgs(string[] args)
        {
            if (args.Length == 0)
            {
                Console.Write("Username: ");
                string username = Console.ReadLine();
                Console.Write("Password: ");
                string password = ReadLineMasked();
                return new ProgramArgs
                {
                    Username = username,
                    Password = password
                };
            }

            if (args.Length != 2)
            {
                throw new ArgumentException("Invalid command line args");
            }

            return new ProgramArgs
            {
                Username = args[0],
                Password = args[1]
            };
        }

        private static string ReadLineMasked(char mask = '*')
        {
            var sb = new StringBuilder();
            ConsoleKeyInfo keyInfo;
            while ((keyInfo = Console.ReadKey(true)).Key != ConsoleKey.Enter)
            {
                if (!char.IsControl(keyInfo.KeyChar))
                {
                    sb.Append(keyInfo.KeyChar);
                    Console.Write(mask);
                }
                else if (keyInfo.Key == ConsoleKey.Backspace && sb.Length > 0)
                {
                    sb.Remove(sb.Length - 1, 1);

                    if (Console.CursorLeft == 0)
                    {
                        Console.SetCursorPosition(Console.BufferWidth - 1, Console.CursorTop - 1);
                        Console.Write(' ');
                        Console.SetCursorPosition(Console.BufferWidth - 1, Console.CursorTop - 1);
                    }
                    else Console.Write("\b \b");
                }
            }
            Console.WriteLine();
            return sb.ToString();
        }
        
        private static async Task Run(ProgramArgs args)
        {
            Console.WriteLine("Getting game code");
            string gameCode = await GetGameCode(args);

            Console.WriteLine("Logging in");
            var client = await Login(gameCode);
            
            Console.WriteLine("Querying data");
            await QueryData(client);
            //await TestCalls(client);
        }

        private static async Task<string> GetGameCode(ProgramArgs args)
        {
            var authConfig = new AuthConfig();
            var limiter = new RateLimiter();
            var authClient = new AuthClient(authConfig, limiter);

            var authentication = await authClient.Authenticate(new AuthRequest
            {
                username = args.Username,
                password = args.Password,
                session_id = Guid.NewGuid().ToString("D")
            });

            authClient.UpdateAuthorizationToken(authentication.token);

            var verification = await authClient.VerifySession(new VerifyRequest { session_id = authentication.session_id });
            authClient.UpdateAuthorizationToken(verification.token);

            var gamecode = await authClient.GetGamecode(authConfig.ProjectId);

            return gamecode.gamecode;
        }

        private static async Task<HydraClient> Login(string gameCode)
        {
            var hydraConfig = new HydraConfig();
            var rateLimiter = new RateLimiter();
            var client = new HydraClient(hydraConfig, rateLimiter);

            hydraConfig.UpdateServiceEndpoint(
                HydraServices.SharedService,
                new Endpoint
                {
                    Name = HydraServices.SharedService,
                    IP = "obt-shared-eds.qcapi.net", // TODO: load URL somewhere
                    Port = 11701,
                    Protocol = 1
                });

            Console.WriteLine("Getting environments");
            var environments = await client.GetEnvironments(new GetEnvironmentsRequest
            {
                versions = hydraConfig.GetServiceVersions()
            });

            hydraConfig.UpdateServiceEndpoint(HydraServices.AuthService2, environments?.data?.Environments.First()?.Endpoint);


            Console.WriteLine("Entering queue");
            const string structureVersion = "12.31";
            var loginResponse = await client.Login(new LoginRequest
            {
                login = gameCode,
                password = "",
                provider = "bnet",
                signature = new LoginSignature
                {
                    Id = Guid.NewGuid().ToString("D")
                },
                structureVersion = structureVersion,
                clientData = new LoginClientData
                {
                    BuildConfiguration = "RetailClient",
                    BuildVersion = "0.1.2589268",
                    ProductBranch = "",
                    Location = "",
                    RequestedProfession = 0,
                    LauncherType = "BETHESDA_NET"
                }
            });

            int position = -1;
            while (loginResponse.data.Token.State != 2)
            {
                if (loginResponse.data.Position != position)
                {
                    position = loginResponse.data.Position;
                    Console.WriteLine($"Queued at position {position}");
                }
                loginResponse = await client.CheckLogin(new CheckLoginRequest
                {
                    token = loginResponse.data.Token.Token
                });
            }

            var tokens = await client.GetTokens(new GetTokensRequest
            {
                token = loginResponse.data.Token.Token,
                versions = hydraConfig.GetServiceVersions()
            });

            client.UpdateAuthorizationToken(tokens.data.AuthorizationToken);
            client.UpdateAccessRoleToken(tokens.data.AccessRoleToken);

            foreach (var endpoint in tokens.data.Endpoints)
            {
                hydraConfig.UpdateServiceEndpoint(endpoint);
            }

            string authorizationToken = tokens.data.AuthorizationToken;
            string diagnosticToken = tokens.data.DiagnosticToken;
            var refreshTokensResponse = await client.RefreshTokens(new RefreshTokensRequest
            {
                tokens = new[]
                {
                    authorizationToken,
                    diagnosticToken
                }
            });

            authorizationToken = refreshTokensResponse.data.Tokens
                .Where(t => t.OldValue == authorizationToken)
                .Select(t => t.NewValue)
                .FirstOrDefault();
            //diagnosticToken = refreshTokensResponse.data.Tokens
            //    .Where(t => t.OldValue == diagnosticToken)
            //    .Select(t => t.NewValue)
            //    .FirstOrDefault();

            client.UpdateAuthorizationToken(authorizationToken);
            
            Console.WriteLine("Logged in");

            return client;
        }


        private static async Task QueryData(HydraClient client)
        {
            //GetGameConfigResponse gameConfig = await client.GetGameConfig(new GetGameConfigRequest
            //{
            //    structureVersion = structureVersion
            //});

            //GetGameConfigLayersResponse gameConfigLayers = await client.GetGameConfigLayers(new GetGameConfigLayersRequest()
            //{
            //    layers = gameConfig.data
            //});

            //await client.LoginMessaging(new LoginMessagingRequest());

            //await client.SendExternalSessionToken(new SendExternalSessionTokenRequest { externalSessionToken = tokens.data.ProviderToken });

            //GetChallengesResponse challenges = await client.GetChallenges(new GetChallengesRequest());
            //GetUserInventoryResponse userInventory = await client.GetUserInventory(new GetUserInventoryRequest());

            //GetUsersResponse users = await client.GetUsers(new GetUsersRequest
            //{
            //    users = new[]
            //    {
            //        new User
            //        {
            //            Uid = new Guid().ToString("D")
            //        }
            //    }
            //});

            GetDataCenterOccupationResponse occupation = await client.GetDataCenterOccupation(new GetDataCenterOccupationRequest
            {
                generation = 1,
                state = 1
            });

            Console.WriteLine($"UsersOnline: {occupation.data.UsersOnline}");
        }

        //private static async Task TestCalls(HydraClient client)
        //{
        //    // Calls that don't require a payload.
        //    //UnknownAuthResponse unknownAuthResponse = await client.UnknownAuth(new UnknownAuthRequest());
        //    //UnknownTokenResponse unknownTokenResponse = await client.UnknownToken(new UnknownTokenRequest());

        //    //UnknownPresence1Response unknownPresence1Response = await client.UnknownPresence1(new UnknownPresence1Request());
        //    //UnknownPresence2Response unknownPresence2Response = await client.UnknownPresence2(new UnknownPresence2Request());
        //    //UnknownPresenceResponse unknownPresenceMatchmake1Response = await client.UnknownPresenceMatchmake1(new UnknownPresenceMatchmake1Request());
        //    //UnknownPresenceResponse unknownPresenceMatchmake2Response = await client.UnknownPresenceMatchmake2(new UnknownPresenceMatchmake2Request());
        //    //UnknownPresenceResponse unknownPresenceMatchmake3Response = await client.UnknownPresenceMatchmake3(new UnknownPresenceMatchmake3Request());
        //    //UnknownPresenceResponse unknownPresenceMatchmake4Response = await client.UnknownPresenceMatchmake4(new UnknownPresenceMatchmake4Request());
        //    //UnknownPresenceResponse unknownPresenceSquad1Response = await client.UnknownPresenceSquad1(new UnknownPresenceSquad1Request());
        //    //UnknownPresenceResponse unknownPresenceSquad2Response = await client.UnknownPresenceSquad2(new UnknownPresenceSquad2Request());
        //    //UnknownPresenceResponse unknownPresenceSquad3Response = await client.UnknownPresenceSquad3(new UnknownPresenceSquad3Request());
        //    //UnknownPresenceResponse unknownPresenceSquad4Response = await client.UnknownPresenceSquad4(new UnknownPresenceSquad4Request());
        //    //UnknownPresenceResponse unknownPresenceTournamentResponse = await client.UnknownPresenceTournament(new UnknownPresenceTournamentRequest());
        //    //UnknownPresenceResponse unknownPresenceTournamentMatchResponse = await client.UnknownPresenceTournamentMatch(new UnknownPresenceTournamentMatchRequest());

        //    // Shared
        //    GetEnvironmentsResponse getDataCenterResponse = await client.GetDataCenter(new GetDataCenterRequest());

        //    // Abstract
        //    //(GetContainerResponse response, ICollection<SslContainer<ChampionLoadoutAbstractDataList>> data) getContainerByNameResponse = await client.GetContainerByName<ChampionLoadoutAbstractDataList>(new GetContainerByNameRequest { containerName = "loadout_private" });
        //    //var updateContainerRequest = new UpdateContainerRequest { containerName = getContainerByNameResponse.Item1.data.ContainerName, data = getContainerByNameResponse.Item1.data.Records.First().Record };
        //    //updateContainerRequest.data.Version += 1;
        //    //SslContainer<ChampionLoadoutAbstractDataList> containerByName = getContainerByNameResponse.data.First();
        //    //UpdateContainerResponse updateContainerResponse = await client.UpdateContainer(updateContainerRequest, containerByName);
        //    ServiceResult unknownContainerResponse = await client.UnknownContainer(new UnknownContainerRequest());
        //    //(GetContainerResponse response, ICollection<SslContainer<ChampionLoadoutAbstractDataList>> data) getContainerByUserIdResponse = await client.GetContainerByUserId<ChampionLoadoutAbstractDataList>(new GetContainerByUserIdRequest { containerName = "loadout_private", keys = new[] { Guid.Empty.ToString("D") } });

        //    // Auth 2
        //    GetTokensResponse unknownAuthTokenResponse = await client.UnknownAuthToken(new UnknownAuthTokenRequest());
        //    CheckTokenBanResponse checkTokenBanResponse = await client.CheckTokenBan(new CheckTokenBanRequest());
        //    LoginResponse unknownLoginResponse = await client.UnknownLogin(new UnknownLoginRequest());

        //    // Diagnostic
        //    ServiceResult unknownProperties1Response = await client.UnknownProperties1(new UnknownPropertiesRequest1());
        //    ServiceResult unknownProperties2Response = await client.UnknownProperties2(new UnknownPropertiesRequest2());
        //    ServiceResult sendLogResponse = await client.SendLog(new SendLogRequest());

        //    // Gameconfig 1
        //    GetLayersNewTokenResponse getLayersNewTokenResponse = await client.GetLayersNewToken(new GetLayersNewTokenRequest());

        //    // Gameconfig 2
        //    ServiceResult sendSteamOffer1 = await client.SteamInitTransaction(new SteamInitTransactionRequest());
        //    ServiceResult sendSteamOffer2 = await client.SteamFinalizeTransaction(new SteamFinalizeTransactionRequest());
        //    SteamGetCurrencyCodeResponse steamGetCurrencyCodeResponse = await client.SteamGetCurrencyCode(new SteamGetCurrencyCodeRequest());

        //    // Gameconfig 3
        //    GetGameconfig3Response getGameconfig3Response = await client.GetGameconfig3(new GetGameconfig3Request());

        //    // Message
        //    ServiceResult sendMessageForUserResponse = await client.SendMessageForUser(new SendMessageForUserRequest());
        //    //GetMessageChannelsByNameGsaResponse getMessageChannelsByNameGsaResponse = await client.GetMessageChannelsByNameGsa(new GetMessageChannelsByNameGsaRequest());
        //    //UnknownMessageServiceResponse unknownMessageServiceResponse = await client.UnknownMessageService(new UnknownMessageServiceRequest());
        //    //GetMessageChannelsResponse getMessageChannelsResponse = await client.GetMessageChannels(new GetMessageChannelsRequest());
        //    //GetMessageChannelsByNameResponse getMessageChannelsByNameResponse = await client.GetMessageChannelsByName(new GetMessageChannelsByNameRequest());
        //    //SendChannelMessageResponse sendChannelMessageResponse = await client.SendChannelMessage(new SendChannelMessageRequest());

        //    // Presence 1
        //    UnknownPresenceResponse unknownPresence3Response = await client.UnknownPresence3(new UnknownPresence3Request());
        //    UnknownPresenceResponse unknownPresenceSquadInvite1Response = await client.UnknownPresenceSquadInvite1(new SquadInvitePresenceRequest());
        //    //UpdatePresenceStateResponse updatePresenceStateResponse = await client.UpdatePresenceState(new UpdatePresenceStateRequest());
        //    //SquadInviteResponse squadInviteResponse = await client.SquadInvite(new SquadInviteRequest());
        //    //UnknownPresenceUser2Response unknownPresenceUser2Response = await client.UnknownPresenceUser2(new UnknownPresenceUser2Request());
        //    //GetUsersClientStateResponse getUsersClientStateResponse = await client.GetUsersClientState(new GetUsersClientStateRequest());
        //    //UnknownPresenceResponse unknownPresenceUser1Response = await client.UnknownPresenceUser1(new UnknownPresenceUser1Request());
        //    //UnknownPresenceResponse unknownPresenceMatchmake5Response = await client.UnknownPresenceMatchmake5(new UnknownPresenceMatchmake5Request());
        //    //GetPlaylistsStatsResponse getPlaylistsStatsResponse = await client.GetPlaylistsStats(new GetPlaylistsStatsRequest());
        //    ServiceResult sendEloRatingPresenceResponse = await client.SendEloRatingPresence(new SendEloRatingPresenceRequest());
        //    UnknownPresenceResponse sendTournamentMatchPresenceResponse = await client.SendTournamentMatchPresence(new SendTournamentMatchPresenceRequest());
        //    UnknownPresenceResponse unknownPresenceSquadInvite2Response = await client.UnknownPresenceSquadInvite2(new SquadInvitePresenceRequest());
        //    UnknownPresenceResponse unknownPresenceSquadInvite3Response = await client.UnknownPresenceSquadInvite3(new SquadInvitePresenceRequest());
        //    //MatchmakeByPlaylistResponse unknownb5e4f488c68d4e95959c0144330908f6Response = await client.MatchmakeByPlaylist(new MatchmakeByPlaylistRequest());
        //    UnknownPresenceResponse unknownPresence4Response = await client.UnknownPresence4(new UnknownPresence4Request());
        //    //SendGameClientVersionResponse sendGameClientVersionResponse = await client.SendGameClientVersion(new SendGameClientVersionRequest());
        //    //UnknownPresenceResponse unknownPresenceSquad5Response = await client.UnknownPresenceSquad5(new UnknownPresenceSquad5Request());
        //    UnknownPresenceResponse unknownPresence5Response = await client.UnknownPresence5(new UnknownPresence5Request());
        //    UnknownPresenceResponse unknownPresence6Response = await client.UnknownPresence6(new UnknownPresence6Request());
        //    UnknownPresenceResponse matchPresenceResponse = await client.MatchPresence(new MatchPresenceRequest());

        //    // Presence 2
        //    //UpdateClientContextResponse updateClientContextResponse = await client.UpdateClientContext(new UpdateClientContextRequest());
        //    GetSessionsPagedResponse getSessionsPagedResponse = await client.GetSessionsPaged(new GetSessionsPagedRequest());

        //    // Uh
        //    //GetDataCenterOccupationVersionedResponse getDataCenterOccupationVersionedResponse = await client.GetDataCenterOccupationVersioned(new GetDataCenterOccupationVersionedRequest());
        //    //UnknownUhGenerationResponse unknownUhGenerationResponse = await client.UnknownUhGeneration(new UnknownUhGenerationRequest());

        //    // User 1
        //    //GetFollowersResponse getFollowersResponse = await client.GetFollowers(new GetFollowersRequest());
        //    ServiceResult sendUserIdResponse = await client.SendUserId(new SendUserIdRequest());
        //    //GetSubscriptionsResponse getSubscriptionsResponse = await client.GetSubscriptions(new GetSubscriptionsRequest());
        //    ChangeUserIgnoreListResponse changeUserIgnoreListResponse = await client.ChangeUserIgnoreList(new ChangeUserIgnoreListRequest());
        //    //UnknownUserService1Response unknownUserService1Response = await client.UnknownUserService1(new UnknownUserService1Request());
        //    //SubscribeUserResponse subscribeUserResponse = await client.SubscribeUser(new SubscribeUserRequest());

        //    // User 2
        //    ServiceResult upgradeRuneResponse = await client.UpgradeRune(new UpgradeRuneRequest());

        //    // User 3
        //    ServiceResult giftSendResponse = await client.GiftSend(new GiftSendRequest());
        //    ServiceResult giftAcceptResponse = await client.GiftAccept(new GiftAcceptRequest());
        //    //OfferTransactionResponse offerTransactionResponse = await client.OfferTransaction(new OfferTransactionRequest());
        //    ServiceResult giftRejectResponse = await client.GiftReject(new GiftRejectRequest());
        //    //GetTransactionsResponse getTransactionsResponse = await client.GetTransactions(new GetTransactionsRequest());

        //    // User 4
        //    ServiceResult sendNicknameResponse = await client.ChangeNickname(new ChangeNicknameRequest());
        //    GetProviderUsersResponse getProviderUsersResponse = await client.GetProviderUsers(new GetProviderUsersRequest());
        //    //SearchUserResponse searchUserResponse = await client.SearchUsersByNicknamePrefix(new SearchUsersByNicknamePrefixRequest());

        //    // User 5
        //    //GetChallengesResponse getChallenges2Response = await client.GetChallenges2(new GetChallenges2Request());
        //    GetChallengeResponse getChallengeResponse = await client.GetChallenge(new GetChallengeRequest());
        //}
    }
}