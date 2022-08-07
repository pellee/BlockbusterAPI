using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockbusterAPI.Entities
{
    public record Season
    {
        public int NumberOfSeason { get; init; }
        public List<Episode> Episodes { get; init; }
    }
}
