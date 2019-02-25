using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BLL.DTO;
using BLL.Models;
using Microsoft.AspNetCore.Identity;

public class AppUser : IdentityUser
{
    // Extended Properties
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public long? FacebookId { get; set; }
    public string PictureUrl { get; set; }
    //public Worker Worker { get; set; }
    //for the managers
    public ICollection<Team> Teams { get; set; }

    public static AppUser UpdateModel(AppUser appUserCurrent, UpdateUserDTO appUserChange) {
        appUserCurrent.FirstName = appUserChange.FirstName;
        appUserCurrent.LastName = appUserChange.LastName;
        appUserCurrent.Email = appUserChange.Email;
        appUserCurrent.UserName = appUserChange.UserName;
        appUserCurrent.PhoneNumber = appUserChange.PhoneNumber;
        appUserCurrent.NormalizedUserName = appUserCurrent.UserName.ToUpper();
        appUserCurrent.NormalizedEmail = appUserCurrent.Email.ToUpper();
        return appUserCurrent;
    }
}