using System;

namespace Hydra.Client.Config
{
    public class AuthConfig
    {
        public Uri ServiceBaseUrl { get; } = new Uri("https://services.bethesda.net/");

        public int ProjectId { get; } = 11;
    }
}