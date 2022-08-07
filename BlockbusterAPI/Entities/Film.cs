using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockbusterAPI.Entities
{
    public record Film
    {
        public string Name { get; init; }
        public string Genre { get; init; }
        public int Duration { get; init; }
        public int Year { get; init; }
        public string ShortDescription { get; init; }
        public string LongDescription { get; init; }
        public List<string> Cast { get; init; }
    }
}
