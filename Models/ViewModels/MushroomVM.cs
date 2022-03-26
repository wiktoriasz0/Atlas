using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Atlas.Models.ViewModels
{
    public class MushroomVM
    {
        public string ID { get; set; }
        /// <summary>
        /// Nazwa grzyba
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Pełny opis grzyba
        /// </summary>
        [Display(Name = "Opis grzyba:")]
        public string Description { get; set; }
        public bool Edibility { get; set; }
        [Required]
        public string LatinName { get; set; }

        /// <summary>
        /// Wystepowanie
        /// </summary>
        public string Occurence { get; set; }
        public string Family { get; set; }
        public string Kind { get; set; }
        [Display(Name="Gatunek grzyba:")]
        public string Genre { get; set; }
    }
}
