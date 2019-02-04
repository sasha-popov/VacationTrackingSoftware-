using System.Collections.Generic;
using BLL.Models;
using Microsoft.AspNetCore.Identity;

public class AppUser : IdentityUser
{
    // Extended Properties
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public long? FacebookId { get; set; }
    public string PictureUrl { get; set; }
    //public Worker Worker { get; set; }
    //for the managers
    public ICollection<Team> Teams { get; set; }
}