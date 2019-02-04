using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BLL.Models;

namespace VacationTrackingSoftware.ViewModels
{
    public class WorkersViewModel
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        //for Employee
        public Team Team { get; set; }
        public int Role { get; set; }
        //for Manager
        public List<Team> Teams { get; set; }
    }
}
