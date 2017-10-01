using System;
using Hydra.Client.Models;

namespace Hydra.Client
{
    public class HydraService
    {
        public string Name { get; set; }

        public Uri BaseUri { get; set; }

        public HydraServiceVersion Version { get; set; }
    }
}