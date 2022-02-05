using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Atlas.Models.ViewModels
{
    public class OccurenceVM
    {
        [Required]
        public string Name { get; set; }
    }
}
