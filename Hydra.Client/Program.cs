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
                HydraConstants.SharedService,
                new Endpoint
                {
                    Name = HydraConstants.SharedService,
                    IP = "obt-shared-eds.qcapi.net",
                    Port = 11701,
                    Protocol = 1
                });

            Console.WriteLine("Getting environments");
            var environments = await client.GetEnvironments(new GetEnvironmentsRequest
            {
                versions = hydraConfig.GetServiceVersions()
            });

            hydraConfig.UpdateServiceEndpoint(HydraConstants.AuthService2, environments?.data?.Environments.First()?.Endpoint);


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
            
            Console.WriteLine($"Logged in");

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
    }
}