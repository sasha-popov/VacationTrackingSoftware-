using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.DTO
{
    public class UserVacationRequestDTO
    {
        [Required]
        public int Id { get; set; }
        public string UserName { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public string VacationType { get; set; }
        [Required]
        public int Status { get; set; }
        [Required]
        public int Payment { get; set; }
    }
}
