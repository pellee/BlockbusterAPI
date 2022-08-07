using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockbusterAPI.Entities
{
    public record User
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string LastName { get; init; }
        public string Username { get; init; }
        public string Email { get; init; }

    }
}
