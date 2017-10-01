using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hydra.Client.Config;
using Hydra.Client.Http;
using Hydra.Client.Models;
using Hydra.Client.Models.Auth;
using Hydra.Client.Models.Hydra;
using Hydra.Client.Models.Unknown;

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
            //await QueryData(client);
            await TestCalls(client);
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

        private static async Task TestCalls(HydraClient client)
        {
            //// Calls that don't require a payload.
            //UnknownAuthResponse unknownAuthResponse = await client.UnknownAuth(new UnknownAuthRequest());
            //UnknownTokenResponse unknownTokenResponse = await client.UnknownToken(new UnknownTokenRequest());

            //UnknownPresence1Response unknownPresence1Response = await client.UnknownPresence1(new UnknownPresence1Request());
            //UnknownPresence2Response unknownPresence2Response = await client.UnknownPresence2(new UnknownPresence2Request());
            //UnknownPresenceResponse unknownPresenceMatchmake1Response = await client.UnknownPresenceMatchmake1(new UnknownPresenceMatchmake1Request());
            //UnknownPresenceResponse unknownPresenceMatchmake2Response = await client.UnknownPresenceMatchmake2(new UnknownPresenceMatchmake2Request());
            //UnknownPresenceResponse unknownPresenceMatchmake3Response = await client.UnknownPresenceMatchmake3(new UnknownPresenceMatchmake3Request());
            //UnknownPresenceResponse unknownPresenceMatchmake4Response = await client.UnknownPresenceMatchmake4(new UnknownPresenceMatchmake4Request());
            //UnknownPresenceResponse unknownPresenceSquad1Response = await client.UnknownPresenceSquad1(new UnknownPresenceSquad1Request());
            //UnknownPresenceResponse unknownPresenceSquad2Response = await client.UnknownPresenceSquad2(new UnknownPresenceSquad2Request());
            //UnknownPresenceResponse unknownPresenceSquad3Response = await client.UnknownPresenceSquad3(new UnknownPresenceSquad3Request());
            //UnknownPresenceResponse unknownPresenceSquad4Response = await client.UnknownPresenceSquad4(new UnknownPresenceSquad4Request());
            //UnknownPresenceResponse unknownPresenceTournamentResponse = await client.UnknownPresenceTournament(new UnknownPresenceTournamentRequest());
            //UnknownPresenceResponse unknownPresenceTournamentMatchResponse = await client.UnknownPresenceTournamentMatch(new UnknownPresenceTournamentMatchRequest());

            //// Shared
            //Unknowna187495943b54cdca28ab8355bbe5898Response unknowna187495943b54cdca28ab8355bbe5898Response = await client.Unknowna187495943b54cdca28ab8355bbe5898(new Unknowna187495943b54cdca28ab8355bbe5898Request()); // RequestContext binary version mismatch. Expected '9'. Received '61116'

            // Abstract
            (GetContainerByNameResponse, SslContainer<ChampionLoadoutAbstractDataList>) getContainerByNameResponse = await client.GetContainer<ChampionLoadoutAbstractDataList>(new GetContainerByNameRequest { containerName = "loadout_private" });
            var updateContainerRequest = new UpdateContainerRequest { containerName = getContainerByNameResponse.Item1.data.ContainerName, data = getContainerByNameResponse.Item1.data.Records.First().Record };
            updateContainerRequest.data.Version += 1;
            UpdateContainerResponse updateContainerResponse = await client.UpdateContainer(updateContainerRequest, getContainerByNameResponse.Item2);
            //Unknown43525158cd024b78be1522899e2c8e14Response unknown43525158cd024b78be1522899e2c8e14Response = await client.Unknown43525158cd024b78be1522899e2c8e14(new Unknown43525158cd024b78be1522899e2c8e14Request()); // {"retCode":353}
            //Unknown8459aa2a4bc24ba990c170cc2ffac9b4Response unknown8459aa2a4bc24ba990c170cc2ffac9b4Response = await client.Unknown8459aa2a4bc24ba990c170cc2ffac9b4(new Unknown8459aa2a4bc24ba990c170cc2ffac9b4Request()); // {"retCode":353}

            //// Auth 2
            //Unknown3e61b8fd7afe45df9bc37786a3bb621eResponse unknown3e61b8fd7afe45df9bc37786a3bb621eResponse = await client.Unknown3e61b8fd7afe45df9bc37786a3bb621e(new Unknown3e61b8fd7afe45df9bc37786a3bb621eRequest()); // {"retCode":3}
            //Unknown79b6105a955c4235b1344048b1f278cfResponse unknown79b6105a955c4235b1344048b1f278cfResponse = await client.Unknown79b6105a955c4235b1344048b1f278cf(new Unknown79b6105a955c4235b1344048b1f278cfRequest()); // {"retCode":3}
            //Unknowna9dd19e747ea46a98aed7dc6058aebdcResponse unknowna9dd19e747ea46a98aed7dc6058aebdcResponse = await client.Unknowna9dd19e747ea46a98aed7dc6058aebdc(new Unknowna9dd19e747ea46a98aed7dc6058aebdcRequest()); // {"retCode":3}

            //// Diagnostic
            //Unknown17a2ffa34c43424bb257658746e55c2dResponse unknown17a2ffa34c43424bb257658746e55c2dResponse = await client.Unknown17a2ffa34c43424bb257658746e55c2d(new Unknown17a2ffa34c43424bb257658746e55c2dRequest()); // {"retCode":347}
            //Unknown75f49a6bb7374463aecc191755b88ef8Response unknown75f49a6bb7374463aecc191755b88ef8Response = await client.Unknown75f49a6bb7374463aecc191755b88ef8(new Unknown75f49a6bb7374463aecc191755b88ef8Request()); // {"retCode":347}
            //Unknown8d3bb2f3cfa54bb0a7e3d3eabb83329aResponse unknown8d3bb2f3cfa54bb0a7e3d3eabb83329aResponse = await client.Unknown8d3bb2f3cfa54bb0a7e3d3eabb83329a(new Unknown8d3bb2f3cfa54bb0a7e3d3eabb83329aRequest()); // {"retCode":347}

            //// Gameconfig 1
            //Unknown8be9ba10a3004bab886a5ce8239a8d5cResponse unknown8be9ba10a3004bab886a5ce8239a8d5cResponse = await client.Unknown8be9ba10a3004bab886a5ce8239a8d5c(new Unknown8be9ba10a3004bab886a5ce8239a8d5cRequest()); // {"retCode":347}

            //// Gameconfig 2
            //Unknown4e0590bd5c1749d1bb60ac1dd9b524ceResponse unknown4e0590bd5c1749d1bb60ac1dd9b524ceResponse = await client.Unknown4e0590bd5c1749d1bb60ac1dd9b524ce(new Unknown4e0590bd5c1749d1bb60ac1dd9b524ceRequest()); // {"retCode":347}
            //Unknownb40a2687e1f84e808b65e95945751cd7Response unknownb40a2687e1f84e808b65e95945751cd7Response = await client.Unknownb40a2687e1f84e808b65e95945751cd7(new Unknownb40a2687e1f84e808b65e95945751cd7Request()); // {"retCode":347}
            //Unknownc05cdc1fd6f44c419591d39408d27a51Response unknownc05cdc1fd6f44c419591d39408d27a51Response = await client.Unknownc05cdc1fd6f44c419591d39408d27a51(new Unknownc05cdc1fd6f44c419591d39408d27a51Request()); // {"retCode":347}

            //// Gameconfig 3
            //Unknown818178d3eec145d6adc2f35a6d241ccdResponse unknown818178d3eec145d6adc2f35a6d241ccdResponse = await client.Unknown818178d3eec145d6adc2f35a6d241ccd(new Unknown818178d3eec145d6adc2f35a6d241ccdRequest()); // {"retCode":347}

            //// Message
            //Unknown0e7ebdaeae61462ab6eba53337b9f509Response unknown0e7ebdaeae61462ab6eba53337b9f509Response = await client.Unknown0e7ebdaeae61462ab6eba53337b9f509(new Unknown0e7ebdaeae61462ab6eba53337b9f509Request()); // {"retCode":347}
            //GetMessageChannelsByNameGsaResponse getMessageChannelsByNameGsaResponse = await client.GetMessageChannelsByNameGsa(new GetMessageChannelsByNameGsaRequest());
            //UnknownMessageServiceResponse unknownMessageServiceResponse = await client.UnknownMessageService(new UnknownMessageServiceRequest());
            //GetMessageChannelsResponse getMessageChannelsResponse = await client.GetMessageChannels(new GetMessageChannelsRequest());
            //GetMessageChannelsByNameResponse getMessageChannelsByNameResponse = await client.GetMessageChannelsByName(new GetMessageChannelsByNameRequest());
            //SendChannelMessageResponse sendChannelMessageResponse = await client.SendChannelMessage(new SendChannelMessageRequest());

            //// Presence 1
            //Unknown06e4cf76e6d148769543e0774aad4b73Response unknown06e4cf76e6d148769543e0774aad4b73Response = await client.Unknown06e4cf76e6d148769543e0774aad4b73(new Unknown06e4cf76e6d148769543e0774aad4b73Request()); // {"retCode":1}
            //Unknown1223411d79164d0497e74abf8edc922aResponse unknown1223411d79164d0497e74abf8edc922aResponse = await client.Unknown1223411d79164d0497e74abf8edc922a(new Unknown1223411d79164d0497e74abf8edc922aRequest()); // {"retCode":1}
            //UpdatePresenceStateResponse updatePresenceStateResponse = await client.UpdatePresenceState(new UpdatePresenceStateRequest());
            //SquadInviteResponse squadInviteResponse = await client.SquadInvite(new SquadInviteRequest());
            //UnknownPresenceUser2Response unknownPresenceUser2Response = await client.UnknownPresenceUser2(new UnknownPresenceUser2Request());
            //GetUsersClientStateResponse getUsersClientStateResponse = await client.GetUsersClientState(new GetUsersClientStateRequest());
            //UnknownPresenceResponse unknownPresenceUser1Response = await client.UnknownPresenceUser1(new UnknownPresenceUser1Request());
            //UnknownPresenceResponse unknownPresenceMatchmake5Response = await client.UnknownPresenceMatchmake5(new UnknownPresenceMatchmake5Request());
            //GetPlaylistsStatsResponse getPlaylistsStatsResponse = await client.GetPlaylistsStats(new GetPlaylistsStatsRequest());
            //Unknown77f0b9cf113141baa2d40e0ac53a6eb8Response unknown77f0b9cf113141baa2d40e0ac53a6eb8Response = await client.Unknown77f0b9cf113141baa2d40e0ac53a6eb8(new Unknown77f0b9cf113141baa2d40e0ac53a6eb8Request()); // {"retCode":1399}
            //Unknown7903a8b8d4ce455da96449ac0d862121Response unknown7903a8b8d4ce455da96449ac0d862121Response = await client.Unknown7903a8b8d4ce455da96449ac0d862121(new Unknown7903a8b8d4ce455da96449ac0d862121Request()); // {"retCode":1}
            //Unknown914980e66e5c4b6d973a291ffb094677Response unknown914980e66e5c4b6d973a291ffb094677Response = await client.Unknown914980e66e5c4b6d973a291ffb094677(new Unknown914980e66e5c4b6d973a291ffb094677Request()); // {"retCode":1}
            //Unknown9ab159368de34865b9c5f4b0b5bdb9e3Response unknown9ab159368de34865b9c5f4b0b5bdb9e3Response = await client.Unknown9ab159368de34865b9c5f4b0b5bdb9e3(new Unknown9ab159368de34865b9c5f4b0b5bdb9e3Request()); // {"retCode":1}
            //MatchmakeByPlaylistResponse unknownb5e4f488c68d4e95959c0144330908f6Response = await client.MatchmakeByPlaylist(new MatchmakeByPlaylistRequest()); // {"retCode":1350}
            //Unknownc2b4cc19b0a74acb8a59186f1f7119b0Response unknownc2b4cc19b0a74acb8a59186f1f7119b0Response = await client.Unknownc2b4cc19b0a74acb8a59186f1f7119b0(new Unknownc2b4cc19b0a74acb8a59186f1f7119b0Request()); // {"retCode":1353}
            //SendGameClientVersionResponse sendGameClientVersionResponse = await client.SendGameClientVersion(new SendGameClientVersionRequest());
            //UnknownPresenceResponse unknownPresenceSquad5Response = await client.UnknownPresenceSquad5(new UnknownPresenceSquad5Request());
            //Unknownda785897995d47fc83b96b40f41bcb59Response unknownda785897995d47fc83b96b40f41bcb59Response = await client.Unknownda785897995d47fc83b96b40f41bcb59(new Unknownda785897995d47fc83b96b40f41bcb59Request()); // {"retCode":1389}
            //Unknowndac0fa31a1a241f8b5a8e033e8e497e6Response unknowndac0fa31a1a241f8b5a8e033e8e497e6Response = await client.Unknowndac0fa31a1a241f8b5a8e033e8e497e6(new Unknowndac0fa31a1a241f8b5a8e033e8e497e6Request()); // {"retCode":1389}
            //Unknowne4c06258140645bbb39c63a8a0cd230eResponse unknowne4c06258140645bbb39c63a8a0cd230eResponse = await client.Unknowne4c06258140645bbb39c63a8a0cd230e(new Unknowne4c06258140645bbb39c63a8a0cd230eRequest()); // {"retCode":1360}

            //// Presence 2
            //UpdateClientContextResponse updateClientContextResponse = await client.UpdateClientContext(new UpdateClientContextRequest());
            //Unknown3bbc298631fc402ea804318bd81c9e61Response unknown3bbc298631fc402ea804318bd81c9e61Response = await client.Unknown3bbc298631fc402ea804318bd81c9e61(new Unknown3bbc298631fc402ea804318bd81c9e61Request()); // {"retCode":347}

            //// Uh
            //GetDataCenterOccupationVersionedResponse getDataCenterOccupationVersionedResponse = await client.GetDataCenterOccupationVersioned(new GetDataCenterOccupationVersionedRequest());
            //UnknownUhGenerationResponse unknownUhGenerationResponse = await client.UnknownUhGeneration(new UnknownUhGenerationRequest());

            //// User 1
            //GetFollowersResponse getFollowersResponse = await client.GetFollowers(new GetFollowersRequest());
            //Unknown76bca0c11f104b59a6c23d8a566771efResponse unknown76bca0c11f104b59a6c23d8a566771efResponse = await client.Unknown76bca0c11f104b59a6c23d8a566771ef(new Unknown76bca0c11f104b59a6c23d8a566771efRequest()); // {"retCode":347}
            //GetSubscriptionsResponse getSubscriptionsResponse = await client.GetSubscriptions(new GetSubscriptionsRequest());
            //Unknownb5d139571f3b48f6a6b17743b490dfbaResponse unknownb5d139571f3b48f6a6b17743b490dfbaResponse = await client.Unknownb5d139571f3b48f6a6b17743b490dfba(new Unknownb5d139571f3b48f6a6b17743b490dfbaRequest()); // {"retCode":347}
            //UnknownUserService1Response unknownUserService1Response = await client.UnknownUserService1(new UnknownUserService1Request());
            //SubscribeUserResponse subscribeUserResponse = await client.SubscribeUser(new SubscribeUserRequest());

            //// User 2
            //Unknown8117a48a84f942dea6e057c53f06fbe5Response unknown8117a48a84f942dea6e057c53f06fbe5Response = await client.Unknown8117a48a84f942dea6e057c53f06fbe5(new Unknown8117a48a84f942dea6e057c53f06fbe5Request()); // {"retCode":347}

            //// User 3
            //Unknown1df66e3e7245462e9cbd24c6ccb78219Response unknown1df66e3e7245462e9cbd24c6ccb78219Response = await client.Unknown1df66e3e7245462e9cbd24c6ccb78219(new Unknown1df66e3e7245462e9cbd24c6ccb78219Request()); // {"retCode":347}
            //Unknown29db08d7f205403b96d2c661c5a21fb5Response unknown29db08d7f205403b96d2c661c5a21fb5Response = await client.Unknown29db08d7f205403b96d2c661c5a21fb5(new Unknown29db08d7f205403b96d2c661c5a21fb5Request()); // {"retCode":347}
            //OfferTransactionResponse offerTransactionResponse = await client.OfferTransaction(new OfferTransactionRequest());
            //Unknown672167912f854154b40175aed5a203deResponse unknown672167912f854154b40175aed5a203deResponse = await client.Unknown672167912f854154b40175aed5a203de(new Unknown672167912f854154b40175aed5a203deRequest()); // {"retCode":347}
            //GetTransactionsResponse getTransactionsResponse = await client.GetTransactions(new GetTransactionsRequest());

            //// User 4
            //Unknown255c934598874d688ac024552a4c8184Response unknown255c934598874d688ac024552a4c8184Response = await client.Unknown255c934598874d688ac024552a4c8184(new Unknown255c934598874d688ac024552a4c8184Request()); // {"retCode":347}
            //Unknown605000618bbf46078491664420dd524aResponse unknown605000618bbf46078491664420dd524aResponse = await client.Unknown605000618bbf46078491664420dd524a(new Unknown605000618bbf46078491664420dd524aRequest()); // {"retCode":347}
            //SearchUserResponse searchUserResponse = await client.SearchUserNicknamePrefix(new SearchUserNicknamePrefixRequest());

            //// User 5
            //GetChallenges2Response getChallenges2Response = await client.GetChallenges2(new GetChallenges2Request());
            //Unknownbc099ffb41aa45c38525b0d40d7309a0Response unknownbc099ffb41aa45c38525b0d40d7309a0Response = await client.Unknownbc099ffb41aa45c38525b0d40d7309a0(new Unknownbc099ffb41aa45c38525b0d40d7309a0Request()); // {"retCode":347}
        }
    }
}