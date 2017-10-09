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
                [HydraServices.AuthService1]       = new HydraService { Name = HydraServices.AuthService1,       Version = new HydraServiceVersion { ServiceName = HydraServices.AuthService1,       Version =  2, MinorVersion = 1 } },
                [HydraServices.AuthService2]       = new HydraService { Name = HydraServices.AuthService2,       Version = new HydraServiceVersion { ServiceName = HydraServices.AuthService2,       Version = 11, MinorVersion = 0 } },
                [HydraServices.UserService1]       = new HydraService { Name = HydraServices.UserService1,       Version = new HydraServiceVersion { ServiceName = HydraServices.UserService1,       Version =  7, MinorVersion = 0 } },
                [HydraServices.MessageService]     = new HydraService { Name = HydraServices.MessageService,     Version = new HydraServiceVersion { ServiceName = HydraServices.MessageService,     Version =  6, MinorVersion = 1 } },
                [HydraServices.PresenceService1]   = new HydraService { Name = HydraServices.PresenceService1,   Version = new HydraServiceVersion { ServiceName = HydraServices.PresenceService1,   Version = 29, MinorVersion = 2 } },
                [HydraServices.PresenceService2]   = new HydraService { Name = HydraServices.PresenceService2,   Version = new HydraServiceVersion { ServiceName = HydraServices.PresenceService2,   Version = 12, MinorVersion = 4 } },
                [HydraServices.GameconfigService1] = new HydraService { Name = HydraServices.GameconfigService1, Version = new HydraServiceVersion { ServiceName = HydraServices.GameconfigService1, Version = 12, MinorVersion = 2 } },
                [HydraServices.AbstractService]    = new HydraService { Name = HydraServices.AbstractService,    Version = new HydraServiceVersion { ServiceName = HydraServices.AbstractService,    Version =  5, MinorVersion = 4 } },
                [HydraServices.UserService2]       = new HydraService { Name = HydraServices.UserService2,       Version = new HydraServiceVersion { ServiceName = HydraServices.UserService2,       Version =  1, MinorVersion = 2 } },
                [HydraServices.UserService3]       = new HydraService { Name = HydraServices.UserService3,       Version = new HydraServiceVersion { ServiceName = HydraServices.UserService3,       Version =  5, MinorVersion = 4 } },
                [HydraServices.UserService4]       = new HydraService { Name = HydraServices.UserService4,       Version = new HydraServiceVersion { ServiceName = HydraServices.UserService4,       Version =  4, MinorVersion = 1 } },
                [HydraServices.UserService5]       = new HydraService { Name = HydraServices.UserService5,       Version = new HydraServiceVersion { ServiceName = HydraServices.UserService5,       Version =  4, MinorVersion = 3 } },
                [HydraServices.DiagnosticService]  = new HydraService { Name = HydraServices.DiagnosticService,  Version = new HydraServiceVersion { ServiceName = HydraServices.DiagnosticService,  Version =  1, MinorVersion = 5 } },
                [HydraServices.UhService]          = new HydraService { Name = HydraServices.UhService,          Version = new HydraServiceVersion { ServiceName = HydraServices.UhService,          Version =  8, MinorVersion = 2 } },
                [HydraServices.GameconfigService2] = new HydraService { Name = HydraServices.GameconfigService2, Version = new HydraServiceVersion { ServiceName = HydraServices.GameconfigService2, Version =  1, MinorVersion = 1 } },
                [HydraServices.GameconfigService3] = new HydraService { Name = HydraServices.GameconfigService3, Version = new HydraServiceVersion { ServiceName = HydraServices.GameconfigService3, Version =  1, MinorVersion = 0 } },
                [HydraServices.SharedService]      = new HydraService { Name = HydraServices.SharedService }
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
            if (!_serviceMap.TryGetValue(name, out HydraService service))
            {
                throw new HydraException($"Unknown service name: {name}");
            }

            return service;
        }
    }
}