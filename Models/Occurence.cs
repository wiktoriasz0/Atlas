using System;
using System.Collections.Generic;

namespace Atlas.Models
{
    public class Occurence
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public List<MushroomInOccurence> MushroomInOccurences { get; set; }
    }
}
