using System;
using System.Collections.Generic;

namespace Atlas.Models
{
    public class Mushroom
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Edibility { get; set; }
        public string LatinName { get; set; }
        public string CommonName { get; set; }
        public string Family { get; set; }
        public string Kind { get; set; }
        public string Genre { get; set; }
        public DateTime Create { get; set; }
        public DateTime Update { get; set; }
        public string Url { get; set; }
        public List<MushroomInOccurence> MushroomInOccurence { get; set; }
    }
}
