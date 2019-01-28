using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace BLL.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //check if user have role "Manager"
        [JsonIgnore]
        public AppUser Manager { get; set; }

        public ICollection<Project> Projects { get; set; }
        //add only employees
        public ICollection<TeamUser> TeamUsers { get; set; }
    }
}

