using System;

namespace Hydra.Client.Models
{
    public class HydraService
    {
        public string Name { get; set; }

        public Uri BaseUri { get; set; }

        public HydraServiceVersion Version { get; set; }
    }
}