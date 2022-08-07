using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockbusterAPI.Entities
{
    public record TvShow
    {
        public Guid Id { get; init; }
        public List<Season> Seasons { get; init; }
    }
}
