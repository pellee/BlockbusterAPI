using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockbusterAPI.Entities
{
    public record User
    {
        private string name;
        private string lastName;

        public Guid Id { get; init; }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value.ToUpper(); }
        }

        public string Name
        {
            get { return name; }
            set { name = value.ToUpper(); }
        }

        public string Username { get; init; }
        public string Email { get; init; }

    }
}
