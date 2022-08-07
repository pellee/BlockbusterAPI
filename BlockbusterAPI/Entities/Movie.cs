using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockbusterAPI.Entities
{
    public record Movie: Film
    {
        public Guid Id { get; init; }

    }
}
