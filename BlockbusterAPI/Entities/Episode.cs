using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockbusterAPI.Entities
{
    public record Episode: Film
    {
        public int NumberOfEpisode { get; init; }
    }
}
