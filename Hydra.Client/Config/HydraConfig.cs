using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hydra.Client.Exceptions;
using Hydra.Client.Models;

namespace Hydra.Client.Config
{
    public class HydraConfig
    {
        private readonly Dictionary<string, HydraService> _serviceMap = new Dictionary<string, HydraService>
            {
                [HydraConstants.AuthService1]       = new HydraService { Name = HydraConstants.AuthService1,       Version = new HydraServiceVersion { ServiceName = HydraConstants.AuthService1,       Version =  2, MinorVersion = 1 } },
                [HydraConstants.AuthService2]       = new HydraService { Name = HydraConstants.AuthService2,       Version = new HydraServiceVersion { ServiceName = HydraConstants.AuthService2,       Version = 11, MinorVersion = 0 } },
                [HydraConstants.UserService1]       = new HydraService { Name = HydraConstants.UserService1,       Version = new HydraServiceVersion { ServiceName = HydraConstants.UserService1,       Version =  7, MinorVersion = 0 } },
                [HydraConstants.MessageService]     = new HydraService { Name = HydraConstants.MessageService,     Version = new HydraServiceVersion { ServiceName = HydraConstants.MessageService,     Version =  6, MinorVersion = 1 } },
                [HydraConstants.PresenceService1]   = new HydraService { Name = HydraConstants.PresenceService1,   Version = new HydraServiceVersion { ServiceName = HydraConstants.PresenceService1,   Version = 29, MinorVersion = 2 } },
                [HydraConstants.PresenceService2]   = new HydraService { Name = HydraConstants.PresenceService2,   Version = new HydraServiceVersion { ServiceName = HydraConstants.PresenceService2,   Version = 12, MinorVersion = 4 } },
                [HydraConstants.GameconfigService1] = new HydraService { Name = HydraConstants.GameconfigService1, Version = new HydraServiceVersion { ServiceName = HydraConstants.GameconfigService1, Version = 12, MinorVersion = 2 } },
                [HydraConstants.AbstractService]    = new HydraService { Name = HydraConstants.AbstractService,    Version = new HydraServiceVersion { ServiceName = HydraConstants.AbstractService,    Version =  5, MinorVersion = 4 } },
                [HydraConstants.UserService2]       = new HydraService { Name = HydraConstants.UserService2,       Version = new HydraServiceVersion { ServiceName = HydraConstants.UserService2,       Version =  1, MinorVersion = 2 } },
                [HydraConstants.UserService3]       = new HydraService { Name = HydraConstants.UserService3,       Version = new HydraServiceVersion { ServiceName = HydraConstants.UserService3,       Version =  5, MinorVersion = 4 } },
                [HydraConstants.UserService4]       = new HydraService { Name = HydraConstants.UserService4,       Version = new HydraServiceVersion { ServiceName = HydraConstants.UserService4,       Version =  4, MinorVersion = 1 } },
                [HydraConstants.UserService5]       = new HydraService { Name = HydraConstants.UserService5,       Version = new HydraServiceVersion { ServiceName = HydraConstants.UserService5,       Version =  4, MinorVersion = 3 } },
                [HydraConstants.DiagnosticService]  = new HydraService { Name = HydraConstants.DiagnosticService,  Version = new HydraServiceVersion { ServiceName = HydraConstants.DiagnosticService,  Version =  1, MinorVersion = 5 } },
                [HydraConstants.UhService]          = new HydraService { Name = HydraConstants.UhService,          Version = new HydraServiceVersion { ServiceName = HydraConstants.UhService,          Version =  8, MinorVersion = 2 } },
                [HydraConstants.GameconfigService2] = new HydraService { Name = HydraConstants.GameconfigService2, Version = new HydraServiceVersion { ServiceName = HydraConstants.GameconfigService2, Version =  1, MinorVersion = 1 } },
                [HydraConstants.GameconfigService3] = new HydraService { Name = HydraConstants.GameconfigService3, Version = new HydraServiceVersion { ServiceName = HydraConstants.GameconfigService3, Version =  1, MinorVersion = 0 } },
                [HydraConstants.SharedService]      = new HydraService { Name = HydraConstants.SharedService }
            };

        public HydraServiceVersion[] GetServiceVersions()
        {
            return _serviceMap.Values.Select(v => v.Version).Where(v => v != null).ToArray();
        }
        
        public void UpdateServiceEndpoint(Endpoint endpoint)
        {
            UpdateServiceEndpoint(endpoint?.Name, endpoint);
        }

        public void UpdateServiceEndpoint(string name, Endpoint endpoint)
        {
            if (endpoint == null)
            {
                throw new HydraException($"Unable to add null endpoint {name}");
            }

            StringBuilder uriBuilder = new StringBuilder();
            switch (endpoint.Protocol)
            {
                case 1:
                case 3:
                    uriBuilder.Append("https://");
                    break;
                default:
                    throw new HydraException($"Unknown protocol: {endpoint.Protocol}");
            }
            
            uriBuilder.Append(endpoint.IP); // TODO: resolve IPV4/IPV6 name
            uriBuilder.Append(":");
            uriBuilder.Append(endpoint.Port);
            uriBuilder.Append("/");
            uriBuilder.Append(name);
            uriBuilder.Append(".svc/");
            
            HydraService service = GetHydraService(name);

            service.BaseUri = new Uri(uriBuilder.ToString());
        }

        public Uri GetServiceUri(string name)
        {
            var service = GetHydraService(name);
            if (service.BaseUri == null)
            {
                throw new HydraException($"Missing url for service {name}");
            }

            return service.BaseUri;
        }
        
        private HydraService GetHydraService(string name)
        {
            HydraService service;
            if (!_serviceMap.TryGetValue(name, out service))
            {
                throw new HydraException($"Unknown service name: {name}");
            }

            return service;
        }
    }
}