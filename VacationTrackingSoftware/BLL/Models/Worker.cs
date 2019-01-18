using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models
{
    public class Worker
    {
        public int Id { get; set; }
        public AppUser User { get; set; }
        public DateTime DateRecruitment { get; set; }
        //public TeamUser TeamUserId { get; set; }
    }
}
