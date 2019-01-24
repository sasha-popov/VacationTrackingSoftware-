using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VacationTrackingSoftware.ViewModels
{
    public class ChangeStatusViewModel
    {
        [Required]
        public bool Choose { get; set; }
        [Required]
        public int Id { get; set; }
    }
}
