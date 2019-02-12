using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VacationTrackingSoftware.ViewModels
{
    public class RegistrationManagerViewModel
    {
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        //public string Location { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        public string Role { get; set; }
        public List<int> TeamsId { get; set; }
    }
}
