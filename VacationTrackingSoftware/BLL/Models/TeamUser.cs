using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace BLL.Models
{
    public class TeamUser
    {
        public int Id { get; set; }
        public Team Team { get; set; }
        public AppUser User { get; set; }
    }
}
