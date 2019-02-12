using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VacationTrackingSoftware.ViewModels
{
    public class UpdateUserTeam
    {
        [Required]
        public string UserId { get; set; }
        //if the user have status employee
        public int TeamId { get; set; }
        //if the user have status hrUser 
        public List<int> TeamIds { get; set; }

        public int Role { get; set; }
    }
}
